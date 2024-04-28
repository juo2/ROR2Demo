using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004FF RID: 1279
	public class TransferDeviceIdAccountOptions
	{
		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x000206D8 File Offset: 0x0001E8D8
		// (set) Token: 0x06001EDD RID: 7901 RVA: 0x000206E0 File Offset: 0x0001E8E0
		public ProductUserId PrimaryLocalUserId { get; set; }

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x000206E9 File Offset: 0x0001E8E9
		// (set) Token: 0x06001EDF RID: 7903 RVA: 0x000206F1 File Offset: 0x0001E8F1
		public ProductUserId LocalDeviceUserId { get; set; }

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x000206FA File Offset: 0x0001E8FA
		// (set) Token: 0x06001EE1 RID: 7905 RVA: 0x00020702 File Offset: 0x0001E902
		public ProductUserId ProductUserIdToPreserve { get; set; }
	}
}
