using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Destructible
{
	// Token: 0x020003D0 RID: 976
	public class FusionCellDeath : BaseState
	{
		// Token: 0x06001176 RID: 4470 RVA: 0x0004CEE4 File Offset: 0x0004B0E4
		public override void OnEnter()
		{
			base.OnEnter();
			ChildLocator component = base.GetModelTransform().GetComponent<ChildLocator>();
			if (component)
			{
				Transform transform = component.FindChild(FusionCellDeath.chargeChildEffectName);
				if (transform)
				{
					transform.gameObject.SetActive(true);
				}
			}
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0004CF2B File Offset: 0x0004B12B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch > FusionCellDeath.chargeDuration)
			{
				this.Explode();
			}
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0004CF58 File Offset: 0x0004B158
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
			if (FusionCellDeath.explosionEffectPrefab && NetworkServer.active)
			{
				EffectManager.SpawnEffect(FusionCellDeath.explosionEffectPrefab, new EffectData
				{
					origin = base.transform.position,
					scale = FusionCellDeath.explosionRadius,
					rotation = Quaternion.identity
				}, true);
			}
			new BlastAttack
			{
				attacker = base.gameObject,
				damageColorIndex = DamageColorIndex.Item,
				baseDamage = this.damageStat * FusionCellDeath.explosionDamageCoefficient * Run.instance.teamlessDamageCoefficient,
				radius = FusionCellDeath.explosionRadius,
				falloffModel = BlastAttack.FalloffModel.None,
				procCoefficient = FusionCellDeath.explosionProcCoefficient,
				teamIndex = TeamIndex.None,
				position = base.transform.position,
				baseForce = FusionCellDeath.explosionForce,
				bonusForce = FusionCellDeath.explosionForce * 0.5f * Vector3.up,
				attackerFiltering = AttackerFiltering.NeverHitSelf
			}.Fire();
			EntityState.Destroy(base.gameObject);
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001623 RID: 5667
		public static string chargeChildEffectName;

		// Token: 0x04001624 RID: 5668
		public static float chargeDuration;

		// Token: 0x04001625 RID: 5669
		public static GameObject explosionEffectPrefab;

		// Token: 0x04001626 RID: 5670
		public static float explosionRadius;

		// Token: 0x04001627 RID: 5671
		public static float explosionDamageCoefficient;

		// Token: 0x04001628 RID: 5672
		public static float explosionProcCoefficient;

		// Token: 0x04001629 RID: 5673
		public static float explosionForce;

		// Token: 0x0400162A RID: 5674
		private float stopwatch;
	}
}
