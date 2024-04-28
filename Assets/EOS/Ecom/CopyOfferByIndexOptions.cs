using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200044B RID: 1099
	public class CopyOfferByIndexOptions
	{
		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x0001C405 File Offset: 0x0001A605
		// (set) Token: 0x06001ACE RID: 6862 RVA: 0x0001C40D File Offset: 0x0001A60D
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x0001C416 File Offset: 0x0001A616
		// (set) Token: 0x06001AD0 RID: 6864 RVA: 0x0001C41E File Offset: 0x0001A61E
		public uint OfferIndex { get; set; }
	}
}
