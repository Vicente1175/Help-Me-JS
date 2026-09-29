using UnityEngine;

public class GameScriptRunner : MonoBehaviour
{
	[Header("Guion")]
	public GameScriptSO gameScript;

	private GameScriptNode currentNode;
	private ChoiceUI choiceUI;

	private Transform signalsUI;

	[SerializeField]
	private CompletionUI completionUI;

	private void Start()
	{
		choiceUI = FindFirstObjectByType<ChoiceUI>();
		completionUI = FindFirstObjectByType<CompletionUI>();

		signalsUI = GameObject.Find("SignalsUI")?.transform;

		if (EventManager.Instance != null)
		{
			EventManager.Instance.OnEventRaised += OnEventRaised;
		}

		StartGameScript();
	}

	private void OnDestroy()
	{
		if (EventManager.Instance != null)
		{
			EventManager.Instance.OnEventRaised -= OnEventRaised;
		}

		if (DialogueManager.Instance != null)
		{
			DialogueManager.Instance.OnSequenceCompleted -= OnDialogueCompleted;
		}
	}

	public void StartGameScript()
	{
		if (gameScript == null)
		{
			Debug.LogWarning(
				"[GameScriptRunner] No se asignó un GameScript."
			);

			return;
		}

		currentNode =
			gameScript.GetNodeById(gameScript.startNodeId);

		if (currentNode == null)
		{
			Debug.LogWarning(
				"[GameScriptRunner] No se encontró el nodo inicial: " +
				gameScript.startNodeId
			);

			return;
		}

		ExecuteCurrentNode();
	}

	private void ExecuteCurrentNode()
	{
		if (currentNode is DialogueNode dialogueNode)
		{
			ExecuteDialogueNode(dialogueNode);
		}
		else if (currentNode is ChoiceNode choiceNode)
		{
			ExecuteChoiceNode(choiceNode);
		}
		else if (currentNode is SignalNode signalNode)
		{
			ExecuteSignalNode(signalNode);
		}
		else if (currentNode is EndNode)
		{
			ExecuteEndNode();
		}
		else
		{
			Debug.LogWarning(
				"[GameScriptRunner] Este tipo de nodo todavía no está conectado: " +
				currentNode.GetType().Name
			);
		}
	}

	private void ExecuteDialogueNode(DialogueNode dialogueNode)
	{
		if (dialogueNode.dialogueSequence == null)
		{
			Debug.LogWarning(
				"[GameScriptRunner] El DialogueNode no tiene una secuencia de diálogo."
			);

			return;
		}

		DialogueManager.Instance.OnSequenceCompleted -= OnDialogueCompleted;
		DialogueManager.Instance.OnSequenceCompleted += OnDialogueCompleted;

		DialogueManager.Instance.PlaySequence(
			dialogueNode.dialogueSequence
		);
	}

	private void OnDialogueCompleted()
	{
		if (DialogueManager.Instance != null)
		{
			DialogueManager.Instance.OnSequenceCompleted -= OnDialogueCompleted;
		}

		DialogueNode dialogueNode =
			currentNode as DialogueNode;

		if (dialogueNode == null)
			return;

		GoToNextNode(dialogueNode.nextNodeId);
	}

	private void ExecuteChoiceNode(ChoiceNode choiceNode)
	{
		if (choiceUI == null)
		{
			Debug.LogWarning(
				"[GameScriptRunner] No se encontró ChoiceUI en la escena."
			);

			return;
		}

		if (choiceNode.options == null ||
			choiceNode.options.Count == 0)
		{
			Debug.LogWarning(
				"[GameScriptRunner] El ChoiceNode no tiene opciones."
			);

			return;
		}

		choiceUI.OnOptionSelected -= OnChoiceSelected;
		choiceUI.OnOptionSelected += OnChoiceSelected;

		choiceUI.Show(
			choiceNode.question,
			choiceNode.options
		);
	}

	private void OnChoiceSelected(int optionIndex)
	{
		choiceUI.OnOptionSelected -= OnChoiceSelected;

		ChoiceNode choiceNode =
			currentNode as ChoiceNode;

		if (choiceNode == null)
			return;

		if (optionIndex < 0 ||
			optionIndex >= choiceNode.options.Count)
			return;

		ChoiceOption selectedOption =
			choiceNode.options[optionIndex];

		if (GameStateManager.Instance != null)
		{
			switch (selectedOption.variable)
			{
				case StateVariable.EmotionalWellbeing:
					GameStateManager.Instance.ModifyWellbeing(
						selectedOption.variableEffect
					);
					break;

				case StateVariable.Energy:
					GameStateManager.Instance.ModifyEnergy(
						selectedOption.variableEffect
					);
					break;
			}

			if (selectedOption.helpProgress !=
				HelpProgressStage.None)
			{
				GameStateManager.Instance.helpProgress =
					selectedOption.helpProgress;
			}
		}

		choiceUI.Hide();

		StateUI stateUI =
			FindFirstObjectByType<StateUI>();

		if (stateUI != null)
		{
			stateUI.UpdateUI();
		}

		HelpProgressUI helpProgressUI =
			FindFirstObjectByType<HelpProgressUI>();

		if (helpProgressUI != null)
		{
			helpProgressUI.UpdateUI();
		}

		GoToNextNode(selectedOption.nextNodeId);
	}

	private void ExecuteSignalNode(SignalNode signalNode)
	{
		if (signalNode.signal == null)
		{
			Debug.LogWarning(
				"[GameScriptRunner] El SignalNode no tiene una señal asignada."
			);

			return;
		}

		if (signalsUI == null)
		{
			Debug.LogWarning(
				"[GameScriptRunner] No se encontró SignalsUI en la escena."
			);

			return;
		}

		for (int i = 0; i < signalsUI.childCount; i++)
		{
			Transform signalObject =
				signalsUI.GetChild(i);

			SignalTrigger trigger =
				signalObject.GetComponent<SignalTrigger>();

			if (trigger != null &&
				trigger.signal == signalNode.signal)
			{
				signalObject.gameObject.SetActive(true);

				return;
			}
		}

		Debug.LogWarning(
			"[GameScriptRunner] No se encontró el objeto UI de la señal: " +
			signalNode.signal.signalId
		);
	}

	private void OnEventRaised(string eventId)
	{
		if (!(currentNode is SignalNode signalNode))
			return;

		if (signalNode.signal == null)
			return;

		string expectedEvent =
			"SignalActivated_" +
			signalNode.signal.signalId;

		if (eventId != expectedEvent)
			return;

		GoToNextNode(signalNode.nextNodeId);
	}

	private void ExecuteEndNode()
	{
		Debug.Log(
			"[GameScriptRunner] Se alcanzó el final del nivel."
		);

		if (completionUI == null)
		{
			Debug.LogError(
				"[GameScriptRunner] CompletionUI es NULL."
			);

			return;
		}

		completionUI.Show();

		currentNode = null;
	}

	private void GoToNextNode(string nextNodeId)
	{
		if (string.IsNullOrEmpty(nextNodeId))
		{
			EndGameScript();
			return;
		}

		currentNode =
			gameScript.GetNodeById(nextNodeId);

		if (currentNode == null)
		{
			EndGameScript();
			return;
		}

		ExecuteCurrentNode();
	}

	private void EndGameScript()
	{
		currentNode = null;

		Debug.Log(
			"[GameScriptRunner] GameScript terminado."
		);
	}
}