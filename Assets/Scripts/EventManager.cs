using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	public static EventManager Instance { get; private set; }

	public event Action<string> OnEventRaised;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	public void Raise(string eventId)
	{
		if (string.IsNullOrEmpty(eventId))
			return;

		OnEventRaised?.Invoke(eventId);
	}
}