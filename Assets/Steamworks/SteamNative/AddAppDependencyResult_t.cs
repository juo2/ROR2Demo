using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000EF RID: 239
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddAppDependencyResult_t
	{
		// Token: 0x0600076D RID: 1901 RVA: 0x00024454 File Offset: 0x00022654
		internal static AddAppDependencyResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (AddAppDependencyResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(AddAppDependencyResult_t.PackSmall));
			}
			return (AddAppDependencyResult_t)Marshal.PtrToStructure(p, typeof(AddAppDependencyResult_t));
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0002448D File Offset: 0x0002268D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(AddAppDependencyResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(AddAppDependencyResult_t));
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000244B5 File Offset: 0x000226B5
		internal static CallResult<AddAppDependencyResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<AddAppDependencyResult_t, bool> CallbackFunction)
		{
			return new CallResult<AddAppDependencyResult_t>(steamworks, call, CallbackFunction, new CallResult<AddAppDependencyResult_t>.ConvertFromPointer(AddAppDependencyResult_t.FromPointer), AddAppDependencyResult_t.StructSize(), 3414);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000244D8 File Offset: 0x000226D8
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
						ResultA = new Callback.VTableWinThis.ResultD(AddAppDependencyResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(AddAppDependencyResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(AddAppDependencyResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(AddAppDependencyResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(AddAppDependencyResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(AddAppDependencyResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(AddAppDependencyResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(AddAppDependencyResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(AddAppDependencyResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(AddAppDependencyResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(AddAppDependencyResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(AddAppDependencyResult_t.OnGetSize)
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
				CallbackId = 3414
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3414);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x000247DE File Offset: 0x000229DE
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			AddAppDependencyResult_t.OnResult(param);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x000247E6 File Offset: 0x000229E6
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			AddAppDependencyResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000247F0 File Offset: 0x000229F0
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return AddAppDependencyResult_t.OnGetSize();
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000247F7 File Offset: 0x000229F7
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return AddAppDependencyResult_t.StructSize();
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000247FE File Offset: 0x000229FE
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			AddAppDependencyResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00024810 File Offset: 0x00022A10
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			AddAppDependencyResult_t data = AddAppDependencyResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<AddAppDependencyResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<AddAppDependencyResult_t>(data);
			}
		}

		// Token: 0x040006A7 RID: 1703
		internal const int CallbackId = 3414;

		// Token: 0x040006A8 RID: 1704
		internal Result Result;

		// Token: 0x040006A9 RID: 1705
		internal ulong PublishedFileId;

		// Token: 0x040006AA RID: 1706
		internal uint AppID;

		// Token: 0x02000213 RID: 531
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D4F RID: 7503 RVA: 0x000630EC File Offset: 0x000612EC
			public static implicit operator AddAppDependencyResult_t(AddAppDependencyResult_t.PackSmall d)
			{
				return new AddAppDependencyResult_t
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					AppID = d.AppID
				};
			}

			// Token: 0x04000AE3 RID: 2787
			internal Result Result;

			// Token: 0x04000AE4 RID: 2788
			internal ulong PublishedFileId;

			// Token: 0x04000AE5 RID: 2789
			internal uint AppID;
		}
	}
}
