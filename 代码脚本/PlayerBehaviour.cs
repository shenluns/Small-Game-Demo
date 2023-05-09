using UnityEngine;

public class PlayerBehaviour
{
	public Player me;

	public int StartSign;

	public virtual void ChangePos()
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

	public virtual void OnShow()
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

	public virtual void BeAttack(float Hp)
	{
		me.NowHp -= Hp;
		me.MoveSituation |= 2;
	}

	public virtual void BeAttack(Enemy enemy)
	{
		me.NowHp -= enemy.ATK;
		me.MoveSituation |= 2;
	}

	public virtual void OnStart()
	{
	}

	public virtual void ChangeSpriteOnStand()
	{
	}

	public virtual void ChangeSpriteOnMove()
	{
	}

	public virtual void ChangeSpriteOnPause()
	{
	}

	public virtual void MoveFail()
	{
	}

	public virtual void MoveSuccess()
	{
	}

	public virtual void RealMove()
	{
		me.LastX = me.NowX;
		me.LastY = me.NowY;
		me.NowX = me.NextX;
		me.NowY = me.NextY;
	}

	public virtual void EndMove()
	{
		me.MoveSign = 0;
		me.LastTime = me.NowTime;
	}

	public virtual void TryMove()
	{
		me.MoveSituation = 0;
		if (me.PressedKey >= 0 && me.PressedKey <= 3 && me.MoveSign == 0)
		{
			me.TryMoveSign = 1;
			me.MoveDirect = me.PressedKey;
			me.NextX = me.NowX + (int)me.bh.MovePos[me.MoveDirect].x;
			me.NextY = me.NowY + (int)me.bh.MovePos[me.MoveDirect].y;
		}
		else
		{
			me.NextX = me.NowX;
			me.NextY = me.NowY;
		}
	}

	public virtual void PressKeyInBattle()
	{
		if (me.PressedKey != -1)
		{
			if (me.PressedKey >= 0 && me.PressedKey <= 3)
			{
				TryMove();
			}
			if (me.PressedKey == 4 || me.PressedKey == 5)
			{
				me.bh.ChangeMusicTime = me.bh.NowTime;
				me.bh.NextMusic = (me.bh.NowMusic + ((me.PressedKey != 4) ? 1 : (-1)) + me.bh.MusicNum) % me.bh.MusicNum;
				me.bh.ChangeMusicSign = 1;
			}
		}
	}

	public virtual void CheckMoney()
	{
		if (me.bh.MoneyMap[me.NowX, me.NowY] > 0)
		{
			GameObject.Find("money-" + me.bh.MoneyMap[me.NowX, me.NowY]).GetComponent<SpritePlayer>().NowStart();
			me.bh.MoneyMap[me.NowX, me.NowY] = -1;
			me.bh.playerMoney++;
		}
	}

	public virtual void OnUpdate()
	{
		me.NowTime = me.bh.NowTime;
		float num = 60f / me.bh.BPM - 2f * me.bh.WrTime;
		if (me.MoveSign == 1 && me.NowTime - me.LastTime >= num)
		{
			RealMove();
		}
		CheckMoney();
		OnShow();
	}
}
