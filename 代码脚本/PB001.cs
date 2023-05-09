using UnityEngine;

public class PB001 : PlayerBehaviour
{
	private SpriteSwitcher[] ss = new SpriteSwitcher[10];

	private string[] FileName = new string[5] { "背面", "向右", "正面", "向左", "站立" };

	public int StandAction;

	public override void OnStart()
	{
		me.MoveSign = 0;
		me.TryMoveSign = 0;
		me.NowTime = (me.LastTime = 0f);
		me.PauseDirect = 4;
		for (int i = 0; i < 4; i++)
		{
			ss[i] = new SpriteSwitcher(1, 6);
			for (int j = 1; j <= 6; j++)
			{
				ss[i].AddSprite(SpriteCenter.GetSprite("Pictures/Fubuki/" + FileName[i] + "走" + j));
			}
		}
		ss[4] = new SpriteSwitcher(1, 4);
		for (int k = 1; k <= 4; k++)
		{
			ss[4].AddSprite(SpriteCenter.GetSprite("Pictures/Fubuki/idle2-" + k));
		}
		ss[5] = new SpriteSwitcher(1, 5);
		for (int l = 5; l <= 9; l++)
		{
			ss[5].AddSprite(SpriteCenter.GetSprite("Pictures/Fubuki/idle2-" + l));
		}
		ss[6] = new SpriteSwitcher(1, 4);
		for (int m = 10; m <= 13; m++)
		{
			ss[6].AddSprite(SpriteCenter.GetSprite("Pictures/Fubuki/idle2-" + m));
		}
		float totleTime = 60f / me.bh.BPM;
		ss[7] = new SpriteSwitcher(1, 5);
		for (int n = 1; n <= 5; n++)
		{
			ss[7].AddSprite(SpriteCenter.GetSprite("Pictures/Fubuki/jianqi/attack 下" + n));
		}
		ss[7].SetTotleTime(totleTime);
		ss[8] = new SpriteSwitcher(1, 5);
		for (int num = 1; num <= 5; num++)
		{
			ss[8].AddSprite(SpriteCenter.GetSprite("Pictures/Fubuki/jianqi/attack 左" + num));
		}
		ss[8].SetTotleTime(totleTime);
	}

	public override void MoveSuccess()
	{
		if (((uint)me.MoveSituation & 2u) != 0)
		{
			SoundCenter.PlayMusic(4, "Audios/Sounds/战斗/徒手 被攻击 SND0301");
			SoundCenter.SetMusicVolume(4, me.bh.dc.maxVolume * me.bh.dc.maxSoundVolume);
		}
	}

