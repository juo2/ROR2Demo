using System;
using RoR2;
using UnityEngine;

namespace EntityStates.LunarGolem
{
	// Token: 0x020002B6 RID: 694
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x06000C4A RID: 3146 RVA: 0x00033C71 File Offset: 0x00031E71
		public override void OnEnter()
		{
			base.OnEnter();
			if (DeathState.deathExplosionEffect)
			{
				EffectManager.SimpleMuzzleFlash(DeathState.deathExplosionEffect, base.gameObject, "Center", false);
			}
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00033C9B File Offset: 0x00031E9B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
		}

		// Token: 0x04000EF3 RID: 3827
		public static GameObject deathExplosionEffect;
	}
}
