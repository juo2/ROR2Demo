using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000B1 RID: 177
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageGetPublishedItemVoteDetailsResult_t
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x00016790 File Offset: 0x00014990
		internal static RemoteStorageGetPublishedItemVoteDetailsResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageGetPublishedItemVoteDetailsResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t.PackSmall));
			}
			return (RemoteStorageGetPublishedItemVoteDetailsResult_t)Marshal.PtrToStructure(p, typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t));
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x000167C9 File Offset: 0x000149C9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageGetPublishedItemVoteDetailsResult_t));
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000167F1 File Offset: 0x000149F1
		internal static CallResult<RemoteStorageGetPublishedItemVoteDetailsResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageGetPublishedItemVoteDetailsResult_t, bool> CallbackFunction)
		{
			return new CallResult<RemoteStorageGetPublishedItemVoteDetailsResult_t>(steamworks, call, CallbackFunction, new CallResult<RemoteStorageGetPublishedItemVoteDetailsResult_t>.ConvertFromPointer(RemoteStorageGetPublishedItemVoteDetailsResult_t.FromPointer), RemoteStorageGetPublishedItemVoteDetailsResult_t.StructSize(), 1320);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00016814 File Offset: 0x00014A14
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageGetPublishedItemVoteDetailsResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageGetPublishedItemVoteDetailsResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageGetPublishedItemVoteDetailsResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageGetPublishedItemVoteDetailsResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageGetPublishedItemVoteDetailsResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageGetPublishedItemVoteDetailsResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageGetPublishedItemVoteDetailsResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageGetPublishedItemVoteDetailsResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageGetPublishedItemVoteDetailsResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageGetPublishedItemVoteDetailsResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageGetPublishedItemVoteDetailsResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageGetPublishedItemVoteDetailsResult_t.OnGetSize)
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
				CallbackId = 1320
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1320);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00016B1A File Offset: 0x00014D1A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageGetPublishedItemVoteDetailsResult_t.OnResult(param);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00016B22 File Offset: 0x00014D22
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageGetPublishedItemVoteDetailsResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00016B2C File Offset: 0x00014D2C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageGetPublishedItemVoteDetailsResult_t.OnGetSize();
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00016B33 File Offset: 0x00014D33
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageGetPublishedItemVoteDetailsResult_t.StructSize();
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00016B3A File Offset: 0x00014D3A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageGetPublishedItemVoteDetailsResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00016B4C File Offset: 0x00014D4C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageGetPublishedItemVoteDetailsResult_t data = RemoteStorageGetPublishedItemVoteDetailsResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageGetPublishedItemVoteDetailsResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageGetPublishedItemVoteDetailsResult_t>(data);
			}
		}

		// Token: 0x040005A2 RID: 1442
		internal const int CallbackId = 1320;

		// Token: 0x040005A3 RID: 1443
		internal Result Result;

		// Token: 0x040005A4 RID: 1444
		internal ulong PublishedFileId;

		// Token: 0x040005A5 RID: 1445
		internal int VotesFor;

		// Token: 0x040005A6 RID: 1446
		internal int VotesAgainst;

		// Token: 0x040005A7 RID: 1447
		internal int Reports;

		// Token: 0x040005A8 RID: 1448
		internal float FScore;

		// Token: 0x020001D5 RID: 469
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D11 RID: 7441 RVA: 0x000620E4 File Offset: 0x000602E4
			public static implicit operator RemoteStorageGetPublishedItemVoteDetailsResult_t(RemoteStorageGetPublishedItemVoteDetailsResult_t.PackSmall d)
			{
				return new RemoteStorageGetPublishedItemVoteDetailsResult_t
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					VotesFor = d.VotesFor,
					VotesAgainst = d.VotesAgainst,
					Reports = d.Reports,
					FScore = d.FScore
				};
			}

			// Token: 0x04000A16 RID: 2582
			internal Result Result;

			// Token: 0x04000A17 RID: 2583
			internal ulong PublishedFileId;

			// Token: 0x04000A18 RID: 2584
			internal int VotesFor;

			// Token: 0x04000A19 RID: 2585
			internal int VotesAgainst;

			// Token: 0x04000A1A RID: 2586
			internal int Reports;

			// Token: 0x04000A1B RID: 2587
			internal float FScore;
		}
	}
}
