using UnityEngine;

public class Tail : MonoBehaviour
{
	private SpriteSwitcher[] ss = new SpriteSwitcher[2];

	private Sprite zm;

	public SpriteRenderer spr;

	public GameObject IfmCmr;

	public BattleHub bh;

	public int show = 1;

	private void Start()
	{
		spr = base.gameObject.GetComponent<SpriteRenderer>();
		IfmCmr = GameObject.Find("Information Camera");
		bh = GameObject.Find("Hub").GetComponent<BattleHub>();
		ss[0] = new SpriteSwitcher(1, 4);
		for (int i = 0; i < 4; i++)
		{
			ss[0].AddSprite(SpriteCenter.GetSprite("Pictures/UI/weiba/daiji/weiba-daiji_000" + i));
		}
		ss[1] = new SpriteSwitcher(1, 11);
		for (int num = 11; num >= 0; num--)
		{
			ss[1].AddSprite(SpriteCenter.GetSprite("Pictures/UI/weiba/yaodong/weiba-daiji_00" + ((num < 10) ? "0" : "") + num));
		}
		zm = SpriteCenter.GetSprite("Pictures/UI/weiba/zhamao/weiba zha");
	}

	private void Update()
	{
		if (show == 0)
		{
			base.gameObject.GetComponent<Transform>().localScale = new Vector3(0f, 0f, 1f);
			return;
		}
		if (bh.hb == null)
		{
			base.gameObject.GetComponent<Transform>().localScale = new Vector3(0f, 0f, 1f);
			return;
		}
		base.gameObject.GetComponent<Transform>().localScale = new Vector3(1.5f, 1.5f, 1f);
		int num = 0;
		float num2 = 60f / bh.BPM;
		if (bh.PressSuccessTime <= bh.PressFailTime)
		{
			if (bh.NowTime - bh.PressFailTime < 0.2f)
			{
				spr.sprite = zm;
				return;
			}
		}
		else if (bh.NowTime - bh.PressSuccessTime < num2)
		{
			num = 1;
		}
		ss[num].SetTotleTime(num2);
		spr.sprite = ss[num].GetSprite(bh.NowTime);
	}
}
