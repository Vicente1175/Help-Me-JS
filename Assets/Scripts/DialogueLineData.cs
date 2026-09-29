using System;
using UnityEngine;



[Serializable]
public class DialogueLineData
{
	[Header("Personaje")]
	public CharacterSO character;

	public string spriteId;

	public CharacterPosition characterPosition;

	public bool characterVisible = true;

	[Header("Contenido")]
	[TextArea(2, 5)]
	public string text;

	[Header("Efectos")]
	[Tooltip("ID del fondo a mostrar. Vacío = no cambia el fondo.")]
	public string backgroundId;

	[Tooltip("ID del sonido a reproducir. Vacío = ningún sonido.")]
	public string soundId;

	[Tooltip("ID de la música de fondo. Vacío = mantiene la música actual.")]
	public string musicId;

	[Tooltip("ID de un evento de juego a disparar.")]
	public string eventId;
}

public enum CharacterPosition
{
	Left,
	Right
}