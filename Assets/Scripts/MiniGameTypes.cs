using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMiniGameConfig", menuName = "GameScript/MiniGame Config")]
public class MiniGameConfigSO : ScriptableObject
{
	public string miniGameId;
}

[Serializable]
public class MiniGameResult
{
	public string miniGameId;
	public bool completed;
	public int score;
	public int attempts;
	public float timeTaken;
}

public interface IMiniGame
{
	void StartGame(MiniGameConfigSO config);

	event Action<MiniGameResult> OnGameFinished;
}