using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000C7 RID: 199
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserAchievementIconFetched_t
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x0001BA28 File Offset: 0x00019C28
		internal static UserAchievementIconFetched_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (UserAchievementIconFetched_t.PackSmall)Marshal.PtrToStructure(p, typeof(UserAchievementIconFetched_t.PackSmall));
			}
			return (UserAchievementIconFetched_t)Marshal.PtrToStructure(p, typeof(UserAchievementIconFetched_t));
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001BA61 File Offset: 0x00019C61
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(UserAchievementIconFetched_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(UserAchievementIconFetched_t));
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001BA8C File Offset: 0x00019C8C
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
						ResultA = new Callback.VTableWinThis.ResultD(UserAchievementIconFetched_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(UserAchievementIconFetched_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(UserAchievementIconFetched_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(UserAchievementIconFetched_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(UserAchievementIconFetched_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(UserAchievementIconFetched_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(UserAchievementIconFetched_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(UserAchievementIconFetched_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(UserAchievementIconFetched_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(UserAchievementIconFetched_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(UserAchievementIconFetched_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(UserAchievementIconFetched_t.OnGetSize)
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
				CallbackId = 1109
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1109);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001BD92 File Offset: 0x00019F92
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			UserAchievementIconFetched_t.OnResult(param);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001BD9A File Offset: 0x00019F9A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			UserAchievementIconFetched_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001BDA4 File Offset: 0x00019FA4
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return UserAchievementIconFetched_t.OnGetSize();
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001BDAB File Offset: 0x00019FAB
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return UserAchievementIconFetched_t.StructSize();
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001BDB2 File Offset: 0x00019FB2
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			UserAchievementIconFetched_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001BDC4 File Offset: 0x00019FC4
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			UserAchievementIconFetched_t data = UserAchievementIconFetched_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<UserAchievementIconFetched_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<UserAchievementIconFetched_t>(data);
			}
		}

		// Token: 0x040005FC RID: 1532
		internal const int CallbackId = 1109;

		// Token: 0x040005FD RID: 1533
		internal ulong GameID;

		// Token: 0x040005FE RID: 1534
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string AchievementName;

		// Token: 0x040005FF RID: 1535
		[MarshalAs(UnmanagedType.I1)]
		internal bool Achieved;

		// Token: 0x04000600 RID: 1536
		internal int IconHandle;

		// Token: 0x020001EB RID: 491
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D27 RID: 7463 RVA: 0x00062660 File Offset: 0x00060860
			public static implicit operator UserAchievementIconFetched_t(UserAchievementIconFetched_t.PackSmall d)
			{
				return new UserAchievementIconFetched_t
				{
					GameID = d.GameID,
					AchievementName = d.AchievementName,
					Achieved = d.Achieved,
					IconHandle = d.IconHandle
				};
			}

			// Token: 0x04000A5B RID: 2651
			internal ulong GameID;

			// Token: 0x04000A5C RID: 2652
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string AchievementName;

			// Token: 0x04000A5D RID: 2653
			[MarshalAs(UnmanagedType.I1)]
			internal bool Achieved;

			// Token: 0x04000A5E RID: 2654
			internal int IconHandle;
		}
	}
}
