using System;
using UnityEngine.Networking;

namespace RoR2.Achievements.Engi
{
	// Token: 0x02000EF1 RID: 3825
	[RegisterAchievement("EngiArmy", "Skills.Engi.WalkerTurret", "Complete30StagesCareer", null)]
	public class EngiArmyAchievement : BaseAchievement
	{
		// Token: 0x06005720 RID: 22304 RVA: 0x00160FED File Offset: 0x0015F1ED
		protected override BodyIndex LookUpRequiredBodyIndex()
		{
			return BodyCatalog.FindBodyIndex("EngiBody");
		}

		// Token: 0x06005721 RID: 22305 RVA: 0x00160FF9 File Offset: 0x0015F1F9
		private void SubscribeToMinionChanges()
		{
			MinionOwnership.onMinionGroupChangedGlobal += this.OnMinionGroupChangedGlobal;
		}

		// Token: 0x06005722 RID: 22306 RVA: 0x0016100C File Offset: 0x0015F20C
		private void UnsubscribeFromMinionChanges()
		{
			MinionOwnership.onMinionGroupChangedGlobal -= this.OnMinionGroupChangedGlobal;
		}

		// Token: 0x06005723 RID: 22307 RVA: 0x00161020 File Offset: 0x0015F220
		private void OnMinionGroupChangedGlobal(MinionOwnership minion)
		{
			int num = EngiArmyAchievement.requirement;
			MinionOwnership.MinionGroup group = minion.group;
			if (num <= ((group != null) ? group.memberCount : 0))
			{
				CharacterMaster master = base.localUser.cachedMasterController.master;
				if (!master)
				{
					return;
				}
				NetworkInstanceId netId = master.netId;
				if (minion.group.ownerId == netId)
				{
					base.Grant();
				}
			}
		}

		// Token: 0x06005724 RID: 22308 RVA: 0x00161080 File Offset: 0x0015F280
		public override void OnInstall()
		{
			base.OnInstall();
			this.monitorMinions = new ToggleAction(new Action(this.SubscribeToMinionChanges), new Action(this.UnsubscribeFromMinionChanges));
		}

		// Token: 0x06005725 RID: 22309 RVA: 0x001610AB File Offset: 0x0015F2AB
		protected override void OnBodyRequirementMet()
		{
			base.OnBodyRequirementMet();
			this.monitorMinions.SetActive(true);
		}

		// Token: 0x06005726 RID: 22310 RVA: 0x001610BF File Offset: 0x0015F2BF
		protected override void OnBodyRequirementBroken()
		{
			this.monitorMinions.SetActive(false);
			base.OnBodyRequirementBroken();
		}

		// Token: 0x040050BB RID: 20667
		private static readonly int requirement = 12;

		// Token: 0x040050BC RID: 20668
		private ToggleAction monitorMinions;
	}
}
