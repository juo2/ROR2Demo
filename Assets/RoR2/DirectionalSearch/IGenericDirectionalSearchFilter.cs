using System;

namespace RoR2.DirectionalSearch
{
	// Token: 0x02000C9D RID: 3229
	public interface IGenericDirectionalSearchFilter<TSource>
	{
		// Token: 0x060049D2 RID: 18898
		bool PassesFilter(TSource candidateInfo);
	}
}
