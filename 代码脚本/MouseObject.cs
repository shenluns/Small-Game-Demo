using UnityEngine;

public class MouseObject : MonoBehaviour
{
	[SerializeField]
	private int z;

	[SerializeField]
	private bool updateToThisObject;
	public float 速度 = 8;

	[HideInInspector]
	public Vector3 mousePos;

	private void Start()
	{
	}

	private void Update()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = (float)z - Camera.main.transform.position.z;
		mousePos = Camera.main.ScreenToWorldPoint(mousePosition);
		if (updateToThisObject)
		{
			base.gameObject.transform.position = (mousePos * -1) / 速度;
		}
	}
}
