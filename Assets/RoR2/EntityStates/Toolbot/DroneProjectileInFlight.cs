using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace EntityStates.Toolbot
{
	// Token: 0x0200019F RID: 415
	public class DroneProjectileInFlight : BaseState
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x0001FFEC File Offset: 0x0001E1EC
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				this.impactEventCaller = base.GetComponent<ProjectileImpactEventCaller>();
				if (this.impactEventCaller)
				{
					this.impactEventCaller.impactEvent.AddListener(new UnityAction<ProjectileImpactInfo>(this.OnImpact));
				}
				this.projectileSimple = base.GetComponent<ProjectileSimple>();
				this.projectileFuse = base.GetComponent<ProjectileFuse>();
				if (this.projectileFuse)
				{
					this.projectileFuse.onFuse.AddListener(new UnityAction(this.OnFuse));
				}
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0002007C File Offset: 0x0001E27C
		public override void OnExit()
		{
			if (this.impactEventCaller)
			{
				this.impactEventCaller.impactEvent.RemoveListener(new UnityAction<ProjectileImpactInfo>(this.OnImpact));
			}
			if (this.projectileFuse)
			{
				this.projectileFuse.onFuse.RemoveListener(new UnityAction(this.OnFuse));
			}
			base.OnEnter();
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000200E1 File Offset: 0x0001E2E1
		private void OnImpact(ProjectileImpactInfo projectileImpactInfo)
		{
			this.Advance();
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000200E1 File Offset: 0x0001E2E1
		private void OnFuse()
		{
			this.Advance();
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000200EC File Offset: 0x0001E2EC
		private void Advance()
		{
			if (NetworkServer.active)
			{
				if (this.projectileSimple)
				{
					this.projectileSimple.velocity = 0f;
					this.projectileSimple.enabled = false;
				}
				if (base.rigidbody)
				{
					base.rigidbody.velocity = new Vector3(0f, Trajectory.CalculateInitialYSpeedForFlightDuration(DroneProjectilePrepHover.duration), 0f);
				}
			}
			if (base.isAuthority)
			{
				this.outer.SetNextState(new DroneProjectilePrepHover());
			}
		}

		// Token: 0x0400090A RID: 2314
		private ProjectileImpactEventCaller impactEventCaller;

		// Token: 0x0400090B RID: 2315
		private ProjectileSimple projectileSimple;

		// Token: 0x0400090C RID: 2316
		private ProjectileFuse projectileFuse;
	}
}
