using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000F9 RID: 249
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_URLChanged_t
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x000260FF File Offset: 0x000242FF
		internal static HTML_URLChanged_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_URLChanged_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_URLChanged_t.PackSmall));
			}
			return (HTML_URLChanged_t)Marshal.PtrToStructure(p, typeof(HTML_URLChanged_t));
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00026138 File Offset: 0x00024338
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_URLChanged_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_URLChanged_t));
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00026160 File Offset: 0x00024360
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_URLChanged_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_URLChanged_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_URLChanged_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_URLChanged_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_URLChanged_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_URLChanged_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_URLChanged_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_URLChanged_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_URLChanged_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_URLChanged_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_URLChanged_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_URLChanged_t.OnGetSize)
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
				CallbackId = 4505
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4505);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00026466 File Offset: 0x00024666
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_URLChanged_t.OnResult(param);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0002646E File Offset: 0x0002466E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_URLChanged_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00026478 File Offset: 0x00024678
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_URLChanged_t.OnGetSize();
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0002647F File Offset: 0x0002467F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_URLChanged_t.StructSize();
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00026486 File Offset: 0x00024686
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_URLChanged_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00026498 File Offset: 0x00024698
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_URLChanged_t data = HTML_URLChanged_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_URLChanged_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_URLChanged_t>(data);
			}
		}

		// Token: 0x040006D0 RID: 1744
		internal const int CallbackId = 4505;

		// Token: 0x040006D1 RID: 1745
		internal uint UnBrowserHandle;

		// Token: 0x040006D2 RID: 1746
		internal string PchURL;

		// Token: 0x040006D3 RID: 1747
		internal string PchPostData;

		// Token: 0x040006D4 RID: 1748
		[MarshalAs(UnmanagedType.I1)]
		internal bool BIsRedirect;

		// Token: 0x040006D5 RID: 1749
		internal string PchPageTitle;

		// Token: 0x040006D6 RID: 1750
		[MarshalAs(UnmanagedType.I1)]
		internal bool BNewNavigation;

		// Token: 0x0200021D RID: 541
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D59 RID: 7513 RVA: 0x00063390 File Offset: 0x00061590
			public static implicit operator HTML_URLChanged_t(HTML_URLChanged_t.PackSmall d)
			{
				return new HTML_URLChanged_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchURL = d.PchURL,
					PchPostData = d.PchPostData,
					BIsRedirect = d.BIsRedirect,
					PchPageTitle = d.PchPageTitle,
					BNewNavigation = d.BNewNavigation
				};
			}

			// Token: 0x04000B05 RID: 2821
			internal uint UnBrowserHandle;

			// Token: 0x04000B06 RID: 2822
			internal string PchURL;

			// Token: 0x04000B07 RID: 2823
			internal string PchPostData;

			// Token: 0x04000B08 RID: 2824
			[MarshalAs(UnmanagedType.I1)]
			internal bool BIsRedirect;

			// Token: 0x04000B09 RID: 2825
			internal string PchPageTitle;

			// Token: 0x04000B0A RID: 2826
			[MarshalAs(UnmanagedType.I1)]
			internal bool BNewNavigation;
		}
	}
}
