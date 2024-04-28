using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x0200007A RID: 122
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct FriendGameInfo_t
	{
		// Token: 0x06000376 RID: 886 RVA: 0x0000A678 File Offset: 0x00008878
		internal static FriendGameInfo_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (FriendGameInfo_t.PackSmall)Marshal.PtrToStructure(p, typeof(FriendGameInfo_t.PackSmall));
			}
			return (FriendGameInfo_t)Marshal.PtrToStructure(p, typeof(FriendGameInfo_t));
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000A6B1 File Offset: 0x000088B1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(FriendGameInfo_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(FriendGameInfo_t));
		}

		// Token: 0x040004B0 RID: 1200
		internal ulong GameID;

		// Token: 0x040004B1 RID: 1201
		internal uint GameIP;

		// Token: 0x040004B2 RID: 1202
		internal ushort GamePort;

		// Token: 0x040004B3 RID: 1203
		internal ushort QueryPort;

		// Token: 0x040004B4 RID: 1204
		internal ulong SteamIDLobby;

		// Token: 0x0200019E RID: 414
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CDA RID: 7386 RVA: 0x00061210 File Offset: 0x0005F410
			public static implicit operator FriendGameInfo_t(FriendGameInfo_t.PackSmall d)
			{
				return new FriendGameInfo_t
				{
					GameID = d.GameID,
					GameIP = d.GameIP,
					GamePort = d.GamePort,
					QueryPort = d.QueryPort,
					SteamIDLobby = d.SteamIDLobby
				};
			}

			// Token: 0x04000955 RID: 2389
			internal ulong GameID;

			// Token: 0x04000956 RID: 2390
			internal uint GameIP;

			// Token: 0x04000957 RID: 2391
			internal ushort GamePort;

			// Token: 0x04000958 RID: 2392
			internal ushort QueryPort;

			// Token: 0x04000959 RID: 2393
			internal ulong SteamIDLobby;
		}
	}
}
