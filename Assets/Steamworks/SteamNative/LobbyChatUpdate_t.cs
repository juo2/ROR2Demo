using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000099 RID: 153
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyChatUpdate_t
	{
		// Token: 0x06000472 RID: 1138 RVA: 0x00010D48 File Offset: 0x0000EF48
		internal static LobbyChatUpdate_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (LobbyChatUpdate_t.PackSmall)Marshal.PtrToStructure(p, typeof(LobbyChatUpdate_t.PackSmall));
			}
			return (LobbyChatUpdate_t)Marshal.PtrToStructure(p, typeof(LobbyChatUpdate_t));
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00010D81 File Offset: 0x0000EF81
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(LobbyChatUpdate_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(LobbyChatUpdate_t));
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00010DAC File Offset: 0x0000EFAC
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
						ResultA = new Callback.VTableWinThis.ResultD(LobbyChatUpdate_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(LobbyChatUpdate_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(LobbyChatUpdate_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(LobbyChatUpdate_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(LobbyChatUpdate_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(LobbyChatUpdate_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(LobbyChatUpdate_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(LobbyChatUpdate_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(LobbyChatUpdate_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(LobbyChatUpdate_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(LobbyChatUpdate_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(LobbyChatUpdate_t.OnGetSize)
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
				CallbackId = 506
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 506);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x000110B2 File Offset: 0x0000F2B2
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			LobbyChatUpdate_t.OnResult(param);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000110BA File Offset: 0x0000F2BA
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			LobbyChatUpdate_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000110C4 File Offset: 0x0000F2C4
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return LobbyChatUpdate_t.OnGetSize();
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000110CB File Offset: 0x0000F2CB
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return LobbyChatUpdate_t.StructSize();
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000110D2 File Offset: 0x0000F2D2
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			LobbyChatUpdate_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000110E4 File Offset: 0x0000F2E4
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			LobbyChatUpdate_t data = LobbyChatUpdate_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<LobbyChatUpdate_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<LobbyChatUpdate_t>(data);
			}
		}

		// Token: 0x0400052D RID: 1325
		internal const int CallbackId = 506;

		// Token: 0x0400052E RID: 1326
		internal ulong SteamIDLobby;

		// Token: 0x0400052F RID: 1327
		internal ulong SteamIDUserChanged;

		// Token: 0x04000530 RID: 1328
		internal ulong SteamIDMakingChange;

		// Token: 0x04000531 RID: 1329
		internal uint GfChatMemberStateChange;

		// Token: 0x020001BD RID: 445
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CF9 RID: 7417 RVA: 0x000619EC File Offset: 0x0005FBEC
			public static implicit operator LobbyChatUpdate_t(LobbyChatUpdate_t.PackSmall d)
			{
				return new LobbyChatUpdate_t
				{
					SteamIDLobby = d.SteamIDLobby,
					SteamIDUserChanged = d.SteamIDUserChanged,
					SteamIDMakingChange = d.SteamIDMakingChange,
					GfChatMemberStateChange = d.GfChatMemberStateChange
				};
			}

			// Token: 0x040009B8 RID: 2488
			internal ulong SteamIDLobby;

			// Token: 0x040009B9 RID: 2489
			internal ulong SteamIDUserChanged;

			// Token: 0x040009BA RID: 2490
			internal ulong SteamIDMakingChange;

			// Token: 0x040009BB RID: 2491
			internal uint GfChatMemberStateChange;
		}
	}
}
