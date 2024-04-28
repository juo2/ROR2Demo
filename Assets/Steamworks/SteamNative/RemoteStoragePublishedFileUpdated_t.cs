using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000BB RID: 187
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStoragePublishedFileUpdated_t
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x00018EA0 File Offset: 0x000170A0
		internal static RemoteStoragePublishedFileUpdated_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStoragePublishedFileUpdated_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStoragePublishedFileUpdated_t.PackSmall));
			}
			return (RemoteStoragePublishedFileUpdated_t)Marshal.PtrToStructure(p, typeof(RemoteStoragePublishedFileUpdated_t));
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00018ED9 File Offset: 0x000170D9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStoragePublishedFileUpdated_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStoragePublishedFileUpdated_t));
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00018F04 File Offset: 0x00017104
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStoragePublishedFileUpdated_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStoragePublishedFileUpdated_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStoragePublishedFileUpdated_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStoragePublishedFileUpdated_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStoragePublishedFileUpdated_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStoragePublishedFileUpdated_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStoragePublishedFileUpdated_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStoragePublishedFileUpdated_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStoragePublishedFileUpdated_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStoragePublishedFileUpdated_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStoragePublishedFileUpdated_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStoragePublishedFileUpdated_t.OnGetSize)
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
				CallbackId = 1330
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1330);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001920A File Offset: 0x0001740A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStoragePublishedFileUpdated_t.OnResult(param);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00019212 File Offset: 0x00017412
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStoragePublishedFileUpdated_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001921C File Offset: 0x0001741C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStoragePublishedFileUpdated_t.OnGetSize();
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00019223 File Offset: 0x00017423
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStoragePublishedFileUpdated_t.StructSize();
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0001922A File Offset: 0x0001742A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStoragePublishedFileUpdated_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0001923C File Offset: 0x0001743C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStoragePublishedFileUpdated_t data = RemoteStoragePublishedFileUpdated_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStoragePublishedFileUpdated_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStoragePublishedFileUpdated_t>(data);
			}
		}

		// Token: 0x040005CC RID: 1484
		internal const int CallbackId = 1330;

		// Token: 0x040005CD RID: 1485
		internal ulong PublishedFileId;

		// Token: 0x040005CE RID: 1486
		internal uint AppID;

		// Token: 0x040005CF RID: 1487
		internal ulong Unused;

		// Token: 0x020001DF RID: 479
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D1B RID: 7451 RVA: 0x00062368 File Offset: 0x00060568
			public static implicit operator RemoteStoragePublishedFileUpdated_t(RemoteStoragePublishedFileUpdated_t.PackSmall d)
			{
				return new RemoteStoragePublishedFileUpdated_t
				{
					PublishedFileId = d.PublishedFileId,
					AppID = d.AppID,
					Unused = d.Unused
				};
			}

			// Token: 0x04000A36 RID: 2614
			internal ulong PublishedFileId;

			// Token: 0x04000A37 RID: 2615
			internal uint AppID;

			// Token: 0x04000A38 RID: 2616
			internal ulong Unused;
		}
	}
}
