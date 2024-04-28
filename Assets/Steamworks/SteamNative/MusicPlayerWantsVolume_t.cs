using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000D8 RID: 216
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct MusicPlayerWantsVolume_t
	{
		// Token: 0x060006B0 RID: 1712 RVA: 0x0001F888 File Offset: 0x0001DA88
		internal static MusicPlayerWantsVolume_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (MusicPlayerWantsVolume_t.PackSmall)Marshal.PtrToStructure(p, typeof(MusicPlayerWantsVolume_t.PackSmall));
			}
			return (MusicPlayerWantsVolume_t)Marshal.PtrToStructure(p, typeof(MusicPlayerWantsVolume_t));
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001F8C1 File Offset: 0x0001DAC1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(MusicPlayerWantsVolume_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(MusicPlayerWantsVolume_t));
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001F8EC File Offset: 0x0001DAEC
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
						ResultA = new Callback.VTableWinThis.ResultD(MusicPlayerWantsVolume_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(MusicPlayerWantsVolume_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(MusicPlayerWantsVolume_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(MusicPlayerWantsVolume_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(MusicPlayerWantsVolume_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(MusicPlayerWantsVolume_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(MusicPlayerWantsVolume_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(MusicPlayerWantsVolume_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(MusicPlayerWantsVolume_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(MusicPlayerWantsVolume_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(MusicPlayerWantsVolume_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(MusicPlayerWantsVolume_t.OnGetSize)
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
				CallbackId = 4011
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4011);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001FBF2 File Offset: 0x0001DDF2
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			MusicPlayerWantsVolume_t.OnResult(param);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001FBFA File Offset: 0x0001DDFA
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			MusicPlayerWantsVolume_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001FC04 File Offset: 0x0001DE04
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return MusicPlayerWantsVolume_t.OnGetSize();
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001FC0B File Offset: 0x0001DE0B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return MusicPlayerWantsVolume_t.StructSize();
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001FC12 File Offset: 0x0001DE12
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			MusicPlayerWantsVolume_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001FC24 File Offset: 0x0001DE24
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			MusicPlayerWantsVolume_t data = MusicPlayerWantsVolume_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<MusicPlayerWantsVolume_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<MusicPlayerWantsVolume_t>(data);
			}
		}

		// Token: 0x04000638 RID: 1592
		internal const int CallbackId = 4011;

		// Token: 0x04000639 RID: 1593
		internal float NewVolume;

		// Token: 0x020001FC RID: 508
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D38 RID: 7480 RVA: 0x00062A24 File Offset: 0x00060C24
			public static implicit operator MusicPlayerWantsVolume_t(MusicPlayerWantsVolume_t.PackSmall d)
			{
				return new MusicPlayerWantsVolume_t
				{
					NewVolume = d.NewVolume
				};
			}

			// Token: 0x04000A87 RID: 2695
			internal float NewVolume;
		}
	}
}
