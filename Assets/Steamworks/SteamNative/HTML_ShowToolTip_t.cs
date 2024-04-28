using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000108 RID: 264
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_ShowToolTip_t
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x00029AA4 File Offset: 0x00027CA4
		internal static HTML_ShowToolTip_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_ShowToolTip_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_ShowToolTip_t.PackSmall));
			}
			return (HTML_ShowToolTip_t)Marshal.PtrToStructure(p, typeof(HTML_ShowToolTip_t));
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00029ADD File Offset: 0x00027CDD
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_ShowToolTip_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_ShowToolTip_t));
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00029B08 File Offset: 0x00027D08
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_ShowToolTip_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_ShowToolTip_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_ShowToolTip_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_ShowToolTip_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_ShowToolTip_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_ShowToolTip_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_ShowToolTip_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_ShowToolTip_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_ShowToolTip_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_ShowToolTip_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_ShowToolTip_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_ShowToolTip_t.OnGetSize)
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
				CallbackId = 4524
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4524);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00029E0E File Offset: 0x0002800E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_ShowToolTip_t.OnResult(param);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00029E16 File Offset: 0x00028016
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_ShowToolTip_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00029E20 File Offset: 0x00028020
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_ShowToolTip_t.OnGetSize();
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00029E27 File Offset: 0x00028027
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_ShowToolTip_t.StructSize();
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00029E2E File Offset: 0x0002802E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_ShowToolTip_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00029E40 File Offset: 0x00028040
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_ShowToolTip_t data = HTML_ShowToolTip_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_ShowToolTip_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_ShowToolTip_t>(data);
			}
		}

		// Token: 0x04000716 RID: 1814
		internal const int CallbackId = 4524;

		// Token: 0x04000717 RID: 1815
		internal uint UnBrowserHandle;

		// Token: 0x04000718 RID: 1816
		internal string PchMsg;

		// Token: 0x0200022C RID: 556
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D68 RID: 7528 RVA: 0x000637B4 File Offset: 0x000619B4
			public static implicit operator HTML_ShowToolTip_t(HTML_ShowToolTip_t.PackSmall d)
			{
				return new HTML_ShowToolTip_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchMsg = d.PchMsg
				};
			}

			// Token: 0x04000B3C RID: 2876
			internal uint UnBrowserHandle;

			// Token: 0x04000B3D RID: 2877
			internal string PchMsg;
		}
	}
}
