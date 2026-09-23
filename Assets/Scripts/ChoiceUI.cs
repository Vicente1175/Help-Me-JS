using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour
{
	[Header("Referencias UI")]
	public GameObject choicePanel;
	public TMP_Text questionText;
	public List<Button> optionButtons = new List<Button>();

	public event Action<int> OnOptionSelected;

	private void Awake()
	{
		Hide();
	}

	public void Show(string question, List<ChoiceOption> options)
	{
		choicePanel.SetActive(true);

		questionText.text = question;

		for (int i = 0; i < optionButtons.Count; i++)
		{
			optionButtons[i].gameObject.SetActive(false);
			optionButtons[i].onClick.RemoveAllListeners();
		}

		for (int i = 0; i < options.Count && i < optionButtons.Count; i++)
		{
			int optionIndex = i;

			optionButtons[i].gameObject.SetActive(true);

			TMP_Text buttonText = optionButtons[i].GetComponentInChildren<TMP_Text>();
			buttonText.text = options[i].optionText;

			optionButtons[i].onClick.AddListener(() =>
			{
				OnOptionSelected?.Invoke(optionIndex);
			});
		}
	}

	public void Hide()
	{
		if (choicePanel != null)
			choicePanel.SetActive(false);
	}
}