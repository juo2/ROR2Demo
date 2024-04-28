using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000E8 RID: 232
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserFavoriteItemsListChanged_t
	{
		// Token: 0x06000727 RID: 1831 RVA: 0x0002288C File Offset: 0x00020A8C
		internal static UserFavoriteItemsListChanged_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (UserFavoriteItemsListChanged_t.PackSmall)Marshal.PtrToStructure(p, typeof(UserFavoriteItemsListChanged_t.PackSmall));
			}
			return (UserFavoriteItemsListChanged_t)Marshal.PtrToStructure(p, typeof(UserFavoriteItemsListChanged_t));
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x000228C5 File Offset: 0x00020AC5
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(UserFavoriteItemsListChanged_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(UserFavoriteItemsListChanged_t));
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000228ED File Offset: 0x00020AED
		internal static CallResult<UserFavoriteItemsListChanged_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<UserFavoriteItemsListChanged_t, bool> CallbackFunction)
		{
			return new CallResult<UserFavoriteItemsListChanged_t>(steamworks, call, CallbackFunction, new CallResult<UserFavoriteItemsListChanged_t>.ConvertFromPointer(UserFavoriteItemsListChanged_t.FromPointer), UserFavoriteItemsListChanged_t.StructSize(), 3407);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00022910 File Offset: 0x00020B10
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
						ResultA = new Callback.VTableWinThis.ResultD(UserFavoriteItemsListChanged_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(UserFavoriteItemsListChanged_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(UserFavoriteItemsListChanged_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(UserFavoriteItemsListChanged_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(UserFavoriteItemsListChanged_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(UserFavoriteItemsListChanged_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(UserFavoriteItemsListChanged_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(UserFavoriteItemsListChanged_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(UserFavoriteItemsListChanged_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(UserFavoriteItemsListChanged_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(UserFavoriteItemsListChanged_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(UserFavoriteItemsListChanged_t.OnGetSize)
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
				CallbackId = 3407
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3407);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00022C16 File Offset: 0x00020E16
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			UserFavoriteItemsListChanged_t.OnResult(param);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00022C1E File Offset: 0x00020E1E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			UserFavoriteItemsListChanged_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00022C28 File Offset: 0x00020E28
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return UserFavoriteItemsListChanged_t.OnGetSize();
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00022C2F File Offset: 0x00020E2F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return UserFavoriteItemsListChanged_t.StructSize();
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00022C36 File Offset: 0x00020E36
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			UserFavoriteItemsListChanged_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00022C48 File Offset: 0x00020E48
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			UserFavoriteItemsListChanged_t data = UserFavoriteItemsListChanged_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<UserFavoriteItemsListChanged_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<UserFavoriteItemsListChanged_t>(data);
			}
		}

		// Token: 0x0400068D RID: 1677
		internal const int CallbackId = 3407;

		// Token: 0x0400068E RID: 1678
		internal ulong PublishedFileId;

		// Token: 0x0400068F RID: 1679
		internal Result Result;

		// Token: 0x04000690 RID: 1680
		[MarshalAs(UnmanagedType.I1)]
		internal bool WasAddRequest;

		// Token: 0x0200020C RID: 524
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D48 RID: 7496 RVA: 0x00062F4C File Offset: 0x0006114C
			public static implicit operator UserFavoriteItemsListChanged_t(UserFavoriteItemsListChanged_t.PackSmall d)
			{
				return new UserFavoriteItemsListChanged_t
				{
					PublishedFileId = d.PublishedFileId,
					Result = d.Result,
					WasAddRequest = d.WasAddRequest
				};
			}

			// Token: 0x04000AD0 RID: 2768
			internal ulong PublishedFileId;

			// Token: 0x04000AD1 RID: 2769
			internal Result Result;

			// Token: 0x04000AD2 RID: 2770
			[MarshalAs(UnmanagedType.I1)]
			internal bool WasAddRequest;
		}
	}
}
