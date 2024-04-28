using System;

namespace EntityStates.Missions.BrotherEncounter
{
	// Token: 0x02000254 RID: 596
	public class Phase4 : BrotherEncounterPhaseBaseState
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0002BBC3 File Offset: 0x00029DC3
		protected override string phaseControllerChildString
		{
			get
			{
				return "Phase4";
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x0002BBCA File Offset: 0x00029DCA
		protected override EntityState nextState
		{
			get
			{
				return new BossDeath();
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0002BBD1 File Offset: 0x00029DD1
		protected override float healthBarShowDelay
		{
			get
			{
				return 6f;
			}
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0002BBD8 File Offset: 0x00029DD8
		public override void OnEnter()
		{
			base.OnEnter();
			base.BeginEncounter();
		}
	}
}
