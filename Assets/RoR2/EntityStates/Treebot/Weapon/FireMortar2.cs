using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Treebot.Weapon
{
	// Token: 0x02000184 RID: 388
	public class FireMortar2 : BaseState
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x0001D294 File Offset: 0x0001B494
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireMortar2.baseDuration / this.attackSpeedStat;
			EffectManager.SimpleMuzzleFlash(FireMortar2.muzzleEffect, base.gameObject, FireMortar2.muzzleName, false);
			Util.PlaySound(FireMortar2.fireSound, base.gameObject);
			base.PlayCrossfade("Gesture, Additive", "FireBomb", 0.1f);
			if (base.isAuthority)
			{
				this.Fire();
			}
			if (NetworkServer.active && base.healthComponent)
			{
				DamageInfo damageInfo = new DamageInfo();
				damageInfo.damage = base.healthComponent.combinedHealth * FireMortar2.healthCostFraction;
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

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001D399 File Offset: 0x0001B599
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001D3C4 File Offset: 0x0001B5C4
		private void Fire()
		{
			RaycastHit raycastHit;
			Vector3 point;
			if (base.inputBank.GetAimRaycast(FireMortar2.maxDistance, out raycastHit))
			{
				point = raycastHit.point;
			}
			else
			{
				point = base.inputBank.GetAimRay().GetPoint(FireMortar2.maxDistance);
			}
			FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
			{
				projectilePrefab = FireMortar2.projectilePrefab,
				position = point,
				rotation = Quaternion.identity,
				owner = base.gameObject,
				damage = FireMortar2.damageCoefficient * this.damageStat,
				force = FireMortar2.force,
				crit = base.RollCrit(),
				damageColorIndex = DamageColorIndex.Default,
				target = null,
				speedOverride = 0f,
				fuseOverride = -1f
			};
			ProjectileManager.instance.FireProjectile(fireProjectileInfo);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000853 RID: 2131
		public static float baseDuration;

		// Token: 0x04000854 RID: 2132
		public static GameObject projectilePrefab;

		// Token: 0x04000855 RID: 2133
		public static string fireSound;

		// Token: 0x04000856 RID: 2134
		public static float maxDistance;

		// Token: 0x04000857 RID: 2135
		public static float damageCoefficient;

		// Token: 0x04000858 RID: 2136
		public static float force;

		// Token: 0x04000859 RID: 2137
		public static string muzzleName;

		// Token: 0x0400085A RID: 2138
		public static GameObject muzzleEffect;

		// Token: 0x0400085B RID: 2139
		public static float healthCostFraction;

		// Token: 0x0400085C RID: 2140
		private float duration;
	}
}
