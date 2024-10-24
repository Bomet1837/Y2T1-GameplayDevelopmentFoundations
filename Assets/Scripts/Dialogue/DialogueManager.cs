using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text speakerText;
    public TMP_Text dialogueText;
    public GameObject choiceButtonPrefab;
    public Transform choiceButtonContainer;

    public GameObject FadeUI;

    private DialogueNode currentNode;
    private bool isProcessingChoice = false; //Used to check if the script is waiting for a choice input

    public void StartDialogue(StartNode startNode)
    {
        currentNode = startNode.GetNextNode();
        ProcessNode();
    }

    public void OnNextButtonClicked()
    {
        if (isProcessingChoice)
        {
            //If waiting for a choice, wait until a choice is made
            return;
        }

        if (currentNode != null)
        {
            ProcessNode(); //Continue to the next node
        }
    }

    private void ProcessNode()
    {
        ClearChoiceButtons(); //Clear the choice buttons

        if (currentNode is MonologueNode monologueNode)
        {
            DisplayMonologue(monologueNode);
        }
        else if (currentNode is ChoiceNode choiceNode)
        {
            DisplayChoices(choiceNode);
        }
        else if (currentNode is PlayerPrefNode playerPrefNode)
        {
            //Automatically process the PlayerPrefNode - This will automatically save and move to the next node
            //This should be invisible to the user
            playerPrefNode.GetNextNode(); //Save the value
            currentNode = playerPrefNode.GetNextNode(); //Get the next node
            ProcessNode(); //Process the next node
        }
        else if (currentNode is PlayerFreezeNode playerFreezeNode)
        {
            //Automatically process the PlayerFreezeNode - This will automatically save and move to the next node
            //This should be invisible to the user
            currentNode = playerFreezeNode.GetNextNode(); //Get the next node
            ProcessNode(); //Process the next node
        }
        else if (currentNode is CameraSwitchNode cameraSwitchNode)
        {
            //Automatically process the CameraSwitchNode - This will automatically save and move to the next node
            //This should be invisible to the user
            cameraSwitchNode.GetNextNode(); //Save the value
            currentNode = cameraSwitchNode.GetNextNode(); //Get the next node
            ProcessNode(); //Process the next node
        }
        else if (currentNode is SwitchSceneNode switchSceneNode)
        {
            //Fade and switch to the next scene, we do not need to do anything else
            StartCoroutine(SwitchScene(switchSceneNode.sceneIndex));
        }
        else if (currentNode is EndNode)
        {
            //If we see an end node, end the dialogue
            EndDialogue();
        }
    }


    private void DisplayMonologue(MonologueNode node)
    {
        speakerText.text = node.speakerName;
        dialogueText.text = node.dialogueText;

        //Play the audio and animation
        //TODO: Test this, it is untested and just threw in there
        PlayAudioClip(node.audioClip);
        TriggerAnimation(node.animationClip);

        currentNode = node.GetNextNode(); //Mode to the next node when the next button is pressed
                                          //TODO: Make it continue automatically when the audio has finished
                                          //      This can be done by either setting a float for time - Like with Aisle 21
                                          //      Or make it finish when the audio finishes (preferred)
    }

    private void DisplayChoices(ChoiceNode node)
    {
        speakerText.text = node.speakerName;
        dialogueText.text = node.dialogueText;

        isProcessingChoice = true; //The user should now be in a choice, do not allow the next button to work
                                   //TODO: Hide the next button

        for (int i = 0; i < node.choices.Length; i++)
        {
            var choice = node.choices[i];
            GameObject buttonObject = Instantiate(choiceButtonPrefab, choiceButtonContainer); //Instantiate a new button
            Button choiceButton = buttonObject.GetComponent<Button>();
            TMP_Text buttonText = choiceButton.GetComponentInChildren<TMP_Text>();

            buttonText.text = choice.choiceText; //Set the text of the instantiated button
            int index = i;
            choiceButton.onClick.RemoveAllListeners();
            choiceButton.onClick.AddListener(() => SelectChoice(node, index)); //Add a listener to the button to select a choice
        }
    }

    //When a button is selected
    private void SelectChoice(ChoiceNode node, int choiceIndex)
    {
        ClearChoiceButtons(); //Clear the choice buttons when
        currentNode = node.GetNextNodeForChoice(choiceIndex); //Get the next node based on the output of the choice
        isProcessingChoice = false; //Enable the next button
                                    //TODO: The oposite of making he button hide - Show the next button
        ProcessNode(); //Continue to the next node
    }

    private void ClearChoiceButtons()
    {
        foreach (Transform child in choiceButtonContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void EndDialogue()
    {
        //Reset everything for next use
        dialogueText.text = "";
        speakerText.text = "";

        this.gameObject.SetActive(false);
    }

    private void PlayAudioClip(AudioClip clip)
    {
        if (clip != null)
        {
            //TODO: Test this
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        }
    }

    private void TriggerAnimation(AnimationClip animationClip)
    {
        if (animationClip != null)
        {
            //TODO: Make animations work using the animator component
        }
    }

    private IEnumerator SwitchScene(int sceneIndex)
    {
        FadeUI.SetActive(true);
        
        yield return new WaitForSeconds(1.5f);
        
        SceneManager.LoadScene(sceneIndex);
    }
}
