using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000EA RID: 234
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetUserItemVoteResult_t
	{
		// Token: 0x0600073B RID: 1851 RVA: 0x0002307C File Offset: 0x0002127C
		internal static GetUserItemVoteResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GetUserItemVoteResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(GetUserItemVoteResult_t.PackSmall));
			}
			return (GetUserItemVoteResult_t)Marshal.PtrToStructure(p, typeof(GetUserItemVoteResult_t));
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x000230B5 File Offset: 0x000212B5
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GetUserItemVoteResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GetUserItemVoteResult_t));
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x000230DD File Offset: 0x000212DD
		internal static CallResult<GetUserItemVoteResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<GetUserItemVoteResult_t, bool> CallbackFunction)
		{
			return new CallResult<GetUserItemVoteResult_t>(steamworks, call, CallbackFunction, new CallResult<GetUserItemVoteResult_t>.ConvertFromPointer(GetUserItemVoteResult_t.FromPointer), GetUserItemVoteResult_t.StructSize(), 3409);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00023100 File Offset: 0x00021300
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
						ResultA = new Callback.VTableWinThis.ResultD(GetUserItemVoteResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GetUserItemVoteResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GetUserItemVoteResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GetUserItemVoteResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GetUserItemVoteResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GetUserItemVoteResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GetUserItemVoteResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GetUserItemVoteResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GetUserItemVoteResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GetUserItemVoteResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GetUserItemVoteResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GetUserItemVoteResult_t.OnGetSize)
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
				CallbackId = 3409
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3409);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00023406 File Offset: 0x00021606
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GetUserItemVoteResult_t.OnResult(param);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0002340E File Offset: 0x0002160E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GetUserItemVoteResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00023418 File Offset: 0x00021618
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GetUserItemVoteResult_t.OnGetSize();
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0002341F File Offset: 0x0002161F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GetUserItemVoteResult_t.StructSize();
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00023426 File Offset: 0x00021626
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GetUserItemVoteResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00023438 File Offset: 0x00021638
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GetUserItemVoteResult_t data = GetUserItemVoteResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GetUserItemVoteResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GetUserItemVoteResult_t>(data);
			}
		}

		// Token: 0x04000695 RID: 1685
		internal const int CallbackId = 3409;

		// Token: 0x04000696 RID: 1686
		internal ulong PublishedFileId;

		// Token: 0x04000697 RID: 1687
		internal Result Result;

		// Token: 0x04000698 RID: 1688
		[MarshalAs(UnmanagedType.I1)]
		internal bool VotedUp;

		// Token: 0x04000699 RID: 1689
		[MarshalAs(UnmanagedType.I1)]
		internal bool VotedDown;

		// Token: 0x0400069A RID: 1690
		[MarshalAs(UnmanagedType.I1)]
		internal bool VoteSkipped;

		// Token: 0x0200020E RID: 526
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D4A RID: 7498 RVA: 0x00062FCC File Offset: 0x000611CC
			public static implicit operator GetUserItemVoteResult_t(GetUserItemVoteResult_t.PackSmall d)
			{
				return new GetUserItemVoteResult_t
				{
					PublishedFileId = d.PublishedFileId,
					Result = d.Result,
					VotedUp = d.VotedUp,
					VotedDown = d.VotedDown,
					VoteSkipped = d.VoteSkipped
				};
			}

			// Token: 0x04000AD6 RID: 2774
			internal ulong PublishedFileId;

			// Token: 0x04000AD7 RID: 2775
			internal Result Result;

			// Token: 0x04000AD8 RID: 2776
			[MarshalAs(UnmanagedType.I1)]
			internal bool VotedUp;

			// Token: 0x04000AD9 RID: 2777
			[MarshalAs(UnmanagedType.I1)]
			internal bool VotedDown;

			// Token: 0x04000ADA RID: 2778
			[MarshalAs(UnmanagedType.I1)]
			internal bool VoteSkipped;
		}
	}
}
