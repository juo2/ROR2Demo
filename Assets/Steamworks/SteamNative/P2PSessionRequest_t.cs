using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000D1 RID: 209
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct P2PSessionRequest_t
	{
		// Token: 0x06000671 RID: 1649 RVA: 0x0001DDA1 File Offset: 0x0001BFA1
		internal static P2PSessionRequest_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (P2PSessionRequest_t.PackSmall)Marshal.PtrToStructure(p, typeof(P2PSessionRequest_t.PackSmall));
			}
			return (P2PSessionRequest_t)Marshal.PtrToStructure(p, typeof(P2PSessionRequest_t));
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001DDDA File Offset: 0x0001BFDA
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(P2PSessionRequest_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(P2PSessionRequest_t));
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001DE04 File Offset: 0x0001C004
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
						ResultA = new Callback.VTableWinThis.ResultD(P2PSessionRequest_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(P2PSessionRequest_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(P2PSessionRequest_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(P2PSessionRequest_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(P2PSessionRequest_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(P2PSessionRequest_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(P2PSessionRequest_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(P2PSessionRequest_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(P2PSessionRequest_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(P2PSessionRequest_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(P2PSessionRequest_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(P2PSessionRequest_t.OnGetSize)
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
				CallbackId = 1202
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1202);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001E10A File Offset: 0x0001C30A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			P2PSessionRequest_t.OnResult(param);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001E112 File Offset: 0x0001C312
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			P2PSessionRequest_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001E11C File Offset: 0x0001C31C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return P2PSessionRequest_t.OnGetSize();
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001E123 File Offset: 0x0001C323
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return P2PSessionRequest_t.StructSize();
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001E12A File Offset: 0x0001C32A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			P2PSessionRequest_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001E13C File Offset: 0x0001C33C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			P2PSessionRequest_t data = P2PSessionRequest_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<P2PSessionRequest_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<P2PSessionRequest_t>(data);
			}
		}

		// Token: 0x04000625 RID: 1573
		internal const int CallbackId = 1202;

		// Token: 0x04000626 RID: 1574
		internal ulong SteamIDRemote;

		// Token: 0x020001F5 RID: 501
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D31 RID: 7473 RVA: 0x000628E8 File Offset: 0x00060AE8
			public static implicit operator P2PSessionRequest_t(P2PSessionRequest_t.PackSmall d)
			{
				return new P2PSessionRequest_t
				{
					SteamIDRemote = d.SteamIDRemote
				};
			}

			// Token: 0x04000A7B RID: 2683
			internal ulong SteamIDRemote;
		}
	}
}
