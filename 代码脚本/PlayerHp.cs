using UnityEngine;

public class PlayerHp : MonoBehaviour
{
	public int num = -1;

	public int ClapMaxNum = 20;

	public int show = 1;

	public GameObject IfmCmr;

	public BattleHub bh;

	public float dis = 5f;

	public static Sprite[] sp = new Sprite[4];

	private void Start()
	{
		IfmCmr = GameObject.Find("Information Camera");
		bh = GameObject.Find("Hub").GetComponent<BattleHub>();
		if (sp[0] == null)
		{
			for (int i = 0; i < 4; i++)
			{
				sp[i] = SpriteCenter.GetSprite("Pictures/血量状态-0" + i);
			}
		}
	}

	private void Update()
	{
		if (num < 0)
		{
			return;
		}
		if (show == 0 || bh.player == null)
		{
			base.gameObject.GetComponent<Transform>().localScale = new Vector3(0f, 0f, 1f);
			return;
		}
		if (bh.player.NowHp <= (float)(num * 3))
		{
			base.gameObject.GetComponent<SpriteRenderer>().sprite = sp[3];
		}
		else if (bh.player.NowHp >= (float)(num * 3 + 3))
		{
			base.gameObject.GetComponent<SpriteRenderer>().sprite = sp[0];
		}
		else
		{
			base.gameObject.GetComponent<SpriteRenderer>().sprite = sp[3 - ((int)bh.player.NowHp - num * 3)];
		}
		base.gameObject.GetComponent<Transform>().localScale = new Vector3(0.1875f, 0.1875f, 1f);
		Vector3 position = new Vector3(IfmCmr.GetComponent<Transform>().position.x, IfmCmr.GetComponent<Transform>().position.y + 4.6f, 80f);
		position.x = position.x + 9.6f - 0.75f * (float)num - 1f;
		base.gameObject.GetComponent<Transform>().position = position;
	}
}
