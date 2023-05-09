using UnityEngine;

public class BattleHub : MonoBehaviour
{
	public Vector3[] MovePos = new Vector3[5]
	{
		new Vector3(0f, 1f, 0f),
		new Vector3(1f, 0f, 0f),
		new Vector3(0f, -1f, 0f),
		new Vector3(-1f, 0f, 0f),
		new Vector3(0f, 0f, 0f)
	};

	public HubBehaviour hb;

	public GameObject playerO;

	public Player player;

	public Enemy[] enemy = new Enemy[10000];

	public int EnemyNum;

	public int SpritePlayerNum;

	public int maxEnemyNum;

	public float BPM = 1f;

	public float StartTime;

	public float WrTime;

	public float LastTime;

	public float NowTime;

	public float ChangeMusicTime;

	public float PressFailTime = -100f;

	public float PressSuccessTime = -100f;

	public int[,] GameMap = new int[310, 310];

	public int[,] RoleMap = new int[310, 310];

	public int[,] MoneyMap = new int[310, 310];

	public int MapW;

	public int MapH;

	public int ChangeMusicSign;

	public int NowMusic;

	public int MusicNum;

	public int NextMusic;

	public float BlockW;

	public float BlockH;

	public DataCenter dc;

	public int turn;

	public AttackList al = new AttackList();

	public float PauseTime;

	public float PauseCostTime;

	public int PauseSign;

	public int ForcePauseSign;

	public int PauseButAct = 1;

	public int maxMoney;

	public int nowMoney;

	public int playerMoney;

	public int Combo;

	public ComboSystem cbs;

	private int start;

	public static int GetFirstNum(ref string str)
	{
		while (str.Length != 0)
		{
			char c = str[0];
			str = str.Substring(1);
			if ((c < '0' || c > '9') && c != '-')
			{
				continue;
			}
			int num = c - 48;
			int num2 = 1;
			if (c == '-')
			{
				num = 0;
				num2 = -1;
			}
			while (str.Length != 0)
			{
				c = str[0];
				str = str.Substring(1);
				if (c < '0' || c > '9')
				{
					break;
				}
				num *= 10;
				num += c - 48;
			}
			return num * num2;
		}
		return -1;
	}

	public void AddMoney(int x, int y)
	{
		if (nowMoney < maxMoney)
		{
			nowMoney++;
			MoneyMap[x, y] = nowMoney;
			GameObject obj = Object.Instantiate(GameObject.Find("SpritePlayer/Money"));
			obj.name = "money-" + nowMoney;
			obj.GetComponent<Transform>().position = new Vector3(BlockW * (float)x, BlockH * (float)y, 60f + BlockH * (float)y * 0.01f + 0.0001f);
		}
	}

	public void Pause()
	{
		if (PauseSign == 0 && ForcePauseSign == 0)
		{
			PauseSign = 1;
			hb.Pause();
			if (cbs != null)
			{
				cbs.ChangeBlock(0);
			}
			PauseTime = Time.time;
		}
	}

	public void PauseEnd()
	{
		if (PauseSign == 1 && ForcePauseSign == 0)
		{
			PauseSign = 0;
			hb.PauseEnd();
			PauseCostTime += Time.time - PauseTime;
			hb.PauseInHB = 0;
		}
	}

	public void ForcePause()
	{
		if (ForcePauseSign != 0)
		{
			return;
		}
		ForcePauseSign = 1;
		if (PauseSign != 1)
		{
			PauseSign = 1;
			hb.Pause();
			if (cbs != null)
			{
				cbs.ChangeBlock(0);
			}
			PauseTime = Time.time;
		}
	}

	public void ForcePauseEnd()
	{
		if (ForcePauseSign == 1)
		{
			PauseSign = 0;
			ForcePauseSign = 0;
			hb.PauseEnd();
			PauseCostTime += Time.time - PauseTime;
			hb.PauseInHB = 0;
		}
	}

	public void AddEnemy(GameObject eobj)
	{
		enemy[EnemyNum] = eobj.GetComponent<Enemy>();
		enemy[EnemyNum].bh = this;
		enemy[EnemyNum].burnTurn = turn;
		enemy[EnemyNum].Number = EnemyNum;
		EnemyNum++;
	}

	public void ChangePlayer(GameObject pobj)
	{
		player = pobj.GetComponent<Player>();
		player.bh = this;
	}

	public void TryMove()
	{
		for (int i = 0; i < EnemyNum; i++)
		{
			enemy[i].TryMove();
		}
	}

	public void ChangeBehaviour(HubBehaviour chb)
	{
		hb = chb;
		hb.me = this;
	}

	public void ChangeClap(int to)
	{
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 20; j++)
			{
				GameObject.Find("Clap" + i + " " + j).GetComponent<Clap>().show = to;
			}
		}
		GameObject.Find("Tail").GetComponent<Tail>().show = to;
	}

	public void ChangePlayerHp(int to)
	{
		for (int i = 0; i < 20; i++)
		{
			GameObject.Find("PlayerHp " + i).GetComponent<PlayerHp>().show = to;
		}
	}

	private void Start()
	{
		cbs = GameObject.Find("Canvas/Combo").GetComponent<ComboSystem>();
		dc = GameObject.Find("DataCenter").GetComponent<DataCenter>();
		if (start == 0 && hb != null && hb.StartSign != 0)
		{
			hb.me = this;
			start = 1;
			hb.OnStart();
		}
	}

	private void Update()
	{
		if (hb != null && hb.StartSign != 0)
		{
			if (start == 0)
			{
				hb.me = this;
				start = 1;
				hb.OnStart();
			}
			if (!(player == null))
			{
				hb.OnUpdate();
			}
		}
	}
}
