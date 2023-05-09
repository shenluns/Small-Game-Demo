using UnityEngine;

public class LC006 : BattleListenerCase_1
{
	public override bool Condition()
	{
		if (publicSign == 1)
		{
			publicSign = 0;
			return true;
		}
		return false;
	}

	public override void OnTrue()
	{
		GameObject.Find("enemy1_0").GetComponent<Enemy>().PauseDirect = 4;
		bl.bh.ForcePauseEnd();
	}
}
