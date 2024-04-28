using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RoR2.Skills;
using RoR2.Stats;

namespace RoR2
{
	// Token: 0x02000914 RID: 2324
	public class GameCompletionStatsHelper
	{
		// Token: 0x0600349C RID: 13468 RVA: 0x000DE2DC File Offset: 0x000DC4DC
		public GameCompletionStatsHelper()
		{
			GameCompletionStatsHelper.<>c__DisplayClass5_0 CS$<>8__locals1;
			CS$<>8__locals1.unlockablesFromAchievements = new HashSet<UnlockableDef>();
			foreach (SurvivorDef survivorDef in SurvivorCatalog.allSurvivorDefs)
			{
				this.<.ctor>g__AddSurvivor|5_1(survivorDef);
			}
			foreach (CharacterBody characterBody in BodyCatalog.allBodyPrefabBodyBodyComponents)
			{
				this.<.ctor>g__AddMonster|5_2(characterBody);
			}
			foreach (AchievementDef achievementDef in AchievementManager.allAchievementDefs)
			{
				this.<.ctor>g__AddAchievement|5_3(achievementDef, ref CS$<>8__locals1);
			}
			foreach (PickupDef pickupDef in PickupCatalog.allPickups)
			{
				this.<.ctor>g__AddPickup|5_4(pickupDef);
			}
			this.unlockablesWithoutAchievements.UnionWith(this.unlockablesForGameCompletion);
			this.unlockablesWithoutAchievements.ExceptWith(CS$<>8__locals1.unlockablesFromAchievements);
			this.completableTotal = 0;
			this.completableTotal += this.unlockablesForGameCompletion.Count;
			this.completableTotal += this.achievementsWithoutUnlockables.Count;
			this.completableTotal += this.encounterablePickups.Count;
			this.completableTotal += SurvivorCatalog.survivorCount * 2;
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x000DE4B4 File Offset: 0x000DC6B4
		public IntFraction GetTotalCompletion(UserProfile userProfile)
		{
			GameCompletionStatsHelper.<>c__DisplayClass6_0 CS$<>8__locals1;
			CS$<>8__locals1.result = new IntFraction(0, 0);
			GameCompletionStatsHelper.<GetTotalCompletion>g__AddResult|6_0(this.GetUnlockableCompletion(userProfile), ref CS$<>8__locals1);
			GameCompletionStatsHelper.<GetTotalCompletion>g__AddResult|6_0(this.GetAchievementWithoutUnlockableCompletion(userProfile), ref CS$<>8__locals1);
			GameCompletionStatsHelper.<GetTotalCompletion>g__AddResult|6_0(this.GetPickupEncounterCompletion(userProfile), ref CS$<>8__locals1);
			GameCompletionStatsHelper.<GetTotalCompletion>g__AddResult|6_0(this.GetSurvivorPickCompletion(userProfile), ref CS$<>8__locals1);
			GameCompletionStatsHelper.<GetTotalCompletion>g__AddResult|6_0(this.GetSurvivorWinCompletion(userProfile), ref CS$<>8__locals1);
			return CS$<>8__locals1.result;
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x000DE51C File Offset: 0x000DC71C
		public IntFraction GetUnlockableCompletion(UserProfile userProfile)
		{
			int num = 0;
			foreach (UnlockableDef unlockableDef in this.unlockablesForGameCompletion)
			{
				if (userProfile.HasUnlockable(unlockableDef))
				{
					num++;
				}
			}
			return new IntFraction(num, this.unlockablesForGameCompletion.Count);
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x000DE588 File Offset: 0x000DC788
		public IntFraction GetCollectibleCompletion(UserProfile userProfile)
		{
			int num = 0;
			foreach (UnlockableDef unlockableDef in this.unlockablesWithoutAchievements)
			{
				if (userProfile.HasUnlockable(unlockableDef))
				{
					num++;
				}
			}
			return new IntFraction(num, this.unlockablesWithoutAchievements.Count);
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x000DE5F4 File Offset: 0x000DC7F4
		public IntFraction GetAchievementCompletion(UserProfile userProfile)
		{
			int num = 0;
			foreach (AchievementDef achievementDef in AchievementManager.allAchievementDefs)
			{
				if (userProfile.HasAchievement(achievementDef.identifier))
				{
					num++;
				}
			}
			return new IntFraction(num, AchievementManager.achievementCount);
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x000DE664 File Offset: 0x000DC864
		private IntFraction GetAchievementWithoutUnlockableCompletion(UserProfile userProfile)
		{
			int num = 0;
			foreach (AchievementDef achievementDef in this.achievementsWithoutUnlockables)
			{
				if (userProfile.HasAchievement(achievementDef.identifier))
				{
					num++;
				}
			}
			return new IntFraction(num, this.achievementsWithoutUnlockables.Count);
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x000DE6D8 File Offset: 0x000DC8D8
		public IntFraction GetPickupEncounterCompletion(UserProfile userProfile)
		{
			int num = 0;
			foreach (PickupDef pickupDef in PickupCatalog.allPickups)
			{
				if (this.encounterablePickups.Contains(pickupDef) && userProfile.HasDiscoveredPickup(pickupDef.pickupIndex))
				{
					num++;
				}
			}
			return new IntFraction(num, this.encounterablePickups.Count);
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x000DE750 File Offset: 0x000DC950
		public IntFraction GetSurvivorPickCompletion(UserProfile userProfile)
		{
			return this.GetSurvivorULongStatCompletion(userProfile, PerBodyStatDef.timesPicked, true);
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x000DE75F File Offset: 0x000DC95F
		public IntFraction GetSurvivorWinCompletion(UserProfile userProfile)
		{
			return this.GetSurvivorULongStatCompletion(userProfile, PerBodyStatDef.totalWins, true);
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x000DE770 File Offset: 0x000DC970
		private IntFraction GetSurvivorULongStatCompletion(UserProfile userProfile, PerBodyStatDef perBodyStatDef, bool ignoreHidden)
		{
			int num = 0;
			int num2 = 0;
			foreach (SurvivorDef survivorDef in SurvivorCatalog.allSurvivorDefs)
			{
				if (!ignoreHidden || !survivorDef.hidden)
				{
					num2++;
					string bodyName = BodyCatalog.GetBodyName(SurvivorCatalog.GetBodyIndexFromSurvivorIndex(survivorDef.survivorIndex));
					if (userProfile.statSheet.GetStatValueULong(perBodyStatDef, bodyName) > 0UL)
					{
						num++;
					}
				}
			}
			return new IntFraction(num, num2);
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x000DE7F8 File Offset: 0x000DC9F8
		[CompilerGenerated]
		private bool <.ctor>g__AddUnlockable|5_0(UnlockableDef unlockableDef)
		{
			if (unlockableDef == null)
			{
				return false;
			}
			this.unlockablesForGameCompletion.Add(unlockableDef);
			return true;
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x000DE814 File Offset: 0x000DCA14
		[CompilerGenerated]
		private void <.ctor>g__AddSurvivor|5_1(SurvivorDef survivorDef)
		{
			this.<.ctor>g__AddUnlockable|5_0(survivorDef.unlockableDef);
			BodyIndex bodyIndexFromSurvivorIndex = SurvivorCatalog.GetBodyIndexFromSurvivorIndex(survivorDef.survivorIndex);
			GenericSkill[] bodyPrefabSkillSlots = BodyCatalog.GetBodyPrefabSkillSlots(bodyIndexFromSurvivorIndex);
			if (bodyPrefabSkillSlots != null)
			{
				GenericSkill[] array = bodyPrefabSkillSlots;
				for (int i = 0; i < array.Length; i++)
				{
					SkillFamily skillFamily = array[i].skillFamily;
					if (skillFamily)
					{
						foreach (SkillFamily.Variant variant in skillFamily.variants)
						{
							this.<.ctor>g__AddUnlockable|5_0(variant.unlockableDef);
						}
					}
				}
			}
			foreach (SkinDef skinDef in BodyCatalog.GetBodySkins(bodyIndexFromSurvivorIndex))
			{
				this.<.ctor>g__AddUnlockable|5_0(skinDef.unlockableDef);
			}
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x000DE8C7 File Offset: 0x000DCAC7
		[CompilerGenerated]
		private void <.ctor>g__AddMonster|5_2(CharacterBody characterBody)
		{
			DeathRewards component = characterBody.GetComponent<DeathRewards>();
			this.<.ctor>g__AddUnlockable|5_0((component != null) ? component.logUnlockableDef : null);
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x000DE8E4 File Offset: 0x000DCAE4
		[CompilerGenerated]
		private void <.ctor>g__AddAchievement|5_3(AchievementDef achievementDef, ref GameCompletionStatsHelper.<>c__DisplayClass5_0 A_2)
		{
			UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef(achievementDef.unlockableRewardIdentifier);
			if (!this.<.ctor>g__AddUnlockable|5_0(unlockableDef))
			{
				this.achievementsWithoutUnlockables.Add(achievementDef);
				return;
			}
			A_2.unlockablesFromAchievements.Add(unlockableDef);
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x000DE924 File Offset: 0x000DCB24
		[CompilerGenerated]
		private void <.ctor>g__AddPickup|5_4(PickupDef pickupDef)
		{
			ItemDef itemDef = ItemCatalog.GetItemDef(pickupDef.itemIndex);
			EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(pickupDef.equipmentIndex);
			if ((itemDef != null && itemDef.inDroppableTier) || (equipmentDef != null && equipmentDef.canDrop))
			{
				this.encounterablePickups.Add(pickupDef);
			}
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x000DE96E File Offset: 0x000DCB6E
		[CompilerGenerated]
		internal static void <GetTotalCompletion>g__AddResult|6_0(IntFraction incoming, ref GameCompletionStatsHelper.<>c__DisplayClass6_0 A_1)
		{
			A_1.result = new IntFraction(A_1.result.numerator + incoming.numerator, A_1.result.denominator + incoming.denominator);
		}

		// Token: 0x0400359C RID: 13724
		private HashSet<UnlockableDef> unlockablesForGameCompletion = new HashSet<UnlockableDef>();

		// Token: 0x0400359D RID: 13725
		private HashSet<AchievementDef> achievementsWithoutUnlockables = new HashSet<AchievementDef>();

		// Token: 0x0400359E RID: 13726
		private HashSet<UnlockableDef> unlockablesWithoutAchievements = new HashSet<UnlockableDef>();

		// Token: 0x0400359F RID: 13727
		private HashSet<PickupDef> encounterablePickups = new HashSet<PickupDef>();

		// Token: 0x040035A0 RID: 13728
		private int completableTotal;
	}
}
