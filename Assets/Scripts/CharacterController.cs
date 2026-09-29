using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
	public static CharacterController Instance { get; private set; }

	[Header("Posiciones")]
	public Transform leftSlot;
	public Transform rightSlot;

	private Image leftImage;
	private Image rightImage;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;

		leftImage = leftSlot.GetComponent<Image>();
		rightImage = rightSlot.GetComponent<Image>();
		leftImage.enabled = false;
		rightImage.enabled = false;
	}

	public void ShowCharacter(
		CharacterSO character,
		string spriteId,
		CharacterPosition position,
		bool visible)
	{
		Image targetImage = GetImage(position);

		if (targetImage == null)
			return;

		if (!visible)
		{
			targetImage.enabled = false;
			return;
		}

		if (character == null)
		{
			targetImage.enabled = false;
			return;
		}

		Sprite sprite = character.GetSprite(spriteId);

		if (sprite == null)
		{
			Debug.LogWarning(
				"[CharacterController] No se encontró el sprite '" +
				spriteId +
				"' del personaje '" +
				character.characterName +
				"'."
			);

			return;
		}

		targetImage.sprite = sprite;
		targetImage.enabled = true;
	}

	private Image GetImage(CharacterPosition position)
	{
		if (position == CharacterPosition.Left)
			return leftImage;

		return rightImage;
	}
}