using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000103 RID: 259
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_JSConfirm_t
	{
		// Token: 0x06000811 RID: 2065 RVA: 0x0002876C File Offset: 0x0002696C
		internal static HTML_JSConfirm_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_JSConfirm_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_JSConfirm_t.PackSmall));
			}
			return (HTML_JSConfirm_t)Marshal.PtrToStructure(p, typeof(HTML_JSConfirm_t));
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x000287A5 File Offset: 0x000269A5
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_JSConfirm_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_JSConfirm_t));
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x000287D0 File Offset: 0x000269D0
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_JSConfirm_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_JSConfirm_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_JSConfirm_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_JSConfirm_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_JSConfirm_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_JSConfirm_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_JSConfirm_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_JSConfirm_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_JSConfirm_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_JSConfirm_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_JSConfirm_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_JSConfirm_t.OnGetSize)
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
				CallbackId = 4515
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4515);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00028AD6 File Offset: 0x00026CD6
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_JSConfirm_t.OnResult(param);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00028ADE File Offset: 0x00026CDE
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_JSConfirm_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00028AE8 File Offset: 0x00026CE8
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_JSConfirm_t.OnGetSize();
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00028AEF File Offset: 0x00026CEF
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_JSConfirm_t.StructSize();
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00028AF6 File Offset: 0x00026CF6
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_JSConfirm_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00028B08 File Offset: 0x00026D08
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_JSConfirm_t data = HTML_JSConfirm_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_JSConfirm_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_JSConfirm_t>(data);
			}
		}

		// Token: 0x04000701 RID: 1793
		internal const int CallbackId = 4515;

		// Token: 0x04000702 RID: 1794
		internal uint UnBrowserHandle;

		// Token: 0x04000703 RID: 1795
		internal string PchMessage;

		// Token: 0x02000227 RID: 551
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D63 RID: 7523 RVA: 0x00063670 File Offset: 0x00061870
			public static implicit operator HTML_JSConfirm_t(HTML_JSConfirm_t.PackSmall d)
			{
				return new HTML_JSConfirm_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchMessage = d.PchMessage
				};
			}

			// Token: 0x04000B2C RID: 2860
			internal uint UnBrowserHandle;

			// Token: 0x04000B2D RID: 2861
			internal string PchMessage;
		}
	}
}
