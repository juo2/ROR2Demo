using System;
using UnityEngine;

namespace EntityStates.ClayBoss
{
	// Token: 0x0200040A RID: 1034
	public class RecoverExit : BaseState
	{
		// Token: 0x06001298 RID: 4760 RVA: 0x0005336D File Offset: 0x0005156D
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			base.PlayAnimation("Body", "ExitSiphon", "ExitSiphon.playbackRate", RecoverExit.exitDuration);
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0005339A File Offset: 0x0005159A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= RecoverExit.exitDuration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x04001802 RID: 6146
		public static float exitDuration = 1f;

		// Token: 0x04001803 RID: 6147
		private float stopwatch;
	}
}
