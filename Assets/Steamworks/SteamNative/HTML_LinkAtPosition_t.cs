using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000101 RID: 257
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_LinkAtPosition_t
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x00027FBC File Offset: 0x000261BC
		internal static HTML_LinkAtPosition_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_LinkAtPosition_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_LinkAtPosition_t.PackSmall));
			}
			return (HTML_LinkAtPosition_t)Marshal.PtrToStructure(p, typeof(HTML_LinkAtPosition_t));
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00027FF5 File Offset: 0x000261F5
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_LinkAtPosition_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_LinkAtPosition_t));
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00028020 File Offset: 0x00026220
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_LinkAtPosition_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_LinkAtPosition_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_LinkAtPosition_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_LinkAtPosition_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_LinkAtPosition_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_LinkAtPosition_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_LinkAtPosition_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_LinkAtPosition_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_LinkAtPosition_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_LinkAtPosition_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_LinkAtPosition_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_LinkAtPosition_t.OnGetSize)
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
				CallbackId = 4513
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4513);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00028326 File Offset: 0x00026526
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_LinkAtPosition_t.OnResult(param);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0002832E File Offset: 0x0002652E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_LinkAtPosition_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00028338 File Offset: 0x00026538
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_LinkAtPosition_t.OnGetSize();
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0002833F File Offset: 0x0002653F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_LinkAtPosition_t.StructSize();
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00028346 File Offset: 0x00026546
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_LinkAtPosition_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00028358 File Offset: 0x00026558
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_LinkAtPosition_t data = HTML_LinkAtPosition_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_LinkAtPosition_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_LinkAtPosition_t>(data);
			}
		}

		// Token: 0x040006F7 RID: 1783
		internal const int CallbackId = 4513;

		// Token: 0x040006F8 RID: 1784
		internal uint UnBrowserHandle;

		// Token: 0x040006F9 RID: 1785
		internal uint X;

		// Token: 0x040006FA RID: 1786
		internal uint Y;

		// Token: 0x040006FB RID: 1787
		internal string PchURL;

		// Token: 0x040006FC RID: 1788
		[MarshalAs(UnmanagedType.I1)]
		internal bool BInput;

		// Token: 0x040006FD RID: 1789
		[MarshalAs(UnmanagedType.I1)]
		internal bool BLiveLink;

		// Token: 0x02000225 RID: 549
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D61 RID: 7521 RVA: 0x000635DC File Offset: 0x000617DC
			public static implicit operator HTML_LinkAtPosition_t(HTML_LinkAtPosition_t.PackSmall d)
			{
				return new HTML_LinkAtPosition_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					X = d.X,
					Y = d.Y,
					PchURL = d.PchURL,
					BInput = d.BInput,
					BLiveLink = d.BLiveLink
				};
			}

			// Token: 0x04000B24 RID: 2852
			internal uint UnBrowserHandle;

			// Token: 0x04000B25 RID: 2853
			internal uint X;

			// Token: 0x04000B26 RID: 2854
			internal uint Y;

			// Token: 0x04000B27 RID: 2855
			internal string PchURL;

			// Token: 0x04000B28 RID: 2856
			[MarshalAs(UnmanagedType.I1)]
			internal bool BInput;

			// Token: 0x04000B29 RID: 2857
			[MarshalAs(UnmanagedType.I1)]
			internal bool BLiveLink;
		}
	}
}
