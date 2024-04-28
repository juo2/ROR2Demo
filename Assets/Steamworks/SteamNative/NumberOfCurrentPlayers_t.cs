using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000C5 RID: 197
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct NumberOfCurrentPlayers_t
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x0001B258 File Offset: 0x00019458
		internal static NumberOfCurrentPlayers_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (NumberOfCurrentPlayers_t.PackSmall)Marshal.PtrToStructure(p, typeof(NumberOfCurrentPlayers_t.PackSmall));
			}
			return (NumberOfCurrentPlayers_t)Marshal.PtrToStructure(p, typeof(NumberOfCurrentPlayers_t));
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001B291 File Offset: 0x00019491
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(NumberOfCurrentPlayers_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(NumberOfCurrentPlayers_t));
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001B2B9 File Offset: 0x000194B9
		internal static CallResult<NumberOfCurrentPlayers_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<NumberOfCurrentPlayers_t, bool> CallbackFunction)
		{
			return new CallResult<NumberOfCurrentPlayers_t>(steamworks, call, CallbackFunction, new CallResult<NumberOfCurrentPlayers_t>.ConvertFromPointer(NumberOfCurrentPlayers_t.FromPointer), NumberOfCurrentPlayers_t.StructSize(), 1107);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001B2DC File Offset: 0x000194DC
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
						ResultA = new Callback.VTableWinThis.ResultD(NumberOfCurrentPlayers_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(NumberOfCurrentPlayers_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(NumberOfCurrentPlayers_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(NumberOfCurrentPlayers_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(NumberOfCurrentPlayers_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(NumberOfCurrentPlayers_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(NumberOfCurrentPlayers_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(NumberOfCurrentPlayers_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(NumberOfCurrentPlayers_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(NumberOfCurrentPlayers_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(NumberOfCurrentPlayers_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(NumberOfCurrentPlayers_t.OnGetSize)
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
				CallbackId = 1107
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1107);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001B5E2 File Offset: 0x000197E2
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			NumberOfCurrentPlayers_t.OnResult(param);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001B5EA File Offset: 0x000197EA
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			NumberOfCurrentPlayers_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001B5F4 File Offset: 0x000197F4
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return NumberOfCurrentPlayers_t.OnGetSize();
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001B5FB File Offset: 0x000197FB
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return NumberOfCurrentPlayers_t.StructSize();
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001B602 File Offset: 0x00019802
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			NumberOfCurrentPlayers_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001B614 File Offset: 0x00019814
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			NumberOfCurrentPlayers_t data = NumberOfCurrentPlayers_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<NumberOfCurrentPlayers_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<NumberOfCurrentPlayers_t>(data);
			}
		}

		// Token: 0x040005F7 RID: 1527
		internal const int CallbackId = 1107;

		// Token: 0x040005F8 RID: 1528
		internal byte Success;

		// Token: 0x040005F9 RID: 1529
		internal int CPlayers;

		// Token: 0x020001E9 RID: 489
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D25 RID: 7461 RVA: 0x0006260C File Offset: 0x0006080C
			public static implicit operator NumberOfCurrentPlayers_t(NumberOfCurrentPlayers_t.PackSmall d)
			{
				return new NumberOfCurrentPlayers_t
				{
					Success = d.Success,
					CPlayers = d.CPlayers
				};
			}

			// Token: 0x04000A58 RID: 2648
			internal byte Success;

			// Token: 0x04000A59 RID: 2649
			internal int CPlayers;
		}
	}
}
