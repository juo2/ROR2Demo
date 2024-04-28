using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000107 RID: 263
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_StatusText_t
	{
		// Token: 0x06000835 RID: 2101 RVA: 0x000296CC File Offset: 0x000278CC
		internal static HTML_StatusText_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_StatusText_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_StatusText_t.PackSmall));
			}
			return (HTML_StatusText_t)Marshal.PtrToStructure(p, typeof(HTML_StatusText_t));
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00029705 File Offset: 0x00027905
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_StatusText_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_StatusText_t));
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00029730 File Offset: 0x00027930
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_StatusText_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_StatusText_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_StatusText_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_StatusText_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_StatusText_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_StatusText_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_StatusText_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_StatusText_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_StatusText_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_StatusText_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_StatusText_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_StatusText_t.OnGetSize)
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
				CallbackId = 4523
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4523);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00029A36 File Offset: 0x00027C36
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_StatusText_t.OnResult(param);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00029A3E File Offset: 0x00027C3E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_StatusText_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00029A48 File Offset: 0x00027C48
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_StatusText_t.OnGetSize();
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00029A4F File Offset: 0x00027C4F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_StatusText_t.StructSize();
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00029A56 File Offset: 0x00027C56
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_StatusText_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00029A68 File Offset: 0x00027C68
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_StatusText_t data = HTML_StatusText_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_StatusText_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_StatusText_t>(data);
			}
		}

		// Token: 0x04000713 RID: 1811
		internal const int CallbackId = 4523;

		// Token: 0x04000714 RID: 1812
		internal uint UnBrowserHandle;

		// Token: 0x04000715 RID: 1813
		internal string PchMsg;

		// Token: 0x0200022B RID: 555
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D67 RID: 7527 RVA: 0x00063784 File Offset: 0x00061984
			public static implicit operator HTML_StatusText_t(HTML_StatusText_t.PackSmall d)
			{
				return new HTML_StatusText_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchMsg = d.PchMsg
				};
			}

			// Token: 0x04000B3A RID: 2874
			internal uint UnBrowserHandle;

			// Token: 0x04000B3B RID: 2875
			internal string PchMsg;
		}
	}
}
