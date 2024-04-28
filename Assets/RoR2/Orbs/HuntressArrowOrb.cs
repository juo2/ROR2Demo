using System;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B05 RID: 2821
	public class HuntressArrowOrb : GenericDamageOrb
	{
		// Token: 0x06004091 RID: 16529 RVA: 0x0010ADE1 File Offset: 0x00108FE1
		public override void Begin()
		{
			this.speed = 120f;
			base.Begin();
		}

		// Token: 0x06004092 RID: 16530 RVA: 0x0010ADF4 File Offset: 0x00108FF4
		protected override GameObject GetOrbEffect()
		{
			return LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/ArrowOrbEffect");
		}
	}
}
