using UnityEngine;

public class EnemyBehavier
{
	public Enemy me;

	public int ShowDefaultHp;

	public int StartSign;

	public virtual void OnStart()
	{
	}

	public virtual void TryMove()
	{
	}

	public virtual void MoveFail()
	{
	}

	public virtual void MoveSuccess()
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

	public virtual void OnDead()
	{
		if (me.HpA != null)
		{
			Object.Destroy(me.HpA, 0f);
		}
		if (me.HpB != null)
		{
			Object.Destroy(me.HpB, 0f);
		}
		me.gameObject.GetComponent<Transform>().localScale = new Vector3(0f, 0f, 0f);
		Enemy enemy = me.bh.enemy[me.bh.EnemyNum - 1];
		enemy.Number = me.Number;
		enemy.gameObject.name = "enemy1_" + enemy.Number;
		me.bh.enemy[me.bh.EnemyNum - 1] = me;
		me.bh.enemy[enemy.Number] = enemy;
		me.bh.EnemyNum--;
		me.bh.RoleMap[enemy.NowX, enemy.NowY] = enemy.Number;
		me.bh.RoleMap[me.NowX, me.NowY] = -2;
		Object.Destroy(me.gameObject, 1f);
	}

	public virtual bool BeAttack(float Hp)
	{
		me.MoveSituation |= 2;
		me.NowHp -= Hp - me.DEF;
		if (me.NowHp <= 0f)
		{
			me.Alive = 0;
			OnDead();
			return false;
		}
		return true;
	}

	public virtual bool BeAttack(Player player)
	{
		me.MoveSituation |= 2;
		me.NowHp -= player.ATK - me.DEF;
		if (me.NowHp <= 0f)
		{
			me.Alive = 0;
			OnDead();
			return false;
		}
		return true;
	}

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

	public virtual void CheckMoney()
	{
		if (me.bh.MoneyMap[me.NowX, me.NowY] > 0)
		{
			GameObject.Find("money-" + me.bh.MoneyMap[me.NowX, me.NowY]).GetComponent<SpritePlayer>().NowStart();
			me.bh.MoneyMap[me.NowX, me.NowY] = -1;
		}
	}

	public virtual void OnUpdate()
	{
		if (me.NowHp != me.MaxHp && me.ShowHp == 0 && ShowDefaultHp == 1)
		{
			me.ShowHp = 1;
			me.HpA = Object.Instantiate(GameObject.Find("HpA"));
			me.HpA.name = "HpA" + me.EnemyNum;
			me.HpB = Object.Instantiate(GameObject.Find("HpB"));
			me.HpB.name = "HpB" + me.EnemyNum;
			if (me.NowHp <= 0f)
			{
				me.Alive = 0;
				Object.Destroy(me.HpA, 1f);
				Object.Destroy(me.HpB, 1f);
			}
		}
		if (me.Alive == 1)
		{
			me.NowTime = me.bh.NowTime;
			CheckMoney();
			OnShow();
		}
		if (me.ShowHp == 1 && (bool)me.HpA && (bool)me.HpB && ShowDefaultHp == 1)
		{
			Vector3 position = me.HpA.GetComponent<Transform>().position;
			Vector3 position2 = me.HpB.GetComponent<Transform>().position;
			position2.x = (position.x = me.gameObject.GetComponent<Transform>().position.x);
			position.y = (position2.y = me.gameObject.GetComponent<Transform>().position.y + 0.5f * me.H + 0.1f);
			me.HpA.GetComponent<Transform>().position = position;
			me.HpB.GetComponent<Transform>().position = position2;
			me.HpB.GetComponent<HpB>().change(me.NowHp / me.MaxHp);
		}
		else if (ShowDefaultHp == 0)
		{
			if (me.HpA != null)
			{
				Object.Destroy(me.HpA, 0f);
			}
			if (me.HpB != null)
			{
				Object.Destroy(me.HpB, 0f);
			}
			me.ShowHp = 0;
		}
	}
}
