using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.BrotherMonster
{
	// Token: 0x02000433 RID: 1075
	public class InstantDeathState : GenericCharacterDeath
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldAutoDestroy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00038373 File Offset: 0x00036573
		public override void OnEnter()
		{
			base.OnEnter();
			base.DestroyModel();
			if (NetworkServer.active)
			{
				base.DestroyBodyAsapServer();
			}
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00055D6B File Offset: 0x00053F6B
		protected override void CreateDeathEffects()
		{
			base.CreateDeathEffects();
			EffectManager.SimpleMuzzleFlash(InstantDeathState.deathEffectPrefab, base.gameObject, "MuzzleCenter", false);
		}

		// Token: 0x040018BA RID: 6330
		public static GameObject deathEffectPrefab;
	}
}
