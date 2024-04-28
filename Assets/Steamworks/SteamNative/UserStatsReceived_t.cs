using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000BF RID: 191
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct UserStatsReceived_t
	{
		// Token: 0x060005CD RID: 1485 RVA: 0x00019AC9 File Offset: 0x00017CC9
		internal static UserStatsReceived_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (UserStatsReceived_t.PackSmall)Marshal.PtrToStructure(p, typeof(UserStatsReceived_t.PackSmall));
			}
			return (UserStatsReceived_t)Marshal.PtrToStructure(p, typeof(UserStatsReceived_t));
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00019B02 File Offset: 0x00017D02
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(UserStatsReceived_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(UserStatsReceived_t));
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00019B2A File Offset: 0x00017D2A
		internal static CallResult<UserStatsReceived_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<UserStatsReceived_t, bool> CallbackFunction)
		{
			return new CallResult<UserStatsReceived_t>(steamworks, call, CallbackFunction, new CallResult<UserStatsReceived_t>.ConvertFromPointer(UserStatsReceived_t.FromPointer), UserStatsReceived_t.StructSize(), 1101);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00019B4C File Offset: 0x00017D4C
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
						ResultA = new Callback.VTableWinThis.ResultD(UserStatsReceived_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(UserStatsReceived_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(UserStatsReceived_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(UserStatsReceived_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(UserStatsReceived_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(UserStatsReceived_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(UserStatsReceived_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(UserStatsReceived_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(UserStatsReceived_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(UserStatsReceived_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(UserStatsReceived_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(UserStatsReceived_t.OnGetSize)
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
				CallbackId = 1101
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1101);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00019E52 File Offset: 0x00018052
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			UserStatsReceived_t.OnResult(param);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00019E5A File Offset: 0x0001805A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			UserStatsReceived_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00019E64 File Offset: 0x00018064
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return UserStatsReceived_t.OnGetSize();
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00019E6B File Offset: 0x0001806B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return UserStatsReceived_t.StructSize();
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00019E72 File Offset: 0x00018072
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			UserStatsReceived_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00019E84 File Offset: 0x00018084
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			UserStatsReceived_t data = UserStatsReceived_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<UserStatsReceived_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<UserStatsReceived_t>(data);
			}
		}

		// Token: 0x040005DC RID: 1500
		internal const int CallbackId = 1101;

		// Token: 0x040005DD RID: 1501
		internal ulong GameID;

		// Token: 0x040005DE RID: 1502
		internal Result Result;

		// Token: 0x040005DF RID: 1503
		internal ulong SteamIDUser;

		// Token: 0x020001E3 RID: 483
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D1F RID: 7455 RVA: 0x00062470 File Offset: 0x00060670
			public static implicit operator UserStatsReceived_t(UserStatsReceived_t.PackSmall d)
			{
				return new UserStatsReceived_t
				{
					GameID = d.GameID,
					Result = d.Result,
					SteamIDUser = d.SteamIDUser
				};
			}

			// Token: 0x04000A43 RID: 2627
			internal ulong GameID;

			// Token: 0x04000A44 RID: 2628
			internal Result Result;

			// Token: 0x04000A45 RID: 2629
			internal ulong SteamIDUser;
		}
	}
}
