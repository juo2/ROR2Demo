using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000120 RID: 288
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GSStatsStored_t
	{
		// Token: 0x06000916 RID: 2326 RVA: 0x0002F444 File Offset: 0x0002D644
		internal static GSStatsStored_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GSStatsStored_t.PackSmall)Marshal.PtrToStructure(p, typeof(GSStatsStored_t.PackSmall));
			}
			return (GSStatsStored_t)Marshal.PtrToStructure(p, typeof(GSStatsStored_t));
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0002F47D File Offset: 0x0002D67D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GSStatsStored_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GSStatsStored_t));
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0002F4A5 File Offset: 0x0002D6A5
		internal static CallResult<GSStatsStored_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<GSStatsStored_t, bool> CallbackFunction)
		{
			return new CallResult<GSStatsStored_t>(steamworks, call, CallbackFunction, new CallResult<GSStatsStored_t>.ConvertFromPointer(GSStatsStored_t.FromPointer), GSStatsStored_t.StructSize(), 1801);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0002F4C8 File Offset: 0x0002D6C8
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
						ResultA = new Callback.VTableWinThis.ResultD(GSStatsStored_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GSStatsStored_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GSStatsStored_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GSStatsStored_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GSStatsStored_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GSStatsStored_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GSStatsStored_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GSStatsStored_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GSStatsStored_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GSStatsStored_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GSStatsStored_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GSStatsStored_t.OnGetSize)
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
				CallbackId = 1801
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1801);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0002F7CE File Offset: 0x0002D9CE
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GSStatsStored_t.OnResult(param);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0002F7D6 File Offset: 0x0002D9D6
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GSStatsStored_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0002F7E0 File Offset: 0x0002D9E0
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GSStatsStored_t.OnGetSize();
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0002F7E7 File Offset: 0x0002D9E7
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GSStatsStored_t.StructSize();
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0002F7EE File Offset: 0x0002D9EE
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GSStatsStored_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0002F800 File Offset: 0x0002DA00
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GSStatsStored_t data = GSStatsStored_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GSStatsStored_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GSStatsStored_t>(data);
			}
		}

		// Token: 0x0400076C RID: 1900
		internal const int CallbackId = 1801;

		// Token: 0x0400076D RID: 1901
		internal Result Result;

		// Token: 0x0400076E RID: 1902
		internal ulong SteamIDUser;

		// Token: 0x02000244 RID: 580
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D80 RID: 7552 RVA: 0x00063D14 File Offset: 0x00061F14
			public static implicit operator GSStatsStored_t(GSStatsStored_t.PackSmall d)
			{
				return new GSStatsStored_t
				{
					Result = d.Result,
					SteamIDUser = d.SteamIDUser
				};
			}

			// Token: 0x04000B7B RID: 2939
			internal Result Result;

			// Token: 0x04000B7C RID: 2940
			internal ulong SteamIDUser;
		}
	}
}
