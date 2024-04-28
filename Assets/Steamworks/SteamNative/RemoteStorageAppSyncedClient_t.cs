using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000A2 RID: 162
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageAppSyncedClient_t
	{
		// Token: 0x060004BE RID: 1214 RVA: 0x00012CA9 File Offset: 0x00010EA9
		internal static RemoteStorageAppSyncedClient_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageAppSyncedClient_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageAppSyncedClient_t.PackSmall));
			}
			return (RemoteStorageAppSyncedClient_t)Marshal.PtrToStructure(p, typeof(RemoteStorageAppSyncedClient_t));
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00012CE2 File Offset: 0x00010EE2
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageAppSyncedClient_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageAppSyncedClient_t));
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00012D0C File Offset: 0x00010F0C
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageAppSyncedClient_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageAppSyncedClient_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageAppSyncedClient_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageAppSyncedClient_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageAppSyncedClient_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageAppSyncedClient_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageAppSyncedClient_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageAppSyncedClient_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageAppSyncedClient_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageAppSyncedClient_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageAppSyncedClient_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageAppSyncedClient_t.OnGetSize)
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
				CallbackId = 1301
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1301);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00013012 File Offset: 0x00011212
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageAppSyncedClient_t.OnResult(param);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001301A File Offset: 0x0001121A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageAppSyncedClient_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00013024 File Offset: 0x00011224
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageAppSyncedClient_t.OnGetSize();
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001302B File Offset: 0x0001122B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageAppSyncedClient_t.StructSize();
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00013032 File Offset: 0x00011232
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageAppSyncedClient_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00013044 File Offset: 0x00011244
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageAppSyncedClient_t data = RemoteStorageAppSyncedClient_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageAppSyncedClient_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageAppSyncedClient_t>(data);
			}
		}

		// Token: 0x0400054C RID: 1356
		internal const int CallbackId = 1301;

		// Token: 0x0400054D RID: 1357
		internal uint AppID;

		// Token: 0x0400054E RID: 1358
		internal Result Result;

		// Token: 0x0400054F RID: 1359
		internal int NumDownloads;

		// Token: 0x020001C6 RID: 454
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D02 RID: 7426 RVA: 0x00061BE8 File Offset: 0x0005FDE8
			public static implicit operator RemoteStorageAppSyncedClient_t(RemoteStorageAppSyncedClient_t.PackSmall d)
			{
				return new RemoteStorageAppSyncedClient_t
				{
					AppID = d.AppID,
					Result = d.Result,
					NumDownloads = d.NumDownloads
				};
			}

			// Token: 0x040009CF RID: 2511
			internal uint AppID;

			// Token: 0x040009D0 RID: 2512
			internal Result Result;

			// Token: 0x040009D1 RID: 2513
			internal int NumDownloads;
		}
	}
}
