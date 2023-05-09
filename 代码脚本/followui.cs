using UnityEngine;

public class followui : MonoBehaviour
{
	public Vector2 offset;

	public RectTransform rectTransform;

	private void Update()
	{
		Vector2 vector = Camera.main.WorldToScreenPoint(base.transform.position);
		rectTransform.position = vector + new Vector2(offset.x, offset.y);
		rectTransform.gameObject.SetActive(value: true);
	}
}
