using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000A9 RID: 169
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageEnumerateUserPublishedFilesResult_t
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x000147D0 File Offset: 0x000129D0
		internal static RemoteStorageEnumerateUserPublishedFilesResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageEnumerateUserPublishedFilesResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageEnumerateUserPublishedFilesResult_t.PackSmall));
			}
			return (RemoteStorageEnumerateUserPublishedFilesResult_t)Marshal.PtrToStructure(p, typeof(RemoteStorageEnumerateUserPublishedFilesResult_t));
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00014809 File Offset: 0x00012A09
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageEnumerateUserPublishedFilesResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageEnumerateUserPublishedFilesResult_t));
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00014831 File Offset: 0x00012A31
		internal static CallResult<RemoteStorageEnumerateUserPublishedFilesResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageEnumerateUserPublishedFilesResult_t, bool> CallbackFunction)
		{
			return new CallResult<RemoteStorageEnumerateUserPublishedFilesResult_t>(steamworks, call, CallbackFunction, new CallResult<RemoteStorageEnumerateUserPublishedFilesResult_t>.ConvertFromPointer(RemoteStorageEnumerateUserPublishedFilesResult_t.FromPointer), RemoteStorageEnumerateUserPublishedFilesResult_t.StructSize(), 1312);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00014854 File Offset: 0x00012A54
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageEnumerateUserPublishedFilesResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageEnumerateUserPublishedFilesResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageEnumerateUserPublishedFilesResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageEnumerateUserPublishedFilesResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageEnumerateUserPublishedFilesResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageEnumerateUserPublishedFilesResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageEnumerateUserPublishedFilesResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageEnumerateUserPublishedFilesResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageEnumerateUserPublishedFilesResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageEnumerateUserPublishedFilesResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageEnumerateUserPublishedFilesResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageEnumerateUserPublishedFilesResult_t.OnGetSize)
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
				CallbackId = 1312
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1312);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00014B5A File Offset: 0x00012D5A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageEnumerateUserPublishedFilesResult_t.OnResult(param);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00014B62 File Offset: 0x00012D62
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageEnumerateUserPublishedFilesResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00014B6C File Offset: 0x00012D6C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageEnumerateUserPublishedFilesResult_t.OnGetSize();
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00014B73 File Offset: 0x00012D73
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageEnumerateUserPublishedFilesResult_t.StructSize();
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00014B7A File Offset: 0x00012D7A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageEnumerateUserPublishedFilesResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00014B8C File Offset: 0x00012D8C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageEnumerateUserPublishedFilesResult_t data = RemoteStorageEnumerateUserPublishedFilesResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageEnumerateUserPublishedFilesResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageEnumerateUserPublishedFilesResult_t>(data);
			}
		}

		// Token: 0x04000568 RID: 1384
		internal const int CallbackId = 1312;

		// Token: 0x04000569 RID: 1385
		internal Result Result;

		// Token: 0x0400056A RID: 1386
		internal int ResultsReturned;

		// Token: 0x0400056B RID: 1387
		internal int TotalResultCount;

		// Token: 0x0400056C RID: 1388
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal ulong[] GPublishedFileId;

		// Token: 0x020001CD RID: 461
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D09 RID: 7433 RVA: 0x00061DA0 File Offset: 0x0005FFA0
			public static implicit operator RemoteStorageEnumerateUserPublishedFilesResult_t(RemoteStorageEnumerateUserPublishedFilesResult_t.PackSmall d)
			{
				return new RemoteStorageEnumerateUserPublishedFilesResult_t
				{
					Result = d.Result,
					ResultsReturned = d.ResultsReturned,
					TotalResultCount = d.TotalResultCount,
					GPublishedFileId = d.GPublishedFileId
				};
			}

			// Token: 0x040009E4 RID: 2532
			internal Result Result;

			// Token: 0x040009E5 RID: 2533
			internal int ResultsReturned;

			// Token: 0x040009E6 RID: 2534
			internal int TotalResultCount;

			// Token: 0x040009E7 RID: 2535
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GPublishedFileId;
		}
	}
}
