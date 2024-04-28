using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Engi.EngiWeapon
{
	// Token: 0x020003A9 RID: 937
	public class FireMines : BaseState
	{
		// Token: 0x060010D7 RID: 4311 RVA: 0x00049B2C File Offset: 0x00047D2C
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(FireMines.throwMineSoundString, base.gameObject);
			this.duration = FireMines.baseDuration / this.attackSpeedStat;
			Ray aimRay = base.GetAimRay();
			base.StartAimMode(aimRay, 2f, false);
			if (base.GetModelAnimator())
			{
				float num = this.duration * 0.3f;
				base.PlayCrossfade("Gesture, Additive", "FireMineRight", "FireMine.playbackRate", this.duration + num, 0.05f);
			}
			string muzzleName = "MuzzleCenter";
			if (FireMines.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireMines.effectPrefab, base.gameObject, muzzleName, false);
			}
			if (base.isAuthority)
			{
				ProjectileManager.instance.FireProjectile(this.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * this.damageCoefficient, this.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00049C37 File Offset: 0x00047E37
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001532 RID: 5426
		public static GameObject effectPrefab;

		// Token: 0x04001533 RID: 5427
		public static GameObject hitEffectPrefab;

		// Token: 0x04001534 RID: 5428
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x04001535 RID: 5429
		[SerializeField]
		public float damageCoefficient;

		// Token: 0x04001536 RID: 5430
		[SerializeField]
		public float force;

		// Token: 0x04001537 RID: 5431
		public static float baseDuration = 2f;

		// Token: 0x04001538 RID: 5432
		public static string throwMineSoundString;

		// Token: 0x04001539 RID: 5433
		private float duration;
	}
}
