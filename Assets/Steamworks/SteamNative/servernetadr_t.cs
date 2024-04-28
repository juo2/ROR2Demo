using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x02000093 RID: 147
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct servernetadr_t
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x0000FD09 File Offset: 0x0000DF09
		internal static servernetadr_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (servernetadr_t.PackSmall)Marshal.PtrToStructure(p, typeof(servernetadr_t.PackSmall));
			}
			return (servernetadr_t)Marshal.PtrToStructure(p, typeof(servernetadr_t));
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000FD42 File Offset: 0x0000DF42
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(servernetadr_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(servernetadr_t));
		}

		// Token: 0x04000503 RID: 1283
		internal ushort ConnectionPort;

		// Token: 0x04000504 RID: 1284
		internal ushort QueryPort;

		// Token: 0x04000505 RID: 1285
		internal uint IP;

		// Token: 0x020001B7 RID: 439
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CF3 RID: 7411 RVA: 0x0006176C File Offset: 0x0005F96C
			public static implicit operator servernetadr_t(servernetadr_t.PackSmall d)
			{
				return new servernetadr_t
				{
					ConnectionPort = d.ConnectionPort,
					QueryPort = d.QueryPort,
					IP = d.IP
				};
			}

			// Token: 0x04000992 RID: 2450
			internal ushort ConnectionPort;

			// Token: 0x04000993 RID: 2451
			internal ushort QueryPort;

			// Token: 0x04000994 RID: 2452
			internal uint IP;
		}
	}
}
