using UnityEngine;

public class HubBehaviour
{
	public BattleHub me;

	public int end;

	public int StartSign;

	public int[] EnemyMoveSign = new int[1000];

	public int PlayerMoveSign;

	public int[,] TmpMap = new int[310, 310];

	public int PauseInHB;

	public virtual void OnStart()
	{
		for (int i = 0; i < 20; i++)
		{
			GameObject gameObject = Object.Instantiate(GameObject.Find("Clap"));
			gameObject.GetComponent<Clap>().dirct = 0;
			gameObject.GetComponent<Clap>().num = i;
			gameObject.name = "Clap" + 0 + " " + i;
			GameObject gameObject2 = Object.Instantiate(GameObject.Find("Clap"));
			gameObject2.GetComponent<Clap>().dirct = 1;
			gameObject2.GetComponent<Clap>().num = i;
			gameObject2.name = "Clap" + 1 + " " + i;
		}
		for (int j = 0; j < 20; j++)
		{
			GameObject gameObject3 = Object.Instantiate(GameObject.Find("PlayerHp"));
			gameObject3.GetComponent<PlayerHp>().num = j;
			gameObject3.name = "PlayerHp " + j;
		}
	}

	public virtual int CheckKeyInBattle()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			return 273;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			return 275;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			return 274;
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			return 276;
		}
		return -1;
	}

	public virtual void TryMove()
	{
		for (int i = 0; i < me.EnemyNum; i++)
		{
			if (me.enemy[i].Alive == 1)
			{
				me.enemy[i].TryMove();
			}
		}
	}

	public int CheckWall(int x, int y)
	{
		if (x < 0 || y < 0 || x >= me.MapW || x >= me.MapH)
		{
			return 0;
		}
		if (me.GameMap[x, y] == 0)
		{
			return 0;
		}
		return 1;
	}

	public virtual void CheckMove()
	{
		int num = me.EnemyNum;
		PlayerMoveSign = me.player.TryMoveSign;
		for (int i = 0; i <= me.MapW; i++)
		{
			for (int j = 0; j <= me.MapH; j++)
			{
				TmpMap[i, j] = 0;
			}
		}
		if (CheckWall(me.player.NextX, me.player.NextY) == 0)
		{
			PlayerMoveSign = 0;
			me.player.MoveSituation |= 4;
		}
		for (int k = 0; k < num; k++)
		{
			if (me.enemy[k].Alive != 1 || me.enemy[k].eb.StartSign != 1)
			{
				continue;
			}
			if (me.player.NextX == me.enemy[k].NowX && me.player.NextY == me.enemy[k].NowY)
			{
				bool num2 = me.enemy[k].BeAttact(me.player);
				me.player.MoveSituation |= 1;
				PlayerMoveSign = 0;
				if (!num2)
				{
					k--;
					num--;
					continue;
				}
			}
			if (me.enemy[k].Alive == 1)
			{
				EnemyMoveSign[k] = me.enemy[k].TryMoveSign;
				if (CheckWall(me.enemy[k].NextX, me.enemy[k].NextY) == 0)
				{
					EnemyMoveSign[k] = 0;
					me.enemy[k].MoveSituation |= 4;
				}
				if (EnemyMoveSign[k] == 0 || TmpMap[me.enemy[k].NextX, me.enemy[k].NextY] == -1)
				{
					TmpMap[me.enemy[k].NowX, me.enemy[k].NowY] = -1;
				}
				else
				{
					TmpMap[me.enemy[k].NextX, me.enemy[k].NextY]++;
				}
			}
		}
		if (PlayerMoveSign == 1)
		{
			TmpMap[me.player.NextX, me.player.NextY] = -1;
		}
		else
		{
			TmpMap[me.player.NowX, me.player.NowY] = -1;
		}
		int num3 = 1;
		while (num3 == 1)
		{
			num3 = 0;
			for (int l = 0; l < num; l++)
			{
				if (me.enemy[l].Alive == 1 && EnemyMoveSign[l] != 0 && me.enemy[l].eb.StartSign != 0 && TmpMap[me.enemy[l].NextX, me.enemy[l].NextY] != 1)
				{
					EnemyMoveSign[l] = 0;
					TmpMap[me.enemy[l].NowX, me.enemy[l].NowY] = -1;
					num3 = 1;
				}
			}
		}
		for (int m = 0; m <= me.MapW; m++)
		{
			for (int n = 0; n <= me.MapH; n++)
			{
				me.RoleMap[m, n] = -2;
			}
		}
		if (PlayerMoveSign == 1)
		{
			me.RoleMap[me.player.NextX, me.player.NextY] = -1;
		}
		else
		{
			me.RoleMap[me.player.NowX, me.player.NowY] = -1;
		}
		for (int num4 = 0; num4 < num; num4++)
		{
			if (me.enemy[num4].Alive == 1 && me.enemy[num4].TryMoveSign == 1 && me.enemy[num4].eb.StartSign == 1 && me.RoleMap[me.enemy[num4].NextX, me.enemy[num4].NextY] == -1)
			{
				me.player.BeAttact(me.enemy[num4]);
				me.enemy[num4].MoveSituation |= 1;
			}
		}
		for (int num5 = 0; num5 < num; num5++)
		{
			if (me.enemy[num5].Alive == 1 && me.enemy[num5].eb.StartSign == 1)
			{
				if (EnemyMoveSign[num5] == 1)
				{
					me.RoleMap[me.enemy[num5].NextX, me.enemy[num5].NextY] = num5;
				}
				else
				{
					me.RoleMap[me.enemy[num5].NowX, me.enemy[num5].NowY] = num5;
				}
			}
		}
		if (PlayerMoveSign == 1)
		{
			me.player.MoveSuccess();
		}
		else
		{
			me.player.MoveFail();
		}
		for (int num6 = 0; num6 < num; num6++)
		{
			if (me.enemy[num6].Alive == 1 && me.enemy[num6].eb.StartSign == 1)
			{
				if (EnemyMoveSign[num6] == 1)
				{
					me.enemy[num6].MoveSuccess();
				}
				else
				{
					me.enemy[num6].MoveFail();
				}
			}
		}
	}

	public virtual void CheckAttackList()
	{
		while (me.al.Size() != 0)
		{
			AttackDate attackDate = me.al.top();
			if (attackDate.turn > me.turn)
			{
				break;
			}
			me.al.pop();
			if (attackDate.turn >= me.turn)
			{
				if (me.RoleMap[attackDate.X, attackDate.Y] == -1)
				{
					me.player.BeAttact(attackDate.DamageToPlayer);
				}
				else if (me.RoleMap[attackDate.X, attackDate.Y] != -2 && me.enemy[me.RoleMap[attackDate.X, attackDate.Y]].Alive == 1 && me.enemy[me.RoleMap[attackDate.X, attackDate.Y]].eb.StartSign == 1)
				{
					me.enemy[me.RoleMap[attackDate.X, attackDate.Y]].BeAttact(attackDate.DamageToEnemy);
				}
			}
		}
	}

	public virtual int CheckEnd()
	{
		return 0;
	}

	public virtual void EndGame()
	{
		end = 1;
	}

	public virtual void Pause()
	{
	}

	public virtual void PauseEnd()
	{
	}

	private float max_(float x, float y)
	{
		if (!(x > y))
		{
			return y;
		}
		return x;
	}

	public virtual void OnUpdate()
	{
		if (CheckEnd() == 1)
		{
			EndGame();
		}
		if (end == 1)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (me.PauseSign == 0)
			{
				GameObject.Find("Canvas/Pause").GetComponent<PauseInBattle>().ClickPause();
			}
			else if (PauseInHB == 1)
			{
				GameObject.Find("Canvas/Pause").GetComponent<PauseInBattle>().ClickPause_Continue();
			}
		}
		if (me.PauseSign == 1)
		{
			return;
		}
		me.NowTime = Time.time - me.PauseCostTime - me.StartTime - me.dc.DelayTime;
		float num = 60f / me.BPM;
		float num2 = me.NowTime + num * 3f - (float)(int)((me.NowTime + num * 3f) / 60f * me.BPM) * 60f / me.BPM;
		if (me.ChangeMusicSign == 1)
		{
			if (me.NowTime - me.ChangeMusicTime > num * 4f)
			{
				SoundCenter.SetMusicVolume(me.NowMusic, 0f);
				SoundCenter.SetMusicVolume(me.NextMusic, 0.5f);
				me.NowMusic = me.NextMusic;
				me.ChangeMusicSign = 0;
			}
			else
			{
				float num3 = (me.NowTime - me.ChangeMusicTime) / (num * 4f) * 0.5f;
				SoundCenter.SetMusicVolume(me.NowMusic, 0.5f - num3);
				SoundCenter.SetMusicVolume(me.NextMusic, num3);
			}
		}
		int num4 = CheckKeyInBattle();
		_ = -1;
		if ((num2 < me.WrTime || num2 > num - me.WrTime) && me.NowTime - me.LastTime > num - 2f * me.WrTime && me.NowTime - me.PressFailTime > 0.5f * num)
		{
			if (num4 != -1)
			{
				me.turn++;
				me.PressSuccessTime = (me.LastTime = me.NowTime);
				me.player.PressKeyInBattle(num4);
				TryMove();
				CheckMove();
				CheckAttackList();
				if (((uint)me.player.MoveSituation & (true ? 1u : 0u)) != 0)
				{
					me.Combo++;
				}
				if (((uint)me.player.MoveSituation & 2u) != 0)
				{
					me.Combo = 0;
				}
			}
		}
		else if (num4 != -1 && !(num2 < me.WrTime) && !(num2 > num - me.WrTime))
		{
			SoundCenter.PlayMusic(6, "Audios/Sounds/在非节拍处按下按键 SND24066");
			SoundCenter.SetMusicVolume(6, me.dc.maxVolume * me.dc.maxSoundVolume);
			me.PressFailTime = me.NowTime;
			me.Combo = 0;
		}
		if (me.NowTime - me.LastTime >= num && num2 >= me.WrTime && num2 < 0.5f * num)
		{
			me.turn++;
			me.LastTime = me.NowTime;
			me.player.MoveSituation = 0;
			me.player.TryMoveSign = 0;
			TryMove();
			CheckMove();
			CheckAttackList();
			if (((uint)me.player.MoveSituation & 2u) != 0)
			{
				me.Combo = 0;
			}
		}
		if (me.Combo >= 3)
		{
			me.cbs.ChangeBlock((me.turn & 1) + 1);
		}
		else
		{
			me.cbs.ChangeBlock(0);
		}
	}

	public virtual void OnAwake()
	{
	}
}
