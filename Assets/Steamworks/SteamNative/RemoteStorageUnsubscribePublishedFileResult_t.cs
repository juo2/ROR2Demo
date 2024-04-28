using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000AC RID: 172
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageUnsubscribePublishedFileResult_t
	{
		// Token: 0x0600051D RID: 1309 RVA: 0x000153B8 File Offset: 0x000135B8
		internal static RemoteStorageUnsubscribePublishedFileResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageUnsubscribePublishedFileResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageUnsubscribePublishedFileResult_t.PackSmall));
			}
			return (RemoteStorageUnsubscribePublishedFileResult_t)Marshal.PtrToStructure(p, typeof(RemoteStorageUnsubscribePublishedFileResult_t));
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000153F1 File Offset: 0x000135F1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageUnsubscribePublishedFileResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageUnsubscribePublishedFileResult_t));
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00015419 File Offset: 0x00013619
		internal static CallResult<RemoteStorageUnsubscribePublishedFileResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageUnsubscribePublishedFileResult_t, bool> CallbackFunction)
		{
			return new CallResult<RemoteStorageUnsubscribePublishedFileResult_t>(steamworks, call, CallbackFunction, new CallResult<RemoteStorageUnsubscribePublishedFileResult_t>.ConvertFromPointer(RemoteStorageUnsubscribePublishedFileResult_t.FromPointer), RemoteStorageUnsubscribePublishedFileResult_t.StructSize(), 1315);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001543C File Offset: 0x0001363C
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageUnsubscribePublishedFileResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageUnsubscribePublishedFileResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageUnsubscribePublishedFileResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageUnsubscribePublishedFileResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageUnsubscribePublishedFileResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageUnsubscribePublishedFileResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageUnsubscribePublishedFileResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageUnsubscribePublishedFileResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageUnsubscribePublishedFileResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageUnsubscribePublishedFileResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageUnsubscribePublishedFileResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageUnsubscribePublishedFileResult_t.OnGetSize)
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
				CallbackId = 1315
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1315);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00015742 File Offset: 0x00013942
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageUnsubscribePublishedFileResult_t.OnResult(param);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001574A File Offset: 0x0001394A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageUnsubscribePublishedFileResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00015754 File Offset: 0x00013954
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageUnsubscribePublishedFileResult_t.OnGetSize();
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001575B File Offset: 0x0001395B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageUnsubscribePublishedFileResult_t.StructSize();
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00015762 File Offset: 0x00013962
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageUnsubscribePublishedFileResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00015774 File Offset: 0x00013974
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageUnsubscribePublishedFileResult_t data = RemoteStorageUnsubscribePublishedFileResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageUnsubscribePublishedFileResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageUnsubscribePublishedFileResult_t>(data);
			}
		}

		// Token: 0x04000576 RID: 1398
		internal const int CallbackId = 1315;

		// Token: 0x04000577 RID: 1399
		internal Result Result;

		// Token: 0x04000578 RID: 1400
		internal ulong PublishedFileId;

		// Token: 0x020001D0 RID: 464
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D0C RID: 7436 RVA: 0x00061E74 File Offset: 0x00060074
			public static implicit operator RemoteStorageUnsubscribePublishedFileResult_t(RemoteStorageUnsubscribePublishedFileResult_t.PackSmall d)
			{
				return new RemoteStorageUnsubscribePublishedFileResult_t
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId
				};
			}

			// Token: 0x040009EF RID: 2543
			internal Result Result;

			// Token: 0x040009F0 RID: 2544
			internal ulong PublishedFileId;
		}
	}
}
