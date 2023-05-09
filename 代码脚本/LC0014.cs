using UnityEngine.SceneManagement;

public class LC0014 : BattleListenerCase_1
{
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
		SceneManager.LoadScene("Demo_Win");
	}
}
