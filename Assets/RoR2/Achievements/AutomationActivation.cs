using System;
using JetBrains.Annotations;
using RoR2.Stats;

namespace RoR2.Achievements
{
	// Token: 0x02000E73 RID: 3699
	[RegisterAchievement("AutomationActivation", "Items.Squid", null, typeof(AutomationActivation))]
	public class AutomationActivation : BaseAchievement
	{
		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060054A9 RID: 21673 RVA: 0x0015CFD6 File Offset: 0x0015B1D6
		private static StatDef statDef
		{
			get
			{
				return StatDef.totalTurretsPurchased;
			}
		}

		// Token: 0x060054AA RID: 21674 RVA: 0x0015CFDD File Offset: 0x0015B1DD
		public override void OnInstall()
		{
			base.OnInstall();
			this.owner.onRunStatsUpdated += this.OnRunStatsUpdated;
		}

		// Token: 0x060054AB RID: 21675 RVA: 0x0015CFFC File Offset: 0x0015B1FC
		public override void OnUninstall()
		{
			this.owner.onRunStatsUpdated -= this.OnRunStatsUpdated;
			base.OnUninstall();
		}

		// Token: 0x060054AC RID: 21676 RVA: 0x0015D01B File Offset: 0x0015B21B
		private void OnRunStatsUpdated([NotNull] StatSheet runStatSheet)
		{
			if (runStatSheet.GetStatValueULong(AutomationActivation.statDef) >= AutomationActivation.requirement)
			{
				base.Grant();
			}
		}

		// Token: 0x0400503B RID: 20539
		private static readonly ulong requirement = 6UL;
	}
}
