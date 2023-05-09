using UnityEngine;

public class MR001 : MonoBehaviour
{
	public BattleListener bl;

	public BattleListenerCase_1[] cases = new BattleListenerCase_1[100];

	private void Start()
	{
		bl = GameObject.Find("Listener").GetComponent<BattleListener>();
		cases[0] = new LC002();
		cases[1] = new LC003();
		cases[2] = new LC004();
		cases[3] = new LC005();
		cases[4] = new LC006();
		cases[5] = new LC007();
		cases[6] = new LC0011();
		cases[7] = new LC0012();
		cases[8] = new LC0013();
		cases[9] = new LC0014();
		cases[10] = new LC0015();
		cases[11] = new LC0016();
		for (int i = 0; i <= 11; i++)
		{
			bl.addCase(cases[i]);
		}
	}

	public void call_0()
	{
		cases[0].publicSign = 1;
	}

	public void call_1()
	{
		cases[1].publicSign = 1;
	}

	public void call_2()
	{
		cases[2].publicSign = 1;
	}

	public void call_3()
	{
		cases[3].publicSign = 1;
	}

	public void call_4()
	{
		cases[4].publicSign = 1;
	}

	public void call_5()
	{
		cases[5].publicSign = 1;
	}

	public void call_6()
	{
		cases[6].publicSign = 1;
	}

	public void call_7()
	{
		cases[7].publicSign = 1;
	}

	public void call_8()
	{
		cases[8].publicSign = 1;
	}

	public void call_9()
	{
		cases[9].publicSign = 1;
	}

	public void call_10()
	{
		cases[10].publicSign = 1;
	}

	public void call_11()
	{
		cases[11].publicSign = 1;
	}
}
