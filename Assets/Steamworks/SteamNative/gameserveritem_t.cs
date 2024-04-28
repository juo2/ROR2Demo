using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x02000094 RID: 148
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct gameserveritem_t
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x0000FD6A File Offset: 0x0000DF6A
		internal static gameserveritem_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (gameserveritem_t.PackSmall)Marshal.PtrToStructure(p, typeof(gameserveritem_t.PackSmall));
			}
			return (gameserveritem_t)Marshal.PtrToStructure(p, typeof(gameserveritem_t));
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000FDA3 File Offset: 0x0000DFA3
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(gameserveritem_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(gameserveritem_t));
		}

		// Token: 0x04000506 RID: 1286
		internal servernetadr_t NetAdr;

		// Token: 0x04000507 RID: 1287
		internal int Ping;

		// Token: 0x04000508 RID: 1288
		[MarshalAs(UnmanagedType.I1)]
		internal bool HadSuccessfulResponse;

		// Token: 0x04000509 RID: 1289
		[MarshalAs(UnmanagedType.I1)]
		internal bool DoNotRefresh;

		// Token: 0x0400050A RID: 1290
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		internal string GameDir;

		// Token: 0x0400050B RID: 1291
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		internal string Map;

		// Token: 0x0400050C RID: 1292
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		internal string GameDescription;

		// Token: 0x0400050D RID: 1293
		internal uint AppID;

		// Token: 0x0400050E RID: 1294
		internal int Players;

		// Token: 0x0400050F RID: 1295
		internal int MaxPlayers;

		// Token: 0x04000510 RID: 1296
		internal int BotPlayers;

		// Token: 0x04000511 RID: 1297
		[MarshalAs(UnmanagedType.I1)]
		internal bool Password;

		// Token: 0x04000512 RID: 1298
		[MarshalAs(UnmanagedType.I1)]
		internal bool Secure;

		// Token: 0x04000513 RID: 1299
		internal uint TimeLastPlayed;

		// Token: 0x04000514 RID: 1300
		internal int ServerVersion;

		// Token: 0x04000515 RID: 1301
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		internal string ServerName;

		// Token: 0x04000516 RID: 1302
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string GameTags;

		// Token: 0x04000517 RID: 1303
		internal ulong SteamID;

		// Token: 0x020001B8 RID: 440
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CF4 RID: 7412 RVA: 0x000617AC File Offset: 0x0005F9AC
			public static implicit operator gameserveritem_t(gameserveritem_t.PackSmall d)
			{
				return new gameserveritem_t
				{
					NetAdr = d.NetAdr,
					Ping = d.Ping,
					HadSuccessfulResponse = d.HadSuccessfulResponse,
					DoNotRefresh = d.DoNotRefresh,
					GameDir = d.GameDir,
					Map = d.Map,
					GameDescription = d.GameDescription,
					AppID = d.AppID,
					Players = d.Players,
					MaxPlayers = d.MaxPlayers,
					BotPlayers = d.BotPlayers,
					Password = d.Password,
					Secure = d.Secure,
					TimeLastPlayed = d.TimeLastPlayed,
					ServerVersion = d.ServerVersion,
					ServerName = d.ServerName,
					GameTags = d.GameTags,
					SteamID = d.SteamID
				};
			}

			// Token: 0x04000995 RID: 2453
			internal servernetadr_t NetAdr;

			// Token: 0x04000996 RID: 2454
			internal int Ping;

			// Token: 0x04000997 RID: 2455
			[MarshalAs(UnmanagedType.I1)]
			internal bool HadSuccessfulResponse;

			// Token: 0x04000998 RID: 2456
			[MarshalAs(UnmanagedType.I1)]
			internal bool DoNotRefresh;

			// Token: 0x04000999 RID: 2457
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			internal string GameDir;

			// Token: 0x0400099A RID: 2458
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			internal string Map;

			// Token: 0x0400099B RID: 2459
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			internal string GameDescription;

			// Token: 0x0400099C RID: 2460
			internal uint AppID;

			// Token: 0x0400099D RID: 2461
			internal int Players;

			// Token: 0x0400099E RID: 2462
			internal int MaxPlayers;

			// Token: 0x0400099F RID: 2463
			internal int BotPlayers;

			// Token: 0x040009A0 RID: 2464
			[MarshalAs(UnmanagedType.I1)]
			internal bool Password;

			// Token: 0x040009A1 RID: 2465
			[MarshalAs(UnmanagedType.I1)]
			internal bool Secure;

			// Token: 0x040009A2 RID: 2466
			internal uint TimeLastPlayed;

			// Token: 0x040009A3 RID: 2467
			internal int ServerVersion;

			// Token: 0x040009A4 RID: 2468
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			internal string ServerName;

			// Token: 0x040009A5 RID: 2469
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string GameTags;

			// Token: 0x040009A6 RID: 2470
			internal ulong SteamID;
		}
	}
}
