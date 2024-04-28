using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000DD RID: 221
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTTPRequestHeadersReceived_t
	{
		// Token: 0x060006DD RID: 1757 RVA: 0x00020BC0 File Offset: 0x0001EDC0
		internal static HTTPRequestHeadersReceived_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTTPRequestHeadersReceived_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTTPRequestHeadersReceived_t.PackSmall));
			}
			return (HTTPRequestHeadersReceived_t)Marshal.PtrToStructure(p, typeof(HTTPRequestHeadersReceived_t));
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00020BF9 File Offset: 0x0001EDF9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTTPRequestHeadersReceived_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTTPRequestHeadersReceived_t));
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00020C24 File Offset: 0x0001EE24
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
						ResultA = new Callback.VTableWinThis.ResultD(HTTPRequestHeadersReceived_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTTPRequestHeadersReceived_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTTPRequestHeadersReceived_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTTPRequestHeadersReceived_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTTPRequestHeadersReceived_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTTPRequestHeadersReceived_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTTPRequestHeadersReceived_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTTPRequestHeadersReceived_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTTPRequestHeadersReceived_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTTPRequestHeadersReceived_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTTPRequestHeadersReceived_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTTPRequestHeadersReceived_t.OnGetSize)
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
				CallbackId = 2102
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 2102);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00020F2A File Offset: 0x0001F12A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTTPRequestHeadersReceived_t.OnResult(param);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00020F32 File Offset: 0x0001F132
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTTPRequestHeadersReceived_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00020F3C File Offset: 0x0001F13C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTTPRequestHeadersReceived_t.OnGetSize();
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00020F43 File Offset: 0x0001F143
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTTPRequestHeadersReceived_t.StructSize();
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00020F4A File Offset: 0x0001F14A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTTPRequestHeadersReceived_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00020F5C File Offset: 0x0001F15C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTTPRequestHeadersReceived_t data = HTTPRequestHeadersReceived_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTTPRequestHeadersReceived_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTTPRequestHeadersReceived_t>(data);
			}
		}

		// Token: 0x04000646 RID: 1606
		internal const int CallbackId = 2102;

		// Token: 0x04000647 RID: 1607
		internal uint Request;

		// Token: 0x04000648 RID: 1608
		internal ulong ContextValue;

		// Token: 0x02000201 RID: 513
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D3D RID: 7485 RVA: 0x00062B0C File Offset: 0x00060D0C
			public static implicit operator HTTPRequestHeadersReceived_t(HTTPRequestHeadersReceived_t.PackSmall d)
			{
				return new HTTPRequestHeadersReceived_t
				{
					Request = d.Request,
					ContextValue = d.ContextValue
				};
			}

			// Token: 0x04000A90 RID: 2704
			internal uint Request;

			// Token: 0x04000A91 RID: 2705
			internal ulong ContextValue;
		}
	}
}
