using System;
using UnityEngine;

// Token: 0x02000095 RID: 149
public class WeightedSelection<T>
{
	// Token: 0x1700002D RID: 45
	// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000ACA6 File Offset: 0x00008EA6
	// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000ACAE File Offset: 0x00008EAE
	public int Count
	{
		get
		{
			return this._count;
		}
		private set
		{
			this._count = value;
		}
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x0000ACB7 File Offset: 0x00008EB7
	public WeightedSelection(int capacity = 8)
	{
		this.choices = new WeightedSelection<T>.ChoiceInfo[capacity];
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000ACCB File Offset: 0x00008ECB
	// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000ACD5 File Offset: 0x00008ED5
	public int Capacity
	{
		get
		{
			return this.choices.Length;
		}
		set
		{
			if (value < 8 || value < this.Count)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			Array sourceArray = this.choices;
			this.choices = new WeightedSelection<T>.ChoiceInfo[value];
			Array.Copy(sourceArray, this.choices, this.Count);
		}
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0000AD14 File Offset: 0x00008F14
	public void AddChoice(T value, float weight)
	{
		this.AddChoice(new WeightedSelection<T>.ChoiceInfo
		{
			value = value,
			weight = weight
		});
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x0000AD40 File Offset: 0x00008F40
	public void AddChoice(WeightedSelection<T>.ChoiceInfo choice)
	{
		if (this.Count == this.Capacity)
		{
			this.Capacity *= 2;
		}
		WeightedSelection<T>.ChoiceInfo[] array = this.choices;
		int count = this.Count;
		this.Count = count + 1;
		array[count] = choice;
		this.totalWeight += choice.weight;
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0000AD9C File Offset: 0x00008F9C
	public void RemoveChoice(int choiceIndex)
	{
		if (choiceIndex < 0 || this.Count <= choiceIndex)
		{
			throw new ArgumentOutOfRangeException("choiceIndex");
		}
		int i = choiceIndex;
		int num = this.Count - 1;
		while (i < num)
		{
			this.choices[i] = this.choices[i + 1];
			i++;
		}
		WeightedSelection<T>.ChoiceInfo[] array = this.choices;
		int num2 = this.Count - 1;
		this.Count = num2;
		array[num2] = default(WeightedSelection<T>.ChoiceInfo);
		this.RecalculateTotalWeight();
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0000AE18 File Offset: 0x00009018
	public void ModifyChoiceWeight(int choiceIndex, float newWeight)
	{
		this.choices[choiceIndex].weight = newWeight;
		this.RecalculateTotalWeight();
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0000AE34 File Offset: 0x00009034
	public void Clear()
	{
		for (int i = 0; i < this.Count; i++)
		{
			this.choices[i] = default(WeightedSelection<T>.ChoiceInfo);
		}
		this.Count = 0;
		this.totalWeight = 0f;
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0000AE78 File Offset: 0x00009078
	private void RecalculateTotalWeight()
	{
		this.totalWeight = 0f;
		for (int i = 0; i < this.Count; i++)
		{
			this.totalWeight += this.choices[i].weight;
		}
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0000AEC0 File Offset: 0x000090C0
	public T Evaluate(float normalizedIndex)
	{
		int num = this.EvaluateToChoiceIndex(normalizedIndex);
		return this.choices[num].value;
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0000AEE6 File Offset: 0x000090E6
	public int EvaluateToChoiceIndex(float normalizedIndex)
	{
		return this.EvaluateToChoiceIndex(normalizedIndex, null);
	}

	// Token: 0x060002AE RID: 686 RVA: 0x0000AEF0 File Offset: 0x000090F0
	public int EvaluateToChoiceIndex(float normalizedIndex, int[] ignoreIndices)
	{
		if (this.Count == 0)
		{
			throw new InvalidOperationException("Cannot call Evaluate without available choices.");
		}
		float num = this.totalWeight;
		if (ignoreIndices != null)
		{
			foreach (int num2 in ignoreIndices)
			{
				num -= this.choices[num2].weight;
			}
		}
		float num3 = normalizedIndex * num;
		float num4 = 0f;
		for (int j = 0; j < this.Count; j++)
		{
			if (ignoreIndices == null || Array.IndexOf<int>(ignoreIndices, j) == -1)
			{
				num4 += this.choices[j].weight;
				if (num3 < num4)
				{
					return j;
				}
			}
		}
		return this.Count - 1;
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0000AF9A File Offset: 0x0000919A
	public WeightedSelection<T>.ChoiceInfo GetChoice(int i)
	{
		return this.choices[i];
	}

	// Token: 0x04000248 RID: 584
	[HideInInspector]
	[SerializeField]
	public WeightedSelection<T>.ChoiceInfo[] choices;

	// Token: 0x04000249 RID: 585
	[SerializeField]
	[HideInInspector]
	private int _count;

	// Token: 0x0400024A RID: 586
	[SerializeField]
	[HideInInspector]
	private float totalWeight;

	// Token: 0x0400024B RID: 587
	private const int minCapacity = 8;

	// Token: 0x02000096 RID: 150
	[Serializable]
	public struct ChoiceInfo
	{
		// Token: 0x0400024C RID: 588
		public T value;

		// Token: 0x0400024D RID: 589
		public float weight;
	}
}
