using System;
using RoR2;

namespace EntityStates.MoonElevator
{
	// Token: 0x0200023B RID: 571
	public class Ready : MoonElevatorBaseState
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override Interactability interactability
		{
			get
			{
				return Interactability.Available;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override bool goToNextStateAutomatically
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override bool showBaseEffects
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00029C45 File Offset: 0x00027E45
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Base", "Ready");
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00029C5D File Offset: 0x00027E5D
		protected override void OnInteractionBegin(Interactor activator)
		{
			base.OnInteractionBegin(activator);
			bool isAuthority = base.isAuthority;
		}
	}
}
