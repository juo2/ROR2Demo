using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000EE RID: 238
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoveUGCDependencyResult_t
	{
		// Token: 0x06000763 RID: 1891 RVA: 0x0002405C File Offset: 0x0002225C
		internal static RemoveUGCDependencyResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoveUGCDependencyResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoveUGCDependencyResult_t.PackSmall));
			}
			return (RemoveUGCDependencyResult_t)Marshal.PtrToStructure(p, typeof(RemoveUGCDependencyResult_t));
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00024095 File Offset: 0x00022295
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoveUGCDependencyResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoveUGCDependencyResult_t));
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000240BD File Offset: 0x000222BD
		internal static CallResult<RemoveUGCDependencyResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoveUGCDependencyResult_t, bool> CallbackFunction)
		{
			return new CallResult<RemoveUGCDependencyResult_t>(steamworks, call, CallbackFunction, new CallResult<RemoveUGCDependencyResult_t>.ConvertFromPointer(RemoveUGCDependencyResult_t.FromPointer), RemoveUGCDependencyResult_t.StructSize(), 3413);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000240E0 File Offset: 0x000222E0
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoveUGCDependencyResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoveUGCDependencyResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoveUGCDependencyResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoveUGCDependencyResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoveUGCDependencyResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoveUGCDependencyResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoveUGCDependencyResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoveUGCDependencyResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoveUGCDependencyResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoveUGCDependencyResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoveUGCDependencyResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoveUGCDependencyResult_t.OnGetSize)
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
				CallbackId = 3413
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3413);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000243E6 File Offset: 0x000225E6
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoveUGCDependencyResult_t.OnResult(param);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000243EE File Offset: 0x000225EE
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoveUGCDependencyResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000243F8 File Offset: 0x000225F8
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoveUGCDependencyResult_t.OnGetSize();
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x000243FF File Offset: 0x000225FF
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoveUGCDependencyResult_t.StructSize();
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00024406 File Offset: 0x00022606
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoveUGCDependencyResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x00024418 File Offset: 0x00022618
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoveUGCDependencyResult_t data = RemoveUGCDependencyResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoveUGCDependencyResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoveUGCDependencyResult_t>(data);
			}
		}

		// Token: 0x040006A3 RID: 1699
		internal const int CallbackId = 3413;

		// Token: 0x040006A4 RID: 1700
		internal Result Result;

		// Token: 0x040006A5 RID: 1701
		internal ulong PublishedFileId;

		// Token: 0x040006A6 RID: 1702
		internal ulong ChildPublishedFileId;

		// Token: 0x02000212 RID: 530
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D4E RID: 7502 RVA: 0x000630AC File Offset: 0x000612AC
			public static implicit operator RemoveUGCDependencyResult_t(RemoveUGCDependencyResult_t.PackSmall d)
			{
				return new RemoveUGCDependencyResult_t
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					ChildPublishedFileId = d.ChildPublishedFileId
				};
			}

			// Token: 0x04000AE0 RID: 2784
			internal Result Result;

			// Token: 0x04000AE1 RID: 2785
			internal ulong PublishedFileId;

			// Token: 0x04000AE2 RID: 2786
			internal ulong ChildPublishedFileId;
		}
	}
}
