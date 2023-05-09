using UnityEngine;

public class Player : MonoBehaviour
{
	public BattleHub bh;

	public PlayerBehaviour pb;

	public SpriteRenderer spr;

	public int PressedKey = -1;

	public int start;

	public int MoveSign;

	public int TryMoveSign;

	public int MoveDirect;

	public int FaceDirect;

	public int PauseDirect;

	public float NowTime;

	public float LastTime;

	public float H;

	public int NowX;

	public int NowY;

	public int NextX;

	public int NextY;

	public int LastX;

	public int LastY;

	public int MoveSituation;

	public float ATK;

	public float NowHp;

	public float MaxHp;

	public static int[] KeyList = new int[600];

	public void ChangeBehaviour(PlayerBehaviour cpb)
	{
		pb = cpb;
		pb.me = this;
	}

	public void PressKeyInBattle(int x)
	{
		PressedKey = KeyList[x];
		pb.PressKeyInBattle();
	}

	public void MoveFail()
	{
		LastX = (NextX = NowX);
		LastY = (NextY = NowY);
		MoveSign = 0;
		LastTime = NowTime;
		bh.RoleMap[NowX, NowY] = -1;
		pb.MoveFail();
	}

	public void MoveSuccess()
	{
		MoveSign = 1;
		LastTime = NowTime;
		bh.RoleMap[NextX, NextY] = -1;
		pb.RealMove();
		pb.MoveSuccess();
	}

	public void BeAttact(float Hp)
	{
		if (pb != null)
		{
			pb.BeAttack(Hp);
		}
	}

	public void BeAttact(Enemy enemy)
	{
		if (pb != null)
		{
			pb.BeAttack(enemy);
		}
	}

	private void Start()
	{
		spr = base.gameObject.GetComponent<SpriteRenderer>();
		if (start == 0 && pb != null && pb.StartSign != 0)
		{
			pb.me = this;
			start = 1;
			pb.OnStart();
		}
	}

	private void Update()
	{
		if (pb != null && pb.StartSign != 0)
		{
			if (start == 0)
			{
				pb.me = this;
				start = 1;
				pb.OnStart();
			}
			NowTime = bh.NowTime;
			pb.OnUpdate();
		}
	}

	private void Awake()
	{
		for (int i = 0; i <= 550; i++)
		{
			KeyList[i] = -1;
		}
		KeyList[273] = 0;
		KeyList[275] = 1;
		KeyList[274] = 2;
		KeyList[276] = 3;
		KeyList[113] = 4;
		KeyList[101] = 5;
		KeyList[119] = 10;
		KeyList[100] = 11;
		KeyList[115] = 12;
		KeyList[97] = 13;
		KeyList[48] = 20;
		KeyList[49] = 21;
		KeyList[50] = 22;
		KeyList[51] = 23;
		KeyList[52] = 24;
		KeyList[53] = 25;
		KeyList[54] = 26;
		KeyList[55] = 27;
		KeyList[56] = 28;
		KeyList[57] = 29;
	}
}
