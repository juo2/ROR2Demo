using System;
using System.Collections;

namespace RoR2.ContentManagement
{
	// Token: 0x02000E37 RID: 3639
	public class SimpleContentPackProvider : IContentPackProvider
	{
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06005388 RID: 21384 RVA: 0x001596A9 File Offset: 0x001578A9
		// (set) Token: 0x06005389 RID: 21385 RVA: 0x001596B1 File Offset: 0x001578B1
		public string identifier { get; set; }

		// Token: 0x0600538A RID: 21386 RVA: 0x001596BA File Offset: 0x001578BA
		public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
		{
			SimpleContentPackProvider.LoadStaticContentAsyncDelegate loadStaticContentImplementation = this.loadStaticContentImplementation;
			if (loadStaticContentImplementation == null)
			{
				return null;
			}
			return loadStaticContentImplementation(args);
		}

		// Token: 0x0600538B RID: 21387 RVA: 0x001596CE File Offset: 0x001578CE
		public IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
		{
			SimpleContentPackProvider.GenerateContentPackAsyncDelegate generateContentPackAsyncImplementation = this.generateContentPackAsyncImplementation;
			if (generateContentPackAsyncImplementation == null)
			{
				return null;
			}
			return generateContentPackAsyncImplementation(args);
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x001596E2 File Offset: 0x001578E2
		public IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
		{
			SimpleContentPackProvider.FinalizeAsyncDelegate finalizeAsyncImplementation = this.finalizeAsyncImplementation;
			if (finalizeAsyncImplementation == null)
			{
				return null;
			}
			return finalizeAsyncImplementation(args);
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600538D RID: 21389 RVA: 0x001596F6 File Offset: 0x001578F6
		// (set) Token: 0x0600538E RID: 21390 RVA: 0x001596FE File Offset: 0x001578FE
		public SimpleContentPackProvider.LoadStaticContentAsyncDelegate loadStaticContentImplementation { get; set; }

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x0600538F RID: 21391 RVA: 0x00159707 File Offset: 0x00157907
		// (set) Token: 0x06005390 RID: 21392 RVA: 0x0015970F File Offset: 0x0015790F
		public SimpleContentPackProvider.GenerateContentPackAsyncDelegate generateContentPackAsyncImplementation { get; set; }

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06005391 RID: 21393 RVA: 0x00159718 File Offset: 0x00157918
		// (set) Token: 0x06005392 RID: 21394 RVA: 0x00159720 File Offset: 0x00157920
		public SimpleContentPackProvider.FinalizeAsyncDelegate finalizeAsyncImplementation { get; set; }

		// Token: 0x02000E38 RID: 3640
		// (Invoke) Token: 0x06005395 RID: 21397
		public delegate IEnumerator LoadStaticContentAsyncDelegate(LoadStaticContentAsyncArgs args);

		// Token: 0x02000E39 RID: 3641
		// (Invoke) Token: 0x06005399 RID: 21401
		public delegate IEnumerator GenerateContentPackAsyncDelegate(GetContentPackAsyncArgs args);

		// Token: 0x02000E3A RID: 3642
		// (Invoke) Token: 0x0600539D RID: 21405
		public delegate IEnumerator FinalizeAsyncDelegate(FinalizeAsyncArgs args);
	}
}
