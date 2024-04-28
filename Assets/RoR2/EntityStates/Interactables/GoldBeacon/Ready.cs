using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Interactables.GoldBeacon
{
	// Token: 0x020002F5 RID: 757
	public class Ready : GoldBeaconBaseState
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x00038FA9 File Offset: 0x000371A9
		// (set) Token: 0x06000D81 RID: 3457 RVA: 0x00038FB0 File Offset: 0x000371B0
		public static int count { get; private set; }

		// Token: 0x06000D82 RID: 3458 RVA: 0x00038FB8 File Offset: 0x000371B8
		public override void OnEnter()
		{
			base.OnEnter();
			base.SetReady(true);
			Ready.count++;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00038FD4 File Offset: 0x000371D4
		public override void OnExit()
		{
			Ready.count--;
			if (!this.outer.destroying)
			{
				EffectManager.SpawnEffect(Ready.activationEffectPrefab, new EffectData
				{
					origin = base.transform.position,
					scale = 10f
				}, false);
			}
			base.OnExit();
		}

		// Token: 0x04001088 RID: 4232
		public static GameObject activationEffectPrefab;
	}
}
