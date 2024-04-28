using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200010A RID: 266
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_HideToolTip_t
	{
		// Token: 0x06000850 RID: 2128 RVA: 0x0002A254 File Offset: 0x00028454
		internal static HTML_HideToolTip_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_HideToolTip_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_HideToolTip_t.PackSmall));
			}
			return (HTML_HideToolTip_t)Marshal.PtrToStructure(p, typeof(HTML_HideToolTip_t));
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0002A28D File Offset: 0x0002848D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_HideToolTip_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_HideToolTip_t));
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0002A2B8 File Offset: 0x000284B8
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_HideToolTip_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_HideToolTip_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_HideToolTip_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_HideToolTip_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_HideToolTip_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_HideToolTip_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_HideToolTip_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_HideToolTip_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_HideToolTip_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_HideToolTip_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_HideToolTip_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_HideToolTip_t.OnGetSize)
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
				CallbackId = 4526
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4526);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0002A5BE File Offset: 0x000287BE
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_HideToolTip_t.OnResult(param);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0002A5C6 File Offset: 0x000287C6
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_HideToolTip_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0002A5D0 File Offset: 0x000287D0
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_HideToolTip_t.OnGetSize();
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0002A5D7 File Offset: 0x000287D7
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_HideToolTip_t.StructSize();
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0002A5DE File Offset: 0x000287DE
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_HideToolTip_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0002A5F0 File Offset: 0x000287F0
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_HideToolTip_t data = HTML_HideToolTip_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_HideToolTip_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_HideToolTip_t>(data);
			}
		}

		// Token: 0x0400071C RID: 1820
		internal const int CallbackId = 4526;

		// Token: 0x0400071D RID: 1821
		internal uint UnBrowserHandle;

		// Token: 0x0200022E RID: 558
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D6A RID: 7530 RVA: 0x00063814 File Offset: 0x00061A14
			public static implicit operator HTML_HideToolTip_t(HTML_HideToolTip_t.PackSmall d)
			{
				return new HTML_HideToolTip_t
				{
					UnBrowserHandle = d.UnBrowserHandle
				};
			}

			// Token: 0x04000B40 RID: 2880
			internal uint UnBrowserHandle;
		}
	}
}
