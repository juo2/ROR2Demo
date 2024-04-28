using System;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B11 RID: 2833
	public class HealOrb : Orb
	{
		// Token: 0x060040B4 RID: 16564 RVA: 0x0010BAF0 File Offset: 0x00109CF0
		public override void Begin()
		{
			if (this.target)
			{
				base.duration = this.overrideDuration;
				float scale = this.scaleOrb ? Mathf.Min(this.healValue / this.target.healthComponent.fullHealth, 1f) : 1f;
				EffectData effectData = new EffectData
				{
					scale = scale,
					origin = this.origin,
					genericFloat = base.duration
				};
				effectData.SetHurtBoxReference(this.target);
				EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/HealthOrbEffect"), effectData, true);
			}
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x0010BB8C File Offset: 0x00109D8C
		public override void OnArrival()
		{
			if (this.target)
			{
				HealthComponent healthComponent = this.target.healthComponent;
				if (healthComponent)
				{
					healthComponent.Heal(this.healValue, default(ProcChainMask), true);
				}
			}
		}

		// Token: 0x04003F12 RID: 16146
		public float healValue;

		// Token: 0x04003F13 RID: 16147
		public bool scaleOrb = true;

		// Token: 0x04003F14 RID: 16148
		public float overrideDuration = 0.3f;
	}
}
