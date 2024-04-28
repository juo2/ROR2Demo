using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000085 RID: 133
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GameConnectedChatJoin_t
	{
		// Token: 0x060003CC RID: 972 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		internal static GameConnectedChatJoin_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GameConnectedChatJoin_t.PackSmall)Marshal.PtrToStructure(p, typeof(GameConnectedChatJoin_t.PackSmall));
			}
			return (GameConnectedChatJoin_t)Marshal.PtrToStructure(p, typeof(GameConnectedChatJoin_t));
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000CA29 File Offset: 0x0000AC29
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GameConnectedChatJoin_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GameConnectedChatJoin_t));
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000CA54 File Offset: 0x0000AC54
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
						ResultA = new Callback.VTableWinThis.ResultD(GameConnectedChatJoin_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GameConnectedChatJoin_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GameConnectedChatJoin_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GameConnectedChatJoin_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GameConnectedChatJoin_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GameConnectedChatJoin_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GameConnectedChatJoin_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GameConnectedChatJoin_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GameConnectedChatJoin_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GameConnectedChatJoin_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GameConnectedChatJoin_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GameConnectedChatJoin_t.OnGetSize)
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
				CallbackId = 339
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 339);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000CD5A File Offset: 0x0000AF5A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GameConnectedChatJoin_t.OnResult(param);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000CD62 File Offset: 0x0000AF62
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GameConnectedChatJoin_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GameConnectedChatJoin_t.OnGetSize();
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000CD73 File Offset: 0x0000AF73
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GameConnectedChatJoin_t.StructSize();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000CD7A File Offset: 0x0000AF7A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GameConnectedChatJoin_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000CD8C File Offset: 0x0000AF8C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GameConnectedChatJoin_t data = GameConnectedChatJoin_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GameConnectedChatJoin_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GameConnectedChatJoin_t>(data);
			}
		}

		// Token: 0x040004D5 RID: 1237
		internal const int CallbackId = 339;

		// Token: 0x040004D6 RID: 1238
		internal ulong SteamIDClanChat;

		// Token: 0x040004D7 RID: 1239
		internal ulong SteamIDUser;

		// Token: 0x020001A9 RID: 425
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CE5 RID: 7397 RVA: 0x00061478 File Offset: 0x0005F678
			public static implicit operator GameConnectedChatJoin_t(GameConnectedChatJoin_t.PackSmall d)
			{
				return new GameConnectedChatJoin_t
				{
					SteamIDClanChat = d.SteamIDClanChat,
					SteamIDUser = d.SteamIDUser
				};
			}

			// Token: 0x04000971 RID: 2417
			internal ulong SteamIDClanChat;

			// Token: 0x04000972 RID: 2418
			internal ulong SteamIDUser;
		}
	}
}
