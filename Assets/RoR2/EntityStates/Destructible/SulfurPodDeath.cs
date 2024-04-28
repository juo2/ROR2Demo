using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Destructible
{
	// Token: 0x020003D2 RID: 978
	public class SulfurPodDeath : GenericCharacterDeath
	{
		// Token: 0x06001180 RID: 4480 RVA: 0x0004D240 File Offset: 0x0004B440
		public override void OnEnter()
		{
			base.OnEnter();
			if (SulfurPodDeath.chargePrefab)
			{
				UnityEngine.Object.Instantiate<GameObject>(SulfurPodDeath.chargePrefab, base.transform);
			}
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0004D265 File Offset: 0x0004B465
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SulfurPodDeath.chargeDuration)
			{
				this.Explode();
			}
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0004D280 File Offset: 0x0004B480
		private void Explode()
		{
			if (this.hasExploded)
			{
				return;
			}
			this.hasExploded = true;
			if (SulfurPodDeath.explosionEffectPrefab)
			{
				EffectManager.SpawnEffect(SulfurPodDeath.explosionEffectPrefab, new EffectData
				{
					origin = base.transform.position,
					scale = SulfurPodDeath.explosionRadius,
					rotation = Quaternion.identity
				}, true);
			}
			base.DestroyModel();
			if (NetworkServer.active)
			{
				new BlastAttack
				{
					attacker = base.gameObject,
					damageColorIndex = DamageColorIndex.Poison,
					baseDamage = this.damageStat * SulfurPodDeath.explosionDamageCoefficient * Run.instance.teamlessDamageCoefficient,
					radius = SulfurPodDeath.explosionRadius,
					falloffModel = BlastAttack.FalloffModel.None,
					procCoefficient = SulfurPodDeath.explosionProcCoefficient,
					teamIndex = TeamIndex.None,
					damageType = DamageType.PoisonOnHit,
					position = base.transform.position,
					baseForce = SulfurPodDeath.explosionForce,
					attackerFiltering = AttackerFiltering.NeverHitSelf
				}.Fire();
				base.DestroyBodyAsapServer();
			}
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001631 RID: 5681
		public static GameObject chargePrefab;

		// Token: 0x04001632 RID: 5682
		public static float chargeDuration;

		// Token: 0x04001633 RID: 5683
		public static GameObject explosionEffectPrefab;

		// Token: 0x04001634 RID: 5684
		public static float explosionRadius;

		// Token: 0x04001635 RID: 5685
		public static float explosionDamageCoefficient;

		// Token: 0x04001636 RID: 5686
		public static float explosionProcCoefficient;

		// Token: 0x04001637 RID: 5687
		public static float explosionForce;

		// Token: 0x04001638 RID: 5688
		private bool hasExploded;
	}
}
