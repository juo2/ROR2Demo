using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000129 RID: 297
	internal struct ScreenshotRequested_t
	{
		// Token: 0x06000968 RID: 2408 RVA: 0x000316F4 File Offset: 0x0002F8F4
		internal static ScreenshotRequested_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (ScreenshotRequested_t.PackSmall)Marshal.PtrToStructure(p, typeof(ScreenshotRequested_t.PackSmall));
			}
			return (ScreenshotRequested_t)Marshal.PtrToStructure(p, typeof(ScreenshotRequested_t));
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0003172D File Offset: 0x0002F92D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(ScreenshotRequested_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(ScreenshotRequested_t));
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00031758 File Offset: 0x0002F958
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
						ResultA = new Callback.VTableWinThis.ResultD(ScreenshotRequested_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(ScreenshotRequested_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(ScreenshotRequested_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(ScreenshotRequested_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(ScreenshotRequested_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(ScreenshotRequested_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(ScreenshotRequested_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(ScreenshotRequested_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(ScreenshotRequested_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(ScreenshotRequested_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(ScreenshotRequested_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(ScreenshotRequested_t.OnGetSize)
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
				CallbackId = 2302
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 2302);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00031A5E File Offset: 0x0002FC5E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			ScreenshotRequested_t.OnResult(param);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00031A66 File Offset: 0x0002FC66
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			ScreenshotRequested_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00031A70 File Offset: 0x0002FC70
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return ScreenshotRequested_t.OnGetSize();
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00031A77 File Offset: 0x0002FC77
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return ScreenshotRequested_t.StructSize();
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00031A7E File Offset: 0x0002FC7E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			ScreenshotRequested_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00031A90 File Offset: 0x0002FC90
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			ScreenshotRequested_t data = ScreenshotRequested_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<ScreenshotRequested_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<ScreenshotRequested_t>(data);
			}
		}

		// Token: 0x0400077B RID: 1915
		internal const int CallbackId = 2302;

		// Token: 0x0200024D RID: 589
		internal struct PackSmall
		{
			// Token: 0x06001D89 RID: 7561 RVA: 0x00063E34 File Offset: 0x00062034
			public static implicit operator ScreenshotRequested_t(ScreenshotRequested_t.PackSmall d)
			{
				return default(ScreenshotRequested_t);
			}
		}
	}
}
