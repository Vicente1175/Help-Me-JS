using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompletionUI : MonoBehaviour
{
	[Header("Textos")]
	public TMP_Text titleText;
	public TMP_Text wellbeingText;
	public TMP_Text energyText;
	public TMP_Text helpProgressText;

	[Header("Botón")]
	public Button continueButton;

	private void Start()
	{
		gameObject.SetActive(false);

		if (continueButton != null)
		{
			continueButton.onClick.AddListener(Continue);
		}
	}

	private void OnDestroy()
	{
		if (continueButton != null)
		{
			continueButton.onClick.RemoveListener(Continue);
		}
	}

	public void Show()
	{
		if (GameStateManager.Instance == null)
			return;

		gameObject.SetActive(true);

		if (titleText != null)
		{
			titleText.text = "NIVEL COMPLETADO";
		}

		if (wellbeingText != null)
		{
			wellbeingText.text =
				"Bienestar emocional: " +
				GameStateManager.Instance.emotionalWellbeing;
		}

		if (energyText != null)
		{
			energyText.text =
				"Energía: " +
				GameStateManager.Instance.energy;
		}

		if (helpProgressText != null)
		{
			helpProgressText.text =
				"Progreso de autocuidado: " +
				GetHelpProgressText(
					GameStateManager.Instance.helpProgress
				);
		}

		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.StopMusic();
		}
	}

	private void Continue()
	{
		if (AudioManager.Instance != null)
		{
			AudioManager.Instance.PlayMusic("Menu_BGM");
		}

		SceneManager.LoadScene("Actos");
	}

	private string GetHelpProgressText(HelpProgressStage stage)
	{
		switch (stage)
		{
			case HelpProgressStage.Recognize:
				return "Reconocer";

			case HelpProgressStage.Accept:
				return "Aceptar";

			case HelpProgressStage.SafePerson:
				return "Persona segura";

			case HelpProgressStage.StartConversation:
				return "Iniciar conversación";

			case HelpProgressStage.AccessHelp:
				return "Acceder a ayuda";

			default:
				return "Sin progreso";
		}
	}
}