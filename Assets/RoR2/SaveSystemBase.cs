using System;
using Zio;

namespace RoR2
{
	// Token: 0x020009C3 RID: 2499
	public abstract class SaveSystemBase
	{
		// Token: 0x0600391A RID: 14618
		protected abstract void ProcessFileOutputQueue();

		// Token: 0x0600391B RID: 14619
		protected abstract void StartSave(UserProfile userProfile, bool blocking);

		// Token: 0x0600391C RID: 14620
		protected abstract LoadUserProfileOperationResult LoadUserProfileFromDisk(IFileSystem fileSystem, UPath path);

		// Token: 0x0600391D RID: 14621
		public abstract UserProfile LoadPrimaryProfile();

		// Token: 0x0600391E RID: 14622
		public abstract string GetPlatformUsernameOrDefault(string defaultName);
	}
}
