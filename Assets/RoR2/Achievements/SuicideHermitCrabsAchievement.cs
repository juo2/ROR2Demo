using System;
using RoR2.Stats;
using UnityEngine;

namespace RoR2.Achievements
{
	// Token: 0x02000EC3 RID: 3779
	[RegisterAchievement("SuicideHermitCrabs", "Items.AutoCastEquipment", null, typeof(SuicideHermitCrabsAchievement.SuicideHermitCrabsServerAchievement))]
	public class SuicideHermitCrabsAchievement : BaseAchievement
	{
		// Token: 0x06005615 RID: 22037 RVA: 0x0015F3E5 File Offset: 0x0015D5E5
		public override void OnInstall()
		{
			base.OnInstall();
			UserProfile userProfile = base.userProfile;
			userProfile.onStatsReceived = (Action)Delegate.Combine(userProfile.onStatsReceived, new Action(this.Check));
			base.SetServerTracked(true);
		}

		// Token: 0x06005616 RID: 22038 RVA: 0x0015F41B File Offset: 0x0015D61B
		public override void OnUninstall()
		{
			UserProfile userProfile = base.userProfile;
			userProfile.onStatsReceived = (Action)Delegate.Remove(userProfile.onStatsReceived, new Action(this.Check));
			base.OnUninstall();
		}

		// Token: 0x06005617 RID: 22039 RVA: 0x0015F44A File Offset: 0x0015D64A
		private void Check()
		{
			if (base.userProfile.statSheet.GetStatValueULong(StatDef.suicideHermitCrabsAchievementProgress) >= 20UL)
			{
				base.Grant();
			}
		}

		// Token: 0x06005618 RID: 22040 RVA: 0x0015F46C File Offset: 0x0015D66C
		public override float ProgressForAchievement()
		{
			return base.userProfile.statSheet.GetStatValueULong(StatDef.suicideHermitCrabsAchievementProgress) / 20f;
		}

		// Token: 0x04005078 RID: 20600
		private const int requirement = 20;

		// Token: 0x02000EC4 RID: 3780
		private class SuicideHermitCrabsServerAchievement : BaseServerAchievement
		{
			// Token: 0x0600561A RID: 22042 RVA: 0x0015F48C File Offset: 0x0015D68C
			public override void OnInstall()
			{
				base.OnInstall();
				this.crabBodyIndex = BodyCatalog.FindBodyIndex("HermitCrabBody");
				GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeath;
			}

			// Token: 0x0600561B RID: 22043 RVA: 0x0015F4B5 File Offset: 0x0015D6B5
			public override void OnUninstall()
			{
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeath;
				base.OnUninstall();
			}

			// Token: 0x0600561C RID: 22044 RVA: 0x0015F4D0 File Offset: 0x0015D6D0
			private void OnCharacterDeath(DamageReport damageReport)
			{
				if (!damageReport.victimBody)
				{
					return;
				}
				GameObject inflictor = damageReport.damageInfo.inflictor;
				if (!inflictor || !inflictor.GetComponent<MapZone>())
				{
					return;
				}
				if (damageReport.victimBody.bodyIndex == this.crabBodyIndex && damageReport.victimBody.teamComponent.teamIndex != TeamIndex.Player)
				{
					PlayerStatsComponent masterPlayerStatsComponent = base.networkUser.masterPlayerStatsComponent;
					if (masterPlayerStatsComponent)
					{
						masterPlayerStatsComponent.currentStats.PushStatValue(StatDef.suicideHermitCrabsAchievementProgress, 1UL);
					}
				}
			}

			// Token: 0x04005079 RID: 20601
			private BodyIndex crabBodyIndex;
		}
	}
}
