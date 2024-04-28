using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000B2 RID: 178
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStoragePublishedFileSubscribed_t
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x00016B88 File Offset: 0x00014D88
		internal static RemoteStoragePublishedFileSubscribed_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStoragePublishedFileSubscribed_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStoragePublishedFileSubscribed_t.PackSmall));
			}
			return (RemoteStoragePublishedFileSubscribed_t)Marshal.PtrToStructure(p, typeof(RemoteStoragePublishedFileSubscribed_t));
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00016BC1 File Offset: 0x00014DC1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStoragePublishedFileSubscribed_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStoragePublishedFileSubscribed_t));
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00016BEC File Offset: 0x00014DEC
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStoragePublishedFileSubscribed_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStoragePublishedFileSubscribed_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStoragePublishedFileSubscribed_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStoragePublishedFileSubscribed_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStoragePublishedFileSubscribed_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStoragePublishedFileSubscribed_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStoragePublishedFileSubscribed_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStoragePublishedFileSubscribed_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStoragePublishedFileSubscribed_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStoragePublishedFileSubscribed_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStoragePublishedFileSubscribed_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStoragePublishedFileSubscribed_t.OnGetSize)
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
				CallbackId = 1321
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1321);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00016EF2 File Offset: 0x000150F2
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStoragePublishedFileSubscribed_t.OnResult(param);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00016EFA File Offset: 0x000150FA
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStoragePublishedFileSubscribed_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00016F04 File Offset: 0x00015104
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStoragePublishedFileSubscribed_t.OnGetSize();
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00016F0B File Offset: 0x0001510B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStoragePublishedFileSubscribed_t.StructSize();
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00016F12 File Offset: 0x00015112
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStoragePublishedFileSubscribed_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00016F24 File Offset: 0x00015124
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStoragePublishedFileSubscribed_t data = RemoteStoragePublishedFileSubscribed_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStoragePublishedFileSubscribed_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStoragePublishedFileSubscribed_t>(data);
			}
		}

		// Token: 0x040005A9 RID: 1449
		internal const int CallbackId = 1321;

		// Token: 0x040005AA RID: 1450
		internal ulong PublishedFileId;

		// Token: 0x040005AB RID: 1451
		internal uint AppID;

		// Token: 0x020001D6 RID: 470
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D12 RID: 7442 RVA: 0x00062148 File Offset: 0x00060348
			public static implicit operator RemoteStoragePublishedFileSubscribed_t(RemoteStoragePublishedFileSubscribed_t.PackSmall d)
			{
				return new RemoteStoragePublishedFileSubscribed_t
				{
					PublishedFileId = d.PublishedFileId,
					AppID = d.AppID
				};
			}

			// Token: 0x04000A1C RID: 2588
			internal ulong PublishedFileId;

			// Token: 0x04000A1D RID: 2589
			internal uint AppID;
		}
	}
}
