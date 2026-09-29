using UnityEngine;
using UnityEngine.UI;

public class HelpProgressUI : MonoBehaviour
{
	[Header("Imagen")]
	public Image progressImage;

	[Header("Sprites de progreso")]
	public Sprite noneSprite;
	public Sprite recognizeSprite;
	public Sprite acceptSprite;
	public Sprite safePersonSprite;
	public Sprite startConversationSprite;
	public Sprite accessHelpSprite;

	private void Start()
	{
		UpdateUI();
	}

	public void UpdateUI()
	{
		if (GameStateManager.Instance == null)
			return;

		switch (GameStateManager.Instance.helpProgress)
		{
			case HelpProgressStage.None:
				progressImage.sprite = noneSprite;
				break;

			case HelpProgressStage.Recognize:
				progressImage.sprite = recognizeSprite;
				break;

			case HelpProgressStage.Accept:
				progressImage.sprite = acceptSprite;
				break;

			case HelpProgressStage.SafePerson:
				progressImage.sprite = safePersonSprite;
				break;

			case HelpProgressStage.StartConversation:
				progressImage.sprite = startConversationSprite;
				break;

			case HelpProgressStage.AccessHelp:
				progressImage.sprite = accessHelpSprite;
				break;
		}
	}
}