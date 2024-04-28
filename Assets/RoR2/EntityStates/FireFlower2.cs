using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates
{
	// Token: 0x020000D8 RID: 216
	public class FireFlower2 : BaseState
	{
		// Token: 0x060003EE RID: 1006 RVA: 0x00010194 File Offset: 0x0000E394
		public override void OnEnter()
		{
			base.OnEnter();
			EffectManager.SimpleMuzzleFlash(FireFlower2.muzzleFlashPrefab, base.gameObject, FireFlower2.muzzleName, false);
			this.duration = FireFlower2.baseDuration / this.attackSpeedStat;
			Util.PlaySound(FireFlower2.enterSoundString, base.gameObject);
			base.PlayAnimation("Gesture, Additive", "FireFlower", "FireFlower.playbackRate", this.duration);
			if (base.isAuthority)
			{
				Ray aimRay = base.GetAimRay();
				FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
				{
					crit = base.RollCrit(),
					damage = FireFlower2.damageCoefficient * this.damageStat,
					damageColorIndex = DamageColorIndex.Default,
					force = 0f,
					owner = base.gameObject,
					position = aimRay.origin,
					procChainMask = default(ProcChainMask),
					projectilePrefab = FireFlower2.projectilePrefab,
					rotation = Quaternion.LookRotation(aimRay.direction),
					useSpeedOverride = false
				};
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
			if (NetworkServer.active && base.healthComponent)
			{
				DamageInfo damageInfo = new DamageInfo();
				damageInfo.damage = base.healthComponent.combinedHealth * FireFlower2.healthCostFraction;
				damageInfo.position = base.characterBody.corePosition;
				damageInfo.force = Vector3.zero;
				damageInfo.damageColorIndex = DamageColorIndex.Default;
				damageInfo.crit = false;
				damageInfo.attacker = null;
				damageInfo.inflictor = null;
				damageInfo.damageType = (DamageType.NonLethal | DamageType.BypassArmor);
				damageInfo.procCoefficient = 0f;
				damageInfo.procChainMask = default(ProcChainMask);
				base.healthComponent.TakeDamage(damageInfo);
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001033B File Offset: 0x0000E53B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040003E6 RID: 998
		public static GameObject projectilePrefab;

		// Token: 0x040003E7 RID: 999
		public static float baseDuration;

		// Token: 0x040003E8 RID: 1000
		public static float damageCoefficient;

		// Token: 0x040003E9 RID: 1001
		public static float healthCostFraction;

		// Token: 0x040003EA RID: 1002
		public static string enterSoundString;

		// Token: 0x040003EB RID: 1003
		public static string muzzleName;

		// Token: 0x040003EC RID: 1004
		public static GameObject muzzleFlashPrefab;

		// Token: 0x040003ED RID: 1005
		private float duration;
	}
}
