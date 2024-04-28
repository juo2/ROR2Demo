using System;

namespace RoR2.Achievements.Mage
{
	// Token: 0x02000EE4 RID: 3812
	[RegisterAchievement("MageFastBoss", "Skills.Mage.IceBomb", "FreeMage", typeof(MageFastBossAchievement.MageFastBossServerAchievement))]
	public class MageFastBossAchievement : BaseAchievement
	{
		// Token: 0x060056CD RID: 22221 RVA: 0x00160754 File Offset: 0x0015E954
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("MageBody");
		}

		// Token: 0x060056CE RID: 22222 RVA: 0x0015D621 File Offset: 0x0015B821
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			base.SetServerTracked(true);
		}

		// Token: 0x060056CF RID: 22223 RVA: 0x0015D630 File Offset: 0x0015B830
		protected override void OnBodyRequirementBroken()
		{
			base.SetServerTracked(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x040050A8 RID: 20648
		private static readonly float window = 1f;

		// Token: 0x02000EE5 RID: 3813
		private class MageFastBossServerAchievement : BaseServerAchievement
		{
			// Token: 0x060056D2 RID: 22226 RVA: 0x0016086D File Offset: 0x0015EA6D
			private void OnBossDamageFirstTaken()
			{
				this.expirationTimeStamp = Run.FixedTimeStamp.now + MageFastBossAchievement.window;
				this.listenForBossDamage.SetActive(false);
				this.listenForBossDefeated.SetActive(true);
			}

			// Token: 0x060056D3 RID: 22227 RVA: 0x0016089C File Offset: 0x0015EA9C
			public override void OnInstall()
			{
				base.OnInstall();
				this.listenForBossDamage = new ToggleAction(delegate()
				{
					GlobalEventManager.onServerDamageDealt += this.OnServerDamageDealt;
				}, delegate()
				{
					GlobalEventManager.onServerDamageDealt -= this.OnServerDamageDealt;
				});
				this.listenForBossDefeated = new ToggleAction(delegate()
				{
					BossGroup.onBossGroupDefeatedServer += this.OnBossGroupDefeatedServer;
				}, delegate()
				{
					BossGroup.onBossGroupDefeatedServer -= this.OnBossGroupDefeatedServer;
				});
				BossGroup.onBossGroupStartServer += this.OnBossGroupStartServer;
				Run.onRunStartGlobal += this.OnRunStart;
				this.Reset();
			}

			// Token: 0x060056D4 RID: 22228 RVA: 0x0016091D File Offset: 0x0015EB1D
			public override void OnUninstall()
			{
				BossGroup.onBossGroupStartServer -= this.OnBossGroupStartServer;
				this.listenForBossDefeated.SetActive(false);
				this.listenForBossDamage.SetActive(false);
				base.OnUninstall();
			}

			// Token: 0x060056D5 RID: 22229 RVA: 0x0016094E File Offset: 0x0015EB4E
			private void OnRunStart(Run run)
			{
				this.Reset();
			}

			// Token: 0x060056D6 RID: 22230 RVA: 0x00160956 File Offset: 0x0015EB56
			private void Reset()
			{
				this.expirationTimeStamp = Run.FixedTimeStamp.negativeInfinity;
				this.listenForBossDefeated.SetActive(false);
				this.listenForBossDamage.SetActive(false);
			}

			// Token: 0x060056D7 RID: 22231 RVA: 0x0016097B File Offset: 0x0015EB7B
			private static bool BossGroupIsTeleporterBoss(BossGroup bossGroup)
			{
				return bossGroup.GetComponent<TeleporterInteraction>();
			}

			// Token: 0x060056D8 RID: 22232 RVA: 0x00160988 File Offset: 0x0015EB88
			private void OnBossGroupStartServer(BossGroup bossGroup)
			{
				if (!MageFastBossAchievement.MageFastBossServerAchievement.BossGroupIsTeleporterBoss(bossGroup))
				{
					return;
				}
				this.listenForBossDamage.SetActive(true);
			}

			// Token: 0x060056D9 RID: 22233 RVA: 0x0016099F File Offset: 0x0015EB9F
			private void OnServerDamageDealt(DamageReport damageReport)
			{
				if (damageReport.victimMaster && damageReport.victimMaster.isBoss)
				{
					this.OnBossDamageFirstTaken();
				}
			}

			// Token: 0x060056DA RID: 22234 RVA: 0x001609C1 File Offset: 0x0015EBC1
			private void OnBossGroupDefeatedServer(BossGroup bossGroup)
			{
				if (!MageFastBossAchievement.MageFastBossServerAchievement.BossGroupIsTeleporterBoss(bossGroup))
				{
					return;
				}
				if (!this.expirationTimeStamp.hasPassed)
				{
					base.Grant();
				}
			}

			// Token: 0x040050A9 RID: 20649
			private ToggleAction listenForBossDamage;

			// Token: 0x040050AA RID: 20650
			private ToggleAction listenForBossDefeated;

			// Token: 0x040050AB RID: 20651
			private Run.FixedTimeStamp expirationTimeStamp;
		}
	}
}
