using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_ChooseDifficulty : MonoBehaviour
{
	public DataCenter dc;

	private void Start()
	{
		dc = GameObject.Find("DataCenter").GetComponent<DataCenter>();
		if (dc.Language == "CN")
		{
			GameObject.Find("Canvas/Easy/Text").GetComponent<Text>().text = "简单模式";
			GameObject.Find("Canvas/Hard/Text").GetComponent<Text>().text = "困难模式";
		}
	}

	public void ClickEasy()
	{
		dc.Difficulty = 0;
		SceneManager.LoadScene("Demo_PekoraBattle");
	}

	public void ClickHard()
	{
		dc.Difficulty = 1;
		SceneManager.LoadScene("Demo_PekoraBattle");
	}

	private void Update()
	{
	}
}
