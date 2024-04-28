using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200008F RID: 143
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamAPICallCompleted_t
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x0000F100 File Offset: 0x0000D300
		internal static SteamAPICallCompleted_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamAPICallCompleted_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamAPICallCompleted_t.PackSmall));
			}
			return (SteamAPICallCompleted_t)Marshal.PtrToStructure(p, typeof(SteamAPICallCompleted_t));
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000F139 File Offset: 0x0000D339
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamAPICallCompleted_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamAPICallCompleted_t));
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000F164 File Offset: 0x0000D364
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
						ResultA = new Callback.VTableWinThis.ResultD(SteamAPICallCompleted_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SteamAPICallCompleted_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SteamAPICallCompleted_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SteamAPICallCompleted_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SteamAPICallCompleted_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SteamAPICallCompleted_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SteamAPICallCompleted_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SteamAPICallCompleted_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SteamAPICallCompleted_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SteamAPICallCompleted_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SteamAPICallCompleted_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SteamAPICallCompleted_t.OnGetSize)
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
				CallbackId = 703
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 703);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000F46A File Offset: 0x0000D66A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SteamAPICallCompleted_t.OnResult(param);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000F472 File Offset: 0x0000D672
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SteamAPICallCompleted_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000F47C File Offset: 0x0000D67C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SteamAPICallCompleted_t.OnGetSize();
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000F483 File Offset: 0x0000D683
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SteamAPICallCompleted_t.StructSize();
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000F48A File Offset: 0x0000D68A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SteamAPICallCompleted_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000F49C File Offset: 0x0000D69C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SteamAPICallCompleted_t data = SteamAPICallCompleted_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SteamAPICallCompleted_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SteamAPICallCompleted_t>(data);
			}
		}

		// Token: 0x040004F8 RID: 1272
		internal const int CallbackId = 703;

		// Token: 0x040004F9 RID: 1273
		internal ulong AsyncCall;

		// Token: 0x040004FA RID: 1274
		internal int Callback;

		// Token: 0x040004FB RID: 1275
		internal uint ParamCount;

		// Token: 0x020001B3 RID: 435
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CEF RID: 7407 RVA: 0x000616A8 File Offset: 0x0005F8A8
			public static implicit operator SteamAPICallCompleted_t(SteamAPICallCompleted_t.PackSmall d)
			{
				return new SteamAPICallCompleted_t
				{
					AsyncCall = d.AsyncCall,
					Callback = d.Callback,
					ParamCount = d.ParamCount
				};
			}

			// Token: 0x0400098A RID: 2442
			internal ulong AsyncCall;

			// Token: 0x0400098B RID: 2443
			internal int Callback;

			// Token: 0x0400098C RID: 2444
			internal uint ParamCount;
		}
	}
}
