using UnityEngine.SceneManagement;

public class LC0015 : BattleListenerCase_1
{
	public override bool Condition()
	{
		if (publicSign == 1)
		{
			return true;
		}
		if ((bool)bl.bh && (bool)bl.bh.player && bl.bh.player.NowHp <= 0f)
		{
			return true;
		}
		return false;
	}

	public override void OnTrue()
	{
		SceneManager.LoadScene("Demo_Lose");
	}
}
