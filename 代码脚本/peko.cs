using UnityEngine;

public class peko : MonoBehaviour
{
	public GameObject hub;

	private SpriteRenderer spr;

	public int MoveSign;

	public int MoveDirect;

	public int FaceDirect;

	public float NowTime;

	public float LastTime;

	public float NowX;

	public float NowY;

	public float H;

	public float ATK;

	public int StandAction;

	private Vector3[] MovePos = new Vector3[5]
	{
		new Vector3(0f, 1f, 0f),
		new Vector3(1f, 0f, 0f),
		new Vector3(0f, -1f, 0f),
		new Vector3(-1f, 0f, 0f),
		new Vector3(0f, 0f, 0f)
	};

	public Sprite[][] sp = new Sprite[5][];

	private string[] FileName = new string[5] { "背面", "向右", "正面", "向左", "站立" };

	private void Start()
	{
		H = 1.2f;
		MoveSign = 0;
		NowTime = (LastTime = 0f);
		NowX = (NowY = 15f);
		ATK = 10f;
		spr = base.gameObject.GetComponent<SpriteRenderer>();
		for (int i = 0; i <= 4; i++)
		{
			int num = ((i == 4) ? 11 : 8);
			sp[i] = new Sprite[num];
			for (int j = 1; j <= num; j++)
			{
				sp[i][j - 1] = SpriteCenter.GetSprite("Pictures/Pekora/" + FileName[i] + "-export" + j);
			}
		}
		hub = GameObject.Find("Hub");
	}

	private void Update()
	{
		NowTime = Time.time;
		float num = 60f / hub.GetComponent<BattleHub>().BPM;
		float num2 = num - 2f * hub.GetComponent<BattleHub>().WrTime;
		if (MoveSign != 0)
		{
			if (NowTime - LastTime >= num2)
			{
				MoveSign = 0;
				LastTime = NowTime;
				NowX += MovePos[MoveDirect].x;
				NowY += MovePos[MoveDirect].y;
			}
			else
			{
				int num3 = (int)((NowTime - LastTime) / num2 * 8f);
				spr.sprite = sp[FaceDirect][num3];
				Vector3 position = new Vector3(NowX, NowY, 0f) + (NowTime - LastTime) / num2 * MovePos[MoveDirect];
				position.y = position.y - 0.5f + 0.5f * H;
				position.z = 80f - position.y * 0.01f;
				base.gameObject.GetComponent<Transform>().position = position;
			}
		}
		if (MoveSign == 0)
		{
			int num4 = (int)((NowTime - LastTime) * 28f / num) % 28;
			if (num4 == 0)
			{
				StandAction = ((Random.Range(0, 10) == 0) ? 1 : 0);
			}
			spr.sprite = ((StandAction == 0) ? sp[4][num4 / 4] : sp[4][7 + num4 / 7]);
			Vector3 position2 = new Vector3(NowX, NowY, 0f);
			position2.y = position2.y - 0.5f + 0.5f * H;
			position2.z = 80f - position2.y * 0.01f;
			base.gameObject.GetComponent<Transform>().position = position2;
		}
	}

	public void TryMove(int drct)
	{
		if (MoveSign != 0)
		{
			return;
		}
		int num = (int)(NowX + MovePos[drct].x);
		int num2 = (int)(NowY + MovePos[drct].y);
		if (hub.GetComponent<BattleHub>().GameMap[num, num2] == 0)
		{
			return;
		}
		FaceDirect = (MoveDirect = drct);
		for (int i = 0; i < hub.GetComponent<BattleHub>().EnemyNum; i++)
		{
			Enemy enemy = hub.GetComponent<BattleHub>().enemy[i];
			if (enemy.Alive != 0 && num == enemy.NowX && num2 == enemy.NowY)
			{
				MoveDirect = 4;
				enemy.GetComponent<Enemy>().BeAttact(ATK);
			}
		}
		MoveSign = 1;
		LastTime = Time.time;
		hub.GetComponent<BattleHub>().LastTime = LastTime;
	}
}
