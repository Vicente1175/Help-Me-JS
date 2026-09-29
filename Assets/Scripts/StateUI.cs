using UnityEngine;
using UnityEngine.UI;

public class StateUI : MonoBehaviour
{
	[Header("Barras")]
	public Slider wellbeingBar;
	public Slider energyBar;

	private void Start()
	{
		UpdateUI();
	}

	public void UpdateUI()
	{
		if (GameStateManager.Instance == null)
			return;

		wellbeingBar.value = GameStateManager.Instance.emotionalWellbeing;
		energyBar.value = GameStateManager.Instance.energy;
	}
}
