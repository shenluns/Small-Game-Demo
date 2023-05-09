using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PianCha_Buttens : MonoBehaviour
{
	private Text dt;

	private Text cb;

	public BattleHub bh;

	public DataCenter dc;

	private void Start()
	{
		dt = GameObject.Find("Canvas/DelayTime").GetComponent<Text>();
		cb = GameObject.Find("Canvas/Combo").GetComponent<Text>();
		bh = GameObject.Find("Hub").GetComponent<BattleHub>();
		dc = GameObject.Find("DataCenter").GetComponent<DataCenter>();
		if (dc.Language == "JP")
		{
			GameObject.Find("Canvas/Back/Text").GetComponent<Text>().text = "戻る";
			GameObject.Find("Canvas/Backandchange/Text").GetComponent<Text>().text = "確認";
			GameObject.Find("Canvas/Text").GetComponent<Text>().text = "リズムに応じてカーソルキーを押して数値が安定するまで";
		}
		if (dc.Language == "CN")
		{
			GameObject.Find("Canvas/Back/Text").GetComponent<Text>().text = "返回";
			GameObject.Find("Canvas/Backandchange/Text").GetComponent<Text>().text = "确认";
			GameObject.Find("Canvas/Text").GetComponent<Text>().text = "请根据节奏按下方向键直至数值平稳";
		}
	}

	public void Click_Back()
	{
		SceneManager.LoadScene("Start");
	}

	public void Click_Backandchange()
	{
		if (bh.hb != null)
		{
			bh.dc.DelayTime = ((HB_PianCha)bh.hb).DelayTime;
			Debug.Log(string.Concat(bh.dc.DelayTime));
		}
		SceneManager.LoadScene("Start");
	}

	private void Update()
	{
		if (bh.hb != null)
		{
			if (dc.Language == "EN")
			{
				dt.text = "Delay Time = " + ((HB_PianCha)bh.hb).DelayTime + "s";
			}
			if (dc.Language == "CN")
			{
				dt.text = "时间延迟 = " + ((HB_PianCha)bh.hb).DelayTime + "秒";
			}
			if (dc.Language == "JP")
			{
				dt.text = "ディレイ = " + ((HB_PianCha)bh.hb).DelayTime + "秒";
			}
			cb.text = bh.Combo + " Combo";
		}
	}
}
