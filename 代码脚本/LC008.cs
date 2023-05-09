using UnityEngine;

public class LC008 : BattleListenerCase
{
	private bool occured;

	public override bool Condition()
	{
		if (occured)
		{
			return false;
		}
		if (bl.bh.enemy[0] != null && bl.bh.enemy[0].NowHp <= bl.bh.enemy[0].MaxHp / 4f)
		{
			occured = true;
			return true;
		}
		return false;
	}

	public override void OnTrue()
	{
		GameObject.Find("enemy1_0").GetComponent<Enemy>().PauseDirect = 16;
		bl.bh.ForcePause();
		bl.fc.ExecuteBlock("PekoTrick-" + bl.bh.dc.Language);
	}
}
