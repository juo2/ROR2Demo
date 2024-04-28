using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002B4 RID: 692
	public class SetPacketQueueSizeOptions
	{
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x00012CB7 File Offset: 0x00010EB7
		// (set) Token: 0x06001197 RID: 4503 RVA: 0x00012CBF File Offset: 0x00010EBF
		public ulong IncomingPacketQueueMaxSizeBytes { get; set; }

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x00012CC8 File Offset: 0x00010EC8
		// (set) Token: 0x06001199 RID: 4505 RVA: 0x00012CD0 File Offset: 0x00010ED0
		public ulong OutgoingPacketQueueMaxSizeBytes { get; set; }
	}
}
