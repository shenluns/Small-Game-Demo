using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
	private Text myText;

	private BattleHub bh;

	private void Start()
	{
		bh = GameObject.Find("Hub").GetComponent<BattleHub>();
		myText = base.gameObject.GetComponent<Text>();
	}

	private void Update()
	{
		myText.text = "Money = " + bh.playerMoney;
	}
}
