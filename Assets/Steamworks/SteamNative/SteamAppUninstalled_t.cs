using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000F4 RID: 244
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamAppUninstalled_t
	{
		// Token: 0x0600079E RID: 1950 RVA: 0x0002580C File Offset: 0x00023A0C
		internal static SteamAppUninstalled_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamAppUninstalled_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamAppUninstalled_t.PackSmall));
			}
			return (SteamAppUninstalled_t)Marshal.PtrToStructure(p, typeof(SteamAppUninstalled_t));
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00025845 File Offset: 0x00023A45
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamAppUninstalled_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamAppUninstalled_t));
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00025870 File Offset: 0x00023A70
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
						ResultA = new Callback.VTableWinThis.ResultD(SteamAppUninstalled_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SteamAppUninstalled_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SteamAppUninstalled_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SteamAppUninstalled_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SteamAppUninstalled_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SteamAppUninstalled_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SteamAppUninstalled_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SteamAppUninstalled_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SteamAppUninstalled_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SteamAppUninstalled_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SteamAppUninstalled_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SteamAppUninstalled_t.OnGetSize)
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
				CallbackId = 3902
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3902);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00025B76 File Offset: 0x00023D76
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SteamAppUninstalled_t.OnResult(param);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00025B7E File Offset: 0x00023D7E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SteamAppUninstalled_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00025B88 File Offset: 0x00023D88
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SteamAppUninstalled_t.OnGetSize();
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00025B8F File Offset: 0x00023D8F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SteamAppUninstalled_t.StructSize();
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00025B96 File Offset: 0x00023D96
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SteamAppUninstalled_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00025BA8 File Offset: 0x00023DA8
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SteamAppUninstalled_t data = SteamAppUninstalled_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SteamAppUninstalled_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SteamAppUninstalled_t>(data);
			}
		}

		// Token: 0x040006BA RID: 1722
		internal const int CallbackId = 3902;

		// Token: 0x040006BB RID: 1723
		internal uint AppID;

		// Token: 0x02000218 RID: 536
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D54 RID: 7508 RVA: 0x00063218 File Offset: 0x00061418
			public static implicit operator SteamAppUninstalled_t(SteamAppUninstalled_t.PackSmall d)
			{
				return new SteamAppUninstalled_t
				{
					AppID = d.AppID
				};
			}

			// Token: 0x04000AF1 RID: 2801
			internal uint AppID;
		}
	}
}
