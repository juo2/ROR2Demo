using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000124 RID: 292
	internal struct SteamParentalSettingsChanged_t
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x000303C4 File Offset: 0x0002E5C4
		internal static SteamParentalSettingsChanged_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamParentalSettingsChanged_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamParentalSettingsChanged_t.PackSmall));
			}
			return (SteamParentalSettingsChanged_t)Marshal.PtrToStructure(p, typeof(SteamParentalSettingsChanged_t));
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000303FD File Offset: 0x0002E5FD
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamParentalSettingsChanged_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamParentalSettingsChanged_t));
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00030428 File Offset: 0x0002E628
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
						ResultA = new Callback.VTableWinThis.ResultD(SteamParentalSettingsChanged_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SteamParentalSettingsChanged_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SteamParentalSettingsChanged_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SteamParentalSettingsChanged_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SteamParentalSettingsChanged_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SteamParentalSettingsChanged_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SteamParentalSettingsChanged_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SteamParentalSettingsChanged_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SteamParentalSettingsChanged_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SteamParentalSettingsChanged_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SteamParentalSettingsChanged_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SteamParentalSettingsChanged_t.OnGetSize)
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
				CallbackId = 5001
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 5001);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0003072E File Offset: 0x0002E92E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SteamParentalSettingsChanged_t.OnResult(param);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00030736 File Offset: 0x0002E936
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SteamParentalSettingsChanged_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00030740 File Offset: 0x0002E940
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SteamParentalSettingsChanged_t.OnGetSize();
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00030747 File Offset: 0x0002E947
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SteamParentalSettingsChanged_t.StructSize();
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0003074E File Offset: 0x0002E94E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SteamParentalSettingsChanged_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x00030760 File Offset: 0x0002E960
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SteamParentalSettingsChanged_t data = SteamParentalSettingsChanged_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SteamParentalSettingsChanged_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SteamParentalSettingsChanged_t>(data);
			}
		}

		// Token: 0x04000775 RID: 1909
		internal const int CallbackId = 5001;

		// Token: 0x02000248 RID: 584
		internal struct PackSmall
		{
			// Token: 0x06001D84 RID: 7556 RVA: 0x00063DB0 File Offset: 0x00061FB0
			public static implicit operator SteamParentalSettingsChanged_t(SteamParentalSettingsChanged_t.PackSmall d)
			{
				return default(SteamParentalSettingsChanged_t);
			}
		}
	}
}
