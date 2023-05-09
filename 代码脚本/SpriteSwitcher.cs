using UnityEngine;

public class SpriteSwitcher
{
	public int Type;

	public int SpriteNum;

	public int NowNum;

	public float TotleTime;

	public float[] Times;

	public Sprite[] spr;

	public SpriteSwitcher(int tp, int sn)
	{
		Type = tp;
		SpriteNum = sn;
		spr = new Sprite[sn];
		NowNum = 0;
	}

	public void AddSprite(Sprite _spr)
	{
		if (NowNum < SpriteNum)
		{
			spr[NowNum++] = _spr;
		}
	}

	public void AddSprite(Sprite _spr, float time)
	{
		if (NowNum < SpriteNum)
		{
			Times[NowNum] = time;
			spr[NowNum++] = _spr;
			if (Type == 2 && NowNum == SpriteNum)
			{
				SetTotleTime();
			}
		}
	}

	public void SetTotleTime(float totle)
	{
		TotleTime = totle;
		if (Type == 2)
		{
			float num = 0f;
			for (int i = 0; i < SpriteNum; i++)
			{
				num += Times[i];
			}
			for (int j = 0; j < SpriteNum; j++)
			{
				Times[j] *= totle / num;
			}
		}
	}

	public void SetTotleTime()
	{
		if (Type == 2)
		{
			TotleTime = 0f;
			for (int i = 0; i < SpriteNum; i++)
			{
				TotleTime += Times[i];
			}
		}
	}

	public Sprite GetSprite(float time)
	{
		int num = (int)(time / TotleTime);
		time -= (float)num * TotleTime;
		if (time < 0f)
		{
			time += TotleTime;
		}
		if (Type == 1)
		{
			return spr[(int)(time * (float)SpriteNum / TotleTime)];
		}
		if (Type == 2)
		{
			float num2 = 0f;
			for (int i = 0; i < SpriteNum; i++)
			{
				if (num2 <= time && num2 + Times[i] > time)
				{
					return spr[i];
				}
				num2 += Times[i];
			}
			return spr[SpriteNum - 1];
		}
		return null;
	}
}
