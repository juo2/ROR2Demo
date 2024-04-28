using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200012D RID: 301
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IPCFailure_t
	{
		// Token: 0x0600098C RID: 2444 RVA: 0x0003264C File Offset: 0x0003084C
		internal static IPCFailure_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (IPCFailure_t.PackSmall)Marshal.PtrToStructure(p, typeof(IPCFailure_t.PackSmall));
			}
			return (IPCFailure_t)Marshal.PtrToStructure(p, typeof(IPCFailure_t));
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00032685 File Offset: 0x00030885
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(IPCFailure_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(IPCFailure_t));
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x000326B0 File Offset: 0x000308B0
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
						ResultA = new Callback.VTableWinThis.ResultD(IPCFailure_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(IPCFailure_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(IPCFailure_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(IPCFailure_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(IPCFailure_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(IPCFailure_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(IPCFailure_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(IPCFailure_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(IPCFailure_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(IPCFailure_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(IPCFailure_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(IPCFailure_t.OnGetSize)
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
				CallbackId = 117
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 117);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x000329B0 File Offset: 0x00030BB0
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			IPCFailure_t.OnResult(param);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x000329B8 File Offset: 0x00030BB8
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			IPCFailure_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x000329C2 File Offset: 0x00030BC2
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return IPCFailure_t.OnGetSize();
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x000329C9 File Offset: 0x00030BC9
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return IPCFailure_t.StructSize();
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x000329D0 File Offset: 0x00030BD0
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			IPCFailure_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x000329E0 File Offset: 0x00030BE0
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			IPCFailure_t data = IPCFailure_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<IPCFailure_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<IPCFailure_t>(data);
			}
		}

		// Token: 0x0400077F RID: 1919
		internal const int CallbackId = 117;

		// Token: 0x04000780 RID: 1920
		internal byte FailureType;

		// Token: 0x02000251 RID: 593
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D8D RID: 7565 RVA: 0x00063E94 File Offset: 0x00062094
			public static implicit operator IPCFailure_t(IPCFailure_t.PackSmall d)
			{
				return new IPCFailure_t
				{
					FailureType = d.FailureType
				};
			}

			// Token: 0x04000B81 RID: 2945
			internal byte FailureType;
		}
	}
}
