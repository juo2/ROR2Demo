using System;
using System.Collections.Generic;

namespace RoR2.Achievements.Engi
{
	// Token: 0x02000EF3 RID: 3827
	[RegisterAchievement("EngiClearTeleporterWithZeroMonsters", "Skills.Engi.Harpoon", "Complete30StagesCareer", null)]
	public class EngiClearTeleporterWithZeroMonstersAchievement : BaseAchievement
	{
		// Token: 0x0600572B RID: 22315 RVA: 0x00160FED File Offset: 0x0015F1ED
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("EngiBody");
		}

		// Token: 0x0600572C RID: 22316 RVA: 0x001610DC File Offset: 0x0015F2DC
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			TeleporterInteraction.onTeleporterChargedGlobal += this.OnTeleporterChargedGlobal;
		}

		// Token: 0x0600572D RID: 22317 RVA: 0x001610F5 File Offset: 0x0015F2F5
		protected override void OnBodyRequirementBroken()
		{
			TeleporterInteraction.onTeleporterChargedGlobal -= this.OnTeleporterChargedGlobal;
			base.OnBodyRequirementBroken();
		}

		// Token: 0x0600572E RID: 22318 RVA: 0x00161110 File Offset: 0x0015F310
		private void OnTeleporterChargedGlobal(TeleporterInteraction teleporterInteraction)
		{
			if (!base.isUserAlive)
			{
				return;
			}
			using (IEnumerator<TeamComponent> enumerator = TeamComponent.GetTeamMembers(TeamIndex.Monster).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.body.healthComponent.alive)
					{
						return;
					}
				}
			}
			base.Grant();
		}
	}
}
