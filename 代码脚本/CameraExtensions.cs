using UnityEngine;

public static class CameraExtensions
{
	public static Bounds OrthographicBounds(this Camera camera)
	{
		float num = (float)Screen.width / (float)Screen.height;
		float num2 = camera.orthographicSize * 2f;
		return new Bounds(camera.transform.position, new Vector3(num2 * num, num2, 0f));
	}
}
