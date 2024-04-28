using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x020000F6 RID: 246
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_NeedsPaint_t
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x00025FDC File Offset: 0x000241DC
		internal static HTML_NeedsPaint_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_NeedsPaint_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_NeedsPaint_t.PackSmall));
			}
			return (HTML_NeedsPaint_t)Marshal.PtrToStructure(p, typeof(HTML_NeedsPaint_t));
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00026015 File Offset: 0x00024215
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_NeedsPaint_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_NeedsPaint_t));
		}

		// Token: 0x040006BE RID: 1726
		internal uint UnBrowserHandle;

		// Token: 0x040006BF RID: 1727
		internal string PBGRA;

		// Token: 0x040006C0 RID: 1728
		internal uint UnWide;

		// Token: 0x040006C1 RID: 1729
		internal uint UnTall;

		// Token: 0x040006C2 RID: 1730
		internal uint UnUpdateX;

		// Token: 0x040006C3 RID: 1731
		internal uint UnUpdateY;

		// Token: 0x040006C4 RID: 1732
		internal uint UnUpdateWide;

		// Token: 0x040006C5 RID: 1733
		internal uint UnUpdateTall;

		// Token: 0x040006C6 RID: 1734
		internal uint UnScrollX;

		// Token: 0x040006C7 RID: 1735
		internal uint UnScrollY;

		// Token: 0x040006C8 RID: 1736
		internal float FlPageScale;

		// Token: 0x040006C9 RID: 1737
		internal uint UnPageSerial;

		// Token: 0x0200021A RID: 538
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D56 RID: 7510 RVA: 0x00063260 File Offset: 0x00061460
			public static implicit operator HTML_NeedsPaint_t(HTML_NeedsPaint_t.PackSmall d)
			{
				return new HTML_NeedsPaint_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PBGRA = d.PBGRA,
					UnWide = d.UnWide,
					UnTall = d.UnTall,
					UnUpdateX = d.UnUpdateX,
					UnUpdateY = d.UnUpdateY,
					UnUpdateWide = d.UnUpdateWide,
					UnUpdateTall = d.UnUpdateTall,
					UnScrollX = d.UnScrollX,
					UnScrollY = d.UnScrollY,
					FlPageScale = d.FlPageScale,
					UnPageSerial = d.UnPageSerial
				};
			}

			// Token: 0x04000AF3 RID: 2803
			internal uint UnBrowserHandle;

			// Token: 0x04000AF4 RID: 2804
			internal string PBGRA;

			// Token: 0x04000AF5 RID: 2805
			internal uint UnWide;

			// Token: 0x04000AF6 RID: 2806
			internal uint UnTall;

			// Token: 0x04000AF7 RID: 2807
			internal uint UnUpdateX;

			// Token: 0x04000AF8 RID: 2808
			internal uint UnUpdateY;

			// Token: 0x04000AF9 RID: 2809
			internal uint UnUpdateWide;

			// Token: 0x04000AFA RID: 2810
			internal uint UnUpdateTall;

			// Token: 0x04000AFB RID: 2811
			internal uint UnScrollX;

			// Token: 0x04000AFC RID: 2812
			internal uint UnScrollY;

			// Token: 0x04000AFD RID: 2813
			internal float FlPageScale;

			// Token: 0x04000AFE RID: 2814
			internal uint UnPageSerial;
		}
	}
}
