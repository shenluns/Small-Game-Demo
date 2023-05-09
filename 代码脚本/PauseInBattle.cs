using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseInBattle : MonoBehaviour
{
	private RectTransform pause_continue;

	private RectTransform pause_setting;

	private RectTransform pause_end;

	public GameObject SettingPad;

	private void Start()
	{
		pause_continue = GameObject.Find("Canvas/Pause-Continue").GetComponent<RectTransform>();
		pause_setting = GameObject.Find("Canvas/Pause-Setting").GetComponent<RectTransform>();
		pause_end = GameObject.Find("Canvas/Pause-End").GetComponent<RectTransform>();
		DataCenter component = GameObject.Find("DataCenter").GetComponent<DataCenter>();
		if (component.Language == "CN")
		{
			GameObject.Find("Canvas/Pause/Text").GetComponent<Text>().text = "暂停";
			GameObject.Find("Canvas/Pause-Continue/Text").GetComponent<Text>().text = "继续游戏";
			GameObject.Find("Canvas/Pause-Setting/Text").GetComponent<Text>().text = "设置";
			GameObject.Find("Canvas/Pause-End/Text").GetComponent<Text>().text = "返回主菜单";
			GameObject.Find("Canvas/Pause-End/Text").GetComponent<Text>().fontSize = 40;
		}
		if (component.Language == "JP")
		{
			GameObject.Find("Canvas/Pause/Text").GetComponent<Text>().text = "一時停止";
			GameObject.Find("Canvas/Pause-Continue/Text").GetComponent<Text>().text = "つづきから";
			GameObject.Find("Canvas/Pause-Setting/Text").GetComponent<Text>().text = "オプション";
			GameObject.Find("Canvas/Pause-End/Text").GetComponent<Text>().text = "メニューに戻る";
		}
	}

	public void Show()
	{
		base.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
	}

	public void Unshow()
	{
		base.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0f, 0f, 1f);
	}

	public void ShowButten()
	{
		pause_continue.localScale = new Vector3(1f, 1f, 1f);
		pause_setting.localScale = new Vector3(1f, 1f, 1f);
		pause_end.localScale = new Vector3(1f, 1f, 1f);
	}

	public void UnshowButten()
	{
		pause_continue.localScale = new Vector3(0f, 0f, 1f);
		pause_setting.localScale = new Vector3(0f, 0f, 1f);
		pause_end.localScale = new Vector3(0f, 0f, 1f);
	}

	public void ClickPause()
	{
		HubBehaviour hb = GameObject.Find("Hub").GetComponent<BattleHub>().hb;
		if (hb != null && hb.me.PauseSign == 0)
		{
			hb.PauseInHB = 1;
			hb.me.Pause();
			Unshow();
			ShowButten();
		}
	}

	public void ClickPause_Continue()
	{
		HubBehaviour hb = GameObject.Find("Hub").GetComponent<BattleHub>().hb;
		if (hb != null && hb.PauseInHB == 1)
		{
			hb.me.PauseEnd();
			Show();
			UnshowButten();
		}
	}

	public void ClickPause_Setting()
	{
		SettingPad.SetActive(value: true);
	}

	public void ClickPause_End()
	{
		SceneManager.LoadScene("Start");
	}
}
