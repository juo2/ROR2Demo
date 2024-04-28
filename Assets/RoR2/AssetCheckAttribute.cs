using System;
using JetBrains.Annotations;

namespace RoR2
{
	// Token: 0x020009F2 RID: 2546
	[MeansImplicitUse]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class AssetCheckAttribute : Attribute
	{
		// Token: 0x06003AE7 RID: 15079 RVA: 0x000F420E File Offset: 0x000F240E
		public AssetCheckAttribute(Type assetType)
		{
			this.assetType = assetType;
		}

		// Token: 0x040039B4 RID: 14772
		public Type assetType;
	}
}
