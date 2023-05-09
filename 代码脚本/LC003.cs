using UnityEngine;

public class LC003 : BattleListenerCase_1
{
	private float startTime = -1f;

	private float tl = 0.3f;

	private GameObject cct;

	private Vector3 stv;

	public override bool Condition()
	{
		if (publicSign == 0)
		{
			return false;
		}
		return true;
	}

	public override void OnTrue()
	{
		camera001 component = GameObject.Find("Main Camera").GetComponent<camera001>();
		if (component.center == null)
		{
			publicSign = 0;
			component.center = GameObject.Find("player");
			return;
		}
		if (startTime == -1f)
		{
			stv = component.center.GetComponent<Transform>().position;
			cct = GameObject.Find("CameraCenter");
			component.center = cct;
			startTime = Time.time;
		}
		float num = Time.time - startTime;
		if (num > tl)
		{
			publicSign = 0;
			startTime = -1f;
			component.center = GameObject.Find("player");
		}
		else
		{
			cct.GetComponent<Transform>().position = (tl - num) / tl * stv + num / tl * GameObject.Find("player").GetComponent<Transform>().position;
		}
	}
}
