using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000105 RID: 261
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_NewWindow_t
	{
		// Token: 0x06000823 RID: 2083 RVA: 0x00028F1C File Offset: 0x0002711C
		internal static HTML_NewWindow_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_NewWindow_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_NewWindow_t.PackSmall));
			}
			return (HTML_NewWindow_t)Marshal.PtrToStructure(p, typeof(HTML_NewWindow_t));
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00028F55 File Offset: 0x00027155
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_NewWindow_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_NewWindow_t));
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00028F80 File Offset: 0x00027180
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_NewWindow_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_NewWindow_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_NewWindow_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_NewWindow_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_NewWindow_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_NewWindow_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_NewWindow_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_NewWindow_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_NewWindow_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_NewWindow_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_NewWindow_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_NewWindow_t.OnGetSize)
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
				CallbackId = 4521
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4521);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00029286 File Offset: 0x00027486
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_NewWindow_t.OnResult(param);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0002928E File Offset: 0x0002748E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_NewWindow_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00029298 File Offset: 0x00027498
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_NewWindow_t.OnGetSize();
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0002929F File Offset: 0x0002749F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_NewWindow_t.StructSize();
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000292A6 File Offset: 0x000274A6
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_NewWindow_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000292B8 File Offset: 0x000274B8
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_NewWindow_t data = HTML_NewWindow_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_NewWindow_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_NewWindow_t>(data);
			}
		}

		// Token: 0x04000708 RID: 1800
		internal const int CallbackId = 4521;

		// Token: 0x04000709 RID: 1801
		internal uint UnBrowserHandle;

		// Token: 0x0400070A RID: 1802
		internal string PchURL;

		// Token: 0x0400070B RID: 1803
		internal uint UnX;

		// Token: 0x0400070C RID: 1804
		internal uint UnY;

		// Token: 0x0400070D RID: 1805
		internal uint UnWide;

		// Token: 0x0400070E RID: 1806
		internal uint UnTall;

		// Token: 0x0400070F RID: 1807
		internal uint UnNewWindow_BrowserHandle;

		// Token: 0x02000229 RID: 553
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D65 RID: 7525 RVA: 0x000636E0 File Offset: 0x000618E0
			public static implicit operator HTML_NewWindow_t(HTML_NewWindow_t.PackSmall d)
			{
				return new HTML_NewWindow_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchURL = d.PchURL,
					UnX = d.UnX,
					UnY = d.UnY,
					UnWide = d.UnWide,
					UnTall = d.UnTall,
					UnNewWindow_BrowserHandle = d.UnNewWindow_BrowserHandle
				};
			}

			// Token: 0x04000B31 RID: 2865
			internal uint UnBrowserHandle;

			// Token: 0x04000B32 RID: 2866
			internal string PchURL;

			// Token: 0x04000B33 RID: 2867
			internal uint UnX;

			// Token: 0x04000B34 RID: 2868
			internal uint UnY;

			// Token: 0x04000B35 RID: 2869
			internal uint UnWide;

			// Token: 0x04000B36 RID: 2870
			internal uint UnTall;

			// Token: 0x04000B37 RID: 2871
			internal uint UnNewWindow_BrowserHandle;
		}
	}
}
