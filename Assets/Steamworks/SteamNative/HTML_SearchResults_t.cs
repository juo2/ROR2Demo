using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000FD RID: 253
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_SearchResults_t
	{
		// Token: 0x060007DB RID: 2011 RVA: 0x0002705C File Offset: 0x0002525C
		internal static HTML_SearchResults_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_SearchResults_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_SearchResults_t.PackSmall));
			}
			return (HTML_SearchResults_t)Marshal.PtrToStructure(p, typeof(HTML_SearchResults_t));
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00027095 File Offset: 0x00025295
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_SearchResults_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_SearchResults_t));
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x000270C0 File Offset: 0x000252C0
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_SearchResults_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_SearchResults_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_SearchResults_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_SearchResults_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_SearchResults_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_SearchResults_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_SearchResults_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_SearchResults_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_SearchResults_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_SearchResults_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_SearchResults_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_SearchResults_t.OnGetSize)
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
				CallbackId = 4509
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4509);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x000273C6 File Offset: 0x000255C6
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_SearchResults_t.OnResult(param);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x000273CE File Offset: 0x000255CE
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_SearchResults_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000273D8 File Offset: 0x000255D8
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_SearchResults_t.OnGetSize();
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000273DF File Offset: 0x000255DF
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_SearchResults_t.StructSize();
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x000273E6 File Offset: 0x000255E6
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_SearchResults_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x000273F8 File Offset: 0x000255F8
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_SearchResults_t data = HTML_SearchResults_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_SearchResults_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_SearchResults_t>(data);
			}
		}

		// Token: 0x040006E1 RID: 1761
		internal const int CallbackId = 4509;

		// Token: 0x040006E2 RID: 1762
		internal uint UnBrowserHandle;

		// Token: 0x040006E3 RID: 1763
		internal uint UnResults;

		// Token: 0x040006E4 RID: 1764
		internal uint UnCurrentMatch;

		// Token: 0x02000221 RID: 545
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D5D RID: 7517 RVA: 0x00063494 File Offset: 0x00061694
			public static implicit operator HTML_SearchResults_t(HTML_SearchResults_t.PackSmall d)
			{
				return new HTML_SearchResults_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					UnResults = d.UnResults,
					UnCurrentMatch = d.UnCurrentMatch
				};
			}

			// Token: 0x04000B12 RID: 2834
			internal uint UnBrowserHandle;

			// Token: 0x04000B13 RID: 2835
			internal uint UnResults;

			// Token: 0x04000B14 RID: 2836
			internal uint UnCurrentMatch;
		}
	}
}
