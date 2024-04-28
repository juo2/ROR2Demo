using System;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B0F RID: 2831
	public class GoldOrb : Orb
	{
		// Token: 0x060040AE RID: 16558 RVA: 0x0010B8FC File Offset: 0x00109AFC
		public override void Begin()
		{
			if (this.target)
			{
				base.duration = this.overrideDuration;
				float scale = this.scaleOrb ? Mathf.Min(this.goldAmount / Run.instance.difficultyCoefficient, 1f) : 1f;
				EffectData effectData = new EffectData
				{
					scale = scale,
					origin = this.origin,
					genericFloat = base.duration
				};
				effectData.SetHurtBoxReference(this.target);
				EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/GoldOrbEffect"), effectData, true);
			}
		}

		// Token: 0x060040AF RID: 16559 RVA: 0x0010B994 File Offset: 0x00109B94
		public override void OnArrival()
		{
			if (this.target && this.target.healthComponent && this.target.healthComponent.body && this.target.healthComponent.body.master)
			{
				this.target.healthComponent.body.master.GiveMoney(this.goldAmount);
			}
		}

		// Token: 0x04003F0D RID: 16141
		public uint goldAmount;

		// Token: 0x04003F0E RID: 16142
		public bool scaleOrb = true;

		// Token: 0x04003F0F RID: 16143
		public float overrideDuration = 0.6f;
	}
}
