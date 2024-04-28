using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000C0 RID: 192
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserStatsStored_t
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x00019EC0 File Offset: 0x000180C0
		internal static UserStatsStored_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (UserStatsStored_t.PackSmall)Marshal.PtrToStructure(p, typeof(UserStatsStored_t.PackSmall));
			}
			return (UserStatsStored_t)Marshal.PtrToStructure(p, typeof(UserStatsStored_t));
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00019EF9 File Offset: 0x000180F9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(UserStatsStored_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(UserStatsStored_t));
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00019F24 File Offset: 0x00018124
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
						ResultA = new Callback.VTableWinThis.ResultD(UserStatsStored_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(UserStatsStored_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(UserStatsStored_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(UserStatsStored_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(UserStatsStored_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(UserStatsStored_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(UserStatsStored_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(UserStatsStored_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(UserStatsStored_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(UserStatsStored_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(UserStatsStored_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(UserStatsStored_t.OnGetSize)
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
				CallbackId = 1102
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1102);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001A22A File Offset: 0x0001842A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			UserStatsStored_t.OnResult(param);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001A232 File Offset: 0x00018432
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			UserStatsStored_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001A23C File Offset: 0x0001843C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return UserStatsStored_t.OnGetSize();
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001A243 File Offset: 0x00018443
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return UserStatsStored_t.StructSize();
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001A24A File Offset: 0x0001844A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			UserStatsStored_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001A25C File Offset: 0x0001845C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			UserStatsStored_t data = UserStatsStored_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<UserStatsStored_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<UserStatsStored_t>(data);
			}
		}

		// Token: 0x040005E0 RID: 1504
		internal const int CallbackId = 1102;

		// Token: 0x040005E1 RID: 1505
		internal ulong GameID;

		// Token: 0x040005E2 RID: 1506
		internal Result Result;

		// Token: 0x020001E4 RID: 484
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D20 RID: 7456 RVA: 0x000624B0 File Offset: 0x000606B0
			public static implicit operator UserStatsStored_t(UserStatsStored_t.PackSmall d)
			{
				return new UserStatsStored_t
				{
					GameID = d.GameID,
					Result = d.Result
				};
			}

			// Token: 0x04000A46 RID: 2630
			internal ulong GameID;

			// Token: 0x04000A47 RID: 2631
			internal Result Result;
		}
	}
}
