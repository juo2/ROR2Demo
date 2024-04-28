using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000D4 RID: 212
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ScreenshotReady_t
	{
		// Token: 0x0600068C RID: 1676 RVA: 0x0001E928 File Offset: 0x0001CB28
		internal static ScreenshotReady_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (ScreenshotReady_t.PackSmall)Marshal.PtrToStructure(p, typeof(ScreenshotReady_t.PackSmall));
			}
			return (ScreenshotReady_t)Marshal.PtrToStructure(p, typeof(ScreenshotReady_t));
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001E961 File Offset: 0x0001CB61
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(ScreenshotReady_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(ScreenshotReady_t));
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001E98C File Offset: 0x0001CB8C
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
						ResultA = new Callback.VTableWinThis.ResultD(ScreenshotReady_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(ScreenshotReady_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(ScreenshotReady_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(ScreenshotReady_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(ScreenshotReady_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(ScreenshotReady_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(ScreenshotReady_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(ScreenshotReady_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(ScreenshotReady_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(ScreenshotReady_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(ScreenshotReady_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(ScreenshotReady_t.OnGetSize)
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
				CallbackId = 2301
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 2301);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001EC92 File Offset: 0x0001CE92
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			ScreenshotReady_t.OnResult(param);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001EC9A File Offset: 0x0001CE9A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			ScreenshotReady_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001ECA4 File Offset: 0x0001CEA4
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return ScreenshotReady_t.OnGetSize();
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001ECAB File Offset: 0x0001CEAB
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return ScreenshotReady_t.StructSize();
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001ECB2 File Offset: 0x0001CEB2
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			ScreenshotReady_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001ECC4 File Offset: 0x0001CEC4
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			ScreenshotReady_t data = ScreenshotReady_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<ScreenshotReady_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<ScreenshotReady_t>(data);
			}
		}

		// Token: 0x0400062F RID: 1583
		internal const int CallbackId = 2301;

		// Token: 0x04000630 RID: 1584
		internal uint Local;

		// Token: 0x04000631 RID: 1585
		internal Result Result;

		// Token: 0x020001F8 RID: 504
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D34 RID: 7476 RVA: 0x00062988 File Offset: 0x00060B88
			public static implicit operator ScreenshotReady_t(ScreenshotReady_t.PackSmall d)
			{
				return new ScreenshotReady_t
				{
					Local = d.Local,
					Result = d.Result
				};
			}

			// Token: 0x04000A82 RID: 2690
			internal uint Local;

			// Token: 0x04000A83 RID: 2691
			internal Result Result;
		}
	}
}
