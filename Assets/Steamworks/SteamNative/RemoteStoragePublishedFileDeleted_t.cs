using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000B4 RID: 180
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStoragePublishedFileDeleted_t
	{
		// Token: 0x0600056B RID: 1387 RVA: 0x00017338 File Offset: 0x00015538
		internal static RemoteStoragePublishedFileDeleted_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStoragePublishedFileDeleted_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStoragePublishedFileDeleted_t.PackSmall));
			}
			return (RemoteStoragePublishedFileDeleted_t)Marshal.PtrToStructure(p, typeof(RemoteStoragePublishedFileDeleted_t));
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00017371 File Offset: 0x00015571
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStoragePublishedFileDeleted_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStoragePublishedFileDeleted_t));
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001739C File Offset: 0x0001559C
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStoragePublishedFileDeleted_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStoragePublishedFileDeleted_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStoragePublishedFileDeleted_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStoragePublishedFileDeleted_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStoragePublishedFileDeleted_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStoragePublishedFileDeleted_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStoragePublishedFileDeleted_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStoragePublishedFileDeleted_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStoragePublishedFileDeleted_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStoragePublishedFileDeleted_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStoragePublishedFileDeleted_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStoragePublishedFileDeleted_t.OnGetSize)
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
				CallbackId = 1323
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1323);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000176A2 File Offset: 0x000158A2
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStoragePublishedFileDeleted_t.OnResult(param);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000176AA File Offset: 0x000158AA
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStoragePublishedFileDeleted_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x000176B4 File Offset: 0x000158B4
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStoragePublishedFileDeleted_t.OnGetSize();
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000176BB File Offset: 0x000158BB
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStoragePublishedFileDeleted_t.StructSize();
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000176C2 File Offset: 0x000158C2
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStoragePublishedFileDeleted_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000176D4 File Offset: 0x000158D4
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStoragePublishedFileDeleted_t data = RemoteStoragePublishedFileDeleted_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStoragePublishedFileDeleted_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStoragePublishedFileDeleted_t>(data);
			}
		}

		// Token: 0x040005AF RID: 1455
		internal const int CallbackId = 1323;

		// Token: 0x040005B0 RID: 1456
		internal ulong PublishedFileId;

		// Token: 0x040005B1 RID: 1457
		internal uint AppID;

		// Token: 0x020001D8 RID: 472
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D14 RID: 7444 RVA: 0x000621A8 File Offset: 0x000603A8
			public static implicit operator RemoteStoragePublishedFileDeleted_t(RemoteStoragePublishedFileDeleted_t.PackSmall d)
			{
				return new RemoteStoragePublishedFileDeleted_t
				{
					PublishedFileId = d.PublishedFileId,
					AppID = d.AppID
				};
			}

			// Token: 0x04000A20 RID: 2592
			internal ulong PublishedFileId;

			// Token: 0x04000A21 RID: 2593
			internal uint AppID;
		}
	}
}
