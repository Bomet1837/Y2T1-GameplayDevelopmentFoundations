using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DEV_ConsoleController : MonoBehaviour
{
    public TMP_Text outputText;
    public TMP_InputField inputBox;

    void Start()
    {
        inputBox.onEndEdit.AddListener(OnEndEdit);
    }

    void OnEndEdit(string command)
    {
        //Waiting for the enter key
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SubmitCommand();
        }
    }

    void SubmitCommand()
    {
        //Trim extra spaces
        string command = inputBox.text.Trim();

        inputBox.text = "";

        //If nothing is entered, ignore
        if (string.IsNullOrEmpty(command))
        {
            return;
        }

        outputText.text += "\n > " + command + "\n";

        //Process the command
        ProcessCommand(command);
    }

    private void ProcessCommand(string command)
    {
        //Split the command by spaces to tell what is the prefix and suffix
        string[] splitCommand = command.Split(' ');

        //"help" command
        if (splitCommand[0].ToLower() == "help")
        {
            ShowHelp();
        }
        //"LoadScene" command
        else if (splitCommand[0].ToLower() == "loadscene")
        {
            if (splitCommand.Length > 1)
            {
                //Load the scene with the specific name (1 being the split word)
                LoadScene(splitCommand[1]);
            }
            else
            {
                //If no scene is entered, show help
                outputText.text += "Scenes you can load: \n MainMenu \n FirstPersonMovementDemo";
            }
        }
        //"Clear" command
        else if(splitCommand[0].ToLower() == "clear")
        {
            outputText.text = "";
        }
        else
        {
            //Unknown command
            outputText.text += "Unknown command: " + command + "\n";
        }
    }

    //"Help" command
    private void ShowHelp()
    {
        outputText.text += "Help: \n LoadScene - Show a list of available scenes \n LoadScene SCENENAME - Load a specific scene \n Clear - Clears the console";
    }

    //"LoadScene" comand
    private void LoadScene(string sceneName)
    {
        try
        {
            SceneManager.LoadScene(sceneName);
            outputText.text += "Loading scene: " + sceneName + "\n";
        }
        catch
        {
            outputText.text += "Failed to load scene: " + sceneName + "\n";
        }
    }
}
