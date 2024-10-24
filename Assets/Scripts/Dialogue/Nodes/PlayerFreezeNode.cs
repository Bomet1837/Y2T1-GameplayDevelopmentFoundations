using UnityEngine;

[CreateAssetMenu(fileName = "PlayerFreezeNode", menuName = "Dialogue/PlayerFreezeNode")]
public class PlayerFreezeNode : DialogueNode
{
    [Output] public DialogueNode nextNode;
    
    [Header("Do not add anything into the fields above.")]

    public bool freezePlayer = true;

    // Used to continue to the next node
    public override DialogueNode GetNextNode()
    {
        if (freezePlayer == true)
        {
            FirstPersonMovement.instance.FreezePlayer();
        }
        else if(freezePlayer == false)
        {
            FirstPersonMovement.instance.UnfreezePlayer();
        }
        else
        {
            Debug.LogError("PlayerFreezeNode can't find the FirstPersonMovement script.");
        }
        
        return GetOutputPort("nextNode").Connection.node as DialogueNode;
    }
}