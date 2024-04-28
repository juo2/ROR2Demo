using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000098 RID: 152
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDataUpdate_t
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x00010970 File Offset: 0x0000EB70
		internal static LobbyDataUpdate_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (LobbyDataUpdate_t.PackSmall)Marshal.PtrToStructure(p, typeof(LobbyDataUpdate_t.PackSmall));
			}
			return (LobbyDataUpdate_t)Marshal.PtrToStructure(p, typeof(LobbyDataUpdate_t));
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000109A9 File Offset: 0x0000EBA9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(LobbyDataUpdate_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(LobbyDataUpdate_t));
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000109D4 File Offset: 0x0000EBD4
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
						ResultA = new Callback.VTableWinThis.ResultD(LobbyDataUpdate_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(LobbyDataUpdate_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(LobbyDataUpdate_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(LobbyDataUpdate_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(LobbyDataUpdate_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(LobbyDataUpdate_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(LobbyDataUpdate_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(LobbyDataUpdate_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(LobbyDataUpdate_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(LobbyDataUpdate_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(LobbyDataUpdate_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(LobbyDataUpdate_t.OnGetSize)
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
				CallbackId = 505
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 505);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00010CDA File Offset: 0x0000EEDA
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			LobbyDataUpdate_t.OnResult(param);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00010CE2 File Offset: 0x0000EEE2
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			LobbyDataUpdate_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00010CEC File Offset: 0x0000EEEC
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return LobbyDataUpdate_t.OnGetSize();
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00010CF3 File Offset: 0x0000EEF3
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return LobbyDataUpdate_t.StructSize();
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00010CFA File Offset: 0x0000EEFA
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			LobbyDataUpdate_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00010D0C File Offset: 0x0000EF0C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			LobbyDataUpdate_t data = LobbyDataUpdate_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<LobbyDataUpdate_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<LobbyDataUpdate_t>(data);
			}
		}

		// Token: 0x04000529 RID: 1321
		internal const int CallbackId = 505;

		// Token: 0x0400052A RID: 1322
		internal ulong SteamIDLobby;

		// Token: 0x0400052B RID: 1323
		internal ulong SteamIDMember;

		// Token: 0x0400052C RID: 1324
		internal byte Success;

		// Token: 0x020001BC RID: 444
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CF8 RID: 7416 RVA: 0x000619AC File Offset: 0x0005FBAC
			public static implicit operator LobbyDataUpdate_t(LobbyDataUpdate_t.PackSmall d)
			{
				return new LobbyDataUpdate_t
				{
					SteamIDLobby = d.SteamIDLobby,
					SteamIDMember = d.SteamIDMember,
					Success = d.Success
				};
			}

			// Token: 0x040009B5 RID: 2485
			internal ulong SteamIDLobby;

			// Token: 0x040009B6 RID: 2486
			internal ulong SteamIDMember;

			// Token: 0x040009B7 RID: 2487
			internal byte Success;
		}
	}
}
