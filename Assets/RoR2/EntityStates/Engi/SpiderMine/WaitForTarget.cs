using System;
using RoR2.Projectile;
using UnityEngine.Networking;

namespace EntityStates.Engi.SpiderMine
{
	// Token: 0x0200038F RID: 911
	public class WaitForTarget : BaseSpiderMineState
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x00047C8D File Offset: 0x00045E8D
		// (set) Token: 0x06001059 RID: 4185 RVA: 0x00047C95 File Offset: 0x00045E95
		private protected ProjectileSphereTargetFinder targetFinder { protected get; private set; }

		// Token: 0x0600105A RID: 4186 RVA: 0x00047C9E File Offset: 0x00045E9E
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				this.targetFinder = base.GetComponent<ProjectileSphereTargetFinder>();
				this.targetFinder.enabled = true;
			}
			this.PlayAnimation("Base", "Armed");
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00047CD8 File Offset: 0x00045ED8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				EntityState entityState = null;
				if (!base.projectileStickOnImpact.stuck)
				{
					entityState = new WaitForStick();
				}
				else if (base.projectileTargetComponent.target)
				{
					entityState = new Unburrow();
				}
				if (entityState != null)
				{
					this.outer.SetNextState(entityState);
				}
			}
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00047D31 File Offset: 0x00045F31
		public override void OnExit()
		{
			if (this.targetFinder)
			{
				this.targetFinder.enabled = false;
			}
			base.OnExit();
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool shouldStick
		{
			get
			{
				return true;
			}
		}
	}
}
