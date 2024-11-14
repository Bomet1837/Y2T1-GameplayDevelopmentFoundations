using System.Collections;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Animations;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text speakerText;
    public TMP_Text dialogueText;
    public GameObject dialogueObject;
    public GameObject choiceButtonPrefab;
    public Transform choiceButtonContainer;

    public GameObject FadeUI;
    public GameObject nextButton;

    private DialogueNode currentNode;
    private bool isProcessingChoice = false; //Used to check if the script is waiting for a choice input
    
    private AudioSource lastAudioSource;

    private bool isGamePaused;

    void Start()
    {
        if (PauseMenuManager.Instance != null)
        {
            PauseMenuManager.Instance.OnPausedStatusChanged.AddListener(OnPausedChanged);
        }
    }

    void Update()
    {
        //This is a way to force show the cursor when it hides
        if (dialogueObject.activeInHierarchy && Cursor.visible == false)
        {
            Debug.LogWarning("Cursor invisible, making visible");
            
            GameManager.instance.ToggleCursor(true);
        }
    }

    public void StartDialogue(StartNode startNode)
    {
        currentNode = startNode.GetNextNode();
        ProcessNode();
    }

    public void OnNextButtonClicked()
    {
        AudioManager.instance.PlayUIClick();
        
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
        if (!isGamePaused)
        {
            ClearChoiceButtons(); //Clear the choice buttons

            nextButton.SetActive(true);

            if (currentNode is MonologueNode monologueNode)
            {
                DisplayMonologue(monologueNode);
            }
            else if (currentNode is ChoiceNode choiceNode)
            {
                nextButton.SetActive(false);
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
                currentNode = playerFreezeNode.GetNextNode();
                ProcessNode();
            }
            else if (currentNode is CameraSwitchNode cameraSwitchNode)
            {
                cameraSwitchNode.GetNextNode();
                currentNode = cameraSwitchNode.GetNextNode();
                ProcessNode();
            }
            else if (currentNode is DestroyNode destroyNode)
            {
                destroyNode.GetNextNode();
                currentNode = destroyNode.GetNextNode();
                ProcessNode();
            }
            else if (currentNode is EnableObjectNode enableObjectNode)
            {
                enableObjectNode.GetNextNode();
                currentNode = enableObjectNode.GetNextNode();
                ProcessNode();
            }
            else if (currentNode is MoveObjectNode moveObjectNode)
            {
                moveObjectNode.GetNextNode();
                currentNode = moveObjectNode.GetNextNode();
                ProcessNode();
            }
            else if (currentNode is ClearNode clearNode)
            {
                DisplayEmpty(clearNode);
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
        else
        {
            return;
        }
    }
    
    private void DisplayEmpty(ClearNode node)
    {
        speakerText.text = "";
        dialogueText.text = "";
        PlayAudioClip(null);
        
        dialogueObject.SetActive(false);
        
        currentNode = node.GetNextNode();
    }


    private void DisplayMonologue(MonologueNode node)
    {
        dialogueObject.SetActive(true);
        
        speakerText.text = node.speakerName;
        dialogueText.text = node.dialogueText;

        //Play the audio
        PlayAudioClip(node.audioClip);
        
        //Play the animation
        TriggerAnimation(node);

        currentNode = node.GetNextNode();
    }

    private void DisplayChoices(ChoiceNode node)
    {
        PlayAudioClip(node.audioClip);
        
        TriggerAnimation(node);
        
        speakerText.text = node.speakerName;
        dialogueText.text = node.dialogueText;

        isProcessingChoice = true; //The user should now be in a choice, do not allow the next button to work

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
        AudioManager.instance.PlayUIClick();
        
        ClearChoiceButtons(); //Clear the choice buttons when
        currentNode = node.GetNextNodeForChoice(choiceIndex); //Get the next node based on the output of the choice
        isProcessingChoice = false; //Enable the next button
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
        // Stop the previous audio if it's playing
        if (lastAudioSource != null && lastAudioSource.isPlaying)
        {
            lastAudioSource.Stop();
            Destroy(lastAudioSource.gameObject); // Clean up the AudioSource
        }

        if (clip != null)
        {
            // Get the currently active camera
            Camera activeCamera = Camera.allCameras.FirstOrDefault(cam => cam.isActiveAndEnabled);

            if (activeCamera != null)
            {
                // Create a new GameObject for the AudioSource at the camera's position
                GameObject audioObject = new GameObject("DialogueAudioSource");
                audioObject.transform.position = activeCamera.transform.position;

                // Add an AudioSource component, configure it, and play the clip
                lastAudioSource = audioObject.AddComponent<AudioSource>();
                lastAudioSource.clip = clip;
                lastAudioSource.Play();

                // Destroy the AudioSource GameObject after the clip finishes
                Destroy(audioObject, clip.length);
            }
        }
    }


    private void TriggerAnimation(DialogueNode node)
    {
        if (node.animationClip != null && !string.IsNullOrEmpty(node.targetAnimationObjectName))
        {
            GameObject targetObject = DialogueUtilities.FindObjectByName(node.targetAnimationObjectName);

            if (targetObject != null)
            {
                Animation animation = targetObject.GetComponent<Animation>();
                if (animation == null)
                {
                    animation = targetObject.AddComponent<Animation>();
                }

                animation.clip = node.animationClip;
                animation.Play();
            }
            else
            {
                Debug.LogError("Target animation object not found: " + node.targetAnimationObjectName);
            }
        }
    }


    private IEnumerator SwitchScene(int sceneIndex)
    {
        FadeUI.SetActive(true);
        
        yield return new WaitForSeconds(1.5f);
        
        SceneManager.LoadScene(sceneIndex);
    }
    
    private void OnPausedChanged(bool isPaused)
    {
        isGamePaused = isPaused;
    }
}
