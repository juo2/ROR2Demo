using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000118 RID: 280
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GSClientAchievementStatus_t
	{
		// Token: 0x060008CA RID: 2250 RVA: 0x0002D50C File Offset: 0x0002B70C
		internal static GSClientAchievementStatus_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GSClientAchievementStatus_t.PackSmall)Marshal.PtrToStructure(p, typeof(GSClientAchievementStatus_t.PackSmall));
			}
			return (GSClientAchievementStatus_t)Marshal.PtrToStructure(p, typeof(GSClientAchievementStatus_t));
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0002D545 File Offset: 0x0002B745
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GSClientAchievementStatus_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GSClientAchievementStatus_t));
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x0002D570 File Offset: 0x0002B770
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
						ResultA = new Callback.VTableWinThis.ResultD(GSClientAchievementStatus_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GSClientAchievementStatus_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GSClientAchievementStatus_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GSClientAchievementStatus_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GSClientAchievementStatus_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GSClientAchievementStatus_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GSClientAchievementStatus_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GSClientAchievementStatus_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GSClientAchievementStatus_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GSClientAchievementStatus_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GSClientAchievementStatus_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GSClientAchievementStatus_t.OnGetSize)
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
				CallbackId = 206
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 206);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0002D876 File Offset: 0x0002BA76
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GSClientAchievementStatus_t.OnResult(param);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0002D87E File Offset: 0x0002BA7E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GSClientAchievementStatus_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0002D888 File Offset: 0x0002BA88
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GSClientAchievementStatus_t.OnGetSize();
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0002D88F File Offset: 0x0002BA8F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GSClientAchievementStatus_t.StructSize();
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002D896 File Offset: 0x0002BA96
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GSClientAchievementStatus_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002D8A8 File Offset: 0x0002BAA8
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GSClientAchievementStatus_t data = GSClientAchievementStatus_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GSClientAchievementStatus_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GSClientAchievementStatus_t>(data);
			}
		}

		// Token: 0x04000749 RID: 1865
		internal const int CallbackId = 206;

		// Token: 0x0400074A RID: 1866
		internal ulong SteamID;

		// Token: 0x0400074B RID: 1867
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string PchAchievement;

		// Token: 0x0400074C RID: 1868
		[MarshalAs(UnmanagedType.I1)]
		internal bool Unlocked;

		// Token: 0x0200023C RID: 572
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D78 RID: 7544 RVA: 0x00063AF8 File Offset: 0x00061CF8
			public static implicit operator GSClientAchievementStatus_t(GSClientAchievementStatus_t.PackSmall d)
			{
				return new GSClientAchievementStatus_t
				{
					SteamID = d.SteamID,
					PchAchievement = d.PchAchievement,
					Unlocked = d.Unlocked
				};
			}

			// Token: 0x04000B60 RID: 2912
			internal ulong SteamID;

			// Token: 0x04000B61 RID: 2913
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string PchAchievement;

			// Token: 0x04000B62 RID: 2914
			[MarshalAs(UnmanagedType.I1)]
			internal bool Unlocked;
		}
	}
}
