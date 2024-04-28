using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Commando.CommandoWeapon
{
	// Token: 0x020003F2 RID: 1010
	public class FireThermite : BaseState
	{
		// Token: 0x0600122A RID: 4650 RVA: 0x00050EB4 File Offset: 0x0004F0B4
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireThermite.baseDuration / this.attackSpeedStat;
			Ray aimRay = base.GetAimRay();
			base.StartAimMode(aimRay, 2f, false);
			base.PlayAnimation("Gesture", "FireFMJ", "FireFMJ.playbackRate", this.duration);
			string muzzleName = "MuzzleCenter";
			if (FireThermite.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireThermite.effectPrefab, base.gameObject, muzzleName, false);
			}
			if (base.isAuthority)
			{
				ProjectileManager.instance.FireProjectile(FireThermite.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * FireThermite.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
			if (base.characterMotor && !base.characterMotor.isGrounded)
			{
				Vector3 vector = -aimRay.direction * FireThermite.selfForce;
				vector.y *= 0.5f;
				base.characterMotor.ApplyForce(vector, true, false);
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00050FD9 File Offset: 0x0004F1D9
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x0400174B RID: 5963
		public static GameObject effectPrefab;

		// Token: 0x0400174C RID: 5964
		public static GameObject projectilePrefab;

		// Token: 0x0400174D RID: 5965
		public static float damageCoefficient;

		// Token: 0x0400174E RID: 5966
		public static float force;

		// Token: 0x0400174F RID: 5967
		public static float selfForce;

		// Token: 0x04001750 RID: 5968
		public static float baseDuration = 2f;

		// Token: 0x04001751 RID: 5969
		private float duration;

		// Token: 0x04001752 RID: 5970
		public int bulletCountCurrent = 1;
	}
}
