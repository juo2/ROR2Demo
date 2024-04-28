using System;
using RoR2;
using UnityEngine;

namespace EntityStates.EngiTurret.EngiTurretWeapon
{
	// Token: 0x0200038B RID: 907
	public class FireGauss : BaseState
	{
		// Token: 0x06001040 RID: 4160 RVA: 0x000478D4 File Offset: 0x00045AD4
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireGauss.baseDuration / this.attackSpeedStat;
			Util.PlaySound(FireGauss.attackSoundString, base.gameObject);
			Ray aimRay = base.GetAimRay();
			base.StartAimMode(aimRay, 2f, false);
			base.PlayAnimation("Gesture", "FireGauss", "FireGauss.playbackRate", this.duration);
			string muzzleName = "Muzzle";
			if (FireGauss.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireGauss.effectPrefab, base.gameObject, muzzleName, false);
			}
			if (base.isAuthority)
			{
				new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = aimRay.origin,
					aimVector = aimRay.direction,
					minSpread = FireGauss.minSpread,
					maxSpread = FireGauss.maxSpread,
					bulletCount = 1U,
					damage = FireGauss.damageCoefficient * this.damageStat,
					force = FireGauss.force,
					tracerEffectPrefab = FireGauss.tracerEffectPrefab,
					muzzleName = muzzleName,
					hitEffectPrefab = FireGauss.hitEffectPrefab,
					isCrit = Util.CheckRoll(this.critStat, base.characterBody.master),
					HitEffectNormal = false,
					radius = 0.15f
				}.Fire();
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00047A29 File Offset: 0x00045C29
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040014B5 RID: 5301
		public static GameObject effectPrefab;

		// Token: 0x040014B6 RID: 5302
		public static GameObject hitEffectPrefab;

		// Token: 0x040014B7 RID: 5303
		public static GameObject tracerEffectPrefab;

		// Token: 0x040014B8 RID: 5304
		public static string attackSoundString;

		// Token: 0x040014B9 RID: 5305
		public static float damageCoefficient;

		// Token: 0x040014BA RID: 5306
		public static float force;

		// Token: 0x040014BB RID: 5307
		public static float minSpread;

		// Token: 0x040014BC RID: 5308
		public static float maxSpread;

		// Token: 0x040014BD RID: 5309
		public static int bulletCount;

		// Token: 0x040014BE RID: 5310
		public static float baseDuration = 2f;

		// Token: 0x040014BF RID: 5311
		public int bulletCountCurrent = 1;

		// Token: 0x040014C0 RID: 5312
		private float duration;
	}
}
