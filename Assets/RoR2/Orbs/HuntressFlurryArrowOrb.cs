using System;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B06 RID: 2822
	public class HuntressFlurryArrowOrb : HuntressArrowOrb
	{
		// Token: 0x06004094 RID: 16532 RVA: 0x0010AE08 File Offset: 0x00109008
		public override void Begin()
		{
			base.Begin();
			this.speed = 80f;
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x0010AE1B File Offset: 0x0010901B
		protected override GameObject GetOrbEffect()
		{
			if (this.isCrit)
			{
				return LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/FlurryArrowCritOrbEffect");
			}
			return LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/FlurryArrowOrbEffect");
		}
	}
}
