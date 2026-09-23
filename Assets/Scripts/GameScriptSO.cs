using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameScript", menuName = "GameScript/Game Script")]
public class GameScriptSO : ScriptableObject
{
	[Header("Inicio del guion")]
	public string startNodeId;

	[Header("Nodos del guion")]
	public List<GameScriptNode> nodes = new List<GameScriptNode>();

	public GameScriptNode GetNodeById(string id)
	{
		if (string.IsNullOrEmpty(id))
			return null;

		for (int i = 0; i < nodes.Count; i++)
		{
			if (nodes[i] != null && nodes[i].nodeId == id)
			{
				return nodes[i];
			}
		}

		Debug.LogWarning(
			"[GameScriptSO] No se encontró ningún nodo con id: " + id
		);

		return null;
	}
}