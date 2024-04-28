using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000071 RID: 113
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamServerConnectFailure_t
	{
		// Token: 0x06000323 RID: 803 RVA: 0x000083B9 File Offset: 0x000065B9
		internal static SteamServerConnectFailure_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamServerConnectFailure_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamServerConnectFailure_t.PackSmall));
			}
			return (SteamServerConnectFailure_t)Marshal.PtrToStructure(p, typeof(SteamServerConnectFailure_t));
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000083F2 File Offset: 0x000065F2
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamServerConnectFailure_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamServerConnectFailure_t));
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000841C File Offset: 0x0000661C
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
						ResultA = new Callback.VTableWinThis.ResultD(SteamServerConnectFailure_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SteamServerConnectFailure_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SteamServerConnectFailure_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SteamServerConnectFailure_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SteamServerConnectFailure_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SteamServerConnectFailure_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SteamServerConnectFailure_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SteamServerConnectFailure_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SteamServerConnectFailure_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SteamServerConnectFailure_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SteamServerConnectFailure_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SteamServerConnectFailure_t.OnGetSize)
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
				CallbackId = 102
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 102);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000871C File Offset: 0x0000691C
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SteamServerConnectFailure_t.OnResult(param);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00008724 File Offset: 0x00006924
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SteamServerConnectFailure_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000872E File Offset: 0x0000692E
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SteamServerConnectFailure_t.OnGetSize();
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00008735 File Offset: 0x00006935
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SteamServerConnectFailure_t.StructSize();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000873C File Offset: 0x0000693C
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SteamServerConnectFailure_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000874C File Offset: 0x0000694C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SteamServerConnectFailure_t data = SteamServerConnectFailure_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SteamServerConnectFailure_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SteamServerConnectFailure_t>(data);
			}
		}

		// Token: 0x04000494 RID: 1172
		internal const int CallbackId = 102;

		// Token: 0x04000495 RID: 1173
		internal Result Result;

		// Token: 0x04000496 RID: 1174
		[MarshalAs(UnmanagedType.I1)]
		internal bool StillRetrying;

		// Token: 0x02000195 RID: 405
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CD1 RID: 7377 RVA: 0x00061048 File Offset: 0x0005F248
			public static implicit operator SteamServerConnectFailure_t(SteamServerConnectFailure_t.PackSmall d)
			{
				return new SteamServerConnectFailure_t
				{
					Result = d.Result,
					StillRetrying = d.StillRetrying
				};
			}

			// Token: 0x04000942 RID: 2370
			internal Result Result;

			// Token: 0x04000943 RID: 2371
			[MarshalAs(UnmanagedType.I1)]
			internal bool StillRetrying;
		}
	}
}
