using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000E7 RID: 231
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DownloadItemResult_t
	{
		// Token: 0x0600071E RID: 1822 RVA: 0x000224B4 File Offset: 0x000206B4
		internal static DownloadItemResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (DownloadItemResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(DownloadItemResult_t.PackSmall));
			}
			return (DownloadItemResult_t)Marshal.PtrToStructure(p, typeof(DownloadItemResult_t));
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x000224ED File Offset: 0x000206ED
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(DownloadItemResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(DownloadItemResult_t));
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00022518 File Offset: 0x00020718
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
						ResultA = new Callback.VTableWinThis.ResultD(DownloadItemResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(DownloadItemResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(DownloadItemResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(DownloadItemResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(DownloadItemResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(DownloadItemResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(DownloadItemResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(DownloadItemResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(DownloadItemResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(DownloadItemResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(DownloadItemResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(DownloadItemResult_t.OnGetSize)
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
				CallbackId = 3406
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3406);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0002281E File Offset: 0x00020A1E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			DownloadItemResult_t.OnResult(param);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00022826 File Offset: 0x00020A26
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			DownloadItemResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00022830 File Offset: 0x00020A30
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return DownloadItemResult_t.OnGetSize();
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00022837 File Offset: 0x00020A37
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return DownloadItemResult_t.StructSize();
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0002283E File Offset: 0x00020A3E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			DownloadItemResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00022850 File Offset: 0x00020A50
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			DownloadItemResult_t data = DownloadItemResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<DownloadItemResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<DownloadItemResult_t>(data);
			}
		}

		// Token: 0x04000689 RID: 1673
		internal const int CallbackId = 3406;

		// Token: 0x0400068A RID: 1674
		internal uint AppID;

		// Token: 0x0400068B RID: 1675
		internal ulong PublishedFileId;

		// Token: 0x0400068C RID: 1676
		internal Result Result;

		// Token: 0x0200020B RID: 523
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D47 RID: 7495 RVA: 0x00062F0C File Offset: 0x0006110C
			public static implicit operator DownloadItemResult_t(DownloadItemResult_t.PackSmall d)
			{
				return new DownloadItemResult_t
				{
					AppID = d.AppID,
					PublishedFileId = d.PublishedFileId,
					Result = d.Result
				};
			}

			// Token: 0x04000ACD RID: 2765
			internal uint AppID;

			// Token: 0x04000ACE RID: 2766
			internal ulong PublishedFileId;

			// Token: 0x04000ACF RID: 2767
			internal Result Result;
		}
	}
}
