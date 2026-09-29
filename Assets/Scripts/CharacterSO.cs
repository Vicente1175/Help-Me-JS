using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "GameScript/Character")]
public class CharacterSO : ScriptableObject
{
	[Header("Identificación")]
	public string characterId;
	public string characterName;

	[Header("Sprites")]
	public List<CharacterSprite> sprites = new List<CharacterSprite>();

	public Sprite GetSprite(string spriteId)
	{
		for (int i = 0; i < sprites.Count; i++)
		{
			if (sprites[i].id == spriteId)
			{
				return sprites[i].sprite;
			}
		}

		return null;
	}
}

[Serializable]
public class CharacterSprite
{
	public string id;
	public Sprite sprite;
}