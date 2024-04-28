using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x020000F8 RID: 248
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_CloseBrowser_t
	{
		// Token: 0x060007B5 RID: 1973 RVA: 0x0002609E File Offset: 0x0002429E
		internal static HTML_CloseBrowser_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_CloseBrowser_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_CloseBrowser_t.PackSmall));
			}
			return (HTML_CloseBrowser_t)Marshal.PtrToStructure(p, typeof(HTML_CloseBrowser_t));
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000260D7 File Offset: 0x000242D7
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_CloseBrowser_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_CloseBrowser_t));
		}

		// Token: 0x040006CF RID: 1743
		internal uint UnBrowserHandle;

		// Token: 0x0200021C RID: 540
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D58 RID: 7512 RVA: 0x0006336C File Offset: 0x0006156C
			public static implicit operator HTML_CloseBrowser_t(HTML_CloseBrowser_t.PackSmall d)
			{
				return new HTML_CloseBrowser_t
				{
					UnBrowserHandle = d.UnBrowserHandle
				};
			}

			// Token: 0x04000B04 RID: 2820
			internal uint UnBrowserHandle;
		}
	}
}
