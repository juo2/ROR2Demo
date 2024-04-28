using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200044D RID: 1101
	public class CopyOfferImageInfoByIndexOptions
	{
		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x0001C47F File Offset: 0x0001A67F
		// (set) Token: 0x06001AD8 RID: 6872 RVA: 0x0001C487 File Offset: 0x0001A687
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x0001C490 File Offset: 0x0001A690
		// (set) Token: 0x06001ADA RID: 6874 RVA: 0x0001C498 File Offset: 0x0001A698
		public string OfferId { get; set; }

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x0001C4A1 File Offset: 0x0001A6A1
		// (set) Token: 0x06001ADC RID: 6876 RVA: 0x0001C4A9 File Offset: 0x0001A6A9
		public uint ImageInfoIndex { get; set; }
	}
}
