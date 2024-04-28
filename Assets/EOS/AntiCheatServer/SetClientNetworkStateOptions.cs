using System;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x0200056D RID: 1389
	public class SetClientNetworkStateOptions
	{
		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x000238D3 File Offset: 0x00021AD3
		// (set) Token: 0x060021AF RID: 8623 RVA: 0x000238DB File Offset: 0x00021ADB
		public IntPtr ClientHandle { get; set; }

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x000238E4 File Offset: 0x00021AE4
		// (set) Token: 0x060021B1 RID: 8625 RVA: 0x000238EC File Offset: 0x00021AEC
		public bool IsNetworkActive { get; set; }
	}
}
