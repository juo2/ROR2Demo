using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000B5 RID: 181
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageUpdateUserPublishedItemVoteResult_t
	{
		// Token: 0x06000574 RID: 1396 RVA: 0x00017710 File Offset: 0x00015910
		internal static RemoteStorageUpdateUserPublishedItemVoteResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageUpdateUserPublishedItemVoteResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t.PackSmall));
			}
			return (RemoteStorageUpdateUserPublishedItemVoteResult_t)Marshal.PtrToStructure(p, typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t));
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00017749 File Offset: 0x00015949
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageUpdateUserPublishedItemVoteResult_t));
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00017771 File Offset: 0x00015971
		internal static CallResult<RemoteStorageUpdateUserPublishedItemVoteResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageUpdateUserPublishedItemVoteResult_t, bool> CallbackFunction)
		{
			return new CallResult<RemoteStorageUpdateUserPublishedItemVoteResult_t>(steamworks, call, CallbackFunction, new CallResult<RemoteStorageUpdateUserPublishedItemVoteResult_t>.ConvertFromPointer(RemoteStorageUpdateUserPublishedItemVoteResult_t.FromPointer), RemoteStorageUpdateUserPublishedItemVoteResult_t.StructSize(), 1324);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00017794 File Offset: 0x00015994
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageUpdateUserPublishedItemVoteResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageUpdateUserPublishedItemVoteResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageUpdateUserPublishedItemVoteResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageUpdateUserPublishedItemVoteResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageUpdateUserPublishedItemVoteResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageUpdateUserPublishedItemVoteResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageUpdateUserPublishedItemVoteResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageUpdateUserPublishedItemVoteResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageUpdateUserPublishedItemVoteResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageUpdateUserPublishedItemVoteResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageUpdateUserPublishedItemVoteResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageUpdateUserPublishedItemVoteResult_t.OnGetSize)
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
				CallbackId = 1324
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1324);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00017A9A File Offset: 0x00015C9A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageUpdateUserPublishedItemVoteResult_t.OnResult(param);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00017AA2 File Offset: 0x00015CA2
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageUpdateUserPublishedItemVoteResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00017AAC File Offset: 0x00015CAC
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageUpdateUserPublishedItemVoteResult_t.OnGetSize();
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00017AB3 File Offset: 0x00015CB3
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageUpdateUserPublishedItemVoteResult_t.StructSize();
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00017ABA File Offset: 0x00015CBA
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageUpdateUserPublishedItemVoteResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00017ACC File Offset: 0x00015CCC
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageUpdateUserPublishedItemVoteResult_t data = RemoteStorageUpdateUserPublishedItemVoteResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageUpdateUserPublishedItemVoteResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageUpdateUserPublishedItemVoteResult_t>(data);
			}
		}

		// Token: 0x040005B2 RID: 1458
		internal const int CallbackId = 1324;

		// Token: 0x040005B3 RID: 1459
		internal Result Result;

		// Token: 0x040005B4 RID: 1460
		internal ulong PublishedFileId;

		// Token: 0x020001D9 RID: 473
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D15 RID: 7445 RVA: 0x000621D8 File Offset: 0x000603D8
			public static implicit operator RemoteStorageUpdateUserPublishedItemVoteResult_t(RemoteStorageUpdateUserPublishedItemVoteResult_t.PackSmall d)
			{
				return new RemoteStorageUpdateUserPublishedItemVoteResult_t
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId
				};
			}

			// Token: 0x04000A22 RID: 2594
			internal Result Result;

			// Token: 0x04000A23 RID: 2595
			internal ulong PublishedFileId;
		}
	}
}
