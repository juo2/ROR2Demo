using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000125 RID: 293
	internal struct SteamServersConnected_t
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x0003079C File Offset: 0x0002E99C
		internal static SteamServersConnected_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamServersConnected_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamServersConnected_t.PackSmall));
			}
			return (SteamServersConnected_t)Marshal.PtrToStructure(p, typeof(SteamServersConnected_t));
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000307D5 File Offset: 0x0002E9D5
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamServersConnected_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamServersConnected_t));
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00030800 File Offset: 0x0002EA00
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
						ResultA = new Callback.VTableWinThis.ResultD(SteamServersConnected_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SteamServersConnected_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SteamServersConnected_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SteamServersConnected_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SteamServersConnected_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SteamServersConnected_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SteamServersConnected_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SteamServersConnected_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SteamServersConnected_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SteamServersConnected_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SteamServersConnected_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SteamServersConnected_t.OnGetSize)
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
				CallbackId = 101
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 101);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00030B00 File Offset: 0x0002ED00
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SteamServersConnected_t.OnResult(param);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00030B08 File Offset: 0x0002ED08
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SteamServersConnected_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00030B12 File Offset: 0x0002ED12
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SteamServersConnected_t.OnGetSize();
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00030B19 File Offset: 0x0002ED19
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SteamServersConnected_t.StructSize();
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00030B20 File Offset: 0x0002ED20
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SteamServersConnected_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00030B30 File Offset: 0x0002ED30
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SteamServersConnected_t data = SteamServersConnected_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SteamServersConnected_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SteamServersConnected_t>(data);
			}
		}

		// Token: 0x04000776 RID: 1910
		internal const int CallbackId = 101;

		// Token: 0x02000249 RID: 585
		internal struct PackSmall
		{
			// Token: 0x06001D85 RID: 7557 RVA: 0x00063DC8 File Offset: 0x00061FC8
			public static implicit operator SteamServersConnected_t(SteamServersConnected_t.PackSmall d)
			{
				return default(SteamServersConnected_t);
			}
		}
	}
}
