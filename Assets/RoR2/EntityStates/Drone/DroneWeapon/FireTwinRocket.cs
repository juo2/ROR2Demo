using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Drone.DroneWeapon
{
	// Token: 0x020003CA RID: 970
	public class FireTwinRocket : BaseState
	{
		// Token: 0x06001152 RID: 4434 RVA: 0x0004C5F8 File Offset: 0x0004A7F8
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			this.duration = FireTwinRocket.baseDuration / this.attackSpeedStat;
			base.GetAimRay();
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				this.childLocator = modelTransform.GetComponent<ChildLocator>();
			}
			this.FireProjectile("GatLeft");
			this.FireProjectile("GatRight");
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x0004C660 File Offset: 0x0004A860
		private void FireProjectile(string muzzleString)
		{
			base.GetAimRay();
			Transform transform = this.childLocator.FindChild(muzzleString);
			if (FireTwinRocket.muzzleEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireTwinRocket.muzzleEffectPrefab, base.gameObject, muzzleString, false);
			}
			if (base.isAuthority && FireTwinRocket.projectilePrefab != null)
			{
				float maxDistance = 1000f;
				Ray aimRay = base.GetAimRay();
				Vector3 forward = aimRay.direction;
				Vector3 position = aimRay.origin;
				if (transform)
				{
					position = transform.position;
					RaycastHit raycastHit;
					if (Physics.Raycast(aimRay, out raycastHit, maxDistance, LayerIndex.world.mask | LayerIndex.entityPrecise.mask))
					{
						forward = raycastHit.point - transform.position;
					}
				}
				ProjectileManager.instance.FireProjectile(FireTwinRocket.projectilePrefab, position, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * FireTwinRocket.damageCoefficient, FireTwinRocket.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0004C778 File Offset: 0x0004A978
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.duration / this.attackSpeedStat && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001600 RID: 5632
		public static GameObject projectilePrefab;

		// Token: 0x04001601 RID: 5633
		public static GameObject muzzleEffectPrefab;

		// Token: 0x04001602 RID: 5634
		public static float damageCoefficient;

		// Token: 0x04001603 RID: 5635
		public static float force;

		// Token: 0x04001604 RID: 5636
		public static float baseDuration = 2f;

		// Token: 0x04001605 RID: 5637
		private ChildLocator childLocator;

		// Token: 0x04001606 RID: 5638
		private float stopwatch;

		// Token: 0x04001607 RID: 5639
		private float duration;
	}
}
