Currently the following ActionTypes are valid:

# MoveForward
Moves the player in the currently looking direction.

Parameters:

| Name | Type | default | Description |
|-|-|-|-|
| stepsize | double | 1.0 | Move distance per step |
| direction | double | 0.0 | Direction in degrees to move relative to looking direction |

# Rotate
Rotates the player clockwise the specified amount of degrees in <duration> steps, rotation/duration per step.

Parameters:

| Name | Type | default | Description |
|-|-|-|-|
| rotation | double | -90.0 | Clockwise rotation in degrees |

# FollowPath
Follows the path of another player.

Parameters:

| Name | Type | default | Description |
|-|-|-|-|
| dependant | int | 0 | The index of the position in the current formation from which the path should be followed |
| timeDiff | int | 0 | How long the following should be delayed. A timeDiff of 0 makes the player stand in the exact same spot. timeDiff 2 would stand exactly where the dependant was standing 2 steps before |
| minTime | int | 0 |  If the localTime - timeDiff is smaller than minTime the position of the player will not be changed |

# CopyMovement
Copies the movement of another player.

Parameters:

| Name | Type | default | Description |
|-|-|-|-|
| dependant | int | 0 | The index of the position in the current formation from which the movement should be copied |
| timeDiff | int | 0 | How long the copying should be delayed. A timeDiff of 0 copies the movement at the current time. timeDiff 2 would perform the movement the dependant did 2 steps ago |
| minTime | int | 0 | If the localTime - timeDiff is smaller than minTime the position of the player will not be changed |

# MoveUpTo
Moves up to another player and then follows in the given distance.

Parameters:

| Name | Type | default | Description |
|-|-|-|-|
| dependant | int | 0 | The index of the position in the current formation to which should be moved up to |
| depth | double | 1.0 | How close the player should move up to the dependant (formation depth) |
| stepsize | double | 1.0 | Move distance per step until the specified depth is reached |
