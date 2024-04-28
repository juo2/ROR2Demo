using System;
using EntityStates;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006E4 RID: 1764
	public class GameEndingController : NetworkBehaviour
	{
		// Token: 0x060022DF RID: 8927 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x00096A2C File Offset: 0x00094C2C
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x020006E5 RID: 1765
		private class GameEndingControllerBaseState : BaseState
		{
			// Token: 0x170002C7 RID: 711
			// (get) Token: 0x060022E3 RID: 8931 RVA: 0x00096A3A File Offset: 0x00094C3A
			// (set) Token: 0x060022E4 RID: 8932 RVA: 0x00096A42 File Offset: 0x00094C42
			private protected GameEndingController gameEndingController { protected get; private set; }

			// Token: 0x060022E5 RID: 8933 RVA: 0x00096A4B File Offset: 0x00094C4B
			public override void OnEnter()
			{
				base.OnEnter();
				this.gameEndingController = base.GetComponent<GameEndingController>();
			}
		}

		// Token: 0x020006E6 RID: 1766
		private class EndingCutsceneState : GameEndingController.GameEndingControllerBaseState
		{
		}

		// Token: 0x020006E7 RID: 1767
		private class CreditsState : GameEndingController.GameEndingControllerBaseState
		{
		}

		// Token: 0x020006E8 RID: 1768
		private class PostGameReportState : GameEndingController.GameEndingControllerBaseState
		{
		}
	}
}
