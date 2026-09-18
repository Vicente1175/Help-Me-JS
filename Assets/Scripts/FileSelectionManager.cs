using UnityEngine;
using UnityEngine.SceneManagement;

public class FileSelectionManager : MonoBehaviour
{
	public void Archivo01()
	{
		SceneManager.LoadScene("Act01");
	}

	public void Archivo02()
	{
		SceneManager.LoadScene("Act02");
	}

	public void Archivo03()
	{
		SceneManager.LoadScene("Act03");
	}
}