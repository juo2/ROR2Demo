using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.BeetleQueenMonster
{
	// Token: 0x02000466 RID: 1126
	public class FireSpit : BaseState
	{
		// Token: 0x0600141B RID: 5147 RVA: 0x000598D4 File Offset: 0x00057AD4
		public override void OnEnter()
		{
			base.OnEnter();
			string muzzleName = "Mouth";
			this.duration = FireSpit.baseDuration / this.attackSpeedStat;
			if (FireSpit.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireSpit.effectPrefab, base.gameObject, muzzleName, false);
			}
			base.PlayCrossfade("Gesture", "FireSpit", "FireSpit.playbackRate", this.duration, 0.1f);
			this.aimRay = base.GetAimRay();
			float magnitude = FireSpit.projectileHSpeed;
			Ray ray = this.aimRay;
			ray.origin = this.aimRay.GetPoint(6f);
			RaycastHit raycastHit;
			if (Util.CharacterRaycast(base.gameObject, ray, out raycastHit, float.PositiveInfinity, LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.Ignore))
			{
				float num = magnitude;
				Vector3 vector = raycastHit.point - this.aimRay.origin;
				Vector2 vector2 = new Vector2(vector.x, vector.z);
				float magnitude2 = vector2.magnitude;
				float y = Trajectory.CalculateInitialYSpeed(magnitude2 / num, vector.y);
				Vector3 a = new Vector3(vector2.x / magnitude2 * num, y, vector2.y / magnitude2 * num);
				magnitude = a.magnitude;
				this.aimRay.direction = a / magnitude;
			}
			EffectManager.SimpleMuzzleFlash(FireSpit.effectPrefab, base.gameObject, muzzleName, false);
			if (base.isAuthority)
			{
				for (int i = 0; i < FireSpit.projectileCount; i++)
				{
					this.FireBlob(this.aimRay, 0f, ((float)FireSpit.projectileCount / 2f - (float)i) * FireSpit.yawSpread, magnitude);
				}
			}
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00059A94 File Offset: 0x00057C94
		private void FireBlob(Ray aimRay, float bonusPitch, float bonusYaw, float speed)
		{
			Vector3 forward = Util.ApplySpread(aimRay.direction, FireSpit.minSpread, FireSpit.maxSpread, 1f, 1f, bonusYaw, bonusPitch);
			ProjectileManager.instance.FireProjectile(FireSpit.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * FireSpit.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, speed);
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00059B11 File Offset: 0x00057D11
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040019C5 RID: 6597
		public static GameObject projectilePrefab;

		// Token: 0x040019C6 RID: 6598
		public static GameObject effectPrefab;

		// Token: 0x040019C7 RID: 6599
		public static float baseDuration = 2f;

		// Token: 0x040019C8 RID: 6600
		public static float damageCoefficient = 1.2f;

		// Token: 0x040019C9 RID: 6601
		public static float force = 20f;

		// Token: 0x040019CA RID: 6602
		public static int projectileCount = 3;

		// Token: 0x040019CB RID: 6603
		public static float yawSpread = 5f;

		// Token: 0x040019CC RID: 6604
		public static float minSpread = 0f;

		// Token: 0x040019CD RID: 6605
		public static float maxSpread = 5f;

		// Token: 0x040019CE RID: 6606
		public static float arcAngle = 5f;

		// Token: 0x040019CF RID: 6607
		public static float projectileHSpeed = 50f;

		// Token: 0x040019D0 RID: 6608
		private Ray aimRay;

		// Token: 0x040019D1 RID: 6609
		private float duration;
	}
}
