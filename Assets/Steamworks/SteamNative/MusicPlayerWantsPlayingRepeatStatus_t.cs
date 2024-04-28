using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000DB RID: 219
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct MusicPlayerWantsPlayingRepeatStatus_t
	{
		// Token: 0x060006CB RID: 1739 RVA: 0x00020410 File Offset: 0x0001E610
		internal static MusicPlayerWantsPlayingRepeatStatus_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (MusicPlayerWantsPlayingRepeatStatus_t.PackSmall)Marshal.PtrToStructure(p, typeof(MusicPlayerWantsPlayingRepeatStatus_t.PackSmall));
			}
			return (MusicPlayerWantsPlayingRepeatStatus_t)Marshal.PtrToStructure(p, typeof(MusicPlayerWantsPlayingRepeatStatus_t));
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00020449 File Offset: 0x0001E649
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(MusicPlayerWantsPlayingRepeatStatus_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(MusicPlayerWantsPlayingRepeatStatus_t));
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00020474 File Offset: 0x0001E674
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
						ResultA = new Callback.VTableWinThis.ResultD(MusicPlayerWantsPlayingRepeatStatus_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(MusicPlayerWantsPlayingRepeatStatus_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(MusicPlayerWantsPlayingRepeatStatus_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(MusicPlayerWantsPlayingRepeatStatus_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(MusicPlayerWantsPlayingRepeatStatus_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(MusicPlayerWantsPlayingRepeatStatus_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(MusicPlayerWantsPlayingRepeatStatus_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(MusicPlayerWantsPlayingRepeatStatus_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(MusicPlayerWantsPlayingRepeatStatus_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(MusicPlayerWantsPlayingRepeatStatus_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(MusicPlayerWantsPlayingRepeatStatus_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(MusicPlayerWantsPlayingRepeatStatus_t.OnGetSize)
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
				CallbackId = 4114
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4114);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0002077A File Offset: 0x0001E97A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			MusicPlayerWantsPlayingRepeatStatus_t.OnResult(param);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00020782 File Offset: 0x0001E982
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			MusicPlayerWantsPlayingRepeatStatus_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0002078C File Offset: 0x0001E98C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return MusicPlayerWantsPlayingRepeatStatus_t.OnGetSize();
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00020793 File Offset: 0x0001E993
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return MusicPlayerWantsPlayingRepeatStatus_t.StructSize();
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0002079A File Offset: 0x0001E99A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			MusicPlayerWantsPlayingRepeatStatus_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000207AC File Offset: 0x0001E9AC
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			MusicPlayerWantsPlayingRepeatStatus_t data = MusicPlayerWantsPlayingRepeatStatus_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<MusicPlayerWantsPlayingRepeatStatus_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<MusicPlayerWantsPlayingRepeatStatus_t>(data);
			}
		}

		// Token: 0x0400063E RID: 1598
		internal const int CallbackId = 4114;

		// Token: 0x0400063F RID: 1599
		internal int PlayingRepeatStatus;

		// Token: 0x020001FF RID: 511
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D3B RID: 7483 RVA: 0x00062A90 File Offset: 0x00060C90
			public static implicit operator MusicPlayerWantsPlayingRepeatStatus_t(MusicPlayerWantsPlayingRepeatStatus_t.PackSmall d)
			{
				return new MusicPlayerWantsPlayingRepeatStatus_t
				{
					PlayingRepeatStatus = d.PlayingRepeatStatus
				};
			}

			// Token: 0x04000A8A RID: 2698
			internal int PlayingRepeatStatus;
		}
	}
}
