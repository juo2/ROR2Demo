using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200008E RID: 142
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LowBatteryPower_t
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x0000ED28 File Offset: 0x0000CF28
		internal static LowBatteryPower_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (LowBatteryPower_t.PackSmall)Marshal.PtrToStructure(p, typeof(LowBatteryPower_t.PackSmall));
			}
			return (LowBatteryPower_t)Marshal.PtrToStructure(p, typeof(LowBatteryPower_t));
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000ED61 File Offset: 0x0000CF61
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(LowBatteryPower_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(LowBatteryPower_t));
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000ED8C File Offset: 0x0000CF8C
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
						ResultA = new Callback.VTableWinThis.ResultD(LowBatteryPower_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(LowBatteryPower_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(LowBatteryPower_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(LowBatteryPower_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(LowBatteryPower_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(LowBatteryPower_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(LowBatteryPower_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(LowBatteryPower_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(LowBatteryPower_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(LowBatteryPower_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(LowBatteryPower_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(LowBatteryPower_t.OnGetSize)
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
				CallbackId = 702
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 702);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000F092 File Offset: 0x0000D292
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			LowBatteryPower_t.OnResult(param);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000F09A File Offset: 0x0000D29A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			LowBatteryPower_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000F0A4 File Offset: 0x0000D2A4
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return LowBatteryPower_t.OnGetSize();
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000F0AB File Offset: 0x0000D2AB
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return LowBatteryPower_t.StructSize();
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000F0B2 File Offset: 0x0000D2B2
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			LowBatteryPower_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			LowBatteryPower_t data = LowBatteryPower_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<LowBatteryPower_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<LowBatteryPower_t>(data);
			}
		}

		// Token: 0x040004F6 RID: 1270
		internal const int CallbackId = 702;

		// Token: 0x040004F7 RID: 1271
		internal byte MinutesBatteryLeft;

		// Token: 0x020001B2 RID: 434
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CEE RID: 7406 RVA: 0x00061684 File Offset: 0x0005F884
			public static implicit operator LowBatteryPower_t(LowBatteryPower_t.PackSmall d)
			{
				return new LowBatteryPower_t
				{
					MinutesBatteryLeft = d.MinutesBatteryLeft
				};
			}

			// Token: 0x04000989 RID: 2441
			internal byte MinutesBatteryLeft;
		}
	}
}
