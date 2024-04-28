using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000A6 RID: 166
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RemoteStorageFileShareResult_t
	{
		// Token: 0x060004E2 RID: 1250 RVA: 0x00013C08 File Offset: 0x00011E08
		internal static RemoteStorageFileShareResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RemoteStorageFileShareResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(RemoteStorageFileShareResult_t.PackSmall));
			}
			return (RemoteStorageFileShareResult_t)Marshal.PtrToStructure(p, typeof(RemoteStorageFileShareResult_t));
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00013C41 File Offset: 0x00011E41
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RemoteStorageFileShareResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RemoteStorageFileShareResult_t));
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00013C69 File Offset: 0x00011E69
		internal static CallResult<RemoteStorageFileShareResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<RemoteStorageFileShareResult_t, bool> CallbackFunction)
		{
			return new CallResult<RemoteStorageFileShareResult_t>(steamworks, call, CallbackFunction, new CallResult<RemoteStorageFileShareResult_t>.ConvertFromPointer(RemoteStorageFileShareResult_t.FromPointer), RemoteStorageFileShareResult_t.StructSize(), 1307);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00013C8C File Offset: 0x00011E8C
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
						ResultA = new Callback.VTableWinThis.ResultD(RemoteStorageFileShareResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RemoteStorageFileShareResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RemoteStorageFileShareResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RemoteStorageFileShareResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RemoteStorageFileShareResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RemoteStorageFileShareResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RemoteStorageFileShareResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RemoteStorageFileShareResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RemoteStorageFileShareResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RemoteStorageFileShareResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RemoteStorageFileShareResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RemoteStorageFileShareResult_t.OnGetSize)
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
				CallbackId = 1307
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1307);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00013F92 File Offset: 0x00012192
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RemoteStorageFileShareResult_t.OnResult(param);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00013F9A File Offset: 0x0001219A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RemoteStorageFileShareResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00013FA4 File Offset: 0x000121A4
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RemoteStorageFileShareResult_t.OnGetSize();
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00013FAB File Offset: 0x000121AB
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RemoteStorageFileShareResult_t.StructSize();
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00013FB2 File Offset: 0x000121B2
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RemoteStorageFileShareResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00013FC4 File Offset: 0x000121C4
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RemoteStorageFileShareResult_t data = RemoteStorageFileShareResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RemoteStorageFileShareResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RemoteStorageFileShareResult_t>(data);
			}
		}

		// Token: 0x0400055D RID: 1373
		internal const int CallbackId = 1307;

		// Token: 0x0400055E RID: 1374
		internal Result Result;

		// Token: 0x0400055F RID: 1375
		internal ulong File;

		// Token: 0x04000560 RID: 1376
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string Filename;

		// Token: 0x020001CA RID: 458
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D06 RID: 7430 RVA: 0x00061CF0 File Offset: 0x0005FEF0
			public static implicit operator RemoteStorageFileShareResult_t(RemoteStorageFileShareResult_t.PackSmall d)
			{
				return new RemoteStorageFileShareResult_t
				{
					Result = d.Result,
					File = d.File,
					Filename = d.Filename
				};
			}

			// Token: 0x040009DC RID: 2524
			internal Result Result;

			// Token: 0x040009DD RID: 2525
			internal ulong File;

			// Token: 0x040009DE RID: 2526
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string Filename;
		}
	}
}
