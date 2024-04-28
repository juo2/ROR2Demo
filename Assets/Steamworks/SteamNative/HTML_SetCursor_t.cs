using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000106 RID: 262
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_SetCursor_t
	{
		// Token: 0x0600082C RID: 2092 RVA: 0x000292F4 File Offset: 0x000274F4
		internal static HTML_SetCursor_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_SetCursor_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_SetCursor_t.PackSmall));
			}
			return (HTML_SetCursor_t)Marshal.PtrToStructure(p, typeof(HTML_SetCursor_t));
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0002932D File Offset: 0x0002752D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_SetCursor_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_SetCursor_t));
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00029358 File Offset: 0x00027558
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_SetCursor_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_SetCursor_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_SetCursor_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_SetCursor_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_SetCursor_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_SetCursor_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_SetCursor_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_SetCursor_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_SetCursor_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_SetCursor_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_SetCursor_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_SetCursor_t.OnGetSize)
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
				CallbackId = 4522
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4522);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0002965E File Offset: 0x0002785E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_SetCursor_t.OnResult(param);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00029666 File Offset: 0x00027866
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_SetCursor_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00029670 File Offset: 0x00027870
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_SetCursor_t.OnGetSize();
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00029677 File Offset: 0x00027877
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_SetCursor_t.StructSize();
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0002967E File Offset: 0x0002787E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_SetCursor_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00029690 File Offset: 0x00027890
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_SetCursor_t data = HTML_SetCursor_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_SetCursor_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_SetCursor_t>(data);
			}
		}

		// Token: 0x04000710 RID: 1808
		internal const int CallbackId = 4522;

		// Token: 0x04000711 RID: 1809
		internal uint UnBrowserHandle;

		// Token: 0x04000712 RID: 1810
		internal uint EMouseCursor;

		// Token: 0x0200022A RID: 554
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D66 RID: 7526 RVA: 0x00063754 File Offset: 0x00061954
			public static implicit operator HTML_SetCursor_t(HTML_SetCursor_t.PackSmall d)
			{
				return new HTML_SetCursor_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					EMouseCursor = d.EMouseCursor
				};
			}

			// Token: 0x04000B38 RID: 2872
			internal uint UnBrowserHandle;

			// Token: 0x04000B39 RID: 2873
			internal uint EMouseCursor;
		}
	}
}
