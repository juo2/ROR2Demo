using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x020000A1 RID: 161
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamParamStringArray_t
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x00012C48 File Offset: 0x00010E48
		internal static SteamParamStringArray_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamParamStringArray_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamParamStringArray_t.PackSmall));
			}
			return (SteamParamStringArray_t)Marshal.PtrToStructure(p, typeof(SteamParamStringArray_t));
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00012C81 File Offset: 0x00010E81
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamParamStringArray_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamParamStringArray_t));
		}

		// Token: 0x0400054A RID: 1354
		internal IntPtr Strings;

		// Token: 0x0400054B RID: 1355
		internal int NumStrings;

		// Token: 0x020001C5 RID: 453
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D01 RID: 7425 RVA: 0x00061BB8 File Offset: 0x0005FDB8
			public static implicit operator SteamParamStringArray_t(SteamParamStringArray_t.PackSmall d)
			{
				return new SteamParamStringArray_t
				{
					Strings = d.Strings,
					NumStrings = d.NumStrings
				};
			}

			// Token: 0x040009CD RID: 2509
			internal IntPtr Strings;

			// Token: 0x040009CE RID: 2510
			internal int NumStrings;
		}
	}
}
