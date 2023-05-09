using UnityEngine;

public class Demo_start001 : MonoBehaviour
{
	private void Start()
	{
		GameObject.Find("Hub").GetComponent<BattleHub>().ChangeBehaviour(new HB001());
		GameObject.Find("Hub").GetComponent<BattleHub>().hb.StartSign = 1;
		GameObject.Find("Listener").GetComponent<BattleListener>().addCase(new LC001());
		GameObject.Find("Listener").GetComponent<BattleListener>().addCase(new LC008());
		GameObject.Find("Listener").GetComponent<BattleListener>().addCase(new LC009());
		GameObject.Find("Listener").GetComponent<BattleListener>().addCase(new LC0010());
	}

	private void Update()
	{
	}
}
