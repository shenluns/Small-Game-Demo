using UnityEngine;

public class Pekora_Cave : EnemyBehavier
{
	private int lstt = -10;

	private GameObject srabbit;

	public int[,] tryAdd = new int[4, 3];

	public GameObject cave2;

	public override void MoveFail()
	{
		if (me.bh.turn - lstt < 10 * (2 - me.bh.dc.Difficulty) || Random.Range(0, 4) != 0 || (srabbit != null && srabbit.GetComponent<Enemy>().Alive != 0))
		{
			return;
		}
		int num = 0;
		for (int i = 0; i < 4; i++)
		{
			tryAdd[num, 0] = me.NowX + (int)me.bh.MovePos[i].x;
			tryAdd[num, 1] = me.NowY + (int)me.bh.MovePos[i].y;
			tryAdd[num, 2] = i;
			if (me.bh.hb.CheckWall(tryAdd[num, 0], tryAdd[num, 1]) == 1 && me.bh.RoleMap[tryAdd[num, 0], tryAdd[num, 1]] == -2)
			{
				num++;
			}
		}
		if (num != 0)
		{
			int num2 = Random.Range(0, num);
			GameObject gameObject = Object.Instantiate(GameObject.Find("Pekora_SRabbit"));
			Enemy component = gameObject.GetComponent<Enemy>();
			component.MaxHp = 12f;
			component.NowHp = 12f;
			component.MoveDirect = tryAdd[num2, 2];
			component.ChangeBehaviour(new Pekora_SRabbit());
			me.bh.AddEnemy(gameObject);
			gameObject.name = "enemy1_" + component.Number;
			component.eb.OnStart();
			if (component.start == 0)
			{
				component.start = 1;
			}
			component.TryMoveSign = 1;
			component.NowX = me.NowX;
			component.NowY = me.NowY;
			component.NextX = tryAdd[num2, 0];
			component.NextY = tryAdd[num2, 1];
			component.NowTime = me.NowTime;
			component.MoveSuccess();
			me.bh.RoleMap[tryAdd[num2, 0], tryAdd[num2, 1]] = component.Number;
			component.eb.StartSign = 1;
			srabbit = gameObject;
			lstt = me.bh.turn;
		}
	}

	public override void OnStart()
	{
		me.H = 1f;
		ShowDefaultHp = 0;
		me.MaxHp = 100f;
		me.NowHp = 100f;
		me.MoveSign = 0;
		me.TryMoveSign = 0;
		me.NextX = me.NowX;
		me.NextY = me.NowY;
		me.Type = 101;
		cave2 = Object.Instantiate(GameObject.Find("Pekora_Cave2"));
		cave2.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX, me.bh.BlockH * (float)me.NowY, 99f);
		cave2.name = "Pekora_Cave2_" + me.Number;
	}

	public override void OnDead()
	{
		GameObject gameObject = Object.Instantiate(GameObject.Find("SpritePlayer/cave-dead"));
		gameObject.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX, me.bh.BlockH * (float)me.NowY, 60f + me.bh.BlockH * (float)me.NowY * 0.01f + 0.0001f);
		gameObject.GetComponent<SpritePlayer>().NowStart();
		base.OnDead();
		if (cave2 != null)
		{
			Object.Destroy(cave2, 0f);
		}
	}

	public override bool BeAttack(float Hp)
	{
		return true;
	}

	public override bool BeAttack(Player player)
	{
		me.NowHp = 0f;
		me.Alive = 0;
		OnDead();
		return false;
	}

	public override void OnShow()
	{
		Vector3 position = new Vector3(me.bh.BlockW * (float)me.NowX, me.bh.BlockH * (float)me.NowY, 0f);
		position.y = position.y - 0.5f + 0.5f * me.H;
		position.z = 99f;
		me.gameObject.GetComponent<Transform>().position = position;
	}
}
