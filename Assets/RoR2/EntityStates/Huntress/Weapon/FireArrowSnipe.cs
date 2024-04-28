using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Huntress.Weapon
{
	// Token: 0x0200031D RID: 797
	public class FireArrowSnipe : GenericBulletBaseState
	{
		// Token: 0x06000E3D RID: 3645 RVA: 0x0003D294 File Offset: 0x0003B494
		protected override void ModifyBullet(BulletAttack bulletAttack)
		{
			base.ModifyBullet(bulletAttack);
			bulletAttack.falloffModel = BulletAttack.FalloffModel.None;
			base.skillLocator ? base.skillLocator.primary : null;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0003D2C8 File Offset: 0x0003B4C8
		protected override void FireBullet(Ray aimRay)
		{
			base.FireBullet(aimRay);
			base.characterBody.SetSpreadBloom(0.2f, false);
			base.AddRecoil(-0.6f * FireArrowSnipe.recoilAmplitude, -0.8f * FireArrowSnipe.recoilAmplitude, -0.1f * FireArrowSnipe.recoilAmplitude, 0.1f * FireArrowSnipe.recoilAmplitude);
			base.PlayAnimation("Body", "FireArrowSnipe", "FireArrowSnipe.playbackRate", this.duration);
			base.healthComponent.TakeDamageForce(aimRay.direction * -400f, true, false);
		}

		// Token: 0x040011B6 RID: 4534
		public float charge;

		// Token: 0x040011B7 RID: 4535
		public static float recoilAmplitude;
	}
}
