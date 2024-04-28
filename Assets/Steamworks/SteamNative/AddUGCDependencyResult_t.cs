using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000ED RID: 237
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddUGCDependencyResult_t
	{
		// Token: 0x06000759 RID: 1881 RVA: 0x00023C64 File Offset: 0x00021E64
		internal static AddUGCDependencyResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (AddUGCDependencyResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(AddUGCDependencyResult_t.PackSmall));
			}
			return (AddUGCDependencyResult_t)Marshal.PtrToStructure(p, typeof(AddUGCDependencyResult_t));
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00023C9D File Offset: 0x00021E9D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(AddUGCDependencyResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(AddUGCDependencyResult_t));
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00023CC5 File Offset: 0x00021EC5
		internal static CallResult<AddUGCDependencyResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<AddUGCDependencyResult_t, bool> CallbackFunction)
		{
			return new CallResult<AddUGCDependencyResult_t>(steamworks, call, CallbackFunction, new CallResult<AddUGCDependencyResult_t>.ConvertFromPointer(AddUGCDependencyResult_t.FromPointer), AddUGCDependencyResult_t.StructSize(), 3412);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00023CE8 File Offset: 0x00021EE8
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
						ResultA = new Callback.VTableWinThis.ResultD(AddUGCDependencyResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(AddUGCDependencyResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(AddUGCDependencyResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(AddUGCDependencyResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(AddUGCDependencyResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(AddUGCDependencyResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(AddUGCDependencyResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(AddUGCDependencyResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(AddUGCDependencyResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(AddUGCDependencyResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(AddUGCDependencyResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(AddUGCDependencyResult_t.OnGetSize)
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
				CallbackId = 3412
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3412);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00023FEE File Offset: 0x000221EE
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			AddUGCDependencyResult_t.OnResult(param);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00023FF6 File Offset: 0x000221F6
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			AddUGCDependencyResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00024000 File Offset: 0x00022200
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return AddUGCDependencyResult_t.OnGetSize();
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00024007 File Offset: 0x00022207
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return AddUGCDependencyResult_t.StructSize();
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0002400E File Offset: 0x0002220E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			AddUGCDependencyResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00024020 File Offset: 0x00022220
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			AddUGCDependencyResult_t data = AddUGCDependencyResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<AddUGCDependencyResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<AddUGCDependencyResult_t>(data);
			}
		}

		// Token: 0x0400069F RID: 1695
		internal const int CallbackId = 3412;

		// Token: 0x040006A0 RID: 1696
		internal Result Result;

		// Token: 0x040006A1 RID: 1697
		internal ulong PublishedFileId;

		// Token: 0x040006A2 RID: 1698
		internal ulong ChildPublishedFileId;

		// Token: 0x02000211 RID: 529
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D4D RID: 7501 RVA: 0x0006306C File Offset: 0x0006126C
			public static implicit operator AddUGCDependencyResult_t(AddUGCDependencyResult_t.PackSmall d)
			{
				return new AddUGCDependencyResult_t
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					ChildPublishedFileId = d.ChildPublishedFileId
				};
			}

			// Token: 0x04000ADD RID: 2781
			internal Result Result;

			// Token: 0x04000ADE RID: 2782
			internal ulong PublishedFileId;

			// Token: 0x04000ADF RID: 2783
			internal ulong ChildPublishedFileId;
		}
	}
}
