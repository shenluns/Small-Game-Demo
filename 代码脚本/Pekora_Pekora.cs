using Fungus;
using UnityEngine;

public class Pekora_Pekora : EnemyBehavier
{
	public static SpriteSwitcher[] ss = new SpriteSwitcher[20];

	public SpriteSwitcher[] sss = new SpriteSwitcher[7];

	private string[] FileName = new string[5] { "背面", "向右", "正面", "向左", "站立" };

	public int StandAction;

	private static int[] tcsosjz = new int[4] { 1, 0, 3, 2 };

	public int tjz = -100;

	public int thj = -10;

	public int tzh = 60;

	public int atkDirect;

	public int daodannum;

	public int[,] daodan = new int[10, 2];

	public int ttnt = -100;

	public int stnt;

	public int szd;

	private int abs(int x)
	{
		if (x <= 0)
		{
			return -1 * x;
		}
		return x;
	}

	public override void OnDead()
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
		Debug.Log("hi");
		GameObject gameObject = Object.Instantiate(GameObject.Find("SpritePlayer/Pekora-GG"));
		Vector3 localScale = gameObject.GetComponent<Transform>().localScale;
		gameObject.GetComponent<Transform>().localScale = localScale;
		gameObject.GetComponent<Transform>().position = me.gameObject.GetComponent<Transform>().position;
		gameObject.GetComponent<SpritePlayer>().ss = sss[6];
		gameObject.GetComponent<SpritePlayer>().LoopTime = -1;
		gameObject.GetComponent<SpritePlayer>().NowStart();
	}

	public override void OnStart()
	{
		ShowDefaultHp = 1;
		me.MoveSign = 0;
		me.TryMoveSign = 0;
		me.NowTime = (me.LastTime = 0f);
		me.DEF = 0f;
		me.ATK = 1f;
		me.PauseDirect = 4;
		if (ss[0] == null)
		{
			for (int i = 0; i < 4; i++)
			{
				ss[i] = new SpriteSwitcher(1, 8);
				for (int j = 1; j <= 8; j++)
				{
					ss[i].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/" + FileName[i] + "-export" + j));
				}
			}
			ss[4] = new SpriteSwitcher(1, 7);
			for (int k = 1; k <= 7; k++)
			{
				ss[4].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/" + FileName[4] + "-export" + k));
			}
			ss[5] = new SpriteSwitcher(1, 4);
			for (int l = 8; l <= 11; l++)
			{
				ss[5].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/" + FileName[4] + "-export" + l));
			}
			for (int m = 6; m <= 9; m++)
			{
				ss[m] = new SpriteSwitcher(1, 5);
				for (int n = m * 9 - 52; n < m * 9 - 47; n++)
				{
					ss[m].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/jinzhan/pekorajinzhan" + n));
				}
			}
			for (int num = 10; num <= 13; num++)
			{
				int num2 = num - 4;
				ss[num] = new SpriteSwitcher(1, 4);
				for (int num3 = num2 * 9 - 47; num3 < num2 * 9 - 43; num3++)
				{
					ss[num].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/jinzhan/pekorajinzhan" + num3));
				}
			}
			ss[14] = new SpriteSwitcher(1, 9);
			for (int num4 = 1; num4 <= 9; num4++)
			{
				ss[14].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/kaipao/jinzhan gongji kaipao 002 " + num4));
			}
			ss[15] = new SpriteSwitcher(1, 8);
			for (int num5 = 10; num5 <= 17; num5++)
			{
				ss[15].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/kaipao/jinzhan gongji kaipao 002 " + num5));
			}
			ss[16] = new SpriteSwitcher(1, 4);
			for (int num6 = 1; num6 <= 4; num6++)
			{
				ss[16].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/others/jiaku" + num6));
			}
			ss[17] = new SpriteSwitcher(1, 4);
			for (int num7 = 1; num7 <= 4; num7++)
			{
				ss[17].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/others/zhenku" + num7));
			}
			ss[18] = new SpriteSwitcher(1, 2);
			for (int num8 = 1; num8 <= 2; num8++)
			{
				ss[18].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/others/daxiao" + num8));
			}
		}
		float num9 = 60f / me.bh.BPM;
		sss[0] = new SpriteSwitcher(1, 4);
		for (int num10 = 1; num10 <= 4; num10++)
		{
			sss[0].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/jinzhan/jianqi" + num10));
		}
		sss[0].SetTotleTime(num9);
		sss[1] = new SpriteSwitcher(1, 4);
		for (int num11 = 5; num11 <= 8; num11++)
		{
			sss[1].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/jinzhan/jianqi" + num11));
		}
		sss[1].SetTotleTime(num9);
		sss[2] = new SpriteSwitcher(1, 9);
		for (int num12 = 1; num12 <= 9; num12++)
		{
			sss[2].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/kaipao/warning/baozha" + num12));
		}
		sss[2].SetTotleTime(1.5f * num9);
		sss[3] = new SpriteSwitcher(1, 8);
		for (int num13 = 10; num13 <= 17; num13++)
		{
			sss[3].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/kaipao/jinzhan gongji kaipao daodan " + num13));
		}
		sss[3].SetTotleTime(num9);
		sss[4] = new SpriteSwitcher(1, 5);
		for (int num14 = 1; num14 <= 5; num14++)
		{
			sss[4].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/kaipao/warning/jingao" + num14));
		}
		sss[4].SetTotleTime(num9);
		sss[5] = new SpriteSwitcher(1, 2);
		sss[5].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/pekoboss-sozai/TNT"));
		sss[5].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/pekoboss-sozai/TNT2"));
		sss[5].SetTotleTime(num9);
		sss[6] = new SpriteSwitcher(1, 4);
		for (int num15 = 1; num15 <= 4; num15++)
		{
			sss[6].AddSprite(SpriteCenter.GetSprite("Pictures/Pekora/others/zhenku" + num15));
		}
		sss[6].SetTotleTime(num9);
	}

	public override void ChangePos()
	{
		if (((uint)me.MoveSituation & 2u) != 0 && me.bh.NowTime - me.bh.LastTime < 0.2f)
		{
			me.spr.color = new Color(0.6f, 0.6f, 0.6f, 1f);
		}
		else
		{
			me.spr.color = new Color(1f, 1f, 1f, 1f);
		}
		base.ChangePos();
	}

	public override void ChangeSpriteOnStand()
	{
		float num = 60f / me.bh.BPM;
		if (tjz == me.bh.turn)
		{
			ss[6 + tcsosjz[atkDirect]].SetTotleTime(num);
			me.spr.sprite = ss[6 + tcsosjz[atkDirect]].GetSprite(me.NowTime - me.LastTime);
			return;
		}
		if (tjz == me.bh.turn - 1)
		{
			ss[10 + tcsosjz[atkDirect]].SetTotleTime(num);
			me.spr.sprite = ss[10 + tcsosjz[atkDirect]].GetSprite(me.NowTime - me.LastTime);
			return;
		}
		if (thj == me.bh.turn)
		{
			ss[14].SetTotleTime(num);
			me.spr.sprite = ss[14].GetSprite(me.NowTime - me.LastTime);
			return;
		}
		if (thj == me.bh.turn - 1)
		{
			ss[15].SetTotleTime(num);
			me.spr.sprite = ss[15].GetSprite(me.NowTime - me.LastTime);
			return;
		}
		if (me.NowTime - me.LastTime - (float)(int)((me.NowTime - me.LastTime) / num) * num < 0.1f)
		{
			StandAction = ((Random.Range(0, 10) == 0) ? 1 : 0);
		}
		if (StandAction == 0)
		{
			ss[4].SetTotleTime(num);
			me.spr.sprite = ss[4].GetSprite(me.NowTime - me.LastTime);
		}
		else
		{
			ss[5].SetTotleTime(num);
			me.spr.sprite = ss[5].GetSprite(me.NowTime - me.LastTime);
		}
	}

	public override void ChangeSpriteOnPause()
	{
		float num = 60f / me.bh.BPM;
		float num2 = Time.time - me.bh.PauseTime;
		if (me.PauseDirect == 4)
		{
			if (num2 - (float)(int)(num2 / num) * num < 0.1f)
			{
				StandAction = ((Random.Range(0, 10) == 0) ? 1 : 0);
			}
			if (StandAction == 0)
			{
				ss[4].SetTotleTime(num);
				me.spr.sprite = ss[4].GetSprite(num2);
			}
			else
			{
				ss[5].SetTotleTime(num);
				me.spr.sprite = ss[5].GetSprite(num2);
			}
		}
		else if (me.PauseDirect == 5)
		{
			ss[5].SetTotleTime(num);
			me.spr.sprite = ss[5].GetSprite(0.01f);
		}
		else if (me.PauseDirect == 16)
		{
			ss[16].SetTotleTime(num);
			me.spr.sprite = ss[16].GetSprite(num2);
		}
		else if (me.PauseDirect == 17)
		{
			ss[17].SetTotleTime(num);
			me.spr.sprite = ss[17].GetSprite(num2);
		}
		else if (me.PauseDirect == 18)
		{
			ss[18].SetTotleTime(num);
			me.spr.sprite = ss[18].GetSprite(num2);
		}
		else
		{
			ss[me.PauseDirect].SetTotleTime(num);
			me.spr.sprite = ss[me.PauseDirect].GetSprite(num2);
		}
	}

	public override void ChangeSpriteOnMove()
	{
		float totleTime = 60f / me.bh.BPM - 2f * me.bh.WrTime;
		ss[me.MoveDirect].SetTotleTime(totleTime);
		me.spr.sprite = ss[me.MoveDirect].GetSprite(me.NowTime - me.LastTime);
	}

	public override void MoveFail()
	{
		if (stnt == 1 && me.bh.turn - ttnt > 15 * (2 - me.bh.dc.Difficulty))
		{
			int num = Random.Range(me.bh.player.NowX - 1, me.bh.player.NowX + 2);
			int num2 = Random.Range(me.bh.player.NowY - 1, me.bh.player.NowY + 2);
			if (me.bh.hb.CheckWall(num, num2) == 1 && me.bh.RoleMap[num, num2] == -2)
			{
				GameObject gameObject = Object.Instantiate(GameObject.Find("Pekora_TNT"));
				Enemy component = gameObject.GetComponent<Enemy>();
				component.Type = 103;
				component.MaxHp = 100f;
				component.NowHp = 100f;
				component.ChangeBehaviour(new Pekora_TNT());
				me.bh.AddEnemy(gameObject);
				gameObject.name = "enemy1_" + component.Number;
				component.eb.OnStart();
				if (component.start == 0)
				{
					component.start = 1;
				}
				component.TryMoveSign = 1;
				component.NowX = num;
				component.NowY = num2 + 8;
				component.NextX = num;
				component.NextY = num2;
				component.NowTime = me.NowTime;
				component.MoveSuccess();
				me.bh.RoleMap[num, num2] = component.Number;
				component.eb.StartSign = 1;
				ttnt = me.bh.turn;
			}
		}
		if (thj == me.bh.turn - 2)
		{
			for (int i = 0; i < daodannum; i++)
			{
				GameObject gameObject2 = Object.Instantiate(GameObject.Find("SpritePlayer/baozha"));
				Vector3 localScale = gameObject2.GetComponent<Transform>().localScale;
				gameObject2.GetComponent<Transform>().localScale = localScale;
				gameObject2.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)daodan[i, 0], me.bh.BlockH * (float)daodan[i, 1] + 0.31f, 60f + me.bh.BlockH * (float)daodan[i, 1] * 0.01f - 0.0001f);
				gameObject2.GetComponent<SpritePlayer>().ss = sss[2];
				gameObject2.GetComponent<SpritePlayer>().NowStart();
				AttackDate attackDate = new AttackDate();
				attackDate.turn = me.bh.turn;
				attackDate.DamageToPlayer = 1f;
				attackDate.DamageToEnemy = 6f;
				attackDate.X = daodan[i, 0];
				attackDate.Y = daodan[i, 1];
				me.bh.al.push(attackDate);
				SoundCenter.PlayMusic(3, "Audios/Sounds/战斗/炮 SND6326");
				SoundCenter.SetMusicVolume(3, me.bh.dc.maxVolume * me.bh.dc.maxSoundVolume);
			}
		}
		if (tjz == me.bh.turn - 1)
		{
			int num3 = 1;
			for (int j = -1; j <= 1; j++)
			{
				AttackDate attackDate2 = new AttackDate();
				attackDate2.turn = me.bh.turn;
				attackDate2.DamageToPlayer = 1f;
				attackDate2.DamageToEnemy = 6f;
				if (atkDirect == 0)
				{
					attackDate2.X = me.NowX + j;
					attackDate2.Y = me.NowY + 1;
					if (me.bh.RoleMap[attackDate2.X, attackDate2.Y] == -1)
					{
						num3 = 0;
					}
				}
				if (atkDirect == 1)
				{
					attackDate2.X = me.NowX + 1;
					attackDate2.Y = me.NowY + j;
					if (me.bh.RoleMap[attackDate2.X, attackDate2.Y] == -1)
					{
						num3 = 0;
					}
				}
				if (atkDirect == 2)
				{
					attackDate2.X = me.NowX + j;
					attackDate2.Y = me.NowY - 1;
					if (me.bh.RoleMap[attackDate2.X, attackDate2.Y] == -1)
					{
						num3 = 0;
					}
				}
				if (atkDirect == 3)
				{
					attackDate2.X = me.NowX - 1;
					attackDate2.Y = me.NowY + j;
					if (me.bh.RoleMap[attackDate2.X, attackDate2.Y] == -1)
					{
						num3 = 0;
					}
				}
				me.bh.al.push(attackDate2);
			}
			if (num3 == 1)
			{
				GameObject.Find("Flowchart").GetComponent<Flowchart>().ExecuteBlock("PekoAttackFail-" + me.bh.dc.Language);
			}
			else
			{
				GameObject.Find("Flowchart").GetComponent<Flowchart>().ExecuteBlock("PekoAttackSuccess-" + me.bh.dc.Language);
			}
			if (atkDirect == 0)
			{
				GameObject gameObject3 = Object.Instantiate(GameObject.Find("SpritePlayer/jinzhan-sx"));
				Vector3 localScale2 = gameObject3.GetComponent<Transform>().localScale;
				localScale2.y *= -1f;
				gameObject3.GetComponent<Transform>().localScale = localScale2;
				gameObject3.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX, me.bh.BlockH * (float)me.NowY + 0.2f, 60f + me.bh.BlockH * (float)(me.NowY + 1) * 0.01f - 0.0001f);
				gameObject3.GetComponent<SpritePlayer>().ss = sss[0];
				gameObject3.GetComponent<SpritePlayer>().bh = me.bh;
				gameObject3.GetComponent<SpritePlayer>().NowStart();
			}
			if (atkDirect == 1)
			{
				GameObject gameObject4 = Object.Instantiate(GameObject.Find("SpritePlayer/jinzhan-zy"));
				Vector3 localScale3 = gameObject4.GetComponent<Transform>().localScale;
				localScale3.x *= -1f;
				gameObject4.GetComponent<Transform>().localScale = localScale3;
				gameObject4.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX + 0.7f, me.bh.BlockH * (float)me.NowY, 60f + me.bh.BlockH * (float)(me.NowY - 1) * 0.01f - 0.0001f);
				gameObject4.GetComponent<SpritePlayer>().ss = sss[1];
				gameObject4.GetComponent<SpritePlayer>().bh = me.bh;
				gameObject4.GetComponent<SpritePlayer>().NowStart();
			}
			if (atkDirect == 2)
			{
				GameObject gameObject5 = Object.Instantiate(GameObject.Find("SpritePlayer/jinzhan-sx"));
				gameObject5.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX, me.bh.BlockH * (float)me.NowY - 0.2f, 60f + me.bh.BlockH * (float)(me.NowY - 1) * 0.01f - 0.0001f);
				gameObject5.GetComponent<SpritePlayer>().ss = sss[0];
				gameObject5.GetComponent<SpritePlayer>().bh = me.bh;
				gameObject5.GetComponent<SpritePlayer>().NowStart();
			}
			if (atkDirect == 3)
			{
				GameObject gameObject6 = Object.Instantiate(GameObject.Find("SpritePlayer/jinzhan-zy"));
				gameObject6.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX - 0.7f, me.bh.BlockH * (float)me.NowY, 60f + me.bh.BlockH * (float)(me.NowY - 1) * 0.01f - 0.0001f);
				gameObject6.GetComponent<SpritePlayer>().ss = sss[1];
				gameObject6.GetComponent<SpritePlayer>().bh = me.bh;
				gameObject6.GetComponent<SpritePlayer>().NowStart();
			}
			SoundCenter.PlayMusic(2, "Audios/Sounds/战斗/剑 被攻击 SND34535");
			SoundCenter.SetMusicVolume(2, me.bh.dc.maxVolume * me.bh.dc.maxSoundVolume);
			return;
		}
		if (thj == me.bh.turn - 1)
		{
			GameObject gameObject7 = Object.Instantiate(GameObject.Find("SpritePlayer/daodan"));
			Vector3 localScale4 = gameObject7.GetComponent<Transform>().localScale;
			gameObject7.GetComponent<Transform>().localScale = localScale4;
			gameObject7.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX, me.bh.BlockH * (float)me.NowY, 30f);
			gameObject7.GetComponent<SpritePlayer>().ss = sss[3];
			gameObject7.GetComponent<SpritePlayer>().bh = me.bh;
			gameObject7.GetComponent<SpritePlayer>().NowStart();
			return;
		}
		if (((uint)me.MoveSituation & 2u) != 0)
		{
			int[] array = new int[200];
			int[,] array2 = new int[4, 3];
			array[0] = 0;
			for (int k = 1; k < me.bh.EnemyNum; k++)
			{
				if (me.bh.enemy[k].Type != 101 || me.bh.enemy[k].Alive != 1)
				{
					continue;
				}
				int num4 = 0;
				for (int l = 0; l < 4; l++)
				{
					array2[num4, 0] = me.bh.enemy[k].NowX + (int)me.bh.MovePos[l].x;
					array2[num4, 1] = me.bh.enemy[k].NowY + (int)me.bh.MovePos[l].y;
					array2[num4, 2] = l;
					if (me.bh.hb.CheckWall(array2[num4, 0], array2[num4, 1]) == 1 && me.bh.RoleMap[array2[num4, 0], array2[num4, 1]] == -2)
					{
						num4++;
						break;
					}
				}
				if (num4 != 0)
				{
					array[++array[0]] = k;
				}
			}
			if (array[0] != 0)
			{
				int num5 = array[Random.Range(0, array[0]) + 1];
				int num6 = 0;
				for (int m = 0; m < 4; m++)
				{
					array2[num6, 0] = me.bh.enemy[num5].NowX + (int)me.bh.MovePos[m].x;
					array2[num6, 1] = me.bh.enemy[num5].NowY + (int)me.bh.MovePos[m].y;
					array2[num6, 2] = m;
					if (me.bh.hb.CheckWall(array2[num6, 0], array2[num6, 1]) == 1 && me.bh.RoleMap[array2[num6, 0], array2[num6, 1]] == -2)
					{
						num6++;
					}
				}
				if (num6 != 0)
				{
					GameObject gameObject8 = Object.Instantiate(GameObject.Find("SpritePlayer/baozha"));
					Vector3 localScale5 = gameObject8.GetComponent<Transform>().localScale;
					gameObject8.GetComponent<Transform>().localScale = localScale5;
					gameObject8.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX, me.bh.BlockH * (float)me.NowY + 0.31f, 60f + me.bh.BlockH * (float)me.NowY * 0.01f + 0.0001f);
					gameObject8.GetComponent<SpritePlayer>().ss = sss[2];
					gameObject8.GetComponent<SpritePlayer>().NowStart();
					int num7 = Random.Range(0, num6);
					me.bh.RoleMap[me.NowX, me.NowY] = -2;
					me.NowX = me.bh.enemy[num5].NowX;
					me.NowY = me.bh.enemy[num5].NowY;
					me.NextX = array2[num7, 0];
					me.NextY = array2[num7, 1];
					me.MoveDirect = array2[num7, 2];
					me.TryMoveSign = 1;
					me.MoveSuccess();
					me.bh.RoleMap[array2[num7, 0], array2[num7, 1]] = me.Number;
					me.NowHp += me.bh.player.ATK - me.DEF;
					me.MoveSituation ^= 2;
					ChangePos();
					SoundCenter.PlayMusic(3, "Audios/Sounds/战斗/炮 SND6326");
					SoundCenter.SetMusicVolume(3, me.bh.dc.maxVolume * me.bh.dc.maxSoundVolume);
					if (szd == 0)
					{
						me.bh.ForcePause();
						GameObject.Find("Flowchart").GetComponent<Flowchart>().ExecuteBlock("PekoDrillingCave-" + me.bh.dc.Language);
					}
					szd = 1;
					return;
				}
			}
		}
		if (me.bh.turn - tjz > 12 + 5 * (1 - me.bh.dc.Difficulty) && thj != me.bh.turn - 2 && abs(me.NowX - me.bh.player.NowX) <= 2 && abs(me.NowY - me.bh.player.NowY) <= 2 && abs(me.NowY - me.bh.player.NowY) + abs(me.NowX - me.bh.player.NowX) != 4)
		{
			tjz = me.bh.turn;
			GameObject.Find("Flowchart").GetComponent<Flowchart>().ExecuteBlock("PekoAttack-" + me.bh.dc.Language);
			if (me.NowX - me.bh.player.NowX == 0)
			{
				int num8 = (me.NowY - me.bh.player.NowY) / abs(me.NowY - me.bh.player.NowY);
				atkDirect = 1 + num8;
				return;
			}
			if (me.NowY - me.bh.player.NowY == 0)
			{
				int num8 = (me.NowX - me.bh.player.NowX) / abs(me.NowX - me.bh.player.NowX);
				atkDirect = 2 + num8;
				return;
			}
			int[] array3 = new int[3];
			if (abs(me.NowX - me.bh.player.NowX) == 2)
			{
				array3[++array3[0]] = 1;
			}
			if (abs(me.NowX - me.bh.player.NowX) == 2)
			{
				array3[++array3[0]] = 2;
			}
			if (array3[0] != 0)
			{
				if (array3[Random.Range(0, array3[0]) + 1] == 2)
				{
					int num8 = (me.NowY - me.bh.player.NowY) / abs(me.NowY - me.bh.player.NowY);
					atkDirect = 1 + num8;
				}
				else
				{
					int num8 = (me.NowX - me.bh.player.NowX) / abs(me.NowX - me.bh.player.NowX);
					atkDirect = 2 + num8;
				}
			}
			else if (Random.Range(0, 2) + 1 == 2)
			{
				int num8 = (me.NowY - me.bh.player.NowY) / abs(me.NowY - me.bh.player.NowY);
				atkDirect = 1 + num8;
			}
			else
			{
				int num8 = (me.NowX - me.bh.player.NowX) / abs(me.NowX - me.bh.player.NowX);
				atkDirect = 2 + num8;
			}
			return;
		}
		if (me.bh.turn - thj > 15 && tjz != me.bh.turn - 2)
		{
			thj = me.bh.turn;
			daodannum = 3;
			daodan[0, 0] = me.bh.player.NowX;
			daodan[0, 1] = me.bh.player.NowY;
			for (int n = 1; n < daodannum; n++)
			{
				while (true)
				{
					daodan[n, 0] = Random.Range(me.bh.player.NowX - 2, me.bh.player.NowX + 3);
					daodan[n, 1] = Random.Range(me.bh.player.NowY - 2, me.bh.player.NowY + 3);
					if (me.bh.hb.CheckWall(daodan[n, 0], daodan[n, 1]) != 1)
					{
						continue;
					}
					int num9 = 1;
					for (int num10 = 0; num10 < n; num10++)
					{
						if (daodan[n, 0] == daodan[num10, 0] && daodan[n, 0] == daodan[num10, 0])
						{
							num9 = 0;
							break;
						}
					}
					if (num9 == 1)
					{
						break;
					}
				}
			}
			for (int num11 = 0; num11 < daodannum; num11++)
			{
				GameObject gameObject9 = Object.Instantiate(GameObject.Find("SpritePlayer/jingao"));
				Vector3 localScale6 = gameObject9.GetComponent<Transform>().localScale;
				gameObject9.GetComponent<Transform>().localScale = localScale6;
				gameObject9.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)daodan[num11, 0], me.bh.BlockH * (float)daodan[num11, 1], 30f);
				gameObject9.GetComponent<SpritePlayer>().ss = sss[4];
				gameObject9.GetComponent<SpritePlayer>().LoopTime = 2;
				gameObject9.GetComponent<SpritePlayer>().bh = me.bh;
				gameObject9.GetComponent<SpritePlayer>().NowStart();
			}
			return;
		}
		if (me.bh.turn - tzh > 48 * (2 - me.bh.dc.Difficulty))
		{
			int num12 = 0;
			for (int num13 = 1; num13 < me.bh.EnemyNum; num13++)
			{
				if (me.bh.enemy[num13].Type == 101 && me.bh.enemy[num13].Alive == 1)
				{
					num12++;
				}
			}
			if (num12 < 6)
			{
				int num14;
				int num15;
				do
				{
					num14 = Random.Range(2, 19);
					num15 = Random.Range(3, 14);
				}
				while ((num14 == 5 && num15 == 11) || (num14 == 14 && num15 == 11));
				if (me.bh.hb.CheckWall(num14, num15) == 1 && me.bh.RoleMap[num14, num15] == -2)
				{
					GameObject gameObject10 = Object.Instantiate(GameObject.Find("Pekora_Cave"));
					Enemy component2 = gameObject10.GetComponent<Enemy>();
					component2.NowX = num14;
					component2.NowY = num15;
					component2.MaxHp = 100f;
					component2.NowHp = 100f;
					component2.MoveDirect = 0;
					component2.ChangeBehaviour(new Pekora_Cave());
					me.bh.AddEnemy(gameObject10);
					gameObject10.name = "enemy1_" + component2.Number;
					component2.eb.StartSign = 1;
					GameObject.Find("Flowchart").GetComponent<Flowchart>().ExecuteBlock("PekoCallSRabbit-" + me.bh.dc.Language);
					tzh = me.bh.turn;
					return;
				}
			}
		}
		if (Random.Range(0, 5) != 0 && ((me.MoveSituation & 2) == 0 || me.bh.dc.Difficulty == 0))
		{
			return;
		}
		int num16 = 0;
		int[,] array4 = new int[4, 3];
		for (int num17 = 0; num17 < 4; num17++)
		{
			array4[num16, 0] = me.NowX + (int)me.bh.MovePos[num17].x;
			array4[num16, 1] = me.NowY + (int)me.bh.MovePos[num17].y;
			array4[num16, 2] = num17;
			if (me.bh.hb.CheckWall(array4[num16, 0], array4[num16, 1]) == 1 && me.bh.RoleMap[array4[num16, 0], array4[num16, 1]] == -2)
			{
				num16++;
			}
		}
		if (num16 != 0)
		{
			int num18 = Random.Range(0, num16);
			me.bh.RoleMap[me.NowX, me.NowY] = -2;
			me.bh.RoleMap[array4[num18, 0], array4[num18, 1]] = me.Number;
			me.NextX = array4[num18, 0];
			me.NextY = array4[num18, 1];
			me.MoveDirect = array4[num18, 2];
			me.TryMoveSign = 1;
			me.MoveSuccess();
		}
	}
}
