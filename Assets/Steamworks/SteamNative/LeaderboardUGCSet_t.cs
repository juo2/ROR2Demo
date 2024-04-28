using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000C9 RID: 201
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaderboardUGCSet_t
	{
		// Token: 0x0600062D RID: 1581 RVA: 0x0001C1F8 File Offset: 0x0001A3F8
		internal static LeaderboardUGCSet_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (LeaderboardUGCSet_t.PackSmall)Marshal.PtrToStructure(p, typeof(LeaderboardUGCSet_t.PackSmall));
			}
			return (LeaderboardUGCSet_t)Marshal.PtrToStructure(p, typeof(LeaderboardUGCSet_t));
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001C231 File Offset: 0x0001A431
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(LeaderboardUGCSet_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(LeaderboardUGCSet_t));
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001C259 File Offset: 0x0001A459
		internal static CallResult<LeaderboardUGCSet_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<LeaderboardUGCSet_t, bool> CallbackFunction)
		{
			return new CallResult<LeaderboardUGCSet_t>(steamworks, call, CallbackFunction, new CallResult<LeaderboardUGCSet_t>.ConvertFromPointer(LeaderboardUGCSet_t.FromPointer), LeaderboardUGCSet_t.StructSize(), 1111);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001C27C File Offset: 0x0001A47C
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
						ResultA = new Callback.VTableWinThis.ResultD(LeaderboardUGCSet_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(LeaderboardUGCSet_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(LeaderboardUGCSet_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(LeaderboardUGCSet_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(LeaderboardUGCSet_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(LeaderboardUGCSet_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(LeaderboardUGCSet_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(LeaderboardUGCSet_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(LeaderboardUGCSet_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(LeaderboardUGCSet_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(LeaderboardUGCSet_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(LeaderboardUGCSet_t.OnGetSize)
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
				CallbackId = 1111
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1111);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001C582 File Offset: 0x0001A782
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			LeaderboardUGCSet_t.OnResult(param);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001C58A File Offset: 0x0001A78A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			LeaderboardUGCSet_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001C594 File Offset: 0x0001A794
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return LeaderboardUGCSet_t.OnGetSize();
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001C59B File Offset: 0x0001A79B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return LeaderboardUGCSet_t.StructSize();
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001C5A2 File Offset: 0x0001A7A2
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			LeaderboardUGCSet_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001C5B4 File Offset: 0x0001A7B4
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			LeaderboardUGCSet_t data = LeaderboardUGCSet_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<LeaderboardUGCSet_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<LeaderboardUGCSet_t>(data);
			}
		}

		// Token: 0x04000604 RID: 1540
		internal const int CallbackId = 1111;

		// Token: 0x04000605 RID: 1541
		internal Result Result;

		// Token: 0x04000606 RID: 1542
		internal ulong SteamLeaderboard;

		// Token: 0x020001ED RID: 493
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D29 RID: 7465 RVA: 0x000626DC File Offset: 0x000608DC
			public static implicit operator LeaderboardUGCSet_t(LeaderboardUGCSet_t.PackSmall d)
			{
				return new LeaderboardUGCSet_t
				{
					Result = d.Result,
					SteamLeaderboard = d.SteamLeaderboard
				};
			}

			// Token: 0x04000A61 RID: 2657
			internal Result Result;

			// Token: 0x04000A62 RID: 2658
			internal ulong SteamLeaderboard;
		}
	}
}
