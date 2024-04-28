using System;

namespace EntityStates
{
	// Token: 0x020000CE RID: 206
	public class LockSkill : BaseState
	{
		// Token: 0x060003BF RID: 959 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}
	}
}
