using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000084 RID: 132
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GameConnectedClanChatMsg_t
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x0000C618 File Offset: 0x0000A818
		internal static GameConnectedClanChatMsg_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GameConnectedClanChatMsg_t.PackSmall)Marshal.PtrToStructure(p, typeof(GameConnectedClanChatMsg_t.PackSmall));
			}
			return (GameConnectedClanChatMsg_t)Marshal.PtrToStructure(p, typeof(GameConnectedClanChatMsg_t));
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000C651 File Offset: 0x0000A851
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GameConnectedClanChatMsg_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GameConnectedClanChatMsg_t));
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000C67C File Offset: 0x0000A87C
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
						ResultA = new Callback.VTableWinThis.ResultD(GameConnectedClanChatMsg_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GameConnectedClanChatMsg_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GameConnectedClanChatMsg_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GameConnectedClanChatMsg_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GameConnectedClanChatMsg_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GameConnectedClanChatMsg_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GameConnectedClanChatMsg_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GameConnectedClanChatMsg_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GameConnectedClanChatMsg_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GameConnectedClanChatMsg_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GameConnectedClanChatMsg_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GameConnectedClanChatMsg_t.OnGetSize)
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
				CallbackId = 338
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 338);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000C982 File Offset: 0x0000AB82
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GameConnectedClanChatMsg_t.OnResult(param);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000C98A File Offset: 0x0000AB8A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GameConnectedClanChatMsg_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000C994 File Offset: 0x0000AB94
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GameConnectedClanChatMsg_t.OnGetSize();
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000C99B File Offset: 0x0000AB9B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GameConnectedClanChatMsg_t.StructSize();
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000C9A2 File Offset: 0x0000ABA2
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GameConnectedClanChatMsg_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000C9B4 File Offset: 0x0000ABB4
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GameConnectedClanChatMsg_t data = GameConnectedClanChatMsg_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GameConnectedClanChatMsg_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GameConnectedClanChatMsg_t>(data);
			}
		}

		// Token: 0x040004D1 RID: 1233
		internal const int CallbackId = 338;

		// Token: 0x040004D2 RID: 1234
		internal ulong SteamIDClanChat;

		// Token: 0x040004D3 RID: 1235
		internal ulong SteamIDUser;

		// Token: 0x040004D4 RID: 1236
		internal int MessageID;

		// Token: 0x020001A8 RID: 424
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CE4 RID: 7396 RVA: 0x00061438 File Offset: 0x0005F638
			public static implicit operator GameConnectedClanChatMsg_t(GameConnectedClanChatMsg_t.PackSmall d)
			{
				return new GameConnectedClanChatMsg_t
				{
					SteamIDClanChat = d.SteamIDClanChat,
					SteamIDUser = d.SteamIDUser,
					MessageID = d.MessageID
				};
			}

			// Token: 0x0400096E RID: 2414
			internal ulong SteamIDClanChat;

			// Token: 0x0400096F RID: 2415
			internal ulong SteamIDUser;

			// Token: 0x04000970 RID: 2416
			internal int MessageID;
		}
	}
}
