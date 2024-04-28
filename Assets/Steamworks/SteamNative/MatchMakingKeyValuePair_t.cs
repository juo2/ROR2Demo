using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x02000092 RID: 146
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct MatchMakingKeyValuePair_t
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x0000FCA8 File Offset: 0x0000DEA8
		internal static MatchMakingKeyValuePair_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (MatchMakingKeyValuePair_t.PackSmall)Marshal.PtrToStructure(p, typeof(MatchMakingKeyValuePair_t.PackSmall));
			}
			return (MatchMakingKeyValuePair_t)Marshal.PtrToStructure(p, typeof(MatchMakingKeyValuePair_t));
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000FCE1 File Offset: 0x0000DEE1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(MatchMakingKeyValuePair_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(MatchMakingKeyValuePair_t));
		}

		// Token: 0x04000501 RID: 1281
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string Key;

		// Token: 0x04000502 RID: 1282
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string Value;

		// Token: 0x020001B6 RID: 438
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CF2 RID: 7410 RVA: 0x0006173C File Offset: 0x0005F93C
			public static implicit operator MatchMakingKeyValuePair_t(MatchMakingKeyValuePair_t.PackSmall d)
			{
				return new MatchMakingKeyValuePair_t
				{
					Key = d.Key,
					Value = d.Value
				};
			}

			// Token: 0x04000990 RID: 2448
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string Key;

			// Token: 0x04000991 RID: 2449
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string Value;
		}
	}
}
