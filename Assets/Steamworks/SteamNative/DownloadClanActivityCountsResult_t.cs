using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000087 RID: 135
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DownloadClanActivityCountsResult_t
	{
		// Token: 0x060003DE RID: 990 RVA: 0x0000D1A0 File Offset: 0x0000B3A0
		internal static DownloadClanActivityCountsResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (DownloadClanActivityCountsResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(DownloadClanActivityCountsResult_t.PackSmall));
			}
			return (DownloadClanActivityCountsResult_t)Marshal.PtrToStructure(p, typeof(DownloadClanActivityCountsResult_t));
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000D1D9 File Offset: 0x0000B3D9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(DownloadClanActivityCountsResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(DownloadClanActivityCountsResult_t));
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000D204 File Offset: 0x0000B404
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
						ResultA = new Callback.VTableWinThis.ResultD(DownloadClanActivityCountsResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(DownloadClanActivityCountsResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(DownloadClanActivityCountsResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(DownloadClanActivityCountsResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(DownloadClanActivityCountsResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(DownloadClanActivityCountsResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(DownloadClanActivityCountsResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(DownloadClanActivityCountsResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(DownloadClanActivityCountsResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(DownloadClanActivityCountsResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(DownloadClanActivityCountsResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(DownloadClanActivityCountsResult_t.OnGetSize)
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
				CallbackId = 341
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 341);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000D50A File Offset: 0x0000B70A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			DownloadClanActivityCountsResult_t.OnResult(param);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000D512 File Offset: 0x0000B712
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			DownloadClanActivityCountsResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000D51C File Offset: 0x0000B71C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return DownloadClanActivityCountsResult_t.OnGetSize();
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000D523 File Offset: 0x0000B723
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return DownloadClanActivityCountsResult_t.StructSize();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000D52A File Offset: 0x0000B72A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			DownloadClanActivityCountsResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000D53C File Offset: 0x0000B73C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			DownloadClanActivityCountsResult_t data = DownloadClanActivityCountsResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<DownloadClanActivityCountsResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<DownloadClanActivityCountsResult_t>(data);
			}
		}

		// Token: 0x040004DD RID: 1245
		internal const int CallbackId = 341;

		// Token: 0x040004DE RID: 1246
		[MarshalAs(UnmanagedType.I1)]
		internal bool Success;

		// Token: 0x020001AB RID: 427
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CE7 RID: 7399 RVA: 0x000614F4 File Offset: 0x0005F6F4
			public static implicit operator DownloadClanActivityCountsResult_t(DownloadClanActivityCountsResult_t.PackSmall d)
			{
				return new DownloadClanActivityCountsResult_t
				{
					Success = d.Success
				};
			}

			// Token: 0x04000977 RID: 2423
			[MarshalAs(UnmanagedType.I1)]
			internal bool Success;
		}
	}
}
