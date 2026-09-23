using UnityEngine;

[CreateAssetMenu(fileName = "NewSignalNode", menuName = "GameScript/Nodes/Signal Node")]
public class SignalNode : GameScriptNode
{
	[Header("Señal")]
	public SignalSO signal;

	[Header("Flujo")]
	public string nextNodeId;
}