using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000111 RID: 273
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamInventoryRequestPricesResult_t
	{
		// Token: 0x0600088A RID: 2186 RVA: 0x0002BA04 File Offset: 0x00029C04
		internal static SteamInventoryRequestPricesResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamInventoryRequestPricesResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamInventoryRequestPricesResult_t.PackSmall));
			}
			return (SteamInventoryRequestPricesResult_t)Marshal.PtrToStructure(p, typeof(SteamInventoryRequestPricesResult_t));
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0002BA3D File Offset: 0x00029C3D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamInventoryRequestPricesResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamInventoryRequestPricesResult_t));
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0002BA65 File Offset: 0x00029C65
		internal static CallResult<SteamInventoryRequestPricesResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<SteamInventoryRequestPricesResult_t, bool> CallbackFunction)
		{
			return new CallResult<SteamInventoryRequestPricesResult_t>(steamworks, call, CallbackFunction, new CallResult<SteamInventoryRequestPricesResult_t>.ConvertFromPointer(SteamInventoryRequestPricesResult_t.FromPointer), SteamInventoryRequestPricesResult_t.StructSize(), 4705);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0002BA88 File Offset: 0x00029C88
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
						ResultA = new Callback.VTableWinThis.ResultD(SteamInventoryRequestPricesResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SteamInventoryRequestPricesResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SteamInventoryRequestPricesResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SteamInventoryRequestPricesResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SteamInventoryRequestPricesResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SteamInventoryRequestPricesResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SteamInventoryRequestPricesResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SteamInventoryRequestPricesResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SteamInventoryRequestPricesResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SteamInventoryRequestPricesResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SteamInventoryRequestPricesResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SteamInventoryRequestPricesResult_t.OnGetSize)
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
				CallbackId = 4705
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4705);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0002BD8E File Offset: 0x00029F8E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SteamInventoryRequestPricesResult_t.OnResult(param);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0002BD96 File Offset: 0x00029F96
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SteamInventoryRequestPricesResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0002BDA0 File Offset: 0x00029FA0
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SteamInventoryRequestPricesResult_t.OnGetSize();
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0002BDA7 File Offset: 0x00029FA7
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SteamInventoryRequestPricesResult_t.StructSize();
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0002BDAE File Offset: 0x00029FAE
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SteamInventoryRequestPricesResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0002BDC0 File Offset: 0x00029FC0
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SteamInventoryRequestPricesResult_t data = SteamInventoryRequestPricesResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SteamInventoryRequestPricesResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SteamInventoryRequestPricesResult_t>(data);
			}
		}

		// Token: 0x04000733 RID: 1843
		internal const int CallbackId = 4705;

		// Token: 0x04000734 RID: 1844
		internal Result Result;

		// Token: 0x04000735 RID: 1845
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
		internal string Currency;

		// Token: 0x02000235 RID: 565
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D71 RID: 7537 RVA: 0x00063994 File Offset: 0x00061B94
			public static implicit operator SteamInventoryRequestPricesResult_t(SteamInventoryRequestPricesResult_t.PackSmall d)
			{
				return new SteamInventoryRequestPricesResult_t
				{
					Result = d.Result,
					Currency = d.Currency
				};
			}

			// Token: 0x04000B51 RID: 2897
			internal Result Result;

			// Token: 0x04000B52 RID: 2898
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
			internal string Currency;
		}
	}
}
