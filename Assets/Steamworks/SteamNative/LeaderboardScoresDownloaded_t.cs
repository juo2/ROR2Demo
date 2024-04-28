using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000C3 RID: 195
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaderboardScoresDownloaded_t
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x0001AA68 File Offset: 0x00018C68
		internal static LeaderboardScoresDownloaded_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (LeaderboardScoresDownloaded_t.PackSmall)Marshal.PtrToStructure(p, typeof(LeaderboardScoresDownloaded_t.PackSmall));
			}
			return (LeaderboardScoresDownloaded_t)Marshal.PtrToStructure(p, typeof(LeaderboardScoresDownloaded_t));
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001AAA1 File Offset: 0x00018CA1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(LeaderboardScoresDownloaded_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(LeaderboardScoresDownloaded_t));
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001AAC9 File Offset: 0x00018CC9
		internal static CallResult<LeaderboardScoresDownloaded_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<LeaderboardScoresDownloaded_t, bool> CallbackFunction)
		{
			return new CallResult<LeaderboardScoresDownloaded_t>(steamworks, call, CallbackFunction, new CallResult<LeaderboardScoresDownloaded_t>.ConvertFromPointer(LeaderboardScoresDownloaded_t.FromPointer), LeaderboardScoresDownloaded_t.StructSize(), 1105);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001AAEC File Offset: 0x00018CEC
		internal static void Register(BaseSteamworks steamworks)
		{
			CallbackHandle callbackHandle = new CallbackHandle(steamworks);
			if (Config.UseThisCall)
			{
				if (Platform.IsWindows)
				{
					callbackHandle.vTablePtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Callback.VTableWinThis)));
					Callback.VTableWinThis vtableWinThis = new Callback.VTableWinThis
					{
						ResultA = new Callback.VTableWinThis.ResultD(LeaderboardScoresDownloaded_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(LeaderboardScoresDownloaded_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(LeaderboardScoresDownloaded_t.OnGetSizeThis)
					};
					callbackHandle.FuncA = GCHandle.Alloc(vtableWinThis.ResultA);
					callbackHandle.FuncB = GCHandle.Alloc(vtableWinThis.ResultB);
					callbackHandle.FuncC = GCHandle.Alloc(vtableWinThis.GetSize);
					Marshal.StructureToPtr<Callback.VTableWinThis>(vtableWinThis, callbackHandle.vTablePtr, false);
				}
				else
				{
					callbackHandle.vTablePtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Callback.VTableThis)));
					Callback.VTableThis vtableThis = new Callback.VTableThis
					{
						ResultA = new Callback.VTableThis.ResultD(LeaderboardScoresDownloaded_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(LeaderboardScoresDownloaded_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(LeaderboardScoresDownloaded_t.OnGetSizeThis)
					};
					callbackHandle.FuncA = GCHandle.Alloc(vtableThis.ResultA);
					callbackHandle.FuncB = GCHandle.Alloc(vtableThis.ResultB);
					callbackHandle.FuncC = GCHandle.Alloc(vtableThis.GetSize);
					Marshal.StructureToPtr<Callback.VTableThis>(vtableThis, callbackHandle.vTablePtr, false);
				}
			}
			else if (Platform.IsWindows)
			{
				callbackHandle.vTablePtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Callback.VTableWin)));
				Callback.VTableWin vtableWin = new Callback.VTableWin
				{
					ResultA = new Callback.VTableWin.ResultD(LeaderboardScoresDownloaded_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(LeaderboardScoresDownloaded_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(LeaderboardScoresDownloaded_t.OnGetSize)
				};
				callbackHandle.FuncA = GCHandle.Alloc(vtableWin.ResultA);
				callbackHandle.FuncB = GCHandle.Alloc(vtableWin.ResultB);
				callbackHandle.FuncC = GCHandle.Alloc(vtableWin.GetSize);
				Marshal.StructureToPtr<Callback.VTableWin>(vtableWin, callbackHandle.vTablePtr, false);
			}
			else
			{
				callbackHandle.vTablePtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Callback.VTable)));
				Callback.VTable vtable = new Callback.VTable
				{
					ResultA = new Callback.VTable.ResultD(LeaderboardScoresDownloaded_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(LeaderboardScoresDownloaded_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(LeaderboardScoresDownloaded_t.OnGetSize)
				};
				callbackHandle.FuncA = GCHandle.Alloc(vtable.ResultA);
				callbackHandle.FuncB = GCHandle.Alloc(vtable.ResultB);
				callbackHandle.FuncC = GCHandle.Alloc(vtable.GetSize);
				Marshal.StructureToPtr<Callback.VTable>(vtable, callbackHandle.vTablePtr, false);
			}
			callbackHandle.PinnedCallback = GCHandle.Alloc(new Callback
			{
				vTablePtr = callbackHandle.vTablePtr,
				CallbackFlags = (steamworks.IsGameServer ? 2 : 0),
				CallbackId = 1105
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1105);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001ADF2 File Offset: 0x00018FF2
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			LeaderboardScoresDownloaded_t.OnResult(param);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001ADFA File Offset: 0x00018FFA
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			LeaderboardScoresDownloaded_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001AE04 File Offset: 0x00019004
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return LeaderboardScoresDownloaded_t.OnGetSize();
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001AE0B File Offset: 0x0001900B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return LeaderboardScoresDownloaded_t.StructSize();
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001AE12 File Offset: 0x00019012
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			LeaderboardScoresDownloaded_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001AE24 File Offset: 0x00019024
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			LeaderboardScoresDownloaded_t data = LeaderboardScoresDownloaded_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<LeaderboardScoresDownloaded_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<LeaderboardScoresDownloaded_t>(data);
			}
		}

		// Token: 0x040005EC RID: 1516
		internal const int CallbackId = 1105;

		// Token: 0x040005ED RID: 1517
		internal ulong SteamLeaderboard;

		// Token: 0x040005EE RID: 1518
		internal ulong SteamLeaderboardEntries;

		// Token: 0x040005EF RID: 1519
		internal int CEntryCount;

		// Token: 0x020001E7 RID: 487
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D23 RID: 7459 RVA: 0x00062568 File Offset: 0x00060768
			public static implicit operator LeaderboardScoresDownloaded_t(LeaderboardScoresDownloaded_t.PackSmall d)
			{
				return new LeaderboardScoresDownloaded_t
				{
					SteamLeaderboard = d.SteamLeaderboard,
					SteamLeaderboardEntries = d.SteamLeaderboardEntries,
					CEntryCount = d.CEntryCount
				};
			}

			// Token: 0x04000A4F RID: 2639
			internal ulong SteamLeaderboard;

			// Token: 0x04000A50 RID: 2640
			internal ulong SteamLeaderboardEntries;

			// Token: 0x04000A51 RID: 2641
			internal int CEntryCount;
		}
	}
}
