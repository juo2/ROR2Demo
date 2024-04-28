using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000FF RID: 255
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_HorizontalScroll_t
	{
		// Token: 0x060007ED RID: 2029 RVA: 0x0002780C File Offset: 0x00025A0C
		internal static HTML_HorizontalScroll_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_HorizontalScroll_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_HorizontalScroll_t.PackSmall));
			}
			return (HTML_HorizontalScroll_t)Marshal.PtrToStructure(p, typeof(HTML_HorizontalScroll_t));
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00027845 File Offset: 0x00025A45
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_HorizontalScroll_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_HorizontalScroll_t));
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00027870 File Offset: 0x00025A70
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_HorizontalScroll_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_HorizontalScroll_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_HorizontalScroll_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_HorizontalScroll_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_HorizontalScroll_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_HorizontalScroll_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_HorizontalScroll_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_HorizontalScroll_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_HorizontalScroll_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_HorizontalScroll_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_HorizontalScroll_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_HorizontalScroll_t.OnGetSize)
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
				CallbackId = 4511
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4511);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00027B76 File Offset: 0x00025D76
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_HorizontalScroll_t.OnResult(param);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00027B7E File Offset: 0x00025D7E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_HorizontalScroll_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00027B88 File Offset: 0x00025D88
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_HorizontalScroll_t.OnGetSize();
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00027B8F File Offset: 0x00025D8F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_HorizontalScroll_t.StructSize();
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00027B96 File Offset: 0x00025D96
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_HorizontalScroll_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00027BA8 File Offset: 0x00025DA8
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_HorizontalScroll_t data = HTML_HorizontalScroll_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_HorizontalScroll_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_HorizontalScroll_t>(data);
			}
		}

		// Token: 0x040006E9 RID: 1769
		internal const int CallbackId = 4511;

		// Token: 0x040006EA RID: 1770
		internal uint UnBrowserHandle;

		// Token: 0x040006EB RID: 1771
		internal uint UnScrollMax;

		// Token: 0x040006EC RID: 1772
		internal uint UnScrollCurrent;

		// Token: 0x040006ED RID: 1773
		internal float FlPageScale;

		// Token: 0x040006EE RID: 1774
		[MarshalAs(UnmanagedType.I1)]
		internal bool BVisible;

		// Token: 0x040006EF RID: 1775
		internal uint UnPageSize;

		// Token: 0x02000223 RID: 547
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D5F RID: 7519 RVA: 0x00063514 File Offset: 0x00061714
			public static implicit operator HTML_HorizontalScroll_t(HTML_HorizontalScroll_t.PackSmall d)
			{
				return new HTML_HorizontalScroll_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					UnScrollMax = d.UnScrollMax,
					UnScrollCurrent = d.UnScrollCurrent,
					FlPageScale = d.FlPageScale,
					BVisible = d.BVisible,
					UnPageSize = d.UnPageSize
				};
			}

			// Token: 0x04000B18 RID: 2840
			internal uint UnBrowserHandle;

			// Token: 0x04000B19 RID: 2841
			internal uint UnScrollMax;

			// Token: 0x04000B1A RID: 2842
			internal uint UnScrollCurrent;

			// Token: 0x04000B1B RID: 2843
			internal float FlPageScale;

			// Token: 0x04000B1C RID: 2844
			[MarshalAs(UnmanagedType.I1)]
			internal bool BVisible;

			// Token: 0x04000B1D RID: 2845
			internal uint UnPageSize;
		}
	}
}
