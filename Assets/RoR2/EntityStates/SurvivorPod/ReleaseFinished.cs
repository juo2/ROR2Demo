using System;

namespace EntityStates.SurvivorPod
{
	// Token: 0x020001BA RID: 442
	public class ReleaseFinished : SurvivorPodBaseState
	{
		// Token: 0x060007F0 RID: 2032 RVA: 0x00021D5C File Offset: 0x0001FF5C
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Base", "Release");
		}
	}
}
