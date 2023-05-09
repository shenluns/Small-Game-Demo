using UnityEngine;

public class LC0010 : BattleListenerCase
{
	private bool occured;

	public override bool Condition()
	{
		if (occured)
		{
			return false;
		}
		if (bl.bh.enemy[0] != null && bl.bh.enemy[0].Alive == 0)
		{
			occured = true;
			return true;
		}
		return false;
	}

	public override void OnTrue()
	{
		GameObject.Find("enemy1_0").GetComponent<Enemy>().PauseDirect = 17;
		bl.bh.ForcePause();
		bl.fc.ExecuteBlock("PekoWin-" + bl.bh.dc.Language);
	}
}
