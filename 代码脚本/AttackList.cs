public class AttackList
{
	private AttackDate[] heap = new AttackDate[1000];

	private int Count;

	private int Compare(AttackDate x, AttackDate y)
	{
		return x.turn - y.turn;
	}

	private void SiftDown(int n)
	{
		AttackDate attackDate = heap[n];
		for (int num = n * 2; num < Count; num *= 2)
		{
			if (num + 1 < Count && Compare(heap[num + 1], heap[num]) > 0)
			{
				num++;
			}
			if (Compare(attackDate, heap[num]) >= 0)
			{
				break;
			}
			heap[n] = heap[num];
			n = num;
		}
		heap[n] = attackDate;
	}

	private void SiftUp(int n)
	{
		AttackDate attackDate = heap[n];
		int num = n / 2;
		while (n > 0 && Compare(attackDate, heap[num]) > 0)
		{
			heap[n] = heap[num];
			n = num;
			num /= 2;
		}
		heap[n] = attackDate;
	}

	public int Size()
	{
		return Count;
	}

	public void pop()
	{
		heap[0] = heap[--Count];
		if (Count > 0)
		{
			SiftDown(0);
		}
	}

	public AttackDate top()
	{
		if (Count > 0)
		{
			return heap[0];
		}
		return null;
	}

	public void push(AttackDate v)
	{
		heap[Count] = v;
		SiftUp(Count++);
	}
}
