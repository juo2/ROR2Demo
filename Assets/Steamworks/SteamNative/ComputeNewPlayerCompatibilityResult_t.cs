using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200011E RID: 286
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct ComputeNewPlayerCompatibilityResult_t
	{
		// Token: 0x06000902 RID: 2306 RVA: 0x0002EC54 File Offset: 0x0002CE54
		internal static ComputeNewPlayerCompatibilityResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (ComputeNewPlayerCompatibilityResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(ComputeNewPlayerCompatibilityResult_t.PackSmall));
			}
			return (ComputeNewPlayerCompatibilityResult_t)Marshal.PtrToStructure(p, typeof(ComputeNewPlayerCompatibilityResult_t));
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0002EC8D File Offset: 0x0002CE8D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(ComputeNewPlayerCompatibilityResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(ComputeNewPlayerCompatibilityResult_t));
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0002ECB5 File Offset: 0x0002CEB5
		internal static CallResult<ComputeNewPlayerCompatibilityResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<ComputeNewPlayerCompatibilityResult_t, bool> CallbackFunction)
		{
			return new CallResult<ComputeNewPlayerCompatibilityResult_t>(steamworks, call, CallbackFunction, new CallResult<ComputeNewPlayerCompatibilityResult_t>.ConvertFromPointer(ComputeNewPlayerCompatibilityResult_t.FromPointer), ComputeNewPlayerCompatibilityResult_t.StructSize(), 211);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x0002ECD8 File Offset: 0x0002CED8
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
						ResultA = new Callback.VTableWinThis.ResultD(ComputeNewPlayerCompatibilityResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(ComputeNewPlayerCompatibilityResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(ComputeNewPlayerCompatibilityResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(ComputeNewPlayerCompatibilityResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(ComputeNewPlayerCompatibilityResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(ComputeNewPlayerCompatibilityResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(ComputeNewPlayerCompatibilityResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(ComputeNewPlayerCompatibilityResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(ComputeNewPlayerCompatibilityResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(ComputeNewPlayerCompatibilityResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(ComputeNewPlayerCompatibilityResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(ComputeNewPlayerCompatibilityResult_t.OnGetSize)
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
				CallbackId = 211
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 211);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0002EFDE File Offset: 0x0002D1DE
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			ComputeNewPlayerCompatibilityResult_t.OnResult(param);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0002EFE6 File Offset: 0x0002D1E6
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			ComputeNewPlayerCompatibilityResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0002EFF0 File Offset: 0x0002D1F0
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return ComputeNewPlayerCompatibilityResult_t.OnGetSize();
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0002EFF7 File Offset: 0x0002D1F7
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return ComputeNewPlayerCompatibilityResult_t.StructSize();
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x0002EFFE File Offset: 0x0002D1FE
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			ComputeNewPlayerCompatibilityResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0002F010 File Offset: 0x0002D210
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			ComputeNewPlayerCompatibilityResult_t data = ComputeNewPlayerCompatibilityResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<ComputeNewPlayerCompatibilityResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<ComputeNewPlayerCompatibilityResult_t>(data);
			}
		}

		// Token: 0x04000763 RID: 1891
		internal const int CallbackId = 211;

		// Token: 0x04000764 RID: 1892
		internal Result Result;

		// Token: 0x04000765 RID: 1893
		internal int CPlayersThatDontLikeCandidate;

		// Token: 0x04000766 RID: 1894
		internal int CPlayersThatCandidateDoesntLike;

		// Token: 0x04000767 RID: 1895
		internal int CClanPlayersThatDontLikeCandidate;

		// Token: 0x04000768 RID: 1896
		internal ulong SteamIDCandidate;

		// Token: 0x02000242 RID: 578
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D7E RID: 7550 RVA: 0x00063C8C File Offset: 0x00061E8C
			public static implicit operator ComputeNewPlayerCompatibilityResult_t(ComputeNewPlayerCompatibilityResult_t.PackSmall d)
			{
				return new ComputeNewPlayerCompatibilityResult_t
				{
					Result = d.Result,
					CPlayersThatDontLikeCandidate = d.CPlayersThatDontLikeCandidate,
					CPlayersThatCandidateDoesntLike = d.CPlayersThatCandidateDoesntLike,
					CClanPlayersThatDontLikeCandidate = d.CClanPlayersThatDontLikeCandidate,
					SteamIDCandidate = d.SteamIDCandidate
				};
			}

			// Token: 0x04000B74 RID: 2932
			internal Result Result;

			// Token: 0x04000B75 RID: 2933
			internal int CPlayersThatDontLikeCandidate;

			// Token: 0x04000B76 RID: 2934
			internal int CPlayersThatCandidateDoesntLike;

			// Token: 0x04000B77 RID: 2935
			internal int CClanPlayersThatDontLikeCandidate;

			// Token: 0x04000B78 RID: 2936
			internal ulong SteamIDCandidate;
		}
	}
}
