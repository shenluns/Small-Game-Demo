using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtens : MonoBehaviour
{
	private Button buttonObj;

	public GameObject tgobj;

	private void Start()
	{
		buttonObj = base.gameObject.GetComponent<Button>();
		if (base.gameObject.name == "StartButton")
		{
			buttonObj.GetComponent<Button>().onClick.AddListener(StartButton);
		}
		if (base.gameObject.name == "SettingButton")
		{
			buttonObj.GetComponent<Button>().onClick.AddListener(SettingButton);
		}
		if (base.gameObject.name == "ExitButton")
		{
			buttonObj.GetComponent<Button>().onClick.AddListener(ExitButton);
		}
		DataCenter component = GameObject.Find("DataCenter").GetComponent<DataCenter>();
		if (component.Language == "CN")
		{
			GameObject.Find("Canvas/格子/StartButton/Text").GetComponent<Text>().text = "开始";
			GameObject.Find("Canvas/格子/SettingButton/Text").GetComponent<Text>().text = "设置";
			GameObject.Find("Canvas/格子/ExitButton/Text").GetComponent<Text>().text = "退出游戏";
		}
		if (component.Language == "JP")
		{
			GameObject.Find("Canvas/格子/StartButton/Text").GetComponent<Text>().text = "はじめから";
			GameObject.Find("Canvas/格子/SettingButton/Text").GetComponent<Text>().text = "オプション";
			GameObject.Find("Canvas/格子/ExitButton/Text").GetComponent<Text>().text = "終わる";
		}
	}

	public void StartButton()
	{
		SceneManager.LoadScene("Demo_ChooseDifficulty");
	}

	public void SettingButton()
	{
		tgobj.SetActive(value: true);
	}

	public void ExitButton()
	{
		Application.Quit();
	}
}
