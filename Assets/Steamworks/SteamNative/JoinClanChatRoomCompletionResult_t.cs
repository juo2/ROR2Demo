using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000088 RID: 136
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct JoinClanChatRoomCompletionResult_t
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x0000D578 File Offset: 0x0000B778
		internal static JoinClanChatRoomCompletionResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (JoinClanChatRoomCompletionResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(JoinClanChatRoomCompletionResult_t.PackSmall));
			}
			return (JoinClanChatRoomCompletionResult_t)Marshal.PtrToStructure(p, typeof(JoinClanChatRoomCompletionResult_t));
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000D5B1 File Offset: 0x0000B7B1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(JoinClanChatRoomCompletionResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(JoinClanChatRoomCompletionResult_t));
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000D5D9 File Offset: 0x0000B7D9
		internal static CallResult<JoinClanChatRoomCompletionResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<JoinClanChatRoomCompletionResult_t, bool> CallbackFunction)
		{
			return new CallResult<JoinClanChatRoomCompletionResult_t>(steamworks, call, CallbackFunction, new CallResult<JoinClanChatRoomCompletionResult_t>.ConvertFromPointer(JoinClanChatRoomCompletionResult_t.FromPointer), JoinClanChatRoomCompletionResult_t.StructSize(), 342);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000D5FC File Offset: 0x0000B7FC
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
						ResultA = new Callback.VTableWinThis.ResultD(JoinClanChatRoomCompletionResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(JoinClanChatRoomCompletionResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(JoinClanChatRoomCompletionResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(JoinClanChatRoomCompletionResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(JoinClanChatRoomCompletionResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(JoinClanChatRoomCompletionResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(JoinClanChatRoomCompletionResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(JoinClanChatRoomCompletionResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(JoinClanChatRoomCompletionResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(JoinClanChatRoomCompletionResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(JoinClanChatRoomCompletionResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(JoinClanChatRoomCompletionResult_t.OnGetSize)
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
				CallbackId = 342
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 342);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000D902 File Offset: 0x0000BB02
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			JoinClanChatRoomCompletionResult_t.OnResult(param);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000D90A File Offset: 0x0000BB0A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			JoinClanChatRoomCompletionResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000D914 File Offset: 0x0000BB14
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return JoinClanChatRoomCompletionResult_t.OnGetSize();
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000D91B File Offset: 0x0000BB1B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return JoinClanChatRoomCompletionResult_t.StructSize();
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000D922 File Offset: 0x0000BB22
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			JoinClanChatRoomCompletionResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000D934 File Offset: 0x0000BB34
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			JoinClanChatRoomCompletionResult_t data = JoinClanChatRoomCompletionResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<JoinClanChatRoomCompletionResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<JoinClanChatRoomCompletionResult_t>(data);
			}
		}

		// Token: 0x040004DF RID: 1247
		internal const int CallbackId = 342;

		// Token: 0x040004E0 RID: 1248
		internal ulong SteamIDClanChat;

		// Token: 0x040004E1 RID: 1249
		internal ChatRoomEnterResponse ChatRoomEnterResponse;

		// Token: 0x020001AC RID: 428
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CE8 RID: 7400 RVA: 0x00061518 File Offset: 0x0005F718
			public static implicit operator JoinClanChatRoomCompletionResult_t(JoinClanChatRoomCompletionResult_t.PackSmall d)
			{
				return new JoinClanChatRoomCompletionResult_t
				{
					SteamIDClanChat = d.SteamIDClanChat,
					ChatRoomEnterResponse = d.ChatRoomEnterResponse
				};
			}

			// Token: 0x04000978 RID: 2424
			internal ulong SteamIDClanChat;

			// Token: 0x04000979 RID: 2425
			internal ChatRoomEnterResponse ChatRoomEnterResponse;
		}
	}
}
