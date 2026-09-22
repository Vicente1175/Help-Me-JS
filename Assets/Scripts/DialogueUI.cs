using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
	[Header("Referencias UI")]
	public GameObject dialoguePanel;
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

	void Awake()
	{
		Hide();
	}

	void Update()
	{
		if (dialoguePanel != null && dialoguePanel.activeSelf)
		{
			if (Mouse.current.leftButton.wasPressedThisFrame ||
	Keyboard.current.spaceKey.wasPressedThisFrame)
			{
				if (isTyping)
				{
					FinishTyping();
				}
				else
				{
					DialogueManager.Instance.Advance();
				}
			}
		}
	}

	public void ShowLine(string characterName, string text)
	{
		if (dialoguePanel != null)
			dialoguePanel.SetActive(true);

		if (speakerNameText != null)
			speakerNameText.text = characterName;

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

		if (typingCoroutine != null)
			StopCoroutine(typingCoroutine);

		isTyping = false;
		typingCoroutine = null;
	}
}