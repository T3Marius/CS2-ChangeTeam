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
[Tag]
Tag = "{red}[ChangeTeams]{default} "

[Permissions]
FLAGS = ["@css/generic"]

[CommandsAdmin]

# Admin commands configuration
MoveTerrorist = ["movet"]  # Commands for moving to the Terrorist team
MoveCounterTerrorist = ["movect"]  # Commands for moving to the Counter-Terrorist team
MoveSpectate = ["spec"]  # Commands for moving to the Spectator team
Swapper = ["swap"]  # Command for swapping teams

[CommandsPlayer]

# Player commands configuration
Terrorist = ["t"]  # Commands for players to join the Terrorist team
CounterTerrorist = ["ct"]  # Commands for players to join the Counter-Terrorist team
Spectate = ["afk"]  # Commands for players to join the Spectator team
```




