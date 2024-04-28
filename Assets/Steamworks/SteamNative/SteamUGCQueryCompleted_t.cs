using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000E3 RID: 227
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamUGCQueryCompleted_t
	{
		// Token: 0x060006F7 RID: 1783 RVA: 0x000214F4 File Offset: 0x0001F6F4
		internal static SteamUGCQueryCompleted_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamUGCQueryCompleted_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamUGCQueryCompleted_t.PackSmall));
			}
			return (SteamUGCQueryCompleted_t)Marshal.PtrToStructure(p, typeof(SteamUGCQueryCompleted_t));
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0002152D File Offset: 0x0001F72D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamUGCQueryCompleted_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamUGCQueryCompleted_t));
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00021555 File Offset: 0x0001F755
		internal static CallResult<SteamUGCQueryCompleted_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<SteamUGCQueryCompleted_t, bool> CallbackFunction)
		{
			return new CallResult<SteamUGCQueryCompleted_t>(steamworks, call, CallbackFunction, new CallResult<SteamUGCQueryCompleted_t>.ConvertFromPointer(SteamUGCQueryCompleted_t.FromPointer), SteamUGCQueryCompleted_t.StructSize(), 3401);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00021578 File Offset: 0x0001F778
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
						ResultA = new Callback.VTableWinThis.ResultD(SteamUGCQueryCompleted_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SteamUGCQueryCompleted_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SteamUGCQueryCompleted_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SteamUGCQueryCompleted_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SteamUGCQueryCompleted_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SteamUGCQueryCompleted_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SteamUGCQueryCompleted_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SteamUGCQueryCompleted_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SteamUGCQueryCompleted_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SteamUGCQueryCompleted_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SteamUGCQueryCompleted_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SteamUGCQueryCompleted_t.OnGetSize)
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
				CallbackId = 3401
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3401);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0002187E File Offset: 0x0001FA7E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SteamUGCQueryCompleted_t.OnResult(param);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00021886 File Offset: 0x0001FA86
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SteamUGCQueryCompleted_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00021890 File Offset: 0x0001FA90
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SteamUGCQueryCompleted_t.OnGetSize();
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00021897 File Offset: 0x0001FA97
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SteamUGCQueryCompleted_t.StructSize();
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0002189E File Offset: 0x0001FA9E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SteamUGCQueryCompleted_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000218B0 File Offset: 0x0001FAB0
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SteamUGCQueryCompleted_t data = SteamUGCQueryCompleted_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SteamUGCQueryCompleted_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SteamUGCQueryCompleted_t>(data);
			}
		}

		// Token: 0x04000678 RID: 1656
		internal const int CallbackId = 3401;

		// Token: 0x04000679 RID: 1657
		internal ulong Handle;

		// Token: 0x0400067A RID: 1658
		internal Result Result;

		// Token: 0x0400067B RID: 1659
		internal uint NumResultsReturned;

		// Token: 0x0400067C RID: 1660
		internal uint TotalMatchingResults;

		// Token: 0x0400067D RID: 1661
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData;

		// Token: 0x02000207 RID: 519
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D43 RID: 7491 RVA: 0x00062E04 File Offset: 0x00061004
			public static implicit operator SteamUGCQueryCompleted_t(SteamUGCQueryCompleted_t.PackSmall d)
			{
				return new SteamUGCQueryCompleted_t
				{
					Handle = d.Handle,
					Result = d.Result,
					NumResultsReturned = d.NumResultsReturned,
					TotalMatchingResults = d.TotalMatchingResults,
					CachedData = d.CachedData
				};
			}

			// Token: 0x04000AC0 RID: 2752
			internal ulong Handle;

			// Token: 0x04000AC1 RID: 2753
			internal Result Result;

			// Token: 0x04000AC2 RID: 2754
			internal uint NumResultsReturned;

			// Token: 0x04000AC3 RID: 2755
			internal uint TotalMatchingResults;

			// Token: 0x04000AC4 RID: 2756
			[MarshalAs(UnmanagedType.I1)]
			internal bool CachedData;
		}
	}
}
