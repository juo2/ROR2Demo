using System;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B10 RID: 2832
	public class HauntOrb : Orb
	{
		// Token: 0x060040B1 RID: 16561 RVA: 0x0010BA30 File Offset: 0x00109C30
		public override void Begin()
		{
			base.duration = this.timeToArrive + UnityEngine.Random.Range(0f, 0.4f);
			EffectData effectData = new EffectData
			{
				scale = this.scale,
				origin = this.origin,
				genericFloat = base.duration
			};
			effectData.SetHurtBoxReference(this.target);
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/HauntOrbEffect"), effectData, true);
		}

		// Token: 0x060040B2 RID: 16562 RVA: 0x0010BAA0 File Offset: 0x00109CA0
		public override void OnArrival()
		{
			if (this.target)
			{
				HealthComponent healthComponent = this.target.healthComponent;
				if (healthComponent && healthComponent.body)
				{
					healthComponent.body.AddTimedBuff(RoR2Content.Buffs.AffixHaunted, 15f);
				}
			}
		}

		// Token: 0x04003F10 RID: 16144
		public float timeToArrive;

		// Token: 0x04003F11 RID: 16145
		public float scale;
	}
}
