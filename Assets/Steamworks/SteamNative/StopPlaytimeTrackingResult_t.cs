using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000EC RID: 236
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StopPlaytimeTrackingResult_t
	{
		// Token: 0x0600074F RID: 1871 RVA: 0x0002386C File Offset: 0x00021A6C
		internal static StopPlaytimeTrackingResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (StopPlaytimeTrackingResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(StopPlaytimeTrackingResult_t.PackSmall));
			}
			return (StopPlaytimeTrackingResult_t)Marshal.PtrToStructure(p, typeof(StopPlaytimeTrackingResult_t));
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000238A5 File Offset: 0x00021AA5
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(StopPlaytimeTrackingResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(StopPlaytimeTrackingResult_t));
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000238CD File Offset: 0x00021ACD
		internal static CallResult<StopPlaytimeTrackingResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<StopPlaytimeTrackingResult_t, bool> CallbackFunction)
		{
			return new CallResult<StopPlaytimeTrackingResult_t>(steamworks, call, CallbackFunction, new CallResult<StopPlaytimeTrackingResult_t>.ConvertFromPointer(StopPlaytimeTrackingResult_t.FromPointer), StopPlaytimeTrackingResult_t.StructSize(), 3411);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000238F0 File Offset: 0x00021AF0
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
						ResultA = new Callback.VTableWinThis.ResultD(StopPlaytimeTrackingResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(StopPlaytimeTrackingResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(StopPlaytimeTrackingResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(StopPlaytimeTrackingResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(StopPlaytimeTrackingResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(StopPlaytimeTrackingResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(StopPlaytimeTrackingResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(StopPlaytimeTrackingResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(StopPlaytimeTrackingResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(StopPlaytimeTrackingResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(StopPlaytimeTrackingResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(StopPlaytimeTrackingResult_t.OnGetSize)
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
				CallbackId = 3411
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3411);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00023BF6 File Offset: 0x00021DF6
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			StopPlaytimeTrackingResult_t.OnResult(param);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00023BFE File Offset: 0x00021DFE
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			StopPlaytimeTrackingResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00023C08 File Offset: 0x00021E08
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return StopPlaytimeTrackingResult_t.OnGetSize();
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00023C0F File Offset: 0x00021E0F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return StopPlaytimeTrackingResult_t.StructSize();
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00023C16 File Offset: 0x00021E16
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			StopPlaytimeTrackingResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00023C28 File Offset: 0x00021E28
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			StopPlaytimeTrackingResult_t data = StopPlaytimeTrackingResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<StopPlaytimeTrackingResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<StopPlaytimeTrackingResult_t>(data);
			}
		}

		// Token: 0x0400069D RID: 1693
		internal const int CallbackId = 3411;

		// Token: 0x0400069E RID: 1694
		internal Result Result;

		// Token: 0x02000210 RID: 528
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D4C RID: 7500 RVA: 0x00063048 File Offset: 0x00061248
			public static implicit operator StopPlaytimeTrackingResult_t(StopPlaytimeTrackingResult_t.PackSmall d)
			{
				return new StopPlaytimeTrackingResult_t
				{
					Result = d.Result
				};
			}

			// Token: 0x04000ADC RID: 2780
			internal Result Result;
		}
	}
}
