using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200008C RID: 140
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct FriendsEnumerateFollowingList_t
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x0000E538 File Offset: 0x0000C738
		internal static FriendsEnumerateFollowingList_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (FriendsEnumerateFollowingList_t.PackSmall)Marshal.PtrToStructure(p, typeof(FriendsEnumerateFollowingList_t.PackSmall));
			}
			return (FriendsEnumerateFollowingList_t)Marshal.PtrToStructure(p, typeof(FriendsEnumerateFollowingList_t));
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000E571 File Offset: 0x0000C771
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(FriendsEnumerateFollowingList_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(FriendsEnumerateFollowingList_t));
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000E599 File Offset: 0x0000C799
		internal static CallResult<FriendsEnumerateFollowingList_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<FriendsEnumerateFollowingList_t, bool> CallbackFunction)
		{
			return new CallResult<FriendsEnumerateFollowingList_t>(steamworks, call, CallbackFunction, new CallResult<FriendsEnumerateFollowingList_t>.ConvertFromPointer(FriendsEnumerateFollowingList_t.FromPointer), FriendsEnumerateFollowingList_t.StructSize(), 346);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000E5BC File Offset: 0x0000C7BC
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
						ResultA = new Callback.VTableWinThis.ResultD(FriendsEnumerateFollowingList_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(FriendsEnumerateFollowingList_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(FriendsEnumerateFollowingList_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(FriendsEnumerateFollowingList_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(FriendsEnumerateFollowingList_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(FriendsEnumerateFollowingList_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(FriendsEnumerateFollowingList_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(FriendsEnumerateFollowingList_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(FriendsEnumerateFollowingList_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(FriendsEnumerateFollowingList_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(FriendsEnumerateFollowingList_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(FriendsEnumerateFollowingList_t.OnGetSize)
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
				CallbackId = 346
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 346);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000E8C2 File Offset: 0x0000CAC2
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			FriendsEnumerateFollowingList_t.OnResult(param);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000E8CA File Offset: 0x0000CACA
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			FriendsEnumerateFollowingList_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000E8D4 File Offset: 0x0000CAD4
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return FriendsEnumerateFollowingList_t.OnGetSize();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000E8DB File Offset: 0x0000CADB
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return FriendsEnumerateFollowingList_t.StructSize();
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000E8E2 File Offset: 0x0000CAE2
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			FriendsEnumerateFollowingList_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000E8F4 File Offset: 0x0000CAF4
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			FriendsEnumerateFollowingList_t data = FriendsEnumerateFollowingList_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<FriendsEnumerateFollowingList_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<FriendsEnumerateFollowingList_t>(data);
			}
		}

		// Token: 0x040004ED RID: 1261
		internal const int CallbackId = 346;

		// Token: 0x040004EE RID: 1262
		internal Result Result;

		// Token: 0x040004EF RID: 1263
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal ulong[] GSteamID;

		// Token: 0x040004F0 RID: 1264
		internal int ResultsReturned;

		// Token: 0x040004F1 RID: 1265
		internal int TotalResultCount;

		// Token: 0x020001B0 RID: 432
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CEC RID: 7404 RVA: 0x000615F8 File Offset: 0x0005F7F8
			public static implicit operator FriendsEnumerateFollowingList_t(FriendsEnumerateFollowingList_t.PackSmall d)
			{
				return new FriendsEnumerateFollowingList_t
				{
					Result = d.Result,
					GSteamID = d.GSteamID,
					ResultsReturned = d.ResultsReturned,
					TotalResultCount = d.TotalResultCount
				};
			}

			// Token: 0x04000982 RID: 2434
			internal Result Result;

			// Token: 0x04000983 RID: 2435
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GSteamID;

			// Token: 0x04000984 RID: 2436
			internal int ResultsReturned;

			// Token: 0x04000985 RID: 2437
			internal int TotalResultCount;
		}
	}
}
