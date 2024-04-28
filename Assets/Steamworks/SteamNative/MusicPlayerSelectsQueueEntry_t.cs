using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000D9 RID: 217
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct MusicPlayerSelectsQueueEntry_t
	{
		// Token: 0x060006B9 RID: 1721 RVA: 0x0001FC60 File Offset: 0x0001DE60
		internal static MusicPlayerSelectsQueueEntry_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (MusicPlayerSelectsQueueEntry_t.PackSmall)Marshal.PtrToStructure(p, typeof(MusicPlayerSelectsQueueEntry_t.PackSmall));
			}
			return (MusicPlayerSelectsQueueEntry_t)Marshal.PtrToStructure(p, typeof(MusicPlayerSelectsQueueEntry_t));
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001FC99 File Offset: 0x0001DE99
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(MusicPlayerSelectsQueueEntry_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(MusicPlayerSelectsQueueEntry_t));
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001FCC4 File Offset: 0x0001DEC4
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
						ResultA = new Callback.VTableWinThis.ResultD(MusicPlayerSelectsQueueEntry_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(MusicPlayerSelectsQueueEntry_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(MusicPlayerSelectsQueueEntry_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(MusicPlayerSelectsQueueEntry_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(MusicPlayerSelectsQueueEntry_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(MusicPlayerSelectsQueueEntry_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(MusicPlayerSelectsQueueEntry_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(MusicPlayerSelectsQueueEntry_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(MusicPlayerSelectsQueueEntry_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(MusicPlayerSelectsQueueEntry_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(MusicPlayerSelectsQueueEntry_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(MusicPlayerSelectsQueueEntry_t.OnGetSize)
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
				CallbackId = 4012
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4012);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001FFCA File Offset: 0x0001E1CA
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			MusicPlayerSelectsQueueEntry_t.OnResult(param);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001FFD2 File Offset: 0x0001E1D2
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			MusicPlayerSelectsQueueEntry_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001FFDC File Offset: 0x0001E1DC
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return MusicPlayerSelectsQueueEntry_t.OnGetSize();
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001FFE3 File Offset: 0x0001E1E3
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return MusicPlayerSelectsQueueEntry_t.StructSize();
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001FFEA File Offset: 0x0001E1EA
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			MusicPlayerSelectsQueueEntry_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001FFFC File Offset: 0x0001E1FC
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			MusicPlayerSelectsQueueEntry_t data = MusicPlayerSelectsQueueEntry_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<MusicPlayerSelectsQueueEntry_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<MusicPlayerSelectsQueueEntry_t>(data);
			}
		}

		// Token: 0x0400063A RID: 1594
		internal const int CallbackId = 4012;

		// Token: 0x0400063B RID: 1595
		internal int NID;

		// Token: 0x020001FD RID: 509
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D39 RID: 7481 RVA: 0x00062A48 File Offset: 0x00060C48
			public static implicit operator MusicPlayerSelectsQueueEntry_t(MusicPlayerSelectsQueueEntry_t.PackSmall d)
			{
				return new MusicPlayerSelectsQueueEntry_t
				{
					NID = d.NID
				};
			}

			// Token: 0x04000A88 RID: 2696
			internal int NID;
		}
	}
}
