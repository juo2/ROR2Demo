using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000C4 RID: 196
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaderboardScoreUploaded_t
	{
		// Token: 0x060005FD RID: 1533 RVA: 0x0001AE60 File Offset: 0x00019060
		internal static LeaderboardScoreUploaded_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (LeaderboardScoreUploaded_t.PackSmall)Marshal.PtrToStructure(p, typeof(LeaderboardScoreUploaded_t.PackSmall));
			}
			return (LeaderboardScoreUploaded_t)Marshal.PtrToStructure(p, typeof(LeaderboardScoreUploaded_t));
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001AE99 File Offset: 0x00019099
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(LeaderboardScoreUploaded_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(LeaderboardScoreUploaded_t));
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001AEC1 File Offset: 0x000190C1
		internal static CallResult<LeaderboardScoreUploaded_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<LeaderboardScoreUploaded_t, bool> CallbackFunction)
		{
			return new CallResult<LeaderboardScoreUploaded_t>(steamworks, call, CallbackFunction, new CallResult<LeaderboardScoreUploaded_t>.ConvertFromPointer(LeaderboardScoreUploaded_t.FromPointer), LeaderboardScoreUploaded_t.StructSize(), 1106);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001AEE4 File Offset: 0x000190E4
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
						ResultA = new Callback.VTableWinThis.ResultD(LeaderboardScoreUploaded_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(LeaderboardScoreUploaded_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(LeaderboardScoreUploaded_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(LeaderboardScoreUploaded_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(LeaderboardScoreUploaded_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(LeaderboardScoreUploaded_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(LeaderboardScoreUploaded_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(LeaderboardScoreUploaded_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(LeaderboardScoreUploaded_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(LeaderboardScoreUploaded_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(LeaderboardScoreUploaded_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(LeaderboardScoreUploaded_t.OnGetSize)
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
				CallbackId = 1106
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1106);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001B1EA File Offset: 0x000193EA
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			LeaderboardScoreUploaded_t.OnResult(param);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001B1F2 File Offset: 0x000193F2
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			LeaderboardScoreUploaded_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001B1FC File Offset: 0x000193FC
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return LeaderboardScoreUploaded_t.OnGetSize();
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001B203 File Offset: 0x00019403
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return LeaderboardScoreUploaded_t.StructSize();
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001B20A File Offset: 0x0001940A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			LeaderboardScoreUploaded_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001B21C File Offset: 0x0001941C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			LeaderboardScoreUploaded_t data = LeaderboardScoreUploaded_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<LeaderboardScoreUploaded_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<LeaderboardScoreUploaded_t>(data);
			}
		}

		// Token: 0x040005F0 RID: 1520
		internal const int CallbackId = 1106;

		// Token: 0x040005F1 RID: 1521
		internal byte Success;

		// Token: 0x040005F2 RID: 1522
		internal ulong SteamLeaderboard;

		// Token: 0x040005F3 RID: 1523
		internal int Score;

		// Token: 0x040005F4 RID: 1524
		internal byte ScoreChanged;

		// Token: 0x040005F5 RID: 1525
		internal int GlobalRankNew;

		// Token: 0x040005F6 RID: 1526
		internal int GlobalRankPrevious;

		// Token: 0x020001E8 RID: 488
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D24 RID: 7460 RVA: 0x000625A8 File Offset: 0x000607A8
			public static implicit operator LeaderboardScoreUploaded_t(LeaderboardScoreUploaded_t.PackSmall d)
			{
				return new LeaderboardScoreUploaded_t
				{
					Success = d.Success,
					SteamLeaderboard = d.SteamLeaderboard,
					Score = d.Score,
					ScoreChanged = d.ScoreChanged,
					GlobalRankNew = d.GlobalRankNew,
					GlobalRankPrevious = d.GlobalRankPrevious
				};
			}

			// Token: 0x04000A52 RID: 2642
			internal byte Success;

			// Token: 0x04000A53 RID: 2643
			internal ulong SteamLeaderboard;

			// Token: 0x04000A54 RID: 2644
			internal int Score;

			// Token: 0x04000A55 RID: 2645
			internal byte ScoreChanged;

			// Token: 0x04000A56 RID: 2646
			internal int GlobalRankNew;

			// Token: 0x04000A57 RID: 2647
			internal int GlobalRankPrevious;
		}
	}
}
