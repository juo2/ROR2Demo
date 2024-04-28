using System;
using RoR2;

namespace EntityStates.GameOver
{
	// Token: 0x02000377 RID: 887
	public class BaseGameOverControllerState : BaseState
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x00046BEB File Offset: 0x00044DEB
		// (set) Token: 0x06000FEF RID: 4079 RVA: 0x00046BF3 File Offset: 0x00044DF3
		private protected GameOverController gameOverController { protected get; private set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x00046BFC File Offset: 0x00044DFC
		// (set) Token: 0x06000FF1 RID: 4081 RVA: 0x00046C04 File Offset: 0x00044E04
		private protected GameEndingDef gameEnding { protected get; private set; }

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00046C0D File Offset: 0x00044E0D
		public override void OnEnter()
		{
			base.OnEnter();
			this.gameOverController = base.GetComponent<GameOverController>();
			this.gameEnding = this.gameOverController.runReport.gameEnding;
		}
	}
}
