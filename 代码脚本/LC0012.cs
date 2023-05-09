using UnityEngine;

public class LC0012 : BattleListenerCase_1
{
	private GameObject tnt;

	private GameObject sr2;

	private GameObject sr3;

	private float startTime;

	private int occurSign;

	private Vector3 to = new Vector3(4.32f, 22.78f, 30f);

	private Vector3 from;

	private Vector3 from2;

	private Vector3 from3;

	private Vector3 to2 = new Vector3(-13.45f, 39.24f, 99f);

	private Vector3 to3 = new Vector3(18.93f, 38.73f, 99f);

	public override bool Condition()
	{
		if (publicSign == 0 || occurSign == 3)
		{
			return false;
		}
		if (occurSign == 0)
		{
			occurSign = 1;
			startTime = Time.time;
			for (int i = 1; i < bl.bh.EnemyNum; i++)
			{
				if (bl.bh.enemy[i].Type == 103 && bl.bh.enemy[i].Alive == 1)
				{
					tnt = bl.bh.enemy[i].gameObject;
					break;
				}
			}
			from = tnt.GetComponent<Transform>().position;
			from.z = 30f;
			((Pekora_Pekora)bl.bh.enemy[0].eb).stnt = 0;
			GameObject.Find("Main Camera").GetComponent<camera001>().center = tnt;
		}
		return true;
	}

	public override void OnTrue()
	{
		if (occurSign == 1)
		{
			float time = Time.time;
			float num = 0.8f;
			if (time - startTime < num)
			{
				tnt.GetComponent<Transform>().position = (num - (time - startTime)) / num * from + (time - startTime) / num * to;
				return;
			}
			tnt.GetComponent<Transform>().position = to;
			if (time - startTime < 3f)
			{
				return;
			}
			tnt.GetComponent<Enemy>().Alive = 0;
			tnt.GetComponent<Enemy>().eb.OnDead();
			GameObject gameObject = Object.Instantiate(GameObject.Find("SpritePlayer/baozha2"));
			Vector3 localScale = gameObject.GetComponent<Transform>().localScale;
			gameObject.GetComponent<Transform>().localScale = localScale;
			gameObject.GetComponent<Transform>().position = new Vector3(4.5f, 24f, 30f);
			gameObject.GetComponent<SpritePlayer>().ss = ((Pekora_Pekora)GameObject.Find("enemy1_0").GetComponent<Enemy>().eb).sss[2];
			gameObject.GetComponent<SpritePlayer>().NowStart();
			sr2 = GameObject.Find("sence001/srabbit/srabbit (1)");
			sr3 = GameObject.Find("sence001/srabbit/srabbit (2)");
			from2 = sr2.GetComponent<Transform>().position;
			from3 = sr3.GetComponent<Transform>().position;
			GameObject.Find("sence001/TNT").GetComponent<Transform>().localScale = new Vector3(0f, 0f, 1f);
			occurSign = 2;
		}
		if (occurSign == 2)
		{
			float num2 = Time.time - 3f;
			float num3 = 0.8f;
			if (num2 - startTime < num3)
			{
				sr2.GetComponent<Transform>().position = (num3 - (num2 - startTime)) / num3 * from2 + (num2 - startTime) / num3 * to2;
				sr3.GetComponent<Transform>().position = (num3 - (num2 - startTime)) / num3 * from3 + (num2 - startTime) / num3 * to3;
				return;
			}
		}
		occurSign = 3;
	}
}
