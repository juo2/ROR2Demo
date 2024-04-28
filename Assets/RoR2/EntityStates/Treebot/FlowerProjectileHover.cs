using System;
using System.Collections.Generic;
using EntityStates.Toolbot;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Treebot
{
	// Token: 0x0200017A RID: 378
	public class FlowerProjectileHover : DroneProjectileHover
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x0001C814 File Offset: 0x0001AA14
		public override void OnEnter()
		{
			base.OnEnter();
			ProjectileController component = base.GetComponent<ProjectileController>();
			if (component)
			{
				this.owner = component.owner;
				this.procChainMask = component.procChainMask;
				this.procCoefficient = component.procCoefficient;
				this.teamIndex = component.teamFilter.teamIndex;
			}
			ProjectileDamage component2 = base.GetComponent<ProjectileDamage>();
			if (component2)
			{
				this.damage = component2.damage;
				this.damageType = component2.damageType;
				this.crit = component2.crit;
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001C8A0 File Offset: 0x0001AAA0
		private void FirstPulse()
		{
			Vector3 position = base.transform.position;
			BullseyeSearch bullseyeSearch = new BullseyeSearch();
			bullseyeSearch.searchOrigin = position;
			bullseyeSearch.teamMaskFilter = TeamMask.GetEnemyTeams(this.teamIndex);
			bullseyeSearch.maxDistanceFilter = this.pulseRadius;
			bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
			bullseyeSearch.filterByLoS = true;
			bullseyeSearch.filterByDistinctEntity = true;
			bullseyeSearch.RefreshCandidates();
			IEnumerable<HurtBox> results = bullseyeSearch.GetResults();
			int num = 0;
			foreach (HurtBox hurtBox in results)
			{
				num++;
				Vector3 a = hurtBox.transform.position - position;
				float magnitude = a.magnitude;
				Vector3 a2 = a / magnitude;
				Rigidbody component = hurtBox.healthComponent.GetComponent<Rigidbody>();
				float num2 = component ? component.mass : 1f;
				float num3 = FlowerProjectileHover.yankSuitabilityCurve.Evaluate(num2);
				Vector3 force = a2 * (num2 * num3 * -FlowerProjectileHover.yankSpeed);
				DamageInfo damageInfo = new DamageInfo
				{
					attacker = this.owner,
					inflictor = base.gameObject,
					crit = this.crit,
					damage = this.damage,
					damageColorIndex = DamageColorIndex.Default,
					damageType = this.damageType,
					force = force,
					position = hurtBox.transform.position,
					procChainMask = this.procChainMask,
					procCoefficient = this.procCoefficient
				};
				hurtBox.healthComponent.TakeDamage(damageInfo);
			}
			this.healPulseHealthFractionValue = (float)num * FlowerProjectileHover.healthFractionYieldPerHit / (float)(this.pulseCount - 1);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001CA5C File Offset: 0x0001AC5C
		private void HealPulse()
		{
			if (this.owner)
			{
				HealthComponent component = this.owner.GetComponent<HealthComponent>();
				if (component)
				{
					component.HealFraction(this.healPulseHealthFractionValue, this.procChainMask);
				}
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001CA9D File Offset: 0x0001AC9D
		protected override void Pulse()
		{
			if (this.pulses == 1)
			{
				this.FirstPulse();
				return;
			}
			this.HealPulse();
		}

		// Token: 0x0400081F RID: 2079
		public static float yankSpeed;

		// Token: 0x04000820 RID: 2080
		public static AnimationCurve yankSuitabilityCurve;

		// Token: 0x04000821 RID: 2081
		public static float healthFractionYieldPerHit;

		// Token: 0x04000822 RID: 2082
		private GameObject owner;

		// Token: 0x04000823 RID: 2083
		private ProcChainMask procChainMask;

		// Token: 0x04000824 RID: 2084
		private float procCoefficient;

		// Token: 0x04000825 RID: 2085
		private float damage;

		// Token: 0x04000826 RID: 2086
		private DamageType damageType;

		// Token: 0x04000827 RID: 2087
		private bool crit;

		// Token: 0x04000828 RID: 2088
		private TeamIndex teamIndex = TeamIndex.None;

		// Token: 0x04000829 RID: 2089
		private float healPulseHealthFractionValue;
	}
}
