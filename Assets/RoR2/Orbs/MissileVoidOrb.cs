using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RoR2.Orbs
{
	// Token: 0x02000B1B RID: 2843
	public class MissileVoidOrb : GenericDamageOrb
	{
		// Token: 0x060040D6 RID: 16598 RVA: 0x0010C82D File Offset: 0x0010AA2D
		public override void Begin()
		{
			this.speed = 75f;
			base.Begin();
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x0010C840 File Offset: 0x0010AA40
		protected override GameObject GetOrbEffect()
		{
			return Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/MissileVoid/MissileVoidOrbEffect.prefab").WaitForCompletion();
		}
	}
}
