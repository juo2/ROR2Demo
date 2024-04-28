using System;
using System.Collections.Generic;
using RoR2.ContentManagement;
using RoR2.Modding;

namespace RoR2
{
	// Token: 0x020004F8 RID: 1272
	public class CatalogModHelperProxy<TEntry>
	{
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06001727 RID: 5927 RVA: 0x0006637C File Offset: 0x0006457C
		// (remove) Token: 0x06001728 RID: 5928 RVA: 0x000026ED File Offset: 0x000008ED
		[Obsolete("Use IContentPackProvider instead.")]
		public event Action<List<TEntry>> getAdditionalEntries
		{
			add
			{
				LegacyModContentPackProvider.instance.HandleLegacyGetAdditionalEntries<TEntry>(this.name, value, this.dest);
			}
			remove
			{
			}
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00066395 File Offset: 0x00064595
		public CatalogModHelperProxy(string name, NamedAssetCollection<TEntry> dest)
		{
			this.name = name;
			this.dest = dest;
		}

		// Token: 0x04001CD8 RID: 7384
		private string name;

		// Token: 0x04001CD9 RID: 7385
		private NamedAssetCollection<TEntry> dest;
	}
}
