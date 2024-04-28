using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000AD RID: 173
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageUpdatePublishedFileResult_t
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x000157B0 File Offset: 0x000139B0
		internal static RemoteStorageUpdatePublishedFileResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageUpdatePublishedFileResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageUpdatePublishedFileResult_t.PackSmall));
			}
			return (RemoteStorageUpdatePublishedFileResult_t)Marshal.PtrToStructure(p, typeof(RemoteStorageUpdatePublishedFileResult_t));
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000157E9 File Offset: 0x000139E9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageUpdatePublishedFileResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageUpdatePublishedFileResult_t));
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00015811 File Offset: 0x00013A11
		internal static CallResult<RemoteStorageUpdatePublishedFileResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageUpdatePublishedFileResult_t, bool> CallbackFunction)
		{
			return new CallResult<RemoteStorageUpdatePublishedFileResult_t>(steamworks, call, CallbackFunction, new CallResult<RemoteStorageUpdatePublishedFileResult_t>.ConvertFromPointer(RemoteStorageUpdatePublishedFileResult_t.FromPointer), RemoteStorageUpdatePublishedFileResult_t.StructSize(), 1316);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00015834 File Offset: 0x00013A34
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageUpdatePublishedFileResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageUpdatePublishedFileResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageUpdatePublishedFileResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageUpdatePublishedFileResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageUpdatePublishedFileResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageUpdatePublishedFileResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageUpdatePublishedFileResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageUpdatePublishedFileResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageUpdatePublishedFileResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageUpdatePublishedFileResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageUpdatePublishedFileResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageUpdatePublishedFileResult_t.OnGetSize)
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
				CallbackId = 1316
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1316);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00015B3A File Offset: 0x00013D3A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageUpdatePublishedFileResult_t.OnResult(param);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00015B42 File Offset: 0x00013D42
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageUpdatePublishedFileResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00015B4C File Offset: 0x00013D4C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageUpdatePublishedFileResult_t.OnGetSize();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00015B53 File Offset: 0x00013D53
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageUpdatePublishedFileResult_t.StructSize();
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00015B5A File Offset: 0x00013D5A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageUpdatePublishedFileResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00015B6C File Offset: 0x00013D6C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageUpdatePublishedFileResult_t data = RemoteStorageUpdatePublishedFileResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageUpdatePublishedFileResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageUpdatePublishedFileResult_t>(data);
			}
		}

		// Token: 0x04000579 RID: 1401
		internal const int CallbackId = 1316;

		// Token: 0x0400057A RID: 1402
		internal Result Result;

		// Token: 0x0400057B RID: 1403
		internal ulong PublishedFileId;

		// Token: 0x0400057C RID: 1404
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement;

		// Token: 0x020001D1 RID: 465
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D0D RID: 7437 RVA: 0x00061EA4 File Offset: 0x000600A4
			public static implicit operator RemoteStorageUpdatePublishedFileResult_t(RemoteStorageUpdatePublishedFileResult_t.PackSmall d)
			{
				return new RemoteStorageUpdatePublishedFileResult_t
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement
				};
			}

			// Token: 0x040009F1 RID: 2545
			internal Result Result;

			// Token: 0x040009F2 RID: 2546
			internal ulong PublishedFileId;

			// Token: 0x040009F3 RID: 2547
			[MarshalAs(UnmanagedType.I1)]
			internal bool UserNeedsToAcceptWorkshopLegalAgreement;
		}
	}
}
