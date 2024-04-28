using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200010B RID: 267
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_BrowserRestarted_t
	{
		// Token: 0x06000859 RID: 2137 RVA: 0x0002A62C File Offset: 0x0002882C
		internal static HTML_BrowserRestarted_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_BrowserRestarted_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_BrowserRestarted_t.PackSmall));
			}
			return (HTML_BrowserRestarted_t)Marshal.PtrToStructure(p, typeof(HTML_BrowserRestarted_t));
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0002A665 File Offset: 0x00028865
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_BrowserRestarted_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_BrowserRestarted_t));
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0002A690 File Offset: 0x00028890
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_BrowserRestarted_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_BrowserRestarted_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_BrowserRestarted_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_BrowserRestarted_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_BrowserRestarted_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_BrowserRestarted_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_BrowserRestarted_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_BrowserRestarted_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_BrowserRestarted_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_BrowserRestarted_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_BrowserRestarted_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_BrowserRestarted_t.OnGetSize)
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
				CallbackId = 4527
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4527);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0002A996 File Offset: 0x00028B96
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_BrowserRestarted_t.OnResult(param);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0002A99E File Offset: 0x00028B9E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_BrowserRestarted_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0002A9A8 File Offset: 0x00028BA8
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_BrowserRestarted_t.OnGetSize();
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0002A9AF File Offset: 0x00028BAF
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_BrowserRestarted_t.StructSize();
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0002A9B6 File Offset: 0x00028BB6
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_BrowserRestarted_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0002A9C8 File Offset: 0x00028BC8
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_BrowserRestarted_t data = HTML_BrowserRestarted_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_BrowserRestarted_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_BrowserRestarted_t>(data);
			}
		}

		// Token: 0x0400071E RID: 1822
		internal const int CallbackId = 4527;

		// Token: 0x0400071F RID: 1823
		internal uint UnBrowserHandle;

		// Token: 0x04000720 RID: 1824
		internal uint UnOldBrowserHandle;

		// Token: 0x0200022F RID: 559
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D6B RID: 7531 RVA: 0x00063838 File Offset: 0x00061A38
			public static implicit operator HTML_BrowserRestarted_t(HTML_BrowserRestarted_t.PackSmall d)
			{
				return new HTML_BrowserRestarted_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					UnOldBrowserHandle = d.UnOldBrowserHandle
				};
			}

			// Token: 0x04000B41 RID: 2881
			internal uint UnBrowserHandle;

			// Token: 0x04000B42 RID: 2882
			internal uint UnOldBrowserHandle;
		}
	}
}
