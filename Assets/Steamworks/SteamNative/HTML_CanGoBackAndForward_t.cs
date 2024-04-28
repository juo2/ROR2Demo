using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000FE RID: 254
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_CanGoBackAndForward_t
	{
		// Token: 0x060007E4 RID: 2020 RVA: 0x00027434 File Offset: 0x00025634
		internal static HTML_CanGoBackAndForward_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_CanGoBackAndForward_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_CanGoBackAndForward_t.PackSmall));
			}
			return (HTML_CanGoBackAndForward_t)Marshal.PtrToStructure(p, typeof(HTML_CanGoBackAndForward_t));
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0002746D File Offset: 0x0002566D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_CanGoBackAndForward_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_CanGoBackAndForward_t));
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00027498 File Offset: 0x00025698
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_CanGoBackAndForward_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_CanGoBackAndForward_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_CanGoBackAndForward_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_CanGoBackAndForward_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_CanGoBackAndForward_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_CanGoBackAndForward_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_CanGoBackAndForward_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_CanGoBackAndForward_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_CanGoBackAndForward_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_CanGoBackAndForward_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_CanGoBackAndForward_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_CanGoBackAndForward_t.OnGetSize)
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
				CallbackId = 4510
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4510);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0002779E File Offset: 0x0002599E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_CanGoBackAndForward_t.OnResult(param);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000277A6 File Offset: 0x000259A6
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_CanGoBackAndForward_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x000277B0 File Offset: 0x000259B0
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_CanGoBackAndForward_t.OnGetSize();
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000277B7 File Offset: 0x000259B7
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_CanGoBackAndForward_t.StructSize();
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000277BE File Offset: 0x000259BE
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_CanGoBackAndForward_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x000277D0 File Offset: 0x000259D0
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_CanGoBackAndForward_t data = HTML_CanGoBackAndForward_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_CanGoBackAndForward_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_CanGoBackAndForward_t>(data);
			}
		}

		// Token: 0x040006E5 RID: 1765
		internal const int CallbackId = 4510;

		// Token: 0x040006E6 RID: 1766
		internal uint UnBrowserHandle;

		// Token: 0x040006E7 RID: 1767
		[MarshalAs(UnmanagedType.I1)]
		internal bool BCanGoBack;

		// Token: 0x040006E8 RID: 1768
		[MarshalAs(UnmanagedType.I1)]
		internal bool BCanGoForward;

		// Token: 0x02000222 RID: 546
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D5E RID: 7518 RVA: 0x000634D4 File Offset: 0x000616D4
			public static implicit operator HTML_CanGoBackAndForward_t(HTML_CanGoBackAndForward_t.PackSmall d)
			{
				return new HTML_CanGoBackAndForward_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					BCanGoBack = d.BCanGoBack,
					BCanGoForward = d.BCanGoForward
				};
			}

			// Token: 0x04000B15 RID: 2837
			internal uint UnBrowserHandle;

			// Token: 0x04000B16 RID: 2838
			[MarshalAs(UnmanagedType.I1)]
			internal bool BCanGoBack;

			// Token: 0x04000B17 RID: 2839
			[MarshalAs(UnmanagedType.I1)]
			internal bool BCanGoForward;
		}
	}
}
