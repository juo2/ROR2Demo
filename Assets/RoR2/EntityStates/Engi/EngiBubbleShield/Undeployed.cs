using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine.Networking;

namespace EntityStates.Engi.EngiBubbleShield
{
	// Token: 0x020003BB RID: 955
	public class Undeployed : EntityState
	{
		// Token: 0x0600110C RID: 4364 RVA: 0x0004AF40 File Offset: 0x00049140
		public override void OnEnter()
		{
			base.OnEnter();
			ProjectileController component = base.GetComponent<ProjectileController>();
			this.projectileStickOnImpact = base.GetComponent<ProjectileStickOnImpact>();
			if (NetworkServer.active && component.owner)
			{
				CharacterBody component2 = component.owner.GetComponent<CharacterBody>();
				if (component2)
				{
					CharacterMaster master = component2.master;
					if (master)
					{
						master.AddDeployable(base.GetComponent<Deployable>(), DeployableSlot.EngiBubbleShield);
					}
				}
			}
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x0004AFAA File Offset: 0x000491AA
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.projectileStickOnImpact.stuck && NetworkServer.active)
			{
				this.SetNextState();
			}
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0004AFCC File Offset: 0x000491CC
		protected virtual void SetNextState()
		{
			this.outer.SetNextState(new Deployed());
		}

		// Token: 0x04001585 RID: 5509
		private ProjectileStickOnImpact projectileStickOnImpact;
	}
}
