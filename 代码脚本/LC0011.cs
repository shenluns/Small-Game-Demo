using UnityEngine.SceneManagement;

public class LC0011 : BattleListenerCase_1
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
		SceneManager.LoadScene("Start");
	}
}
