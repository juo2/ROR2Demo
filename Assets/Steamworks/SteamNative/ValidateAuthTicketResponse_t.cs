using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000074 RID: 116
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct ValidateAuthTicketResponse_t
	{
		// Token: 0x0600033E RID: 830 RVA: 0x00008F28 File Offset: 0x00007128
		internal static ValidateAuthTicketResponse_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (ValidateAuthTicketResponse_t.PackSmall)Marshal.PtrToStructure(p, typeof(ValidateAuthTicketResponse_t.PackSmall));
			}
			return (ValidateAuthTicketResponse_t)Marshal.PtrToStructure(p, typeof(ValidateAuthTicketResponse_t));
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00008F61 File Offset: 0x00007161
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(ValidateAuthTicketResponse_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(ValidateAuthTicketResponse_t));
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00008F8C File Offset: 0x0000718C
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
						ResultA = new Callback.VTableWinThis.ResultD(ValidateAuthTicketResponse_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(ValidateAuthTicketResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(ValidateAuthTicketResponse_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(ValidateAuthTicketResponse_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(ValidateAuthTicketResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(ValidateAuthTicketResponse_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(ValidateAuthTicketResponse_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(ValidateAuthTicketResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(ValidateAuthTicketResponse_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(ValidateAuthTicketResponse_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(ValidateAuthTicketResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(ValidateAuthTicketResponse_t.OnGetSize)
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
				CallbackId = 143
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 143);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00009292 File Offset: 0x00007492
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			ValidateAuthTicketResponse_t.OnResult(param);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000929A File Offset: 0x0000749A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			ValidateAuthTicketResponse_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000092A4 File Offset: 0x000074A4
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return ValidateAuthTicketResponse_t.OnGetSize();
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000092AB File Offset: 0x000074AB
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return ValidateAuthTicketResponse_t.StructSize();
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000092B2 File Offset: 0x000074B2
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			ValidateAuthTicketResponse_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000092C4 File Offset: 0x000074C4
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			ValidateAuthTicketResponse_t data = ValidateAuthTicketResponse_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<ValidateAuthTicketResponse_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<ValidateAuthTicketResponse_t>(data);
			}
		}

		// Token: 0x0400049F RID: 1183
		internal const int CallbackId = 143;

		// Token: 0x040004A0 RID: 1184
		internal ulong SteamID;

		// Token: 0x040004A1 RID: 1185
		internal AuthSessionResponse AuthSessionResponse;

		// Token: 0x040004A2 RID: 1186
		internal ulong OwnerSteamID;

		// Token: 0x02000198 RID: 408
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CD4 RID: 7380 RVA: 0x000610F4 File Offset: 0x0005F2F4
			public static implicit operator ValidateAuthTicketResponse_t(ValidateAuthTicketResponse_t.PackSmall d)
			{
				return new ValidateAuthTicketResponse_t
				{
					SteamID = d.SteamID,
					AuthSessionResponse = d.AuthSessionResponse,
					OwnerSteamID = d.OwnerSteamID
				};
			}

			// Token: 0x0400094A RID: 2378
			internal ulong SteamID;

			// Token: 0x0400094B RID: 2379
			internal AuthSessionResponse AuthSessionResponse;

			// Token: 0x0400094C RID: 2380
			internal ulong OwnerSteamID;
		}
	}
}
