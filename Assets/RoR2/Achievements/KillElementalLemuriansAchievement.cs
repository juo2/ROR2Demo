using System;

namespace RoR2.Achievements
{
	// Token: 0x02000EA7 RID: 3751
	[RegisterAchievement("KillElementalLemurians", "Items.ElementalRings", null, typeof(KillElementalLemuriansAchievement.KillElementalLemuriansServerAchievement))]
	public class KillElementalLemuriansAchievement : BaseAchievement
	{
		// Token: 0x060055A1 RID: 21921 RVA: 0x0015D5A9 File Offset: 0x0015B7A9
		public override void OnInstall()
		{
			base.OnInstall();
			base.SetServerTracked(true);
		}

		// Token: 0x02000EA8 RID: 3752
		private class KillElementalLemuriansServerAchievement : BaseServerAchievement
		{
		}
	}
}
