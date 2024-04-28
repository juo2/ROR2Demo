using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000B0 RID: 176
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageEnumerateWorkshopFilesResult_t
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x00016398 File Offset: 0x00014598
		internal static RemoteStorageEnumerateWorkshopFilesResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageEnumerateWorkshopFilesResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageEnumerateWorkshopFilesResult_t.PackSmall));
			}
			return (RemoteStorageEnumerateWorkshopFilesResult_t)Marshal.PtrToStructure(p, typeof(RemoteStorageEnumerateWorkshopFilesResult_t));
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x000163D1 File Offset: 0x000145D1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageEnumerateWorkshopFilesResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageEnumerateWorkshopFilesResult_t));
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x000163F9 File Offset: 0x000145F9
		internal static CallResult<RemoteStorageEnumerateWorkshopFilesResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageEnumerateWorkshopFilesResult_t, bool> CallbackFunction)
		{
			return new CallResult<RemoteStorageEnumerateWorkshopFilesResult_t>(steamworks, call, CallbackFunction, new CallResult<RemoteStorageEnumerateWorkshopFilesResult_t>.ConvertFromPointer(RemoteStorageEnumerateWorkshopFilesResult_t.FromPointer), RemoteStorageEnumerateWorkshopFilesResult_t.StructSize(), 1319);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001641C File Offset: 0x0001461C
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageEnumerateWorkshopFilesResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageEnumerateWorkshopFilesResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageEnumerateWorkshopFilesResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageEnumerateWorkshopFilesResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageEnumerateWorkshopFilesResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageEnumerateWorkshopFilesResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageEnumerateWorkshopFilesResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageEnumerateWorkshopFilesResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageEnumerateWorkshopFilesResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageEnumerateWorkshopFilesResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageEnumerateWorkshopFilesResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageEnumerateWorkshopFilesResult_t.OnGetSize)
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
				CallbackId = 1319
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1319);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00016722 File Offset: 0x00014922
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageEnumerateWorkshopFilesResult_t.OnResult(param);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001672A File Offset: 0x0001492A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageEnumerateWorkshopFilesResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00016734 File Offset: 0x00014934
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageEnumerateWorkshopFilesResult_t.OnGetSize();
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001673B File Offset: 0x0001493B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageEnumerateWorkshopFilesResult_t.StructSize();
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00016742 File Offset: 0x00014942
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageEnumerateWorkshopFilesResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00016754 File Offset: 0x00014954
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageEnumerateWorkshopFilesResult_t data = RemoteStorageEnumerateWorkshopFilesResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageEnumerateWorkshopFilesResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageEnumerateWorkshopFilesResult_t>(data);
			}
		}

		// Token: 0x0400059A RID: 1434
		internal const int CallbackId = 1319;

		// Token: 0x0400059B RID: 1435
		internal Result Result;

		// Token: 0x0400059C RID: 1436
		internal int ResultsReturned;

		// Token: 0x0400059D RID: 1437
		internal int TotalResultCount;

		// Token: 0x0400059E RID: 1438
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal ulong[] GPublishedFileId;

		// Token: 0x0400059F RID: 1439
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.R4)]
		internal float[] GScore;

		// Token: 0x040005A0 RID: 1440
		internal uint AppId;

		// Token: 0x040005A1 RID: 1441
		internal uint StartIndex;

		// Token: 0x020001D4 RID: 468
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D10 RID: 7440 RVA: 0x00062070 File Offset: 0x00060270
			public static implicit operator RemoteStorageEnumerateWorkshopFilesResult_t(RemoteStorageEnumerateWorkshopFilesResult_t.PackSmall d)
			{
				return new RemoteStorageEnumerateWorkshopFilesResult_t
				{
					Result = d.Result,
					ResultsReturned = d.ResultsReturned,
					TotalResultCount = d.TotalResultCount,
					GPublishedFileId = d.GPublishedFileId,
					GScore = d.GScore,
					AppId = d.AppId,
					StartIndex = d.StartIndex
				};
			}

			// Token: 0x04000A0F RID: 2575
			internal Result Result;

			// Token: 0x04000A10 RID: 2576
			internal int ResultsReturned;

			// Token: 0x04000A11 RID: 2577
			internal int TotalResultCount;

			// Token: 0x04000A12 RID: 2578
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GPublishedFileId;

			// Token: 0x04000A13 RID: 2579
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.R4)]
			internal float[] GScore;

			// Token: 0x04000A14 RID: 2580
			internal uint AppId;

			// Token: 0x04000A15 RID: 2581
			internal uint StartIndex;
		}
	}
}
