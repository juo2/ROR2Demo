using System;

namespace RoR2.Items
{
	// Token: 0x02000BEC RID: 3052
	public class TeamSizeDamageBonusBodyBehavior : BaseItemBodyBehavior
	{
		// Token: 0x0600453C RID: 17724 RVA: 0x00120512 File Offset: 0x0011E712
		[BaseItemBodyBehavior.ItemDefAssociationAttribute(useOnServer = true, useOnClient = true)]
		private static ItemDef GetItemDef()
		{
			return RoR2Content.Items.TeamSizeDamageBonus;
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x00120519 File Offset: 0x0011E719
		private void OnJoinTeamGlobal(TeamComponent teamComponent, TeamIndex newTeamIndex)
		{
			if (teamComponent == base.body.teamComponent || newTeamIndex == base.body.teamComponent.teamIndex)
			{
				base.body.MarkAllStatsDirty();
			}
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x00120519 File Offset: 0x0011E719
		private void OnLeaveTeamGlobal(TeamComponent teamComponent, TeamIndex oldTeamIndex)
		{
			if (teamComponent == base.body.teamComponent || oldTeamIndex == base.body.teamComponent.teamIndex)
			{
				base.body.MarkAllStatsDirty();
			}
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x00120547 File Offset: 0x0011E747
		private void OnEnable()
		{
			TeamComponent.onJoinTeamGlobal += this.OnJoinTeamGlobal;
			TeamComponent.onLeaveTeamGlobal += this.OnLeaveTeamGlobal;
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x0012056B File Offset: 0x0011E76B
		private void OnDisable()
		{
			TeamComponent.onJoinTeamGlobal -= this.OnJoinTeamGlobal;
			TeamComponent.onLeaveTeamGlobal -= this.OnLeaveTeamGlobal;
		}
	}
}
