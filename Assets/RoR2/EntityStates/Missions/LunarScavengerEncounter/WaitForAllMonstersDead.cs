using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.Missions.LunarScavengerEncounter
{
	// Token: 0x02000249 RID: 585
	public class WaitForAllMonstersDead : BaseState
	{
		// Token: 0x06000A56 RID: 2646 RVA: 0x0002AE12 File Offset: 0x00029012
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				this.FixedUpdateServer();
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002AE27 File Offset: 0x00029027
		private void FixedUpdateServer()
		{
			if (TeamComponent.GetTeamMembers(TeamIndex.Monster).Count == 0)
			{
				this.outer.SetNextState(new FadeOut());
			}
		}
	}
}
