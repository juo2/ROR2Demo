using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200012C RID: 300
	internal struct IPCountry_t
	{
		// Token: 0x06000983 RID: 2435 RVA: 0x00032274 File Offset: 0x00030474
		internal static IPCountry_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (IPCountry_t.PackSmall)Marshal.PtrToStructure(p, typeof(IPCountry_t.PackSmall));
			}
			return (IPCountry_t)Marshal.PtrToStructure(p, typeof(IPCountry_t));
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x000322AD File Offset: 0x000304AD
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(IPCountry_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(IPCountry_t));
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x000322D8 File Offset: 0x000304D8
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
						ResultA = new Callback.VTableWinThis.ResultD(IPCountry_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(IPCountry_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(IPCountry_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(IPCountry_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(IPCountry_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(IPCountry_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(IPCountry_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(IPCountry_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(IPCountry_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(IPCountry_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(IPCountry_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(IPCountry_t.OnGetSize)
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
				CallbackId = 701
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 701);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000325DE File Offset: 0x000307DE
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			IPCountry_t.OnResult(param);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x000325E6 File Offset: 0x000307E6
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			IPCountry_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x000325F0 File Offset: 0x000307F0
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return IPCountry_t.OnGetSize();
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x000325F7 File Offset: 0x000307F7
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return IPCountry_t.StructSize();
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x000325FE File Offset: 0x000307FE
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			IPCountry_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00032610 File Offset: 0x00030810
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			IPCountry_t data = IPCountry_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<IPCountry_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<IPCountry_t>(data);
			}
		}

		// Token: 0x0400077E RID: 1918
		internal const int CallbackId = 701;

		// Token: 0x02000250 RID: 592
		internal struct PackSmall
		{
			// Token: 0x06001D8C RID: 7564 RVA: 0x00063E7C File Offset: 0x0006207C
			public static implicit operator IPCountry_t(IPCountry_t.PackSmall d)
			{
				return default(IPCountry_t);
			}
		}
	}
}
