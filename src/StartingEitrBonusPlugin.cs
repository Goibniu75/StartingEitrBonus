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
        var targetMethod =
            AccessTools.Method(typeof(Player), "GetTotalFoodValue")
            ?? AccessTools.Method(typeof(Player), "getTotalFoodValue");

        if (targetMethod == null)
        {
            Logger.LogWarning("Could not find Player.GetTotalFoodValue. The Eitr bonus patch was not applied.");
            return;
        }

        var postfix = new HarmonyMethod(
            typeof(PlayerGetTotalFoodValuePatch),
            nameof(PlayerGetTotalFoodValuePatch.Postfix));

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
