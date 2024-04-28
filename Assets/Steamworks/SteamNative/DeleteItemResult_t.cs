using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000F2 RID: 242
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteItemResult_t
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x0002503C File Offset: 0x0002323C
		internal static DeleteItemResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (DeleteItemResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(DeleteItemResult_t.PackSmall));
			}
			return (DeleteItemResult_t)Marshal.PtrToStructure(p, typeof(DeleteItemResult_t));
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00025075 File Offset: 0x00023275
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(DeleteItemResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(DeleteItemResult_t));
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0002509D File Offset: 0x0002329D
		internal static CallResult<DeleteItemResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<DeleteItemResult_t, bool> CallbackFunction)
		{
			return new CallResult<DeleteItemResult_t>(steamworks, call, CallbackFunction, new CallResult<DeleteItemResult_t>.ConvertFromPointer(DeleteItemResult_t.FromPointer), DeleteItemResult_t.StructSize(), 3417);
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000250C0 File Offset: 0x000232C0
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
						ResultA = new Callback.VTableWinThis.ResultD(DeleteItemResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(DeleteItemResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(DeleteItemResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(DeleteItemResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(DeleteItemResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(DeleteItemResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(DeleteItemResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(DeleteItemResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(DeleteItemResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(DeleteItemResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(DeleteItemResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(DeleteItemResult_t.OnGetSize)
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
				CallbackId = 3417
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3417);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000253C6 File Offset: 0x000235C6
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			DeleteItemResult_t.OnResult(param);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000253CE File Offset: 0x000235CE
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			DeleteItemResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000253D8 File Offset: 0x000235D8
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return DeleteItemResult_t.OnGetSize();
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x000253DF File Offset: 0x000235DF
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return DeleteItemResult_t.StructSize();
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000253E6 File Offset: 0x000235E6
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			DeleteItemResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x000253F8 File Offset: 0x000235F8
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			DeleteItemResult_t data = DeleteItemResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<DeleteItemResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<DeleteItemResult_t>(data);
			}
		}

		// Token: 0x040006B5 RID: 1717
		internal const int CallbackId = 3417;

		// Token: 0x040006B6 RID: 1718
		internal Result Result;

		// Token: 0x040006B7 RID: 1719
		internal ulong PublishedFileId;

		// Token: 0x02000216 RID: 534
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D52 RID: 7506 RVA: 0x000631C4 File Offset: 0x000613C4
			public static implicit operator DeleteItemResult_t(DeleteItemResult_t.PackSmall d)
			{
				return new DeleteItemResult_t
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId
				};
			}

			// Token: 0x04000AEE RID: 2798
			internal Result Result;

			// Token: 0x04000AEF RID: 2799
			internal ulong PublishedFileId;
		}
	}
}
