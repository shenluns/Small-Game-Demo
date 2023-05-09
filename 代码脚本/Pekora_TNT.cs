using Fungus;
using UnityEngine;

public class Pekora_TNT : EnemyBehavier
{
	private Pekora_Pekora pekora;

	public int baDirect;

	public int sfly;

	public override void MoveFail()
	{
		if (sfly == 1)
		{
			return;
		}
		if (me.bh.turn - me.burnTurn >= 6)
		{
			me.Alive = 0;
			OnDead();
			for (int i = me.NowX - 2; i <= me.NowX + 2; i++)
			{
				for (int j = me.NowY - 2; j <= me.NowY + 2; j++)
				{
					AttackDate attackDate = new AttackDate();
					attackDate.turn = me.bh.turn;
					attackDate.DamageToPlayer = me.bh.dc.Difficulty + 1;
					attackDate.DamageToEnemy = 6 * (2 - me.bh.dc.Difficulty);
					attackDate.X = i;
					attackDate.Y = j;
					me.bh.al.push(attackDate);
				}
			}
			GameObject gameObject = Object.Instantiate(GameObject.Find("SpritePlayer/baozha2"));
			Vector3 localScale = gameObject.GetComponent<Transform>().localScale;
			gameObject.GetComponent<Transform>().localScale = localScale;
			gameObject.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX, me.bh.BlockH * (float)me.NowY, 30f);
			gameObject.GetComponent<SpritePlayer>().ss = pekora.sss[2];
			gameObject.GetComponent<SpritePlayer>().NowStart();
			SoundCenter.PlayMusic(3, "Audios/Sounds/战斗/炮 SND6326");
			SoundCenter.SetMusicVolume(3, 0.5f);
		}
		else
		{
			if ((me.MoveSituation & 2) == 0)
			{
				return;
			}
			if (me.Type != 203 && baDirect == 0 && me.NowY >= 11)
			{
				sfly = 1;
				me.bh.ForcePause();
				GameObject.Find("Flowchart").GetComponent<Flowchart>().ExecuteBlock("PekoTNTFail-" + me.bh.dc.Language);
				return;
			}
			int num = me.NowX;
			int num2 = me.NowY;
			for (int k = 1; k <= 4; k++)
			{
				int num3 = num + (int)me.bh.MovePos[baDirect].x;
				int num4 = num2 + (int)me.bh.MovePos[baDirect].y;
				if (me.bh.hb.CheckWall(num3, num4) != 1 || me.bh.RoleMap[num3, num4] != -2)
				{
					break;
				}
				num = num3;
				num2 = num4;
			}
			me.bh.RoleMap[me.NowX, me.NowY] = -2;
			me.bh.RoleMap[num, num2] = me.Number;
			me.NextX = num;
			me.NextY = num2;
			me.TryMoveSign = 1;
			me.MoveSuccess();
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
		pekora = (Pekora_Pekora)GameObject.Find("enemy1_0").GetComponent<Enemy>().eb;
	}

	public override void ChangeSpriteOnStand()
	{
		me.spr.sprite = pekora.sss[5].GetSprite(me.NowTime - me.LastTime);
	}

	public override void ChangeSpriteOnPause()
	{
		me.spr.sprite = pekora.sss[5].GetSprite(Time.time - me.bh.PauseTime);
	}

	public override void ChangeSpriteOnMove()
	{
		me.spr.sprite = pekora.sss[5].GetSprite(me.NowTime - me.LastTime);
	}

	public override bool BeAttack(float Hp)
	{
		return true;
	}

	public override bool BeAttack(Player player)
	{
		me.MoveSituation |= 2;
		if (player.NowY < me.NowY)
		{
			baDirect = 0;
		}
		if (player.NowX < me.NowX)
		{
			baDirect = 1;
		}
		if (player.NowY > me.NowY)
		{
			baDirect = 2;
		}
		if (player.NowX > me.NowX)
		{
			baDirect = 3;
		}
		return true;
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
				position.z = 60f + position.y * 0.01f;
				position.y = position.y - 0.5f + 0.5f * me.H;
				if (me.burnTurn == me.bh.turn)
				{
					position.z = 60f + me.bh.BlockH * (float)me.NowY * 0.01f;
				}
				me.gameObject.GetComponent<Transform>().position = position;
			}
		}
		if (me.MoveSign == 0)
		{
			Vector3 position2 = new Vector3(me.bh.BlockW * (float)me.NowX, me.bh.BlockH * (float)me.NowY, 0f);
			position2.z = 60f + position2.y * 0.01f;
			position2.y = position2.y - 0.5f + 0.5f * me.H;
			me.gameObject.GetComponent<Transform>().position = position2;
		}
	}

	public override void OnShow()
	{
		if (sfly == 1)
		{
			ChangeSpriteOnPause();
		}
		else
		{
			base.OnShow();
		}
	}
}
