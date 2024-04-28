using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000A5 RID: 165
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageAppSyncStatusCheck_t
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x00013830 File Offset: 0x00011A30
		internal static RemoteStorageAppSyncStatusCheck_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageAppSyncStatusCheck_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageAppSyncStatusCheck_t.PackSmall));
			}
			return (RemoteStorageAppSyncStatusCheck_t)Marshal.PtrToStructure(p, typeof(RemoteStorageAppSyncStatusCheck_t));
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00013869 File Offset: 0x00011A69
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageAppSyncStatusCheck_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageAppSyncStatusCheck_t));
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00013894 File Offset: 0x00011A94
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageAppSyncStatusCheck_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageAppSyncStatusCheck_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageAppSyncStatusCheck_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageAppSyncStatusCheck_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageAppSyncStatusCheck_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageAppSyncStatusCheck_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageAppSyncStatusCheck_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageAppSyncStatusCheck_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageAppSyncStatusCheck_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageAppSyncStatusCheck_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageAppSyncStatusCheck_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageAppSyncStatusCheck_t.OnGetSize)
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
				CallbackId = 1305
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1305);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00013B9A File Offset: 0x00011D9A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageAppSyncStatusCheck_t.OnResult(param);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00013BA2 File Offset: 0x00011DA2
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageAppSyncStatusCheck_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00013BAC File Offset: 0x00011DAC
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageAppSyncStatusCheck_t.OnGetSize();
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00013BB3 File Offset: 0x00011DB3
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageAppSyncStatusCheck_t.StructSize();
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00013BBA File Offset: 0x00011DBA
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageAppSyncStatusCheck_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00013BCC File Offset: 0x00011DCC
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageAppSyncStatusCheck_t data = RemoteStorageAppSyncStatusCheck_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageAppSyncStatusCheck_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageAppSyncStatusCheck_t>(data);
			}
		}

		// Token: 0x0400055A RID: 1370
		internal const int CallbackId = 1305;

		// Token: 0x0400055B RID: 1371
		internal uint AppID;

		// Token: 0x0400055C RID: 1372
		internal Result Result;

		// Token: 0x020001C9 RID: 457
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D05 RID: 7429 RVA: 0x00061CC0 File Offset: 0x0005FEC0
			public static implicit operator RemoteStorageAppSyncStatusCheck_t(RemoteStorageAppSyncStatusCheck_t.PackSmall d)
			{
				return new RemoteStorageAppSyncStatusCheck_t
				{
					AppID = d.AppID,
					Result = d.Result
				};
			}

			// Token: 0x040009DA RID: 2522
			internal uint AppID;

			// Token: 0x040009DB RID: 2523
			internal Result Result;
		}
	}
}
