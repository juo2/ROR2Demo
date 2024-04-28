using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000B3 RID: 179
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStoragePublishedFileUnsubscribed_t
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x00016F60 File Offset: 0x00015160
		internal static RemoteStoragePublishedFileUnsubscribed_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStoragePublishedFileUnsubscribed_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStoragePublishedFileUnsubscribed_t.PackSmall));
			}
			return (RemoteStoragePublishedFileUnsubscribed_t)Marshal.PtrToStructure(p, typeof(RemoteStoragePublishedFileUnsubscribed_t));
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00016F99 File Offset: 0x00015199
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStoragePublishedFileUnsubscribed_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStoragePublishedFileUnsubscribed_t));
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00016FC4 File Offset: 0x000151C4
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStoragePublishedFileUnsubscribed_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStoragePublishedFileUnsubscribed_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStoragePublishedFileUnsubscribed_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStoragePublishedFileUnsubscribed_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStoragePublishedFileUnsubscribed_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStoragePublishedFileUnsubscribed_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStoragePublishedFileUnsubscribed_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStoragePublishedFileUnsubscribed_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStoragePublishedFileUnsubscribed_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStoragePublishedFileUnsubscribed_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStoragePublishedFileUnsubscribed_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStoragePublishedFileUnsubscribed_t.OnGetSize)
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
				CallbackId = 1322
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1322);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000172CA File Offset: 0x000154CA
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStoragePublishedFileUnsubscribed_t.OnResult(param);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000172D2 File Offset: 0x000154D2
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStoragePublishedFileUnsubscribed_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000172DC File Offset: 0x000154DC
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStoragePublishedFileUnsubscribed_t.OnGetSize();
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x000172E3 File Offset: 0x000154E3
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStoragePublishedFileUnsubscribed_t.StructSize();
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000172EA File Offset: 0x000154EA
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStoragePublishedFileUnsubscribed_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000172FC File Offset: 0x000154FC
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStoragePublishedFileUnsubscribed_t data = RemoteStoragePublishedFileUnsubscribed_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStoragePublishedFileUnsubscribed_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStoragePublishedFileUnsubscribed_t>(data);
			}
		}

		// Token: 0x040005AC RID: 1452
		internal const int CallbackId = 1322;

		// Token: 0x040005AD RID: 1453
		internal ulong PublishedFileId;

		// Token: 0x040005AE RID: 1454
		internal uint AppID;

		// Token: 0x020001D7 RID: 471
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D13 RID: 7443 RVA: 0x00062178 File Offset: 0x00060378
			public static implicit operator RemoteStoragePublishedFileUnsubscribed_t(RemoteStoragePublishedFileUnsubscribed_t.PackSmall d)
			{
				return new RemoteStoragePublishedFileUnsubscribed_t
				{
					PublishedFileId = d.PublishedFileId,
					AppID = d.AppID
				};
			}

			// Token: 0x04000A1E RID: 2590
			internal ulong PublishedFileId;

			// Token: 0x04000A1F RID: 2591
			internal uint AppID;
		}
	}
}
