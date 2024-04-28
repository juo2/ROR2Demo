using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000405 RID: 1029
	public class RequestPermissionsOptions
	{
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x0001A230 File Offset: 0x00018430
		// (set) Token: 0x060018D2 RID: 6354 RVA: 0x0001A238 File Offset: 0x00018438
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x0001A241 File Offset: 0x00018441
		// (set) Token: 0x060018D4 RID: 6356 RVA: 0x0001A249 File Offset: 0x00018449
		public string[] PermissionKeys { get; set; }
	}
}