	public override void MoveFail()
	{
		if (((uint)me.MoveSituation & 2u) != 0)
		{
			SoundCenter.PlayMusic(4, "Audios/Sounds/战斗/徒手 被攻击 SND0301");
			SoundCenter.SetMusicVolume(4, me.bh.dc.maxVolume * me.bh.dc.maxSoundVolume);
		}
		if (((uint)me.MoveSituation & (true ? 1u : 0u)) != 0)
		{
			SoundCenter.PlayMusic(5, "Audios/Sounds/战斗/徒手 攻击 SND14128");
			SoundCenter.SetMusicVolume(5, me.bh.dc.maxVolume * me.bh.dc.maxSoundVolume);
			if (me.MoveDirect == 0)
			{
				GameObject gameObject = Object.Instantiate(GameObject.Find("SpritePlayer/jinzhan-sx2"));
				Vector3 localScale = gameObject.GetComponent<Transform>().localScale;
				localScale.y *= -1f;
				gameObject.GetComponent<Transform>().localScale = localScale;
				gameObject.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX, me.bh.BlockH * (float)me.NowY + 0.7f, 60f + me.bh.BlockH * (float)(me.NowY + 1) * 0.01f - 0.0001f);
				gameObject.GetComponent<SpritePlayer>().ss = ss[7];
				gameObject.GetComponent<SpritePlayer>().bh = me.bh;
				gameObject.GetComponent<SpritePlayer>().NowStart();
			}
			if (me.MoveDirect == 1)
			{
				GameObject gameObject2 = Object.Instantiate(GameObject.Find("SpritePlayer/jinzhan-zy2"));
				Vector3 localScale2 = gameObject2.GetComponent<Transform>().localScale;
				localScale2.x *= -1f;
				gameObject2.GetComponent<Transform>().localScale = localScale2;
				gameObject2.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX + 0.7f, me.bh.BlockH * (float)me.NowY, 60f + me.bh.BlockH * (float)me.NowY * 0.01f - 0.0001f);
				gameObject2.GetComponent<SpritePlayer>().ss = ss[8];
				gameObject2.GetComponent<SpritePlayer>().bh = me.bh;
				gameObject2.GetComponent<SpritePlayer>().NowStart();
			}
			if (me.MoveDirect == 2)
			{
				GameObject gameObject3 = Object.Instantiate(GameObject.Find("SpritePlayer/jinzhan-sx2"));
				gameObject3.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX, me.bh.BlockH * (float)me.NowY - 0.7f, 60f + me.bh.BlockH * (float)(me.NowY - 1) * 0.01f - 0.0001f);
				gameObject3.GetComponent<SpritePlayer>().ss = ss[7];
				gameObject3.GetComponent<SpritePlayer>().bh = me.bh;
				gameObject3.GetComponent<SpritePlayer>().NowStart();
			}
			if (me.MoveDirect == 3)
			{
				GameObject gameObject4 = Object.Instantiate(GameObject.Find("SpritePlayer/jinzhan-zy2"));
				gameObject4.GetComponent<Transform>().position = new Vector3(me.bh.BlockW * (float)me.NowX - 0.7f, me.bh.BlockH * (float)me.NowY, 60f + me.bh.BlockH * (float)me.NowY * 0.01f - 0.0001f);
				gameObject4.GetComponent<SpritePlayer>().ss = ss[8];
				gameObject4.GetComponent<SpritePlayer>().bh = me.bh;
				gameObject4.GetComponent<SpritePlayer>().NowStart();
			}
			me.MoveSign = 1;
		}
	}

	public override void ChangeSpriteOnStand()
	{
		float totleTime = 60f / me.bh.BPM;
		if (me.bh.turn % 3 == 0)
		{
			ss[4].SetTotleTime(totleTime);
			me.spr.sprite = ss[4].GetSprite(me.NowTime - me.LastTime);
		}
		else if (me.bh.turn % 3 == 1)
		{
			ss[5].SetTotleTime(totleTime);
			me.spr.sprite = ss[5].GetSprite(me.NowTime - me.LastTime);
		}
		else
		{
			ss[6].SetTotleTime(totleTime);
			me.spr.sprite = ss[6].GetSprite(me.NowTime - me.LastTime);
		}
	}

	public override void ChangeSpriteOnPause()
	{
		float num = 60f / me.bh.BPM;
		float num2 = Time.time - me.bh.PauseTime;
		int num3 = (int)(num2 / num);
		if (me.PauseDirect == 4)
		{
			if (num3 % 3 == 0)
			{
				ss[4].SetTotleTime(num);
				me.spr.sprite = ss[4].GetSprite(num2);
			}
			else if (num3 % 3 == 1)
			{
				ss[5].SetTotleTime(num);
				me.spr.sprite = ss[5].GetSprite(num2);
			}
			else
			{
				ss[6].SetTotleTime(num);
				me.spr.sprite = ss[6].GetSprite(num2);
			}
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
				position.y = position.y - 0.5f + 0.5f * me.H - 0.05f;
				me.gameObject.GetComponent<Transform>().position = position;
			}
		}
		if (me.MoveSign == 0)
		{
			Vector3 position2 = new Vector3(me.bh.BlockW * (float)me.NowX, me.bh.BlockH * (float)me.NowY, 0f);
			position2.z = 60f + position2.y * 0.01f;
			position2.y = position2.y - 0.5f + 0.5f * me.H - 0.05f;
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
}
