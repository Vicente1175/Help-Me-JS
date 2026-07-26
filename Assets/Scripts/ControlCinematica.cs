using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class ControlCinematica : MonoBehaviour
{
private VideoPlayer videoPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		videoPlayer = GetComponent<VideoPlayer>();
		videoPlayer.loopPointReached += VideoTerminado;
	}

    // Update is called once per frame
    void VideoTerminado(VideoPlayer videoPlayer)
    {
        SceneManager.LoadScene("Juego");
    }
}
