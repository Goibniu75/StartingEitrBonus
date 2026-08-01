# Starting Eitr Bonus

A small Valheim BepInEx mod that adds a configurable base Eitr bonus.

By default, Valheim starts players at `0` Eitr unless food provides an Eitr value. This mod patches Valheim's total food stat calculation and adds the configured amount to Eitr as a base bonus. With the default config, a player with no Eitr food has `25` Eitr.

## Configuration

After the first launch, edit:

```text
BepInEx/config/com.bcbry.valheim.startingeitrbonus.cfg
```

Options:

```ini
[General]
Enabled = true
BaseEitrBonus = 25
```

Set `BaseEitrBonus` to any value from `0` to `250`.

## Build

Install BepInEx for Valheim, then build with:

```powershell
dotnet build .\StartingEitrBonus.csproj -c Release
```

If Valheim is installed somewhere other than the default Steam location, pass the install path:

```powershell
dotnet build .\StartingEitrBonus.csproj -c Release -p:ValheimInstallDir="D:\SteamLibrary\steamapps\common\Valheim"
```

The compiled file will be:

```text
bin/Release/net472/StartingEitrBonus.dll
```

## Install

Copy `StartingEitrBonus.dll` into:

```text
Valheim/BepInEx/plugins/StartingEitrBonus/
```

Start Valheim once to generate the config file, then adjust `BaseEitrBonus` as desired.
