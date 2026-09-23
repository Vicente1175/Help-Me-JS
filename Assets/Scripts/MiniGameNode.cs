using UnityEngine;

[CreateAssetMenu(fileName = "NewMiniGameNode", menuName = "GameScript/Nodes/MiniGame Node")]
public class MiniGameNode : GameScriptNode
{
	[Header("Minijuego")]
	public GameObject miniGamePrefab;

	public MiniGameConfigSO config;

	[Header("Flujo")]
	public string nextNodeId;
}