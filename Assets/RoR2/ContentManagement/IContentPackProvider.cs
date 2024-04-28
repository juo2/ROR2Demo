using System;
using System.Collections;

namespace RoR2.ContentManagement
{
	// Token: 0x02000E2B RID: 3627
	public interface IContentPackProvider
	{
		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x0600534C RID: 21324
		string identifier { get; }

		// Token: 0x0600534D RID: 21325
		IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args);

		// Token: 0x0600534E RID: 21326
		IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args);

		// Token: 0x0600534F RID: 21327
		IEnumerator FinalizeAsync(FinalizeAsyncArgs args);
	}
}
