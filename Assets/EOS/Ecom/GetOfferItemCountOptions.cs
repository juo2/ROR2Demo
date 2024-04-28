using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000465 RID: 1125
	public class GetOfferItemCountOptions
	{
		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x0001D3BF File Offset: 0x0001B5BF
		// (set) Token: 0x06001B7C RID: 7036 RVA: 0x0001D3C7 File Offset: 0x0001B5C7
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x0001D3D0 File Offset: 0x0001B5D0
		// (set) Token: 0x06001B7E RID: 7038 RVA: 0x0001D3D8 File Offset: 0x0001B5D8
		public string OfferId { get; set; }
	}
}
