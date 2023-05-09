using UnityEngine;

public class FPScount : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		float num = 1f / Time.deltaTime;
		base.gameObject.name = "FPS=" + num;
	}
}
