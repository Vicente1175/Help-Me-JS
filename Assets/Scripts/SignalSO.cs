using UnityEngine;

[CreateAssetMenu(fileName = "NewSignal", menuName = "GameScript/Signal")]
public class SignalSO : ScriptableObject
{
	[Header("Identificación")]
	public string signalId;

	[Header("Información de la señal")]
	public string signalType;

	[TextArea(2, 4)]
	public string description;

	[Header("Condición")]
	public string requiredCondition;

	[Header("Feedback")]
	public string feedbackNodeId;
}