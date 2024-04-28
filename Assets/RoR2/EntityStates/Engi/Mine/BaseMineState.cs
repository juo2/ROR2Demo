using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Engi.Mine
{
	// Token: 0x0200039D RID: 925
	public class BaseMineState : BaseState
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x0004883B File Offset: 0x00046A3B
		// (set) Token: 0x06001092 RID: 4242 RVA: 0x00048843 File Offset: 0x00046A43
		private protected ProjectileStickOnImpact projectileStickOnImpact { protected get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x0004884C File Offset: 0x00046A4C
		// (set) Token: 0x06001094 RID: 4244 RVA: 0x00048854 File Offset: 0x00046A54
		private protected EntityStateMachine armingStateMachine { protected get; private set; }

		// Token: 0x06001095 RID: 4245 RVA: 0x00048860 File Offset: 0x00046A60
		public override void OnEnter()
		{
			base.OnEnter();
			this.projectileStickOnImpact = base.GetComponent<ProjectileStickOnImpact>();
			this.armingStateMachine = EntityStateMachine.FindByCustomName(base.gameObject, "Arming");
			if (this.projectileStickOnImpact.enabled != this.shouldStick)
			{
				this.projectileStickOnImpact.enabled = this.shouldStick;
			}
			Util.PlaySound(this.enterSoundString, base.gameObject);
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x000488CB File Offset: 0x00046ACB
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && this.shouldRevertToWaitForStickOnSurfaceLost && !this.projectileStickOnImpact.stuck)
			{
				this.outer.SetNextState(new WaitForStick());
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected virtual bool shouldStick
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected virtual bool shouldRevertToWaitForStickOnSurfaceLost
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040014EC RID: 5356
		[SerializeField]
		public string enterSoundString;
	}
}
