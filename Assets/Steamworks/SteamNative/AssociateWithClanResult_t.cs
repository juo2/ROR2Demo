using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200011D RID: 285
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AssociateWithClanResult_t
	{
		// Token: 0x060008F8 RID: 2296 RVA: 0x0002E85C File Offset: 0x0002CA5C
		internal static AssociateWithClanResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (AssociateWithClanResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(AssociateWithClanResult_t.PackSmall));
			}
			return (AssociateWithClanResult_t)Marshal.PtrToStructure(p, typeof(AssociateWithClanResult_t));
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0002E895 File Offset: 0x0002CA95
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(AssociateWithClanResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(AssociateWithClanResult_t));
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0002E8BD File Offset: 0x0002CABD
		internal static CallResult<AssociateWithClanResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<AssociateWithClanResult_t, bool> CallbackFunction)
		{
			return new CallResult<AssociateWithClanResult_t>(steamworks, call, CallbackFunction, new CallResult<AssociateWithClanResult_t>.ConvertFromPointer(AssociateWithClanResult_t.FromPointer), AssociateWithClanResult_t.StructSize(), 210);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002E8E0 File Offset: 0x0002CAE0
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
						ResultA = new Callback.VTableWinThis.ResultD(AssociateWithClanResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(AssociateWithClanResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(AssociateWithClanResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(AssociateWithClanResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(AssociateWithClanResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(AssociateWithClanResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(AssociateWithClanResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(AssociateWithClanResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(AssociateWithClanResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(AssociateWithClanResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(AssociateWithClanResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(AssociateWithClanResult_t.OnGetSize)
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
				CallbackId = 210
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 210);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0002EBE6 File Offset: 0x0002CDE6
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			AssociateWithClanResult_t.OnResult(param);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0002EBEE File Offset: 0x0002CDEE
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			AssociateWithClanResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0002EBF8 File Offset: 0x0002CDF8
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return AssociateWithClanResult_t.OnGetSize();
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0002EBFF File Offset: 0x0002CDFF
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return AssociateWithClanResult_t.StructSize();
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0002EC06 File Offset: 0x0002CE06
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			AssociateWithClanResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0002EC18 File Offset: 0x0002CE18
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			AssociateWithClanResult_t data = AssociateWithClanResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<AssociateWithClanResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<AssociateWithClanResult_t>(data);
			}
		}

		// Token: 0x04000761 RID: 1889
		internal const int CallbackId = 210;

		// Token: 0x04000762 RID: 1890
		internal Result Result;

		// Token: 0x02000241 RID: 577
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D7D RID: 7549 RVA: 0x00063C68 File Offset: 0x00061E68
			public static implicit operator AssociateWithClanResult_t(AssociateWithClanResult_t.PackSmall d)
			{
				return new AssociateWithClanResult_t
				{
					Result = d.Result
				};
			}

			// Token: 0x04000B73 RID: 2931
			internal Result Result;
		}
	}
}
