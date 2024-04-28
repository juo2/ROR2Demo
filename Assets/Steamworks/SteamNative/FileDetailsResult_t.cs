using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000CF RID: 207
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FileDetailsResult_t
	{
		// Token: 0x06000665 RID: 1637 RVA: 0x0001D948 File Offset: 0x0001BB48
		internal static FileDetailsResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (FileDetailsResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(FileDetailsResult_t.PackSmall));
			}
			return (FileDetailsResult_t)Marshal.PtrToStructure(p, typeof(FileDetailsResult_t));
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001D981 File Offset: 0x0001BB81
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(FileDetailsResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(FileDetailsResult_t));
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001D9A9 File Offset: 0x0001BBA9
		internal static CallResult<FileDetailsResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<FileDetailsResult_t, bool> CallbackFunction)
		{
			return new CallResult<FileDetailsResult_t>(steamworks, call, CallbackFunction, new CallResult<FileDetailsResult_t>.ConvertFromPointer(FileDetailsResult_t.FromPointer), FileDetailsResult_t.StructSize(), 1023);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0001D9CC File Offset: 0x0001BBCC
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
						ResultA = new Callback.VTableWinThis.ResultD(FileDetailsResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(FileDetailsResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(FileDetailsResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(FileDetailsResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(FileDetailsResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(FileDetailsResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(FileDetailsResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(FileDetailsResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(FileDetailsResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(FileDetailsResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(FileDetailsResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(FileDetailsResult_t.OnGetSize)
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
				CallbackId = 1023
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1023);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001DCD2 File Offset: 0x0001BED2
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			FileDetailsResult_t.OnResult(param);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001DCDA File Offset: 0x0001BEDA
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			FileDetailsResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0001DCE4 File Offset: 0x0001BEE4
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return FileDetailsResult_t.OnGetSize();
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001DCEB File Offset: 0x0001BEEB
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return FileDetailsResult_t.StructSize();
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001DCF2 File Offset: 0x0001BEF2
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			FileDetailsResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001DD04 File Offset: 0x0001BF04
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			FileDetailsResult_t data = FileDetailsResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<FileDetailsResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<FileDetailsResult_t>(data);
			}
		}

		// Token: 0x04000618 RID: 1560
		internal const int CallbackId = 1023;

		// Token: 0x04000619 RID: 1561
		internal Result Result;

		// Token: 0x0400061A RID: 1562
		internal ulong FileSize;

		// Token: 0x0400061B RID: 1563
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
		internal char FileSHA;

		// Token: 0x0400061C RID: 1564
		internal uint Flags;

		// Token: 0x020001F3 RID: 499
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D2F RID: 7471 RVA: 0x0006281C File Offset: 0x00060A1C
			public static implicit operator FileDetailsResult_t(FileDetailsResult_t.PackSmall d)
			{
				return new FileDetailsResult_t
				{
					Result = d.Result,
					FileSize = d.FileSize,
					FileSHA = d.FileSHA,
					Flags = d.Flags
				};
			}

			// Token: 0x04000A6F RID: 2671
			internal Result Result;

			// Token: 0x04000A70 RID: 2672
			internal ulong FileSize;

			// Token: 0x04000A71 RID: 2673
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
			internal char FileSHA;

			// Token: 0x04000A72 RID: 2674
			internal uint Flags;
		}
	}
}
