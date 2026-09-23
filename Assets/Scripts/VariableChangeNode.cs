using UnityEngine;

public enum GameVariable
{
	EmotionalWellbeing,
	Energy,
	HelpProgress
}

[CreateAssetMenu(fileName = "NewVariableChangeNode", menuName = "GameScript/Nodes/Variable Change Node")]
public class VariableChangeNode : GameScriptNode
{
	[Header("Variable")]
	public GameVariable variable;

	[Header("Cambio")]
	public int amount;

	[Header("Flujo")]
	public string nextNodeId;
}