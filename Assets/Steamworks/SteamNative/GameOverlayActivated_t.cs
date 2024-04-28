using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200007D RID: 125
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GameOverlayActivated_t
	{
		// Token: 0x06000383 RID: 899 RVA: 0x0000AB10 File Offset: 0x00008D10
		internal static GameOverlayActivated_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GameOverlayActivated_t.PackSmall)Marshal.PtrToStructure(p, typeof(GameOverlayActivated_t.PackSmall));
			}
			return (GameOverlayActivated_t)Marshal.PtrToStructure(p, typeof(GameOverlayActivated_t));
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000AB49 File Offset: 0x00008D49
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GameOverlayActivated_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GameOverlayActivated_t));
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000AB74 File Offset: 0x00008D74
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
						ResultA = new Callback.VTableWinThis.ResultD(GameOverlayActivated_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GameOverlayActivated_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GameOverlayActivated_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GameOverlayActivated_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GameOverlayActivated_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GameOverlayActivated_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GameOverlayActivated_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GameOverlayActivated_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GameOverlayActivated_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GameOverlayActivated_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GameOverlayActivated_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GameOverlayActivated_t.OnGetSize)
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
				CallbackId = 331
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 331);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000AE7A File Offset: 0x0000907A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GameOverlayActivated_t.OnResult(param);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000AE82 File Offset: 0x00009082
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GameOverlayActivated_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000AE8C File Offset: 0x0000908C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GameOverlayActivated_t.OnGetSize();
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000AE93 File Offset: 0x00009093
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GameOverlayActivated_t.StructSize();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000AE9A File Offset: 0x0000909A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GameOverlayActivated_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000AEAC File Offset: 0x000090AC
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GameOverlayActivated_t data = GameOverlayActivated_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GameOverlayActivated_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GameOverlayActivated_t>(data);
			}
		}

		// Token: 0x040004BA RID: 1210
		internal const int CallbackId = 331;

		// Token: 0x040004BB RID: 1211
		internal byte Active;

		// Token: 0x020001A1 RID: 417
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CDD RID: 7389 RVA: 0x000612C8 File Offset: 0x0005F4C8
			public static implicit operator GameOverlayActivated_t(GameOverlayActivated_t.PackSmall d)
			{
				return new GameOverlayActivated_t
				{
					Active = d.Active
				};
			}

			// Token: 0x0400095E RID: 2398
			internal byte Active;
		}
	}
}
