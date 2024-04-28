using System;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B13 RID: 2835
	public class InfusionOrb : Orb
	{
		// Token: 0x060040B8 RID: 16568 RVA: 0x0010BBEC File Offset: 0x00109DEC
		public override void Begin()
		{
			base.duration = base.distanceToTarget / 30f;
			EffectData effectData = new EffectData
			{
				origin = this.origin,
				genericFloat = base.duration
			};
			effectData.SetHurtBoxReference(this.target);
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/InfusionOrbEffect"), effectData, true);
			HurtBox component = this.target.GetComponent<HurtBox>();
			CharacterBody characterBody = (component != null) ? component.healthComponent.GetComponent<CharacterBody>() : null;
			if (characterBody)
			{
				this.targetInventory = characterBody.inventory;
			}
		}

		// Token: 0x060040B9 RID: 16569 RVA: 0x0010BC77 File Offset: 0x00109E77
		public override void OnArrival()
		{
			if (this.targetInventory)
			{
				this.targetInventory.AddInfusionBonus((uint)this.maxHpValue);
			}
		}

		// Token: 0x04003F15 RID: 16149
		private const float speed = 30f;

		// Token: 0x04003F16 RID: 16150
		public int maxHpValue;

		// Token: 0x04003F17 RID: 16151
		private Inventory targetInventory;
	}
}
