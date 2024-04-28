using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000BA RID: 186
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStoragePublishFileProgress_t
	{
		// Token: 0x060005A4 RID: 1444 RVA: 0x00018AA8 File Offset: 0x00016CA8
		internal static RemoteStoragePublishFileProgress_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStoragePublishFileProgress_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStoragePublishFileProgress_t.PackSmall));
			}
			return (RemoteStoragePublishFileProgress_t)Marshal.PtrToStructure(p, typeof(RemoteStoragePublishFileProgress_t));
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00018AE1 File Offset: 0x00016CE1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStoragePublishFileProgress_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStoragePublishFileProgress_t));
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00018B09 File Offset: 0x00016D09
		internal static CallResult<RemoteStoragePublishFileProgress_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStoragePublishFileProgress_t, bool> CallbackFunction)
		{
			return new CallResult<RemoteStoragePublishFileProgress_t>(steamworks, call, CallbackFunction, new CallResult<RemoteStoragePublishFileProgress_t>.ConvertFromPointer(RemoteStoragePublishFileProgress_t.FromPointer), RemoteStoragePublishFileProgress_t.StructSize(), 1329);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00018B2C File Offset: 0x00016D2C
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStoragePublishFileProgress_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStoragePublishFileProgress_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStoragePublishFileProgress_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStoragePublishFileProgress_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStoragePublishFileProgress_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStoragePublishFileProgress_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStoragePublishFileProgress_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStoragePublishFileProgress_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStoragePublishFileProgress_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStoragePublishFileProgress_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStoragePublishFileProgress_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStoragePublishFileProgress_t.OnGetSize)
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
				CallbackId = 1329
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1329);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00018E32 File Offset: 0x00017032
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStoragePublishFileProgress_t.OnResult(param);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00018E3A File Offset: 0x0001703A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStoragePublishFileProgress_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00018E44 File Offset: 0x00017044
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStoragePublishFileProgress_t.OnGetSize();
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00018E4B File Offset: 0x0001704B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStoragePublishFileProgress_t.StructSize();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00018E52 File Offset: 0x00017052
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStoragePublishFileProgress_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00018E64 File Offset: 0x00017064
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStoragePublishFileProgress_t data = RemoteStoragePublishFileProgress_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStoragePublishFileProgress_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStoragePublishFileProgress_t>(data);
			}
		}

		// Token: 0x040005C9 RID: 1481
		internal const int CallbackId = 1329;

		// Token: 0x040005CA RID: 1482
		internal double DPercentFile;

		// Token: 0x040005CB RID: 1483
		[MarshalAs(UnmanagedType.I1)]
		internal bool Preview;

		// Token: 0x020001DE RID: 478
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D1A RID: 7450 RVA: 0x00062338 File Offset: 0x00060538
			public static implicit operator RemoteStoragePublishFileProgress_t(RemoteStoragePublishFileProgress_t.PackSmall d)
			{
				return new RemoteStoragePublishFileProgress_t
				{
					DPercentFile = d.DPercentFile,
					Preview = d.Preview
				};
			}

			// Token: 0x04000A34 RID: 2612
			internal double DPercentFile;

			// Token: 0x04000A35 RID: 2613
			[MarshalAs(UnmanagedType.I1)]
			internal bool Preview;
		}
	}
}
