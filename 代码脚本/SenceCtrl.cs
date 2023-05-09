using UnityEngine;

public class SenceCtrl : MonoBehaviour
{
	public BattleHub bh;

	public SenceBehaviour sb;

	public float H;

	private void Start()
	{
	}

	private void Update()
	{
		if (bh == null || sb == null)
		{
			Vector3 position = base.gameObject.GetComponent<Transform>().position;
			position.z = 60f + (position.y - 0.5f * H + 0.5f) * 0.01f;
			base.gameObject.GetComponent<Transform>().position = position;
		}
	}
}
