using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000F5 RID: 245
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_BrowserReady_t
	{
		// Token: 0x060007A7 RID: 1959 RVA: 0x00025BE4 File Offset: 0x00023DE4
		internal static HTML_BrowserReady_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_BrowserReady_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_BrowserReady_t.PackSmall));
			}
			return (HTML_BrowserReady_t)Marshal.PtrToStructure(p, typeof(HTML_BrowserReady_t));
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00025C1D File Offset: 0x00023E1D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_BrowserReady_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_BrowserReady_t));
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00025C45 File Offset: 0x00023E45
		internal static CallResult<HTML_BrowserReady_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<HTML_BrowserReady_t, bool> CallbackFunction)
		{
			return new CallResult<HTML_BrowserReady_t>(steamworks, call, CallbackFunction, new CallResult<HTML_BrowserReady_t>.ConvertFromPointer(HTML_BrowserReady_t.FromPointer), HTML_BrowserReady_t.StructSize(), 4501);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00025C68 File Offset: 0x00023E68
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_BrowserReady_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_BrowserReady_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_BrowserReady_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_BrowserReady_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_BrowserReady_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_BrowserReady_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_BrowserReady_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_BrowserReady_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_BrowserReady_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_BrowserReady_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_BrowserReady_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_BrowserReady_t.OnGetSize)
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
				CallbackId = 4501
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4501);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00025F6E File Offset: 0x0002416E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_BrowserReady_t.OnResult(param);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00025F76 File Offset: 0x00024176
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_BrowserReady_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00025F80 File Offset: 0x00024180
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_BrowserReady_t.OnGetSize();
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00025F87 File Offset: 0x00024187
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_BrowserReady_t.StructSize();
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00025F8E File Offset: 0x0002418E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_BrowserReady_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00025FA0 File Offset: 0x000241A0
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_BrowserReady_t data = HTML_BrowserReady_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_BrowserReady_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_BrowserReady_t>(data);
			}
		}

		// Token: 0x040006BC RID: 1724
		internal const int CallbackId = 4501;

		// Token: 0x040006BD RID: 1725
		internal uint UnBrowserHandle;

		// Token: 0x02000219 RID: 537
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D55 RID: 7509 RVA: 0x0006323C File Offset: 0x0006143C
			public static implicit operator HTML_BrowserReady_t(HTML_BrowserReady_t.PackSmall d)
			{
				return new HTML_BrowserReady_t
				{
					UnBrowserHandle = d.UnBrowserHandle
				};
			}

			// Token: 0x04000AF2 RID: 2802
			internal uint UnBrowserHandle;
		}
	}
}
