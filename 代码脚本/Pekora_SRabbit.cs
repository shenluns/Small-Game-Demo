using UnityEngine;

public class Pekora_SRabbit : EnemyBehavier
{
	private static SpriteSwitcher[] ss = new SpriteSwitcher[6];

	private string[] FileName = new string[4] { "byobyo", "ganbare", "hekon", "jump" };

	public int StandAction;

	public override void OnStart()
	{
		me.H = 1.2f;
		ShowDefaultHp = 1;
		me.MoveSign = 0;
		me.TryMoveSign = 0;
		me.NowTime = (me.LastTime = 0f);
		me.DEF = 0f;
		me.ATK = 1f;
		me.PauseDirect = 4;
		me.Type = 102;
		if (ss[0] != null)
		{
			return;
		}
		for (int i = 0; i < 3; i++)
		{
			ss[i] = new SpriteSwitcher(1, 4);
			for (int j = 0; j < 4; j++)
			{
				ss[i].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/pekoboss-sozai/Pekoraboss/nousagi-" + FileName[i] + "_000" + j));
			}
		}
		ss[3] = new SpriteSwitcher(1, 8);
		for (int k = 0; k < 8; k++)
		{
			ss[3].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/pekoboss-sozai/Pekoraboss/nousagi-" + FileName[3] + "_000" + k));
		}
	}

	public override void ChangeSpriteOnStand()
	{
		float totleTime = 60f / me.bh.BPM;
		ss[0].SetTotleTime(totleTime);
		me.spr.sprite = ss[0].GetSprite(me.NowTime - me.LastTime);
	}

	public override void ChangeSpriteOnPause()
	{
		float totleTime = 60f / me.bh.BPM;
		float time = Time.time - me.bh.PauseTime;
		ss[0].SetTotleTime(totleTime);
		me.spr.sprite = ss[0].GetSprite(time);
	}

	public override void ChangeSpriteOnMove()
	{
		float totleTime = 60f / me.bh.BPM - 2f * me.bh.WrTime;
		ss[3].SetTotleTime(totleTime);
		me.spr.sprite = ss[3].GetSprite(me.NowTime - me.LastTime);
	}

	public override void TryMove()
	{
		if ((me.bh.turn - me.burnTurn) % (3 - me.bh.dc.Difficulty) != 0)
		{
			return;
		}
		me.TryMoveSign = 1;
		int num = me.bh.player.NowX - me.NowX;
		int num2 = me.bh.player.NowY - me.NowY;
		num = ((num != 0) ? ((num > 0) ? 1 : (-1)) : 0);
		num2 = ((num2 != 0) ? ((num2 > 0) ? 1 : (-1)) : 0);
		if (num != 0 && num2 != 0)
		{
			if (Random.Range(0, 2) == 0)
			{
				num = 0;
			}
			else
			{
				num2 = 0;
			}
		}
		me.NextX = num + me.NowX;
		me.NextY = num2 + me.NowY;
		if (num2 == 1)
		{
			me.MoveDirect = 0;
		}
		if (num == 1)
		{
			me.MoveDirect = 1;
		}
		if (num2 == -1)
		{
			me.MoveDirect = 2;
		}
		if (num == -1)
		{
			me.MoveDirect = 3;
		}
	}

