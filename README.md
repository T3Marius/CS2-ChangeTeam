# CS2-ChangeTeams
# Info / Colors
```
This plugin is created for easier transfer through the teams. It has admin commands too for faster moving.
COLORS:
{red}
{blue}
{purple}
{green}
{yellow}
{magenta}
{lime}
{darkred}
{darkblue}
{lightred}
{orange}
{gold}
{olive}

```

# Commands
```
css_t (player can go to terrorist)
css_ct (player can go to counterterrorist)
css_afk (player can go to spectate)
ALL THE COMMANDS ARE CONFIGURABLE IN CONFIG FILE.
```
# Config
```
{
  "Tag": "{red}[SWAP]{default} ",
  "Permissions": ["@css/generic"], // flags who have acces to move commands.
  "CommandsPlayer": {
    "Terrorist": [
      "t",
      "terrorist"
    ],
    "CounterTerrorist": [
      "ct",
      "counterterrorist"
    ],
    "Spectator": [
      "afk",
      "spectate"
    ]
  }, // commands used by admin, able to move players through teams.
  "CommandsAdmin": {
    "Terrorist": [
      "movet", // !movet {playername}
      "moveterrorist"
    ],
    "CounterTerrorist": [
      "movect",  // !movect {playername}
      "movecounterterrorist"
    ],
    "Spectator": [
      "spec", // !spec {playername}
      "movespectate"
    ],
    "Swapper": [
      "swap" // !swap {playername} if a player is terrorist and you use swap command on him it swaps him to counter terrorist.
    ]
  },
  "ConfigVersion": 1
}
```




