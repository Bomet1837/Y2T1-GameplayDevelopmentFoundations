V1
* Imported demo first person controller
* Created a working main menu template
* Created a dev console accessable by pressing left shift, right shift, 5, 7 (type "help" for a list of commands)

V2
* Updated dev console with new commands
* Updated first person controller (audio). Converted multiple functions into one single function.

V3
* Hud UI has been added to the FirstPersonDemo
* Branching dialogue system has been created with support for xNode (a node based graph editor) for ease of use with users and it supports the following:
- Choice node: This allows us to add choices for the user to choose
- Start + End nodes: These mark the start and end of each graph (dialogue tree)
- Monologue node: This is for dialogue only and does not require the user to make a choice
- Player Pref Node: This uses Unity's built in playerpref system to save choices made by the player
* Audio has been implemented for the monologue but it has not been tested
* Animations have a placeholder function but there is no functionality
* A DialogueDemo scene has been created with a trigger (which is activated by enabling the gameobject) - This will show all the current features in the dialogue system
* Imported: xNode (https://github.com/Siccity/xNode) to allow us to create graphs
* Imported: PlayerPrefs Editor (https://assetstore.unity.com/packages/tools/utilities/playerprefs-editor-167903) to allow us to easily manipulate the playerprefs via the Unity UI
* Demos of the development for the dialogue system can be found here: https://www.youtube.com/watch?v=djgXvdNBEYc https://www.youtube.com/watch?v=dqqKegGubGQ

V4
* Merged the grey box court scene with the main game.

V5
* Added Nodes: CameraSwitchNode, PlayerFreezeNode, SceneSwitchNode
* Updated Player: Removed unnecessary functions, added freeze and unfreeze function
* Created a dialogue tree for the grey box level

V6
* Updated DialogueManager: Tested and fixed the PlayAudioClip() function