using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000AA RID: 170
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageSubscribePublishedFileResult_t
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x00014BC8 File Offset: 0x00012DC8
		internal static RemoteStorageSubscribePublishedFileResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageSubscribePublishedFileResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageSubscribePublishedFileResult_t.PackSmall));
			}
			return (RemoteStorageSubscribePublishedFileResult_t)Marshal.PtrToStructure(p, typeof(RemoteStorageSubscribePublishedFileResult_t));
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00014C01 File Offset: 0x00012E01
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageSubscribePublishedFileResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageSubscribePublishedFileResult_t));
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00014C29 File Offset: 0x00012E29
		internal static CallResult<RemoteStorageSubscribePublishedFileResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageSubscribePublishedFileResult_t, bool> CallbackFunction)
		{
			return new CallResult<RemoteStorageSubscribePublishedFileResult_t>(steamworks, call, CallbackFunction, new CallResult<RemoteStorageSubscribePublishedFileResult_t>.ConvertFromPointer(RemoteStorageSubscribePublishedFileResult_t.FromPointer), RemoteStorageSubscribePublishedFileResult_t.StructSize(), 1313);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00014C4C File Offset: 0x00012E4C
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageSubscribePublishedFileResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageSubscribePublishedFileResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageSubscribePublishedFileResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageSubscribePublishedFileResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageSubscribePublishedFileResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageSubscribePublishedFileResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageSubscribePublishedFileResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageSubscribePublishedFileResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageSubscribePublishedFileResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageSubscribePublishedFileResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageSubscribePublishedFileResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageSubscribePublishedFileResult_t.OnGetSize)
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
				CallbackId = 1313
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1313);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00014F52 File Offset: 0x00013152
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageSubscribePublishedFileResult_t.OnResult(param);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00014F5A File Offset: 0x0001315A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageSubscribePublishedFileResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00014F64 File Offset: 0x00013164
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageSubscribePublishedFileResult_t.OnGetSize();
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00014F6B File Offset: 0x0001316B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageSubscribePublishedFileResult_t.StructSize();
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00014F72 File Offset: 0x00013172
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageSubscribePublishedFileResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00014F84 File Offset: 0x00013184
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageSubscribePublishedFileResult_t data = RemoteStorageSubscribePublishedFileResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageSubscribePublishedFileResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageSubscribePublishedFileResult_t>(data);
			}
		}

		// Token: 0x0400056D RID: 1389
		internal const int CallbackId = 1313;

		// Token: 0x0400056E RID: 1390
		internal Result Result;

		// Token: 0x0400056F RID: 1391
		internal ulong PublishedFileId;

		// Token: 0x020001CE RID: 462
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D0A RID: 7434 RVA: 0x00061DEC File Offset: 0x0005FFEC
			public static implicit operator RemoteStorageSubscribePublishedFileResult_t(RemoteStorageSubscribePublishedFileResult_t.PackSmall d)
			{
				return new RemoteStorageSubscribePublishedFileResult_t
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId
				};
			}

			// Token: 0x040009E8 RID: 2536
			internal Result Result;

			// Token: 0x040009E9 RID: 2537
			internal ulong PublishedFileId;
		}
	}
}
