using System;
using JetBrains.Annotations;

namespace RoR2.ContentManagement
{
	// Token: 0x02000E30 RID: 3632
	public abstract class NamedAssetCollection
	{
		// Token: 0x06005356 RID: 21334
		[CanBeNull]
		public abstract bool Find([NotNull] string assetName, out object result);
	}
}
