using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000F1 RID: 241
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetAppDependenciesResult_t
	{
		// Token: 0x06000781 RID: 1921 RVA: 0x00024C44 File Offset: 0x00022E44
		internal static GetAppDependenciesResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GetAppDependenciesResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(GetAppDependenciesResult_t.PackSmall));
			}
			return (GetAppDependenciesResult_t)Marshal.PtrToStructure(p, typeof(GetAppDependenciesResult_t));
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00024C7D File Offset: 0x00022E7D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GetAppDependenciesResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GetAppDependenciesResult_t));
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00024CA5 File Offset: 0x00022EA5
		internal static CallResult<GetAppDependenciesResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<GetAppDependenciesResult_t, bool> CallbackFunction)
		{
			return new CallResult<GetAppDependenciesResult_t>(steamworks, call, CallbackFunction, new CallResult<GetAppDependenciesResult_t>.ConvertFromPointer(GetAppDependenciesResult_t.FromPointer), GetAppDependenciesResult_t.StructSize(), 3416);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00024CC8 File Offset: 0x00022EC8
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
						ResultA = new Callback.VTableWinThis.ResultD(GetAppDependenciesResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GetAppDependenciesResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GetAppDependenciesResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GetAppDependenciesResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GetAppDependenciesResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GetAppDependenciesResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GetAppDependenciesResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GetAppDependenciesResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GetAppDependenciesResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GetAppDependenciesResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GetAppDependenciesResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GetAppDependenciesResult_t.OnGetSize)
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
				CallbackId = 3416
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3416);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00024FCE File Offset: 0x000231CE
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GetAppDependenciesResult_t.OnResult(param);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00024FD6 File Offset: 0x000231D6
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GetAppDependenciesResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00024FE0 File Offset: 0x000231E0
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GetAppDependenciesResult_t.OnGetSize();
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00024FE7 File Offset: 0x000231E7
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GetAppDependenciesResult_t.StructSize();
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00024FEE File Offset: 0x000231EE
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GetAppDependenciesResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00025000 File Offset: 0x00023200
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GetAppDependenciesResult_t data = GetAppDependenciesResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GetAppDependenciesResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GetAppDependenciesResult_t>(data);
			}
		}

		// Token: 0x040006AF RID: 1711
		internal const int CallbackId = 3416;

		// Token: 0x040006B0 RID: 1712
		internal Result Result;

		// Token: 0x040006B1 RID: 1713
		internal ulong PublishedFileId;

		// Token: 0x040006B2 RID: 1714
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.U4)]
		internal AppId_t[] GAppIDs;

		// Token: 0x040006B3 RID: 1715
		internal uint NumAppDependencies;

		// Token: 0x040006B4 RID: 1716
		internal uint TotalNumAppDependencies;

		// Token: 0x02000215 RID: 533
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D51 RID: 7505 RVA: 0x0006316C File Offset: 0x0006136C
			public static implicit operator GetAppDependenciesResult_t(GetAppDependenciesResult_t.PackSmall d)
			{
				return new GetAppDependenciesResult_t
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					GAppIDs = d.GAppIDs,
					NumAppDependencies = d.NumAppDependencies,
					TotalNumAppDependencies = d.TotalNumAppDependencies
				};
			}

			// Token: 0x04000AE9 RID: 2793
			internal Result Result;

			// Token: 0x04000AEA RID: 2794
			internal ulong PublishedFileId;

			// Token: 0x04000AEB RID: 2795
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.U4)]
			internal AppId_t[] GAppIDs;

			// Token: 0x04000AEC RID: 2796
			internal uint NumAppDependencies;

			// Token: 0x04000AED RID: 2797
			internal uint TotalNumAppDependencies;
		}
	}
}
