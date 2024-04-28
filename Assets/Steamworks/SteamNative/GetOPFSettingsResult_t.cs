using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000114 RID: 276
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetOPFSettingsResult_t
	{
		// Token: 0x060008A6 RID: 2214 RVA: 0x0002C5AC File Offset: 0x0002A7AC
		internal static GetOPFSettingsResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GetOPFSettingsResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(GetOPFSettingsResult_t.PackSmall));
			}
			return (GetOPFSettingsResult_t)Marshal.PtrToStructure(p, typeof(GetOPFSettingsResult_t));
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0002C5E5 File Offset: 0x0002A7E5
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GetOPFSettingsResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GetOPFSettingsResult_t));
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0002C610 File Offset: 0x0002A810
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
						ResultA = new Callback.VTableWinThis.ResultD(GetOPFSettingsResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GetOPFSettingsResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GetOPFSettingsResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GetOPFSettingsResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GetOPFSettingsResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GetOPFSettingsResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GetOPFSettingsResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GetOPFSettingsResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GetOPFSettingsResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GetOPFSettingsResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GetOPFSettingsResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GetOPFSettingsResult_t.OnGetSize)
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
				CallbackId = 4624
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4624);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0002C916 File Offset: 0x0002AB16
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GetOPFSettingsResult_t.OnResult(param);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0002C91E File Offset: 0x0002AB1E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GetOPFSettingsResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0002C928 File Offset: 0x0002AB28
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GetOPFSettingsResult_t.OnGetSize();
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0002C92F File Offset: 0x0002AB2F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GetOPFSettingsResult_t.StructSize();
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0002C936 File Offset: 0x0002AB36
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GetOPFSettingsResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0002C948 File Offset: 0x0002AB48
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GetOPFSettingsResult_t data = GetOPFSettingsResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GetOPFSettingsResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GetOPFSettingsResult_t>(data);
			}
		}

		// Token: 0x0400073C RID: 1852
		internal const int CallbackId = 4624;

		// Token: 0x0400073D RID: 1853
		internal Result Result;

		// Token: 0x0400073E RID: 1854
		internal uint VideoAppID;

		// Token: 0x02000238 RID: 568
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D74 RID: 7540 RVA: 0x00063A28 File Offset: 0x00061C28
			public static implicit operator GetOPFSettingsResult_t(GetOPFSettingsResult_t.PackSmall d)
			{
				return new GetOPFSettingsResult_t
				{
					Result = d.Result,
					VideoAppID = d.VideoAppID
				};
			}

			// Token: 0x04000B57 RID: 2903
			internal Result Result;

			// Token: 0x04000B58 RID: 2904
			internal uint VideoAppID;
		}
	}
}
