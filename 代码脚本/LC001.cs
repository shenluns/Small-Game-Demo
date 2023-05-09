public class LC001 : BattleListenerCase
{
	private bool occured;

	private float ft = -1f;

	public override bool Condition()
	{
		if (occured)
		{
			return false;
		}
		if (bl.bh == null)
		{
			return false;
		}
		if (ft == -1f)
		{
			if (bl.bh.EnemyNum != 1)
			{
				return false;
			}
			ft = bl.bh.NowTime;
		}
		float num = 60f / bl.bh.BPM - 2f * bl.bh.WrTime;
		if (bl.bh.NowTime - ft >= num)
		{
			occured = true;
			return true;
		}
		return false;
	}

	public override void OnTrue()
	{
		bl.fc.ExecuteBlock("PekoCaveClear-" + bl.bh.dc.Language);
	}
}
