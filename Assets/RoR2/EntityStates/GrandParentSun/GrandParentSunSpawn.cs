using System;
using UnityEngine;

namespace EntityStates.GrandParentSun
{
	// Token: 0x0200034F RID: 847
	public class GrandParentSunSpawn : GrandParentSunBase
	{
		// Token: 0x06000F2E RID: 3886 RVA: 0x000415A2 File Offset: 0x0003F7A2
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = GrandParentSunSpawn.baseDuration;
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x000415B5 File Offset: 0x0003F7B5
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new GrandParentSunMain());
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x000415E3 File Offset: 0x0003F7E3
		protected override float desiredVfxScale
		{
			get
			{
				return Mathf.Clamp01(base.age / this.duration);
			}
		}

		// Token: 0x040012F1 RID: 4849
		public static float baseDuration;

		// Token: 0x040012F2 RID: 4850
		private float duration;
	}
}
