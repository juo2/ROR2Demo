using System;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B1A RID: 2842
	public class MicroMissileOrb : GenericDamageOrb
	{
		// Token: 0x060040D3 RID: 16595 RVA: 0x0010C80E File Offset: 0x0010AA0E
		public override void Begin()
		{
			this.speed = 55f;
			base.Begin();
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x0010C821 File Offset: 0x0010AA21
		protected override GameObject GetOrbEffect()
		{
			return LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/MicroMissileOrbEffect");
		}
	}
}
