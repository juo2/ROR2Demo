using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000B8 RID: 184
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageSetUserPublishedFileActionResult_t
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x000182B8 File Offset: 0x000164B8
		internal static RemoteStorageSetUserPublishedFileActionResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageSetUserPublishedFileActionResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageSetUserPublishedFileActionResult_t.PackSmall));
			}
			return (RemoteStorageSetUserPublishedFileActionResult_t)Marshal.PtrToStructure(p, typeof(RemoteStorageSetUserPublishedFileActionResult_t));
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000182F1 File Offset: 0x000164F1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageSetUserPublishedFileActionResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageSetUserPublishedFileActionResult_t));
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00018319 File Offset: 0x00016519
		internal static CallResult<RemoteStorageSetUserPublishedFileActionResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageSetUserPublishedFileActionResult_t, bool> CallbackFunction)
		{
			return new CallResult<RemoteStorageSetUserPublishedFileActionResult_t>(steamworks, call, CallbackFunction, new CallResult<RemoteStorageSetUserPublishedFileActionResult_t>.ConvertFromPointer(RemoteStorageSetUserPublishedFileActionResult_t.FromPointer), RemoteStorageSetUserPublishedFileActionResult_t.StructSize(), 1327);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001833C File Offset: 0x0001653C
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageSetUserPublishedFileActionResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageSetUserPublishedFileActionResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageSetUserPublishedFileActionResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageSetUserPublishedFileActionResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageSetUserPublishedFileActionResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageSetUserPublishedFileActionResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageSetUserPublishedFileActionResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageSetUserPublishedFileActionResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageSetUserPublishedFileActionResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageSetUserPublishedFileActionResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageSetUserPublishedFileActionResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageSetUserPublishedFileActionResult_t.OnGetSize)
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
				CallbackId = 1327
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1327);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00018642 File Offset: 0x00016842
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageSetUserPublishedFileActionResult_t.OnResult(param);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001864A File Offset: 0x0001684A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageSetUserPublishedFileActionResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00018654 File Offset: 0x00016854
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageSetUserPublishedFileActionResult_t.OnGetSize();
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001865B File Offset: 0x0001685B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageSetUserPublishedFileActionResult_t.StructSize();
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00018662 File Offset: 0x00016862
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageSetUserPublishedFileActionResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00018674 File Offset: 0x00016874
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageSetUserPublishedFileActionResult_t data = RemoteStorageSetUserPublishedFileActionResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageSetUserPublishedFileActionResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageSetUserPublishedFileActionResult_t>(data);
			}
		}

		// Token: 0x040005BE RID: 1470
		internal const int CallbackId = 1327;

		// Token: 0x040005BF RID: 1471
		internal Result Result;

		// Token: 0x040005C0 RID: 1472
		internal ulong PublishedFileId;

		// Token: 0x040005C1 RID: 1473
		internal WorkshopFileAction Action;

		// Token: 0x020001DC RID: 476
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D18 RID: 7448 RVA: 0x00062294 File Offset: 0x00060494
			public static implicit operator RemoteStorageSetUserPublishedFileActionResult_t(RemoteStorageSetUserPublishedFileActionResult_t.PackSmall d)
			{
				return new RemoteStorageSetUserPublishedFileActionResult_t
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					Action = d.Action
				};
			}

			// Token: 0x04000A2B RID: 2603
			internal Result Result;

			// Token: 0x04000A2C RID: 2604
			internal ulong PublishedFileId;

			// Token: 0x04000A2D RID: 2605
			internal WorkshopFileAction Action;
		}
	}
}
