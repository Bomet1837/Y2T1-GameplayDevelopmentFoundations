using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueGraph dialogueGraph;
    public DialogueManager dialogueManager;

    public bool destroyOnTrigger = true;

    private void OnEnable()
    {
        if (dialogueManager != null && dialogueGraph != null)
        {
            EnableDialogueUi();
            
            //The GetStartNode method is used to get the StartNode from the graph
            StartNode startNode = dialogueGraph.GetStartNode();

            if (startNode != null)
            {
                dialogueManager.StartDialogue(startNode);

                if (destroyOnTrigger)
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                Debug.LogWarning("DialogueTrigger: There is no StartNode in the dialogue graph.");
            }
        }
        else
        {
            Debug.LogWarning("DialogueTrigger: DialogueManager or DialogueGraph does not exist.");
        }
    }

    private void OnDisable()
    {
        //TODO: Add cleanup
    }

    private void EnableDialogueUi()
    {
        dialogueManager.gameObject.SetActive(true);
    }
}
