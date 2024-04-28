using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000109 RID: 265
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_UpdateToolTip_t
	{
		// Token: 0x06000847 RID: 2119 RVA: 0x00029E7C File Offset: 0x0002807C
		internal static HTML_UpdateToolTip_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_UpdateToolTip_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_UpdateToolTip_t.PackSmall));
			}
			return (HTML_UpdateToolTip_t)Marshal.PtrToStructure(p, typeof(HTML_UpdateToolTip_t));
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00029EB5 File Offset: 0x000280B5
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_UpdateToolTip_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_UpdateToolTip_t));
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00029EE0 File Offset: 0x000280E0
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_UpdateToolTip_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_UpdateToolTip_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_UpdateToolTip_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_UpdateToolTip_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_UpdateToolTip_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_UpdateToolTip_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_UpdateToolTip_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_UpdateToolTip_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_UpdateToolTip_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_UpdateToolTip_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_UpdateToolTip_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_UpdateToolTip_t.OnGetSize)
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
				CallbackId = 4525
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4525);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0002A1E6 File Offset: 0x000283E6
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_UpdateToolTip_t.OnResult(param);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0002A1EE File Offset: 0x000283EE
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_UpdateToolTip_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0002A1F8 File Offset: 0x000283F8
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_UpdateToolTip_t.OnGetSize();
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0002A1FF File Offset: 0x000283FF
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_UpdateToolTip_t.StructSize();
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0002A206 File Offset: 0x00028406
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_UpdateToolTip_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0002A218 File Offset: 0x00028418
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_UpdateToolTip_t data = HTML_UpdateToolTip_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_UpdateToolTip_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_UpdateToolTip_t>(data);
			}
		}

		// Token: 0x04000719 RID: 1817
		internal const int CallbackId = 4525;

		// Token: 0x0400071A RID: 1818
		internal uint UnBrowserHandle;

		// Token: 0x0400071B RID: 1819
		internal string PchMsg;

		// Token: 0x0200022D RID: 557
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D69 RID: 7529 RVA: 0x000637E4 File Offset: 0x000619E4
			public static implicit operator HTML_UpdateToolTip_t(HTML_UpdateToolTip_t.PackSmall d)
			{
				return new HTML_UpdateToolTip_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchMsg = d.PchMsg
				};
			}

			// Token: 0x04000B3E RID: 2878
			internal uint UnBrowserHandle;

			// Token: 0x04000B3F RID: 2879
			internal string PchMsg;
		}
	}
}
