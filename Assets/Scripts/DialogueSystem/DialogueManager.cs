using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public NPC npc;
    bool isTalking = false;
    float distance;
    float currentResponseTracker = 0f;
    public GameObject player;
    public GameObject dialogueUI;

    public TMP_Text npcName;
    public TMP_Text npcDialogueBox;
    public TMP_Text playerResponse;

    void Start()
    {
        dialogueUI.SetActive(true);
    }

    void OnMouseOver()
    {
        distance = Vector3.Distance(player.transform.position, this.transform.position);

        if(distance <= 2.5f)
        {
            //Trigger dialogue
            if (Input.GetKeyDown(KeyCode.E) && !isTalking)
            {
                StartConversation();
            }
            else if(Input.GetKeyDown(KeyCode.E) && isTalking)
            {
                EndDialogue();
            }
        }
    }

    void StartConversation()
    {
        isTalking = true;
        currentResponseTracker = 0f;
        dialogueUI.SetActive(true);
        npcName.text = npc.name;
        npcDialogueBox.text = npc.dialogue[0];
    }

    void EndDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);
    }
}
