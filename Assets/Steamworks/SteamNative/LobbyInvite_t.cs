using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000096 RID: 150
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyInvite_t
	{
		// Token: 0x06000456 RID: 1110 RVA: 0x000101A0 File Offset: 0x0000E3A0
		internal static LobbyInvite_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (LobbyInvite_t.PackSmall)Marshal.PtrToStructure(p, typeof(LobbyInvite_t.PackSmall));
			}
			return (LobbyInvite_t)Marshal.PtrToStructure(p, typeof(LobbyInvite_t));
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000101D9 File Offset: 0x0000E3D9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(LobbyInvite_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(LobbyInvite_t));
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00010204 File Offset: 0x0000E404
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
						ResultA = new Callback.VTableWinThis.ResultD(LobbyInvite_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(LobbyInvite_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(LobbyInvite_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(LobbyInvite_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(LobbyInvite_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(LobbyInvite_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(LobbyInvite_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(LobbyInvite_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(LobbyInvite_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(LobbyInvite_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(LobbyInvite_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(LobbyInvite_t.OnGetSize)
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
				CallbackId = 503
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 503);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001050A File Offset: 0x0000E70A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			LobbyInvite_t.OnResult(param);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00010512 File Offset: 0x0000E712
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			LobbyInvite_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001051C File Offset: 0x0000E71C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return LobbyInvite_t.OnGetSize();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00010523 File Offset: 0x0000E723
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return LobbyInvite_t.StructSize();
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001052A File Offset: 0x0000E72A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			LobbyInvite_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001053C File Offset: 0x0000E73C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			LobbyInvite_t data = LobbyInvite_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<LobbyInvite_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<LobbyInvite_t>(data);
			}
		}

		// Token: 0x04000520 RID: 1312
		internal const int CallbackId = 503;

		// Token: 0x04000521 RID: 1313
		internal ulong SteamIDUser;

		// Token: 0x04000522 RID: 1314
		internal ulong SteamIDLobby;

		// Token: 0x04000523 RID: 1315
		internal ulong GameID;

		// Token: 0x020001BA RID: 442
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CF6 RID: 7414 RVA: 0x00061920 File Offset: 0x0005FB20
			public static implicit operator LobbyInvite_t(LobbyInvite_t.PackSmall d)
			{
				return new LobbyInvite_t
				{
					SteamIDUser = d.SteamIDUser,
					SteamIDLobby = d.SteamIDLobby,
					GameID = d.GameID
				};
			}

			// Token: 0x040009AE RID: 2478
			internal ulong SteamIDUser;

			// Token: 0x040009AF RID: 2479
			internal ulong SteamIDLobby;

			// Token: 0x040009B0 RID: 2480
			internal ulong GameID;
		}
	}
}
