using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Drone.DroneWeapon
{
	// Token: 0x020003C6 RID: 966
	public class FireGatling : BaseState
	{
		// Token: 0x06001139 RID: 4409 RVA: 0x0004BDD4 File Offset: 0x00049FD4
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireGatling.baseDuration / this.attackSpeedStat;
			string muzzleName = "Muzzle";
			Util.PlaySound(FireGatling.fireGatlingSoundString, base.gameObject);
			this.PlayAnimation("Gesture", "FireGatling");
			if (FireGatling.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireGatling.effectPrefab, base.gameObject, muzzleName, false);
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
					minSpread = FireGatling.minSpread,
					maxSpread = FireGatling.maxSpread,
					damage = FireGatling.damageCoefficient * this.damageStat,
					force = FireGatling.force,
					tracerEffectPrefab = FireGatling.tracerEffectPrefab,
					muzzleName = muzzleName,
					hitEffectPrefab = FireGatling.hitEffectPrefab,
					isCrit = Util.CheckRoll(this.critStat, base.characterBody.master)
				}.Fire();
			}
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0004BEF8 File Offset: 0x0004A0F8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040015CA RID: 5578
		public static GameObject effectPrefab;

		// Token: 0x040015CB RID: 5579
		public static GameObject hitEffectPrefab;

		// Token: 0x040015CC RID: 5580
		public static GameObject tracerEffectPrefab;

		// Token: 0x040015CD RID: 5581
		public static float damageCoefficient;

		// Token: 0x040015CE RID: 5582
		public static float force;

		// Token: 0x040015CF RID: 5583
		public static float minSpread;

		// Token: 0x040015D0 RID: 5584
		public static float maxSpread;

		// Token: 0x040015D1 RID: 5585
		public static float baseDuration = 2f;

		// Token: 0x040015D2 RID: 5586
		public static string fireGatlingSoundString;

		// Token: 0x040015D3 RID: 5587
		public int bulletCountCurrent = 1;

		// Token: 0x040015D4 RID: 5588
		private float duration;
	}
}
