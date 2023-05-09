using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_ChooseLunguage : MonoBehaviour
{
	public DataCenter dc;

	private void Start()
	{
		dc = GameObject.Find("DataCenter").GetComponent<DataCenter>();
	}

	public void ClickCN()
	{
		dc.Language = "CN";
		SceneManager.LoadScene("CheckDelayTime");
	}

	public void ClickEN()
	{
		dc.Language = "EN";
		SceneManager.LoadScene("CheckDelayTime");
	}

	public void ClickJP()
	{
		dc.Language = "JP";
		SceneManager.LoadScene("CheckDelayTime");
	}
}
