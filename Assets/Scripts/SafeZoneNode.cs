using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSafeZoneNode", menuName = "GameScript/Nodes/Safe Zone Node")]
public class SafeZoneNode : GameScriptNode
{
	[Header("Zona segura")]
	public SafeZoneDataSO safeZone;

	[Header("Flujo")]
	public string nextNodeId;
}

[CreateAssetMenu(fileName = "NewSafeZoneData", menuName = "GameScript/Safe Zone Data")]
public class SafeZoneDataSO : ScriptableObject
{
	[Header("Identificación")]
	public string zoneId;

	[Header("Reflexión")]
	[TextArea(2, 4)]
	public string reflectionPrompt;

	[Header("Opciones")]
	public List<string> options = new List<string>();

	[Header("Feedback")]
	public string feedbackNodeId;
}