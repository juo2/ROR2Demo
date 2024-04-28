using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x020000D0 RID: 208
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct P2PSessionState_t
	{
		// Token: 0x0600066F RID: 1647 RVA: 0x0001DD40 File Offset: 0x0001BF40
		internal static P2PSessionState_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (P2PSessionState_t.PackSmall)Marshal.PtrToStructure(p, typeof(P2PSessionState_t.PackSmall));
			}
			return (P2PSessionState_t)Marshal.PtrToStructure(p, typeof(P2PSessionState_t));
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001DD79 File Offset: 0x0001BF79
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(P2PSessionState_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(P2PSessionState_t));
		}

		// Token: 0x0400061D RID: 1565
		internal byte ConnectionActive;

		// Token: 0x0400061E RID: 1566
		internal byte Connecting;

		// Token: 0x0400061F RID: 1567
		internal byte P2PSessionError;

		// Token: 0x04000620 RID: 1568
		internal byte UsingRelay;

		// Token: 0x04000621 RID: 1569
		internal int BytesQueuedForSend;

		// Token: 0x04000622 RID: 1570
		internal int PacketsQueuedForSend;

		// Token: 0x04000623 RID: 1571
		internal uint RemoteIP;

		// Token: 0x04000624 RID: 1572
		internal ushort RemotePort;

		// Token: 0x020001F4 RID: 500
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D30 RID: 7472 RVA: 0x00062868 File Offset: 0x00060A68
			public static implicit operator P2PSessionState_t(P2PSessionState_t.PackSmall d)
			{
				return new P2PSessionState_t
				{
					ConnectionActive = d.ConnectionActive,
					Connecting = d.Connecting,
					P2PSessionError = d.P2PSessionError,
					UsingRelay = d.UsingRelay,
					BytesQueuedForSend = d.BytesQueuedForSend,
					PacketsQueuedForSend = d.PacketsQueuedForSend,
					RemoteIP = d.RemoteIP,
					RemotePort = d.RemotePort
				};
			}

			// Token: 0x04000A73 RID: 2675
			internal byte ConnectionActive;

			// Token: 0x04000A74 RID: 2676
			internal byte Connecting;

			// Token: 0x04000A75 RID: 2677
			internal byte P2PSessionError;

			// Token: 0x04000A76 RID: 2678
			internal byte UsingRelay;

			// Token: 0x04000A77 RID: 2679
			internal int BytesQueuedForSend;

			// Token: 0x04000A78 RID: 2680
			internal int PacketsQueuedForSend;

			// Token: 0x04000A79 RID: 2681
			internal uint RemoteIP;

			// Token: 0x04000A7A RID: 2682
			internal ushort RemotePort;
		}
	}
}
