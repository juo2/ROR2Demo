using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000EB RID: 235
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StartPlaytimeTrackingResult_t
	{
		// Token: 0x06000745 RID: 1861 RVA: 0x00023474 File Offset: 0x00021674
		internal static StartPlaytimeTrackingResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (StartPlaytimeTrackingResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(StartPlaytimeTrackingResult_t.PackSmall));
			}
			return (StartPlaytimeTrackingResult_t)Marshal.PtrToStructure(p, typeof(StartPlaytimeTrackingResult_t));
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x000234AD File Offset: 0x000216AD
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(StartPlaytimeTrackingResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(StartPlaytimeTrackingResult_t));
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x000234D5 File Offset: 0x000216D5
		internal static CallResult<StartPlaytimeTrackingResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<StartPlaytimeTrackingResult_t, bool> CallbackFunction)
		{
			return new CallResult<StartPlaytimeTrackingResult_t>(steamworks, call, CallbackFunction, new CallResult<StartPlaytimeTrackingResult_t>.ConvertFromPointer(StartPlaytimeTrackingResult_t.FromPointer), StartPlaytimeTrackingResult_t.StructSize(), 3410);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000234F8 File Offset: 0x000216F8
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
						ResultA = new Callback.VTableWinThis.ResultD(StartPlaytimeTrackingResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(StartPlaytimeTrackingResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(StartPlaytimeTrackingResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(StartPlaytimeTrackingResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(StartPlaytimeTrackingResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(StartPlaytimeTrackingResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(StartPlaytimeTrackingResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(StartPlaytimeTrackingResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(StartPlaytimeTrackingResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(StartPlaytimeTrackingResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(StartPlaytimeTrackingResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(StartPlaytimeTrackingResult_t.OnGetSize)
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
				CallbackId = 3410
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3410);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000237FE File Offset: 0x000219FE
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			StartPlaytimeTrackingResult_t.OnResult(param);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00023806 File Offset: 0x00021A06
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			StartPlaytimeTrackingResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00023810 File Offset: 0x00021A10
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return StartPlaytimeTrackingResult_t.OnGetSize();
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00023817 File Offset: 0x00021A17
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return StartPlaytimeTrackingResult_t.StructSize();
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0002381E File Offset: 0x00021A1E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			StartPlaytimeTrackingResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00023830 File Offset: 0x00021A30
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			StartPlaytimeTrackingResult_t data = StartPlaytimeTrackingResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<StartPlaytimeTrackingResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<StartPlaytimeTrackingResult_t>(data);
			}
		}

		// Token: 0x0400069B RID: 1691
		internal const int CallbackId = 3410;

		// Token: 0x0400069C RID: 1692
		internal Result Result;

		// Token: 0x0200020F RID: 527
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D4B RID: 7499 RVA: 0x00063024 File Offset: 0x00061224
			public static implicit operator StartPlaytimeTrackingResult_t(StartPlaytimeTrackingResult_t.PackSmall d)
			{
				return new StartPlaytimeTrackingResult_t
				{
					Result = d.Result
				};
			}

			// Token: 0x04000ADB RID: 2779
			internal Result Result;
		}
	}
}
