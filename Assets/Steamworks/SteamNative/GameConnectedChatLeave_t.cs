using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000086 RID: 134
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GameConnectedChatLeave_t
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x0000CDC8 File Offset: 0x0000AFC8
		internal static GameConnectedChatLeave_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GameConnectedChatLeave_t.PackSmall)Marshal.PtrToStructure(p, typeof(GameConnectedChatLeave_t.PackSmall));
			}
			return (GameConnectedChatLeave_t)Marshal.PtrToStructure(p, typeof(GameConnectedChatLeave_t));
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000CE01 File Offset: 0x0000B001
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GameConnectedChatLeave_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GameConnectedChatLeave_t));
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000CE2C File Offset: 0x0000B02C
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
						ResultA = new Callback.VTableWinThis.ResultD(GameConnectedChatLeave_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GameConnectedChatLeave_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GameConnectedChatLeave_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GameConnectedChatLeave_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GameConnectedChatLeave_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GameConnectedChatLeave_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GameConnectedChatLeave_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GameConnectedChatLeave_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GameConnectedChatLeave_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GameConnectedChatLeave_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GameConnectedChatLeave_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GameConnectedChatLeave_t.OnGetSize)
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
				CallbackId = 340
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 340);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000D132 File Offset: 0x0000B332
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GameConnectedChatLeave_t.OnResult(param);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000D13A File Offset: 0x0000B33A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GameConnectedChatLeave_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000D144 File Offset: 0x0000B344
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GameConnectedChatLeave_t.OnGetSize();
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000D14B File Offset: 0x0000B34B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GameConnectedChatLeave_t.StructSize();
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000D152 File Offset: 0x0000B352
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GameConnectedChatLeave_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000D164 File Offset: 0x0000B364
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GameConnectedChatLeave_t data = GameConnectedChatLeave_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GameConnectedChatLeave_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GameConnectedChatLeave_t>(data);
			}
		}

		// Token: 0x040004D8 RID: 1240
		internal const int CallbackId = 340;

		// Token: 0x040004D9 RID: 1241
		internal ulong SteamIDClanChat;

		// Token: 0x040004DA RID: 1242
		internal ulong SteamIDUser;

		// Token: 0x040004DB RID: 1243
		[MarshalAs(UnmanagedType.I1)]
		internal bool Kicked;

		// Token: 0x040004DC RID: 1244
		[MarshalAs(UnmanagedType.I1)]
		internal bool Dropped;

		// Token: 0x020001AA RID: 426
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CE6 RID: 7398 RVA: 0x000614A8 File Offset: 0x0005F6A8
			public static implicit operator GameConnectedChatLeave_t(GameConnectedChatLeave_t.PackSmall d)
			{
				return new GameConnectedChatLeave_t
				{
					SteamIDClanChat = d.SteamIDClanChat,
					SteamIDUser = d.SteamIDUser,
					Kicked = d.Kicked,
					Dropped = d.Dropped
				};
			}

			// Token: 0x04000973 RID: 2419
			internal ulong SteamIDClanChat;

			// Token: 0x04000974 RID: 2420
			internal ulong SteamIDUser;

			// Token: 0x04000975 RID: 2421
			[MarshalAs(UnmanagedType.I1)]
			internal bool Kicked;

			// Token: 0x04000976 RID: 2422
			[MarshalAs(UnmanagedType.I1)]
			internal bool Dropped;
		}
	}
}
