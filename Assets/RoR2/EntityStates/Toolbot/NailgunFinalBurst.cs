using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Toolbot
{
	// Token: 0x0200019A RID: 410
	public class NailgunFinalBurst : BaseNailgunState
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x0001F3C6 File Offset: 0x0001D5C6
		protected override float GetBaseDuration()
		{
			return (float)NailgunFinalBurst.finalBurstBulletCount * FireNailgun.baseRefireInterval * NailgunFinalBurst.burstTimeCostCoefficient;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001F3DC File Offset: 0x0001D5DC
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.characterBody)
			{
				base.characterBody.SetSpreadBloom(1f, false);
			}
			Ray aimRay = base.GetAimRay();
			base.FireBullet(base.GetAimRay(), NailgunFinalBurst.finalBurstBulletCount, BaseNailgunState.spreadPitchScale, BaseNailgunState.spreadYawScale);
			if (!base.isInDualWield)
			{
				base.PlayAnimation("Gesture, Additive", "FireGrenadeLauncher", "FireGrenadeLauncher.playbackRate", 0.45f / this.attackSpeedStat);
			}
			else
			{
				BaseToolbotPrimarySkillStateMethods.PlayGenericFireAnim<NailgunFinalBurst>(this, base.gameObject, base.skillLocator, 0.45f / this.attackSpeedStat);
			}
			Util.PlaySound(NailgunFinalBurst.burstSound, base.gameObject);
			if (base.isAuthority)
			{
				float num = NailgunFinalBurst.selfForce * (base.characterMotor.isGrounded ? 0.5f : 1f) * base.characterMotor.mass;
				base.characterMotor.ApplyForce(aimRay.direction * -num, false, false);
			}
			Util.PlaySound(BaseNailgunState.fireSoundString, base.gameObject);
			Util.PlaySound(BaseNailgunState.fireSoundString, base.gameObject);
			Util.PlaySound(BaseNailgunState.fireSoundString, base.gameObject);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001F50B File Offset: 0x0001D70B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040008E0 RID: 2272
		public static int finalBurstBulletCount = 8;

		// Token: 0x040008E1 RID: 2273
		public static float burstTimeCostCoefficient = 1.2f;

		// Token: 0x040008E2 RID: 2274
		public static string burstSound;

		// Token: 0x040008E3 RID: 2275
		public static float selfForce = 1000f;
	}
}
