using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.VagrantMonster.Weapon
{
	// Token: 0x020002E9 RID: 745
	public class JellyStorm : BaseState
	{
		// Token: 0x06000D4F RID: 3407 RVA: 0x000380CC File Offset: 0x000362CC
		public override void OnEnter()
		{
			base.OnEnter();
			this.missileStopwatch -= JellyStorm.missileSpawnDelay;
			if (base.sfxLocator && base.sfxLocator.barkSound != "")
			{
				Util.PlaySound(base.sfxLocator.barkSound, base.gameObject);
			}
			this.PlayAnimation("Gesture", "StormEnter");
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				this.childLocator = modelTransform.GetComponent<ChildLocator>();
				if (this.childLocator)
				{
					this.childLocator.FindChild(JellyStorm.stormPointChildString);
				}
			}
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0003817C File Offset: 0x0003637C
		private void FireBlob(Ray aimRay, float bonusPitch, float bonusYaw, float speed)
		{
			Vector3 forward = Util.ApplySpread(aimRay.direction, 0f, 0f, 1f, 1f, bonusYaw, bonusPitch);
			ProjectileManager.instance.FireProjectile(JellyStorm.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(forward), base.gameObject, this.damageStat * JellyStorm.damageCoefficient, 0f, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, speed);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x000381FC File Offset: 0x000363FC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			this.missileStopwatch += Time.fixedDeltaTime;
			if (this.missileStopwatch >= 1f / JellyStorm.missileSpawnFrequency && !this.beginExitTransition)
			{
				this.missileStopwatch -= 1f / JellyStorm.missileSpawnFrequency;
				Transform transform = this.childLocator.FindChild(JellyStorm.stormPointChildString);
				if (transform)
				{
					for (int i = 0; i < JellyStorm.missileTurretCount; i++)
					{
						float bonusYaw = 360f / (float)JellyStorm.missileTurretCount * (float)i + 360f * JellyStorm.missileTurretYawFrequency * this.stopwatch;
						float bonusPitch = Mathf.Sin(6.2831855f * JellyStorm.missileTurretPitchFrequency * this.stopwatch) * JellyStorm.missileTurretPitchMagnitude;
						this.FireBlob(new Ray
						{
							origin = transform.position,
							direction = transform.transform.forward
						}, bonusPitch, bonusYaw, JellyStorm.missileSpeed);
					}
				}
			}
			if (this.stopwatch >= JellyStorm.stormDuration - JellyStorm.stormToIdleTransitionDuration && !this.beginExitTransition)
			{
				this.beginExitTransition = true;
				base.PlayCrossfade("Gesture", "StormExit", "StormExit.playbackRate", JellyStorm.stormToIdleTransitionDuration, 0.5f);
			}
			if (this.stopwatch >= JellyStorm.stormDuration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x04001042 RID: 4162
		private float stopwatch;

		// Token: 0x04001043 RID: 4163
		private float missileStopwatch;

		// Token: 0x04001044 RID: 4164
		public static float stormDuration;

		// Token: 0x04001045 RID: 4165
		public static float stormToIdleTransitionDuration;

		// Token: 0x04001046 RID: 4166
		public static string stormPointChildString;

		// Token: 0x04001047 RID: 4167
		public static float missileSpawnFrequency;

		// Token: 0x04001048 RID: 4168
		public static float missileSpawnDelay;

		// Token: 0x04001049 RID: 4169
		public static int missileTurretCount;

		// Token: 0x0400104A RID: 4170
		public static float missileTurretYawFrequency;

		// Token: 0x0400104B RID: 4171
		public static float missileTurretPitchFrequency;

		// Token: 0x0400104C RID: 4172
		public static float missileTurretPitchMagnitude;

		// Token: 0x0400104D RID: 4173
		public static float missileSpeed;

		// Token: 0x0400104E RID: 4174
		public static float damageCoefficient;

		// Token: 0x0400104F RID: 4175
		public static GameObject projectilePrefab;

		// Token: 0x04001050 RID: 4176
		public static GameObject effectPrefab;

		// Token: 0x04001051 RID: 4177
		private bool beginExitTransition;

		// Token: 0x04001052 RID: 4178
		private ChildLocator childLocator;
	}
}
