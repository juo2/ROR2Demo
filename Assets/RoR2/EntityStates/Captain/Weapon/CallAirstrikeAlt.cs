using System;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Captain.Weapon
{
	// Token: 0x02000423 RID: 1059
	public class CallAirstrikeAlt : CallAirstrikeBase
	{
		// Token: 0x06001309 RID: 4873 RVA: 0x00054D2C File Offset: 0x00052F2C
		public override void OnExit()
		{
			this.PlayAnimation("Gesture, Override", "CallAirstrike3");
			this.PlayAnimation("Gesture, Additive", "CallAirstrike3");
			base.AddRecoil(-2f, -2f, -0.5f, 0.5f);
			base.OnExit();
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00054D79 File Offset: 0x00052F79
		protected override void ModifyProjectile(ref FireProjectileInfo fireProjectileInfo)
		{
			base.ModifyProjectile(ref fireProjectileInfo);
			fireProjectileInfo.force = this.projectileForce;
		}

		// Token: 0x0400186B RID: 6251
		[SerializeField]
		public float projectileForce;
	}
}
