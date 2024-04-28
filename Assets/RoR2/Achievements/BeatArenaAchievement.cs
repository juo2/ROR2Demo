using System;

namespace RoR2.Achievements
{
	// Token: 0x02000E78 RID: 3704
	[RegisterAchievement("BeatArena", "Characters.Croco", null, typeof(BeatArenaAchievement.BeatArenaServerAchievement))]
	public class BeatArenaAchievement : BaseAchievement
	{
		// Token: 0x060054DA RID: 21722 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x060054DB RID: 21723 RVA: 0x0015D5B8 File Offset: 0x0015B7B8
		public override void OnUninstall()
		{
			base.OnUninstall();
		}

		// Token: 0x02000E79 RID: 3705
		private class BeatArenaServerAchievement : BaseServerAchievement
		{
			// Token: 0x060054DD RID: 21725 RVA: 0x0015D5C0 File Offset: 0x0015B7C0
			public override void OnInstall()
			{
				base.OnInstall();
				ArenaMissionController.onBeatArena += this.OnBeatArena;
			}

			// Token: 0x060054DE RID: 21726 RVA: 0x0015D5D9 File Offset: 0x0015B7D9
			public override void OnUninstall()
			{
				ArenaMissionController.onBeatArena -= this.OnBeatArena;
				base.OnInstall();
			}

			// Token: 0x060054DF RID: 21727 RVA: 0x0015D5F2 File Offset: 0x0015B7F2
			private void OnBeatArena()
			{
				base.Grant();
			}
		}
	}
}
