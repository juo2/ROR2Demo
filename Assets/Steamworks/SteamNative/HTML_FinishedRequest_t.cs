using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000FA RID: 250
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_FinishedRequest_t
	{
		// Token: 0x060007C0 RID: 1984 RVA: 0x000264D4 File Offset: 0x000246D4
		internal static HTML_FinishedRequest_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_FinishedRequest_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_FinishedRequest_t.PackSmall));
			}
			return (HTML_FinishedRequest_t)Marshal.PtrToStructure(p, typeof(HTML_FinishedRequest_t));
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0002650D File Offset: 0x0002470D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_FinishedRequest_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_FinishedRequest_t));
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00026538 File Offset: 0x00024738
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_FinishedRequest_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_FinishedRequest_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_FinishedRequest_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_FinishedRequest_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_FinishedRequest_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_FinishedRequest_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_FinishedRequest_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_FinishedRequest_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_FinishedRequest_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_FinishedRequest_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_FinishedRequest_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_FinishedRequest_t.OnGetSize)
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
				CallbackId = 4506
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4506);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0002683E File Offset: 0x00024A3E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_FinishedRequest_t.OnResult(param);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00026846 File Offset: 0x00024A46
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_FinishedRequest_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00026850 File Offset: 0x00024A50
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_FinishedRequest_t.OnGetSize();
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00026857 File Offset: 0x00024A57
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_FinishedRequest_t.StructSize();
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0002685E File Offset: 0x00024A5E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_FinishedRequest_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00026870 File Offset: 0x00024A70
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_FinishedRequest_t data = HTML_FinishedRequest_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_FinishedRequest_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_FinishedRequest_t>(data);
			}
		}

		// Token: 0x040006D7 RID: 1751
		internal const int CallbackId = 4506;

		// Token: 0x040006D8 RID: 1752
		internal uint UnBrowserHandle;

		// Token: 0x040006D9 RID: 1753
		internal string PchURL;

		// Token: 0x040006DA RID: 1754
		internal string PchPageTitle;

		// Token: 0x0200021E RID: 542
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D5A RID: 7514 RVA: 0x000633F4 File Offset: 0x000615F4
			public static implicit operator HTML_FinishedRequest_t(HTML_FinishedRequest_t.PackSmall d)
			{
				return new HTML_FinishedRequest_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchURL = d.PchURL,
					PchPageTitle = d.PchPageTitle
				};
			}

			// Token: 0x04000B0B RID: 2827
			internal uint UnBrowserHandle;

			// Token: 0x04000B0C RID: 2828
			internal string PchURL;

			// Token: 0x04000B0D RID: 2829
			internal string PchPageTitle;
		}
	}
}
