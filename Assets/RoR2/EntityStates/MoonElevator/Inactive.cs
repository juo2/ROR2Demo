using System;
using RoR2;

namespace EntityStates.MoonElevator
{
	// Token: 0x02000239 RID: 569
	public class Inactive : MoonElevatorBaseState
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override Interactability interactability
		{
			get
			{
				return Interactability.ConditionsNotMet;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override bool goToNextStateAutomatically
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override bool showBaseEffects
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00029BFB File Offset: 0x00027DFB
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Base", "Inactive");
		}
	}
}
