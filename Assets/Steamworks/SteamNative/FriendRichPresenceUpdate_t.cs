using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000082 RID: 130
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct FriendRichPresenceUpdate_t
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x0000BE68 File Offset: 0x0000A068
		internal static FriendRichPresenceUpdate_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (FriendRichPresenceUpdate_t.PackSmall)Marshal.PtrToStructure(p, typeof(FriendRichPresenceUpdate_t.PackSmall));
			}
			return (FriendRichPresenceUpdate_t)Marshal.PtrToStructure(p, typeof(FriendRichPresenceUpdate_t));
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000BEA1 File Offset: 0x0000A0A1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(FriendRichPresenceUpdate_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(FriendRichPresenceUpdate_t));
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000BECC File Offset: 0x0000A0CC
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
						ResultA = new Callback.VTableWinThis.ResultD(FriendRichPresenceUpdate_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(FriendRichPresenceUpdate_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(FriendRichPresenceUpdate_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(FriendRichPresenceUpdate_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(FriendRichPresenceUpdate_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(FriendRichPresenceUpdate_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(FriendRichPresenceUpdate_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(FriendRichPresenceUpdate_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(FriendRichPresenceUpdate_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(FriendRichPresenceUpdate_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(FriendRichPresenceUpdate_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(FriendRichPresenceUpdate_t.OnGetSize)
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
				CallbackId = 336
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 336);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000C1D2 File Offset: 0x0000A3D2
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			FriendRichPresenceUpdate_t.OnResult(param);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000C1DA File Offset: 0x0000A3DA
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			FriendRichPresenceUpdate_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return FriendRichPresenceUpdate_t.OnGetSize();
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000C1EB File Offset: 0x0000A3EB
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return FriendRichPresenceUpdate_t.StructSize();
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000C1F2 File Offset: 0x0000A3F2
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			FriendRichPresenceUpdate_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000C204 File Offset: 0x0000A404
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			FriendRichPresenceUpdate_t data = FriendRichPresenceUpdate_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<FriendRichPresenceUpdate_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<FriendRichPresenceUpdate_t>(data);
			}
		}

		// Token: 0x040004CB RID: 1227
		internal const int CallbackId = 336;

		// Token: 0x040004CC RID: 1228
		internal ulong SteamIDFriend;

		// Token: 0x040004CD RID: 1229
		internal uint AppID;

		// Token: 0x020001A6 RID: 422
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CE2 RID: 7394 RVA: 0x000613D8 File Offset: 0x0005F5D8
			public static implicit operator FriendRichPresenceUpdate_t(FriendRichPresenceUpdate_t.PackSmall d)
			{
				return new FriendRichPresenceUpdate_t
				{
					SteamIDFriend = d.SteamIDFriend,
					AppID = d.AppID
				};
			}

			// Token: 0x0400096A RID: 2410
			internal ulong SteamIDFriend;

			// Token: 0x0400096B RID: 2411
			internal uint AppID;
		}
	}
}
