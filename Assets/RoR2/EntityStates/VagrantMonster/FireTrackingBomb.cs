using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.VagrantMonster
{
	// Token: 0x020002E6 RID: 742
	public class FireTrackingBomb : BaseState
	{
		// Token: 0x06000D40 RID: 3392 RVA: 0x00037C60 File Offset: 0x00035E60
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			this.duration = FireTrackingBomb.baseDuration / this.attackSpeedStat;
			base.PlayAnimation("Gesture, Override", "FireTrackingBomb", "FireTrackingBomb.playbackRate", this.duration);
			this.FireBomb();
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00037CB1 File Offset: 0x00035EB1
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00037CF0 File Offset: 0x00035EF0
		private void FireBomb()
		{
			Ray aimRay = base.GetAimRay();
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					aimRay.origin = component.FindChild("TrackingBombMuzzle").transform.position;
				}
			}
			EffectManager.SimpleMuzzleFlash(FireTrackingBomb.muzzleEffectPrefab, base.gameObject, "TrackingBombMuzzle", false);
			if (base.isAuthority)
			{
				ProjectileManager.instance.FireProjectile(FireTrackingBomb.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * FireTrackingBomb.bombDamageCoefficient, FireTrackingBomb.bombForce, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x0400102A RID: 4138
		public static float baseDuration = 3f;

		// Token: 0x0400102B RID: 4139
		public static GameObject projectilePrefab;

		// Token: 0x0400102C RID: 4140
		public static GameObject muzzleEffectPrefab;

		// Token: 0x0400102D RID: 4141
		public static string fireBombSoundString;

		// Token: 0x0400102E RID: 4142
		public static float bombDamageCoefficient;

		// Token: 0x0400102F RID: 4143
		public static float bombForce;

		// Token: 0x04001030 RID: 4144
		public float novaRadius;

		// Token: 0x04001031 RID: 4145
		private float duration;

		// Token: 0x04001032 RID: 4146
		private float stopwatch;
	}
}
