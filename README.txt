Notes:
There are deviations from the architecture notes in the script names and quantity. "Apply[Effect]" has been consolidated into the DestroyByContact script, and the DestroyByContact scripts will be re-written for each powerup prefab to apply the desired effects. This will be easier to manage. Also of note is that "Rotator" is now "MorselAnimator". This decision was made under the realization than any number of objects may require rotations, and so "Rotator" is too general for script management.

The following are discussed interfaces used for cross-script functionality. These function names and types will remain permanent so as to maintain consistency between modules.

PlayerController:
SetSpeed(float) - Used by powerup to set player speed.
GetSpeed() - Useful getter method for implementation-independent speed altering.

Example uses:
player.GetComponent<PlayerController>().SetSpeed(50); //sets player speed to 50.
layer.GetComponent<PlayerController>().SetSpeed(2 * PlayerController.GetSpeed()); //doubles player's current speed.



GameController:
AddMorsel() - Used for incrementing the morsel count for the level.
PlayerDestroy() - Used for handling player death event. Used by level boundary box script and any player-killing level structures.

Example uses:
gc.GetComponent<GameController>().PlayerDestroy();
gc.GetComponent<GameController>().AddMorsel(); //upon entering a morsel's collider.



CameraController:
SetPlayer(GameObject) - Used for removing player GameObject pointer upon player death,
prevents camera from following dead player or attempting to follow non-existing player GameObject.

Example uses:
camera.GetComponent<CameraController>().SetPlayer(null); //upon player death, used for CameraController conditional check before updating position.
camera.GetComponent<CameraController>().SetPlayer((GameObject)Instantiate(player), startPosition, startRotation); //upon player respawn, if needed.