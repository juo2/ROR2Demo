using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Destructible
{
	// Token: 0x020003D3 RID: 979
	public class TimeCrystalDeath : BaseState
	{
		// Token: 0x06001185 RID: 4485 RVA: 0x0004D383 File Offset: 0x0004B583
		public override void OnEnter()
		{
			base.OnEnter();
			this.Explode();
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0004D391 File Offset: 0x0004B591
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0004D3AC File Offset: 0x0004B5AC
		private void Explode()
		{
			if (base.modelLocator)
			{
				if (base.modelLocator.modelBaseTransform)
				{
					EntityState.Destroy(base.modelLocator.modelBaseTransform.gameObject);
				}
				if (base.modelLocator.modelTransform)
				{
					EntityState.Destroy(base.modelLocator.modelTransform.gameObject);
				}
			}
			if (TimeCrystalDeath.explosionEffectPrefab && NetworkServer.active)
			{
				EffectManager.SpawnEffect(TimeCrystalDeath.explosionEffectPrefab, new EffectData
				{
					origin = base.transform.position,
					scale = TimeCrystalDeath.explosionRadius,
					rotation = Quaternion.identity
				}, true);
			}
			new BlastAttack
			{
				attacker = base.gameObject,
				damageColorIndex = DamageColorIndex.Item,
				baseDamage = this.damageStat * TimeCrystalDeath.explosionDamageCoefficient * Run.instance.teamlessDamageCoefficient,
				radius = TimeCrystalDeath.explosionRadius,
				falloffModel = BlastAttack.FalloffModel.None,
				procCoefficient = TimeCrystalDeath.explosionProcCoefficient,
				teamIndex = TeamIndex.None,
				position = base.transform.position,
				baseForce = TimeCrystalDeath.explosionForce,
				bonusForce = TimeCrystalDeath.explosionForce * 0.5f * Vector3.up,
				attackerFiltering = AttackerFiltering.NeverHitSelf
			}.Fire();
			EntityState.Destroy(base.gameObject);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001639 RID: 5689
		public static GameObject explosionEffectPrefab;

		// Token: 0x0400163A RID: 5690
		public static float explosionRadius;

		// Token: 0x0400163B RID: 5691
		public static float explosionDamageCoefficient;

		// Token: 0x0400163C RID: 5692
		public static float explosionProcCoefficient;

		// Token: 0x0400163D RID: 5693
		public static float explosionForce;

		// Token: 0x0400163E RID: 5694
		private float stopwatch;
	}
}
