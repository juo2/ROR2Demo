using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200009A RID: 154
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyChatMsg_t
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x00011120 File Offset: 0x0000F320
		internal static LobbyChatMsg_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (LobbyChatMsg_t.PackSmall)Marshal.PtrToStructure(p, typeof(LobbyChatMsg_t.PackSmall));
			}
			return (LobbyChatMsg_t)Marshal.PtrToStructure(p, typeof(LobbyChatMsg_t));
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00011159 File Offset: 0x0000F359
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(LobbyChatMsg_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(LobbyChatMsg_t));
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00011184 File Offset: 0x0000F384
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
						ResultA = new Callback.VTableWinThis.ResultD(LobbyChatMsg_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(LobbyChatMsg_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(LobbyChatMsg_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(LobbyChatMsg_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(LobbyChatMsg_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(LobbyChatMsg_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(LobbyChatMsg_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(LobbyChatMsg_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(LobbyChatMsg_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(LobbyChatMsg_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(LobbyChatMsg_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(LobbyChatMsg_t.OnGetSize)
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
				CallbackId = 507
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 507);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001148A File Offset: 0x0000F68A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			LobbyChatMsg_t.OnResult(param);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00011492 File Offset: 0x0000F692
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			LobbyChatMsg_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001149C File Offset: 0x0000F69C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return LobbyChatMsg_t.OnGetSize();
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000114A3 File Offset: 0x0000F6A3
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return LobbyChatMsg_t.StructSize();
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000114AA File Offset: 0x0000F6AA
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			LobbyChatMsg_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000114BC File Offset: 0x0000F6BC
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			LobbyChatMsg_t data = LobbyChatMsg_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<LobbyChatMsg_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<LobbyChatMsg_t>(data);
			}
		}

		// Token: 0x04000532 RID: 1330
		internal const int CallbackId = 507;

		// Token: 0x04000533 RID: 1331
		internal ulong SteamIDLobby;

		// Token: 0x04000534 RID: 1332
		internal ulong SteamIDUser;

		// Token: 0x04000535 RID: 1333
		internal byte ChatEntryType;

		// Token: 0x04000536 RID: 1334
		internal uint ChatID;

		// Token: 0x020001BE RID: 446
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CFA RID: 7418 RVA: 0x00061A38 File Offset: 0x0005FC38
			public static implicit operator LobbyChatMsg_t(LobbyChatMsg_t.PackSmall d)
			{
				return new LobbyChatMsg_t
				{
					SteamIDLobby = d.SteamIDLobby,
					SteamIDUser = d.SteamIDUser,
					ChatEntryType = d.ChatEntryType,
					ChatID = d.ChatID
				};
			}

			// Token: 0x040009BC RID: 2492
			internal ulong SteamIDLobby;

			// Token: 0x040009BD RID: 2493
			internal ulong SteamIDUser;

			// Token: 0x040009BE RID: 2494
			internal byte ChatEntryType;

			// Token: 0x040009BF RID: 2495
			internal uint ChatID;
		}
	}
}
