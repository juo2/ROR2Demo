using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Destructible
{
	// Token: 0x020003CF RID: 975
	public class ExplosivePotDeath : BaseState
	{
		// Token: 0x06001171 RID: 4465 RVA: 0x0004CDB5 File Offset: 0x0004AFB5
		public override void OnEnter()
		{
			base.OnEnter();
			if (ExplosivePotDeath.chargePrefab)
			{
				UnityEngine.Object.Instantiate<GameObject>(ExplosivePotDeath.chargePrefab, base.transform);
			}
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0004CDDA File Offset: 0x0004AFDA
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge >= ExplosivePotDeath.chargeDuration)
			{
				this.Explode();
			}
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0004CDFC File Offset: 0x0004AFFC
		private void Explode()
		{
			if (ExplosivePotDeath.explosionEffectPrefab)
			{
				EffectManager.SpawnEffect(ExplosivePotDeath.explosionEffectPrefab, new EffectData
				{
					origin = base.transform.position,
					scale = ExplosivePotDeath.explosionRadius,
					rotation = Quaternion.identity
				}, true);
			}
			new BlastAttack
			{
				attacker = base.gameObject,
				damageColorIndex = DamageColorIndex.Item,
				baseDamage = this.damageStat * ExplosivePotDeath.explosionDamageCoefficient * Run.instance.teamlessDamageCoefficient,
				radius = ExplosivePotDeath.explosionRadius,
				falloffModel = BlastAttack.FalloffModel.None,
				procCoefficient = ExplosivePotDeath.explosionProcCoefficient,
				teamIndex = TeamIndex.None,
				damageType = DamageType.ClayGoo,
				position = base.transform.position,
				baseForce = ExplosivePotDeath.explosionForce,
				attackerFiltering = AttackerFiltering.NeverHitSelf
			}.Fire();
			EntityState.Destroy(base.gameObject);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x0400161C RID: 5660
		public static GameObject chargePrefab;

		// Token: 0x0400161D RID: 5661
		public static float chargeDuration;

		// Token: 0x0400161E RID: 5662
		public static GameObject explosionEffectPrefab;

		// Token: 0x0400161F RID: 5663
		public static float explosionRadius;

		// Token: 0x04001620 RID: 5664
		public static float explosionDamageCoefficient;

		// Token: 0x04001621 RID: 5665
		public static float explosionProcCoefficient;

		// Token: 0x04001622 RID: 5666
		public static float explosionForce;
	}
}
