using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
	public int showComboNum;

	private Text myText;

	private BattleHub bh;

	private Transform[,] blockT;

	public void Init()
	{
		blockT = new Transform[bh.MapW + 1, bh.MapH + 1];
		for (int i = 0; i <= bh.MapW; i++)
		{
			for (int j = 0; j <= bh.MapH; j++)
			{
				GameObject gameObject = ((((uint)(i + j) & (true ? 1u : 0u)) != 0) ? Object.Instantiate(GameObject.Find("SpritePlayer/RedBlock")) : Object.Instantiate(GameObject.Find("SpritePlayer/BlueBlock")));
				gameObject.name = "block-" + i + "-" + j;
				blockT[i, j] = gameObject.GetComponent<Transform>();
				blockT[i, j].position = new Vector3((float)i * bh.BlockW, (float)j * bh.BlockH, 100f);
			}
		}
	}

	public void ChangeBlock(int to)
	{
		if (to == 0)
		{
			for (int i = 0; i <= bh.MapW; i++)
			{
				for (int j = 0; j <= bh.MapH; j++)
				{
					blockT[i, j].localScale = new Vector3(0f, 0f, 1f);
				}
			}
		}
		if (to == 1)
		{
			for (int k = 0; k <= bh.MapW; k++)
			{
				for (int l = 0; l <= bh.MapH; l++)
				{
					if (((k + l) & 1) == 0 && bh.hb.CheckWall(k, l) == 1)
					{
						blockT[k, l].localScale = new Vector3(1f, 1f, 1f);
					}
					else
					{
						blockT[k, l].localScale = new Vector3(0f, 0f, 1f);
					}
				}
			}
		}
		if (to != 2)
		{
			return;
		}
		for (int m = 0; m <= bh.MapW; m++)
		{
			for (int n = 0; n <= bh.MapH; n++)
			{
				if (((m + n) & 1) == 1 && bh.hb.CheckWall(m, n) == 1)
				{
					blockT[m, n].localScale = new Vector3(1f, 1f, 1f);
				}
				else
				{
					blockT[m, n].localScale = new Vector3(0f, 0f, 1f);
				}
			}
		}
	}

	private void Start()
	{
		bh = GameObject.Find("Hub").GetComponent<BattleHub>();
		myText = base.gameObject.GetComponent<Text>();
	}

	private void Update()
	{
		if (showComboNum == 1)
		{
			myText.text = "Combo = " + bh.Combo;
		}
	}
}
