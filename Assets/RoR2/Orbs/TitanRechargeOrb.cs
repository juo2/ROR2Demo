using System;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B23 RID: 2851
	public class TitanRechargeOrb : Orb
	{
		// Token: 0x06004107 RID: 16647 RVA: 0x0010D364 File Offset: 0x0010B564
		public override void Begin()
		{
			base.duration = 1f;
			EffectData effectData = new EffectData
			{
				origin = this.origin,
				genericFloat = base.duration
			};
			effectData.SetHurtBoxReference(this.target);
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/HealthOrbEffect"), effectData, true);
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnArrival()
		{
		}

		// Token: 0x04003F77 RID: 16247
		public int targetRockInt;

		// Token: 0x04003F78 RID: 16248
		public TitanRockController titanRockController;
	}
}
