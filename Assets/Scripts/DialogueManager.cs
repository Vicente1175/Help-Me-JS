using System;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
	public static DialogueManager Instance { get; private set; }

	private DialogueUI dialogueUI;
	private DialogueSequenceSO currentSequence;
	private int currentIndex = -1;

	public event Action<DialogueLineData> OnLineStarted;
	public event Action OnSequenceCompleted;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	private void Start()
	{
		FindDialogueUI();
	}

	private void FindDialogueUI()
	{
		dialogueUI = FindFirstObjectByType<DialogueUI>();
	}

	public void PlaySequence(DialogueSequenceSO sequence)
	{
		if (sequence == null || sequence.lines.Count == 0)
		{
			Debug.LogWarning(
				"[DialogueManager] Se intentó reproducir una secuencia vacía o nula."
			);

			return;
		}

		FindDialogueUI();

		currentSequence = sequence;
		currentIndex = 0;

		ShowCurrentLine();
	}

	public void Advance()
	{
		if (currentSequence == null)
		{
			Debug.LogWarning(
				"[DialogueManager] No hay una secuencia activa."
			);

			return;
		}

		currentIndex++;

		if (currentIndex >= currentSequence.lines.Count)
		{
			EndSequence();
			return;
		}

		ShowCurrentLine();
	}

	private void ShowCurrentLine()
	{
		DialogueLineData line =
			currentSequence.GetLineAt(currentIndex);

		if (line == null)
		{
			EndSequence();
			return;
		}

		if (!string.IsNullOrEmpty(line.backgroundId))
		{
			BackgroundManager.Instance.ChangeBackground(
				line.backgroundId
			);
		}

		if (!string.IsNullOrEmpty(line.soundId))
		{
			AudioManager.Instance.PlaySFX(
				line.soundId
			);
		}

		if (!string.IsNullOrEmpty(line.musicId))
		{
			AudioManager.Instance.PlayMusic(
				line.musicId
			);
		}

		if (!string.IsNullOrEmpty(line.eventId))
		{
			EventManager.Instance.Raise(
				line.eventId
			);
		}

		if (CharacterController.Instance != null)
		{
			CharacterController.Instance.ShowCharacter(
				line.character,
				line.spriteId,
				line.characterPosition,
				line.characterVisible
			);
		}

		if (dialogueUI != null)
		{
			dialogueUI.ShowLine(
				line.character != null
					? line.character.characterName
					: "",
				line.text,
				line.characterVisible
			);
		}

		OnLineStarted?.Invoke(line);
	}

	private void EndSequence()
	{
		if (dialogueUI != null)
		{
			dialogueUI.Hide();
		}

		// Primero limpiamos la secuencia anterior.
		currentSequence = null;
		currentIndex = -1;

		// Después avisamos al GameScriptRunner.
		// Esto permite que pueda iniciar la siguiente secuencia
		// sin que la anterior la sobrescriba.
		OnSequenceCompleted?.Invoke();
	}
}