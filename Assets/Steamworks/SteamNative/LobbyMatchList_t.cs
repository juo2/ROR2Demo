using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200009C RID: 156
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyMatchList_t
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x000118D0 File Offset: 0x0000FAD0
		internal static LobbyMatchList_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (LobbyMatchList_t.PackSmall)Marshal.PtrToStructure(p, typeof(LobbyMatchList_t.PackSmall));
			}
			return (LobbyMatchList_t)Marshal.PtrToStructure(p, typeof(LobbyMatchList_t));
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00011909 File Offset: 0x0000FB09
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(LobbyMatchList_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(LobbyMatchList_t));
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00011931 File Offset: 0x0000FB31
		internal static CallResult<LobbyMatchList_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<LobbyMatchList_t, bool> CallbackFunction)
		{
			return new CallResult<LobbyMatchList_t>(steamworks, call, CallbackFunction, new CallResult<LobbyMatchList_t>.ConvertFromPointer(LobbyMatchList_t.FromPointer), LobbyMatchList_t.StructSize(), 510);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00011954 File Offset: 0x0000FB54
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
						ResultA = new Callback.VTableWinThis.ResultD(LobbyMatchList_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(LobbyMatchList_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(LobbyMatchList_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(LobbyMatchList_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(LobbyMatchList_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(LobbyMatchList_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(LobbyMatchList_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(LobbyMatchList_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(LobbyMatchList_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(LobbyMatchList_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(LobbyMatchList_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(LobbyMatchList_t.OnGetSize)
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
				CallbackId = 510
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 510);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00011C5A File Offset: 0x0000FE5A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			LobbyMatchList_t.OnResult(param);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00011C62 File Offset: 0x0000FE62
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			LobbyMatchList_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00011C6C File Offset: 0x0000FE6C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return LobbyMatchList_t.OnGetSize();
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00011C73 File Offset: 0x0000FE73
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return LobbyMatchList_t.StructSize();
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00011C7A File Offset: 0x0000FE7A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			LobbyMatchList_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00011C8C File Offset: 0x0000FE8C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			LobbyMatchList_t data = LobbyMatchList_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<LobbyMatchList_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<LobbyMatchList_t>(data);
			}
		}

		// Token: 0x0400053C RID: 1340
		internal const int CallbackId = 510;

		// Token: 0x0400053D RID: 1341
		internal uint LobbiesMatching;

		// Token: 0x020001C0 RID: 448
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CFC RID: 7420 RVA: 0x00061AD0 File Offset: 0x0005FCD0
			public static implicit operator LobbyMatchList_t(LobbyMatchList_t.PackSmall d)
			{
				return new LobbyMatchList_t
				{
					LobbiesMatching = d.LobbiesMatching
				};
			}

			// Token: 0x040009C4 RID: 2500
			internal uint LobbiesMatching;
		}
	}
}
