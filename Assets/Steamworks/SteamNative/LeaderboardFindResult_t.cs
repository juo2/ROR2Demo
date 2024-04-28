using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000C2 RID: 194
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaderboardFindResult_t
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x0001A670 File Offset: 0x00018870
		internal static LeaderboardFindResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (LeaderboardFindResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(LeaderboardFindResult_t.PackSmall));
			}
			return (LeaderboardFindResult_t)Marshal.PtrToStructure(p, typeof(LeaderboardFindResult_t));
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001A6A9 File Offset: 0x000188A9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(LeaderboardFindResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(LeaderboardFindResult_t));
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001A6D1 File Offset: 0x000188D1
		internal static CallResult<LeaderboardFindResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<LeaderboardFindResult_t, bool> CallbackFunction)
		{
			return new CallResult<LeaderboardFindResult_t>(steamworks, call, CallbackFunction, new CallResult<LeaderboardFindResult_t>.ConvertFromPointer(LeaderboardFindResult_t.FromPointer), LeaderboardFindResult_t.StructSize(), 1104);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001A6F4 File Offset: 0x000188F4
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
						ResultA = new Callback.VTableWinThis.ResultD(LeaderboardFindResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(LeaderboardFindResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(LeaderboardFindResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(LeaderboardFindResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(LeaderboardFindResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(LeaderboardFindResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(LeaderboardFindResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(LeaderboardFindResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(LeaderboardFindResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(LeaderboardFindResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(LeaderboardFindResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(LeaderboardFindResult_t.OnGetSize)
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
				CallbackId = 1104
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1104);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001A9FA File Offset: 0x00018BFA
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			LeaderboardFindResult_t.OnResult(param);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001AA02 File Offset: 0x00018C02
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			LeaderboardFindResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001AA0C File Offset: 0x00018C0C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return LeaderboardFindResult_t.OnGetSize();
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001AA13 File Offset: 0x00018C13
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return LeaderboardFindResult_t.StructSize();
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001AA1A File Offset: 0x00018C1A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			LeaderboardFindResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001AA2C File Offset: 0x00018C2C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			LeaderboardFindResult_t data = LeaderboardFindResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<LeaderboardFindResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<LeaderboardFindResult_t>(data);
			}
		}

		// Token: 0x040005E9 RID: 1513
		internal const int CallbackId = 1104;

		// Token: 0x040005EA RID: 1514
		internal ulong SteamLeaderboard;

		// Token: 0x040005EB RID: 1515
		internal byte LeaderboardFound;

		// Token: 0x020001E6 RID: 486
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D22 RID: 7458 RVA: 0x00062538 File Offset: 0x00060738
			public static implicit operator LeaderboardFindResult_t(LeaderboardFindResult_t.PackSmall d)
			{
				return new LeaderboardFindResult_t
				{
					SteamLeaderboard = d.SteamLeaderboard,
					LeaderboardFound = d.LeaderboardFound
				};
			}

			// Token: 0x04000A4D RID: 2637
			internal ulong SteamLeaderboard;

			// Token: 0x04000A4E RID: 2638
			internal byte LeaderboardFound;
		}
	}
}
