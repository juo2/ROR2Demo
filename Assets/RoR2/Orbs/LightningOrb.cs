using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B16 RID: 2838
	public class LightningOrb : Orb
	{
		// Token: 0x060040C3 RID: 16579 RVA: 0x0010BF54 File Offset: 0x0010A154
		public override void Begin()
		{
			base.duration = 0.1f;
			string path = null;
			switch (this.lightningType)
			{
			case LightningOrb.LightningType.Ukulele:
				path = "Prefabs/Effects/OrbEffects/LightningOrbEffect";
				break;
			case LightningOrb.LightningType.Tesla:
				path = "Prefabs/Effects/OrbEffects/TeslaOrbEffect";
				break;
			case LightningOrb.LightningType.BFG:
				path = "Prefabs/Effects/OrbEffects/BeamSphereOrbEffect";
				base.duration = 0.4f;
				break;
			case LightningOrb.LightningType.TreePoisonDart:
				path = "Prefabs/Effects/OrbEffects/TreePoisonDartOrbEffect";
				this.speed = 40f;
				base.duration = base.distanceToTarget / this.speed;
				break;
			case LightningOrb.LightningType.HuntressGlaive:
				path = "Prefabs/Effects/OrbEffects/HuntressGlaiveOrbEffect";
				base.duration = base.distanceToTarget / this.speed;
				this.canBounceOnSameTarget = true;
				break;
			case LightningOrb.LightningType.Loader:
				path = "Prefabs/Effects/OrbEffects/LoaderLightningOrbEffect";
				break;
			case LightningOrb.LightningType.RazorWire:
				path = "Prefabs/Effects/OrbEffects/RazorwireOrbEffect";
				base.duration = 0.2f;
				break;
			case LightningOrb.LightningType.CrocoDisease:
				path = "Prefabs/Effects/OrbEffects/CrocoDiseaseOrbEffect";
				base.duration = 0.6f;
				this.targetsToFindPerBounce = 2;
				break;
			case LightningOrb.LightningType.MageLightning:
				path = "Prefabs/Effects/OrbEffects/MageLightningOrbEffect";
				base.duration = 0.1f;
				break;
			}
			EffectData effectData = new EffectData
			{
				origin = this.origin,
				genericFloat = base.duration
			};
			effectData.SetHurtBoxReference(this.target);
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>(path), effectData, true);
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x0010C098 File Offset: 0x0010A298
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
					damageInfo.inflictor = this.inflictor;
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
				this.failedToKill |= (!healthComponent || healthComponent.alive);
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
							LightningOrb lightningOrb = new LightningOrb();
							lightningOrb.search = this.search;
							lightningOrb.origin = this.target.transform.position;
							lightningOrb.target = hurtBox;
							lightningOrb.attacker = this.attacker;
							lightningOrb.inflictor = this.inflictor;
							lightningOrb.teamIndex = this.teamIndex;
							lightningOrb.damageValue = this.damageValue * this.damageCoefficientPerBounce;
							lightningOrb.bouncesRemaining = this.bouncesRemaining - 1;
							lightningOrb.isCrit = this.isCrit;
							lightningOrb.bouncedObjects = this.bouncedObjects;
							lightningOrb.lightningType = this.lightningType;
							lightningOrb.procChainMask = this.procChainMask;
							lightningOrb.procCoefficient = this.procCoefficient;
							lightningOrb.damageColorIndex = this.damageColorIndex;
							lightningOrb.damageCoefficientPerBounce = this.damageCoefficientPerBounce;
							lightningOrb.speed = this.speed;
							lightningOrb.range = this.range;
							lightningOrb.damageType = this.damageType;
							lightningOrb.failedToKill = this.failedToKill;
							OrbManager.instance.AddOrb(lightningOrb);
						}
					}
					return;
				}
				if (!this.failedToKill)
				{
					Action<LightningOrb> action = LightningOrb.onLightningOrbKilledOnAllBounces;
					if (action == null)
					{
						return;
					}
					action(this);
				}
			}
		}

		// Token: 0x060040C5 RID: 16581 RVA: 0x0010C344 File Offset: 0x0010A544
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
			this.search.maxDistanceFilter = this.range;
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

		// Token: 0x140000D9 RID: 217
		// (add) Token: 0x060040C6 RID: 16582 RVA: 0x0010C418 File Offset: 0x0010A618
		// (remove) Token: 0x060040C7 RID: 16583 RVA: 0x0010C44C File Offset: 0x0010A64C
		public static event Action<LightningOrb> onLightningOrbKilledOnAllBounces;

		// Token: 0x04003F23 RID: 16163
		public float speed = 100f;

		// Token: 0x04003F24 RID: 16164
		public float damageValue;

		// Token: 0x04003F25 RID: 16165
		public GameObject attacker;

		// Token: 0x04003F26 RID: 16166
		public GameObject inflictor;

		// Token: 0x04003F27 RID: 16167
		public int bouncesRemaining;

		// Token: 0x04003F28 RID: 16168
		public List<HealthComponent> bouncedObjects;

		// Token: 0x04003F29 RID: 16169
		public TeamIndex teamIndex;

		// Token: 0x04003F2A RID: 16170
		public bool isCrit;

		// Token: 0x04003F2B RID: 16171
		public ProcChainMask procChainMask;

		// Token: 0x04003F2C RID: 16172
		public float procCoefficient = 1f;

		// Token: 0x04003F2D RID: 16173
		public DamageColorIndex damageColorIndex;

		// Token: 0x04003F2E RID: 16174
		public float range = 20f;

		// Token: 0x04003F2F RID: 16175
		public float damageCoefficientPerBounce = 1f;

		// Token: 0x04003F30 RID: 16176
		public int targetsToFindPerBounce = 1;

		// Token: 0x04003F31 RID: 16177
		public DamageType damageType;

		// Token: 0x04003F32 RID: 16178
		private bool canBounceOnSameTarget;

		// Token: 0x04003F33 RID: 16179
		private bool failedToKill;

		// Token: 0x04003F34 RID: 16180
		public LightningOrb.LightningType lightningType;

		// Token: 0x04003F35 RID: 16181
		private BullseyeSearch search;

		// Token: 0x02000B17 RID: 2839
		public enum LightningType
		{
			// Token: 0x04003F38 RID: 16184
			Ukulele,
			// Token: 0x04003F39 RID: 16185
			Tesla,
			// Token: 0x04003F3A RID: 16186
			BFG,
			// Token: 0x04003F3B RID: 16187
			TreePoisonDart,
			// Token: 0x04003F3C RID: 16188
			HuntressGlaive,
			// Token: 0x04003F3D RID: 16189
			Loader,
			// Token: 0x04003F3E RID: 16190
			RazorWire,
			// Token: 0x04003F3F RID: 16191
			CrocoDisease,
			// Token: 0x04003F40 RID: 16192
			MageLightning,
			// Token: 0x04003F41 RID: 16193
			Count
		}
	}
}
