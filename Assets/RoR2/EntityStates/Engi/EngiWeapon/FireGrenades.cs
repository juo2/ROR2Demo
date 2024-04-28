using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Engi.EngiWeapon
{
	// Token: 0x020003A8 RID: 936
	public class FireGrenades : BaseState
	{
		// Token: 0x060010D0 RID: 4304 RVA: 0x000497C0 File Offset: 0x000479C0
		private void FireGrenade(string targetMuzzle)
		{
			Util.PlaySound(FireGrenades.attackSoundString, base.gameObject);
			this.projectileRay = base.GetAimRay();
			if (this.modelTransform)
			{
				ChildLocator component = this.modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild(targetMuzzle);
					if (transform)
					{
						this.projectileRay.origin = transform.position;
					}
				}
			}
			base.AddRecoil(-1f * FireGrenades.recoilAmplitude, -2f * FireGrenades.recoilAmplitude, -1f * FireGrenades.recoilAmplitude, 1f * FireGrenades.recoilAmplitude);
			if (FireGrenades.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireGrenades.effectPrefab, base.gameObject, targetMuzzle, false);
			}
			if (base.isAuthority)
			{
				float x = UnityEngine.Random.Range(0f, base.characterBody.spreadBloomAngle);
				float z = UnityEngine.Random.Range(0f, 360f);
				Vector3 up = Vector3.up;
				Vector3 axis = Vector3.Cross(up, this.projectileRay.direction);
				Vector3 vector = Quaternion.Euler(0f, 0f, z) * (Quaternion.Euler(x, 0f, 0f) * Vector3.forward);
				float y = vector.y;
				vector.y = 0f;
				float angle = Mathf.Atan2(vector.z, vector.x) * 57.29578f - 90f;
				float angle2 = Mathf.Atan2(y, vector.magnitude) * 57.29578f + FireGrenades.arcAngle;
				Vector3 forward = Quaternion.AngleAxis(angle, up) * (Quaternion.AngleAxis(angle2, axis) * this.projectileRay.direction);
				ProjectileManager.instance.FireProjectile(FireGrenades.projectilePrefab, this.projectileRay.origin, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * FireGrenades.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
			base.characterBody.AddSpreadBloom(FireGrenades.spreadBloomValue);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x000499D5 File Offset: 0x00047BD5
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireGrenades.baseDuration / this.attackSpeedStat;
			this.modelTransform = base.GetModelTransform();
			base.StartAimMode(2f, false);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00049A08 File Offset: 0x00047C08
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.fireTimer -= Time.fixedDeltaTime;
			float num = FireGrenades.fireDuration / this.attackSpeedStat / (float)this.grenadeCountMax;
			if (this.fireTimer <= 0f && this.grenadeCount < this.grenadeCountMax)
			{
				this.fireTimer += num;
				if (this.grenadeCount % 2 == 0)
				{
					this.FireGrenade("MuzzleLeft");
					base.PlayCrossfade("Gesture Left Cannon, Additive", "FireGrenadeLeft", 0.1f);
				}
				else
				{
					this.FireGrenade("MuzzleRight");
					base.PlayCrossfade("Gesture Right Cannon, Additive", "FireGrenadeRight", 0.1f);
				}
				this.grenadeCount++;
			}
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001523 RID: 5411
		public static GameObject effectPrefab;

		// Token: 0x04001524 RID: 5412
		public static GameObject projectilePrefab;

		// Token: 0x04001525 RID: 5413
		public int grenadeCountMax = 3;

		// Token: 0x04001526 RID: 5414
		public static float damageCoefficient;

		// Token: 0x04001527 RID: 5415
		public static float fireDuration = 1f;

		// Token: 0x04001528 RID: 5416
		public static float baseDuration = 2f;

		// Token: 0x04001529 RID: 5417
		public static float arcAngle = 5f;

		// Token: 0x0400152A RID: 5418
		public static float recoilAmplitude = 1f;

		// Token: 0x0400152B RID: 5419
		public static string attackSoundString;

		// Token: 0x0400152C RID: 5420
		public static float spreadBloomValue = 0.3f;

		// Token: 0x0400152D RID: 5421
		private Ray projectileRay;

		// Token: 0x0400152E RID: 5422
		private Transform modelTransform;

		// Token: 0x0400152F RID: 5423
		private float duration;

		// Token: 0x04001530 RID: 5424
		private float fireTimer;

		// Token: 0x04001531 RID: 5425
		private int grenadeCount;
	}
}
