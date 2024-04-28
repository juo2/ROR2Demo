using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200009B RID: 155
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyGameCreated_t
	{
		// Token: 0x06000484 RID: 1156 RVA: 0x000114F8 File Offset: 0x0000F6F8
		internal static LobbyGameCreated_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (LobbyGameCreated_t.PackSmall)Marshal.PtrToStructure(p, typeof(LobbyGameCreated_t.PackSmall));
			}
			return (LobbyGameCreated_t)Marshal.PtrToStructure(p, typeof(LobbyGameCreated_t));
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00011531 File Offset: 0x0000F731
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(LobbyGameCreated_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(LobbyGameCreated_t));
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001155C File Offset: 0x0000F75C
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
						ResultA = new Callback.VTableWinThis.ResultD(LobbyGameCreated_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(LobbyGameCreated_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(LobbyGameCreated_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(LobbyGameCreated_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(LobbyGameCreated_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(LobbyGameCreated_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(LobbyGameCreated_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(LobbyGameCreated_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(LobbyGameCreated_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(LobbyGameCreated_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(LobbyGameCreated_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(LobbyGameCreated_t.OnGetSize)
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
				CallbackId = 509
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 509);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00011862 File Offset: 0x0000FA62
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			LobbyGameCreated_t.OnResult(param);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001186A File Offset: 0x0000FA6A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			LobbyGameCreated_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00011874 File Offset: 0x0000FA74
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return LobbyGameCreated_t.OnGetSize();
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001187B File Offset: 0x0000FA7B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return LobbyGameCreated_t.StructSize();
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00011882 File Offset: 0x0000FA82
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			LobbyGameCreated_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00011894 File Offset: 0x0000FA94
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			LobbyGameCreated_t data = LobbyGameCreated_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<LobbyGameCreated_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<LobbyGameCreated_t>(data);
			}
		}

		// Token: 0x04000537 RID: 1335
		internal const int CallbackId = 509;

		// Token: 0x04000538 RID: 1336
		internal ulong SteamIDLobby;

		// Token: 0x04000539 RID: 1337
		internal ulong SteamIDGameServer;

		// Token: 0x0400053A RID: 1338
		internal uint IP;

		// Token: 0x0400053B RID: 1339
		internal ushort Port;

		// Token: 0x020001BF RID: 447
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CFB RID: 7419 RVA: 0x00061A84 File Offset: 0x0005FC84
			public static implicit operator LobbyGameCreated_t(LobbyGameCreated_t.PackSmall d)
			{
				return new LobbyGameCreated_t
				{
					SteamIDLobby = d.SteamIDLobby,
					SteamIDGameServer = d.SteamIDGameServer,
					IP = d.IP,
					Port = d.Port
				};
			}

			// Token: 0x040009C0 RID: 2496
			internal ulong SteamIDLobby;

			// Token: 0x040009C1 RID: 2497
			internal ulong SteamIDGameServer;

			// Token: 0x040009C2 RID: 2498
			internal uint IP;

			// Token: 0x040009C3 RID: 2499
			internal ushort Port;
		}
	}
}
