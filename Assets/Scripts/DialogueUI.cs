using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueUI : MonoBehaviour
{
	[Header("Referencias UI")]
	public GameObject dialoguePanel;
	public GameObject speakerNameBackground;
	public TMP_Text speakerNameText;
	public TMP_Text dialogueText;

	[Header("Diálogo de esta escena")]
	public DialogueSequenceSO startingSequence;

	[Header("Efecto de escritura")]
	[Tooltip("Tiempo entre cada letra.")]
	public float typingSpeed = 0.03f;

	private Coroutine typingCoroutine;
	private string currentText;
	private bool isTyping;

	private void Awake()
	{
		Hide();
	}

	private void Update()
	{
		if (dialoguePanel == null || !dialoguePanel.activeSelf)
			return;

		if (Mouse.current != null &&
			Mouse.current.leftButton.wasPressedThisFrame)
		{
			Next();
		}

		if (Keyboard.current != null &&
			Keyboard.current.spaceKey.wasPressedThisFrame)
		{
			Next();
		}
	}

	private void Next()
	{
		if (isTyping)
		{
			FinishTyping();
			return;
		}

		if (DialogueManager.Instance != null)
		{
			DialogueManager.Instance.Advance();
		}
	}

	public void ShowLine(
		string characterName,
		string text,
		bool characterVisible)
	{
		if (dialoguePanel != null)
			dialoguePanel.SetActive(true);

		bool hasCharacter =
			characterVisible &&
			!string.IsNullOrEmpty(characterName);

		if (speakerNameBackground != null)
			speakerNameBackground.SetActive(hasCharacter);

		if (speakerNameText != null)
		{
			speakerNameText.gameObject.SetActive(hasCharacter);
			speakerNameText.text = characterName;
		}

		currentText = text;

		if (typingCoroutine != null)
			StopCoroutine(typingCoroutine);

		typingCoroutine = StartCoroutine(TypeText());
	}

	private IEnumerator TypeText()
	{
		isTyping = true;
		dialogueText.text = "";

		foreach (char letter in currentText)
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(typingSpeed);
		}

		isTyping = false;
		typingCoroutine = null;
	}

	private void FinishTyping()
	{
		if (typingCoroutine != null)
			StopCoroutine(typingCoroutine);

		dialogueText.text = currentText;

		isTyping = false;
		typingCoroutine = null;
	}

	public void Hide()
	{
		if (dialoguePanel != null)
			dialoguePanel.SetActive(false);

		if (speakerNameBackground != null)
			speakerNameBackground.SetActive(false);

		if (speakerNameText != null)
			speakerNameText.gameObject.SetActive(false);

		if (typingCoroutine != null)
			StopCoroutine(typingCoroutine);

		isTyping = false;
		typingCoroutine = null;
	}
}