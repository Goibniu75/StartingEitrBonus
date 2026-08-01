# Starting Eitr Bonus

Why don't you start with any Eitr? You are a dead Viking brought back to life by Odin that magicly respawns when you die. This is a small mod that adds a configurable base Eitr bonus. This mod patches Valheim's total food stat calculation and adds the configured amount to Eitr as a base bonus. 

You could set `BaseEitrBonus` to any value from `0` to `250`. The default config gives the player `25` Eitr as a base amount, the same base value as your starting health. Eatting food that gives Eitr will add to this amount.

## Configuration

After the first launch, edit:
```text
BepInEx/config/com.badbatch.valheim.startingeitrbonus.cfg
```

Options:
```ini
[General]
Enabled = true
BaseEitrBonus = 25
```
Warning:
This mod is a learning experience for me. I'm not good at programmer, so I used AI to help generate some parts of the code. As I learn more, I will slowly rebuild this mod by hand. 

Question, Complains, pointers or interested in turning this into something more useful? Head over to this mod's discord or my github page.
[StartingEitrBonus](https://discord.gg/t3MQyhrCC)

Revisions:
1.0.3 - Added revision info to Mod Profile. Changed name of config file.
1.0.2 - Changed Max Eitr Bonus to 250. 
1.0.1 - Added Mod information to ReadMe file.
