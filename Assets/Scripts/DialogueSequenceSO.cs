using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contiene todas las líneas de diálogo de una escena o fragmento de historia.
/// Las líneas se reproducen en el orden en que aparecen en el Inspector.
/// </summary>
[CreateAssetMenu(fileName = "NewDialogueSequence", menuName = "GameScript/Dialogue Sequence")]
public class DialogueSequenceSO : ScriptableObject
{
	[Tooltip("Todas las líneas de esta secuencia, en el orden en que se reproducen.")]
	public List<DialogueLineData> lines = new List<DialogueLineData>();

	/// <summary>
	/// Devuelve la línea en la posición dada, o null si está fuera de rango.
	/// </summary>
	public DialogueLineData GetLineAt(int index)
	{
		if (index < 0 || index >= lines.Count)
			return null;

		return lines[index];
	}
}