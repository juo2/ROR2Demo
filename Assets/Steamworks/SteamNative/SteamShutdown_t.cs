using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200012B RID: 299
	internal struct SteamShutdown_t
	{
		// Token: 0x0600097A RID: 2426 RVA: 0x00031E9C File Offset: 0x0003009C
		internal static SteamShutdown_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamShutdown_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamShutdown_t.PackSmall));
			}
			return (SteamShutdown_t)Marshal.PtrToStructure(p, typeof(SteamShutdown_t));
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00031ED5 File Offset: 0x000300D5
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamShutdown_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamShutdown_t));
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00031F00 File Offset: 0x00030100
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
						ResultA = new Callback.VTableWinThis.ResultD(SteamShutdown_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SteamShutdown_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SteamShutdown_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SteamShutdown_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SteamShutdown_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SteamShutdown_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SteamShutdown_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SteamShutdown_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SteamShutdown_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SteamShutdown_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SteamShutdown_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SteamShutdown_t.OnGetSize)
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
				CallbackId = 704
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 704);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00032206 File Offset: 0x00030406
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SteamShutdown_t.OnResult(param);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0003220E File Offset: 0x0003040E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SteamShutdown_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00032218 File Offset: 0x00030418
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SteamShutdown_t.OnGetSize();
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0003221F File Offset: 0x0003041F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SteamShutdown_t.StructSize();
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00032226 File Offset: 0x00030426
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SteamShutdown_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00032238 File Offset: 0x00030438
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SteamShutdown_t data = SteamShutdown_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SteamShutdown_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SteamShutdown_t>(data);
			}
		}

		// Token: 0x0400077D RID: 1917
		internal const int CallbackId = 704;

		// Token: 0x0200024F RID: 591
		internal struct PackSmall
		{
			// Token: 0x06001D8B RID: 7563 RVA: 0x00063E64 File Offset: 0x00062064
			public static implicit operator SteamShutdown_t(SteamShutdown_t.PackSmall d)
			{
				return default(SteamShutdown_t);
			}
		}
	}
}
