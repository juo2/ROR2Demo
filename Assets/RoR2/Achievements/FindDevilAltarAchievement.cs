using System;
using EntityStates.Destructible;

namespace RoR2.Achievements
{
	// Token: 0x02000E9B RID: 3739
	[RegisterAchievement("FindDevilAltar", "Items.NovaOnHeal", null, null)]
	public class FindDevilAltarAchievement : BaseAchievement
	{
		// Token: 0x0600556A RID: 21866 RVA: 0x0015E30C File Offset: 0x0015C50C
		public override void OnInstall()
		{
			base.OnInstall();
			AltarSkeletonDeath.onDeath += this.OnDeath;
		}

		// Token: 0x0600556B RID: 21867 RVA: 0x0015E325 File Offset: 0x0015C525
		public override void OnUninstall()
		{
			base.OnUninstall();
			AltarSkeletonDeath.onDeath -= this.OnDeath;
		}

		// Token: 0x0600556C RID: 21868 RVA: 0x0015E33E File Offset: 0x0015C53E
		private void OnDeath()
		{
			base.Grant();
		}
	}
}
