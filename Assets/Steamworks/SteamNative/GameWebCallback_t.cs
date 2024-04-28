using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000078 RID: 120
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GameWebCallback_t
	{
		// Token: 0x06000363 RID: 867 RVA: 0x00009EA8 File Offset: 0x000080A8
		internal static GameWebCallback_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GameWebCallback_t.PackSmall)Marshal.PtrToStructure(p, typeof(GameWebCallback_t.PackSmall));
			}
			return (GameWebCallback_t)Marshal.PtrToStructure(p, typeof(GameWebCallback_t));
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00009EE1 File Offset: 0x000080E1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GameWebCallback_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GameWebCallback_t));
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00009F0C File Offset: 0x0000810C
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
						ResultA = new Callback.VTableWinThis.ResultD(GameWebCallback_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GameWebCallback_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GameWebCallback_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GameWebCallback_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GameWebCallback_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GameWebCallback_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GameWebCallback_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GameWebCallback_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GameWebCallback_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GameWebCallback_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GameWebCallback_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GameWebCallback_t.OnGetSize)
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
				CallbackId = 164
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 164);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000A212 File Offset: 0x00008412
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GameWebCallback_t.OnResult(param);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000A21A File Offset: 0x0000841A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GameWebCallback_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000A224 File Offset: 0x00008424
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GameWebCallback_t.OnGetSize();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000A22B File Offset: 0x0000842B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GameWebCallback_t.StructSize();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000A232 File Offset: 0x00008432
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GameWebCallback_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000A244 File Offset: 0x00008444
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GameWebCallback_t data = GameWebCallback_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GameWebCallback_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GameWebCallback_t>(data);
			}
		}

		// Token: 0x040004AC RID: 1196
		internal const int CallbackId = 164;

		// Token: 0x040004AD RID: 1197
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string URL;

		// Token: 0x0200019C RID: 412
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CD8 RID: 7384 RVA: 0x000611C8 File Offset: 0x0005F3C8
			public static implicit operator GameWebCallback_t(GameWebCallback_t.PackSmall d)
			{
				return new GameWebCallback_t
				{
					URL = d.URL
				};
			}

			// Token: 0x04000953 RID: 2387
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string URL;
		}
	}
}
