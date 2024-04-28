using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000089 RID: 137
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GameConnectedFriendChatMsg_t
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x0000D970 File Offset: 0x0000BB70
		internal static GameConnectedFriendChatMsg_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GameConnectedFriendChatMsg_t.PackSmall)Marshal.PtrToStructure(p, typeof(GameConnectedFriendChatMsg_t.PackSmall));
			}
			return (GameConnectedFriendChatMsg_t)Marshal.PtrToStructure(p, typeof(GameConnectedFriendChatMsg_t));
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000D9A9 File Offset: 0x0000BBA9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GameConnectedFriendChatMsg_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GameConnectedFriendChatMsg_t));
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
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
						ResultA = new Callback.VTableWinThis.ResultD(GameConnectedFriendChatMsg_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GameConnectedFriendChatMsg_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GameConnectedFriendChatMsg_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GameConnectedFriendChatMsg_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GameConnectedFriendChatMsg_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GameConnectedFriendChatMsg_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GameConnectedFriendChatMsg_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GameConnectedFriendChatMsg_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GameConnectedFriendChatMsg_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GameConnectedFriendChatMsg_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GameConnectedFriendChatMsg_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GameConnectedFriendChatMsg_t.OnGetSize)
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
				CallbackId = 343
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 343);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000DCDA File Offset: 0x0000BEDA
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GameConnectedFriendChatMsg_t.OnResult(param);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000DCE2 File Offset: 0x0000BEE2
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GameConnectedFriendChatMsg_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000DCEC File Offset: 0x0000BEEC
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GameConnectedFriendChatMsg_t.OnGetSize();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000DCF3 File Offset: 0x0000BEF3
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GameConnectedFriendChatMsg_t.StructSize();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000DCFA File Offset: 0x0000BEFA
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GameConnectedFriendChatMsg_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000DD0C File Offset: 0x0000BF0C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GameConnectedFriendChatMsg_t data = GameConnectedFriendChatMsg_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GameConnectedFriendChatMsg_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GameConnectedFriendChatMsg_t>(data);
			}
		}

		// Token: 0x040004E2 RID: 1250
		internal const int CallbackId = 343;

		// Token: 0x040004E3 RID: 1251
		internal ulong SteamIDUser;

		// Token: 0x040004E4 RID: 1252
		internal int MessageID;

		// Token: 0x020001AD RID: 429
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CE9 RID: 7401 RVA: 0x00061548 File Offset: 0x0005F748
			public static implicit operator GameConnectedFriendChatMsg_t(GameConnectedFriendChatMsg_t.PackSmall d)
			{
				return new GameConnectedFriendChatMsg_t
				{
					SteamIDUser = d.SteamIDUser,
					MessageID = d.MessageID
				};
			}

			// Token: 0x0400097A RID: 2426
			internal ulong SteamIDUser;

			// Token: 0x0400097B RID: 2427
			internal int MessageID;
		}
	}
}
