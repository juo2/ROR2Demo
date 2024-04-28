using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000DA RID: 218
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct MusicPlayerSelectsPlaylistEntry_t
	{
		// Token: 0x060006C2 RID: 1730 RVA: 0x00020038 File Offset: 0x0001E238
		internal static MusicPlayerSelectsPlaylistEntry_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (MusicPlayerSelectsPlaylistEntry_t.PackSmall)Marshal.PtrToStructure(p, typeof(MusicPlayerSelectsPlaylistEntry_t.PackSmall));
			}
			return (MusicPlayerSelectsPlaylistEntry_t)Marshal.PtrToStructure(p, typeof(MusicPlayerSelectsPlaylistEntry_t));
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00020071 File Offset: 0x0001E271
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(MusicPlayerSelectsPlaylistEntry_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(MusicPlayerSelectsPlaylistEntry_t));
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0002009C File Offset: 0x0001E29C
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
						ResultA = new Callback.VTableWinThis.ResultD(MusicPlayerSelectsPlaylistEntry_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(MusicPlayerSelectsPlaylistEntry_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(MusicPlayerSelectsPlaylistEntry_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(MusicPlayerSelectsPlaylistEntry_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(MusicPlayerSelectsPlaylistEntry_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(MusicPlayerSelectsPlaylistEntry_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(MusicPlayerSelectsPlaylistEntry_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(MusicPlayerSelectsPlaylistEntry_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(MusicPlayerSelectsPlaylistEntry_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(MusicPlayerSelectsPlaylistEntry_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(MusicPlayerSelectsPlaylistEntry_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(MusicPlayerSelectsPlaylistEntry_t.OnGetSize)
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
				CallbackId = 4013
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4013);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000203A2 File Offset: 0x0001E5A2
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			MusicPlayerSelectsPlaylistEntry_t.OnResult(param);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x000203AA File Offset: 0x0001E5AA
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			MusicPlayerSelectsPlaylistEntry_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x000203B4 File Offset: 0x0001E5B4
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return MusicPlayerSelectsPlaylistEntry_t.OnGetSize();
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x000203BB File Offset: 0x0001E5BB
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return MusicPlayerSelectsPlaylistEntry_t.StructSize();
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000203C2 File Offset: 0x0001E5C2
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			MusicPlayerSelectsPlaylistEntry_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000203D4 File Offset: 0x0001E5D4
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			MusicPlayerSelectsPlaylistEntry_t data = MusicPlayerSelectsPlaylistEntry_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<MusicPlayerSelectsPlaylistEntry_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<MusicPlayerSelectsPlaylistEntry_t>(data);
			}
		}

		// Token: 0x0400063C RID: 1596
		internal const int CallbackId = 4013;

		// Token: 0x0400063D RID: 1597
		internal int NID;

		// Token: 0x020001FE RID: 510
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D3A RID: 7482 RVA: 0x00062A6C File Offset: 0x00060C6C
			public static implicit operator MusicPlayerSelectsPlaylistEntry_t(MusicPlayerSelectsPlaylistEntry_t.PackSmall d)
			{
				return new MusicPlayerSelectsPlaylistEntry_t
				{
					NID = d.NID
				};
			}

			// Token: 0x04000A89 RID: 2697
			internal int NID;
		}
	}
}
