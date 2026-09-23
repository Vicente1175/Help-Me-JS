using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueNode", menuName = "GameScript/Nodes/Dialogue Node")]
public class DialogueNode : GameScriptNode
{
	public DialogueSequenceSO dialogueSequence;

	public string nextNodeId;
}