using UnityEngine;


public enum HelpProgressStage
{
	None,
	Recognize,
	Accept,
	SafePerson,
	StartConversation,
	AccessHelp
}
public class GameStateManager : MonoBehaviour
{
	public static GameStateManager Instance;

	[Range(0, 100)]
	public int emotionalWellbeing = 70;

	[Range(0, 100)]
	public int energy = 70;

	public HelpProgressStage helpProgress = HelpProgressStage.None;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void ModifyWellbeing(int amount)
	{
		emotionalWellbeing = Mathf.Clamp(emotionalWellbeing + amount, 0, 100);
	}

	public void ModifyEnergy(int amount)
	{
		energy = Mathf.Clamp(energy + amount, 0, 100);
	}

	public void AdvanceHelpProgress()
	{
		if (helpProgress < HelpProgressStage.AccessHelp)
		{
			helpProgress++;
		}
	}
}
