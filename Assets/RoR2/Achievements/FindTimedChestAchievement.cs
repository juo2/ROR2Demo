using System;
using EntityStates.TimedChest;
using UnityEngine;

namespace RoR2.Achievements
{
	// Token: 0x02000E9C RID: 3740
	[RegisterAchievement("FindTimedChest", "Items.BFG", null, typeof(FindTimedChestAchievement.FindTimedChestServerAchievement))]
	public class FindTimedChestAchievement : BaseAchievement
	{
		// Token: 0x0600556E RID: 21870 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x0600556F RID: 21871 RVA: 0x0015D5B8 File Offset: 0x0015B7B8
		public override void OnUninstall()
		{
			base.OnUninstall();
		}

		// Token: 0x02000E9D RID: 3741
		private class FindTimedChestServerAchievement : BaseServerAchievement
		{
			// Token: 0x06005571 RID: 21873 RVA: 0x0015E346 File Offset: 0x0015C546
			public override void OnInstall()
			{
				base.OnInstall();
				Debug.Log("subscribed");
				Opening.onOpened += this.OnOpened;
			}

			// Token: 0x06005572 RID: 21874 RVA: 0x0015E369 File Offset: 0x0015C569
			public override void OnUninstall()
			{
				base.OnInstall();
				Opening.onOpened -= this.OnOpened;
			}

			// Token: 0x06005573 RID: 21875 RVA: 0x0015E382 File Offset: 0x0015C582
			private void OnOpened()
			{
				Debug.Log("grant");
				base.Grant();
			}
		}
	}
}
