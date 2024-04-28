using System;
using RoR2.Projectile;

namespace EntityStates.Engi.MineDeployer
{
	// Token: 0x02000398 RID: 920
	public class WaitForStick : BaseMineDeployerState
	{
		// Token: 0x06001087 RID: 4231 RVA: 0x00048733 File Offset: 0x00046933
		public override void OnEnter()
		{
			base.OnEnter();
			this.projectileStickOnImpact = base.GetComponent<ProjectileStickOnImpact>();
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00048747 File Offset: 0x00046947
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && this.projectileStickOnImpact.stuck)
			{
				this.outer.SetNextState(new FireMine());
			}
		}

		// Token: 0x040014E0 RID: 5344
		private ProjectileStickOnImpact projectileStickOnImpact;
	}
}
