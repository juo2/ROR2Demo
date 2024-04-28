using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x0200028C RID: 652
	public class GetNextReceivedPacketSizeOptions
	{
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x00011AF5 File Offset: 0x0000FCF5
		// (set) Token: 0x06001095 RID: 4245 RVA: 0x00011AFD File Offset: 0x0000FCFD
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x00011B06 File Offset: 0x0000FD06
		// (set) Token: 0x06001097 RID: 4247 RVA: 0x00011B0E File Offset: 0x0000FD0E
		public byte? RequestedChannel { get; set; }
	}
}
