using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000BC RID: 188
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageFileWriteAsyncComplete_t
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x00019278 File Offset: 0x00017478
		internal static RemoteStorageFileWriteAsyncComplete_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageFileWriteAsyncComplete_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageFileWriteAsyncComplete_t.PackSmall));
			}
			return (RemoteStorageFileWriteAsyncComplete_t)Marshal.PtrToStructure(p, typeof(RemoteStorageFileWriteAsyncComplete_t));
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000192B1 File Offset: 0x000174B1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageFileWriteAsyncComplete_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageFileWriteAsyncComplete_t));
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000192D9 File Offset: 0x000174D9
		internal static CallResult<RemoteStorageFileWriteAsyncComplete_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageFileWriteAsyncComplete_t, bool> CallbackFunction)
		{
			return new CallResult<RemoteStorageFileWriteAsyncComplete_t>(steamworks, call, CallbackFunction, new CallResult<RemoteStorageFileWriteAsyncComplete_t>.ConvertFromPointer(RemoteStorageFileWriteAsyncComplete_t.FromPointer), RemoteStorageFileWriteAsyncComplete_t.StructSize(), 1331);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000192FC File Offset: 0x000174FC
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageFileWriteAsyncComplete_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageFileWriteAsyncComplete_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageFileWriteAsyncComplete_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageFileWriteAsyncComplete_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageFileWriteAsyncComplete_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageFileWriteAsyncComplete_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageFileWriteAsyncComplete_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageFileWriteAsyncComplete_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageFileWriteAsyncComplete_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageFileWriteAsyncComplete_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageFileWriteAsyncComplete_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageFileWriteAsyncComplete_t.OnGetSize)
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
				CallbackId = 1331
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1331);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00019602 File Offset: 0x00017802
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageFileWriteAsyncComplete_t.OnResult(param);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001960A File Offset: 0x0001780A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageFileWriteAsyncComplete_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00019614 File Offset: 0x00017814
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageFileWriteAsyncComplete_t.OnGetSize();
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001961B File Offset: 0x0001781B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageFileWriteAsyncComplete_t.StructSize();
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00019622 File Offset: 0x00017822
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageFileWriteAsyncComplete_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00019634 File Offset: 0x00017834
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageFileWriteAsyncComplete_t data = RemoteStorageFileWriteAsyncComplete_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageFileWriteAsyncComplete_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageFileWriteAsyncComplete_t>(data);
			}
		}

		// Token: 0x040005D0 RID: 1488
		internal const int CallbackId = 1331;

		// Token: 0x040005D1 RID: 1489
		internal Result Result;

		// Token: 0x020001E0 RID: 480
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D1C RID: 7452 RVA: 0x000623A8 File Offset: 0x000605A8
			public static implicit operator RemoteStorageFileWriteAsyncComplete_t(RemoteStorageFileWriteAsyncComplete_t.PackSmall d)
			{
				return new RemoteStorageFileWriteAsyncComplete_t
				{
					Result = d.Result
				};
			}

			// Token: 0x04000A39 RID: 2617
			internal Result Result;
		}
	}
}
