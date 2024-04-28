using System;
using UnityEngine;

namespace RoR2.DirectionalSearch
{
	// Token: 0x02000C9C RID: 3228
	public interface IGenericWorldSearchSelector<TSource>
	{
		// Token: 0x060049D0 RID: 18896
		Transform GetTransform(TSource source);

		// Token: 0x060049D1 RID: 18897
		GameObject GetRootObject(TSource source);
	}
}
