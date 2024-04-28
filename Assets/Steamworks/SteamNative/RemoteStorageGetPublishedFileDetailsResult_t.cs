using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000AF RID: 175
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageGetPublishedFileDetailsResult_t
	{
		// Token: 0x0600053B RID: 1339 RVA: 0x00015FA0 File Offset: 0x000141A0
		internal static RemoteStorageGetPublishedFileDetailsResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageGetPublishedFileDetailsResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageGetPublishedFileDetailsResult_t.PackSmall));
			}
			return (RemoteStorageGetPublishedFileDetailsResult_t)Marshal.PtrToStructure(p, typeof(RemoteStorageGetPublishedFileDetailsResult_t));
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00015FD9 File Offset: 0x000141D9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageGetPublishedFileDetailsResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageGetPublishedFileDetailsResult_t));
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00016001 File Offset: 0x00014201
		internal static CallResult<RemoteStorageGetPublishedFileDetailsResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageGetPublishedFileDetailsResult_t, bool> CallbackFunction)
		{
			return new CallResult<RemoteStorageGetPublishedFileDetailsResult_t>(steamworks, call, CallbackFunction, new CallResult<RemoteStorageGetPublishedFileDetailsResult_t>.ConvertFromPointer(RemoteStorageGetPublishedFileDetailsResult_t.FromPointer), RemoteStorageGetPublishedFileDetailsResult_t.StructSize(), 1318);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00016024 File Offset: 0x00014224
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageGetPublishedFileDetailsResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageGetPublishedFileDetailsResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageGetPublishedFileDetailsResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageGetPublishedFileDetailsResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageGetPublishedFileDetailsResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageGetPublishedFileDetailsResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageGetPublishedFileDetailsResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageGetPublishedFileDetailsResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageGetPublishedFileDetailsResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageGetPublishedFileDetailsResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageGetPublishedFileDetailsResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageGetPublishedFileDetailsResult_t.OnGetSize)
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
				CallbackId = 1318
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1318);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001632A File Offset: 0x0001452A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageGetPublishedFileDetailsResult_t.OnResult(param);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00016332 File Offset: 0x00014532
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageGetPublishedFileDetailsResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001633C File Offset: 0x0001453C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageGetPublishedFileDetailsResult_t.OnGetSize();
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00016343 File Offset: 0x00014543
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageGetPublishedFileDetailsResult_t.StructSize();
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001634A File Offset: 0x0001454A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageGetPublishedFileDetailsResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001635C File Offset: 0x0001455C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageGetPublishedFileDetailsResult_t data = RemoteStorageGetPublishedFileDetailsResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageGetPublishedFileDetailsResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageGetPublishedFileDetailsResult_t>(data);
			}
		}

		// Token: 0x04000584 RID: 1412
		internal const int CallbackId = 1318;

		// Token: 0x04000585 RID: 1413
		internal Result Result;

		// Token: 0x04000586 RID: 1414
		internal ulong PublishedFileId;

		// Token: 0x04000587 RID: 1415
		internal uint CreatorAppID;

		// Token: 0x04000588 RID: 1416
		internal uint ConsumerAppID;

		// Token: 0x04000589 RID: 1417
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
		internal string Title;

		// Token: 0x0400058A RID: 1418
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
		internal string Description;

		// Token: 0x0400058B RID: 1419
		internal ulong File;

		// Token: 0x0400058C RID: 1420
		internal ulong PreviewFile;

		// Token: 0x0400058D RID: 1421
		internal ulong SteamIDOwner;

		// Token: 0x0400058E RID: 1422
		internal uint TimeCreated;

		// Token: 0x0400058F RID: 1423
		internal uint TimeUpdated;

		// Token: 0x04000590 RID: 1424
		internal RemoteStoragePublishedFileVisibility Visibility;

		// Token: 0x04000591 RID: 1425
		[MarshalAs(UnmanagedType.I1)]
		internal bool Banned;

		// Token: 0x04000592 RID: 1426
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
		internal string Tags;

		// Token: 0x04000593 RID: 1427
		[MarshalAs(UnmanagedType.I1)]
		internal bool TagsTruncated;

		// Token: 0x04000594 RID: 1428
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string PchFileName;

		// Token: 0x04000595 RID: 1429
		internal int FileSize;

		// Token: 0x04000596 RID: 1430
		internal int PreviewFileSize;

		// Token: 0x04000597 RID: 1431
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string URL;

		// Token: 0x04000598 RID: 1432
		internal WorkshopFileType FileType;

		// Token: 0x04000599 RID: 1433
		[MarshalAs(UnmanagedType.I1)]
		internal bool AcceptedForUse;

		// Token: 0x020001D3 RID: 467
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D0F RID: 7439 RVA: 0x00061F48 File Offset: 0x00060148
			public static implicit operator RemoteStorageGetPublishedFileDetailsResult_t(RemoteStorageGetPublishedFileDetailsResult_t.PackSmall d)
			{
				return new RemoteStorageGetPublishedFileDetailsResult_t
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					CreatorAppID = d.CreatorAppID,
					ConsumerAppID = d.ConsumerAppID,
					Title = d.Title,
					Description = d.Description,
					File = d.File,
					PreviewFile = d.PreviewFile,
					SteamIDOwner = d.SteamIDOwner,
					TimeCreated = d.TimeCreated,
					TimeUpdated = d.TimeUpdated,
					Visibility = d.Visibility,
					Banned = d.Banned,
					Tags = d.Tags,
					TagsTruncated = d.TagsTruncated,
					PchFileName = d.PchFileName,
					FileSize = d.FileSize,
					PreviewFileSize = d.PreviewFileSize,
					URL = d.URL,
					FileType = d.FileType,
					AcceptedForUse = d.AcceptedForUse
				};
			}

			// Token: 0x040009FA RID: 2554
			internal Result Result;

			// Token: 0x040009FB RID: 2555
			internal ulong PublishedFileId;

			// Token: 0x040009FC RID: 2556
			internal uint CreatorAppID;

			// Token: 0x040009FD RID: 2557
			internal uint ConsumerAppID;

			// Token: 0x040009FE RID: 2558
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
			internal string Title;

			// Token: 0x040009FF RID: 2559
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
			internal string Description;

			// Token: 0x04000A00 RID: 2560
			internal ulong File;

			// Token: 0x04000A01 RID: 2561
			internal ulong PreviewFile;

			// Token: 0x04000A02 RID: 2562
			internal ulong SteamIDOwner;

			// Token: 0x04000A03 RID: 2563
			internal uint TimeCreated;

			// Token: 0x04000A04 RID: 2564
			internal uint TimeUpdated;

			// Token: 0x04000A05 RID: 2565
			internal RemoteStoragePublishedFileVisibility Visibility;

			// Token: 0x04000A06 RID: 2566
			[MarshalAs(UnmanagedType.I1)]
			internal bool Banned;

			// Token: 0x04000A07 RID: 2567
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
			internal string Tags;

			// Token: 0x04000A08 RID: 2568
			[MarshalAs(UnmanagedType.I1)]
			internal bool TagsTruncated;

			// Token: 0x04000A09 RID: 2569
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string PchFileName;

			// Token: 0x04000A0A RID: 2570
			internal int FileSize;

			// Token: 0x04000A0B RID: 2571
			internal int PreviewFileSize;

			// Token: 0x04000A0C RID: 2572
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string URL;

			// Token: 0x04000A0D RID: 2573
			internal WorkshopFileType FileType;

			// Token: 0x04000A0E RID: 2574
			[MarshalAs(UnmanagedType.I1)]
			internal bool AcceptedForUse;
		}
	}
}
