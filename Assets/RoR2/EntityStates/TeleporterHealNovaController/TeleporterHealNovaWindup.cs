using System;
using RoR2;
using UnityEngine;

namespace EntityStates.TeleporterHealNovaController
{
	// Token: 0x020001B1 RID: 433
	public class TeleporterHealNovaWindup : BaseState
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x000213A2 File Offset: 0x0001F5A2
		public override void OnEnter()
		{
			base.OnEnter();
			EffectManager.SimpleEffect(TeleporterHealNovaWindup.chargeEffectPrefab, base.transform.position, Quaternion.identity, false);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000213C5 File Offset: 0x0001F5C5
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && TeleporterHealNovaWindup.duration <= base.fixedAge)
			{
				this.outer.SetNextState(new TeleporterHealNovaPulse());
			}
		}

		// Token: 0x04000956 RID: 2390
		public static GameObject chargeEffectPrefab;

		// Token: 0x04000957 RID: 2391
		public static float duration;
	}
}
