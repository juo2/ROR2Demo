using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Pot.PotWeapon
{
	// Token: 0x02000221 RID: 545
	public class FireCannon : BaseState
	{
		// Token: 0x06000996 RID: 2454 RVA: 0x00027680 File Offset: 0x00025880
		private void FireBullet(string targetMuzzle)
		{
			this.aimRay = base.GetAimRay();
			if (this.modelTransform)
			{
				ChildLocator component = this.modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild(targetMuzzle);
					if (transform)
					{
						base.rigidbody.AddForceAtPosition(transform.forward * FireCannon.selfForce, transform.position, ForceMode.Impulse);
					}
				}
			}
			if (FireCannon.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireCannon.effectPrefab, base.gameObject, targetMuzzle, false);
			}
			if (base.isAuthority)
			{
				float x = UnityEngine.Random.Range(FireCannon.minSpread, FireCannon.maxSpread);
				float z = UnityEngine.Random.Range(0f, 360f);
				Vector3 up = Vector3.up;
				Vector3 axis = Vector3.Cross(up, this.aimRay.direction);
				Vector3 vector = Quaternion.Euler(0f, 0f, z) * (Quaternion.Euler(x, 0f, 0f) * Vector3.forward);
				float y = vector.y;
				vector.y = 0f;
				float angle = Mathf.Atan2(vector.z, vector.x) * 57.29578f - 90f;
				float angle2 = Mathf.Atan2(y, vector.magnitude) * 57.29578f + FireCannon.arcAngle;
				Vector3 forward = Quaternion.AngleAxis(angle, up) * (Quaternion.AngleAxis(angle2, axis) * this.aimRay.direction);
				ProjectileManager.instance.FireProjectile(FireCannon.projectilePrefab, this.aimRay.origin, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * FireCannon.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00027850 File Offset: 0x00025A50
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireCannon.baseDuration / this.attackSpeedStat;
			this.modelTransform = base.GetModelTransform();
			this.aimRay = base.GetAimRay();
			base.StartAimMode(this.aimRay, 2f, false);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000278A0 File Offset: 0x00025AA0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				this.fireTimer -= Time.fixedDeltaTime;
				float num = FireCannon.fireDuration / this.attackSpeedStat / (float)FireCannon.grenadeCountMax;
				if (this.fireTimer <= 0f && this.grenadeCount < FireCannon.grenadeCountMax)
				{
					this.fireTimer += num;
					if (this.grenadeCount % 2 == 0)
					{
						this.FireBullet("MuzzleLeft");
						base.PlayCrossfade("Gesture, Left Cannon", "FireGrenadeLeft", 0.1f);
					}
					else
					{
						this.FireBullet("MuzzleRight");
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

		// Token: 0x0600099A RID: 2458 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000B1B RID: 2843
		public static GameObject effectPrefab;

		// Token: 0x04000B1C RID: 2844
		public static GameObject hitEffectPrefab;

		// Token: 0x04000B1D RID: 2845
		public static GameObject projectilePrefab;

		// Token: 0x04000B1E RID: 2846
		public static float selfForce = 1000f;

		// Token: 0x04000B1F RID: 2847
		public static int grenadeCountMax = 3;

		// Token: 0x04000B20 RID: 2848
		public static float damageCoefficient;

		// Token: 0x04000B21 RID: 2849
		public static float fireDuration = 1f;

		// Token: 0x04000B22 RID: 2850
		public static float baseDuration = 2f;

		// Token: 0x04000B23 RID: 2851
		public static float minSpread = 0f;

		// Token: 0x04000B24 RID: 2852
		public static float maxSpread = 5f;

		// Token: 0x04000B25 RID: 2853
		public static float arcAngle = 5f;

		// Token: 0x04000B26 RID: 2854
		private Ray aimRay;

		// Token: 0x04000B27 RID: 2855
		private Transform modelTransform;

		// Token: 0x04000B28 RID: 2856
		private float duration;

		// Token: 0x04000B29 RID: 2857
		private float fireTimer;

		// Token: 0x04000B2A RID: 2858
		private int grenadeCount;
	}
}
