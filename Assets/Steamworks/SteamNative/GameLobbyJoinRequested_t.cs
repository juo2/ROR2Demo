using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200007F RID: 127
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GameLobbyJoinRequested_t
	{
		// Token: 0x06000395 RID: 917 RVA: 0x0000B2C0 File Offset: 0x000094C0
		internal static GameLobbyJoinRequested_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GameLobbyJoinRequested_t.PackSmall)Marshal.PtrToStructure(p, typeof(GameLobbyJoinRequested_t.PackSmall));
			}
			return (GameLobbyJoinRequested_t)Marshal.PtrToStructure(p, typeof(GameLobbyJoinRequested_t));
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000B2F9 File Offset: 0x000094F9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GameLobbyJoinRequested_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GameLobbyJoinRequested_t));
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000B324 File Offset: 0x00009524
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
						ResultA = new Callback.VTableWinThis.ResultD(GameLobbyJoinRequested_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GameLobbyJoinRequested_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GameLobbyJoinRequested_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GameLobbyJoinRequested_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GameLobbyJoinRequested_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GameLobbyJoinRequested_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GameLobbyJoinRequested_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GameLobbyJoinRequested_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GameLobbyJoinRequested_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GameLobbyJoinRequested_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GameLobbyJoinRequested_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GameLobbyJoinRequested_t.OnGetSize)
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
				CallbackId = 333
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 333);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000B62A File Offset: 0x0000982A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GameLobbyJoinRequested_t.OnResult(param);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000B632 File Offset: 0x00009832
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GameLobbyJoinRequested_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000B63C File Offset: 0x0000983C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GameLobbyJoinRequested_t.OnGetSize();
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000B643 File Offset: 0x00009843
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GameLobbyJoinRequested_t.StructSize();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000B64A File Offset: 0x0000984A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GameLobbyJoinRequested_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000B65C File Offset: 0x0000985C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GameLobbyJoinRequested_t data = GameLobbyJoinRequested_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GameLobbyJoinRequested_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GameLobbyJoinRequested_t>(data);
			}
		}

		// Token: 0x040004BF RID: 1215
		internal const int CallbackId = 333;

		// Token: 0x040004C0 RID: 1216
		internal ulong SteamIDLobby;

		// Token: 0x040004C1 RID: 1217
		internal ulong SteamIDFriend;

		// Token: 0x020001A3 RID: 419
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CDF RID: 7391 RVA: 0x0006131C File Offset: 0x0005F51C
			public static implicit operator GameLobbyJoinRequested_t(GameLobbyJoinRequested_t.PackSmall d)
			{
				return new GameLobbyJoinRequested_t
				{
					SteamIDLobby = d.SteamIDLobby,
					SteamIDFriend = d.SteamIDFriend
				};
			}

			// Token: 0x04000961 RID: 2401
			internal ulong SteamIDLobby;

			// Token: 0x04000962 RID: 2402
			internal ulong SteamIDFriend;
		}
	}
}
