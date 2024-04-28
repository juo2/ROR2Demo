using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200008A RID: 138
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct FriendsGetFollowerCount_t
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x0000DD48 File Offset: 0x0000BF48
		internal static FriendsGetFollowerCount_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (FriendsGetFollowerCount_t.PackSmall)Marshal.PtrToStructure(p, typeof(FriendsGetFollowerCount_t.PackSmall));
			}
			return (FriendsGetFollowerCount_t)Marshal.PtrToStructure(p, typeof(FriendsGetFollowerCount_t));
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000DD81 File Offset: 0x0000BF81
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(FriendsGetFollowerCount_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(FriendsGetFollowerCount_t));
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal static CallResult<FriendsGetFollowerCount_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<FriendsGetFollowerCount_t, bool> CallbackFunction)
		{
			return new CallResult<FriendsGetFollowerCount_t>(steamworks, call, CallbackFunction, new CallResult<FriendsGetFollowerCount_t>.ConvertFromPointer(FriendsGetFollowerCount_t.FromPointer), FriendsGetFollowerCount_t.StructSize(), 344);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000DDCC File Offset: 0x0000BFCC
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
						ResultA = new Callback.VTableWinThis.ResultD(FriendsGetFollowerCount_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(FriendsGetFollowerCount_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(FriendsGetFollowerCount_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(FriendsGetFollowerCount_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(FriendsGetFollowerCount_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(FriendsGetFollowerCount_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(FriendsGetFollowerCount_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(FriendsGetFollowerCount_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(FriendsGetFollowerCount_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(FriendsGetFollowerCount_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(FriendsGetFollowerCount_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(FriendsGetFollowerCount_t.OnGetSize)
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
				CallbackId = 344
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 344);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000E0D2 File Offset: 0x0000C2D2
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			FriendsGetFollowerCount_t.OnResult(param);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000E0DA File Offset: 0x0000C2DA
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			FriendsGetFollowerCount_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return FriendsGetFollowerCount_t.OnGetSize();
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000E0EB File Offset: 0x0000C2EB
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return FriendsGetFollowerCount_t.StructSize();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000E0F2 File Offset: 0x0000C2F2
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			FriendsGetFollowerCount_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000E104 File Offset: 0x0000C304
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			FriendsGetFollowerCount_t data = FriendsGetFollowerCount_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<FriendsGetFollowerCount_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<FriendsGetFollowerCount_t>(data);
			}
		}

		// Token: 0x040004E5 RID: 1253
		internal const int CallbackId = 344;

		// Token: 0x040004E6 RID: 1254
		internal Result Result;

		// Token: 0x040004E7 RID: 1255
		internal ulong SteamID;

		// Token: 0x040004E8 RID: 1256
		internal int Count;

		// Token: 0x020001AE RID: 430
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CEA RID: 7402 RVA: 0x00061578 File Offset: 0x0005F778
			public static implicit operator FriendsGetFollowerCount_t(FriendsGetFollowerCount_t.PackSmall d)
			{
				return new FriendsGetFollowerCount_t
				{
					Result = d.Result,
					SteamID = d.SteamID,
					Count = d.Count
				};
			}

			// Token: 0x0400097C RID: 2428
			internal Result Result;

			// Token: 0x0400097D RID: 2429
			internal ulong SteamID;

			// Token: 0x0400097E RID: 2430
			internal int Count;
		}
	}
}
