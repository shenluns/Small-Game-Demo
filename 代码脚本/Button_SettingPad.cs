using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_SettingPad : MonoBehaviour
{
	public DataCenter dc;

	public string nowSceneName;

	private void Start()
	{
		nowSceneName = SceneManager.GetActiveScene().name;
		dc = GameObject.Find("DataCenter").GetComponent<DataCenter>();
		GameObject.Find("Canvas/SettingPad/Volume").GetComponent<Slider>().value = dc.maxVolume;
		if (dc.Language == "EN")
		{
			GameObject.Find("Canvas/SettingPad/VolumeText").GetComponent<Text>().text = "Volume";
			GameObject.Find("Canvas/SettingPad/SettingText").GetComponent<Text>().text = "Setting";
			GameObject.Find("Canvas/SettingPad/SettingText").GetComponent<Text>().fontSize = 30;
			GameObject.Find("Canvas/SettingPad/Save/Text").GetComponent<Text>().text = "Confirm";
			if (nowSceneName == "Start")
			{
				GameObject.Find("Canvas/SettingPad/SetPiancha/Text").GetComponent<Text>().text = "Audio Latency Calibration";
				GameObject.Find("Canvas/SettingPad/SetPiancha/Text").GetComponent<Text>().fontSize = 20;
			}
		}
		if (dc.Language == "JP")
		{
			GameObject.Find("Canvas/SettingPad/VolumeText").GetComponent<Text>().text = "ボリューム";
			GameObject.Find("Canvas/SettingPad/VolumeText").GetComponent<Text>().fontSize = 30;
			GameObject.Find("Canvas/SettingPad/SettingText").GetComponent<Text>().text = "オプション";
			GameObject.Find("Canvas/SettingPad/SettingText").GetComponent<Text>().fontSize = 30;
			GameObject.Find("Canvas/SettingPad/Save/Text").GetComponent<Text>().text = "確認";
			if (nowSceneName == "Start")
			{
				GameObject.Find("Canvas/SettingPad/SetPiancha/Text").GetComponent<Text>().text = "リズム較正";
			}
		}
	}

	public void ClickBack()
	{
		base.gameObject.SetActive(value: false);
	}

	public void ClickPiancha()
	{
		SceneManager.LoadScene("CheckDelayTime");
	}

	private void Update()
	{
		dc.maxVolume = GameObject.Find("Canvas/SettingPad/Volume").GetComponent<Slider>().value;
		if (nowSceneName == "Start")
		{
			GameObject.Find("SoundCenter").GetComponent<AudioSource>().volume = dc.maxVolume;
		}
		else
		{
			SoundCenter.SetMusicVolume(0, dc.maxVolume);
		}
	}
}
