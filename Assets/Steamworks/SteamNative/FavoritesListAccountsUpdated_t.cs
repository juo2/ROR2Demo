using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000A0 RID: 160
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FavoritesListAccountsUpdated_t
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x00012870 File Offset: 0x00010A70
		internal static FavoritesListAccountsUpdated_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (FavoritesListAccountsUpdated_t.PackSmall)Marshal.PtrToStructure(p, typeof(FavoritesListAccountsUpdated_t.PackSmall));
			}
			return (FavoritesListAccountsUpdated_t)Marshal.PtrToStructure(p, typeof(FavoritesListAccountsUpdated_t));
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x000128A9 File Offset: 0x00010AA9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(FavoritesListAccountsUpdated_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(FavoritesListAccountsUpdated_t));
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000128D4 File Offset: 0x00010AD4
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
						ResultA = new Callback.VTableWinThis.ResultD(FavoritesListAccountsUpdated_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(FavoritesListAccountsUpdated_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(FavoritesListAccountsUpdated_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(FavoritesListAccountsUpdated_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(FavoritesListAccountsUpdated_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(FavoritesListAccountsUpdated_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(FavoritesListAccountsUpdated_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(FavoritesListAccountsUpdated_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(FavoritesListAccountsUpdated_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(FavoritesListAccountsUpdated_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(FavoritesListAccountsUpdated_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(FavoritesListAccountsUpdated_t.OnGetSize)
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
				CallbackId = 516
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 516);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00012BDA File Offset: 0x00010DDA
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			FavoritesListAccountsUpdated_t.OnResult(param);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00012BE2 File Offset: 0x00010DE2
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			FavoritesListAccountsUpdated_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00012BEC File Offset: 0x00010DEC
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return FavoritesListAccountsUpdated_t.OnGetSize();
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00012BF3 File Offset: 0x00010DF3
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return FavoritesListAccountsUpdated_t.StructSize();
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00012BFA File Offset: 0x00010DFA
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			FavoritesListAccountsUpdated_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00012C0C File Offset: 0x00010E0C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			FavoritesListAccountsUpdated_t data = FavoritesListAccountsUpdated_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<FavoritesListAccountsUpdated_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<FavoritesListAccountsUpdated_t>(data);
			}
		}

		// Token: 0x04000548 RID: 1352
		internal const int CallbackId = 516;

		// Token: 0x04000549 RID: 1353
		internal Result Result;

		// Token: 0x020001C4 RID: 452
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D00 RID: 7424 RVA: 0x00061B94 File Offset: 0x0005FD94
			public static implicit operator FavoritesListAccountsUpdated_t(FavoritesListAccountsUpdated_t.PackSmall d)
			{
				return new FavoritesListAccountsUpdated_t
				{
					Result = d.Result
				};
			}

			// Token: 0x040009CC RID: 2508
			internal Result Result;
		}
	}
}
