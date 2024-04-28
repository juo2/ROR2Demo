using System;
using UnityEngine.Networking;

namespace EntityStates.Engi.Mine
{
	// Token: 0x0200039B RID: 923
	public class MineArmingWeak : BaseMineArmingState
	{
		// Token: 0x0600108E RID: 4238 RVA: 0x0004880F File Offset: 0x00046A0F
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && MineArmingWeak.duration <= base.fixedAge)
			{
				this.outer.SetNextState(new MineArmingFull());
			}
		}

		// Token: 0x040014E9 RID: 5353
		public static float duration;
	}
}
