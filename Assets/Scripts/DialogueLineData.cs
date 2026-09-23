using System;
using UnityEngine;

/// <summary>
/// Representa una única línea de diálogo. No es un ScriptableObject por sí misma:
/// vive dentro de la lista de un DialogueSequenceSO, para que puedas ver y editar
/// todo el guion de un Acto en un solo archivo, en vez de crear un asset por línea.
/// </summary>
[Serializable]
public class DialogueLineData
{
	[Tooltip("Identificador opcional. Solo es necesario rellenarlo si OTRA línea " +
			 "necesita saltar hacia esta (por ejemplo, para unir dos ramas de una " +
			 "decisión). Si nadie apunta hacia ella, puedes dejarlo vacío.")]
	public string id;

	[Header("Contenido")]
	[Tooltip("Nombre del personaje que habla. Se reemplazará por una referencia a " +
			 "CharacterSO cuando construyamos el sistema de personajes; por ahora " +
			 "basta con el nombre como texto.")]
	public string characterName;

	[TextArea(2, 5)]
	public string text;

	[Header("Efectos (se conectarán cuando existan sus managers)")]
	[Tooltip("Expresión del personaje: Idle, Talking, Tired, Worried, Sad, Neutral. " +
			 "Se usará al conectar el CharacterController.")]
	public string expression;

	[Tooltip("ID del fondo a mostrar. Vacío = no cambia el fondo. " +
			 "Se usará al conectar el BackgroundManager.")]
	public string backgroundId;

	[Tooltip("ID del sonido a reproducir. Vacío = ningún sonido. " +
			 "Se usará al conectar el AudioManager.")]
	public string soundId;

	[Tooltip("ID de la música de fondo. Vacío = mantiene la música actual. " +
			 "Se usará al conectar el AudioManager.")]
	public string musicId;

	[Tooltip("ID de un evento de juego a disparar (ej. \"IniciarMinijuego01\"). " +
			 "Vacío = ningún evento. Se usará al conectar el EventManager.")]
	public string eventId;

	[Header("Flujo")]
	[Tooltip("Déjalo vacío para que el diálogo avance a la SIGUIENTE línea de la " +
			 "lista (orden normal). Rellénalo con el id de otra línea solo cuando " +
			 "necesites ramificar o converger diálogos.")]
	public string jumpToId;
}