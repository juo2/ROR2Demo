using System;

namespace RoR2.ContentManagement
{
	// Token: 0x02000E3B RID: 3643
	public class TargetAssetNameAttribute : Attribute
	{
		// Token: 0x060053A0 RID: 21408 RVA: 0x00159729 File Offset: 0x00157929
		public TargetAssetNameAttribute(string targetAssetName)
		{
			this.targetAssetName = targetAssetName;
		}

		// Token: 0x04004FA2 RID: 20386
		public readonly string targetAssetName;
	}
}
