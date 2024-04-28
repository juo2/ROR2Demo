using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200010F RID: 271
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct SteamInventoryEligiblePromoItemDefIDs_t
	{
		// Token: 0x06000876 RID: 2166 RVA: 0x0002B214 File Offset: 0x00029414
		internal static SteamInventoryEligiblePromoItemDefIDs_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamInventoryEligiblePromoItemDefIDs_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamInventoryEligiblePromoItemDefIDs_t.PackSmall));
			}
			return (SteamInventoryEligiblePromoItemDefIDs_t)Marshal.PtrToStructure(p, typeof(SteamInventoryEligiblePromoItemDefIDs_t));
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0002B24D File Offset: 0x0002944D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamInventoryEligiblePromoItemDefIDs_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamInventoryEligiblePromoItemDefIDs_t));
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0002B275 File Offset: 0x00029475
		internal static CallResult<SteamInventoryEligiblePromoItemDefIDs_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<SteamInventoryEligiblePromoItemDefIDs_t, bool> CallbackFunction)
		{
			return new CallResult<SteamInventoryEligiblePromoItemDefIDs_t>(steamworks, call, CallbackFunction, new CallResult<SteamInventoryEligiblePromoItemDefIDs_t>.ConvertFromPointer(SteamInventoryEligiblePromoItemDefIDs_t.FromPointer), SteamInventoryEligiblePromoItemDefIDs_t.StructSize(), 4703);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0002B298 File Offset: 0x00029498
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
						ResultA = new Callback.VTableWinThis.ResultD(SteamInventoryEligiblePromoItemDefIDs_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SteamInventoryEligiblePromoItemDefIDs_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SteamInventoryEligiblePromoItemDefIDs_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SteamInventoryEligiblePromoItemDefIDs_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SteamInventoryEligiblePromoItemDefIDs_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SteamInventoryEligiblePromoItemDefIDs_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SteamInventoryEligiblePromoItemDefIDs_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SteamInventoryEligiblePromoItemDefIDs_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SteamInventoryEligiblePromoItemDefIDs_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SteamInventoryEligiblePromoItemDefIDs_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SteamInventoryEligiblePromoItemDefIDs_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SteamInventoryEligiblePromoItemDefIDs_t.OnGetSize)
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
				CallbackId = 4703
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4703);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0002B59E File Offset: 0x0002979E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SteamInventoryEligiblePromoItemDefIDs_t.OnResult(param);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0002B5A6 File Offset: 0x000297A6
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SteamInventoryEligiblePromoItemDefIDs_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0002B5B0 File Offset: 0x000297B0
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SteamInventoryEligiblePromoItemDefIDs_t.OnGetSize();
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0002B5B7 File Offset: 0x000297B7
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SteamInventoryEligiblePromoItemDefIDs_t.StructSize();
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0002B5BE File Offset: 0x000297BE
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SteamInventoryEligiblePromoItemDefIDs_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0002B5D0 File Offset: 0x000297D0
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SteamInventoryEligiblePromoItemDefIDs_t data = SteamInventoryEligiblePromoItemDefIDs_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SteamInventoryEligiblePromoItemDefIDs_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SteamInventoryEligiblePromoItemDefIDs_t>(data);
			}
		}

		// Token: 0x0400072A RID: 1834
		internal const int CallbackId = 4703;

		// Token: 0x0400072B RID: 1835
		internal Result Result;

		// Token: 0x0400072C RID: 1836
		internal ulong SteamID;

		// Token: 0x0400072D RID: 1837
		internal int UmEligiblePromoItemDefs;

		// Token: 0x0400072E RID: 1838
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData;

		// Token: 0x02000233 RID: 563
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D6F RID: 7535 RVA: 0x00063908 File Offset: 0x00061B08
			public static implicit operator SteamInventoryEligiblePromoItemDefIDs_t(SteamInventoryEligiblePromoItemDefIDs_t.PackSmall d)
			{
				return new SteamInventoryEligiblePromoItemDefIDs_t
				{
					Result = d.Result,
					SteamID = d.SteamID,
					UmEligiblePromoItemDefs = d.UmEligiblePromoItemDefs,
					CachedData = d.CachedData
				};
			}

			// Token: 0x04000B4A RID: 2890
			internal Result Result;

			// Token: 0x04000B4B RID: 2891
			internal ulong SteamID;

			// Token: 0x04000B4C RID: 2892
			internal int UmEligiblePromoItemDefs;

			// Token: 0x04000B4D RID: 2893
			[MarshalAs(UnmanagedType.I1)]
			internal bool CachedData;
		}
	}
}
