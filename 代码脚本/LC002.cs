using Fungus;
using UnityEngine;

public class LC002 : BattleListenerCase_1
{
	public override bool Condition()
	{
		if (publicSign == 1)
		{
			publicSign = 0;
			return true;
		}
		return false;
	}

	public override void OnTrue()
	{
		bl.bh.ChangeClap(1);
		bl.bh.ChangePlayerHp(1);
		bl.bh.enemy[0].NowX = 9;
		bl.bh.enemy[0].NowY = 13;
		bl.bh.ForcePauseEnd();
		bl.bh.PauseTime = 0f;
		bl.bh.StartTime = Time.time;
		SoundCenter.PlayMusic(0, "Audios/Musics/兔子嚣张3.25混音可循环132BPM");
		SoundCenter.MusicPlayer[0].loop = true;
		SoundCenter.SetMusicVolume(0, bl.bh.dc.maxVolume * bl.bh.dc.maxMusicVolume);
		GameObject.Find("Flowchart").GetComponent<Flowchart>().ExecuteBlock("PekoCallSRabbit-" + bl.bh.dc.Language);
	}
}
