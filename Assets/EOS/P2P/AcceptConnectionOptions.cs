using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000278 RID: 632
	public class AcceptConnectionOptions
	{
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x00011645 File Offset: 0x0000F845
		// (set) Token: 0x0600103E RID: 4158 RVA: 0x0001164D File Offset: 0x0000F84D
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x00011656 File Offset: 0x0000F856
		// (set) Token: 0x06001040 RID: 4160 RVA: 0x0001165E File Offset: 0x0000F85E
		public ProductUserId RemoteUserId { get; set; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x00011667 File Offset: 0x0000F867
		// (set) Token: 0x06001042 RID: 4162 RVA: 0x0001166F File Offset: 0x0000F86F
		public SocketId SocketId { get; set; }
	}
}
