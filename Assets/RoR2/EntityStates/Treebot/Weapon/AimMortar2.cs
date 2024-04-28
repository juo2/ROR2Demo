using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Treebot.Weapon
{
	// Token: 0x0200017E RID: 382
	public class AimMortar2 : AimThrowableBase
	{
		// Token: 0x060006AA RID: 1706 RVA: 0x0001CCBC File Offset: 0x0001AEBC
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayCrossfade("Gesture, Additive", "PrepBomb", 0.1f);
			Util.PlaySound(this.enterSound, base.gameObject);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001CCEC File Offset: 0x0001AEEC
		protected override void OnProjectileFiredLocal()
		{
			if (NetworkServer.active && base.healthComponent && this.healthCostFraction >= Mathf.Epsilon)
			{
				DamageInfo damageInfo = new DamageInfo();
				damageInfo.damage = base.healthComponent.combinedHealth * this.healthCostFraction;
				damageInfo.position = base.characterBody.corePosition;
				damageInfo.force = Vector3.zero;
				damageInfo.damageColorIndex = DamageColorIndex.Default;
				damageInfo.crit = false;
				damageInfo.attacker = null;
				damageInfo.inflictor = null;
				damageInfo.damageType = DamageType.NonLethal;
				damageInfo.procCoefficient = 0f;
				damageInfo.procChainMask = default(ProcChainMask);
				base.healthComponent.TakeDamage(damageInfo);
			}
			EffectManager.SimpleMuzzleFlash(this.muzzleFlashPrefab, base.gameObject, AimMortar2.muzzleName, false);
			Util.PlaySound(this.fireSound, base.gameObject);
			base.PlayCrossfade("Gesture, Additive", "FireBomb", 0.1f);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001CDE1 File Offset: 0x0001AFE1
		protected override bool KeyIsDown()
		{
			return base.inputBank.skill2.down;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001CDF3 File Offset: 0x0001AFF3
		protected override void ModifyProjectile(ref FireProjectileInfo fireProjectileInfo)
		{
			base.ModifyProjectile(ref fireProjectileInfo);
			fireProjectileInfo.position = this.currentTrajectoryInfo.hitPoint;
			fireProjectileInfo.rotation = Quaternion.identity;
			fireProjectileInfo.speedOverride = 0f;
		}

		// Token: 0x04000834 RID: 2100
		[SerializeField]
		public float healthCostFraction;

		// Token: 0x04000835 RID: 2101
		public static string muzzleName;

		// Token: 0x04000836 RID: 2102
		[SerializeField]
		public GameObject muzzleFlashPrefab;

		// Token: 0x04000837 RID: 2103
		[SerializeField]
		public string fireSound;

		// Token: 0x04000838 RID: 2104
		[SerializeField]
		public string enterSound;
	}
}
