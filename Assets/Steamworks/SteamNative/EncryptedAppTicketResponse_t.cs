using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000076 RID: 118
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EncryptedAppTicketResponse_t
	{
		// Token: 0x06000350 RID: 848 RVA: 0x000096D8 File Offset: 0x000078D8
		internal static EncryptedAppTicketResponse_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (EncryptedAppTicketResponse_t.PackSmall)Marshal.PtrToStructure(p, typeof(EncryptedAppTicketResponse_t.PackSmall));
			}
			return (EncryptedAppTicketResponse_t)Marshal.PtrToStructure(p, typeof(EncryptedAppTicketResponse_t));
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00009711 File Offset: 0x00007911
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(EncryptedAppTicketResponse_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(EncryptedAppTicketResponse_t));
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00009739 File Offset: 0x00007939
		internal static CallResult<EncryptedAppTicketResponse_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<EncryptedAppTicketResponse_t, bool> CallbackFunction)
		{
			return new CallResult<EncryptedAppTicketResponse_t>(steamworks, call, CallbackFunction, new CallResult<EncryptedAppTicketResponse_t>.ConvertFromPointer(EncryptedAppTicketResponse_t.FromPointer), EncryptedAppTicketResponse_t.StructSize(), 154);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000975C File Offset: 0x0000795C
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
						ResultA = new Callback.VTableWinThis.ResultD(EncryptedAppTicketResponse_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(EncryptedAppTicketResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(EncryptedAppTicketResponse_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(EncryptedAppTicketResponse_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(EncryptedAppTicketResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(EncryptedAppTicketResponse_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(EncryptedAppTicketResponse_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(EncryptedAppTicketResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(EncryptedAppTicketResponse_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(EncryptedAppTicketResponse_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(EncryptedAppTicketResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(EncryptedAppTicketResponse_t.OnGetSize)
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
				CallbackId = 154
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 154);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00009A62 File Offset: 0x00007C62
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			EncryptedAppTicketResponse_t.OnResult(param);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00009A6A File Offset: 0x00007C6A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			EncryptedAppTicketResponse_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00009A74 File Offset: 0x00007C74
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return EncryptedAppTicketResponse_t.OnGetSize();
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00009A7B File Offset: 0x00007C7B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return EncryptedAppTicketResponse_t.StructSize();
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00009A82 File Offset: 0x00007C82
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			EncryptedAppTicketResponse_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00009A94 File Offset: 0x00007C94
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			EncryptedAppTicketResponse_t data = EncryptedAppTicketResponse_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<EncryptedAppTicketResponse_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<EncryptedAppTicketResponse_t>(data);
			}
		}

		// Token: 0x040004A7 RID: 1191
		internal const int CallbackId = 154;

		// Token: 0x040004A8 RID: 1192
		internal Result Result;

		// Token: 0x0200019A RID: 410
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CD6 RID: 7382 RVA: 0x00061174 File Offset: 0x0005F374
			public static implicit operator EncryptedAppTicketResponse_t(EncryptedAppTicketResponse_t.PackSmall d)
			{
				return new EncryptedAppTicketResponse_t
				{
					Result = d.Result
				};
			}

			// Token: 0x04000950 RID: 2384
			internal Result Result;
		}
	}
}
