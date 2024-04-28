using System;
using RoR2;

namespace EntityStates.MoonElevator
{
	// Token: 0x0200023A RID: 570
	public class InactiveToReady : MoonElevatorBaseState
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override Interactability interactability
		{
			get
			{
				return Interactability.Disabled;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override bool goToNextStateAutomatically
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00029C1B File Offset: 0x00027E1B
		public override EntityState nextState
		{
			get
			{
				return new Ready();
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override bool showBaseEffects
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00029C22 File Offset: 0x00027E22
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Base", "InactiveToActive", "playbackRate", this.duration);
		}
	}
}
