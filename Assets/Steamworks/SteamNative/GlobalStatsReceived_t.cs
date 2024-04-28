using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000CB RID: 203
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GlobalStatsReceived_t
	{
		// Token: 0x06000640 RID: 1600 RVA: 0x0001C9C8 File Offset: 0x0001ABC8
		internal static GlobalStatsReceived_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GlobalStatsReceived_t.PackSmall)Marshal.PtrToStructure(p, typeof(GlobalStatsReceived_t.PackSmall));
			}
			return (GlobalStatsReceived_t)Marshal.PtrToStructure(p, typeof(GlobalStatsReceived_t));
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001CA01 File Offset: 0x0001AC01
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GlobalStatsReceived_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GlobalStatsReceived_t));
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001CA29 File Offset: 0x0001AC29
		internal static CallResult<GlobalStatsReceived_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<GlobalStatsReceived_t, bool> CallbackFunction)
		{
			return new CallResult<GlobalStatsReceived_t>(steamworks, call, CallbackFunction, new CallResult<GlobalStatsReceived_t>.ConvertFromPointer(GlobalStatsReceived_t.FromPointer), GlobalStatsReceived_t.StructSize(), 1112);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001CA4C File Offset: 0x0001AC4C
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
						ResultA = new Callback.VTableWinThis.ResultD(GlobalStatsReceived_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GlobalStatsReceived_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GlobalStatsReceived_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GlobalStatsReceived_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GlobalStatsReceived_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GlobalStatsReceived_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GlobalStatsReceived_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GlobalStatsReceived_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GlobalStatsReceived_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GlobalStatsReceived_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GlobalStatsReceived_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GlobalStatsReceived_t.OnGetSize)
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
				CallbackId = 1112
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1112);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001CD52 File Offset: 0x0001AF52
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GlobalStatsReceived_t.OnResult(param);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001CD5A File Offset: 0x0001AF5A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GlobalStatsReceived_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001CD64 File Offset: 0x0001AF64
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GlobalStatsReceived_t.OnGetSize();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001CD6B File Offset: 0x0001AF6B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GlobalStatsReceived_t.StructSize();
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001CD72 File Offset: 0x0001AF72
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GlobalStatsReceived_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001CD84 File Offset: 0x0001AF84
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GlobalStatsReceived_t data = GlobalStatsReceived_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GlobalStatsReceived_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GlobalStatsReceived_t>(data);
			}
		}

		// Token: 0x0400060B RID: 1547
		internal const int CallbackId = 1112;

		// Token: 0x0400060C RID: 1548
		internal ulong GameID;

		// Token: 0x0400060D RID: 1549
		internal Result Result;

		// Token: 0x020001EF RID: 495
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D2B RID: 7467 RVA: 0x0006274C File Offset: 0x0006094C
			public static implicit operator GlobalStatsReceived_t(GlobalStatsReceived_t.PackSmall d)
			{
				return new GlobalStatsReceived_t
				{
					GameID = d.GameID,
					Result = d.Result
				};
			}

			// Token: 0x04000A66 RID: 2662
			internal ulong GameID;

			// Token: 0x04000A67 RID: 2663
			internal Result Result;
		}
	}
}
