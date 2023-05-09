using Fungus;
using UnityEngine;

public class BattleListener : MonoBehaviour
{
	public BattleListenerCase[] cases = new BattleListenerCase[100];

	public BattleHub bh;

	public Flowchart fc;

	public int caseCnt;

	public void addCase(BattleListenerCase blc)
	{
		blc.bl = this;
		cases[caseCnt++] = blc;
	}

	private void Start()
	{
		bh = GameObject.Find("Hub").GetComponent<BattleHub>();
		fc = GameObject.Find("Flowchart").GetComponent<Flowchart>();
	}

	private void Update()
	{
		for (int i = 0; i < caseCnt; i++)
		{
			if (cases[i] != null && cases[i].Condition())
			{
				cases[i].OnTrue();
			}
		}
	}
}
