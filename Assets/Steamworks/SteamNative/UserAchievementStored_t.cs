using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000C1 RID: 193
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserAchievementStored_t
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x0001A298 File Offset: 0x00018498
		internal static UserAchievementStored_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (UserAchievementStored_t.PackSmall)Marshal.PtrToStructure(p, typeof(UserAchievementStored_t.PackSmall));
			}
			return (UserAchievementStored_t)Marshal.PtrToStructure(p, typeof(UserAchievementStored_t));
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001A2D1 File Offset: 0x000184D1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(UserAchievementStored_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(UserAchievementStored_t));
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001A2FC File Offset: 0x000184FC
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
						ResultA = new Callback.VTableWinThis.ResultD(UserAchievementStored_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(UserAchievementStored_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(UserAchievementStored_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(UserAchievementStored_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(UserAchievementStored_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(UserAchievementStored_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(UserAchievementStored_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(UserAchievementStored_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(UserAchievementStored_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(UserAchievementStored_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(UserAchievementStored_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(UserAchievementStored_t.OnGetSize)
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
				CallbackId = 1103
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1103);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001A602 File Offset: 0x00018802
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			UserAchievementStored_t.OnResult(param);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001A60A File Offset: 0x0001880A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			UserAchievementStored_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001A614 File Offset: 0x00018814
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return UserAchievementStored_t.OnGetSize();
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001A61B File Offset: 0x0001881B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return UserAchievementStored_t.StructSize();
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001A622 File Offset: 0x00018822
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			UserAchievementStored_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001A634 File Offset: 0x00018834
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			UserAchievementStored_t data = UserAchievementStored_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<UserAchievementStored_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<UserAchievementStored_t>(data);
			}
		}

		// Token: 0x040005E3 RID: 1507
		internal const int CallbackId = 1103;

		// Token: 0x040005E4 RID: 1508
		internal ulong GameID;

		// Token: 0x040005E5 RID: 1509
		[MarshalAs(UnmanagedType.I1)]
		internal bool GroupAchievement;

		// Token: 0x040005E6 RID: 1510
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string AchievementName;

		// Token: 0x040005E7 RID: 1511
		internal uint CurProgress;

		// Token: 0x040005E8 RID: 1512
		internal uint MaxProgress;

		// Token: 0x020001E5 RID: 485
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D21 RID: 7457 RVA: 0x000624E0 File Offset: 0x000606E0
			public static implicit operator UserAchievementStored_t(UserAchievementStored_t.PackSmall d)
			{
				return new UserAchievementStored_t
				{
					GameID = d.GameID,
					GroupAchievement = d.GroupAchievement,
					AchievementName = d.AchievementName,
					CurProgress = d.CurProgress,
					MaxProgress = d.MaxProgress
				};
			}

			// Token: 0x04000A48 RID: 2632
			internal ulong GameID;

			// Token: 0x04000A49 RID: 2633
			[MarshalAs(UnmanagedType.I1)]
			internal bool GroupAchievement;

			// Token: 0x04000A4A RID: 2634
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string AchievementName;

			// Token: 0x04000A4B RID: 2635
			internal uint CurProgress;

			// Token: 0x04000A4C RID: 2636
			internal uint MaxProgress;
		}
	}
}
