using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000B9 RID: 185
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageEnumeratePublishedFilesByUserActionResult_t
	{
		// Token: 0x0600059A RID: 1434 RVA: 0x000186B0 File Offset: 0x000168B0
		internal static RemoteStorageEnumeratePublishedFilesByUserActionResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageEnumeratePublishedFilesByUserActionResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t.PackSmall));
			}
			return (RemoteStorageEnumeratePublishedFilesByUserActionResult_t)Marshal.PtrToStructure(p, typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t));
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x000186E9 File Offset: 0x000168E9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageEnumeratePublishedFilesByUserActionResult_t));
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00018711 File Offset: 0x00016911
		internal static CallResult<RemoteStorageEnumeratePublishedFilesByUserActionResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageEnumeratePublishedFilesByUserActionResult_t, bool> CallbackFunction)
		{
			return new CallResult<RemoteStorageEnumeratePublishedFilesByUserActionResult_t>(steamworks, call, CallbackFunction, new CallResult<RemoteStorageEnumeratePublishedFilesByUserActionResult_t>.ConvertFromPointer(RemoteStorageEnumeratePublishedFilesByUserActionResult_t.FromPointer), RemoteStorageEnumeratePublishedFilesByUserActionResult_t.StructSize(), 1328);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00018734 File Offset: 0x00016934
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageEnumeratePublishedFilesByUserActionResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageEnumeratePublishedFilesByUserActionResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageEnumeratePublishedFilesByUserActionResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageEnumeratePublishedFilesByUserActionResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageEnumeratePublishedFilesByUserActionResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageEnumeratePublishedFilesByUserActionResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageEnumeratePublishedFilesByUserActionResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageEnumeratePublishedFilesByUserActionResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageEnumeratePublishedFilesByUserActionResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageEnumeratePublishedFilesByUserActionResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageEnumeratePublishedFilesByUserActionResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageEnumeratePublishedFilesByUserActionResult_t.OnGetSize)
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
				CallbackId = 1328
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1328);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00018A3A File Offset: 0x00016C3A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageEnumeratePublishedFilesByUserActionResult_t.OnResult(param);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00018A42 File Offset: 0x00016C42
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageEnumeratePublishedFilesByUserActionResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00018A4C File Offset: 0x00016C4C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageEnumeratePublishedFilesByUserActionResult_t.OnGetSize();
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00018A53 File Offset: 0x00016C53
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageEnumeratePublishedFilesByUserActionResult_t.StructSize();
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00018A5A File Offset: 0x00016C5A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageEnumeratePublishedFilesByUserActionResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00018A6C File Offset: 0x00016C6C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageEnumeratePublishedFilesByUserActionResult_t data = RemoteStorageEnumeratePublishedFilesByUserActionResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageEnumeratePublishedFilesByUserActionResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageEnumeratePublishedFilesByUserActionResult_t>(data);
			}
		}

		// Token: 0x040005C2 RID: 1474
		internal const int CallbackId = 1328;

		// Token: 0x040005C3 RID: 1475
		internal Result Result;

		// Token: 0x040005C4 RID: 1476
		internal WorkshopFileAction Action;

		// Token: 0x040005C5 RID: 1477
		internal int ResultsReturned;

		// Token: 0x040005C6 RID: 1478
		internal int TotalResultCount;

		// Token: 0x040005C7 RID: 1479
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal ulong[] GPublishedFileId;

		// Token: 0x040005C8 RID: 1480
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
		internal uint[] GRTimeUpdated;

		// Token: 0x020001DD RID: 477
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D19 RID: 7449 RVA: 0x000622D4 File Offset: 0x000604D4
			public static implicit operator RemoteStorageEnumeratePublishedFilesByUserActionResult_t(RemoteStorageEnumeratePublishedFilesByUserActionResult_t.PackSmall d)
			{
				return new RemoteStorageEnumeratePublishedFilesByUserActionResult_t
				{
					Result = d.Result,
					Action = d.Action,
					ResultsReturned = d.ResultsReturned,
					TotalResultCount = d.TotalResultCount,
					GPublishedFileId = d.GPublishedFileId,
					GRTimeUpdated = d.GRTimeUpdated
				};
			}

			// Token: 0x04000A2E RID: 2606
			internal Result Result;

			// Token: 0x04000A2F RID: 2607
			internal WorkshopFileAction Action;

			// Token: 0x04000A30 RID: 2608
			internal int ResultsReturned;

			// Token: 0x04000A31 RID: 2609
			internal int TotalResultCount;

			// Token: 0x04000A32 RID: 2610
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GPublishedFileId;

			// Token: 0x04000A33 RID: 2611
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
			internal uint[] GRTimeUpdated;
		}
	}
}
