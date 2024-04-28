using System;

namespace RoR2.ContentManagement
{
	// Token: 0x02000E2C RID: 3628
	public struct ContentPackLoadInfo
	{
		// Token: 0x04004F6F RID: 20335
		public int index;

		// Token: 0x04004F70 RID: 20336
		public string contentPackProviderIdentifier;

		// Token: 0x04004F71 RID: 20337
		public IContentPackProvider contentPackProvider;

		// Token: 0x04004F72 RID: 20338
		public ReadOnlyContentPack previousContentPack;

		// Token: 0x04004F73 RID: 20339
		public int retries;
	}
}
