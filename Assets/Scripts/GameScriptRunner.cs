using UnityEngine;

public class GameScriptRunner : MonoBehaviour
{
	[Header("Guion")]
	public GameScriptSO gameScript;

	private GameScriptNode currentNode;
	private ChoiceUI choiceUI;

	private void Start()
	{
		choiceUI = FindFirstObjectByType<ChoiceUI>();

		StartGameScript();
	}

	public void StartGameScript()
	{
		if (gameScript == null)
		{
			Debug.LogWarning("[GameScriptRunner] No se asignó un GameScript.");
			return;
		}

		currentNode = gameScript.GetNodeById(gameScript.startNodeId);

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

		DialogueManager.Instance.OnSequenceCompleted += OnDialogueCompleted;

		DialogueManager.Instance.PlaySequence(dialogueNode.dialogueSequence);
	}

	private void OnDialogueCompleted()
	{
		DialogueManager.Instance.OnSequenceCompleted -= OnDialogueCompleted;

		DialogueNode dialogueNode = currentNode as DialogueNode;

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

		if (choiceNode.options == null || choiceNode.options.Count == 0)
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

		ChoiceNode choiceNode = currentNode as ChoiceNode;

		if (choiceNode == null)
			return;

		if (optionIndex < 0 || optionIndex >= choiceNode.options.Count)
			return;

		ChoiceOption selectedOption = choiceNode.options[optionIndex];

		choiceUI.Hide();

		GoToNextNode(selectedOption.nextNodeId);
	}

	private void GoToNextNode(string nextNodeId)
	{
		if (string.IsNullOrEmpty(nextNodeId))
		{
			EndGameScript();
			return;
		}

		currentNode = gameScript.GetNodeById(nextNodeId);

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

		Debug.Log("[GameScriptRunner] GameScript terminado.");
	}
}