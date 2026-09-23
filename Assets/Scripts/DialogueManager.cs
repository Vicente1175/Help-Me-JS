
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
	public static DialogueManager Instance { get; private set; }

	private DialogueUI dialogueUI;
	private DialogueSequenceSO currentSequence;
	private int currentIndex = -1;

	public event Action<DialogueLineData> OnLineStarted;
	public event Action OnSequenceCompleted;

	void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void Start()
	{
		FindDialogueUI();
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		FindDialogueUI();

		if (dialogueUI != null && dialogueUI.startingSequence != null)
		{
			PlaySequence(dialogueUI.startingSequence);
		}
	}

	private void FindDialogueUI()
	{
		dialogueUI = FindFirstObjectByType<DialogueUI>();
	}

	public void PlaySequence(DialogueSequenceSO sequence)
	{
		if (sequence == null || sequence.lines.Count == 0)
		{
			Debug.LogWarning("[DialogueManager] Se intentó reproducir una secuencia vacía o nula.");
			return;
		}

		FindDialogueUI();

		currentSequence = sequence;
		currentIndex = 0;

		ShowCurrentLine();
	}

	public void Advance()
	{
		if (currentSequence == null) return;

		DialogueLineData current = currentSequence.GetLineAt(currentIndex);

		if (current != null && !string.IsNullOrEmpty(current.jumpToId))
		{
			int jumpIndex = currentSequence.GetIndexById(current.jumpToId);
			currentIndex = jumpIndex >= 0 ? jumpIndex : currentIndex + 1;
		}
		else
		{
			currentIndex++;
		}

		if (currentIndex >= currentSequence.lines.Count)
		{
			EndSequence();
			return;
		}

		ShowCurrentLine();
	}

	private void ShowCurrentLine()
	{
		DialogueLineData line = currentSequence.GetLineAt(currentIndex);
		if (line == null)
		{
			EndSequence();
			return;
		}

		// Future hooks:
		if (!string.IsNullOrEmpty(line.backgroundId))
			BackgroundManager.Instance.ChangeBackground(line.backgroundId);

		if (!string.IsNullOrEmpty(line.soundId))
			AudioManager.Instance.PlaySFX(line.soundId);

		if (!string.IsNullOrEmpty(line.musicId))
			AudioManager.Instance.PlayMusic(line.musicId);

		if (!string.IsNullOrEmpty(line.eventId))
			EventManager.Instance.Raise(line.eventId);

		// if (!string.IsNullOrEmpty(line.expression))
		//     CharacterController.Get(line.characterName)?.SetExpression(line.expression);

		if (dialogueUI != null)
		{
			dialogueUI.ShowLine(line.characterName, line.text);
		}

		OnLineStarted?.Invoke(line);
	}

	private void EndSequence()
	{
		if (dialogueUI != null)
		{
			dialogueUI.Hide();
		}

		OnSequenceCompleted?.Invoke();
		currentSequence = null;
		currentIndex = -1;
	}
}

