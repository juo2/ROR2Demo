using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.LemurianMonster
{
	// Token: 0x020002D5 RID: 725
	public class FireFireball : BaseState
	{
		// Token: 0x06000CE6 RID: 3302 RVA: 0x00036644 File Offset: 0x00034844
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireFireball.baseDuration / this.attackSpeedStat;
			base.PlayAnimation("Gesture", "FireFireball", "FireFireball.playbackRate", this.duration);
			Util.PlaySound(FireFireball.attackString, base.gameObject);
			Ray aimRay = base.GetAimRay();
			string muzzleName = "MuzzleMouth";
			if (FireFireball.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireFireball.effectPrefab, base.gameObject, muzzleName, false);
			}
			if (base.isAuthority)
			{
				ProjectileManager.instance.FireProjectile(FireFireball.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * FireFireball.damageCoefficient, FireFireball.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0003671E File Offset: 0x0003491E
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04000FCD RID: 4045
		public static GameObject projectilePrefab;

		// Token: 0x04000FCE RID: 4046
		public static GameObject effectPrefab;

		// Token: 0x04000FCF RID: 4047
		public static float baseDuration = 2f;

		// Token: 0x04000FD0 RID: 4048
		public static float damageCoefficient = 1.2f;

		// Token: 0x04000FD1 RID: 4049
		public static float force = 20f;

		// Token: 0x04000FD2 RID: 4050
		public static string attackString;

		// Token: 0x04000FD3 RID: 4051
		private float duration;
	}
}
