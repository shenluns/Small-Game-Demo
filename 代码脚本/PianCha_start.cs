using UnityEngine;

public class PianCha_start : MonoBehaviour
{
	private void Start()
	{
		GameObject.Find("Hub").GetComponent<BattleHub>().ChangeBehaviour(new HB_PianCha());
		GameObject.Find("Hub").GetComponent<BattleHub>().hb.StartSign = 1;
	}

	private void Update()
	{
	}
}
