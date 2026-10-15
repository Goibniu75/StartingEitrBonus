using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace StartingEitrBonus;

[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
public sealed class StartingEitrBonusPlugin : BaseUnityPlugin
{
    public const string PluginGuid = "badbatch.valheim.startingeitrbonus";
    public const string PluginName = "Starting Eitr Bonus";
    public const string PluginVersion = "1.0.4";

    internal static ConfigEntry<bool> ModEnabled = null!;
    internal static ConfigEntry<float> BaseEitrBonus = null!;

    private readonly Harmony _harmony = new(PluginGuid);

    private void Awake()
    {
        ModEnabled = Config.Bind(
            "General",
            "Enabled",
            true,
            "Enable or disable the starting Eitr bonus.");

        BaseEitrBonus = Config.Bind(
            "General",
            "BaseEitrBonus",
            25f,
            new ConfigDescription(
                "Amount of base Eitr to add before food bonuses. Set to 0 to keep vanilla behavior.",
                new AcceptableValueRange<float>(0f, 250f)));

        PatchTotalFoodValue();
        Logger.LogInfo($"{PluginName} {PluginVersion} loaded. Base Eitr bonus: {BaseEitrBonus.Value}");
    }

    private void OnDestroy()
    {
        _harmony.UnpatchSelf();
    }

    private void PatchTotalFoodValue()
    {
        // Try known legacy and newer method names first, then fall back to reflection-based discovery.
        string[] candidateNames = new[]
        {
            "GetTotalFoodValue",
            "getTotalFoodValue",
            "GetTotalEitr",
            "GetTotalEitrValue",
            "GetTotalFoodEitr",
            "GetEitrFromFood",
            "GetTotalEitrFromFood",
        };

        System.Reflection.MethodInfo? targetMethod = null;
        foreach (var name in candidateNames)
        {
            targetMethod = AccessTools.Method(typeof(Player), name);
            if (targetMethod != null)
                break;
        }

        // Fallback: find a parameterless instance method returning float whose name contains 'Total' and ('Food' or 'Eitr')
        if (targetMethod == null)
        {
            var methods = typeof(Player).GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            foreach (var m in methods)
            {
                if (m.ReturnType == typeof(float) && m.GetParameters().Length == 0)
                {
                    var n = m.Name.ToLowerInvariant();
                    if (n.Contains("total") && (n.Contains("food") || n.Contains("eitr")))
                    {
                        targetMethod = m;
                        break;
                    }
                }
            }
        }

        if (targetMethod == null)
        {
            Logger.LogWarning("Could not find Player total Eitr/food value method. The Eitr bonus patch was not applied.");
            return;
        }

        var postfix = new HarmonyMethod(typeof(PlayerGetTotalFoodValuePatch), nameof(PlayerGetTotalFoodValuePatch.Postfix));
        _harmony.Patch(targetMethod, postfix: postfix);
    }
}

internal static class PlayerGetTotalFoodValuePatch
{
    internal static void Postfix(ref float eitr)
    {
        if (!StartingEitrBonusPlugin.ModEnabled.Value)
        {
            return;
        }

        float bonus = StartingEitrBonusPlugin.BaseEitrBonus.Value;
        if (bonus <= 0f)
        {
            return;
        }

        eitr += bonus;
    }
}
