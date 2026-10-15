# Starting Eitr Bonus

Why don't you start with any Eitr? You are a dead Viking brought back to life by Odin that magicly respawns when you die. 

This is a small mod that adds a configurable base Eitr bonus. This mod patches Valheim's total food stat calculation and adds the configured amount to Eitr as a base bonus. 

You can set `BaseEitrBonus` to any value from `0` to `250`. The default config gives the player `25` Eitr as a base amount, the same base value as your starting health. Eating food that gives Eitr will add to this amount.

## Configuration

After the first launch, edit:
```text
BepInEx/config/com.badbatch.valheim.startingeitrbonus.cfg
```
or use the in-game configuration menu to change the values.

Options:
```ini
[General]
Enabled = true
BaseEitrBonus = 25
```

Conflicts:
```text
If you are using another mod that changes the starting eitr amount, there might be conflicts. 
I have tested this mod with a few magic mods like Wizardry, RtDMythic, and EitrMagicExtended and did not have any problems.
If you any issues , please let me know and I will try to fix it.
```

Warning:
This mod is a learning experience for me. I'm not good at programmer, so I used AI to help generate some parts of the code. As I learn more, I will slowly rebuild this mod by hand. 

Question, Complains, pointers or interested in turning this into something more useful? 
Head over to my discord page. [BadBatch Mod](https://discord.gg/a7DwPCt5U) 
or visit the [GitHub Repo](https://github.com/Goibniu75/StartingEitrBonus/tree/master)

```text
Revisions:Revisions:
1.0.4 - Updated code to work properly with Valheim Deep North Update.
1.0.3 - Verified that the mod works with Valheim Deep North Update. 
1.0.2 - Changed Max Eitr Bonus to 250. 
1.0.1 - Added Mod information to ReadMe file.
```
