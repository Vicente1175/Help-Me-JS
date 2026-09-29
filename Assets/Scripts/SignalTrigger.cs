using UnityEngine;
using UnityEngine.UI;

public class SignalTrigger : MonoBehaviour
{
	[Header("Señal")]
	public SignalSO signal;

	private Button button;
	private Image image;

	private void Awake()
	{
		button = GetComponent<Button>();
		image = GetComponent<Image>();

		if (button != null)
		{
			button.onClick.AddListener(ActivateSignal);
		}

		UpdateImage();
	}

	private void OnDestroy()
	{
		if (button != null)
		{
			button.onClick.RemoveListener(ActivateSignal);
		}
	}

	private void UpdateImage()
	{
		if (signal == null)
			return;

		if (image == null)
			return;

		image.sprite = signal.image;
	}

	private void ActivateSignal()
	{
		if (signal == null)
		{
			Debug.LogWarning(
				"[SignalTrigger] No se asignó una señal."
			);

			return;
		}

		gameObject.SetActive(false);

		EventManager.Instance.Raise(
			"SignalActivated_" + signal.signalId
		);
	}
}