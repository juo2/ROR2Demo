using System;

namespace EntityStates.Missions.BrotherEncounter
{
	// Token: 0x02000256 RID: 598
	public class EncounterFinished : BrotherEncounterBaseState
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldEnableArenaNodes
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002BBE6 File Offset: 0x00029DE6
		public override void OnEnter()
		{
			base.OnEnter();
		}
	}
}
