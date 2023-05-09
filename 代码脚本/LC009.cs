public class LC009 : BattleListenerCase
{
	private bool occured;

	public override bool Condition()
	{
		if (occured)
		{
			return false;
		}
		if (bl.bh.enemy[0] != null && bl.bh.enemy[0].NowHp <= bl.bh.enemy[0].MaxHp / 2f)
		{
			occured = true;
			return true;
		}
		return false;
	}

	public override void OnTrue()
	{
		bl.bh.ForcePause();
		bl.fc.ExecuteBlock("PekoTNT-" + bl.bh.dc.Language);
		((Pekora_Pekora)bl.bh.enemy[0].eb).stnt = 1;
	}
}
