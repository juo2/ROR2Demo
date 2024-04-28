using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000A7 RID: 167
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStoragePublishFileResult_t
	{
		// Token: 0x060004EC RID: 1260 RVA: 0x00014000 File Offset: 0x00012200
		internal static RemoteStoragePublishFileResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStoragePublishFileResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStoragePublishFileResult_t.PackSmall));
			}
			return (RemoteStoragePublishFileResult_t)Marshal.PtrToStructure(p, typeof(RemoteStoragePublishFileResult_t));
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00014039 File Offset: 0x00012239
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStoragePublishFileResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStoragePublishFileResult_t));
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00014064 File Offset: 0x00012264
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStoragePublishFileResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStoragePublishFileResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStoragePublishFileResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStoragePublishFileResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStoragePublishFileResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStoragePublishFileResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStoragePublishFileResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStoragePublishFileResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStoragePublishFileResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStoragePublishFileResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStoragePublishFileResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStoragePublishFileResult_t.OnGetSize)
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
				CallbackId = 1309
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1309);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001436A File Offset: 0x0001256A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStoragePublishFileResult_t.OnResult(param);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00014372 File Offset: 0x00012572
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStoragePublishFileResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001437C File Offset: 0x0001257C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStoragePublishFileResult_t.OnGetSize();
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00014383 File Offset: 0x00012583
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStoragePublishFileResult_t.StructSize();
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001438A File Offset: 0x0001258A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStoragePublishFileResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001439C File Offset: 0x0001259C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStoragePublishFileResult_t data = RemoteStoragePublishFileResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStoragePublishFileResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStoragePublishFileResult_t>(data);
			}
		}

		// Token: 0x04000561 RID: 1377
		internal const int CallbackId = 1309;

		// Token: 0x04000562 RID: 1378
		internal Result Result;

		// Token: 0x04000563 RID: 1379
		internal ulong PublishedFileId;

		// Token: 0x04000564 RID: 1380
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement;

		// Token: 0x020001CB RID: 459
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D07 RID: 7431 RVA: 0x00061D30 File Offset: 0x0005FF30
			public static implicit operator RemoteStoragePublishFileResult_t(RemoteStoragePublishFileResult_t.PackSmall d)
			{
				return new RemoteStoragePublishFileResult_t
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement
				};
			}

			// Token: 0x040009DF RID: 2527
			internal Result Result;

			// Token: 0x040009E0 RID: 2528
			internal ulong PublishedFileId;

			// Token: 0x040009E1 RID: 2529
			[MarshalAs(UnmanagedType.I1)]
			internal bool UserNeedsToAcceptWorkshopLegalAgreement;
		}
	}
}
