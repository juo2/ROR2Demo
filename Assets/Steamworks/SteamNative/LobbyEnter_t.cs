using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000097 RID: 151
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyEnter_t
	{
		// Token: 0x0600045F RID: 1119 RVA: 0x00010578 File Offset: 0x0000E778
		internal static LobbyEnter_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (LobbyEnter_t.PackSmall)Marshal.PtrToStructure(p, typeof(LobbyEnter_t.PackSmall));
			}
			return (LobbyEnter_t)Marshal.PtrToStructure(p, typeof(LobbyEnter_t));
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000105B1 File Offset: 0x0000E7B1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(LobbyEnter_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(LobbyEnter_t));
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000105D9 File Offset: 0x0000E7D9
		internal static CallResult<LobbyEnter_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<LobbyEnter_t, bool> CallbackFunction)
		{
			return new CallResult<LobbyEnter_t>(steamworks, call, CallbackFunction, new CallResult<LobbyEnter_t>.ConvertFromPointer(LobbyEnter_t.FromPointer), LobbyEnter_t.StructSize(), 504);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000105FC File Offset: 0x0000E7FC
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
						ResultA = new Callback.VTableWinThis.ResultD(LobbyEnter_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(LobbyEnter_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(LobbyEnter_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(LobbyEnter_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(LobbyEnter_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(LobbyEnter_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(LobbyEnter_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(LobbyEnter_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(LobbyEnter_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(LobbyEnter_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(LobbyEnter_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(LobbyEnter_t.OnGetSize)
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
				CallbackId = 504
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 504);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00010902 File Offset: 0x0000EB02
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			LobbyEnter_t.OnResult(param);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001090A File Offset: 0x0000EB0A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			LobbyEnter_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00010914 File Offset: 0x0000EB14
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return LobbyEnter_t.OnGetSize();
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001091B File Offset: 0x0000EB1B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return LobbyEnter_t.StructSize();
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00010922 File Offset: 0x0000EB22
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			LobbyEnter_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00010934 File Offset: 0x0000EB34
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			LobbyEnter_t data = LobbyEnter_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<LobbyEnter_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<LobbyEnter_t>(data);
			}
		}

		// Token: 0x04000524 RID: 1316
		internal const int CallbackId = 504;

		// Token: 0x04000525 RID: 1317
		internal ulong SteamIDLobby;

		// Token: 0x04000526 RID: 1318
		internal uint GfChatPermissions;

		// Token: 0x04000527 RID: 1319
		[MarshalAs(UnmanagedType.I1)]
		internal bool Locked;

		// Token: 0x04000528 RID: 1320
		internal uint EChatRoomEnterResponse;

		// Token: 0x020001BB RID: 443
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CF7 RID: 7415 RVA: 0x00061960 File Offset: 0x0005FB60
			public static implicit operator LobbyEnter_t(LobbyEnter_t.PackSmall d)
			{
				return new LobbyEnter_t
				{
					SteamIDLobby = d.SteamIDLobby,
					GfChatPermissions = d.GfChatPermissions,
					Locked = d.Locked,
					EChatRoomEnterResponse = d.EChatRoomEnterResponse
				};
			}

			// Token: 0x040009B1 RID: 2481
			internal ulong SteamIDLobby;

			// Token: 0x040009B2 RID: 2482
			internal uint GfChatPermissions;

			// Token: 0x040009B3 RID: 2483
			[MarshalAs(UnmanagedType.I1)]
			internal bool Locked;

			// Token: 0x040009B4 RID: 2484
			internal uint EChatRoomEnterResponse;
		}
	}
}
