using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Drone.DroneWeapon
{
	// Token: 0x020003C8 RID: 968
	public class FireMissileBarrage : BaseState
	{
		// Token: 0x06001145 RID: 4421 RVA: 0x0004C188 File Offset: 0x0004A388
		private void FireMissile(string targetMuzzle)
		{
			this.missileCount++;
			this.PlayAnimation("Gesture, Additive", "FireMissile");
			Ray aimRay = base.GetAimRay();
			if (this.modelTransform)
			{
				ChildLocator component = this.modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					Transform transform = component.FindChild(targetMuzzle);
					if (transform)
					{
						aimRay.origin = transform.position;
					}
				}
			}
			if (FireMissileBarrage.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireMissileBarrage.effectPrefab, base.gameObject, targetMuzzle, false);
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(2f);
			}
			if (base.isAuthority)
			{
				float x = UnityEngine.Random.Range(FireMissileBarrage.minSpread, FireMissileBarrage.maxSpread);
				float z = UnityEngine.Random.Range(0f, 360f);
				Vector3 up = Vector3.up;
				Vector3 axis = Vector3.Cross(up, aimRay.direction);
				Vector3 vector = Quaternion.Euler(0f, 0f, z) * (Quaternion.Euler(x, 0f, 0f) * Vector3.forward);
				float y = vector.y;
				vector.y = 0f;
				float angle = Mathf.Atan2(vector.z, vector.x) * 57.29578f - 90f;
				float angle2 = Mathf.Atan2(y, vector.magnitude) * 57.29578f;
				Vector3 forward = Quaternion.AngleAxis(angle, up) * (Quaternion.AngleAxis(angle2, axis) * aimRay.direction);
				ProjectileManager.instance.FireProjectile(FireMissileBarrage.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * FireMissileBarrage.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0004C366 File Offset: 0x0004A566
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelTransform = base.GetModelTransform();
			this.fireInterval = FireMissileBarrage.baseFireInterval / this.attackSpeedStat;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0004C38C File Offset: 0x0004A58C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.fireTimer -= Time.fixedDeltaTime;
			if (this.fireTimer <= 0f)
			{
				this.FireMissile("Muzzle");
				this.fireTimer += this.fireInterval;
			}
			if (this.missileCount >= FireMissileBarrage.maxMissileCount && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040015E7 RID: 5607
		public static GameObject effectPrefab;

		// Token: 0x040015E8 RID: 5608
		public static GameObject projectilePrefab;

		// Token: 0x040015E9 RID: 5609
		public static float damageCoefficient = 1f;

		// Token: 0x040015EA RID: 5610
		public static float baseFireInterval = 0.1f;

		// Token: 0x040015EB RID: 5611
		public static float minSpread = 0f;

		// Token: 0x040015EC RID: 5612
		public static float maxSpread = 5f;

		// Token: 0x040015ED RID: 5613
		public static int maxMissileCount;

		// Token: 0x040015EE RID: 5614
		private float fireTimer;

		// Token: 0x040015EF RID: 5615
		private float fireInterval;

		// Token: 0x040015F0 RID: 5616
		private Transform modelTransform;

		// Token: 0x040015F1 RID: 5617
		private AimAnimator aimAnimator;

		// Token: 0x040015F2 RID: 5618
		private int missileCount;
	}
}
