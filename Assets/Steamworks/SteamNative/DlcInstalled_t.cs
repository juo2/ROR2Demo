using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000CC RID: 204
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DlcInstalled_t
	{
		// Token: 0x0600064A RID: 1610 RVA: 0x0001CDC0 File Offset: 0x0001AFC0
		internal static DlcInstalled_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (DlcInstalled_t.PackSmall)Marshal.PtrToStructure(p, typeof(DlcInstalled_t.PackSmall));
			}
			return (DlcInstalled_t)Marshal.PtrToStructure(p, typeof(DlcInstalled_t));
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001CDF9 File Offset: 0x0001AFF9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(DlcInstalled_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(DlcInstalled_t));
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001CE24 File Offset: 0x0001B024
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
						ResultA = new Callback.VTableWinThis.ResultD(DlcInstalled_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(DlcInstalled_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(DlcInstalled_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(DlcInstalled_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(DlcInstalled_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(DlcInstalled_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(DlcInstalled_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(DlcInstalled_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(DlcInstalled_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(DlcInstalled_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(DlcInstalled_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(DlcInstalled_t.OnGetSize)
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
				CallbackId = 1005
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1005);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0001D12A File Offset: 0x0001B32A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			DlcInstalled_t.OnResult(param);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0001D132 File Offset: 0x0001B332
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			DlcInstalled_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0001D13C File Offset: 0x0001B33C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return DlcInstalled_t.OnGetSize();
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001D143 File Offset: 0x0001B343
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return DlcInstalled_t.StructSize();
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001D14A File Offset: 0x0001B34A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			DlcInstalled_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001D15C File Offset: 0x0001B35C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			DlcInstalled_t data = DlcInstalled_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<DlcInstalled_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<DlcInstalled_t>(data);
			}
		}

		// Token: 0x0400060E RID: 1550
		internal const int CallbackId = 1005;

		// Token: 0x0400060F RID: 1551
		internal uint AppID;

		// Token: 0x020001F0 RID: 496
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D2C RID: 7468 RVA: 0x0006277C File Offset: 0x0006097C
			public static implicit operator DlcInstalled_t(DlcInstalled_t.PackSmall d)
			{
				return new DlcInstalled_t
				{
					AppID = d.AppID
				};
			}

			// Token: 0x04000A68 RID: 2664
			internal uint AppID;
		}
	}
}
