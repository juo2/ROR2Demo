using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B09 RID: 2825
	public class ChainGunOrb : GenericDamageOrb
	{
		// Token: 0x0600409E RID: 16542 RVA: 0x0010B163 File Offset: 0x00109363
		public ChainGunOrb(GameObject orbEffectObject)
		{
			this.orbEffectPrefab = orbEffectObject;
			this.bouncedObjects = new List<HealthComponent>();
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x0010B19A File Offset: 0x0010939A
		protected override GameObject GetOrbEffect()
		{
			return this.orbEffectPrefab;
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x0010B1A4 File Offset: 0x001093A4
		public override void OnArrival()
		{
			if (this.target)
			{
				HealthComponent healthComponent = this.target.healthComponent;
				if (healthComponent)
				{
					DamageInfo damageInfo = new DamageInfo();
					damageInfo.damage = this.damageValue;
					damageInfo.attacker = this.attacker;
					damageInfo.force = Vector3.zero;
					damageInfo.crit = this.isCrit;
					damageInfo.procChainMask = this.procChainMask;
					damageInfo.procCoefficient = this.procCoefficient;
					damageInfo.position = this.target.transform.position;
					damageInfo.damageColorIndex = this.damageColorIndex;
					damageInfo.damageType = this.damageType;
					healthComponent.TakeDamage(damageInfo);
					GlobalEventManager.instance.OnHitEnemy(damageInfo, healthComponent.gameObject);
					GlobalEventManager.instance.OnHitAll(damageInfo, healthComponent.gameObject);
				}
				if (this.bouncesRemaining > 0)
				{
					for (int i = 0; i < this.targetsToFindPerBounce; i++)
					{
						if (this.bouncedObjects != null)
						{
							if (this.canBounceOnSameTarget)
							{
								this.bouncedObjects.Clear();
							}
							this.bouncedObjects.Add(this.target.healthComponent);
						}
						HurtBox hurtBox = this.PickNextTarget(this.target.transform.position);
						if (hurtBox)
						{
							ChainGunOrb chainGunOrb = new ChainGunOrb(this.orbEffectPrefab);
							chainGunOrb.search = this.search;
							chainGunOrb.origin = this.target.transform.position;
							chainGunOrb.target = hurtBox;
							chainGunOrb.attacker = this.attacker;
							chainGunOrb.teamIndex = this.teamIndex;
							chainGunOrb.damageValue = this.damageValue * this.damageCoefficientPerBounce;
							chainGunOrb.bouncesRemaining = this.bouncesRemaining - 1;
							chainGunOrb.isCrit = this.isCrit;
							chainGunOrb.bouncedObjects = this.bouncedObjects;
							chainGunOrb.procChainMask = this.procChainMask;
							chainGunOrb.procCoefficient = this.procCoefficient;
							chainGunOrb.damageColorIndex = this.damageColorIndex;
							chainGunOrb.damageCoefficientPerBounce = this.damageCoefficientPerBounce;
							chainGunOrb.speed = this.speed;
							chainGunOrb.bounceRange = this.bounceRange;
							chainGunOrb.damageType = this.damageType;
							OrbManager.instance.AddOrb(chainGunOrb);
						}
					}
				}
			}
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x0010B3EC File Offset: 0x001095EC
		public HurtBox PickNextTarget(Vector3 position)
		{
			if (this.search == null)
			{
				this.search = new BullseyeSearch();
			}
			this.search.searchOrigin = position;
			this.search.searchDirection = Vector3.zero;
			this.search.teamMaskFilter = TeamMask.allButNeutral;
			this.search.teamMaskFilter.RemoveTeam(this.teamIndex);
			this.search.filterByLoS = false;
			this.search.sortMode = BullseyeSearch.SortMode.Distance;
			this.search.maxDistanceFilter = this.bounceRange;
			this.search.RefreshCandidates();
			HurtBox hurtBox = (from v in this.search.GetResults()
			where !this.bouncedObjects.Contains(v.healthComponent)
			select v).FirstOrDefault<HurtBox>();
			if (hurtBox)
			{
				this.bouncedObjects.Add(hurtBox.healthComponent);
			}
			return hurtBox;
		}

		// Token: 0x04003EE9 RID: 16105
		private GameObject orbEffectPrefab;

		// Token: 0x04003EEA RID: 16106
		public int bouncesRemaining;

		// Token: 0x04003EEB RID: 16107
		public float bounceRange = 20f;

		// Token: 0x04003EEC RID: 16108
		public float damageCoefficientPerBounce = 1f;

		// Token: 0x04003EED RID: 16109
		public int targetsToFindPerBounce = 1;

		// Token: 0x04003EEE RID: 16110
		public bool canBounceOnSameTarget;

		// Token: 0x04003EEF RID: 16111
		private List<HealthComponent> bouncedObjects;

		// Token: 0x04003EF0 RID: 16112
		private BullseyeSearch search;
	}
}
