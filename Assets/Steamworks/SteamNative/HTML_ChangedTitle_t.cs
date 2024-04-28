using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000FC RID: 252
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_ChangedTitle_t
	{
		// Token: 0x060007D2 RID: 2002 RVA: 0x00026C84 File Offset: 0x00024E84
		internal static HTML_ChangedTitle_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_ChangedTitle_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_ChangedTitle_t.PackSmall));
			}
			return (HTML_ChangedTitle_t)Marshal.PtrToStructure(p, typeof(HTML_ChangedTitle_t));
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00026CBD File Offset: 0x00024EBD
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_ChangedTitle_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_ChangedTitle_t));
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00026CE8 File Offset: 0x00024EE8
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_ChangedTitle_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_ChangedTitle_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_ChangedTitle_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_ChangedTitle_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_ChangedTitle_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_ChangedTitle_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_ChangedTitle_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_ChangedTitle_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_ChangedTitle_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_ChangedTitle_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_ChangedTitle_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_ChangedTitle_t.OnGetSize)
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
				CallbackId = 4508
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4508);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00026FEE File Offset: 0x000251EE
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_ChangedTitle_t.OnResult(param);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00026FF6 File Offset: 0x000251F6
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_ChangedTitle_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00027000 File Offset: 0x00025200
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_ChangedTitle_t.OnGetSize();
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00027007 File Offset: 0x00025207
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_ChangedTitle_t.StructSize();
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0002700E File Offset: 0x0002520E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_ChangedTitle_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00027020 File Offset: 0x00025220
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_ChangedTitle_t data = HTML_ChangedTitle_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_ChangedTitle_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_ChangedTitle_t>(data);
			}
		}

		// Token: 0x040006DE RID: 1758
		internal const int CallbackId = 4508;

		// Token: 0x040006DF RID: 1759
		internal uint UnBrowserHandle;

		// Token: 0x040006E0 RID: 1760
		internal string PchTitle;

		// Token: 0x02000220 RID: 544
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D5C RID: 7516 RVA: 0x00063464 File Offset: 0x00061664
			public static implicit operator HTML_ChangedTitle_t(HTML_ChangedTitle_t.PackSmall d)
			{
				return new HTML_ChangedTitle_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchTitle = d.PchTitle
				};
			}

			// Token: 0x04000B10 RID: 2832
			internal uint UnBrowserHandle;

			// Token: 0x04000B11 RID: 2833
			internal string PchTitle;
		}
	}
}
