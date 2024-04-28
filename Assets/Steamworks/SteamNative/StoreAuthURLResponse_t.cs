using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000079 RID: 121
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StoreAuthURLResponse_t
	{
		// Token: 0x0600036C RID: 876 RVA: 0x0000A280 File Offset: 0x00008480
		internal static StoreAuthURLResponse_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (StoreAuthURLResponse_t.PackSmall)Marshal.PtrToStructure(p, typeof(StoreAuthURLResponse_t.PackSmall));
			}
			return (StoreAuthURLResponse_t)Marshal.PtrToStructure(p, typeof(StoreAuthURLResponse_t));
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000A2B9 File Offset: 0x000084B9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(StoreAuthURLResponse_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(StoreAuthURLResponse_t));
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000A2E1 File Offset: 0x000084E1
		internal static CallResult<StoreAuthURLResponse_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<StoreAuthURLResponse_t, bool> CallbackFunction)
		{
			return new CallResult<StoreAuthURLResponse_t>(steamworks, call, CallbackFunction, new CallResult<StoreAuthURLResponse_t>.ConvertFromPointer(StoreAuthURLResponse_t.FromPointer), StoreAuthURLResponse_t.StructSize(), 165);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000A304 File Offset: 0x00008504
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
						ResultA = new Callback.VTableWinThis.ResultD(StoreAuthURLResponse_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(StoreAuthURLResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(StoreAuthURLResponse_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(StoreAuthURLResponse_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(StoreAuthURLResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(StoreAuthURLResponse_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(StoreAuthURLResponse_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(StoreAuthURLResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(StoreAuthURLResponse_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(StoreAuthURLResponse_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(StoreAuthURLResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(StoreAuthURLResponse_t.OnGetSize)
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
				CallbackId = 165
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 165);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000A60A File Offset: 0x0000880A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			StoreAuthURLResponse_t.OnResult(param);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000A612 File Offset: 0x00008812
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			StoreAuthURLResponse_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000A61C File Offset: 0x0000881C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return StoreAuthURLResponse_t.OnGetSize();
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000A623 File Offset: 0x00008823
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return StoreAuthURLResponse_t.StructSize();
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000A62A File Offset: 0x0000882A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			StoreAuthURLResponse_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000A63C File Offset: 0x0000883C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			StoreAuthURLResponse_t data = StoreAuthURLResponse_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<StoreAuthURLResponse_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<StoreAuthURLResponse_t>(data);
			}
		}

		// Token: 0x040004AE RID: 1198
		internal const int CallbackId = 165;

		// Token: 0x040004AF RID: 1199
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
		internal string URL;

		// Token: 0x0200019D RID: 413
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CD9 RID: 7385 RVA: 0x000611EC File Offset: 0x0005F3EC
			public static implicit operator StoreAuthURLResponse_t(StoreAuthURLResponse_t.PackSmall d)
			{
				return new StoreAuthURLResponse_t
				{
					URL = d.URL
				};
			}

			// Token: 0x04000954 RID: 2388
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
			internal string URL;
		}
	}
}
