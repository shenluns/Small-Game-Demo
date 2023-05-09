public class LC0016 : BattleListenerCase_1
{
	public override bool Condition()
	{
		if (publicSign == 0)
		{
			return false;
		}
		publicSign = 0;
		return true;
	}

	public override void OnTrue()
	{
		bl.bh.enemy[0].PauseDirect = 18;
	}
}
