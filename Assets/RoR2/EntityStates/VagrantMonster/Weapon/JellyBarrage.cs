using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.VagrantMonster.Weapon
{
	// Token: 0x020002E8 RID: 744
	public class JellyBarrage : BaseState
	{
		// Token: 0x06000D4B RID: 3403 RVA: 0x00037E84 File Offset: 0x00036084
		public override void OnEnter()
		{
			base.OnEnter();
			this.missileStopwatch -= JellyBarrage.missileSpawnDelay;
			if (base.sfxLocator && base.sfxLocator.barkSound != "")
			{
				Util.PlaySound(base.sfxLocator.barkSound, base.gameObject);
			}
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				this.childLocator = modelTransform.GetComponent<ChildLocator>();
				if (this.childLocator)
				{
					this.childLocator.FindChild(JellyBarrage.muzzleString);
				}
			}
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00037F24 File Offset: 0x00036124
		private void FireBlob(Ray projectileRay, float bonusPitch, float bonusYaw)
		{
			projectileRay.direction = Util.ApplySpread(projectileRay.direction, 0f, JellyBarrage.maxSpread, 1f, 1f, bonusYaw, bonusPitch);
			ProjectileManager.instance.FireProjectile(JellyBarrage.projectilePrefab, projectileRay.origin, Util.QuaternionSafeLookRotation(projectileRay.direction), base.gameObject, this.damageStat * JellyBarrage.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00037FB0 File Offset: 0x000361B0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			this.missileStopwatch += Time.fixedDeltaTime;
			if (this.missileStopwatch >= 1f / JellyBarrage.missileSpawnFrequency)
			{
				this.missileStopwatch -= 1f / JellyBarrage.missileSpawnFrequency;
				Transform transform = this.childLocator.FindChild(JellyBarrage.muzzleString);
				if (transform)
				{
					Ray projectileRay = default(Ray);
					projectileRay.origin = transform.position;
					projectileRay.direction = base.GetAimRay().direction;
					float maxDistance = 1000f;
					RaycastHit raycastHit;
					if (Physics.Raycast(base.GetAimRay(), out raycastHit, maxDistance, LayerIndex.world.mask))
					{
						projectileRay.direction = raycastHit.point - transform.position;
					}
					this.FireBlob(projectileRay, 0f, 0f);
				}
			}
			if (this.stopwatch >= JellyBarrage.baseDuration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x04001037 RID: 4151
		private float stopwatch;

		// Token: 0x04001038 RID: 4152
		private float missileStopwatch;

		// Token: 0x04001039 RID: 4153
		public static float baseDuration;

		// Token: 0x0400103A RID: 4154
		public static string muzzleString;

		// Token: 0x0400103B RID: 4155
		public static float missileSpawnFrequency;

		// Token: 0x0400103C RID: 4156
		public static float missileSpawnDelay;

		// Token: 0x0400103D RID: 4157
		public static float damageCoefficient;

		// Token: 0x0400103E RID: 4158
		public static float maxSpread;

		// Token: 0x0400103F RID: 4159
		public static GameObject projectilePrefab;

		// Token: 0x04001040 RID: 4160
		public static GameObject muzzleflashPrefab;

		// Token: 0x04001041 RID: 4161
		private ChildLocator childLocator;
	}
}
