using System;
using RoR2;

namespace EntityStates.GrandParentSun
{
	// Token: 0x0200034E RID: 846
	public class GrandParentSunMain : GrandParentSunBase
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool shouldEnableSunController
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x00041555 File Offset: 0x0003F755
		protected override float desiredVfxScale
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0004155C File Offset: 0x0003F75C
		public override void OnEnter()
		{
			base.OnEnter();
			this.ownership = base.GetComponent<GenericOwnership>();
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00041570 File Offset: 0x0003F770
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && !this.ownership.ownerObject)
			{
				this.outer.SetNextState(new GrandParentSunDeath());
			}
		}

		// Token: 0x040012F0 RID: 4848
		private GenericOwnership ownership;
	}
}
