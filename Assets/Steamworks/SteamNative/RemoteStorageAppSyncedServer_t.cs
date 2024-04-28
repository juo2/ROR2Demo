using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000A3 RID: 163
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageAppSyncedServer_t
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x00013080 File Offset: 0x00011280
		internal static RemoteStorageAppSyncedServer_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageAppSyncedServer_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageAppSyncedServer_t.PackSmall));
			}
			return (RemoteStorageAppSyncedServer_t)Marshal.PtrToStructure(p, typeof(RemoteStorageAppSyncedServer_t));
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000130B9 File Offset: 0x000112B9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageAppSyncedServer_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageAppSyncedServer_t));
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000130E4 File Offset: 0x000112E4
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageAppSyncedServer_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageAppSyncedServer_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageAppSyncedServer_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageAppSyncedServer_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageAppSyncedServer_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageAppSyncedServer_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageAppSyncedServer_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageAppSyncedServer_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageAppSyncedServer_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageAppSyncedServer_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageAppSyncedServer_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageAppSyncedServer_t.OnGetSize)
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
				CallbackId = 1302
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1302);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000133EA File Offset: 0x000115EA
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageAppSyncedServer_t.OnResult(param);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000133F2 File Offset: 0x000115F2
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageAppSyncedServer_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000133FC File Offset: 0x000115FC
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageAppSyncedServer_t.OnGetSize();
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00013403 File Offset: 0x00011603
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageAppSyncedServer_t.StructSize();
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001340A File Offset: 0x0001160A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageAppSyncedServer_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001341C File Offset: 0x0001161C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageAppSyncedServer_t data = RemoteStorageAppSyncedServer_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageAppSyncedServer_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageAppSyncedServer_t>(data);
			}
		}

		// Token: 0x04000550 RID: 1360
		internal const int CallbackId = 1302;

		// Token: 0x04000551 RID: 1361
		internal uint AppID;

		// Token: 0x04000552 RID: 1362
		internal Result Result;

		// Token: 0x04000553 RID: 1363
		internal int NumUploads;

		// Token: 0x020001C7 RID: 455
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D03 RID: 7427 RVA: 0x00061C28 File Offset: 0x0005FE28
			public static implicit operator RemoteStorageAppSyncedServer_t(RemoteStorageAppSyncedServer_t.PackSmall d)
			{
				return new RemoteStorageAppSyncedServer_t
				{
					AppID = d.AppID,
					Result = d.Result,
					NumUploads = d.NumUploads
				};
			}

			// Token: 0x040009D2 RID: 2514
			internal uint AppID;

			// Token: 0x040009D3 RID: 2515
			internal Result Result;

			// Token: 0x040009D4 RID: 2516
			internal int NumUploads;
		}
	}
}
