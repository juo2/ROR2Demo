using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000D2 RID: 210
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct P2PSessionConnectFail_t
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x0001E178 File Offset: 0x0001C378
		internal static P2PSessionConnectFail_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (P2PSessionConnectFail_t.PackSmall)Marshal.PtrToStructure(p, typeof(P2PSessionConnectFail_t.PackSmall));
			}
			return (P2PSessionConnectFail_t)Marshal.PtrToStructure(p, typeof(P2PSessionConnectFail_t));
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001E1B1 File Offset: 0x0001C3B1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(P2PSessionConnectFail_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(P2PSessionConnectFail_t));
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001E1DC File Offset: 0x0001C3DC
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
						ResultA = new Callback.VTableWinThis.ResultD(P2PSessionConnectFail_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(P2PSessionConnectFail_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(P2PSessionConnectFail_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(P2PSessionConnectFail_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(P2PSessionConnectFail_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(P2PSessionConnectFail_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(P2PSessionConnectFail_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(P2PSessionConnectFail_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(P2PSessionConnectFail_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(P2PSessionConnectFail_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(P2PSessionConnectFail_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(P2PSessionConnectFail_t.OnGetSize)
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
				CallbackId = 1203
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1203);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0001E4E2 File Offset: 0x0001C6E2
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			P2PSessionConnectFail_t.OnResult(param);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0001E4EA File Offset: 0x0001C6EA
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			P2PSessionConnectFail_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001E4F4 File Offset: 0x0001C6F4
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return P2PSessionConnectFail_t.OnGetSize();
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001E4FB File Offset: 0x0001C6FB
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return P2PSessionConnectFail_t.StructSize();
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001E502 File Offset: 0x0001C702
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			P2PSessionConnectFail_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001E514 File Offset: 0x0001C714
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			P2PSessionConnectFail_t data = P2PSessionConnectFail_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<P2PSessionConnectFail_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<P2PSessionConnectFail_t>(data);
			}
		}

		// Token: 0x04000627 RID: 1575
		internal const int CallbackId = 1203;

		// Token: 0x04000628 RID: 1576
		internal ulong SteamIDRemote;

		// Token: 0x04000629 RID: 1577
		internal byte P2PSessionError;

		// Token: 0x020001F6 RID: 502
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D32 RID: 7474 RVA: 0x0006290C File Offset: 0x00060B0C
			public static implicit operator P2PSessionConnectFail_t(P2PSessionConnectFail_t.PackSmall d)
			{
				return new P2PSessionConnectFail_t
				{
					SteamIDRemote = d.SteamIDRemote,
					P2PSessionError = d.P2PSessionError
				};
			}

			// Token: 0x04000A7C RID: 2684
			internal ulong SteamIDRemote;

			// Token: 0x04000A7D RID: 2685
			internal byte P2PSessionError;
		}
	}
}
