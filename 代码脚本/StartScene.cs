using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
	[RuntimeInitializeOnLoadMethod]
	private static void Initialize()
	{
		string text = "Start-FirstTime";
		if (!SceneManager.GetActiveScene().name.Equals(text))
		{
			SceneManager.LoadScene(text);
		}
	}
}
