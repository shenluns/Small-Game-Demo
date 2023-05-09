using UnityEngine;

public class HB_PianCha : HubBehaviour
{
	public float DelayTime;

	public override void OnStart()
	{
		base.OnStart();
		Screen.SetResolution(1920, 1080, fullscreen: true);
		me.BlockW = 1f;
		me.BlockH = 1f;
		me.LastTime = -100f;
		me.BPM = 132f;
		me.WrTime = 0.12f;
		me.MusicNum = 1;
		me.NowMusic = (me.NextMusic = 1);
		me.ChangeMusicSign = 0;
		me.ChangePlayer(GameObject.Find("player"));
		me.player.ChangeBehaviour(new PB001());
		me.player.NowX = 0;
		me.player.NowY = 0;
		me.player.MaxHp = 0f;
		me.player.NowHp = 0f;
		me.EnemyNum = 0;
		me.player.pb.StartSign = 1;
		me.ChangePlayerHp(0);
		DelayTime = me.dc.DelayTime;
		Vector3 position = GameObject.Find("Information Camera").GetComponent<Transform>().position;
		position.x = 1000f;
		GameObject.Find("Information Camera").GetComponent<Transform>().position = position;
		me.StartTime = Time.time;
		SoundCenter.PlayMusic(0, "Audios/Musics/兔子嚣张3.25混音可循环132BPM");
		SoundCenter.MusicPlayer[0].loop = true;
		SoundCenter.SetMusicVolume(0, me.dc.maxVolume * me.dc.maxSoundVolume);
	}

	public override void Pause()
	{
		SoundCenter.PauseMusic(0);
		SoundCenter.PauseMusic(1);
	}

	public override void PauseEnd()
	{
		SoundCenter.PlayMusic(0);
		SoundCenter.PlayMusic(1);
	}

	public override void OnUpdate()
	{
		if (CheckEnd() == 1)
		{
			EndGame();
		}
		if (end == 1 || me.PauseSign == 1)
		{
			return;
		}
		me.NowTime = Time.time - me.PauseCostTime - me.StartTime - DelayTime;
		float num = 60f / me.BPM;
		float num2 = me.NowTime + num * 3f - (float)(int)((me.NowTime + num * 3f) / 60f * me.BPM) * 60f / me.BPM;
		me.ChangeMusicSign = 0;
		int num3 = CheckKeyInBattle();
		if ((num2 < me.WrTime || num2 > num - me.WrTime) && me.NowTime - me.LastTime > num - 2f * me.WrTime)
		{
			if (num3 != -1)
			{
				me.turn++;
				me.PressSuccessTime = (me.LastTime = me.NowTime);
				me.Combo++;
			}
		}
		else if (num3 != -1 && !(num2 < me.WrTime) && !(num2 > num - me.WrTime))
		{
			me.PressFailTime = me.NowTime;
			me.Combo = 0;
		}
		if (num3 != -1)
		{
			float num4 = num2;
			int num5 = 1;
			if (num4 > 0.5f * num)
			{
				num5 = -1;
				num4 = num - num4;
			}
			if (num4 > 0.01f)
			{
				num4 *= 50f;
				num4 = (float)(int)(num4 + 0.5f) * 0.01f;
				if (num4 < 0.01f)
				{
					num4 = 0.01f;
				}
				DelayTime += (float)num5 * num4;
				DelayTime = (float)(int)(DelayTime * 100f + 0.5f) * 0.01f;
			}
		}
		if (me.NowTime - me.LastTime >= num && num2 >= me.WrTime && num2 < 0.5f * num)
		{
			me.turn++;
			me.LastTime = me.NowTime;
			me.player.MoveSituation = 0;
			me.player.TryMoveSign = 0;
			me.Combo = 0;
		}
	}
}
