using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Drone.DroneWeapon
{
	// Token: 0x020003C9 RID: 969
	public class FireTurret : BaseState
	{
		// Token: 0x0600114C RID: 4428 RVA: 0x0004C428 File Offset: 0x0004A628
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireTurret.baseDuration / this.attackSpeedStat;
			string muzzleName = "Muzzle";
			Util.PlaySound(FireTurret.attackSoundString, base.gameObject);
			if (FireTurret.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireTurret.effectPrefab, base.gameObject, muzzleName, false);
			}
			if (base.isAuthority)
			{
				Ray aimRay = base.GetAimRay();
				new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = aimRay.origin,
					aimVector = aimRay.direction,
					minSpread = FireTurret.minSpread,
					maxSpread = FireTurret.maxSpread,
					damage = FireTurret.damageCoefficient * this.damageStat,
					force = FireTurret.force,
					tracerEffectPrefab = FireTurret.tracerEffectPrefab,
					muzzleName = muzzleName,
					hitEffectPrefab = FireTurret.hitEffectPrefab,
					isCrit = Util.CheckRoll(this.critStat, base.characterBody.master)
				}.Fire();
			}
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0004C53C File Offset: 0x0004A73C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= FireTurret.durationBetweenShots / this.attackSpeedStat && this.bulletCountCurrent < FireTurret.bulletCount && base.isAuthority)
			{
				FireTurret fireTurret = new FireTurret();
				fireTurret.bulletCountCurrent = this.bulletCountCurrent + 1;
				this.outer.SetNextState(fireTurret);
				return;
			}
			if (base.fixedAge >= this.duration && this.bulletCountCurrent >= FireTurret.bulletCount && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040015F3 RID: 5619
		public static GameObject effectPrefab;

		// Token: 0x040015F4 RID: 5620
		public static GameObject hitEffectPrefab;

		// Token: 0x040015F5 RID: 5621
		public static GameObject tracerEffectPrefab;

		// Token: 0x040015F6 RID: 5622
		public static string attackSoundString;

		// Token: 0x040015F7 RID: 5623
		public static float damageCoefficient;

		// Token: 0x040015F8 RID: 5624
		public static float force;

		// Token: 0x040015F9 RID: 5625
		public static float minSpread;

		// Token: 0x040015FA RID: 5626
		public static float maxSpread;

		// Token: 0x040015FB RID: 5627
		public static int bulletCount;

		// Token: 0x040015FC RID: 5628
		public static float durationBetweenShots = 1f;

		// Token: 0x040015FD RID: 5629
		public static float baseDuration = 2f;

		// Token: 0x040015FE RID: 5630
		public int bulletCountCurrent = 1;

		// Token: 0x040015FF RID: 5631
		private float duration = 2f;
	}
}
