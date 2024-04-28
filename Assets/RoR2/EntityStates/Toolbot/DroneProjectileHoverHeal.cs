using System;
using System.Collections.Generic;
using System.Linq;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Toolbot
{
	// Token: 0x020001A3 RID: 419
	public class DroneProjectileHoverHeal : DroneProjectileHover
	{
		// Token: 0x06000782 RID: 1922 RVA: 0x000203B0 File Offset: 0x0001E5B0
		protected override void Pulse()
		{
			float num = 1f;
			ProjectileDamage component = base.GetComponent<ProjectileDamage>();
			if (component)
			{
				num = component.damage;
			}
			this.HealOccupants(this.pulseRadius, DroneProjectileHoverHeal.healPointsCoefficient * num, DroneProjectileHoverHeal.healFraction);
			EffectData effectData = new EffectData();
			effectData.origin = base.transform.position;
			effectData.scale = this.pulseRadius;
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/ExplosionVFX"), effectData, true);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00020428 File Offset: 0x0001E628
		private static HealthComponent SelectHealthComponent(Collider collider)
		{
			HurtBox component = collider.GetComponent<HurtBox>();
			if (component && component.healthComponent)
			{
				return component.healthComponent;
			}
			return null;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0002045C File Offset: 0x0001E65C
		private void HealOccupants(float radius, float healPoints, float healFraction)
		{
			IEnumerable<Collider> source = Physics.OverlapSphere(base.transform.position, radius, LayerIndex.entityPrecise.mask);
			TeamIndex teamIndex = this.teamFilter ? this.teamFilter.teamIndex : TeamIndex.None;
			IEnumerable<HealthComponent> source2 = source.Select(new Func<Collider, HealthComponent>(DroneProjectileHoverHeal.SelectHealthComponent));
			Func<HealthComponent, bool> <>9__0;
			Func<HealthComponent, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((HealthComponent healthComponent) => healthComponent && healthComponent.body.teamComponent.teamIndex == teamIndex));
			}
			foreach (HealthComponent healthComponent2 in source2.Where(predicate).Distinct<HealthComponent>())
			{
				float num = healPoints + healthComponent2.fullHealth * healFraction;
				if (num > 0f)
				{
					healthComponent2.Heal(num, default(ProcChainMask), true);
				}
			}
		}

		// Token: 0x04000914 RID: 2324
		public static float healPointsCoefficient;

		// Token: 0x04000915 RID: 2325
		public static float healFraction;
	}
}
