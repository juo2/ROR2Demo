using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000104 RID: 260
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_FileOpenDialog_t
	{
		// Token: 0x0600081A RID: 2074 RVA: 0x00028B44 File Offset: 0x00026D44
		internal static HTML_FileOpenDialog_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTML_FileOpenDialog_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTML_FileOpenDialog_t.PackSmall));
			}
			return (HTML_FileOpenDialog_t)Marshal.PtrToStructure(p, typeof(HTML_FileOpenDialog_t));
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00028B7D File Offset: 0x00026D7D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTML_FileOpenDialog_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTML_FileOpenDialog_t));
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00028BA8 File Offset: 0x00026DA8
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
						ResultA = new Callback.VTableWinThis.ResultD(HTML_FileOpenDialog_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTML_FileOpenDialog_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTML_FileOpenDialog_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTML_FileOpenDialog_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTML_FileOpenDialog_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTML_FileOpenDialog_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTML_FileOpenDialog_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTML_FileOpenDialog_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTML_FileOpenDialog_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTML_FileOpenDialog_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTML_FileOpenDialog_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTML_FileOpenDialog_t.OnGetSize)
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
				CallbackId = 4516
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4516);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00028EAE File Offset: 0x000270AE
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTML_FileOpenDialog_t.OnResult(param);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00028EB6 File Offset: 0x000270B6
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTML_FileOpenDialog_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00028EC0 File Offset: 0x000270C0
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTML_FileOpenDialog_t.OnGetSize();
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00028EC7 File Offset: 0x000270C7
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTML_FileOpenDialog_t.StructSize();
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00028ECE File Offset: 0x000270CE
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTML_FileOpenDialog_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00028EE0 File Offset: 0x000270E0
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTML_FileOpenDialog_t data = HTML_FileOpenDialog_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTML_FileOpenDialog_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTML_FileOpenDialog_t>(data);
			}
		}

		// Token: 0x04000704 RID: 1796
		internal const int CallbackId = 4516;

		// Token: 0x04000705 RID: 1797
		internal uint UnBrowserHandle;

		// Token: 0x04000706 RID: 1798
		internal string PchTitle;

		// Token: 0x04000707 RID: 1799
		internal string PchInitialFile;

		// Token: 0x02000228 RID: 552
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D64 RID: 7524 RVA: 0x000636A0 File Offset: 0x000618A0
			public static implicit operator HTML_FileOpenDialog_t(HTML_FileOpenDialog_t.PackSmall d)
			{
				return new HTML_FileOpenDialog_t
				{
					UnBrowserHandle = d.UnBrowserHandle,
					PchTitle = d.PchTitle,
					PchInitialFile = d.PchInitialFile
				};
			}

			// Token: 0x04000B2E RID: 2862
			internal uint UnBrowserHandle;

			// Token: 0x04000B2F RID: 2863
			internal string PchTitle;

			// Token: 0x04000B30 RID: 2864
			internal string PchInitialFile;
		}
	}
}
