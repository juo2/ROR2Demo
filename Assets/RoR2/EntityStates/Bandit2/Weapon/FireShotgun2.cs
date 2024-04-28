using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Bandit2.Weapon
{
	// Token: 0x02000479 RID: 1145
	public class FireShotgun2 : Bandit2FirePrimaryBase
	{
		// Token: 0x06001478 RID: 5240 RVA: 0x0005B375 File Offset: 0x00059575
		protected override void ModifyBullet(BulletAttack bulletAttack)
		{
			base.ModifyBullet(bulletAttack);
			bulletAttack.bulletCount = 1U;
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0005B388 File Offset: 0x00059588
		protected override void FireBullet(Ray aimRay)
		{
			base.StartAimMode(aimRay, 3f, false);
			this.DoFireEffects();
			this.PlayFireAnimation();
			base.AddRecoil(-1f * this.recoilAmplitudeY, -1.5f * this.recoilAmplitudeY, -1f * this.recoilAmplitudeX, 1f * this.recoilAmplitudeX);
			if (base.isAuthority)
			{
				Vector3 rhs = Vector3.Cross(Vector3.up, aimRay.direction);
				Vector3 axis = Vector3.Cross(aimRay.direction, rhs);
				float num = 0f;
				if (base.characterBody)
				{
					num = base.characterBody.spreadBloomAngle;
				}
				float angle = 0f;
				float num2 = 0f;
				if (this.bulletCount > 1)
				{
					num2 = UnityEngine.Random.Range(this.minFixedSpreadYaw + num, this.maxFixedSpreadYaw + num) * 2f;
					angle = num2 / (float)(this.bulletCount - 1);
				}
				Vector3 direction = Quaternion.AngleAxis(-num2 * 0.5f, axis) * aimRay.direction;
				Quaternion rotation = Quaternion.AngleAxis(angle, axis);
				Ray aimRay2 = new Ray(aimRay.origin, direction);
				for (int i = 0; i < this.bulletCount; i++)
				{
					BulletAttack bulletAttack = base.GenerateBulletAttack(aimRay2);
					this.ModifyBullet(bulletAttack);
					bulletAttack.Fire();
					aimRay2.direction = rotation * aimRay2.direction;
				}
			}
		}

		// Token: 0x04001A42 RID: 6722
		[SerializeField]
		public float minFixedSpreadYaw;

		// Token: 0x04001A43 RID: 6723
		[SerializeField]
		public float maxFixedSpreadYaw;
	}
}
