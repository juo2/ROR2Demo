using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Toolbot
{
	// Token: 0x020001A2 RID: 418
	public class DroneProjectileHoverStun : DroneProjectileHover
	{
		// Token: 0x06000780 RID: 1920 RVA: 0x00020290 File Offset: 0x0001E490
		protected override void Pulse()
		{
			BlastAttack blastAttack = new BlastAttack
			{
				baseDamage = 0f,
				baseForce = 0f,
				attacker = (base.projectileController ? base.projectileController.owner : null),
				inflictor = base.gameObject,
				bonusForce = Vector3.zero,
				attackerFiltering = AttackerFiltering.NeverHitSelf,
				crit = false,
				damageColorIndex = DamageColorIndex.Default,
				damageType = DamageType.Stun1s,
				falloffModel = BlastAttack.FalloffModel.None,
				procChainMask = default(ProcChainMask),
				position = base.transform.position,
				procCoefficient = 0f,
				teamIndex = (base.projectileController ? base.projectileController.teamFilter.teamIndex : TeamIndex.None),
				radius = this.pulseRadius
			};
			blastAttack.Fire();
			EffectData effectData = new EffectData();
			effectData.origin = blastAttack.position;
			effectData.scale = blastAttack.radius;
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/ExplosionVFX"), effectData, true);
		}
	}
}
