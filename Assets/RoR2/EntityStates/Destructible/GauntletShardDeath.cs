using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Destructible
{
	// Token: 0x020003D1 RID: 977
	public class GauntletShardDeath : BaseState
	{
		// Token: 0x0600117B RID: 4475 RVA: 0x0004D0B2 File Offset: 0x0004B2B2
		public override void OnEnter()
		{
			base.OnEnter();
			this.Explode();
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0004D0C0 File Offset: 0x0004B2C0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0004D0DC File Offset: 0x0004B2DC
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
			if (GauntletShardDeath.explosionEffectPrefab && NetworkServer.active)
			{
				EffectManager.SpawnEffect(GauntletShardDeath.explosionEffectPrefab, new EffectData
				{
					origin = base.transform.position,
					scale = GauntletShardDeath.explosionRadius,
					rotation = Quaternion.identity
				}, true);
			}
			new BlastAttack
			{
				attacker = base.gameObject,
				damageColorIndex = DamageColorIndex.Item,
				baseDamage = this.damageStat * GauntletShardDeath.explosionDamageCoefficient * Run.instance.teamlessDamageCoefficient,
				radius = GauntletShardDeath.explosionRadius,
				falloffModel = BlastAttack.FalloffModel.None,
				procCoefficient = GauntletShardDeath.explosionProcCoefficient,
				teamIndex = TeamIndex.None,
				position = base.transform.position,
				baseForce = GauntletShardDeath.explosionForce,
				bonusForce = GauntletShardDeath.explosionForce * 0.5f * Vector3.up,
				attackerFiltering = AttackerFiltering.NeverHitSelf
			}.Fire();
			Debug.Log("Gauntlet Shard Destroyed!");
			EntityState.Destroy(base.gameObject);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x0400162B RID: 5675
		public static GameObject explosionEffectPrefab;

		// Token: 0x0400162C RID: 5676
		public static float explosionRadius;

		// Token: 0x0400162D RID: 5677
		public static float explosionDamageCoefficient;

		// Token: 0x0400162E RID: 5678
		public static float explosionProcCoefficient;

		// Token: 0x0400162F RID: 5679
		public static float explosionForce;

		// Token: 0x04001630 RID: 5680
		private float stopwatch;
	}
}
