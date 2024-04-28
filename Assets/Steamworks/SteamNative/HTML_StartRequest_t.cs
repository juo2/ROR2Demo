using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x020000F7 RID: 247
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_StartRequest_t
	{
		// Token: 0x060007B3 RID: 1971 RVA: 0x0002603D File Offset: 0x0002423D
		internal static HTML_StartRequest_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_StartRequest_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_StartRequest_t.PackSmall));
			}
			return (HTML_StartRequest_t)Marshal.PtrToStructure(p, typeof(HTML_StartRequest_t));
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00026076 File Offset: 0x00024276
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_StartRequest_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_StartRequest_t));
		}

		// Token: 0x040006CA RID: 1738
		internal uint UnBrowserHandle;

		// Token: 0x040006CB RID: 1739
		internal string PchURL;

		// Token: 0x040006CC RID: 1740
		internal string PchTarget;

		// Token: 0x040006CD RID: 1741
		internal string PchPostData;

		// Token: 0x040006CE RID: 1742
		[MarshalAs(UnmanagedType.I1)]
		internal bool BIsRedirect;

		// Token: 0x0200021B RID: 539
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D57 RID: 7511 RVA: 0x00063314 File Offset: 0x00061514
			public static implicit operator HTML_StartRequest_t(HTML_StartRequest_t.PackSmall d)
			{
				return new HTML_StartRequest_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchURL = d.PchURL,
					PchTarget = d.PchTarget,
					PchPostData = d.PchPostData,
					BIsRedirect = d.BIsRedirect
				};
			}

			// Token: 0x04000AFF RID: 2815
			internal uint UnBrowserHandle;

			// Token: 0x04000B00 RID: 2816
			internal string PchURL;

			// Token: 0x04000B01 RID: 2817
			internal string PchTarget;

			// Token: 0x04000B02 RID: 2818
			internal string PchPostData;

			// Token: 0x04000B03 RID: 2819
			[MarshalAs(UnmanagedType.I1)]
			internal bool BIsRedirect;
		}
	}
}
