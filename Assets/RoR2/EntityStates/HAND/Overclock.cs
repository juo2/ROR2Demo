using System;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.HAND
{
	// Token: 0x02000332 RID: 818
	public class Overclock : BaseState
	{
		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003F8FC File Offset: 0x0003DAFC
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				base.characterBody;
			}
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0003F917 File Offset: 0x0003DB17
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > Overclock.baseDuration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0400126D RID: 4717
		public static float baseDuration = 0.25f;

		// Token: 0x0400126E RID: 4718
		public static GameObject healEffectPrefab;

		// Token: 0x0400126F RID: 4719
		public static float healPercentage = 0.15f;
	}
}
