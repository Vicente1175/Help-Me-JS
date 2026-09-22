using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Un asset .asset que contiene todas las líneas de diálogo de una escena o
/// fragmento de historia. Para crear uno nuevo: click derecho en el Project ->
/// Create -> GameScript -> Dialogue Sequence.
/// </summary>
[CreateAssetMenu(fileName = "NewDialogueSequence", menuName = "GameScript/Dialogue Sequence")]
public class DialogueSequenceSO : ScriptableObject
{
	[Tooltip("Todas las líneas de esta secuencia, en el orden en que se reproducen " +
			 "por defecto. Reordénalas arrastrándolas en el Inspector.")]
	public List<DialogueLineData> lines = new List<DialogueLineData>();

	/// <summary>Devuelve la línea en la posición dada, o null si está fuera de rango.</summary>
	public DialogueLineData GetLineAt(int index)
	{
		if (index < 0 || index >= lines.Count) return null;
		return lines[index];
	}

	/// <summary>Busca el índice de la línea con el id dado. Devuelve -1 si no existe.</summary>
	public int GetIndexById(string id)
	{
		if (string.IsNullOrEmpty(id)) return -1;

		for (int i = 0; i < lines.Count; i++)
		{
			if (lines[i].id == id) return i;
		}

		Debug.LogWarning($"[DialogueSequenceSO] No se encontró ninguna línea con id " +
						  $"'{id}' en la secuencia '{name}'. Revisa que el jumpToId " +
						  $"esté bien escrito.");
		return -1;
	}
}