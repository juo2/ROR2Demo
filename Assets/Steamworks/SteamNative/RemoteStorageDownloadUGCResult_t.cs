using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000AE RID: 174
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageDownloadUGCResult_t
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x00015BA8 File Offset: 0x00013DA8
		internal static RemoteStorageDownloadUGCResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageDownloadUGCResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageDownloadUGCResult_t.PackSmall));
			}
			return (RemoteStorageDownloadUGCResult_t)Marshal.PtrToStructure(p, typeof(RemoteStorageDownloadUGCResult_t));
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00015BE1 File Offset: 0x00013DE1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageDownloadUGCResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageDownloadUGCResult_t));
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00015C09 File Offset: 0x00013E09
		internal static CallResult<RemoteStorageDownloadUGCResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageDownloadUGCResult_t, bool> CallbackFunction)
		{
			return new CallResult<RemoteStorageDownloadUGCResult_t>(steamworks, call, CallbackFunction, new CallResult<RemoteStorageDownloadUGCResult_t>.ConvertFromPointer(RemoteStorageDownloadUGCResult_t.FromPointer), RemoteStorageDownloadUGCResult_t.StructSize(), 1317);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00015C2C File Offset: 0x00013E2C
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageDownloadUGCResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageDownloadUGCResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageDownloadUGCResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageDownloadUGCResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageDownloadUGCResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageDownloadUGCResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageDownloadUGCResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageDownloadUGCResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageDownloadUGCResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageDownloadUGCResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageDownloadUGCResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageDownloadUGCResult_t.OnGetSize)
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
				CallbackId = 1317
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1317);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00015F32 File Offset: 0x00014132
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageDownloadUGCResult_t.OnResult(param);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00015F3A File Offset: 0x0001413A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageDownloadUGCResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00015F44 File Offset: 0x00014144
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageDownloadUGCResult_t.OnGetSize();
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00015F4B File Offset: 0x0001414B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageDownloadUGCResult_t.StructSize();
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00015F52 File Offset: 0x00014152
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageDownloadUGCResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00015F64 File Offset: 0x00014164
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageDownloadUGCResult_t data = RemoteStorageDownloadUGCResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageDownloadUGCResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageDownloadUGCResult_t>(data);
			}
		}

		// Token: 0x0400057D RID: 1405
		internal const int CallbackId = 1317;

		// Token: 0x0400057E RID: 1406
		internal Result Result;

		// Token: 0x0400057F RID: 1407
		internal ulong File;

		// Token: 0x04000580 RID: 1408
		internal uint AppID;

		// Token: 0x04000581 RID: 1409
		internal int SizeInBytes;

		// Token: 0x04000582 RID: 1410
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string PchFileName;

		// Token: 0x04000583 RID: 1411
		internal ulong SteamIDOwner;

		// Token: 0x020001D2 RID: 466
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D0E RID: 7438 RVA: 0x00061EE4 File Offset: 0x000600E4
			public static implicit operator RemoteStorageDownloadUGCResult_t(RemoteStorageDownloadUGCResult_t.PackSmall d)
			{
				return new RemoteStorageDownloadUGCResult_t
				{
					Result = d.Result,
					File = d.File,
					AppID = d.AppID,
					SizeInBytes = d.SizeInBytes,
					PchFileName = d.PchFileName,
					SteamIDOwner = d.SteamIDOwner
				};
			}

			// Token: 0x040009F4 RID: 2548
			internal Result Result;

			// Token: 0x040009F5 RID: 2549
			internal ulong File;

			// Token: 0x040009F6 RID: 2550
			internal uint AppID;

			// Token: 0x040009F7 RID: 2551
			internal int SizeInBytes;

			// Token: 0x040009F8 RID: 2552
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string PchFileName;

			// Token: 0x040009F9 RID: 2553
			internal ulong SteamIDOwner;
		}
	}
}
