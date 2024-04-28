using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000BD RID: 189
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageFileReadAsyncComplete_t
	{
		// Token: 0x060005C1 RID: 1473 RVA: 0x00019670 File Offset: 0x00017870
		internal static RemoteStorageFileReadAsyncComplete_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageFileReadAsyncComplete_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageFileReadAsyncComplete_t.PackSmall));
			}
			return (RemoteStorageFileReadAsyncComplete_t)Marshal.PtrToStructure(p, typeof(RemoteStorageFileReadAsyncComplete_t));
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000196A9 File Offset: 0x000178A9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageFileReadAsyncComplete_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageFileReadAsyncComplete_t));
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000196D1 File Offset: 0x000178D1
		internal static CallResult<RemoteStorageFileReadAsyncComplete_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageFileReadAsyncComplete_t, bool> CallbackFunction)
		{
			return new CallResult<RemoteStorageFileReadAsyncComplete_t>(steamworks, call, CallbackFunction, new CallResult<RemoteStorageFileReadAsyncComplete_t>.ConvertFromPointer(RemoteStorageFileReadAsyncComplete_t.FromPointer), RemoteStorageFileReadAsyncComplete_t.StructSize(), 1332);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000196F4 File Offset: 0x000178F4
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageFileReadAsyncComplete_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageFileReadAsyncComplete_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageFileReadAsyncComplete_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageFileReadAsyncComplete_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageFileReadAsyncComplete_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageFileReadAsyncComplete_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageFileReadAsyncComplete_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageFileReadAsyncComplete_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageFileReadAsyncComplete_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageFileReadAsyncComplete_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageFileReadAsyncComplete_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageFileReadAsyncComplete_t.OnGetSize)
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
				CallbackId = 1332
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1332);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000199FA File Offset: 0x00017BFA
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageFileReadAsyncComplete_t.OnResult(param);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00019A02 File Offset: 0x00017C02
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageFileReadAsyncComplete_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00019A0C File Offset: 0x00017C0C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageFileReadAsyncComplete_t.OnGetSize();
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00019A13 File Offset: 0x00017C13
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageFileReadAsyncComplete_t.StructSize();
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00019A1A File Offset: 0x00017C1A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageFileReadAsyncComplete_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00019A2C File Offset: 0x00017C2C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageFileReadAsyncComplete_t data = RemoteStorageFileReadAsyncComplete_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageFileReadAsyncComplete_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageFileReadAsyncComplete_t>(data);
			}
		}

		// Token: 0x040005D2 RID: 1490
		internal const int CallbackId = 1332;

		// Token: 0x040005D3 RID: 1491
		internal ulong FileReadAsync;

		// Token: 0x040005D4 RID: 1492
		internal Result Result;

		// Token: 0x040005D5 RID: 1493
		internal uint Offset;

		// Token: 0x040005D6 RID: 1494
		internal uint Read;

		// Token: 0x020001E1 RID: 481
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D1D RID: 7453 RVA: 0x000623CC File Offset: 0x000605CC
			public static implicit operator RemoteStorageFileReadAsyncComplete_t(RemoteStorageFileReadAsyncComplete_t.PackSmall d)
			{
				return new RemoteStorageFileReadAsyncComplete_t
				{
					FileReadAsync = d.FileReadAsync,
					Result = d.Result,
					Offset = d.Offset,
					Read = d.Read
				};
			}

			// Token: 0x04000A3A RID: 2618
			internal ulong FileReadAsync;

			// Token: 0x04000A3B RID: 2619
			internal Result Result;

			// Token: 0x04000A3C RID: 2620
			internal uint Offset;

			// Token: 0x04000A3D RID: 2621
			internal uint Read;
		}
	}
}
