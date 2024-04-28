using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200009E RID: 158
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyCreated_t
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x000120A0 File Offset: 0x000102A0
		internal static LobbyCreated_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (LobbyCreated_t.PackSmall)Marshal.PtrToStructure(p, typeof(LobbyCreated_t.PackSmall));
			}
			return (LobbyCreated_t)Marshal.PtrToStructure(p, typeof(LobbyCreated_t));
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000120D9 File Offset: 0x000102D9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(LobbyCreated_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(LobbyCreated_t));
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00012101 File Offset: 0x00010301
		internal static CallResult<LobbyCreated_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<LobbyCreated_t, bool> CallbackFunction)
		{
			return new CallResult<LobbyCreated_t>(steamworks, call, CallbackFunction, new CallResult<LobbyCreated_t>.ConvertFromPointer(LobbyCreated_t.FromPointer), LobbyCreated_t.StructSize(), 513);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00012124 File Offset: 0x00010324
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
						ResultA = new Callback.VTableWinThis.ResultD(LobbyCreated_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(LobbyCreated_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(LobbyCreated_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(LobbyCreated_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(LobbyCreated_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(LobbyCreated_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(LobbyCreated_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(LobbyCreated_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(LobbyCreated_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(LobbyCreated_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(LobbyCreated_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(LobbyCreated_t.OnGetSize)
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
				CallbackId = 513
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 513);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001242A File Offset: 0x0001062A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			LobbyCreated_t.OnResult(param);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00012432 File Offset: 0x00010632
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			LobbyCreated_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001243C File Offset: 0x0001063C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return LobbyCreated_t.OnGetSize();
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00012443 File Offset: 0x00010643
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return LobbyCreated_t.StructSize();
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001244A File Offset: 0x0001064A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			LobbyCreated_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001245C File Offset: 0x0001065C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			LobbyCreated_t data = LobbyCreated_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<LobbyCreated_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<LobbyCreated_t>(data);
			}
		}

		// Token: 0x04000542 RID: 1346
		internal const int CallbackId = 513;

		// Token: 0x04000543 RID: 1347
		internal Result Result;

		// Token: 0x04000544 RID: 1348
		internal ulong SteamIDLobby;

		// Token: 0x020001C2 RID: 450
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CFE RID: 7422 RVA: 0x00061B34 File Offset: 0x0005FD34
			public static implicit operator LobbyCreated_t(LobbyCreated_t.PackSmall d)
			{
				return new LobbyCreated_t
				{
					Result = d.Result,
					SteamIDLobby = d.SteamIDLobby
				};
			}

			// Token: 0x040009C8 RID: 2504
			internal Result Result;

			// Token: 0x040009C9 RID: 2505
			internal ulong SteamIDLobby;
		}
	}
}
