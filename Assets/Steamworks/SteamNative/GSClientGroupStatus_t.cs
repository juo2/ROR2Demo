using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200011B RID: 283
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GSClientGroupStatus_t
	{
		// Token: 0x060008E5 RID: 2277 RVA: 0x0002E08C File Offset: 0x0002C28C
		internal static GSClientGroupStatus_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GSClientGroupStatus_t.PackSmall)Marshal.PtrToStructure(p, typeof(GSClientGroupStatus_t.PackSmall));
			}
			return (GSClientGroupStatus_t)Marshal.PtrToStructure(p, typeof(GSClientGroupStatus_t));
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0002E0C5 File Offset: 0x0002C2C5
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GSClientGroupStatus_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GSClientGroupStatus_t));
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0002E0F0 File Offset: 0x0002C2F0
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
						ResultA = new Callback.VTableWinThis.ResultD(GSClientGroupStatus_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GSClientGroupStatus_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GSClientGroupStatus_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GSClientGroupStatus_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GSClientGroupStatus_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GSClientGroupStatus_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GSClientGroupStatus_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GSClientGroupStatus_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GSClientGroupStatus_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GSClientGroupStatus_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GSClientGroupStatus_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GSClientGroupStatus_t.OnGetSize)
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
				CallbackId = 208
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 208);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0002E3F6 File Offset: 0x0002C5F6
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GSClientGroupStatus_t.OnResult(param);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0002E3FE File Offset: 0x0002C5FE
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GSClientGroupStatus_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0002E408 File Offset: 0x0002C608
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GSClientGroupStatus_t.OnGetSize();
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0002E40F File Offset: 0x0002C60F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GSClientGroupStatus_t.StructSize();
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0002E416 File Offset: 0x0002C616
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GSClientGroupStatus_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0002E428 File Offset: 0x0002C628
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GSClientGroupStatus_t data = GSClientGroupStatus_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GSClientGroupStatus_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GSClientGroupStatus_t>(data);
			}
		}

		// Token: 0x04000754 RID: 1876
		internal const int CallbackId = 208;

		// Token: 0x04000755 RID: 1877
		internal ulong SteamIDUser;

		// Token: 0x04000756 RID: 1878
		internal ulong SteamIDGroup;

		// Token: 0x04000757 RID: 1879
		[MarshalAs(UnmanagedType.I1)]
		internal bool Member;

		// Token: 0x04000758 RID: 1880
		[MarshalAs(UnmanagedType.I1)]
		internal bool Officer;

		// Token: 0x0200023F RID: 575
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D7B RID: 7547 RVA: 0x00063BA8 File Offset: 0x00061DA8
			public static implicit operator GSClientGroupStatus_t(GSClientGroupStatus_t.PackSmall d)
			{
				return new GSClientGroupStatus_t
				{
					SteamIDUser = d.SteamIDUser,
					SteamIDGroup = d.SteamIDGroup,
					Member = d.Member,
					Officer = d.Officer
				};
			}

			// Token: 0x04000B68 RID: 2920
			internal ulong SteamIDUser;

			// Token: 0x04000B69 RID: 2921
			internal ulong SteamIDGroup;

			// Token: 0x04000B6A RID: 2922
			[MarshalAs(UnmanagedType.I1)]
			internal bool Member;

			// Token: 0x04000B6B RID: 2923
			[MarshalAs(UnmanagedType.I1)]
			internal bool Officer;
		}
	}
}
