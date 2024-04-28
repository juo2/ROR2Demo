using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000D6 RID: 214
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct MusicPlayerWantsShuffled_t
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x0001F0D8 File Offset: 0x0001D2D8
		internal static MusicPlayerWantsShuffled_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (MusicPlayerWantsShuffled_t.PackSmall)Marshal.PtrToStructure(p, typeof(MusicPlayerWantsShuffled_t.PackSmall));
			}
			return (MusicPlayerWantsShuffled_t)Marshal.PtrToStructure(p, typeof(MusicPlayerWantsShuffled_t));
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001F111 File Offset: 0x0001D311
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(MusicPlayerWantsShuffled_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(MusicPlayerWantsShuffled_t));
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001F13C File Offset: 0x0001D33C
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
						ResultA = new Callback.VTableWinThis.ResultD(MusicPlayerWantsShuffled_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(MusicPlayerWantsShuffled_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(MusicPlayerWantsShuffled_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(MusicPlayerWantsShuffled_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(MusicPlayerWantsShuffled_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(MusicPlayerWantsShuffled_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(MusicPlayerWantsShuffled_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(MusicPlayerWantsShuffled_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(MusicPlayerWantsShuffled_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(MusicPlayerWantsShuffled_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(MusicPlayerWantsShuffled_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(MusicPlayerWantsShuffled_t.OnGetSize)
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
				CallbackId = 4109
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4109);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001F442 File Offset: 0x0001D642
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			MusicPlayerWantsShuffled_t.OnResult(param);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001F44A File Offset: 0x0001D64A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			MusicPlayerWantsShuffled_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001F454 File Offset: 0x0001D654
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return MusicPlayerWantsShuffled_t.OnGetSize();
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001F45B File Offset: 0x0001D65B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return MusicPlayerWantsShuffled_t.StructSize();
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001F462 File Offset: 0x0001D662
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			MusicPlayerWantsShuffled_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001F474 File Offset: 0x0001D674
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			MusicPlayerWantsShuffled_t data = MusicPlayerWantsShuffled_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<MusicPlayerWantsShuffled_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<MusicPlayerWantsShuffled_t>(data);
			}
		}

		// Token: 0x04000634 RID: 1588
		internal const int CallbackId = 4109;

		// Token: 0x04000635 RID: 1589
		[MarshalAs(UnmanagedType.I1)]
		internal bool Shuffled;

		// Token: 0x020001FA RID: 506
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D36 RID: 7478 RVA: 0x000629DC File Offset: 0x00060BDC
			public static implicit operator MusicPlayerWantsShuffled_t(MusicPlayerWantsShuffled_t.PackSmall d)
			{
				return new MusicPlayerWantsShuffled_t
				{
					Shuffled = d.Shuffled
				};
			}

			// Token: 0x04000A85 RID: 2693
			[MarshalAs(UnmanagedType.I1)]
			internal bool Shuffled;
		}
	}
}
