using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000E5 RID: 229
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateItemResult_t
	{
		// Token: 0x0600070A RID: 1802 RVA: 0x00021CC4 File Offset: 0x0001FEC4
		internal static CreateItemResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (CreateItemResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(CreateItemResult_t.PackSmall));
			}
			return (CreateItemResult_t)Marshal.PtrToStructure(p, typeof(CreateItemResult_t));
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00021CFD File Offset: 0x0001FEFD
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(CreateItemResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(CreateItemResult_t));
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00021D25 File Offset: 0x0001FF25
		internal static CallResult<CreateItemResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<CreateItemResult_t, bool> CallbackFunction)
		{
			return new CallResult<CreateItemResult_t>(steamworks, call, CallbackFunction, new CallResult<CreateItemResult_t>.ConvertFromPointer(CreateItemResult_t.FromPointer), CreateItemResult_t.StructSize(), 3403);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00021D48 File Offset: 0x0001FF48
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
						ResultA = new Callback.VTableWinThis.ResultD(CreateItemResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(CreateItemResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(CreateItemResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(CreateItemResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(CreateItemResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(CreateItemResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(CreateItemResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(CreateItemResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(CreateItemResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(CreateItemResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(CreateItemResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(CreateItemResult_t.OnGetSize)
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
				CallbackId = 3403
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3403);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0002204E File Offset: 0x0002024E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			CreateItemResult_t.OnResult(param);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00022056 File Offset: 0x00020256
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			CreateItemResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00022060 File Offset: 0x00020260
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return CreateItemResult_t.OnGetSize();
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00022067 File Offset: 0x00020267
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return CreateItemResult_t.StructSize();
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0002206E File Offset: 0x0002026E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			CreateItemResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00022080 File Offset: 0x00020280
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			CreateItemResult_t data = CreateItemResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<CreateItemResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<CreateItemResult_t>(data);
			}
		}

		// Token: 0x04000681 RID: 1665
		internal const int CallbackId = 3403;

		// Token: 0x04000682 RID: 1666
		internal Result Result;

		// Token: 0x04000683 RID: 1667
		internal ulong PublishedFileId;

		// Token: 0x04000684 RID: 1668
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement;

		// Token: 0x02000209 RID: 521
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D45 RID: 7493 RVA: 0x00062E8C File Offset: 0x0006108C
			public static implicit operator CreateItemResult_t(CreateItemResult_t.PackSmall d)
			{
				return new CreateItemResult_t
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement
				};
			}

			// Token: 0x04000AC7 RID: 2759
			internal Result Result;

			// Token: 0x04000AC8 RID: 2760
			internal ulong PublishedFileId;

			// Token: 0x04000AC9 RID: 2761
			[MarshalAs(UnmanagedType.I1)]
			internal bool UserNeedsToAcceptWorkshopLegalAgreement;
		}
	}
}
