using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RoR2.Orbs
{
	// Token: 0x02000B24 RID: 2852
	public class VendingMachineOrb : Orb
	{
		// Token: 0x0600410A RID: 16650 RVA: 0x0010D3B8 File Offset: 0x0010B5B8
		public override void Begin()
		{
			if (this.target)
			{
				base.duration = Mathf.Max(1f, base.distanceToTarget / this.speed);
				float scale = this.scaleOrb ? Mathf.Min(this.healFraction, 1f) : 1f;
				EffectData effectData = new EffectData
				{
					scale = scale,
					origin = this.origin,
					genericFloat = base.duration
				};
				effectData.SetHurtBoxReference(this.target);
				EffectManager.SpawnEffect(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VendingMachine/VendingMachineOrbEffect.prefab").WaitForCompletion(), effectData, true);
			}
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x0010D45C File Offset: 0x0010B65C
		public override void OnArrival()
		{
			if (this.target)
			{
				HealthComponent healthComponent = this.target.healthComponent;
				if (healthComponent)
				{
					healthComponent.HealFraction(this.healFraction, default(ProcChainMask));
				}
			}
		}

		// Token: 0x04003F79 RID: 16249
		public float healFraction;

		// Token: 0x04003F7A RID: 16250
		public bool scaleOrb = true;

		// Token: 0x04003F7B RID: 16251
		public float speed = 10f;
	}
}
