using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundManager : MonoBehaviour
{
	public static BackgroundManager Instance { get; private set; }

	[Serializable]
	public class BackgroundData
	{
		public string id;
		public Sprite sprite;
	}

	[Header("Fondos disponibles")]
	public BackgroundData[] backgrounds;

	private SpriteRenderer backgroundRenderer;

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

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		FindBackgroundRenderer();
	}

	private void Start()
	{
		FindBackgroundRenderer();
	}

	private void FindBackgroundRenderer()
	{
		GameObject background = GameObject.FindGameObjectWithTag("Background");

		if (background != null)
		{
			backgroundRenderer = background.GetComponent<SpriteRenderer>();
		}
		else
		{
			Debug.LogWarning("[BackgroundManager] No se encontró un objeto con el tag Background.");
		}
	}

	public void ChangeBackground(string backgroundId)
	{
		if (string.IsNullOrEmpty(backgroundId))
			return;

		if (backgroundRenderer == null)
		{
			FindBackgroundRenderer();
		}

		for (int i = 0; i < backgrounds.Length; i++)
		{
			if (backgrounds[i].id == backgroundId)
			{
				backgroundRenderer.sprite = backgrounds[i].sprite;
				return;
			}
		}

		Debug.LogWarning("[BackgroundManager] No se encontró el fondo: " + backgroundId);
	}
}