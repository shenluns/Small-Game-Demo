using UnityEngine;

public class SpritePlayer : MonoBehaviour
{
	public SpriteSwitcher ss;

	public SpriteRenderer spr;

	public int StartSign;

	public int LoopTime = 1;

	public int Weaken;

	public float StartTime;

	public float NowTime;

	public float TotleTime;

	public BattleHub bh;

	private void Start()
	{
		spr = base.gameObject.GetComponent<SpriteRenderer>();
	}

	public void NowStart()
	{
		if (bh != null)
		{
			StartTime = bh.NowTime;
		}
		else
		{
			StartTime = Time.time;
		}
		StartSign = 1;
		if (ss != null)
		{
			TotleTime = ss.TotleTime;
		}
	}

	private void Update()
	{
		if (StartSign == 0)
		{
			return;
		}
		if (bh != null)
		{
			NowTime = bh.NowTime;
		}
		else
		{
			NowTime = Time.time;
		}
		if (!(NowTime < StartTime))
		{
			if (LoopTime != -1 && StartTime + (float)LoopTime * TotleTime <= NowTime)
			{
				Object.Destroy(base.gameObject, 0f);
			}
			if (spr != null && ss != null)
			{
				spr.sprite = ss.GetSprite(NowTime - StartTime);
			}
			if (LoopTime != -1 && Weaken == 1)
			{
				spr.color = new Color(1f, 1f, 1f, 1f - (NowTime - StartTime) / ((float)LoopTime * TotleTime));
			}
		}
	}
}
