using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance { get; private set; }

	[Serializable]
	public class AudioData
	{
		public string id;
		public AudioClip clip;
	}

	[Header("Audio Sources")]
	public AudioSource musicSource;
	public AudioSource sfxSource;

	[Header("Música disponible")]
	public AudioData[] music;

	[Header("Efectos disponibles")]
	public AudioData[] sfx;

	private void Start()
	{
		PlayMusic("Menu_BGM");
	}
	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	public void PlayMusic(string musicId)
	{
		if (string.IsNullOrEmpty(musicId))
			return;

		for (int i = 0; i < music.Length; i++)
		{
			if (music[i].id == musicId)
			{
				if (musicSource.clip == music[i].clip && musicSource.isPlaying)
					return;

				musicSource.clip = music[i].clip;
				musicSource.Play();
				return;
			}
		}

		Debug.LogWarning("[AudioManager] No se encontró la música: " + musicId);
	}

	public void StopMusic()
	{
		musicSource.Stop();
	}

	public void PlaySFX(string sfxId)
	{
		if (string.IsNullOrEmpty(sfxId))
			return;

		for (int i = 0; i < sfx.Length; i++)
		{
			if (sfx[i].id == sfxId)
			{
				sfxSource.PlayOneShot(sfx[i].clip);
				return;
			}
		}

		Debug.LogWarning("[AudioManager] No se encontró el SFX: " + sfxId);
	}
}