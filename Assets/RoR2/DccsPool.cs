using System;
using RoR2.ExpansionManagement;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200052A RID: 1322
	[CreateAssetMenu(menuName = "RoR2/DCCS/DccsPool")]
	public class DccsPool : ScriptableObject
	{
		// Token: 0x0600180E RID: 6158 RVA: 0x00069968 File Offset: 0x00067B68
		public WeightedSelection<DirectorCardCategorySelection> GenerateWeightedSelection()
		{
			WeightedSelection<DirectorCardCategorySelection> weightedSelection = new WeightedSelection<DirectorCardCategorySelection>(8);
			foreach (DccsPool.Category category in this.poolCategories)
			{
				float num = this.SumAllWeightsInCategory(category);
				if (num > 0f)
				{
					float num2 = category.categoryWeight / num;
					foreach (DccsPool.PoolEntry poolEntry in category.alwaysIncluded)
					{
						if (poolEntry.dccs.IsAvailable())
						{
							weightedSelection.AddChoice(poolEntry.dccs, poolEntry.weight * num2);
						}
					}
					bool flag = false;
					foreach (DccsPool.ConditionalPoolEntry conditionalPoolEntry in category.includedIfConditionsMet)
					{
						if (conditionalPoolEntry.dccs.IsAvailable() && this.AreConditionsMet(conditionalPoolEntry))
						{
							weightedSelection.AddChoice(conditionalPoolEntry.dccs, conditionalPoolEntry.weight * num2);
							flag = true;
						}
					}
					if (!flag)
					{
						foreach (DccsPool.PoolEntry poolEntry2 in category.includedIfNoConditionsMet)
						{
							if (poolEntry2.dccs.IsAvailable())
							{
								weightedSelection.AddChoice(poolEntry2.dccs, poolEntry2.weight * num2);
							}
						}
					}
				}
			}
			return weightedSelection;
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x00069AA8 File Offset: 0x00067CA8
		private float SumAllWeightsInCategory(DccsPool.Category category)
		{
			float num = 0f;
			foreach (DccsPool.PoolEntry poolEntry in category.alwaysIncluded)
			{
				if (poolEntry.dccs.IsAvailable())
				{
					num += poolEntry.weight;
				}
			}
			bool flag = false;
			foreach (DccsPool.ConditionalPoolEntry conditionalPoolEntry in category.includedIfConditionsMet)
			{
				if (conditionalPoolEntry.dccs.IsAvailable() && this.AreConditionsMet(conditionalPoolEntry))
				{
					num += conditionalPoolEntry.weight;
					flag = true;
				}
			}
			if (!flag)
			{
				foreach (DccsPool.PoolEntry poolEntry2 in category.includedIfNoConditionsMet)
				{
					if (poolEntry2.dccs.IsAvailable())
					{
						num += poolEntry2.weight;
					}
				}
			}
			return num;
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x00069B68 File Offset: 0x00067D68
		private bool AreConditionsMet(DccsPool.ConditionalPoolEntry entry)
		{
			if (Run.instance != null)
			{
				foreach (ExpansionDef expansionDef in entry.requiredExpansions)
				{
					if (!Run.instance.IsExpansionEnabled(expansionDef))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x04001DB7 RID: 7607
		[SerializeField]
		private DccsPool.Category[] poolCategories;

		// Token: 0x0200052B RID: 1323
		[Serializable]
		public class PoolEntry : ISerializationCallbackReceiver
		{
			// Token: 0x06001812 RID: 6162 RVA: 0x00069BAD File Offset: 0x00067DAD
			public void OnBeforeSerialize()
			{
				this.hasBeenSerialized = true;
			}

			// Token: 0x06001813 RID: 6163 RVA: 0x00069BB6 File Offset: 0x00067DB6
			public virtual void OnAfterDeserialize()
			{
				if (!this.hasBeenSerialized)
				{
					this.weight = 1f;
				}
			}

			// Token: 0x04001DB8 RID: 7608
			public DirectorCardCategorySelection dccs;

			// Token: 0x04001DB9 RID: 7609
			[Tooltip("The weight of this entry relative to its siblings")]
			public float weight;

			// Token: 0x04001DBA RID: 7610
			[SerializeField]
			[HideInInspector]
			protected bool hasBeenSerialized;
		}

		// Token: 0x0200052C RID: 1324
		[Serializable]
		public class ConditionalPoolEntry : DccsPool.PoolEntry
		{
			// Token: 0x04001DBB RID: 7611
			[Tooltip("ALL expansions in this list must be enabled for this run for this entry to be considered.")]
			public ExpansionDef[] requiredExpansions;
		}

		// Token: 0x0200052D RID: 1325
		[Serializable]
		public class Category : ISerializationCallbackReceiver
		{
			// Token: 0x06001816 RID: 6166 RVA: 0x00069BD3 File Offset: 0x00067DD3
			public void OnBeforeSerialize()
			{
				this.hasBeenSerialized = true;
			}

			// Token: 0x06001817 RID: 6167 RVA: 0x00069BDC File Offset: 0x00067DDC
			public void OnAfterDeserialize()
			{
				if (!this.hasBeenSerialized)
				{
					this.categoryWeight = 1f;
				}
			}

			// Token: 0x04001DBC RID: 7612
			[Tooltip("A name to help identify this category")]
			public string name;

			// Token: 0x04001DBD RID: 7613
			[Tooltip("The weight of all entries in this category relative to the sibling categories.")]
			public float categoryWeight = 1f;

			// Token: 0x04001DBE RID: 7614
			[Tooltip("These entries are always considered.")]
			public DccsPool.PoolEntry[] alwaysIncluded;

			// Token: 0x04001DBF RID: 7615
			[Tooltip("These entries are only considered if their individual conditions are met.")]
			public DccsPool.ConditionalPoolEntry[] includedIfConditionsMet;

			// Token: 0x04001DC0 RID: 7616
			[Tooltip("These entries are considered only if no entries from 'includedIfConditionsMet' have been included.")]
			public DccsPool.PoolEntry[] includedIfNoConditionsMet;

			// Token: 0x04001DC1 RID: 7617
			[HideInInspector]
			[SerializeField]
			protected bool hasBeenSerialized;
		}
	}
}
