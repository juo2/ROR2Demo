using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000083 RID: 131
	public class QueryFileOptions
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00005AFE File Offset: 0x00003CFE
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x00005B06 File Offset: 0x00003D06
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00005B0F File Offset: 0x00003D0F
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x00005B17 File Offset: 0x00003D17
		public string Filename { get; set; }
	}
}
