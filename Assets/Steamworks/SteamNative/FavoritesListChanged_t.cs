using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000095 RID: 149
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FavoritesListChanged_t
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x0000FDCB File Offset: 0x0000DFCB
		internal static FavoritesListChanged_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (FavoritesListChanged_t.PackSmall)Marshal.PtrToStructure(p, typeof(FavoritesListChanged_t.PackSmall));
			}
			return (FavoritesListChanged_t)Marshal.PtrToStructure(p, typeof(FavoritesListChanged_t));
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000FE04 File Offset: 0x0000E004
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(FavoritesListChanged_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(FavoritesListChanged_t));
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000FE2C File Offset: 0x0000E02C
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
						ResultA = new Callback.VTableWinThis.ResultD(FavoritesListChanged_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(FavoritesListChanged_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(FavoritesListChanged_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(FavoritesListChanged_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(FavoritesListChanged_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(FavoritesListChanged_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(FavoritesListChanged_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(FavoritesListChanged_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(FavoritesListChanged_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(FavoritesListChanged_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(FavoritesListChanged_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(FavoritesListChanged_t.OnGetSize)
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
				CallbackId = 502
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 502);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00010132 File Offset: 0x0000E332
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			FavoritesListChanged_t.OnResult(param);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001013A File Offset: 0x0000E33A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			FavoritesListChanged_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00010144 File Offset: 0x0000E344
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return FavoritesListChanged_t.OnGetSize();
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001014B File Offset: 0x0000E34B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return FavoritesListChanged_t.StructSize();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00010152 File Offset: 0x0000E352
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			FavoritesListChanged_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00010164 File Offset: 0x0000E364
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			FavoritesListChanged_t data = FavoritesListChanged_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<FavoritesListChanged_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<FavoritesListChanged_t>(data);
			}
		}

		// Token: 0x04000518 RID: 1304
		internal const int CallbackId = 502;

		// Token: 0x04000519 RID: 1305
		internal uint IP;

		// Token: 0x0400051A RID: 1306
		internal uint QueryPort;

		// Token: 0x0400051B RID: 1307
		internal uint ConnPort;

		// Token: 0x0400051C RID: 1308
		internal uint AppID;

		// Token: 0x0400051D RID: 1309
		internal uint Flags;

		// Token: 0x0400051E RID: 1310
		[MarshalAs(UnmanagedType.I1)]
		internal bool Add;

		// Token: 0x0400051F RID: 1311
		internal uint AccountId;

		// Token: 0x020001B9 RID: 441
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CF5 RID: 7413 RVA: 0x000618AC File Offset: 0x0005FAAC
			public static implicit operator FavoritesListChanged_t(FavoritesListChanged_t.PackSmall d)
			{
				return new FavoritesListChanged_t
				{
					IP = d.IP,
					QueryPort = d.QueryPort,
					ConnPort = d.ConnPort,
					AppID = d.AppID,
					Flags = d.Flags,
					Add = d.Add,
					AccountId = d.AccountId
				};
			}

			// Token: 0x040009A7 RID: 2471
			internal uint IP;

			// Token: 0x040009A8 RID: 2472
			internal uint QueryPort;

			// Token: 0x040009A9 RID: 2473
			internal uint ConnPort;

			// Token: 0x040009AA RID: 2474
			internal uint AppID;

			// Token: 0x040009AB RID: 2475
			internal uint Flags;

			// Token: 0x040009AC RID: 2476
			[MarshalAs(UnmanagedType.I1)]
			internal bool Add;

			// Token: 0x040009AD RID: 2477
			internal uint AccountId;
		}
	}
}
