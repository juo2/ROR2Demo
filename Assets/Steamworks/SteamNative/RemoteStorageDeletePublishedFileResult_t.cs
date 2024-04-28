using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000A8 RID: 168
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageDeletePublishedFileResult_t
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x000143D8 File Offset: 0x000125D8
		internal static RemoteStorageDeletePublishedFileResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageDeletePublishedFileResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageDeletePublishedFileResult_t.PackSmall));
			}
			return (RemoteStorageDeletePublishedFileResult_t)Marshal.PtrToStructure(p, typeof(RemoteStorageDeletePublishedFileResult_t));
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00014411 File Offset: 0x00012611
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageDeletePublishedFileResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageDeletePublishedFileResult_t));
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00014439 File Offset: 0x00012639
		internal static CallResult<RemoteStorageDeletePublishedFileResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageDeletePublishedFileResult_t, bool> CallbackFunction)
		{
			return new CallResult<RemoteStorageDeletePublishedFileResult_t>(steamworks, call, CallbackFunction, new CallResult<RemoteStorageDeletePublishedFileResult_t>.ConvertFromPointer(RemoteStorageDeletePublishedFileResult_t.FromPointer), RemoteStorageDeletePublishedFileResult_t.StructSize(), 1311);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001445C File Offset: 0x0001265C
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageDeletePublishedFileResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageDeletePublishedFileResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageDeletePublishedFileResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageDeletePublishedFileResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageDeletePublishedFileResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageDeletePublishedFileResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageDeletePublishedFileResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageDeletePublishedFileResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageDeletePublishedFileResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageDeletePublishedFileResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageDeletePublishedFileResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageDeletePublishedFileResult_t.OnGetSize)
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
				CallbackId = 1311
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1311);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00014762 File Offset: 0x00012962
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageDeletePublishedFileResult_t.OnResult(param);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001476A File Offset: 0x0001296A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageDeletePublishedFileResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00014774 File Offset: 0x00012974
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageDeletePublishedFileResult_t.OnGetSize();
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001477B File Offset: 0x0001297B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageDeletePublishedFileResult_t.StructSize();
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00014782 File Offset: 0x00012982
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageDeletePublishedFileResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00014794 File Offset: 0x00012994
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageDeletePublishedFileResult_t data = RemoteStorageDeletePublishedFileResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageDeletePublishedFileResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageDeletePublishedFileResult_t>(data);
			}
		}

		// Token: 0x04000565 RID: 1381
		internal const int CallbackId = 1311;

		// Token: 0x04000566 RID: 1382
		internal Result Result;

		// Token: 0x04000567 RID: 1383
		internal ulong PublishedFileId;

		// Token: 0x020001CC RID: 460
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D08 RID: 7432 RVA: 0x00061D70 File Offset: 0x0005FF70
			public static implicit operator RemoteStorageDeletePublishedFileResult_t(RemoteStorageDeletePublishedFileResult_t.PackSmall d)
			{
				return new RemoteStorageDeletePublishedFileResult_t
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId
				};
			}

			// Token: 0x040009E2 RID: 2530
			internal Result Result;

			// Token: 0x040009E3 RID: 2531
			internal ulong PublishedFileId;
		}
	}
}
