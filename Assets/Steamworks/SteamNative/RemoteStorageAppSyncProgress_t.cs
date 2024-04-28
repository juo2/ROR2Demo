using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000A4 RID: 164
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageAppSyncProgress_t
	{
		// Token: 0x060004D0 RID: 1232 RVA: 0x00013458 File Offset: 0x00011658
		internal static RemoteStorageAppSyncProgress_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageAppSyncProgress_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageAppSyncProgress_t.PackSmall));
			}
			return (RemoteStorageAppSyncProgress_t)Marshal.PtrToStructure(p, typeof(RemoteStorageAppSyncProgress_t));
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00013491 File Offset: 0x00011691
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageAppSyncProgress_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageAppSyncProgress_t));
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000134BC File Offset: 0x000116BC
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageAppSyncProgress_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageAppSyncProgress_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageAppSyncProgress_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageAppSyncProgress_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageAppSyncProgress_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageAppSyncProgress_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageAppSyncProgress_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageAppSyncProgress_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageAppSyncProgress_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageAppSyncProgress_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageAppSyncProgress_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageAppSyncProgress_t.OnGetSize)
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
				CallbackId = 1303
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1303);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000137C2 File Offset: 0x000119C2
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageAppSyncProgress_t.OnResult(param);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000137CA File Offset: 0x000119CA
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageAppSyncProgress_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000137D4 File Offset: 0x000119D4
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageAppSyncProgress_t.OnGetSize();
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000137DB File Offset: 0x000119DB
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageAppSyncProgress_t.StructSize();
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000137E2 File Offset: 0x000119E2
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageAppSyncProgress_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000137F4 File Offset: 0x000119F4
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageAppSyncProgress_t data = RemoteStorageAppSyncProgress_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageAppSyncProgress_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageAppSyncProgress_t>(data);
			}
		}

		// Token: 0x04000554 RID: 1364
		internal const int CallbackId = 1303;

		// Token: 0x04000555 RID: 1365
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string CurrentFile;

		// Token: 0x04000556 RID: 1366
		internal uint AppID;

		// Token: 0x04000557 RID: 1367
		internal uint BytesTransferredThisChunk;

		// Token: 0x04000558 RID: 1368
		internal double DAppPercentComplete;

		// Token: 0x04000559 RID: 1369
		[MarshalAs(UnmanagedType.I1)]
		internal bool Uploading;

		// Token: 0x020001C8 RID: 456
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D04 RID: 7428 RVA: 0x00061C68 File Offset: 0x0005FE68
			public static implicit operator RemoteStorageAppSyncProgress_t(RemoteStorageAppSyncProgress_t.PackSmall d)
			{
				return new RemoteStorageAppSyncProgress_t
				{
					CurrentFile = d.CurrentFile,
					AppID = d.AppID,
					BytesTransferredThisChunk = d.BytesTransferredThisChunk,
					DAppPercentComplete = d.DAppPercentComplete,
					Uploading = d.Uploading
				};
			}

			// Token: 0x040009D5 RID: 2517
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string CurrentFile;

			// Token: 0x040009D6 RID: 2518
			internal uint AppID;

			// Token: 0x040009D7 RID: 2519
			internal uint BytesTransferredThisChunk;

			// Token: 0x040009D8 RID: 2520
			internal double DAppPercentComplete;

			// Token: 0x040009D9 RID: 2521
			[MarshalAs(UnmanagedType.I1)]
			internal bool Uploading;
		}
	}
}
