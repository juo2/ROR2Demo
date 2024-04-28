using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000110 RID: 272
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamInventoryStartPurchaseResult_t
	{
		// Token: 0x06000880 RID: 2176 RVA: 0x0002B60C File Offset: 0x0002980C
		internal static SteamInventoryStartPurchaseResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamInventoryStartPurchaseResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamInventoryStartPurchaseResult_t.PackSmall));
			}
			return (SteamInventoryStartPurchaseResult_t)Marshal.PtrToStructure(p, typeof(SteamInventoryStartPurchaseResult_t));
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0002B645 File Offset: 0x00029845
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamInventoryStartPurchaseResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamInventoryStartPurchaseResult_t));
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0002B66D File Offset: 0x0002986D
		internal static CallResult<SteamInventoryStartPurchaseResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<SteamInventoryStartPurchaseResult_t, bool> CallbackFunction)
		{
			return new CallResult<SteamInventoryStartPurchaseResult_t>(steamworks, call, CallbackFunction, new CallResult<SteamInventoryStartPurchaseResult_t>.ConvertFromPointer(SteamInventoryStartPurchaseResult_t.FromPointer), SteamInventoryStartPurchaseResult_t.StructSize(), 4704);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0002B690 File Offset: 0x00029890
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
						ResultA = new Callback.VTableWinThis.ResultD(SteamInventoryStartPurchaseResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SteamInventoryStartPurchaseResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SteamInventoryStartPurchaseResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SteamInventoryStartPurchaseResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SteamInventoryStartPurchaseResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SteamInventoryStartPurchaseResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SteamInventoryStartPurchaseResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SteamInventoryStartPurchaseResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SteamInventoryStartPurchaseResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SteamInventoryStartPurchaseResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SteamInventoryStartPurchaseResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SteamInventoryStartPurchaseResult_t.OnGetSize)
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
				CallbackId = 4704
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4704);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0002B996 File Offset: 0x00029B96
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SteamInventoryStartPurchaseResult_t.OnResult(param);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0002B99E File Offset: 0x00029B9E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SteamInventoryStartPurchaseResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0002B9A8 File Offset: 0x00029BA8
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SteamInventoryStartPurchaseResult_t.OnGetSize();
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0002B9AF File Offset: 0x00029BAF
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SteamInventoryStartPurchaseResult_t.StructSize();
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0002B9B6 File Offset: 0x00029BB6
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SteamInventoryStartPurchaseResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0002B9C8 File Offset: 0x00029BC8
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SteamInventoryStartPurchaseResult_t data = SteamInventoryStartPurchaseResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SteamInventoryStartPurchaseResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SteamInventoryStartPurchaseResult_t>(data);
			}
		}

		// Token: 0x0400072F RID: 1839
		internal const int CallbackId = 4704;

		// Token: 0x04000730 RID: 1840
		internal Result Result;

		// Token: 0x04000731 RID: 1841
		internal ulong OrderID;

		// Token: 0x04000732 RID: 1842
		internal ulong TransID;

		// Token: 0x02000234 RID: 564
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D70 RID: 7536 RVA: 0x00063954 File Offset: 0x00061B54
			public static implicit operator SteamInventoryStartPurchaseResult_t(SteamInventoryStartPurchaseResult_t.PackSmall d)
			{
				return new SteamInventoryStartPurchaseResult_t
				{
					Result = d.Result,
					OrderID = d.OrderID,
					TransID = d.TransID
				};
			}

			// Token: 0x04000B4E RID: 2894
			internal Result Result;

			// Token: 0x04000B4F RID: 2895
			internal ulong OrderID;

			// Token: 0x04000B50 RID: 2896
			internal ulong TransID;
		}
	}
}
