using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_BackToMainMenu : MonoBehaviour
{
	public void BackToMainMenu()
	{
		SceneManager.LoadScene("Start");
	}
}
