using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000C6 RID: 198
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct UserStatsUnloaded_t
	{
		// Token: 0x06000611 RID: 1553 RVA: 0x0001B650 File Offset: 0x00019850
		internal static UserStatsUnloaded_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (UserStatsUnloaded_t.PackSmall)Marshal.PtrToStructure(p, typeof(UserStatsUnloaded_t.PackSmall));
			}
			return (UserStatsUnloaded_t)Marshal.PtrToStructure(p, typeof(UserStatsUnloaded_t));
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001B689 File Offset: 0x00019889
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(UserStatsUnloaded_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(UserStatsUnloaded_t));
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001B6B4 File Offset: 0x000198B4
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
						ResultA = new Callback.VTableWinThis.ResultD(UserStatsUnloaded_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(UserStatsUnloaded_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(UserStatsUnloaded_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(UserStatsUnloaded_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(UserStatsUnloaded_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(UserStatsUnloaded_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(UserStatsUnloaded_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(UserStatsUnloaded_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(UserStatsUnloaded_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(UserStatsUnloaded_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(UserStatsUnloaded_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(UserStatsUnloaded_t.OnGetSize)
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
				CallbackId = 1108
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1108);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001B9BA File Offset: 0x00019BBA
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			UserStatsUnloaded_t.OnResult(param);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001B9C2 File Offset: 0x00019BC2
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			UserStatsUnloaded_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001B9CC File Offset: 0x00019BCC
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return UserStatsUnloaded_t.OnGetSize();
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001B9D3 File Offset: 0x00019BD3
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return UserStatsUnloaded_t.StructSize();
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001B9DA File Offset: 0x00019BDA
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			UserStatsUnloaded_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001B9EC File Offset: 0x00019BEC
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			UserStatsUnloaded_t data = UserStatsUnloaded_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<UserStatsUnloaded_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<UserStatsUnloaded_t>(data);
			}
		}

		// Token: 0x040005FA RID: 1530
		internal const int CallbackId = 1108;

		// Token: 0x040005FB RID: 1531
		internal ulong SteamIDUser;

		// Token: 0x020001EA RID: 490
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D26 RID: 7462 RVA: 0x0006263C File Offset: 0x0006083C
			public static implicit operator UserStatsUnloaded_t(UserStatsUnloaded_t.PackSmall d)
			{
				return new UserStatsUnloaded_t
				{
					SteamIDUser = d.SteamIDUser
				};
			}

			// Token: 0x04000A5A RID: 2650
			internal ulong SteamIDUser;
		}
	}
}
