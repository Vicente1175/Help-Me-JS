using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
	public static MiniGameManager Instance { get; private set; }

	public event Action<MiniGameResult> OnMiniGameFinished;

	private Transform miniGameContainer;
	private GameObject currentMiniGame;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void Start()
	{
		FindMiniGameContainer();
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		FindMiniGameContainer();
	}

	private void FindMiniGameContainer()
	{
		GameObject container = GameObject.Find("MiniGameContainer");

		if (container != null)
		{
			miniGameContainer = container.transform;

			Debug.Log("[MiniGameManager] MiniGameContainer encontrado.");
		}
		else
		{
			miniGameContainer = null;

			Debug.Log("[MiniGameManager] No se encontró MiniGameContainer en esta escena.");
		}
	}

	public void StartMiniGame(GameObject miniGamePrefab, MiniGameConfigSO config)
	{
		if (miniGamePrefab == null)
		{
			Debug.LogWarning("[MiniGameManager] No se asignó un prefab de minijuego.");
			return;
		}

		if (miniGameContainer == null)
		{
			FindMiniGameContainer();
		}

		if (miniGameContainer == null)
		{
			Debug.LogWarning("[MiniGameManager] No se encontró MiniGameContainer en la escena.");
			return;
		}

		if (currentMiniGame != null)
		{
			Destroy(currentMiniGame);
		}

		currentMiniGame = Instantiate(miniGamePrefab, miniGameContainer);

		IMiniGame miniGame = currentMiniGame.GetComponent<IMiniGame>();

		if (miniGame != null)
		{
			miniGame.OnGameFinished += FinishMiniGame;
			miniGame.StartGame(config);
		}
		else
		{
			Debug.LogWarning(
				"[MiniGameManager] El prefab no contiene un componente que implemente IMiniGame."
			);
		}
	}

	private void FinishMiniGame(MiniGameResult result)
	{
		OnMiniGameFinished?.Invoke(result);

		if (currentMiniGame != null)
		{
			Destroy(currentMiniGame);
			currentMiniGame = null;
		}
	}
}