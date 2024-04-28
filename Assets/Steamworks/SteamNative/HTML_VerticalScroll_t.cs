using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000100 RID: 256
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_VerticalScroll_t
	{
		// Token: 0x060007F6 RID: 2038 RVA: 0x00027BE4 File Offset: 0x00025DE4
		internal static HTML_VerticalScroll_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_VerticalScroll_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_VerticalScroll_t.PackSmall));
			}
			return (HTML_VerticalScroll_t)Marshal.PtrToStructure(p, typeof(HTML_VerticalScroll_t));
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00027C1D File Offset: 0x00025E1D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_VerticalScroll_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_VerticalScroll_t));
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00027C48 File Offset: 0x00025E48
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_VerticalScroll_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_VerticalScroll_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_VerticalScroll_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_VerticalScroll_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_VerticalScroll_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_VerticalScroll_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_VerticalScroll_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_VerticalScroll_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_VerticalScroll_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_VerticalScroll_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_VerticalScroll_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_VerticalScroll_t.OnGetSize)
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
				CallbackId = 4512
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4512);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00027F4E File Offset: 0x0002614E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_VerticalScroll_t.OnResult(param);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00027F56 File Offset: 0x00026156
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_VerticalScroll_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00027F60 File Offset: 0x00026160
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_VerticalScroll_t.OnGetSize();
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00027F67 File Offset: 0x00026167
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_VerticalScroll_t.StructSize();
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00027F6E File Offset: 0x0002616E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_VerticalScroll_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00027F80 File Offset: 0x00026180
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_VerticalScroll_t data = HTML_VerticalScroll_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_VerticalScroll_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_VerticalScroll_t>(data);
			}
		}

		// Token: 0x040006F0 RID: 1776
		internal const int CallbackId = 4512;

		// Token: 0x040006F1 RID: 1777
		internal uint UnBrowserHandle;

		// Token: 0x040006F2 RID: 1778
		internal uint UnScrollMax;

		// Token: 0x040006F3 RID: 1779
		internal uint UnScrollCurrent;

		// Token: 0x040006F4 RID: 1780
		internal float FlPageScale;

		// Token: 0x040006F5 RID: 1781
		[MarshalAs(UnmanagedType.I1)]
		internal bool BVisible;

		// Token: 0x040006F6 RID: 1782
		internal uint UnPageSize;

		// Token: 0x02000224 RID: 548
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D60 RID: 7520 RVA: 0x00063578 File Offset: 0x00061778
			public static implicit operator HTML_VerticalScroll_t(HTML_VerticalScroll_t.PackSmall d)
			{
				return new HTML_VerticalScroll_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					UnScrollMax = d.UnScrollMax,
					UnScrollCurrent = d.UnScrollCurrent,
					FlPageScale = d.FlPageScale,
					BVisible = d.BVisible,
					UnPageSize = d.UnPageSize
				};
			}

			// Token: 0x04000B1E RID: 2846
			internal uint UnBrowserHandle;

			// Token: 0x04000B1F RID: 2847
			internal uint UnScrollMax;

			// Token: 0x04000B20 RID: 2848
			internal uint UnScrollCurrent;

			// Token: 0x04000B21 RID: 2849
			internal float FlPageScale;

			// Token: 0x04000B22 RID: 2850
			[MarshalAs(UnmanagedType.I1)]
			internal bool BVisible;

			// Token: 0x04000B23 RID: 2851
			internal uint UnPageSize;
		}
	}
}
