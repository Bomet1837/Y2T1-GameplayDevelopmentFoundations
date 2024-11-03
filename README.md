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
* Used https://ttsmp3.com for the text to speech

V7
* Added animations into the monologue and choice nodes
* Created DestroyNode to destroy game objects from within the dialogue graph
* Moved the FindObjectByName function into DialogueUtilities for reusability across scripts within the dialogue system

V8 
* Created demo character objects for the grey box scene to give a more realistic view on how the characters will look
* Added textures onto the grey box demo
* Created an EnableObjectNode script
* Added a freezeOnStart bool to the FirstPersonMovement script to freeze the player on start if the developer wishes with the option to also allow the cam or freeze the cam with it using the enableCamOnFreeze bool
* New node: Move object
* New node: Comment node
* Edit node: EnableObjectNode: Add option to disable object

V9
* Added the template scenes into the main project
* Fixed the pause menu - It now shows and hides the mouse correctly.
* Created an options menu - There is a visual representation of an options menu available. There is a script available for it in Scripts/MainMenu/Options however, it is incomplete. The options menu is there to show for the prototype.
* Removed the next button when making a choice
* Fixed the UI formatting
* Added "ReloadScene" command into the dev console
* Added "RestartGame" command into the dev console
* Added start logos and information to the main menu, this will show on load of the game. You can press space to skip this.
* Created an empty world template in /Scenes/Levels/Templates with all the parts necessary for a scene. This can be copied and expanded upon to create a new scene.
* Fixed cursor enable/disable on start - The cursor is now controlled by the GameManager instance

V10
* Created a scene template for the Level. This can be found by going to "File/New Scene" and clicking "Level Template"
* Added Cinzel as the main font
* Created a hint popup
* Fixed the canvas scaler with the following:
  * UI Scale Mode: Scale With Screen Size
  * Reference Resolution: X: 1920, Y: 1080
  * Screen Match Mode: Match Width Or Height
  * Match: 0.5
* Created a Hint_TriggerTemplate script in Scripts/Hint/HintTriggers which can be copied to create a trigger for when and if hints are needed