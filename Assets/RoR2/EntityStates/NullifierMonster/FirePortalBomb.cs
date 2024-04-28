using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.NullifierMonster
{
	// Token: 0x02000235 RID: 565
	public class FirePortalBomb : BaseState
	{
		// Token: 0x060009FF RID: 2559 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0002965C File Offset: 0x0002785C
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FirePortalBomb.baseDuration / this.attackSpeedStat;
			base.StartAimMode(4f, false);
			if (base.isAuthority)
			{
				this.fireInterval = this.duration / (float)FirePortalBomb.portalBombCount;
				this.fireTimer = 0f;
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x000296B4 File Offset: 0x000278B4
		private void FireBomb(Ray fireRay)
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(fireRay, out raycastHit, FirePortalBomb.maxDistance, LayerIndex.world.mask))
			{
				Vector3 vector = raycastHit.point;
				Vector3 vector2 = vector - this.lastBombPosition;
				if (this.bombsFired > 0 && vector2.sqrMagnitude < FirePortalBomb.minimumDistanceBetweenBombs * FirePortalBomb.minimumDistanceBetweenBombs)
				{
					vector += vector2.normalized * FirePortalBomb.minimumDistanceBetweenBombs;
				}
				FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
				fireProjectileInfo.projectilePrefab = FirePortalBomb.portalBombProjectileEffect;
				fireProjectileInfo.position = vector;
				fireProjectileInfo.rotation = Quaternion.identity;
				fireProjectileInfo.owner = base.gameObject;
				fireProjectileInfo.damage = this.damageStat * FirePortalBomb.damageCoefficient;
				fireProjectileInfo.force = FirePortalBomb.force;
				fireProjectileInfo.crit = base.characterBody.RollCrit();
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
				this.lastBombPosition = vector;
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000297A8 File Offset: 0x000279A8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				this.fireTimer -= Time.fixedDeltaTime;
				if (this.fireTimer <= 0f)
				{
					this.fireTimer += this.fireInterval;
					if (this.startRotation != null && this.endRotation != null)
					{
						float num = 1f / ((float)FirePortalBomb.portalBombCount - 1f);
						float t = (float)this.bombsFired * num;
						Ray aimRay = base.GetAimRay();
						Quaternion rotation = Quaternion.Slerp(this.startRotation.Value, this.endRotation.Value, t);
						aimRay.direction = rotation * Vector3.forward;
						this.FireBomb(aimRay);
						EffectManager.SimpleMuzzleFlash(FirePortalBomb.muzzleflashEffectPrefab, base.gameObject, FirePortalBomb.muzzleString, true);
					}
					this.bombsFired++;
				}
				if (base.fixedAge >= this.duration)
				{
					this.outer.SetNextStateToMain();
				}
			}
		}

		// Token: 0x04000B9D RID: 2973
		public static GameObject portalBombProjectileEffect;

		// Token: 0x04000B9E RID: 2974
		public static GameObject muzzleflashEffectPrefab;

		// Token: 0x04000B9F RID: 2975
		public static string muzzleString;

		// Token: 0x04000BA0 RID: 2976
		public static int portalBombCount;

		// Token: 0x04000BA1 RID: 2977
		public static float baseDuration;

		// Token: 0x04000BA2 RID: 2978
		public static float maxDistance;

		// Token: 0x04000BA3 RID: 2979
		public static float damageCoefficient;

		// Token: 0x04000BA4 RID: 2980
		public static float procCoefficient;

		// Token: 0x04000BA5 RID: 2981
		public static float randomRadius;

		// Token: 0x04000BA6 RID: 2982
		public static float force;

		// Token: 0x04000BA7 RID: 2983
		public static float minimumDistanceBetweenBombs;

		// Token: 0x04000BA8 RID: 2984
		public Quaternion? startRotation;

		// Token: 0x04000BA9 RID: 2985
		public Quaternion? endRotation;

		// Token: 0x04000BAA RID: 2986
		private float duration;

		// Token: 0x04000BAB RID: 2987
		private int bombsFired;

		// Token: 0x04000BAC RID: 2988
		private float fireTimer;

		// Token: 0x04000BAD RID: 2989
		private float fireInterval;

		// Token: 0x04000BAE RID: 2990
		private Vector3 lastBombPosition;
	}
}
