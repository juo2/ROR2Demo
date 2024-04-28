using System;
using RoR2.ExpansionManagement;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006A8 RID: 1704
	[Serializable]
	public class DirectorCard
	{
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x0008E9B9 File Offset: 0x0008CBB9
		public int cost
		{
			get
			{
				return this.spawnCard.directorCreditCost;
			}
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x0008E9C6 File Offset: 0x0008CBC6
		[Obsolete("'CardIsValid' is confusingly named. Use 'IsAvailable' instead.", false)]
		public bool CardIsValid()
		{
			return this.IsAvailable();
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x0008E9D0 File Offset: 0x0008CBD0
		public bool IsAvailable()
		{
			ref string ptr = ref this.requiredUnlockable;
			ref string ptr2 = ref this.forbiddenUnlockable;
			if (!this.requiredUnlockableDef && !string.IsNullOrEmpty(ptr))
			{
				this.requiredUnlockableDef = UnlockableCatalog.GetUnlockableDef(ptr);
				ptr = null;
			}
			if (!this.forbiddenUnlockableDef && !string.IsNullOrEmpty(ptr2))
			{
				this.forbiddenUnlockableDef = UnlockableCatalog.GetUnlockableDef(ptr2);
				ptr2 = null;
			}
			bool flag = !this.requiredUnlockableDef || Run.instance.IsUnlockableUnlocked(this.requiredUnlockableDef);
			bool flag2 = this.forbiddenUnlockableDef && Run.instance.DoesEveryoneHaveThisUnlockableUnlocked(this.forbiddenUnlockableDef);
			if (Run.instance && Run.instance.stageClearCount >= this.minimumStageCompletions && flag && !flag2 && this.spawnCard && this.spawnCard.prefab)
			{
				ExpansionRequirementComponent component = this.spawnCard.prefab.GetComponent<ExpansionRequirementComponent>();
				if (!component)
				{
					CharacterMaster component2 = this.spawnCard.prefab.GetComponent<CharacterMaster>();
					if (component2 && component2.bodyPrefab)
					{
						component = component2.bodyPrefab.GetComponent<ExpansionRequirementComponent>();
					}
				}
				return !component || Run.instance.IsExpansionEnabled(component.requiredExpansion);
			}
			return false;
		}

		// Token: 0x0400268E RID: 9870
		public SpawnCard spawnCard;

		// Token: 0x0400268F RID: 9871
		public int selectionWeight;

		// Token: 0x04002690 RID: 9872
		public DirectorCore.MonsterSpawnDistance spawnDistance;

		// Token: 0x04002691 RID: 9873
		public bool preventOverhead;

		// Token: 0x04002692 RID: 9874
		public int minimumStageCompletions;

		// Token: 0x04002693 RID: 9875
		[Tooltip("'requiredUnlockable' will be discontinued. Use 'requiredUnlockableDef' instead.")]
		[Obsolete("'requiredUnlockable' will be discontinued. Use 'requiredUnlockableDef' instead.", false)]
		public string requiredUnlockable;

		// Token: 0x04002694 RID: 9876
		[Obsolete("'forbiddenUnlockable' will be discontinued. Use 'forbiddenUnlockableDef' instead.", false)]
		[Tooltip("'forbiddenUnlockable' will be discontinued. Use 'forbiddenUnlockableDef' instead.")]
		public string forbiddenUnlockable;

		// Token: 0x04002695 RID: 9877
		public UnlockableDef requiredUnlockableDef;

		// Token: 0x04002696 RID: 9878
		public UnlockableDef forbiddenUnlockableDef;
	}
}
