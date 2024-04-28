using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000FB RID: 251
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_OpenLinkInNewTab_t
	{
		// Token: 0x060007C9 RID: 1993 RVA: 0x000268AC File Offset: 0x00024AAC
		internal static HTML_OpenLinkInNewTab_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_OpenLinkInNewTab_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_OpenLinkInNewTab_t.PackSmall));
			}
			return (HTML_OpenLinkInNewTab_t)Marshal.PtrToStructure(p, typeof(HTML_OpenLinkInNewTab_t));
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000268E5 File Offset: 0x00024AE5
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_OpenLinkInNewTab_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_OpenLinkInNewTab_t));
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00026910 File Offset: 0x00024B10
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_OpenLinkInNewTab_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_OpenLinkInNewTab_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_OpenLinkInNewTab_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_OpenLinkInNewTab_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_OpenLinkInNewTab_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_OpenLinkInNewTab_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_OpenLinkInNewTab_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_OpenLinkInNewTab_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_OpenLinkInNewTab_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_OpenLinkInNewTab_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_OpenLinkInNewTab_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_OpenLinkInNewTab_t.OnGetSize)
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
				CallbackId = 4507
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4507);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00026C16 File Offset: 0x00024E16
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_OpenLinkInNewTab_t.OnResult(param);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00026C1E File Offset: 0x00024E1E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_OpenLinkInNewTab_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00026C28 File Offset: 0x00024E28
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_OpenLinkInNewTab_t.OnGetSize();
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00026C2F File Offset: 0x00024E2F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_OpenLinkInNewTab_t.StructSize();
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00026C36 File Offset: 0x00024E36
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_OpenLinkInNewTab_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00026C48 File Offset: 0x00024E48
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_OpenLinkInNewTab_t data = HTML_OpenLinkInNewTab_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_OpenLinkInNewTab_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_OpenLinkInNewTab_t>(data);
			}
		}

		// Token: 0x040006DB RID: 1755
		internal const int CallbackId = 4507;

		// Token: 0x040006DC RID: 1756
		internal uint UnBrowserHandle;

		// Token: 0x040006DD RID: 1757
		internal string PchURL;

		// Token: 0x0200021F RID: 543
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D5B RID: 7515 RVA: 0x00063434 File Offset: 0x00061634
			public static implicit operator HTML_OpenLinkInNewTab_t(HTML_OpenLinkInNewTab_t.PackSmall d)
			{
				return new HTML_OpenLinkInNewTab_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchURL = d.PchURL
				};
			}

			// Token: 0x04000B0E RID: 2830
			internal uint UnBrowserHandle;

			// Token: 0x04000B0F RID: 2831
			internal string PchURL;
		}
	}
}
