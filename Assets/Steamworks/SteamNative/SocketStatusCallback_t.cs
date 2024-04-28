using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000D3 RID: 211
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct SocketStatusCallback_t
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x0001E550 File Offset: 0x0001C750
		internal static SocketStatusCallback_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SocketStatusCallback_t.PackSmall)Marshal.PtrToStructure(p, typeof(SocketStatusCallback_t.PackSmall));
			}
			return (SocketStatusCallback_t)Marshal.PtrToStructure(p, typeof(SocketStatusCallback_t));
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001E589 File Offset: 0x0001C789
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SocketStatusCallback_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SocketStatusCallback_t));
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001E5B4 File Offset: 0x0001C7B4
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
						ResultA = new Callback.VTableWinThis.ResultD(SocketStatusCallback_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SocketStatusCallback_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SocketStatusCallback_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SocketStatusCallback_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SocketStatusCallback_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SocketStatusCallback_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SocketStatusCallback_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SocketStatusCallback_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SocketStatusCallback_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SocketStatusCallback_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SocketStatusCallback_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SocketStatusCallback_t.OnGetSize)
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
				CallbackId = 1201
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1201);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001E8BA File Offset: 0x0001CABA
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SocketStatusCallback_t.OnResult(param);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001E8C2 File Offset: 0x0001CAC2
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SocketStatusCallback_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001E8CC File Offset: 0x0001CACC
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SocketStatusCallback_t.OnGetSize();
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001E8D3 File Offset: 0x0001CAD3
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SocketStatusCallback_t.StructSize();
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001E8DA File Offset: 0x0001CADA
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SocketStatusCallback_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001E8EC File Offset: 0x0001CAEC
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SocketStatusCallback_t data = SocketStatusCallback_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SocketStatusCallback_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SocketStatusCallback_t>(data);
			}
		}

		// Token: 0x0400062A RID: 1578
		internal const int CallbackId = 1201;

		// Token: 0x0400062B RID: 1579
		internal uint Socket;

		// Token: 0x0400062C RID: 1580
		internal uint ListenSocket;

		// Token: 0x0400062D RID: 1581
		internal ulong SteamIDRemote;

		// Token: 0x0400062E RID: 1582
		internal int SNetSocketState;

		// Token: 0x020001F7 RID: 503
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D33 RID: 7475 RVA: 0x0006293C File Offset: 0x00060B3C
			public static implicit operator SocketStatusCallback_t(SocketStatusCallback_t.PackSmall d)
			{
				return new SocketStatusCallback_t
				{
					Socket = d.Socket,
					ListenSocket = d.ListenSocket,
					SteamIDRemote = d.SteamIDRemote,
					SNetSocketState = d.SNetSocketState
				};
			}

			// Token: 0x04000A7E RID: 2686
			internal uint Socket;

			// Token: 0x04000A7F RID: 2687
			internal uint ListenSocket;

			// Token: 0x04000A80 RID: 2688
			internal ulong SteamIDRemote;

			// Token: 0x04000A81 RID: 2689
			internal int SNetSocketState;
		}
	}
}
