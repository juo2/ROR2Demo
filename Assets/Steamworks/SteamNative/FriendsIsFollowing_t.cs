using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200008B RID: 139
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct FriendsIsFollowing_t
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0000E140 File Offset: 0x0000C340
		internal static FriendsIsFollowing_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (FriendsIsFollowing_t.PackSmall)Marshal.PtrToStructure(p, typeof(FriendsIsFollowing_t.PackSmall));
			}
			return (FriendsIsFollowing_t)Marshal.PtrToStructure(p, typeof(FriendsIsFollowing_t));
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000E179 File Offset: 0x0000C379
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(FriendsIsFollowing_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(FriendsIsFollowing_t));
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000E1A1 File Offset: 0x0000C3A1
		internal static CallResult<FriendsIsFollowing_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<FriendsIsFollowing_t, bool> CallbackFunction)
		{
			return new CallResult<FriendsIsFollowing_t>(steamworks, call, CallbackFunction, new CallResult<FriendsIsFollowing_t>.ConvertFromPointer(FriendsIsFollowing_t.FromPointer), FriendsIsFollowing_t.StructSize(), 345);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
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
						ResultA = new Callback.VTableWinThis.ResultD(FriendsIsFollowing_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(FriendsIsFollowing_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(FriendsIsFollowing_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(FriendsIsFollowing_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(FriendsIsFollowing_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(FriendsIsFollowing_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(FriendsIsFollowing_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(FriendsIsFollowing_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(FriendsIsFollowing_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(FriendsIsFollowing_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(FriendsIsFollowing_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(FriendsIsFollowing_t.OnGetSize)
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
				CallbackId = 345
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 345);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000E4CA File Offset: 0x0000C6CA
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			FriendsIsFollowing_t.OnResult(param);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000E4D2 File Offset: 0x0000C6D2
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			FriendsIsFollowing_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000E4DC File Offset: 0x0000C6DC
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return FriendsIsFollowing_t.OnGetSize();
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000E4E3 File Offset: 0x0000C6E3
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return FriendsIsFollowing_t.StructSize();
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000E4EA File Offset: 0x0000C6EA
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			FriendsIsFollowing_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000E4FC File Offset: 0x0000C6FC
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			FriendsIsFollowing_t data = FriendsIsFollowing_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<FriendsIsFollowing_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<FriendsIsFollowing_t>(data);
			}
		}

		// Token: 0x040004E9 RID: 1257
		internal const int CallbackId = 345;

		// Token: 0x040004EA RID: 1258
		internal Result Result;

		// Token: 0x040004EB RID: 1259
		internal ulong SteamID;

		// Token: 0x040004EC RID: 1260
		[MarshalAs(UnmanagedType.I1)]
		internal bool IsFollowing;

		// Token: 0x020001AF RID: 431
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CEB RID: 7403 RVA: 0x000615B8 File Offset: 0x0005F7B8
			public static implicit operator FriendsIsFollowing_t(FriendsIsFollowing_t.PackSmall d)
			{
				return new FriendsIsFollowing_t
				{
					Result = d.Result,
					SteamID = d.SteamID,
					IsFollowing = d.IsFollowing
				};
			}

			// Token: 0x0400097F RID: 2431
			internal Result Result;

			// Token: 0x04000980 RID: 2432
			internal ulong SteamID;

			// Token: 0x04000981 RID: 2433
			[MarshalAs(UnmanagedType.I1)]
			internal bool IsFollowing;
		}
	}
}
