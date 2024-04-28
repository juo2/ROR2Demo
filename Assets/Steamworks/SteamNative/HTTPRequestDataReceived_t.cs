using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000DE RID: 222
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTTPRequestDataReceived_t
	{
		// Token: 0x060006E6 RID: 1766 RVA: 0x00020F98 File Offset: 0x0001F198
		internal static HTTPRequestDataReceived_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTTPRequestDataReceived_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTTPRequestDataReceived_t.PackSmall));
			}
			return (HTTPRequestDataReceived_t)Marshal.PtrToStructure(p, typeof(HTTPRequestDataReceived_t));
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00020FD1 File Offset: 0x0001F1D1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTTPRequestDataReceived_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTTPRequestDataReceived_t));
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00020FFC File Offset: 0x0001F1FC
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
						ResultA = new Callback.VTableWinThis.ResultD(HTTPRequestDataReceived_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTTPRequestDataReceived_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTTPRequestDataReceived_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTTPRequestDataReceived_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTTPRequestDataReceived_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTTPRequestDataReceived_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTTPRequestDataReceived_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTTPRequestDataReceived_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTTPRequestDataReceived_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTTPRequestDataReceived_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTTPRequestDataReceived_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTTPRequestDataReceived_t.OnGetSize)
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
				CallbackId = 2103
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 2103);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00021302 File Offset: 0x0001F502
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTTPRequestDataReceived_t.OnResult(param);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0002130A File Offset: 0x0001F50A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTTPRequestDataReceived_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00021314 File Offset: 0x0001F514
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTTPRequestDataReceived_t.OnGetSize();
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0002131B File Offset: 0x0001F51B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTTPRequestDataReceived_t.StructSize();
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00021322 File Offset: 0x0001F522
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTTPRequestDataReceived_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00021334 File Offset: 0x0001F534
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTTPRequestDataReceived_t data = HTTPRequestDataReceived_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTTPRequestDataReceived_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTTPRequestDataReceived_t>(data);
			}
		}

		// Token: 0x04000649 RID: 1609
		internal const int CallbackId = 2103;

		// Token: 0x0400064A RID: 1610
		internal uint Request;

		// Token: 0x0400064B RID: 1611
		internal ulong ContextValue;

		// Token: 0x0400064C RID: 1612
		internal uint COffset;

		// Token: 0x0400064D RID: 1613
		internal uint CBytesReceived;

		// Token: 0x02000202 RID: 514
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D3E RID: 7486 RVA: 0x00062B3C File Offset: 0x00060D3C
			public static implicit operator HTTPRequestDataReceived_t(HTTPRequestDataReceived_t.PackSmall d)
			{
				return new HTTPRequestDataReceived_t
				{
					Request = d.Request,
					ContextValue = d.ContextValue,
					COffset = d.COffset,
					CBytesReceived = d.CBytesReceived
				};
			}

			// Token: 0x04000A92 RID: 2706
			internal uint Request;

			// Token: 0x04000A93 RID: 2707
			internal ulong ContextValue;

			// Token: 0x04000A94 RID: 2708
			internal uint COffset;

			// Token: 0x04000A95 RID: 2709
			internal uint CBytesReceived;
		}
	}
}
