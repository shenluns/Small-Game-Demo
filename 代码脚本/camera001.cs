using UnityEngine;

public class camera001 : MonoBehaviour
{
	public GameObject center;

	private void Start()
	{
	}

	private void Update()
	{
		if (!(center == null))
		{
			Vector3 position = center.GetComponent<Transform>().position;
			position.z = base.gameObject.GetComponent<Transform>().position.z;
			base.gameObject.GetComponent<Transform>().position = position;
		}
	}
}
