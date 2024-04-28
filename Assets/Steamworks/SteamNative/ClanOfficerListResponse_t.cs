using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000081 RID: 129
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct ClanOfficerListResponse_t
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x0000BA70 File Offset: 0x00009C70
		internal static ClanOfficerListResponse_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (ClanOfficerListResponse_t.PackSmall)Marshal.PtrToStructure(p, typeof(ClanOfficerListResponse_t.PackSmall));
			}
			return (ClanOfficerListResponse_t)Marshal.PtrToStructure(p, typeof(ClanOfficerListResponse_t));
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000BAA9 File Offset: 0x00009CA9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(ClanOfficerListResponse_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(ClanOfficerListResponse_t));
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000BAD1 File Offset: 0x00009CD1
		internal static CallResult<ClanOfficerListResponse_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<ClanOfficerListResponse_t, bool> CallbackFunction)
		{
			return new CallResult<ClanOfficerListResponse_t>(steamworks, call, CallbackFunction, new CallResult<ClanOfficerListResponse_t>.ConvertFromPointer(ClanOfficerListResponse_t.FromPointer), ClanOfficerListResponse_t.StructSize(), 335);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000BAF4 File Offset: 0x00009CF4
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
						ResultA = new Callback.VTableWinThis.ResultD(ClanOfficerListResponse_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(ClanOfficerListResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(ClanOfficerListResponse_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(ClanOfficerListResponse_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(ClanOfficerListResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(ClanOfficerListResponse_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(ClanOfficerListResponse_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(ClanOfficerListResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(ClanOfficerListResponse_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(ClanOfficerListResponse_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(ClanOfficerListResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(ClanOfficerListResponse_t.OnGetSize)
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
				CallbackId = 335
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 335);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000BDFA File Offset: 0x00009FFA
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			ClanOfficerListResponse_t.OnResult(param);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000BE02 File Offset: 0x0000A002
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			ClanOfficerListResponse_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000BE0C File Offset: 0x0000A00C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return ClanOfficerListResponse_t.OnGetSize();
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000BE13 File Offset: 0x0000A013
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return ClanOfficerListResponse_t.StructSize();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000BE1A File Offset: 0x0000A01A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			ClanOfficerListResponse_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000BE2C File Offset: 0x0000A02C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			ClanOfficerListResponse_t data = ClanOfficerListResponse_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<ClanOfficerListResponse_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<ClanOfficerListResponse_t>(data);
			}
		}

		// Token: 0x040004C7 RID: 1223
		internal const int CallbackId = 335;

		// Token: 0x040004C8 RID: 1224
		internal ulong SteamIDClan;

		// Token: 0x040004C9 RID: 1225
		internal int COfficers;

		// Token: 0x040004CA RID: 1226
		internal byte Success;

		// Token: 0x020001A5 RID: 421
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CE1 RID: 7393 RVA: 0x00061398 File Offset: 0x0005F598
			public static implicit operator ClanOfficerListResponse_t(ClanOfficerListResponse_t.PackSmall d)
			{
				return new ClanOfficerListResponse_t
				{
					SteamIDClan = d.SteamIDClan,
					COfficers = d.COfficers,
					Success = d.Success
				};
			}

			// Token: 0x04000967 RID: 2407
			internal ulong SteamIDClan;

			// Token: 0x04000968 RID: 2408
			internal int COfficers;

			// Token: 0x04000969 RID: 2409
			internal byte Success;
		}
	}
}
