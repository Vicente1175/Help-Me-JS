using UnityEngine;

[CreateAssetMenu(fileName = "NewSignal", menuName = "GameScript/Signal")]
public class SignalSO : ScriptableObject
{
	[Header("Identificación")]
	public string signalId;

	[Header("Imagen")]
	public Sprite image;
}