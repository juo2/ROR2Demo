using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000B7 RID: 183
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageEnumerateUserSharedWorkshopFilesResult_t
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x00017EE0 File Offset: 0x000160E0
		internal static RemoteStorageEnumerateUserSharedWorkshopFilesResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.PackSmall));
			}
			return (RemoteStorageEnumerateUserSharedWorkshopFilesResult_t)Marshal.PtrToStructure(p, typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t));
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00017F19 File Offset: 0x00016119
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t));
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00017F44 File Offset: 0x00016144
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.OnGetSize)
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
				CallbackId = 1326
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1326);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001824A File Offset: 0x0001644A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.OnResult(param);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00018252 File Offset: 0x00016452
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001825C File Offset: 0x0001645C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.OnGetSize();
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00018263 File Offset: 0x00016463
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.StructSize();
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001826A File Offset: 0x0001646A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001827C File Offset: 0x0001647C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageEnumerateUserSharedWorkshopFilesResult_t data = RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageEnumerateUserSharedWorkshopFilesResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageEnumerateUserSharedWorkshopFilesResult_t>(data);
			}
		}

		// Token: 0x040005B9 RID: 1465
		internal const int CallbackId = 1326;

		// Token: 0x040005BA RID: 1466
		internal Result Result;

		// Token: 0x040005BB RID: 1467
		internal int ResultsReturned;

		// Token: 0x040005BC RID: 1468
		internal int TotalResultCount;

		// Token: 0x040005BD RID: 1469
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal ulong[] GPublishedFileId;

		// Token: 0x020001DB RID: 475
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D17 RID: 7447 RVA: 0x00062248 File Offset: 0x00060448
			public static implicit operator RemoteStorageEnumerateUserSharedWorkshopFilesResult_t(RemoteStorageEnumerateUserSharedWorkshopFilesResult_t.PackSmall d)
			{
				return new RemoteStorageEnumerateUserSharedWorkshopFilesResult_t
				{
					Result = d.Result,
					ResultsReturned = d.ResultsReturned,
					TotalResultCount = d.TotalResultCount,
					GPublishedFileId = d.GPublishedFileId
				};
			}

			// Token: 0x04000A27 RID: 2599
			internal Result Result;

			// Token: 0x04000A28 RID: 2600
			internal int ResultsReturned;

			// Token: 0x04000A29 RID: 2601
			internal int TotalResultCount;

			// Token: 0x04000A2A RID: 2602
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GPublishedFileId;
		}
	}
}
