using System;
using UnityEngine.Networking;

namespace EntityStates.Missions.BrotherEncounter
{
	// Token: 0x02000255 RID: 597
	public class BossDeath : BrotherEncounterBaseState
	{
		// Token: 0x06000A9E RID: 2718 RVA: 0x0002BBE6 File Offset: 0x00029DE6
		public override void OnEnter()
		{
			base.OnEnter();
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0002BBEE File Offset: 0x00029DEE
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				this.outer.SetNextState(new EncounterFinished());
			}
		}
	}
}
