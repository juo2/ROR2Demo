using System;

namespace RoR2.Navigation
{
	// Token: 0x02000B29 RID: 2857
	public interface IBlockMapSearchFilter<TItem>
	{
		// Token: 0x06004113 RID: 16659
		bool CheckItem(TItem item, ref bool shouldFinish);
	}
}
