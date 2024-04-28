using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000075 RID: 117
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct MicroTxnAuthorizationResponse_t
	{
		// Token: 0x06000347 RID: 839 RVA: 0x00009300 File Offset: 0x00007500
		internal static MicroTxnAuthorizationResponse_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (MicroTxnAuthorizationResponse_t.PackSmall)Marshal.PtrToStructure(p, typeof(MicroTxnAuthorizationResponse_t.PackSmall));
			}
			return (MicroTxnAuthorizationResponse_t)Marshal.PtrToStructure(p, typeof(MicroTxnAuthorizationResponse_t));
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00009339 File Offset: 0x00007539
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(MicroTxnAuthorizationResponse_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(MicroTxnAuthorizationResponse_t));
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00009364 File Offset: 0x00007564
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
						ResultA = new Callback.VTableWinThis.ResultD(MicroTxnAuthorizationResponse_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(MicroTxnAuthorizationResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(MicroTxnAuthorizationResponse_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(MicroTxnAuthorizationResponse_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(MicroTxnAuthorizationResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(MicroTxnAuthorizationResponse_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(MicroTxnAuthorizationResponse_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(MicroTxnAuthorizationResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(MicroTxnAuthorizationResponse_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(MicroTxnAuthorizationResponse_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(MicroTxnAuthorizationResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(MicroTxnAuthorizationResponse_t.OnGetSize)
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
				CallbackId = 152
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 152);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000966A File Offset: 0x0000786A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			MicroTxnAuthorizationResponse_t.OnResult(param);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00009672 File Offset: 0x00007872
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			MicroTxnAuthorizationResponse_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000967C File Offset: 0x0000787C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return MicroTxnAuthorizationResponse_t.OnGetSize();
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00009683 File Offset: 0x00007883
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return MicroTxnAuthorizationResponse_t.StructSize();
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000968A File Offset: 0x0000788A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			MicroTxnAuthorizationResponse_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000969C File Offset: 0x0000789C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			MicroTxnAuthorizationResponse_t data = MicroTxnAuthorizationResponse_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<MicroTxnAuthorizationResponse_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<MicroTxnAuthorizationResponse_t>(data);
			}
		}

		// Token: 0x040004A3 RID: 1187
		internal const int CallbackId = 152;

		// Token: 0x040004A4 RID: 1188
		internal uint AppID;

		// Token: 0x040004A5 RID: 1189
		internal ulong OrderID;

		// Token: 0x040004A6 RID: 1190
		internal byte Authorized;

		// Token: 0x02000199 RID: 409
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CD5 RID: 7381 RVA: 0x00061134 File Offset: 0x0005F334
			public static implicit operator MicroTxnAuthorizationResponse_t(MicroTxnAuthorizationResponse_t.PackSmall d)
			{
				return new MicroTxnAuthorizationResponse_t
				{
					AppID = d.AppID,
					OrderID = d.OrderID,
					Authorized = d.Authorized
				};
			}

			// Token: 0x0400094D RID: 2381
			internal uint AppID;

			// Token: 0x0400094E RID: 2382
			internal ulong OrderID;

			// Token: 0x0400094F RID: 2383
			internal byte Authorized;
		}
	}
}
