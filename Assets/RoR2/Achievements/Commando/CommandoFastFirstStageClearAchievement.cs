using System;

namespace RoR2.Achievements.Commando
{
	// Token: 0x02000EFE RID: 3838
	[RegisterAchievement("CommandoFastFirstStageClear", "Skills.Commando.SlideJet", null, null)]
	public class CommandoFastFirstStageClearAchievement : BaseAchievement
	{
		// Token: 0x06005754 RID: 22356 RVA: 0x001613DC File Offset: 0x0015F5DC
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("CommandoBody");
		}

		// Token: 0x06005755 RID: 22357 RVA: 0x001613E8 File Offset: 0x0015F5E8
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			TeleporterInteraction.onTeleporterChargedGlobal += this.OnTeleporterChargedGlobal;
		}

		// Token: 0x06005756 RID: 22358 RVA: 0x00161401 File Offset: 0x0015F601
		private void OnTeleporterChargedGlobal(TeleporterInteraction teleporterInteraction)
		{
			if (Run.instance.GetRunStopwatch() < CommandoFastFirstStageClearAchievement.timeRequirement && Run.instance.stageClearCount == 0 && base.isUserAlive)
			{
				base.Grant();
			}
		}

		// Token: 0x06005757 RID: 22359 RVA: 0x0016142E File Offset: 0x0015F62E
		protected override void OnBodyRequirementBroken()
		{
			TeleporterInteraction.onTeleporterChargedGlobal -= this.OnTeleporterChargedGlobal;
			base.OnBodyRequirementBroken();
		}

		// Token: 0x040050C2 RID: 20674
		private static readonly float timeRequirement = 300f;
	}
}
