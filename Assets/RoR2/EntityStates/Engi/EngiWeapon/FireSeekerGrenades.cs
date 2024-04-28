using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Engi.EngiWeapon
{
	// Token: 0x020003AC RID: 940
	public class FireSeekerGrenades : BaseState
	{
		// Token: 0x060010DF RID: 4319 RVA: 0x00049C78 File Offset: 0x00047E78
		private void FireGrenade(string targetMuzzle)
		{
			Util.PlaySound(FireSeekerGrenades.attackSoundString, base.gameObject);
			this.aimRay = base.GetAimRay();
			if (this.modelTransform)
			{
				ChildLocator component = this.modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild(targetMuzzle);
					if (transform)
					{
						this.aimRay.origin = transform.position;
					}
				}
			}
			base.AddRecoil(-1f * FireSeekerGrenades.recoilAmplitude, -2f * FireSeekerGrenades.recoilAmplitude, -1f * FireSeekerGrenades.recoilAmplitude, 1f * FireSeekerGrenades.recoilAmplitude);
			if (FireSeekerGrenades.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireSeekerGrenades.effectPrefab, base.gameObject, targetMuzzle, false);
			}
			if (base.isAuthority)
			{
				float x = UnityEngine.Random.Range(FireSeekerGrenades.minSpread, FireSeekerGrenades.maxSpread);
				float z = UnityEngine.Random.Range(0f, 360f);
				Vector3 up = Vector3.up;
				Vector3 axis = Vector3.Cross(up, this.aimRay.direction);
				Vector3 vector = Quaternion.Euler(0f, 0f, z) * (Quaternion.Euler(x, 0f, 0f) * Vector3.forward);
				float y = vector.y;
				vector.y = 0f;
				float angle = Mathf.Atan2(vector.z, vector.x) * 57.29578f - 90f;
				float angle2 = Mathf.Atan2(y, vector.magnitude) * 57.29578f + FireSeekerGrenades.arcAngle;
				Vector3 forward = Quaternion.AngleAxis(angle, up) * (Quaternion.AngleAxis(angle2, axis) * this.aimRay.direction);
				ProjectileManager.instance.FireProjectile(FireSeekerGrenades.projectilePrefab, this.aimRay.origin, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * FireSeekerGrenades.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00049E78 File Offset: 0x00048078
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireSeekerGrenades.baseDuration / this.attackSpeedStat;
			this.modelTransform = base.GetModelTransform();
			this.aimRay = base.GetAimRay();
			base.StartAimMode(this.aimRay, 2f, false);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00049EC8 File Offset: 0x000480C8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				this.fireTimer -= Time.fixedDeltaTime;
				float num = FireSeekerGrenades.fireDuration / this.attackSpeedStat / (float)FireSeekerGrenades.grenadeCountMax;
				if (this.fireTimer <= 0f && this.grenadeCount < FireSeekerGrenades.grenadeCountMax)
				{
					this.fireTimer += num;
					if (this.grenadeCount % 2 == 0)
					{
						this.FireGrenade("MuzzleLeft");
						base.PlayCrossfade("Gesture, Left Cannon", "FireGrenadeLeft", 0.1f);
					}
					else
					{
						this.FireGrenade("MuzzleRight");
						base.PlayCrossfade("Gesture, Right Cannon", "FireGrenadeRight", 0.1f);
					}
					this.grenadeCount++;
				}
				if (base.fixedAge >= this.duration)
				{
					this.outer.SetNextStateToMain();
					return;
				}
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x0400153A RID: 5434
		public static GameObject effectPrefab;

		// Token: 0x0400153B RID: 5435
		public static GameObject hitEffectPrefab;

		// Token: 0x0400153C RID: 5436
		public static GameObject projectilePrefab;

		// Token: 0x0400153D RID: 5437
		public static int grenadeCountMax = 3;

		// Token: 0x0400153E RID: 5438
		public static float damageCoefficient;

		// Token: 0x0400153F RID: 5439
		public static float fireDuration = 1f;

		// Token: 0x04001540 RID: 5440
		public static float baseDuration = 2f;

		// Token: 0x04001541 RID: 5441
		public static float minSpread = 0f;

		// Token: 0x04001542 RID: 5442
		public static float maxSpread = 5f;

		// Token: 0x04001543 RID: 5443
		public static float arcAngle = 5f;

		// Token: 0x04001544 RID: 5444
		public static float recoilAmplitude = 1f;

		// Token: 0x04001545 RID: 5445
		public static string attackSoundString;

		// Token: 0x04001546 RID: 5446
		private Ray aimRay;

		// Token: 0x04001547 RID: 5447
		private Transform modelTransform;

		// Token: 0x04001548 RID: 5448
		private float duration;

		// Token: 0x04001549 RID: 5449
		private float fireTimer;

		// Token: 0x0400154A RID: 5450
		private int grenadeCount;
	}
}
