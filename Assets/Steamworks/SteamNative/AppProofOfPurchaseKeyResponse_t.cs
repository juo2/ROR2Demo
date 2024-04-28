using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000CE RID: 206
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AppProofOfPurchaseKeyResponse_t
	{
		// Token: 0x0600065C RID: 1628 RVA: 0x0001D570 File Offset: 0x0001B770
		internal static AppProofOfPurchaseKeyResponse_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (AppProofOfPurchaseKeyResponse_t.PackSmall)Marshal.PtrToStructure(p, typeof(AppProofOfPurchaseKeyResponse_t.PackSmall));
			}
			return (AppProofOfPurchaseKeyResponse_t)Marshal.PtrToStructure(p, typeof(AppProofOfPurchaseKeyResponse_t));
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001D5A9 File Offset: 0x0001B7A9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(AppProofOfPurchaseKeyResponse_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(AppProofOfPurchaseKeyResponse_t));
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001D5D4 File Offset: 0x0001B7D4
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
						ResultA = new Callback.VTableWinThis.ResultD(AppProofOfPurchaseKeyResponse_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(AppProofOfPurchaseKeyResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(AppProofOfPurchaseKeyResponse_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(AppProofOfPurchaseKeyResponse_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(AppProofOfPurchaseKeyResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(AppProofOfPurchaseKeyResponse_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(AppProofOfPurchaseKeyResponse_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(AppProofOfPurchaseKeyResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(AppProofOfPurchaseKeyResponse_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(AppProofOfPurchaseKeyResponse_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(AppProofOfPurchaseKeyResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(AppProofOfPurchaseKeyResponse_t.OnGetSize)
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
				CallbackId = 1021
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1021);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001D8DA File Offset: 0x0001BADA
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			AppProofOfPurchaseKeyResponse_t.OnResult(param);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001D8E2 File Offset: 0x0001BAE2
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			AppProofOfPurchaseKeyResponse_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001D8EC File Offset: 0x0001BAEC
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return AppProofOfPurchaseKeyResponse_t.OnGetSize();
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001D8F3 File Offset: 0x0001BAF3
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return AppProofOfPurchaseKeyResponse_t.StructSize();
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001D8FA File Offset: 0x0001BAFA
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			AppProofOfPurchaseKeyResponse_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001D90C File Offset: 0x0001BB0C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			AppProofOfPurchaseKeyResponse_t data = AppProofOfPurchaseKeyResponse_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<AppProofOfPurchaseKeyResponse_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<AppProofOfPurchaseKeyResponse_t>(data);
			}
		}

		// Token: 0x04000613 RID: 1555
		internal const int CallbackId = 1021;

		// Token: 0x04000614 RID: 1556
		internal Result Result;

		// Token: 0x04000615 RID: 1557
		internal uint AppID;

		// Token: 0x04000616 RID: 1558
		internal uint CchKeyLength;

		// Token: 0x04000617 RID: 1559
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 240)]
		internal string Key;

		// Token: 0x020001F2 RID: 498
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D2E RID: 7470 RVA: 0x000627D0 File Offset: 0x000609D0
			public static implicit operator AppProofOfPurchaseKeyResponse_t(AppProofOfPurchaseKeyResponse_t.PackSmall d)
			{
				return new AppProofOfPurchaseKeyResponse_t
				{
					Result = d.Result,
					AppID = d.AppID,
					CchKeyLength = d.CchKeyLength,
					Key = d.Key
				};
			}

			// Token: 0x04000A6B RID: 2667
			internal Result Result;

			// Token: 0x04000A6C RID: 2668
			internal uint AppID;

			// Token: 0x04000A6D RID: 2669
			internal uint CchKeyLength;

			// Token: 0x04000A6E RID: 2670
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 240)]
			internal string Key;
		}
	}
}
