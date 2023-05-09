using UnityEngine;

public class LC0013 : BattleListenerCase_1
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
		Enemy enemy = bl.bh.enemy[0];
		int[] array = new int[3];
		do
		{
			array[0] = Random.Range(2, 19);
			array[1] = Random.Range(3, 14);
		}
		while (bl.bh.GameMap[array[0], array[1]] != 1 || bl.bh.RoleMap[array[0], array[1]] != -2 || (array[0] == enemy.NowX && array[1] == enemy.NowY));
		bl.bh.RoleMap[enemy.NowX, enemy.NowY] = -2;
		GameObject gameObject = Object.Instantiate(GameObject.Find("SpritePlayer/baozha"));
		Vector3 localScale = gameObject.GetComponent<Transform>().localScale;
		gameObject.GetComponent<Transform>().localScale = localScale;
		gameObject.GetComponent<Transform>().position = new Vector3(enemy.bh.BlockW * (float)enemy.NowX, enemy.bh.BlockH * (float)enemy.NowY + 0.31f, 60f + enemy.bh.BlockH * (float)enemy.NowY * 0.01f - 0.0001f);
		gameObject.GetComponent<SpritePlayer>().ss = ((Pekora_Pekora)enemy.eb).sss[2];
		gameObject.GetComponent<SpritePlayer>().NowStart();
		GameObject gameObject2 = Object.Instantiate(GameObject.Find("Pekora_TNT"));
		Enemy component = gameObject2.GetComponent<Enemy>();
		component.MaxHp = 100f;
		component.NowHp = 100f;
		component.Type = 203;
		component.ChangeBehaviour(new Pekora_TNT());
		bl.bh.AddEnemy(gameObject2);
		gameObject2.name = "enemy1_" + component.Number;
		component.eb.OnStart();
		if (component.start == 0)
		{
			component.start = 1;
		}
		component.TryMoveSign = 0;
		if (enemy.MoveSign == 1)
		{
			component.NowX = (component.NextX = (component.LastX = enemy.LastX));
			component.NowY = (component.NextY = (component.LastY = enemy.LastY));
		}
		else
		{
			component.NowX = (component.NextX = (component.LastX = enemy.NowX));
			component.NowY = (component.NextY = (component.LastY = enemy.NowY));
		}
		component.NowTime = enemy.NowTime;
		bl.bh.RoleMap[enemy.NowX, enemy.NowY] = component.Number;
		component.eb.StartSign = 1;
		SoundCenter.PlayMusic(3, "Audios/Sounds/战斗/炮 SND6326");
		SoundCenter.SetMusicVolume(3, 0.5f);
		enemy.NowX = (enemy.LastX = (enemy.NextX = array[0]));
		enemy.NowY = (enemy.LastY = (enemy.NextY = array[1]));
		component.eb.ChangePos();
		enemy.eb.ChangePos();
		GameObject.Find("Main Camera").GetComponent<camera001>().center = gameObject2;
	}
}
