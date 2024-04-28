using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000D7 RID: 215
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct MusicPlayerWantsLooped_t
	{
		// Token: 0x060006A7 RID: 1703 RVA: 0x0001F4B0 File Offset: 0x0001D6B0
		internal static MusicPlayerWantsLooped_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (MusicPlayerWantsLooped_t.PackSmall)Marshal.PtrToStructure(p, typeof(MusicPlayerWantsLooped_t.PackSmall));
			}
			return (MusicPlayerWantsLooped_t)Marshal.PtrToStructure(p, typeof(MusicPlayerWantsLooped_t));
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001F4E9 File Offset: 0x0001D6E9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(MusicPlayerWantsLooped_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(MusicPlayerWantsLooped_t));
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001F514 File Offset: 0x0001D714
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
						ResultA = new Callback.VTableWinThis.ResultD(MusicPlayerWantsLooped_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(MusicPlayerWantsLooped_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(MusicPlayerWantsLooped_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(MusicPlayerWantsLooped_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(MusicPlayerWantsLooped_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(MusicPlayerWantsLooped_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(MusicPlayerWantsLooped_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(MusicPlayerWantsLooped_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(MusicPlayerWantsLooped_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(MusicPlayerWantsLooped_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(MusicPlayerWantsLooped_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(MusicPlayerWantsLooped_t.OnGetSize)
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
				CallbackId = 4110
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4110);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001F81A File Offset: 0x0001DA1A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			MusicPlayerWantsLooped_t.OnResult(param);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001F822 File Offset: 0x0001DA22
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			MusicPlayerWantsLooped_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001F82C File Offset: 0x0001DA2C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return MusicPlayerWantsLooped_t.OnGetSize();
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001F833 File Offset: 0x0001DA33
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return MusicPlayerWantsLooped_t.StructSize();
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001F83A File Offset: 0x0001DA3A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			MusicPlayerWantsLooped_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001F84C File Offset: 0x0001DA4C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			MusicPlayerWantsLooped_t data = MusicPlayerWantsLooped_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<MusicPlayerWantsLooped_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<MusicPlayerWantsLooped_t>(data);
			}
		}

		// Token: 0x04000636 RID: 1590
		internal const int CallbackId = 4110;

		// Token: 0x04000637 RID: 1591
		[MarshalAs(UnmanagedType.I1)]
		internal bool Looped;

		// Token: 0x020001FB RID: 507
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D37 RID: 7479 RVA: 0x00062A00 File Offset: 0x00060C00
			public static implicit operator MusicPlayerWantsLooped_t(MusicPlayerWantsLooped_t.PackSmall d)
			{
				return new MusicPlayerWantsLooped_t
				{
					Looped = d.Looped
				};
			}

			// Token: 0x04000A86 RID: 2694
			[MarshalAs(UnmanagedType.I1)]
			internal bool Looped;
		}
	}
}
