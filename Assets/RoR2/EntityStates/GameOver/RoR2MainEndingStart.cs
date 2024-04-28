using System;
using UnityEngine.Networking;

namespace EntityStates.GameOver
{
	// Token: 0x02000378 RID: 888
	public class RoR2MainEndingStart : BaseGameOverControllerState
	{
		// Token: 0x06000FF4 RID: 4084 RVA: 0x00046C37 File Offset: 0x00044E37
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.fixedAge >= RoR2MainEndingStart.duration)
			{
				this.outer.SetNextState(new RoR2MainEndingSetSceneAndWaitForPlayers());
			}
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00046C63 File Offset: 0x00044E63
		public override void OnEnter()
		{
			base.OnEnter();
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x04001486 RID: 5254
		public static float duration = 6f;
	}
}
