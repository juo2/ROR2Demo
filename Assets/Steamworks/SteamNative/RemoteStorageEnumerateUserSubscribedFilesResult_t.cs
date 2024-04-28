using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000AB RID: 171
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageEnumerateUserSubscribedFilesResult_t
	{
		// Token: 0x06000513 RID: 1299 RVA: 0x00014FC0 File Offset: 0x000131C0
		internal static RemoteStorageEnumerateUserSubscribedFilesResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageEnumerateUserSubscribedFilesResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t.PackSmall));
			}
			return (RemoteStorageEnumerateUserSubscribedFilesResult_t)Marshal.PtrToStructure(p, typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t));
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00014FF9 File Offset: 0x000131F9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t));
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00015021 File Offset: 0x00013221
		internal static CallResult<RemoteStorageEnumerateUserSubscribedFilesResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageEnumerateUserSubscribedFilesResult_t, bool> CallbackFunction)
		{
			return new CallResult<RemoteStorageEnumerateUserSubscribedFilesResult_t>(steamworks, call, CallbackFunction, new CallResult<RemoteStorageEnumerateUserSubscribedFilesResult_t>.ConvertFromPointer(RemoteStorageEnumerateUserSubscribedFilesResult_t.FromPointer), RemoteStorageEnumerateUserSubscribedFilesResult_t.StructSize(), 1314);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00015044 File Offset: 0x00013244
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageEnumerateUserSubscribedFilesResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageEnumerateUserSubscribedFilesResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageEnumerateUserSubscribedFilesResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageEnumerateUserSubscribedFilesResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageEnumerateUserSubscribedFilesResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageEnumerateUserSubscribedFilesResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageEnumerateUserSubscribedFilesResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageEnumerateUserSubscribedFilesResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageEnumerateUserSubscribedFilesResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageEnumerateUserSubscribedFilesResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageEnumerateUserSubscribedFilesResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageEnumerateUserSubscribedFilesResult_t.OnGetSize)
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
				CallbackId = 1314
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1314);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001534A File Offset: 0x0001354A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageEnumerateUserSubscribedFilesResult_t.OnResult(param);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00015352 File Offset: 0x00013552
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageEnumerateUserSubscribedFilesResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001535C File Offset: 0x0001355C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageEnumerateUserSubscribedFilesResult_t.OnGetSize();
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00015363 File Offset: 0x00013563
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageEnumerateUserSubscribedFilesResult_t.StructSize();
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001536A File Offset: 0x0001356A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageEnumerateUserSubscribedFilesResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001537C File Offset: 0x0001357C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageEnumerateUserSubscribedFilesResult_t data = RemoteStorageEnumerateUserSubscribedFilesResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageEnumerateUserSubscribedFilesResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageEnumerateUserSubscribedFilesResult_t>(data);
			}
		}

		// Token: 0x04000570 RID: 1392
		internal const int CallbackId = 1314;

		// Token: 0x04000571 RID: 1393
		internal Result Result;

		// Token: 0x04000572 RID: 1394
		internal int ResultsReturned;

		// Token: 0x04000573 RID: 1395
		internal int TotalResultCount;

		// Token: 0x04000574 RID: 1396
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
		internal ulong[] GPublishedFileId;

		// Token: 0x04000575 RID: 1397
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
		internal uint[] GRTimeSubscribed;

		// Token: 0x020001CF RID: 463
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D0B RID: 7435 RVA: 0x00061E1C File Offset: 0x0006001C
			public static implicit operator RemoteStorageEnumerateUserSubscribedFilesResult_t(RemoteStorageEnumerateUserSubscribedFilesResult_t.PackSmall d)
			{
				return new RemoteStorageEnumerateUserSubscribedFilesResult_t
				{
					Result = d.Result,
					ResultsReturned = d.ResultsReturned,
					TotalResultCount = d.TotalResultCount,
					GPublishedFileId = d.GPublishedFileId,
					GRTimeSubscribed = d.GRTimeSubscribed
				};
			}

			// Token: 0x040009EA RID: 2538
			internal Result Result;

			// Token: 0x040009EB RID: 2539
			internal int ResultsReturned;

			// Token: 0x040009EC RID: 2540
			internal int TotalResultCount;

			// Token: 0x040009ED RID: 2541
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U8)]
			internal ulong[] GPublishedFileId;

			// Token: 0x040009EE RID: 2542
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50, ArraySubType = UnmanagedType.U4)]
			internal uint[] GRTimeSubscribed;
		}
	}
}
