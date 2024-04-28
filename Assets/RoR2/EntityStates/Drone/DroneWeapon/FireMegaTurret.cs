using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Drone.DroneWeapon
{
	// Token: 0x020003C7 RID: 967
	public class FireMegaTurret : BaseState
	{
		// Token: 0x0600113F RID: 4415 RVA: 0x0004BF40 File Offset: 0x0004A140
		public override void OnEnter()
		{
			base.OnEnter();
			this.fireStopwatch = 0f;
			this.totalDuration = FireMegaTurret.baseTotalDuration / this.attackSpeedStat;
			this.durationBetweenShots = this.totalDuration / (float)FireMegaTurret.maxBulletCount;
			base.GetAimRay();
			Transform transform = base.GetModelTransform();
			if (transform)
			{
				this.childLocator = transform.GetComponent<ChildLocator>();
			}
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0004BFA8 File Offset: 0x0004A1A8
		private void FireBullet(string muzzleString)
		{
			Ray aimRay = base.GetAimRay();
			Vector3 origin = aimRay.origin;
			Util.PlayAttackSpeedSound(FireMegaTurret.attackSoundString, base.gameObject, FireMegaTurret.attackSoundPlaybackCoefficient);
			this.PlayAnimation("Gesture, Additive", "FireGat");
			if (this.childLocator)
			{
				Transform transform = this.childLocator.FindChild(muzzleString);
				if (transform)
				{
					Vector3 position = transform.position;
				}
			}
			if (FireMegaTurret.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireMegaTurret.effectPrefab, base.gameObject, muzzleString, false);
			}
			if (base.isAuthority)
			{
				new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = aimRay.origin,
					aimVector = aimRay.direction,
					minSpread = FireMegaTurret.minSpread,
					maxSpread = FireMegaTurret.maxSpread,
					damage = FireMegaTurret.damageCoefficient * this.damageStat,
					force = FireMegaTurret.force,
					tracerEffectPrefab = FireMegaTurret.tracerEffectPrefab,
					muzzleName = muzzleString,
					hitEffectPrefab = FireMegaTurret.hitEffectPrefab,
					isCrit = Util.CheckRoll(this.critStat, base.characterBody.master)
				}.Fire();
			}
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0004C0E4 File Offset: 0x0004A2E4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.fireStopwatch += Time.fixedDeltaTime;
			this.stopwatch += Time.fixedDeltaTime;
			if (this.fireStopwatch >= this.durationBetweenShots)
			{
				this.bulletCount++;
				this.fireStopwatch -= this.durationBetweenShots;
				this.FireBullet((this.bulletCount % 2 == 0) ? "GatLeft" : "GatRight");
			}
			if (this.stopwatch >= this.totalDuration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040015D5 RID: 5589
		public static GameObject effectPrefab;

		// Token: 0x040015D6 RID: 5590
		public static GameObject hitEffectPrefab;

		// Token: 0x040015D7 RID: 5591
		public static GameObject tracerEffectPrefab;

		// Token: 0x040015D8 RID: 5592
		public static string attackSoundString;

		// Token: 0x040015D9 RID: 5593
		public static float attackSoundPlaybackCoefficient;

		// Token: 0x040015DA RID: 5594
		public static float damageCoefficient;

		// Token: 0x040015DB RID: 5595
		public static float force;

		// Token: 0x040015DC RID: 5596
		public static float minSpread;

		// Token: 0x040015DD RID: 5597
		public static float maxSpread;

		// Token: 0x040015DE RID: 5598
		public static int maxBulletCount;

		// Token: 0x040015DF RID: 5599
		public static float baseTotalDuration;

		// Token: 0x040015E0 RID: 5600
		private Transform modelTransform;

		// Token: 0x040015E1 RID: 5601
		private ChildLocator childLocator;

		// Token: 0x040015E2 RID: 5602
		private float fireStopwatch;

		// Token: 0x040015E3 RID: 5603
		private float stopwatch;

		// Token: 0x040015E4 RID: 5604
		private float durationBetweenShots;

		// Token: 0x040015E5 RID: 5605
		private float totalDuration;

		// Token: 0x040015E6 RID: 5606
		private int bulletCount;
	}
}
