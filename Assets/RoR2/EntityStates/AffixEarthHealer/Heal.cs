using System;
using System.Collections.Generic;
using RoR2;
using RoR2.Orbs;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.AffixEarthHealer
{
	// Token: 0x020004A1 RID: 1185
	public class Heal : BaseState
	{
		// Token: 0x06001546 RID: 5446 RVA: 0x0005E620 File Offset: 0x0005C820
		public override void OnEnter()
		{
			base.OnEnter();
			float healValue = base.characterBody.damage * Heal.healCoefficient;
			if (NetworkServer.active)
			{
				List<HealthComponent> list = new List<HealthComponent>();
				SphereSearch sphereSearch = new SphereSearch();
				sphereSearch.radius = Heal.radius;
				sphereSearch.origin = base.transform.position;
				sphereSearch.queryTriggerInteraction = QueryTriggerInteraction.Ignore;
				sphereSearch.mask = LayerIndex.entityPrecise.mask;
				sphereSearch.RefreshCandidates();
				sphereSearch.FilterCandidatesByDistinctHurtBoxEntities();
				HurtBox[] hurtBoxes = sphereSearch.GetHurtBoxes();
				for (int i = 0; i < hurtBoxes.Length; i++)
				{
					HealthComponent healthComponent = hurtBoxes[i].healthComponent;
					if (!list.Contains(healthComponent))
					{
						list.Add(healthComponent);
					}
				}
				foreach (HealthComponent healthComponent2 in list)
				{
					HealOrb healOrb = new HealOrb();
					healOrb.origin = base.transform.position;
					healOrb.target = healthComponent2.body.mainHurtBox;
					healOrb.healValue = healValue;
					healOrb.overrideDuration = Heal.healOrbTravelDuration;
					OrbManager.instance.AddOrb(healOrb);
				}
				EffectManager.SimpleEffect(Heal.effectPrefab, base.transform.position, Quaternion.identity, true);
				base.characterBody.healthComponent.Suicide(null, null, DamageType.Generic);
			}
		}

		// Token: 0x04001B19 RID: 6937
		public static float radius;

		// Token: 0x04001B1A RID: 6938
		public static float healCoefficient;

		// Token: 0x04001B1B RID: 6939
		public static float healOrbTravelDuration;

		// Token: 0x04001B1C RID: 6940
		public static GameObject effectPrefab;
	}
}
