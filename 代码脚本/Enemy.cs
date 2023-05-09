using UnityEngine;

public class Enemy : MonoBehaviour
{
	public GameObject HpA;

	public GameObject HpB;

	public BattleHub bh;

	public EnemyBehavier eb;

	public SpriteRenderer spr;

	public float H;

	public int NowX;

	public int NowY;

	public int NextX;

	public int NextY;

	public int LastX;

	public int LastY;

	public float NowHp;

	public float MaxHp;

	public float DEF;

	public float ATK;

	public float NowTime;

	public float LastTime;

	public int Alive = 1;

	public int EnemyNum;

	public int ShowHp;

	public int MoveSign;

	public int TryMoveSign;

	public int MoveDirect;

	public int FaceDirect;

	public int PauseDirect;

	public int start;

	public int MoveSituation;

	public int burnTurn;

	public int Type;

	public int Number;

	public void ChangeBehaviour(EnemyBehavier ceb)
	{
		eb = ceb;
		eb.me = this;
	}

	public void TryMove()
	{
		MoveSign = 0;
		MoveSituation = 0;
		TryMoveSign = 0;
		eb.TryMove();
	}

	public void MoveFail()
	{
		MoveSign = 0;
		LastTime = NowTime;
		eb.MoveFail();
	}

	public void MoveSuccess()
	{
		MoveSign = 1;
		LastTime = NowTime;
		eb.RealMove();
		eb.MoveSuccess();
	}

	private void Start()
	{
		spr = base.gameObject.GetComponent<SpriteRenderer>();
		if (start == 0 && eb != null && eb.StartSign != 0)
		{
			eb.me = this;
			eb.OnStart();
			start = 1;
		}
	}

	public bool BeAttact(float Hp)
	{
		if (eb != null && Alive == 1)
		{
			return eb.BeAttack(Hp);
		}
		return true;
	}

	public bool BeAttact(Player player)
	{
		if (eb != null && Alive == 1)
		{
			return eb.BeAttack(player);
		}
		return true;
	}

	private void Update()
	{
		if (!(bh == null) && eb != null && eb.StartSign != 0)
		{
			if (start == 0)
			{
				eb.me = this;
				eb.OnStart();
				start = 1;
			}
			eb.OnUpdate();
		}
	}
}
