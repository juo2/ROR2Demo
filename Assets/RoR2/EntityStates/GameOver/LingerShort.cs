using System;
using UnityEngine.Networking;

namespace EntityStates.GameOver
{
	// Token: 0x0200037B RID: 891
	public class LingerShort : BaseGameOverControllerState
	{
		// Token: 0x06001000 RID: 4096 RVA: 0x00046D8E File Offset: 0x00044F8E
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge >= LingerShort.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x04001488 RID: 5256
		private static readonly float duration = 3f;
	}
}
