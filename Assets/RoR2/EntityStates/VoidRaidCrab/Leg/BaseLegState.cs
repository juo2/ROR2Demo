using System;
using RoR2.VoidRaidCrab;

namespace EntityStates.VoidRaidCrab.Leg
{
	// Token: 0x02000138 RID: 312
	public class BaseLegState : EntityState
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00017DC0 File Offset: 0x00015FC0
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x00017DC8 File Offset: 0x00015FC8
		private protected LegController legController { protected get; private set; }

		// Token: 0x0600058E RID: 1422 RVA: 0x00017DD1 File Offset: 0x00015FD1
		public override void OnEnter()
		{
			base.OnEnter();
			this.legController = (this.legController ?? base.GetComponent<LegController>());
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00017DF0 File Offset: 0x00015FF0
		public override void ModifyNextState(EntityState nextState)
		{
			base.ModifyNextState(nextState);
			BaseLegState baseLegState;
			if ((baseLegState = (nextState as BaseLegState)) != null)
			{
				baseLegState.legController = this.legController;
			}
		}
	}
}
