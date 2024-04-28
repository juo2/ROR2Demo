using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000E9 RID: 233
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetUserItemVoteResult_t
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x00022C84 File Offset: 0x00020E84
		internal static SetUserItemVoteResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SetUserItemVoteResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(SetUserItemVoteResult_t.PackSmall));
			}
			return (SetUserItemVoteResult_t)Marshal.PtrToStructure(p, typeof(SetUserItemVoteResult_t));
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00022CBD File Offset: 0x00020EBD
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SetUserItemVoteResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SetUserItemVoteResult_t));
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00022CE5 File Offset: 0x00020EE5
		internal static CallResult<SetUserItemVoteResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<SetUserItemVoteResult_t, bool> CallbackFunction)
		{
			return new CallResult<SetUserItemVoteResult_t>(steamworks, call, CallbackFunction, new CallResult<SetUserItemVoteResult_t>.ConvertFromPointer(SetUserItemVoteResult_t.FromPointer), SetUserItemVoteResult_t.StructSize(), 3408);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00022D08 File Offset: 0x00020F08
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
						ResultA = new Callback.VTableWinThis.ResultD(SetUserItemVoteResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SetUserItemVoteResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SetUserItemVoteResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SetUserItemVoteResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SetUserItemVoteResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SetUserItemVoteResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SetUserItemVoteResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SetUserItemVoteResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SetUserItemVoteResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SetUserItemVoteResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SetUserItemVoteResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SetUserItemVoteResult_t.OnGetSize)
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
				CallbackId = 3408
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3408);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0002300E File Offset: 0x0002120E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SetUserItemVoteResult_t.OnResult(param);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00023016 File Offset: 0x00021216
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SetUserItemVoteResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00023020 File Offset: 0x00021220
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SetUserItemVoteResult_t.OnGetSize();
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00023027 File Offset: 0x00021227
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SetUserItemVoteResult_t.StructSize();
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0002302E File Offset: 0x0002122E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SetUserItemVoteResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00023040 File Offset: 0x00021240
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SetUserItemVoteResult_t data = SetUserItemVoteResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SetUserItemVoteResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SetUserItemVoteResult_t>(data);
			}
		}

		// Token: 0x04000691 RID: 1681
		internal const int CallbackId = 3408;

		// Token: 0x04000692 RID: 1682
		internal ulong PublishedFileId;

		// Token: 0x04000693 RID: 1683
		internal Result Result;

		// Token: 0x04000694 RID: 1684
		[MarshalAs(UnmanagedType.I1)]
		internal bool VoteUp;

		// Token: 0x0200020D RID: 525
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D49 RID: 7497 RVA: 0x00062F8C File Offset: 0x0006118C
			public static implicit operator SetUserItemVoteResult_t(SetUserItemVoteResult_t.PackSmall d)
			{
				return new SetUserItemVoteResult_t
				{
					PublishedFileId = d.PublishedFileId,
					Result = d.Result,
					VoteUp = d.VoteUp
				};
			}

			// Token: 0x04000AD3 RID: 2771
			internal ulong PublishedFileId;

			// Token: 0x04000AD4 RID: 2772
			internal Result Result;

			// Token: 0x04000AD5 RID: 2773
			[MarshalAs(UnmanagedType.I1)]
			internal bool VoteUp;
		}
	}
}
