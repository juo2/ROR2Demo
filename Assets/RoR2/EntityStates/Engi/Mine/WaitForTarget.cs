using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine.Networking;

namespace EntityStates.Engi.Mine
{
	// Token: 0x020003A0 RID: 928
	public class WaitForTarget : BaseMineState
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool shouldStick
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00048980 File Offset: 0x00046B80
		public override void OnEnter()
		{
			base.OnEnter();
			this.projectileTargetComponent = base.GetComponent<ProjectileTargetComponent>();
			this.targetFinder = base.GetComponent<ProjectileSphereTargetFinder>();
			if (NetworkServer.active)
			{
				this.targetFinder.enabled = true;
				base.armingStateMachine.SetNextState(new MineArmingWeak());
			}
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x000489CE File Offset: 0x00046BCE
		public override void OnExit()
		{
			if (this.targetFinder)
			{
				this.targetFinder.enabled = false;
			}
			base.OnExit();
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x000489F0 File Offset: 0x00046BF0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && this.targetFinder)
			{
				if (this.projectileTargetComponent.target)
				{
					this.outer.SetNextState(new PreDetonate());
				}
				EntityStateMachine armingStateMachine = base.armingStateMachine;
				BaseMineArmingState baseMineArmingState;
				if ((baseMineArmingState = (((armingStateMachine != null) ? armingStateMachine.state : null) as BaseMineArmingState)) != null)
				{
					this.targetFinder.enabled = (baseMineArmingState.triggerRadius != 0f);
					this.targetFinder.lookRange = baseMineArmingState.triggerRadius;
				}
			}
		}

		// Token: 0x040014EE RID: 5358
		private ProjectileSphereTargetFinder targetFinder;

		// Token: 0x040014EF RID: 5359
		private ProjectileTargetComponent projectileTargetComponent;

		// Token: 0x040014F0 RID: 5360
		private ProjectileImpactExplosion projectileImpactExplosion;
	}
}
