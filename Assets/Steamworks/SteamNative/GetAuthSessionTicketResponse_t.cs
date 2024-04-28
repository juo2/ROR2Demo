using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000077 RID: 119
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetAuthSessionTicketResponse_t
	{
		// Token: 0x0600035A RID: 858 RVA: 0x00009AD0 File Offset: 0x00007CD0
		internal static GetAuthSessionTicketResponse_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GetAuthSessionTicketResponse_t.PackSmall)Marshal.PtrToStructure(p, typeof(GetAuthSessionTicketResponse_t.PackSmall));
			}
			return (GetAuthSessionTicketResponse_t)Marshal.PtrToStructure(p, typeof(GetAuthSessionTicketResponse_t));
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00009B09 File Offset: 0x00007D09
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GetAuthSessionTicketResponse_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GetAuthSessionTicketResponse_t));
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00009B34 File Offset: 0x00007D34
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
						ResultA = new Callback.VTableWinThis.ResultD(GetAuthSessionTicketResponse_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GetAuthSessionTicketResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GetAuthSessionTicketResponse_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GetAuthSessionTicketResponse_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GetAuthSessionTicketResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GetAuthSessionTicketResponse_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GetAuthSessionTicketResponse_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GetAuthSessionTicketResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GetAuthSessionTicketResponse_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GetAuthSessionTicketResponse_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GetAuthSessionTicketResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GetAuthSessionTicketResponse_t.OnGetSize)
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
				CallbackId = 163
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 163);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00009E3A File Offset: 0x0000803A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GetAuthSessionTicketResponse_t.OnResult(param);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00009E42 File Offset: 0x00008042
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GetAuthSessionTicketResponse_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00009E4C File Offset: 0x0000804C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GetAuthSessionTicketResponse_t.OnGetSize();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00009E53 File Offset: 0x00008053
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GetAuthSessionTicketResponse_t.StructSize();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00009E5A File Offset: 0x0000805A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GetAuthSessionTicketResponse_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00009E6C File Offset: 0x0000806C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GetAuthSessionTicketResponse_t data = GetAuthSessionTicketResponse_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GetAuthSessionTicketResponse_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GetAuthSessionTicketResponse_t>(data);
			}
		}

		// Token: 0x040004A9 RID: 1193
		internal const int CallbackId = 163;

		// Token: 0x040004AA RID: 1194
		internal uint AuthTicket;

		// Token: 0x040004AB RID: 1195
		internal Result Result;

		// Token: 0x0200019B RID: 411
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CD7 RID: 7383 RVA: 0x00061198 File Offset: 0x0005F398
			public static implicit operator GetAuthSessionTicketResponse_t(GetAuthSessionTicketResponse_t.PackSmall d)
			{
				return new GetAuthSessionTicketResponse_t
				{
					AuthTicket = d.AuthTicket,
					Result = d.Result
				};
			}

			// Token: 0x04000951 RID: 2385
			internal uint AuthTicket;

			// Token: 0x04000952 RID: 2386
			internal Result Result;
		}
	}
}
