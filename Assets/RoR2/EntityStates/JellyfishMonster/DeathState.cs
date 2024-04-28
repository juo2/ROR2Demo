using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.JellyfishMonster
{
	// Token: 0x020002EA RID: 746
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x06000D53 RID: 3411 RVA: 0x00038373 File Offset: 0x00036573
		public override void OnEnter()
		{
			base.OnEnter();
			base.DestroyModel();
			if (NetworkServer.active)
			{
				base.DestroyBodyAsapServer();
			}
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0003838E File Offset: 0x0003658E
		protected override void CreateDeathEffects()
		{
			base.CreateDeathEffects();
			if (DeathState.enterEffectPrefab)
			{
				EffectManager.SimpleEffect(DeathState.enterEffectPrefab, base.transform.position, base.transform.rotation, false);
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001053 RID: 4179
		public static GameObject enterEffectPrefab;
	}
}
