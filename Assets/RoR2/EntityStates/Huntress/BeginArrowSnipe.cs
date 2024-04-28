using System;

namespace EntityStates.Huntress
{
	// Token: 0x02000314 RID: 788
	public class BeginArrowSnipe : BaseBeginArrowBarrage
	{
		// Token: 0x06000E13 RID: 3603 RVA: 0x0003C162 File Offset: 0x0003A362
		protected override EntityState InstantiateNextState()
		{
			return new AimArrowSnipe();
		}
	}
}
