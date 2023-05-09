using System.Collections.Generic;
using UnityEngine;

public static class SoundCenter
{
	public static Dictionary<string, AudioClip> AllClips = new Dictionary<string, AudioClip>();

	public static AudioSource[] MusicPlayer = new AudioSource[10];

	private static int[] paused = new int[10];

	public static GameObject SC;

	public static int PlayMusic(int channel, string name)
	{
		if (MusicPlayer[0] == null)
		{
			SC = GameObject.Find("SoundCenter");
			for (int i = 0; i < 10; i++)
			{
				SC.AddComponent<AudioSource>();
			}
			for (int j = 0; j < 10; j++)
			{
				paused[j] = -1;
				MusicPlayer[j] = SC.GetComponents<AudioSource>()[j];
			}
		}
		paused[channel] = 0;
		AudioClip value;
		if (!AllClips.ContainsKey(name))
		{
			value = Resources.Load<AudioClip>(name);
			AllClips.Add(name, value);
		}
		else
		{
			AllClips.TryGetValue(name, out value);
		}
		MusicPlayer[channel].clip = value;
		MusicPlayer[channel].Play();
		return 1;
	}

	public static void StopMusic(int channel)
	{
		MusicPlayer[channel].Stop();
		paused[channel] = -1;
	}

	public static void PauseMusic(int channel)
	{
		if (MusicPlayer[channel] != null && paused[channel] == 0)
		{
			paused[channel] = 1;
			MusicPlayer[channel].Pause();
		}
	}

	public static void PlayMusic(int channel)
	{
		if (MusicPlayer[channel] != null && paused[channel] == 1)
		{
			paused[channel] = 0;
			MusicPlayer[channel].UnPause();
		}
	}

	public static void SetMusicVolume(int channel, float volume)
	{
		MusicPlayer[channel].volume = volume;
	}
}
