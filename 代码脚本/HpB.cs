using UnityEngine;

public class HpB : MonoBehaviour
{
	public float W = 0.8f;

	public float LW = 1f;

	public Transform myTransform;

	public void change(float Hp)
	{
		if (Hp < 0f)
		{
			Hp = 0f;
		}
		if (Hp > 1f)
		{
			Hp = 1f;
		}
		Vector3 localScale = myTransform.localScale;
		localScale.x = Hp * LW;
		myTransform.localScale = localScale;
		myTransform.position -= new Vector3(LW * (1f - Hp) * W * 0.5f, 0f, 0f);
	}

	private void Start()
	{
		myTransform = base.gameObject.GetComponent<Transform>();
	}

	private void Update()
	{
	}
}
