using System;

namespace EntityStates.Huntress
{
	// Token: 0x02000319 RID: 793
	public class BeginArrowRain : BaseBeginArrowBarrage
	{
		// Token: 0x06000E2D RID: 3629 RVA: 0x0003CAD4 File Offset: 0x0003ACD4
		protected override EntityState InstantiateNextState()
		{
			return new ArrowRain();
		}
	}
}
