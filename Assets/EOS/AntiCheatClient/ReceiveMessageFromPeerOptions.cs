using System;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005CE RID: 1486
	public class ReceiveMessageFromPeerOptions
	{
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x00025D6E File Offset: 0x00023F6E
		// (set) Token: 0x060023D9 RID: 9177 RVA: 0x00025D76 File Offset: 0x00023F76
		public IntPtr PeerHandle { get; set; }

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x060023DA RID: 9178 RVA: 0x00025D7F File Offset: 0x00023F7F
		// (set) Token: 0x060023DB RID: 9179 RVA: 0x00025D87 File Offset: 0x00023F87
		public byte[] Data { get; set; }
	}
}
