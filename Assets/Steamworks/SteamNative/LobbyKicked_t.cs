using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200009D RID: 157
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyKicked_t
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x00011CC8 File Offset: 0x0000FEC8
		internal static LobbyKicked_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (LobbyKicked_t.PackSmall)Marshal.PtrToStructure(p, typeof(LobbyKicked_t.PackSmall));
			}
			return (LobbyKicked_t)Marshal.PtrToStructure(p, typeof(LobbyKicked_t));
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00011D01 File Offset: 0x0000FF01
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(LobbyKicked_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(LobbyKicked_t));
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00011D2C File Offset: 0x0000FF2C
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
						ResultA = new Callback.VTableWinThis.ResultD(LobbyKicked_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(LobbyKicked_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(LobbyKicked_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(LobbyKicked_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(LobbyKicked_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(LobbyKicked_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(LobbyKicked_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(LobbyKicked_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(LobbyKicked_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(LobbyKicked_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(LobbyKicked_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(LobbyKicked_t.OnGetSize)
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
				CallbackId = 512
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 512);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00012032 File Offset: 0x00010232
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			LobbyKicked_t.OnResult(param);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001203A File Offset: 0x0001023A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			LobbyKicked_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00012044 File Offset: 0x00010244
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return LobbyKicked_t.OnGetSize();
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001204B File Offset: 0x0001024B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return LobbyKicked_t.StructSize();
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00012052 File Offset: 0x00010252
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			LobbyKicked_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00012064 File Offset: 0x00010264
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			LobbyKicked_t data = LobbyKicked_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<LobbyKicked_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<LobbyKicked_t>(data);
			}
		}

		// Token: 0x0400053E RID: 1342
		internal const int CallbackId = 512;

		// Token: 0x0400053F RID: 1343
		internal ulong SteamIDLobby;

		// Token: 0x04000540 RID: 1344
		internal ulong SteamIDAdmin;

		// Token: 0x04000541 RID: 1345
		internal byte KickedDueToDisconnect;

		// Token: 0x020001C1 RID: 449
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CFD RID: 7421 RVA: 0x00061AF4 File Offset: 0x0005FCF4
			public static implicit operator LobbyKicked_t(LobbyKicked_t.PackSmall d)
			{
				return new LobbyKicked_t
				{
					SteamIDLobby = d.SteamIDLobby,
					SteamIDAdmin = d.SteamIDAdmin,
					KickedDueToDisconnect = d.KickedDueToDisconnect
				};
			}

			// Token: 0x040009C5 RID: 2501
			internal ulong SteamIDLobby;

			// Token: 0x040009C6 RID: 2502
			internal ulong SteamIDAdmin;

			// Token: 0x040009C7 RID: 2503
			internal byte KickedDueToDisconnect;
		}
	}
}
