using UnityEngine;

public class DataCenter : MonoBehaviour
{
	public string Language = "CN";

	public float DelayTime;

	public int Difficulty;

	public bool Invincible;

	public float maxVolume = 1f;

	public float maxMusicVolume = 1f;

	public float maxSoundVolume = 1f;

	private void Start()
	{
		Screen.SetResolution(1920, 1080, fullscreen: true);
		Object.DontDestroyOnLoad(base.gameObject);
	}
}
