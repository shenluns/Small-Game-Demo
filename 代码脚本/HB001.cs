using Fungus;
using UnityEngine;

public class HB001 : HubBehaviour
{
	public override void OnStart()
	{
		base.OnStart();
		Screen.SetResolution(1920, 1080, fullscreen: true);
		me.BlockW = 1f;
		me.BlockH = 1f;
		string str = ((TextAsset)Resources.Load("Map/peko/peko001")).text;
		me.MapH = BattleHub.GetFirstNum(ref str);
		me.MapW = BattleHub.GetFirstNum(ref str);
		for (int num = me.MapH - 1; num >= 0; num--)
		{
			for (int i = 0; i < me.MapW; i++)
			{
				me.GameMap[i, num] = BattleHub.GetFirstNum(ref str);
				me.MoneyMap[i, num] = -1;
			}
		}
		me.cbs.Init();
		me.ChangePlayer(GameObject.Find("player"));
		me.player.ChangeBehaviour(new PB001());
		me.player.NowX = 10;
		me.player.NowY = 3;
		me.player.MaxHp = 15f;
		me.player.NowHp = 15f;
		me.EnemyNum = 0;
		me.playerMoney = 0;
		me.nowMoney = 0;
		me.maxMoney = 30;
		me.player.ATK = 6 * (2 - me.dc.Difficulty);
		GameObject gameObject = GameObject.Find("Pekora_Pekora");
		Enemy component = gameObject.GetComponent<Enemy>();
		component.MaxHp = 240f;
		component.NowHp = 240f;
		component.NowX = 9;
		component.NowY = 24;
		component.MoveDirect = 0;
		component.ChangeBehaviour(new Pekora_Pekora());
		me.AddEnemy(gameObject);
		gameObject.name = "enemy1_" + component.Number;
		int[,] array = new int[10, 2]
		{
			{ 10, 3 },
			{ 9, 13 },
			{ 5, 11 },
			{ 14, 11 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		};
		for (int j = 0; j < 3; j++)
		{
			gameObject = Object.Instantiate(GameObject.Find("Pekora_Cave"));
			component = gameObject.GetComponent<Enemy>();
			while (true)
			{
				component.NowX = Random.Range(2, 19);
				component.NowY = Random.Range(3, 14);
				if (me.GameMap[component.NowX, component.NowY] != 1)
				{
					continue;
				}
				int num2 = 1;
				for (int k = 0; k < 4 + j; k++)
				{
					if (component.NowX == array[k, 0] && component.NowY == array[k, 1])
					{
						num2 = 0;
						break;
					}
				}
				if (num2 != 0)
				{
					break;
				}
			}
			array[4 + j, 0] = component.NowX;
			array[4 + j, 1] = component.NowY;
			component.MaxHp = 100f;
			component.NowHp = 100f;
			component.MoveDirect = 0;
			component.ChangeBehaviour(new Pekora_Cave());
			me.AddEnemy(gameObject);
			gameObject.name = "enemy1_" + component.Number;
		}
		me.LastTime = -100f;
		me.BPM = 132f;
		me.MusicNum = 1;
		me.NowMusic = (me.NextMusic = 1);
		me.ChangeMusicSign = 0;
		me.StartTime = Time.time;
		for (int l = 0; l < me.EnemyNum; l++)
		{
			me.enemy[l].eb.StartSign = 1;
			me.enemy[l].eb.ChangePos();
		}
		me.enemy[0].gameObject.GetComponent<Transform>().position += new Vector3(0.5f, 0f, 0f);
		me.player.pb.StartSign = 1;
		me.player.pb.ChangePos();
		me.ForcePause();
		me.ChangeClap(0);
		me.ChangePlayerHp(0);
		Vector3 position = GameObject.Find("Information Camera").GetComponent<Transform>().position;
		position.x = 1000f;
		GameObject.Find("Information Camera").GetComponent<Transform>().position = position;
		GameObject.Find("Flowchart").GetComponent<Flowchart>().ExecuteBlock("PekoBegin-" + me.dc.Language);
	}

	public override void Pause()
	{
		SoundCenter.PauseMusic(0);
		GameObject.Find("Canvas/Pause").GetComponent<PauseInBattle>().Unshow();
	}

	public override void PauseEnd()
	{
		SoundCenter.PlayMusic(0);
		GameObject.Find("Canvas/Pause").GetComponent<PauseInBattle>().Show();
	}
}
