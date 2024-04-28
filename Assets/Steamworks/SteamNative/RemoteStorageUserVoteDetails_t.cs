using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000B6 RID: 182
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageUserVoteDetails_t
	{
		// Token: 0x0600057E RID: 1406 RVA: 0x00017B08 File Offset: 0x00015D08
		internal static RemoteStorageUserVoteDetails_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageUserVoteDetails_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageUserVoteDetails_t.PackSmall));
			}
			return (RemoteStorageUserVoteDetails_t)Marshal.PtrToStructure(p, typeof(RemoteStorageUserVoteDetails_t));
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00017B41 File Offset: 0x00015D41
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageUserVoteDetails_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageUserVoteDetails_t));
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00017B6C File Offset: 0x00015D6C
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageUserVoteDetails_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageUserVoteDetails_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageUserVoteDetails_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageUserVoteDetails_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageUserVoteDetails_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageUserVoteDetails_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageUserVoteDetails_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageUserVoteDetails_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageUserVoteDetails_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageUserVoteDetails_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageUserVoteDetails_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageUserVoteDetails_t.OnGetSize)
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
				CallbackId = 1325
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1325);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00017E72 File Offset: 0x00016072
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageUserVoteDetails_t.OnResult(param);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00017E7A File Offset: 0x0001607A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageUserVoteDetails_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00017E84 File Offset: 0x00016084
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageUserVoteDetails_t.OnGetSize();
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00017E8B File Offset: 0x0001608B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageUserVoteDetails_t.StructSize();
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00017E92 File Offset: 0x00016092
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageUserVoteDetails_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00017EA4 File Offset: 0x000160A4
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageUserVoteDetails_t data = RemoteStorageUserVoteDetails_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageUserVoteDetails_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageUserVoteDetails_t>(data);
			}
		}

		// Token: 0x040005B5 RID: 1461
		internal const int CallbackId = 1325;

		// Token: 0x040005B6 RID: 1462
		internal Result Result;

		// Token: 0x040005B7 RID: 1463
		internal ulong PublishedFileId;

		// Token: 0x040005B8 RID: 1464
		internal WorkshopVote Vote;

		// Token: 0x020001DA RID: 474
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D16 RID: 7446 RVA: 0x00062208 File Offset: 0x00060408
			public static implicit operator RemoteStorageUserVoteDetails_t(RemoteStorageUserVoteDetails_t.PackSmall d)
			{
				return new RemoteStorageUserVoteDetails_t
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					Vote = d.Vote
				};
			}

			// Token: 0x04000A24 RID: 2596
			internal Result Result;

			// Token: 0x04000A25 RID: 2597
			internal ulong PublishedFileId;

			// Token: 0x04000A26 RID: 2598
			internal WorkshopVote Vote;
		}
	}
}
