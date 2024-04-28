using System;

namespace RoR2.Achievements.Artifacts
{
	// Token: 0x02000F0A RID: 3850
	public abstract class BaseObtainArtifactAchievement : BaseAchievement
	{
		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x0600578F RID: 22415
		protected abstract ArtifactDef artifactDef { get; }

		// Token: 0x06005790 RID: 22416 RVA: 0x00161AD0 File Offset: 0x0015FCD0
		public override void OnInstall()
		{
			base.OnInstall();
			this.unlockableDef = this.artifactDef.unlockableDef;
			UserProfile.onUnlockableGranted += this.OnUnlockableGranted;
			if (this.unlockableDef != null && base.userProfile.HasUnlockable(this.unlockableDef))
			{
				base.Grant();
			}
		}

		// Token: 0x06005791 RID: 22417 RVA: 0x00161B26 File Offset: 0x0015FD26
		public override void OnUninstall()
		{
			UserProfile.onUnlockableGranted -= this.OnUnlockableGranted;
			this.unlockableDef = null;
			base.OnUninstall();
		}

		// Token: 0x06005792 RID: 22418 RVA: 0x00161B46 File Offset: 0x0015FD46
		private void OnUnlockableGranted(UserProfile userProfile, UnlockableDef unlockableDef)
		{
			if (unlockableDef == this.unlockableDef)
			{
				base.Grant();
			}
		}

		// Token: 0x040050D1 RID: 20689
		private UnlockableDef unlockableDef;
	}
}
