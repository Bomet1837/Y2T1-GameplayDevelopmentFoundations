using UnityEngine;

[CreateAssetMenu(fileName = "MonologueNode", menuName = "Dialogue/MonologueNode")]
public class MonologueNode : DialogueNode
{
    [Output] public DialogueNode nextNode;

    //Used to continue to the next node
    public override DialogueNode GetNextNode()
    {
        return GetOutputPort("nextNode").Connection.node as DialogueNode;
    }
}
