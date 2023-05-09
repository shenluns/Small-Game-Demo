using UnityEngine;

public class LC007 : BattleListenerCase_1
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
			component.center = GameObject.Find("sence001/TNT");
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
			component.center = GameObject.Find("sence001/TNT");
		}
		else
		{
			cct.GetComponent<Transform>().position = (tl - num) / tl * stv + num / tl * GameObject.Find("sence001/TNT").GetComponent<Transform>().position;
		}
	}
}
