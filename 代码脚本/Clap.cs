using UnityEngine;

public class Clap : MonoBehaviour
{
	public int dirct = -1;

	public int num;

	public int ClapMaxNum = 20;

	public int show = 1;

	public Transform IfmCmrT;

	public Transform myTransform;

	public BattleHub Bhub;

	public float dis = 5f;

	private void Start()
	{
		IfmCmrT = GameObject.Find("Information Camera").GetComponent<Transform>();
		Bhub = GameObject.Find("Hub").GetComponent<BattleHub>();
		myTransform = base.gameObject.GetComponent<Transform>();
	}

	private void Update()
	{
		if (show == 0)
		{
			myTransform.localScale = new Vector3(0f, 0f, 1f);
		}
		else if (dirct == 0 || dirct == 1)
		{
			myTransform.localScale = new Vector3(0.1875f, 0.1875f, 1f);
			float num = Bhub.NowTime + 6000000f / Bhub.BPM;
			Vector3 position = new Vector3(0f, IfmCmrT.position.y - 4.29f, 80f);
			position.x = (int)(num * Bhub.BPM / 60f);
			position.x = num - position.x * 60f / Bhub.BPM + (float)(((int)position.x + this.num) % ClapMaxNum) * 60f / Bhub.BPM;
			if (dirct == 0)
			{
				position.x = IfmCmrT.position.x + ((float)ClapMaxNum * 60f / Bhub.BPM - position.x) * dis;
			}
			else
			{
				position.x = IfmCmrT.position.x - ((float)ClapMaxNum * 60f / Bhub.BPM - position.x) * dis;
			}
			myTransform.position = position;
		}
	}
}
