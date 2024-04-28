using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000083 RID: 131
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GameRichPresenceJoinRequested_t
	{
		// Token: 0x060003BA RID: 954 RVA: 0x0000C240 File Offset: 0x0000A440
		internal static GameRichPresenceJoinRequested_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GameRichPresenceJoinRequested_t.PackSmall)Marshal.PtrToStructure(p, typeof(GameRichPresenceJoinRequested_t.PackSmall));
			}
			return (GameRichPresenceJoinRequested_t)Marshal.PtrToStructure(p, typeof(GameRichPresenceJoinRequested_t));
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000C279 File Offset: 0x0000A479
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GameRichPresenceJoinRequested_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GameRichPresenceJoinRequested_t));
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000C2A4 File Offset: 0x0000A4A4
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
						ResultA = new Callback.VTableWinThis.ResultD(GameRichPresenceJoinRequested_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GameRichPresenceJoinRequested_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GameRichPresenceJoinRequested_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GameRichPresenceJoinRequested_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GameRichPresenceJoinRequested_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GameRichPresenceJoinRequested_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GameRichPresenceJoinRequested_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GameRichPresenceJoinRequested_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GameRichPresenceJoinRequested_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GameRichPresenceJoinRequested_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GameRichPresenceJoinRequested_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GameRichPresenceJoinRequested_t.OnGetSize)
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
				CallbackId = 337
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 337);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000C5AA File Offset: 0x0000A7AA
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GameRichPresenceJoinRequested_t.OnResult(param);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000C5B2 File Offset: 0x0000A7B2
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GameRichPresenceJoinRequested_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000C5BC File Offset: 0x0000A7BC
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GameRichPresenceJoinRequested_t.OnGetSize();
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000C5C3 File Offset: 0x0000A7C3
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GameRichPresenceJoinRequested_t.StructSize();
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000C5CA File Offset: 0x0000A7CA
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GameRichPresenceJoinRequested_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000C5DC File Offset: 0x0000A7DC
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GameRichPresenceJoinRequested_t data = GameRichPresenceJoinRequested_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GameRichPresenceJoinRequested_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GameRichPresenceJoinRequested_t>(data);
			}
		}

		// Token: 0x040004CE RID: 1230
		internal const int CallbackId = 337;

		// Token: 0x040004CF RID: 1231
		internal ulong SteamIDFriend;

		// Token: 0x040004D0 RID: 1232
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string Connect;

		// Token: 0x020001A7 RID: 423
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CE3 RID: 7395 RVA: 0x00061408 File Offset: 0x0005F608
			public static implicit operator GameRichPresenceJoinRequested_t(GameRichPresenceJoinRequested_t.PackSmall d)
			{
				return new GameRichPresenceJoinRequested_t
				{
					SteamIDFriend = d.SteamIDFriend,
					Connect = d.Connect
				};
			}

			// Token: 0x0400096C RID: 2412
			internal ulong SteamIDFriend;

			// Token: 0x0400096D RID: 2413
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string Connect;
		}
	}
}
