using System;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.GrandParentSun
{
	// Token: 0x0200034D RID: 845
	public class GrandParentSunDeath : GrandParentSunBase
	{
		// Token: 0x06000F25 RID: 3877 RVA: 0x000414F8 File Offset: 0x0003F6F8
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = GrandParentSunDeath.baseDuration;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0004150B File Offset: 0x0003F70B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge >= this.duration)
			{
				NetworkServer.Destroy(base.gameObject);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x00041533 File Offset: 0x0003F733
		protected override float desiredVfxScale
		{
			get
			{
				return 1f - Mathf.Clamp01(base.age / this.duration);
			}
		}

		// Token: 0x040012EE RID: 4846
		public static float baseDuration;

		// Token: 0x040012EF RID: 4847
		private float duration;
	}
}
