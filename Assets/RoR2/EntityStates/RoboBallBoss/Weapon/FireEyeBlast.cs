using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.RoboBallBoss.Weapon
{
	// Token: 0x020001E6 RID: 486
	public class FireEyeBlast : BaseState
	{
		// Token: 0x060008AF RID: 2223 RVA: 0x00024AFC File Offset: 0x00022CFC
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = this.baseDuration / this.attackSpeedStat;
			this.fireDuration = this.baseFireDuration / this.attackSpeedStat;
			base.PlayAnimation("Gesture, Additive", "FireEyeBlast", "FireEyeBlast.playbackRate", this.duration);
			Util.PlaySound(FireEyeBlast.attackString, base.gameObject);
			if (base.isAuthority)
			{
				base.healthComponent.TakeDamageForce(base.GetAimRay().direction * FireEyeBlast.selfForce, false, false);
			}
			if (UnityEngine.Random.value <= 0.5f)
			{
				this.projectileSpreadIsYaw = true;
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00024BA4 File Offset: 0x00022DA4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				int num = Mathf.FloorToInt(base.fixedAge / this.fireDuration * (float)this.projectileCount);
				if (this.projectilesFired <= num && this.projectilesFired < this.projectileCount)
				{
					if (FireEyeBlast.muzzleflashEffectPrefab)
					{
						EffectManager.SimpleMuzzleFlash(FireEyeBlast.muzzleflashEffectPrefab, base.gameObject, FireEyeBlast.muzzleString, false);
					}
					Ray aimRay = base.GetAimRay();
					float speedOverride = this.projectileSpeed;
					int num2 = Mathf.FloorToInt((float)this.projectilesFired - (float)(this.projectileCount - 1) / 2f);
					float bonusYaw = 0f;
					float bonusPitch = 0f;
					if (this.projectileSpreadIsYaw)
					{
						bonusYaw = (float)num2 / (float)(this.projectileCount - 1) * this.totalYawSpread;
					}
					else
					{
						bonusPitch = (float)num2 / (float)(this.projectileCount - 1) * this.totalYawSpread;
					}
					Vector3 forward = Util.ApplySpread(aimRay.direction, 0f, 0f, 1f, 1f, bonusYaw, bonusPitch);
					ProjectileManager.instance.FireProjectile(this.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * this.damageCoefficient, FireEyeBlast.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, speedOverride);
					this.projectilesFired++;
				}
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000A38 RID: 2616
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x04000A39 RID: 2617
		public static GameObject muzzleflashEffectPrefab;

		// Token: 0x04000A3A RID: 2618
		[SerializeField]
		public int projectileCount = 3;

		// Token: 0x04000A3B RID: 2619
		[SerializeField]
		public float totalYawSpread = 5f;

		// Token: 0x04000A3C RID: 2620
		[SerializeField]
		public float baseDuration = 2f;

		// Token: 0x04000A3D RID: 2621
		[SerializeField]
		public float baseFireDuration = 0.2f;

		// Token: 0x04000A3E RID: 2622
		[SerializeField]
		public float damageCoefficient = 1.2f;

		// Token: 0x04000A3F RID: 2623
		[SerializeField]
		public float projectileSpeed;

		// Token: 0x04000A40 RID: 2624
		public static float force = 20f;

		// Token: 0x04000A41 RID: 2625
		public static float selfForce;

		// Token: 0x04000A42 RID: 2626
		public static string attackString;

		// Token: 0x04000A43 RID: 2627
		public static string muzzleString;

		// Token: 0x04000A44 RID: 2628
		private float duration;

		// Token: 0x04000A45 RID: 2629
		private float fireDuration;

		// Token: 0x04000A46 RID: 2630
		private int projectilesFired;

		// Token: 0x04000A47 RID: 2631
		private bool projectileSpreadIsYaw;
	}
}
