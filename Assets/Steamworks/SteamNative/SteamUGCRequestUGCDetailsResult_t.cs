using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000E4 RID: 228
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamUGCRequestUGCDetailsResult_t
	{
		// Token: 0x06000701 RID: 1793 RVA: 0x000218EC File Offset: 0x0001FAEC
		internal static SteamUGCRequestUGCDetailsResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamUGCRequestUGCDetailsResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamUGCRequestUGCDetailsResult_t.PackSmall));
			}
			return (SteamUGCRequestUGCDetailsResult_t)Marshal.PtrToStructure(p, typeof(SteamUGCRequestUGCDetailsResult_t));
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00021925 File Offset: 0x0001FB25
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamUGCRequestUGCDetailsResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamUGCRequestUGCDetailsResult_t));
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00021950 File Offset: 0x0001FB50
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
						ResultA = new Callback.VTableWinThis.ResultD(SteamUGCRequestUGCDetailsResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SteamUGCRequestUGCDetailsResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SteamUGCRequestUGCDetailsResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SteamUGCRequestUGCDetailsResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SteamUGCRequestUGCDetailsResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SteamUGCRequestUGCDetailsResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SteamUGCRequestUGCDetailsResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SteamUGCRequestUGCDetailsResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SteamUGCRequestUGCDetailsResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SteamUGCRequestUGCDetailsResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SteamUGCRequestUGCDetailsResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SteamUGCRequestUGCDetailsResult_t.OnGetSize)
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
				CallbackId = 3402
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3402);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00021C56 File Offset: 0x0001FE56
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SteamUGCRequestUGCDetailsResult_t.OnResult(param);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00021C5E File Offset: 0x0001FE5E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SteamUGCRequestUGCDetailsResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00021C68 File Offset: 0x0001FE68
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SteamUGCRequestUGCDetailsResult_t.OnGetSize();
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00021C6F File Offset: 0x0001FE6F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SteamUGCRequestUGCDetailsResult_t.StructSize();
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00021C76 File Offset: 0x0001FE76
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SteamUGCRequestUGCDetailsResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00021C88 File Offset: 0x0001FE88
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SteamUGCRequestUGCDetailsResult_t data = SteamUGCRequestUGCDetailsResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SteamUGCRequestUGCDetailsResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SteamUGCRequestUGCDetailsResult_t>(data);
			}
		}

		// Token: 0x0400067E RID: 1662
		internal const int CallbackId = 3402;

		// Token: 0x0400067F RID: 1663
		internal SteamUGCDetails_t Details;

		// Token: 0x04000680 RID: 1664
		[MarshalAs(UnmanagedType.I1)]
		internal bool CachedData;

		// Token: 0x02000208 RID: 520
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D44 RID: 7492 RVA: 0x00062E5C File Offset: 0x0006105C
			public static implicit operator SteamUGCRequestUGCDetailsResult_t(SteamUGCRequestUGCDetailsResult_t.PackSmall d)
			{
				return new SteamUGCRequestUGCDetailsResult_t
				{
					Details = d.Details,
					CachedData = d.CachedData
				};
			}

			// Token: 0x04000AC5 RID: 2757
			internal SteamUGCDetails_t Details;

			// Token: 0x04000AC6 RID: 2758
			[MarshalAs(UnmanagedType.I1)]
			internal bool CachedData;
		}
	}
}