	public override void OnDead()
	{
		GameObject gameObject = Object.Instantiate(GameObject.Find("SpritePlayer/srabbit-dead"));
		gameObject.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX + 0.02f, me.bh.BlockH * (float)me.NowY + 0.08f, 60f + me.bh.BlockH * (float)me.NowY * 0.01f - 0.0001f);
		gameObject.GetComponent<SpritePlayer>().NowStart();
		if (Random.Range(0, 2) == 0)
		{
			me.bh.AddMoney(me.NowX, me.NowY);
		}
		base.OnDead();
	}

	public override void MoveFail()
	{
		if (me.TryMoveSign == 1 && ((uint)me.MoveSituation & (true ? 1u : 0u)) != 0)
		{
			if (me.MoveDirect == 0)
			{
				GameObject gameObject = Object.Instantiate(GameObject.Find("SpritePlayer/jinzhan-sx1"));
				Vector3 localScale = gameObject.GetComponent<Transform>().localScale;
				localScale.y *= -1f;
				gameObject.GetComponent<Transform>().localScale = localScale;
				gameObject.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX, me.bh.BlockH * (float)me.NowY + 0.2f, 60f + me.bh.BlockH * (float)(me.NowY + 1) * 0.01f - 0.0001f);
				gameObject.GetComponent<SpritePlayer>().ss = ((Pekora_Pekora)me.bh.enemy[0].eb).sss[0];
				gameObject.GetComponent<SpritePlayer>().bh = me.bh;
				gameObject.GetComponent<SpritePlayer>().NowStart();
			}
			if (me.MoveDirect == 1)
			{
				GameObject gameObject2 = Object.Instantiate(GameObject.Find("SpritePlayer/jinzhan-zy1"));
				Vector3 localScale2 = gameObject2.GetComponent<Transform>().localScale;
				localScale2.x *= -1f;
				gameObject2.GetComponent<Transform>().localScale = localScale2;
				gameObject2.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX + 0.7f, me.bh.BlockH * (float)me.NowY, 60f + me.bh.BlockH * (float)me.NowY * 0.01f - 0.0001f);
				gameObject2.GetComponent<SpritePlayer>().ss = ((Pekora_Pekora)me.bh.enemy[0].eb).sss[1];
				gameObject2.GetComponent<SpritePlayer>().bh = me.bh;
				gameObject2.GetComponent<SpritePlayer>().NowStart();
			}
			if (me.MoveDirect == 2)
			{
				GameObject gameObject3 = Object.Instantiate(GameObject.Find("SpritePlayer/jinzhan-sx1"));
				gameObject3.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX, me.bh.BlockH * (float)me.NowY - 0.2f, 60f + me.bh.BlockH * (float)(me.NowY - 1) * 0.01f - 0.0001f);
				gameObject3.GetComponent<SpritePlayer>().ss = ((Pekora_Pekora)me.bh.enemy[0].eb).sss[0];
				gameObject3.GetComponent<SpritePlayer>().bh = me.bh;
				gameObject3.GetComponent<SpritePlayer>().NowStart();
			}
			if (me.MoveDirect == 3)
			{
				GameObject gameObject4 = Object.Instantiate(GameObject.Find("SpritePlayer/jinzhan-zy1"));
				gameObject4.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX - 0.7f, me.bh.BlockH * (float)me.NowY, 60f + me.bh.BlockH * (float)me.NowY * 0.01f - 0.0001f);
				gameObject4.GetComponent<SpritePlayer>().ss = ((Pekora_Pekora)me.bh.enemy[0].eb).sss[1];
				gameObject4.GetComponent<SpritePlayer>().bh = me.bh;
				gameObject4.GetComponent<SpritePlayer>().NowStart();
			}
		}
	}

	public override void ChangePos()
	{
		float num = 60f / me.bh.BPM - 2f * me.bh.WrTime;
		if (me.MoveSign != 0)
		{
			if (me.NowTime - me.LastTime >= num)
			{
				EndMove();
			}
			else
			{
				float num2 = (me.NowTime - me.LastTime) / num;
				Vector3 position = new Vector3(me.bh.BlockW * ((float)me.LastX * (1f - num2) + (float)me.NowX * num2), me.bh.BlockH * ((float)me.LastY * (1f - num2) + (float)me.NowY * num2), 0f);
				position.x += 0.02f;
				position.z = 60f + position.y * 0.01f;
				position.y = position.y - 0.5f + 0.5f * me.H;
				me.gameObject.GetComponent<Transform>().position = position;
			}
		}
		if (me.MoveSign == 0)
		{
			Vector3 position2 = new Vector3(me.bh.BlockW * (float)me.NowX, me.bh.BlockH * (float)me.NowY, 0f);
			position2.x += 0.02f;
			position2.z = 60f + position2.y * 0.01f;
			position2.y = position2.y - 0.5f + 0.5f * me.H;
			me.gameObject.GetComponent<Transform>().position = position2;
		}
		if (((uint)me.MoveSituation & 2u) != 0 && me.bh.NowTime - me.bh.LastTime < 0.2f)
		{
			me.spr.color = new Color(0.6f, 0.6f, 0.6f, 1f);
		}
		else
		{
			me.spr.color = new Color(1f, 1f, 1f, 1f);
		}
	}

	public override void OnShow()
	{
		if (me.bh.PauseSign == 1)
		{
			Vector3 position = me.gameObject.GetComponent<Transform>().position;
			position.z = 60f + (position.y + 0.5f - 0.5f * me.H) * 0.01f;
			me.gameObject.GetComponent<Transform>().position = position;
			if (me.bh.PauseButAct == 1)
			{
				ChangeSpriteOnPause();
			}
		}
		else
		{
			ChangePos();
			if (me.MoveSign != 0)
			{
				ChangeSpriteOnMove();
			}
			else
			{
				ChangeSpriteOnStand();
			}
		}
	}
}
