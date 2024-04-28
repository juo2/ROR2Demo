using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Wisp1Monster
{
	// Token: 0x020000DC RID: 220
	public class FireEmbers : BaseState
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x000106B0 File Offset: 0x0000E8B0
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireEmbers.baseDuration / this.attackSpeedStat;
			Util.PlayAttackSpeedSound(FireEmbers.attackString, base.gameObject, this.attackSpeedStat);
			Ray aimRay = base.GetAimRay();
			base.StartAimMode(aimRay, 2f, false);
			base.PlayAnimation("Body", "FireAttack1", "FireAttack1.playbackRate", this.duration);
			string muzzleName = "";
			if (FireEmbers.effectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(FireEmbers.effectPrefab, base.gameObject, muzzleName, false);
			}
			if (base.isAuthority)
			{
				new BulletAttack
				{
					owner = base.gameObject,
					weapon = base.gameObject,
					origin = aimRay.origin,
					aimVector = aimRay.direction,
					minSpread = FireEmbers.minSpread,
					maxSpread = FireEmbers.maxSpread,
					bulletCount = (uint)((FireEmbers.bulletCount > 0) ? FireEmbers.bulletCount : 0),
					damage = FireEmbers.damageCoefficient * this.damageStat,
					force = FireEmbers.force,
					tracerEffectPrefab = FireEmbers.tracerEffectPrefab,
					muzzleName = muzzleName,
					hitEffectPrefab = FireEmbers.hitEffectPrefab,
					isCrit = Util.CheckRoll(this.critStat, base.characterBody.master),
					falloffModel = BulletAttack.FalloffModel.DefaultBullet,
					HitEffectNormal = false,
					radius = 0.5f,
					procCoefficient = 1f / (float)FireEmbers.bulletCount
				}.Fire();
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00010833 File Offset: 0x0000EA33
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x040003F9 RID: 1017
		public static GameObject effectPrefab;

		// Token: 0x040003FA RID: 1018
		public static GameObject hitEffectPrefab;

		// Token: 0x040003FB RID: 1019
		public static GameObject tracerEffectPrefab;

		// Token: 0x040003FC RID: 1020
		public static float damageCoefficient;

		// Token: 0x040003FD RID: 1021
		public static float force;

		// Token: 0x040003FE RID: 1022
		public static float minSpread;

		// Token: 0x040003FF RID: 1023
		public static float maxSpread;

		// Token: 0x04000400 RID: 1024
		public static int bulletCount;

		// Token: 0x04000401 RID: 1025
		public static float baseDuration = 2f;

		// Token: 0x04000402 RID: 1026
		public static string attackString;

		// Token: 0x04000403 RID: 1027
		private float duration;
	}
}
