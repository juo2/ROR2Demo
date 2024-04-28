using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000073 RID: 115
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ClientGameServerDeny_t
	{
		// Token: 0x06000335 RID: 821 RVA: 0x00008B58 File Offset: 0x00006D58
		internal static ClientGameServerDeny_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (ClientGameServerDeny_t.PackSmall)Marshal.PtrToStructure(p, typeof(ClientGameServerDeny_t.PackSmall));
			}
			return (ClientGameServerDeny_t)Marshal.PtrToStructure(p, typeof(ClientGameServerDeny_t));
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00008B91 File Offset: 0x00006D91
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(ClientGameServerDeny_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(ClientGameServerDeny_t));
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00008BBC File Offset: 0x00006DBC
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
						ResultA = new Callback.VTableWinThis.ResultD(ClientGameServerDeny_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(ClientGameServerDeny_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(ClientGameServerDeny_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(ClientGameServerDeny_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(ClientGameServerDeny_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(ClientGameServerDeny_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(ClientGameServerDeny_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(ClientGameServerDeny_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(ClientGameServerDeny_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(ClientGameServerDeny_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(ClientGameServerDeny_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(ClientGameServerDeny_t.OnGetSize)
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
				CallbackId = 113
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 113);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00008EBC File Offset: 0x000070BC
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			ClientGameServerDeny_t.OnResult(param);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00008EC4 File Offset: 0x000070C4
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			ClientGameServerDeny_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00008ECE File Offset: 0x000070CE
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return ClientGameServerDeny_t.OnGetSize();
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00008ED5 File Offset: 0x000070D5
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return ClientGameServerDeny_t.StructSize();
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00008EDC File Offset: 0x000070DC
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			ClientGameServerDeny_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00008EEC File Offset: 0x000070EC
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			ClientGameServerDeny_t data = ClientGameServerDeny_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<ClientGameServerDeny_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<ClientGameServerDeny_t>(data);
			}
		}

		// Token: 0x04000499 RID: 1177
		internal const int CallbackId = 113;

		// Token: 0x0400049A RID: 1178
		internal uint AppID;

		// Token: 0x0400049B RID: 1179
		internal uint GameServerIP;

		// Token: 0x0400049C RID: 1180
		internal ushort GameServerPort;

		// Token: 0x0400049D RID: 1181
		internal ushort Secure;

		// Token: 0x0400049E RID: 1182
		internal uint Reason;

		// Token: 0x02000197 RID: 407
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CD3 RID: 7379 RVA: 0x0006109C File Offset: 0x0005F29C
			public static implicit operator ClientGameServerDeny_t(ClientGameServerDeny_t.PackSmall d)
			{
				return new ClientGameServerDeny_t
				{
					AppID = d.AppID,
					GameServerIP = d.GameServerIP,
					GameServerPort = d.GameServerPort,
					Secure = d.Secure,
					Reason = d.Reason
				};
			}

			// Token: 0x04000945 RID: 2373
			internal uint AppID;

			// Token: 0x04000946 RID: 2374
			internal uint GameServerIP;

			// Token: 0x04000947 RID: 2375
			internal ushort GameServerPort;

			// Token: 0x04000948 RID: 2376
			internal ushort Secure;

			// Token: 0x04000949 RID: 2377
			internal uint Reason;
		}
	}
}
