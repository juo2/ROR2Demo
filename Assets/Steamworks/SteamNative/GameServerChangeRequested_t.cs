using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200007E RID: 126
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GameServerChangeRequested_t
	{
		// Token: 0x0600038C RID: 908 RVA: 0x0000AEE8 File Offset: 0x000090E8
		internal static GameServerChangeRequested_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GameServerChangeRequested_t.PackSmall)Marshal.PtrToStructure(p, typeof(GameServerChangeRequested_t.PackSmall));
			}
			return (GameServerChangeRequested_t)Marshal.PtrToStructure(p, typeof(GameServerChangeRequested_t));
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000AF21 File Offset: 0x00009121
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GameServerChangeRequested_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GameServerChangeRequested_t));
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000AF4C File Offset: 0x0000914C
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
						ResultA = new Callback.VTableWinThis.ResultD(GameServerChangeRequested_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GameServerChangeRequested_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GameServerChangeRequested_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GameServerChangeRequested_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GameServerChangeRequested_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GameServerChangeRequested_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GameServerChangeRequested_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GameServerChangeRequested_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GameServerChangeRequested_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GameServerChangeRequested_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GameServerChangeRequested_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GameServerChangeRequested_t.OnGetSize)
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
				CallbackId = 332
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 332);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000B252 File Offset: 0x00009452
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GameServerChangeRequested_t.OnResult(param);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000B25A File Offset: 0x0000945A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GameServerChangeRequested_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000B264 File Offset: 0x00009464
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GameServerChangeRequested_t.OnGetSize();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000B26B File Offset: 0x0000946B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GameServerChangeRequested_t.StructSize();
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000B272 File Offset: 0x00009472
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GameServerChangeRequested_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000B284 File Offset: 0x00009484
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GameServerChangeRequested_t data = GameServerChangeRequested_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GameServerChangeRequested_t>(data);
			}
			if (Facepunch.Steamworks.Server.Instance != null)
			{
				Facepunch.Steamworks.Server.Instance.OnCallback<GameServerChangeRequested_t>(data);
			}
		}

		// Token: 0x040004BC RID: 1212
		internal const int CallbackId = 332;

		// Token: 0x040004BD RID: 1213
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		internal string Server;

		// Token: 0x040004BE RID: 1214
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		internal string Password;

		// Token: 0x020001A2 RID: 418
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CDE RID: 7390 RVA: 0x000612EC File Offset: 0x0005F4EC
			public static implicit operator GameServerChangeRequested_t(GameServerChangeRequested_t.PackSmall d)
			{
				return new GameServerChangeRequested_t
				{
					Server = d.Server,
					Password = d.Password
				};
			}

			// Token: 0x0400095F RID: 2399
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			internal string Server;

			// Token: 0x04000960 RID: 2400
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
			internal string Password;
		}
	}
}
