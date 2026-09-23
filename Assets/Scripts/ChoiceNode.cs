using System;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewChoiceNode", menuName = "GameScript/Nodes/Choice Node")]
public class ChoiceNode : GameScriptNode
{
	[TextArea(2, 4)]
	public string question;

	public List<ChoiceOption> options = new List<ChoiceOption>();
}

[Serializable]
public class ChoiceOption
{
	public string optionText;

	public string nextNodeId;

	public int variableEffect;
}