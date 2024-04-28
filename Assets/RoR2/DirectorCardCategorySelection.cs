using System;
using HG;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200052E RID: 1326
	[CreateAssetMenu(menuName = "RoR2/DCCS/DirectorCardCategorySelection")]
	public class DirectorCardCategorySelection : ScriptableObject
	{
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06001819 RID: 6169 RVA: 0x00069C04 File Offset: 0x00067E04
		// (remove) Token: 0x0600181A RID: 6170 RVA: 0x00069C38 File Offset: 0x00067E38
		public static event DirectorCardCategorySelection.CalcCardWeight calcCardWeight;

		// Token: 0x0600181B RID: 6171 RVA: 0x00069C6C File Offset: 0x00067E6C
		public float SumAllWeightsInCategory(DirectorCardCategorySelection.Category category)
		{
			float num = 0f;
			for (int i = 0; i < category.cards.Length; i++)
			{
				if (category.cards[i].IsAvailable())
				{
					num += (float)category.cards[i].selectionWeight;
				}
			}
			return num;
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00069CB4 File Offset: 0x00067EB4
		public int FindCategoryIndexByName(string categoryName)
		{
			for (int i = 0; i < this.categories.Length; i++)
			{
				if (string.CompareOrdinal(this.categories[i].name, base.name) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00069CF8 File Offset: 0x00067EF8
		public void CopyFrom([NotNull] DirectorCardCategorySelection src)
		{
			DirectorCardCategorySelection.Category[] array = src.categories;
			Array.Resize<DirectorCardCategorySelection.Category>(ref this.categories, src.categories.Length);
			for (int i = 0; i < this.categories.Length; i++)
			{
				DirectorCardCategorySelection.Category[] array2 = this.categories;
				int num = i;
				array2[num] = array[i];
				array2[num].cards = ArrayUtils.Clone<DirectorCard>(array2[num].cards);
			}
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00069D5C File Offset: 0x00067F5C
		public WeightedSelection<DirectorCard> GenerateDirectorCardWeightedSelection()
		{
			WeightedSelection<DirectorCard> weightedSelection = new WeightedSelection<DirectorCard>(8);
			for (int i = 0; i < this.categories.Length; i++)
			{
				ref DirectorCardCategorySelection.Category ptr = ref this.categories[i];
				float num = this.SumAllWeightsInCategory(ptr);
				float num2 = ptr.selectionWeight / num;
				if (num > 0f)
				{
					foreach (DirectorCard directorCard in ptr.cards)
					{
						if (directorCard.IsAvailable())
						{
							float weight = (float)directorCard.selectionWeight * num2;
							DirectorCardCategorySelection.CalcCardWeight calcCardWeight = DirectorCardCategorySelection.calcCardWeight;
							if (calcCardWeight != null)
							{
								calcCardWeight(directorCard, ref weight);
							}
							weightedSelection.AddChoice(directorCard, weight);
						}
					}
				}
			}
			return weightedSelection;
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00069E0C File Offset: 0x0006800C
		public void Clear()
		{
			this.categories = Array.Empty<DirectorCardCategorySelection.Category>();
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00069E1C File Offset: 0x0006801C
		public int AddCategory(string name, float selectionWeight)
		{
			DirectorCardCategorySelection.Category category = new DirectorCardCategorySelection.Category
			{
				name = name,
				cards = Array.Empty<DirectorCard>(),
				selectionWeight = selectionWeight
			};
			ArrayUtils.ArrayAppend<DirectorCardCategorySelection.Category>(ref this.categories, category);
			return this.categories.Length - 1;
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00069E66 File Offset: 0x00068066
		public int AddCard(int categoryIndex, DirectorCard card)
		{
			if ((ulong)categoryIndex >= (ulong)((long)this.categories.Length))
			{
				throw new ArgumentOutOfRangeException("categoryIndex");
			}
			DirectorCardCategorySelection.Category[] array = this.categories;
			ArrayUtils.ArrayAppend<DirectorCard>(ref array[categoryIndex].cards, card);
			return array[categoryIndex].cards.Length - 1;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00069EA0 File Offset: 0x000680A0
		public void RemoveCardsThatFailFilter(Predicate<DirectorCard> predicate)
		{
			for (int i = 0; i < this.categories.Length; i++)
			{
				ref DirectorCardCategorySelection.Category ptr = ref this.categories[i];
				for (int j = ptr.cards.Length - 1; j >= 0; j--)
				{
					DirectorCard obj = ptr.cards[j];
					if (!predicate(obj))
					{
						ArrayUtils.ArrayRemoveAtAndResize<DirectorCard>(ref ptr.cards, j, 1);
					}
				}
			}
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public virtual bool IsAvailable()
		{
			return true;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void OnSelected(ClassicStageInfo stageInfo)
		{
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00069F04 File Offset: 0x00068104
		public void OnValidate()
		{
			for (int i = 0; i < this.categories.Length; i++)
			{
				DirectorCardCategorySelection.Category category = this.categories[i];
				if (category.selectionWeight <= 0f)
				{
					Debug.LogErrorFormat("'{0}' in '{1}' has no weight!", new object[]
					{
						category.name,
						this
					});
				}
				for (int j = 0; j < category.cards.Length; j++)
				{
					DirectorCard directorCard = category.cards[j];
					if ((float)directorCard.selectionWeight <= 0f)
					{
						Debug.LogErrorFormat("'{0}' in '{1}' has no weight!", new object[]
						{
							directorCard.spawnCard.name,
							this
						});
					}
				}
			}
		}

		// Token: 0x04001DC3 RID: 7619
		public DirectorCardCategorySelection.Category[] categories = Array.Empty<DirectorCardCategorySelection.Category>();

		// Token: 0x0200052F RID: 1327
		// (Invoke) Token: 0x06001828 RID: 6184
		public delegate void CalcCardWeight(DirectorCard card, ref float weight);

		// Token: 0x02000530 RID: 1328
		[Serializable]
		public struct Category
		{
			// Token: 0x04001DC4 RID: 7620
			[Tooltip("A name to help identify this category")]
			public string name;

			// Token: 0x04001DC5 RID: 7621
			public DirectorCard[] cards;

			// Token: 0x04001DC6 RID: 7622
			public float selectionWeight;
		}
	}
}
