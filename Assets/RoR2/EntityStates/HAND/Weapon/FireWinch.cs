using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.HAND.Weapon
{
	// Token: 0x02000334 RID: 820
	public class FireWinch : BaseState
	{
		// Token: 0x06000EBE RID: 3774 RVA: 0x0003FA18 File Offset: 0x0003DC18
		public override void OnEnter()
		{
			base.OnEnter();
			Ray aimRay = base.GetAimRay();
			base.StartAimMode(aimRay, 2f, false);
			string muzzleName = "WinchHole";
			if (FireWinch.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireWinch.effectPrefab, base.gameObject, muzzleName, false);
			}
			if (base.isAuthority)
			{
				ProjectileManager.instance.FireProjectile(FireWinch.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * FireWinch.damageCoefficient, FireWinch.force, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
			}
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0003FAC1 File Offset: 0x0003DCC1
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= FireWinch.duration / this.attackSpeedStat && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x04001273 RID: 4723
		public static GameObject projectilePrefab;

		// Token: 0x04001274 RID: 4724
		public static GameObject effectPrefab;

		// Token: 0x04001275 RID: 4725
		public static float duration = 2f;

		// Token: 0x04001276 RID: 4726
		public static float baseDuration = 2f;

		// Token: 0x04001277 RID: 4727
		public static float damageCoefficient = 1.2f;

		// Token: 0x04001278 RID: 4728
		public static float force = 20f;
	}
}
