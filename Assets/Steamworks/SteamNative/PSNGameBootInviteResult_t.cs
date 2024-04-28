using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200009F RID: 159
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct PSNGameBootInviteResult_t
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x00012498 File Offset: 0x00010698
		internal static PSNGameBootInviteResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (PSNGameBootInviteResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(PSNGameBootInviteResult_t.PackSmall));
			}
			return (PSNGameBootInviteResult_t)Marshal.PtrToStructure(p, typeof(PSNGameBootInviteResult_t));
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000124D1 File Offset: 0x000106D1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(PSNGameBootInviteResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(PSNGameBootInviteResult_t));
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000124FC File Offset: 0x000106FC
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
						ResultA = new Callback.VTableWinThis.ResultD(PSNGameBootInviteResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(PSNGameBootInviteResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(PSNGameBootInviteResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(PSNGameBootInviteResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(PSNGameBootInviteResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(PSNGameBootInviteResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(PSNGameBootInviteResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(PSNGameBootInviteResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(PSNGameBootInviteResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(PSNGameBootInviteResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(PSNGameBootInviteResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(PSNGameBootInviteResult_t.OnGetSize)
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
				CallbackId = 515
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 515);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00012802 File Offset: 0x00010A02
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			PSNGameBootInviteResult_t.OnResult(param);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001280A File Offset: 0x00010A0A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			PSNGameBootInviteResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00012814 File Offset: 0x00010A14
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return PSNGameBootInviteResult_t.OnGetSize();
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001281B File Offset: 0x00010A1B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return PSNGameBootInviteResult_t.StructSize();
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00012822 File Offset: 0x00010A22
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			PSNGameBootInviteResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00012834 File Offset: 0x00010A34
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			PSNGameBootInviteResult_t data = PSNGameBootInviteResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<PSNGameBootInviteResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<PSNGameBootInviteResult_t>(data);
			}
		}

		// Token: 0x04000545 RID: 1349
		internal const int CallbackId = 515;

		// Token: 0x04000546 RID: 1350
		[MarshalAs(UnmanagedType.I1)]
		internal bool GameBootInviteExists;

		// Token: 0x04000547 RID: 1351
		internal ulong SteamIDLobby;

		// Token: 0x020001C3 RID: 451
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CFF RID: 7423 RVA: 0x00061B64 File Offset: 0x0005FD64
			public static implicit operator PSNGameBootInviteResult_t(PSNGameBootInviteResult_t.PackSmall d)
			{
				return new PSNGameBootInviteResult_t
				{
					GameBootInviteExists = d.GameBootInviteExists,
					SteamIDLobby = d.SteamIDLobby
				};
			}

			// Token: 0x040009CA RID: 2506
			[MarshalAs(UnmanagedType.I1)]
			internal bool GameBootInviteExists;

			// Token: 0x040009CB RID: 2507
			internal ulong SteamIDLobby;
		}
	}
}
