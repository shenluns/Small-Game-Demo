using UnityEngine;

public class LC005 : BattleListenerCase_1
{
	private GameObject peko;

	private float startTime;

	private int occurSign;

	private float[] tl = new float[5] { 1f, 0.2f, 0.5f, 1f, 2.3f };

	private float[] pl = new float[6] { 24f, 24f, 26f, 18f, 18f, 13f };

	public override bool Condition()
	{
		if (publicSign == 0 || occurSign == 2)
		{
			return false;
		}
		if (occurSign == 0)
		{
			occurSign = 1;
			startTime = Time.time;
			peko = GameObject.Find("enemy1_0");
		}
		return true;
	}

	public override void OnTrue()
	{
		float num = Time.time;
		for (int i = 0; i < 5; i++)
		{
			num -= tl[i];
			if (startTime > num)
			{
				if (i == 1)
				{
					peko.GetComponent<Enemy>().PauseDirect = 5;
				}
				if (i == 3)
				{
					peko.GetComponent<Enemy>().PauseDirect = 4;
				}
				if (i == 4)
				{
					peko.GetComponent<Enemy>().PauseDirect = 2;
				}
				float num2 = num + tl[i] - startTime;
				Vector3 position = peko.GetComponent<Transform>().position;
				position.y = (tl[i] - num2) / tl[i] * pl[i] + num2 / tl[i] * pl[i + 1] - 0.5f + 0.6f;
				peko.GetComponent<Transform>().position = position;
				return;
			}
		}
		peko.GetComponent<Enemy>().PauseDirect = 4;
		occurSign = 2;
	}
}
