using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NewMonoBehaviourScript : MonoBehaviour
{
	public Button btnPlay;
	public Button btnExit;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		btnPlay.onClick.AddListener(Play);
		btnExit.onClick.AddListener(Exit);
	}

    // Update is called once per frame
    public void Play()
{
		// Cambia "NombreDeTuEscena" por el nombre exacto de tu escena
		SceneManager.LoadScene("House");
	}

	public void Exit()
	{
		Debug.Log("Saliendo del juego...");
		Application.Quit();
	}

}
