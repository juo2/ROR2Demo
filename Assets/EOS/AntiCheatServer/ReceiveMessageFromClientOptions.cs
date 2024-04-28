using System;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x02000569 RID: 1385
	public class ReceiveMessageFromClientOptions
	{
		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x00023732 File Offset: 0x00021932
		// (set) Token: 0x06002192 RID: 8594 RVA: 0x0002373A File Offset: 0x0002193A
		public IntPtr ClientHandle { get; set; }

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06002193 RID: 8595 RVA: 0x00023743 File Offset: 0x00021943
		// (set) Token: 0x06002194 RID: 8596 RVA: 0x0002374B File Offset: 0x0002194B
		public byte[] Data { get; set; }
	}
}
