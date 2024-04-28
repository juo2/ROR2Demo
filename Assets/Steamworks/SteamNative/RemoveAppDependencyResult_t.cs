using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000F0 RID: 240
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoveAppDependencyResult_t
	{
		// Token: 0x06000777 RID: 1911 RVA: 0x0002484C File Offset: 0x00022A4C
		internal static RemoveAppDependencyResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoveAppDependencyResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoveAppDependencyResult_t.PackSmall));
			}
			return (RemoveAppDependencyResult_t)Marshal.PtrToStructure(p, typeof(RemoveAppDependencyResult_t));
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00024885 File Offset: 0x00022A85
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoveAppDependencyResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoveAppDependencyResult_t));
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000248AD File Offset: 0x00022AAD
		internal static CallResult<RemoveAppDependencyResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoveAppDependencyResult_t, bool> CallbackFunction)
		{
			return new CallResult<RemoveAppDependencyResult_t>(steamworks, call, CallbackFunction, new CallResult<RemoveAppDependencyResult_t>.ConvertFromPointer(RemoveAppDependencyResult_t.FromPointer), RemoveAppDependencyResult_t.StructSize(), 3415);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000248D0 File Offset: 0x00022AD0
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoveAppDependencyResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoveAppDependencyResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoveAppDependencyResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoveAppDependencyResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoveAppDependencyResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoveAppDependencyResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoveAppDependencyResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoveAppDependencyResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoveAppDependencyResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoveAppDependencyResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoveAppDependencyResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoveAppDependencyResult_t.OnGetSize)
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
				CallbackId = 3415
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3415);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00024BD6 File Offset: 0x00022DD6
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoveAppDependencyResult_t.OnResult(param);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00024BDE File Offset: 0x00022DDE
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoveAppDependencyResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00024BE8 File Offset: 0x00022DE8
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoveAppDependencyResult_t.OnGetSize();
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00024BEF File Offset: 0x00022DEF
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoveAppDependencyResult_t.StructSize();
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00024BF6 File Offset: 0x00022DF6
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoveAppDependencyResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00024C08 File Offset: 0x00022E08
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoveAppDependencyResult_t data = RemoveAppDependencyResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoveAppDependencyResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoveAppDependencyResult_t>(data);
			}
		}

		// Token: 0x040006AB RID: 1707
		internal const int CallbackId = 3415;

		// Token: 0x040006AC RID: 1708
		internal Result Result;

		// Token: 0x040006AD RID: 1709
		internal ulong PublishedFileId;

		// Token: 0x040006AE RID: 1710
		internal uint AppID;

		// Token: 0x02000214 RID: 532
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D50 RID: 7504 RVA: 0x0006312C File Offset: 0x0006132C
			public static implicit operator RemoveAppDependencyResult_t(RemoveAppDependencyResult_t.PackSmall d)
			{
				return new RemoveAppDependencyResult_t
				{
					Result = d.Result,
					PublishedFileId = d.PublishedFileId,
					AppID = d.AppID
				};
			}

			// Token: 0x04000AE6 RID: 2790
			internal Result Result;

			// Token: 0x04000AE7 RID: 2791
			internal ulong PublishedFileId;

			// Token: 0x04000AE8 RID: 2792
			internal uint AppID;
		}
	}
}
