using System;
using UnityEngine.Networking;

namespace EntityStates.Engi.Mine
{
	// Token: 0x0200039E RID: 926
	public class WaitForStick : BaseMineState
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool shouldStick
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldRevertToWaitForStickOnSurfaceLost
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x000488FF File Offset: 0x00046AFF
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				base.armingStateMachine.SetNextState(new MineArmingUnarmed());
			}
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0004891E File Offset: 0x00046B1E
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && base.projectileStickOnImpact.stuck)
			{
				this.outer.SetNextState(new Arm());
			}
		}
	}
}
