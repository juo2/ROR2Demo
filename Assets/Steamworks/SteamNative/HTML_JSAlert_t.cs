using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000102 RID: 258
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_JSAlert_t
	{
		// Token: 0x06000808 RID: 2056 RVA: 0x00028394 File Offset: 0x00026594
		internal static HTML_JSAlert_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_JSAlert_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_JSAlert_t.PackSmall));
			}
			return (HTML_JSAlert_t)Marshal.PtrToStructure(p, typeof(HTML_JSAlert_t));
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x000283CD File Offset: 0x000265CD
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_JSAlert_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_JSAlert_t));
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000283F8 File Offset: 0x000265F8
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_JSAlert_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_JSAlert_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_JSAlert_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_JSAlert_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_JSAlert_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_JSAlert_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_JSAlert_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_JSAlert_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_JSAlert_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_JSAlert_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_JSAlert_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_JSAlert_t.OnGetSize)
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
				CallbackId = 4514
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4514);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x000286FE File Offset: 0x000268FE
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_JSAlert_t.OnResult(param);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00028706 File Offset: 0x00026906
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_JSAlert_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00028710 File Offset: 0x00026910
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_JSAlert_t.OnGetSize();
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00028717 File Offset: 0x00026917
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_JSAlert_t.StructSize();
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0002871E File Offset: 0x0002691E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_JSAlert_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00028730 File Offset: 0x00026930
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_JSAlert_t data = HTML_JSAlert_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_JSAlert_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_JSAlert_t>(data);
			}
		}

		// Token: 0x040006FE RID: 1790
		internal const int CallbackId = 4514;

		// Token: 0x040006FF RID: 1791
		internal uint UnBrowserHandle;

		// Token: 0x04000700 RID: 1792
		internal string PchMessage;

		// Token: 0x02000226 RID: 550
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D62 RID: 7522 RVA: 0x00063640 File Offset: 0x00061840
			public static implicit operator HTML_JSAlert_t(HTML_JSAlert_t.PackSmall d)
			{
				return new HTML_JSAlert_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchMessage = d.PchMessage
				};
			}

			// Token: 0x04000B2A RID: 2858
			internal uint UnBrowserHandle;

			// Token: 0x04000B2B RID: 2859
			internal string PchMessage;
		}
	}
}
