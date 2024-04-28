using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200012A RID: 298
	internal struct LicensesUpdated_t
	{
		// Token: 0x06000971 RID: 2417 RVA: 0x00031ACC File Offset: 0x0002FCCC
		internal static LicensesUpdated_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (LicensesUpdated_t.PackSmall)Marshal.PtrToStructure(p, typeof(LicensesUpdated_t.PackSmall));
			}
			return (LicensesUpdated_t)Marshal.PtrToStructure(p, typeof(LicensesUpdated_t));
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00031B05 File Offset: 0x0002FD05
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(LicensesUpdated_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(LicensesUpdated_t));
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00031B30 File Offset: 0x0002FD30
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
						ResultA = new Callback.VTableWinThis.ResultD(LicensesUpdated_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(LicensesUpdated_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(LicensesUpdated_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(LicensesUpdated_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(LicensesUpdated_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(LicensesUpdated_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(LicensesUpdated_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(LicensesUpdated_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(LicensesUpdated_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(LicensesUpdated_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(LicensesUpdated_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(LicensesUpdated_t.OnGetSize)
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
				CallbackId = 125
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 125);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00031E30 File Offset: 0x00030030
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			LicensesUpdated_t.OnResult(param);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00031E38 File Offset: 0x00030038
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			LicensesUpdated_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00031E42 File Offset: 0x00030042
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return LicensesUpdated_t.OnGetSize();
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00031E49 File Offset: 0x00030049
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return LicensesUpdated_t.StructSize();
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00031E50 File Offset: 0x00030050
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			LicensesUpdated_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00031E60 File Offset: 0x00030060
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			LicensesUpdated_t data = LicensesUpdated_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<LicensesUpdated_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<LicensesUpdated_t>(data);
			}
		}

		// Token: 0x0400077C RID: 1916
		internal const int CallbackId = 125;

		// Token: 0x0200024E RID: 590
		internal struct PackSmall
		{
			// Token: 0x06001D8A RID: 7562 RVA: 0x00063E4C File Offset: 0x0006204C
			public static implicit operator LicensesUpdated_t(LicensesUpdated_t.PackSmall d)
			{
				return default(LicensesUpdated_t);
			}
		}
	}
}
