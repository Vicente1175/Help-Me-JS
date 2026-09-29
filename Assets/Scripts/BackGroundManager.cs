using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

	private Image backgroundImage;

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

	private void Start()
	{
		FindBackgroundImage();
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		FindBackgroundImage();
	}

	private void FindBackgroundImage()
	{
		GameObject background =
			GameObject.FindGameObjectWithTag("Background");

		if (background == null)
		{
			Debug.LogWarning(
				"[BackgroundManager] No se encontró ningún objeto con el Tag Background."
			);

			return;
		}

		backgroundImage =
			background.GetComponent<Image>();

		if (backgroundImage == null)
		{
			Debug.LogWarning(
				"[BackgroundManager] El objeto Background no tiene componente Image."
			);
		}
	}

	public void ChangeBackground(string backgroundId)
	{
		if (string.IsNullOrEmpty(backgroundId))
			return;

		if (backgroundImage == null)
		{
			FindBackgroundImage();
		}

		if (backgroundImage == null)
		{
			Debug.LogWarning(
				"[BackgroundManager] No se encontró el Image del Background."
			);

			return;
		}

		for (int i = 0; i < backgrounds.Length; i++)
		{
			if (backgrounds[i].id == backgroundId)
			{
				backgroundImage.sprite =
					backgrounds[i].sprite;

				return;
			}
		}

		Debug.LogWarning(
			"[BackgroundManager] No se encontró el fondo: " +
			backgroundId
		);
	}
}