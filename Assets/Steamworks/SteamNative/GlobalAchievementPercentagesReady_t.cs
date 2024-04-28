using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000C8 RID: 200
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GlobalAchievementPercentagesReady_t
	{
		// Token: 0x06000623 RID: 1571 RVA: 0x0001BE00 File Offset: 0x0001A000
		internal static GlobalAchievementPercentagesReady_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GlobalAchievementPercentagesReady_t.PackSmall)Marshal.PtrToStructure(p, typeof(GlobalAchievementPercentagesReady_t.PackSmall));
			}
			return (GlobalAchievementPercentagesReady_t)Marshal.PtrToStructure(p, typeof(GlobalAchievementPercentagesReady_t));
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001BE39 File Offset: 0x0001A039
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GlobalAchievementPercentagesReady_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GlobalAchievementPercentagesReady_t));
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001BE61 File Offset: 0x0001A061
		internal static CallResult<GlobalAchievementPercentagesReady_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<GlobalAchievementPercentagesReady_t, bool> CallbackFunction)
		{
			return new CallResult<GlobalAchievementPercentagesReady_t>(steamworks, call, CallbackFunction, new CallResult<GlobalAchievementPercentagesReady_t>.ConvertFromPointer(GlobalAchievementPercentagesReady_t.FromPointer), GlobalAchievementPercentagesReady_t.StructSize(), 1110);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001BE84 File Offset: 0x0001A084
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
						ResultA = new Callback.VTableWinThis.ResultD(GlobalAchievementPercentagesReady_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GlobalAchievementPercentagesReady_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GlobalAchievementPercentagesReady_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GlobalAchievementPercentagesReady_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GlobalAchievementPercentagesReady_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GlobalAchievementPercentagesReady_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GlobalAchievementPercentagesReady_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GlobalAchievementPercentagesReady_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GlobalAchievementPercentagesReady_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GlobalAchievementPercentagesReady_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GlobalAchievementPercentagesReady_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GlobalAchievementPercentagesReady_t.OnGetSize)
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
				CallbackId = 1110
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1110);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001C18A File Offset: 0x0001A38A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GlobalAchievementPercentagesReady_t.OnResult(param);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001C192 File Offset: 0x0001A392
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GlobalAchievementPercentagesReady_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001C19C File Offset: 0x0001A39C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GlobalAchievementPercentagesReady_t.OnGetSize();
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001C1A3 File Offset: 0x0001A3A3
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GlobalAchievementPercentagesReady_t.StructSize();
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001C1AA File Offset: 0x0001A3AA
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GlobalAchievementPercentagesReady_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001C1BC File Offset: 0x0001A3BC
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GlobalAchievementPercentagesReady_t data = GlobalAchievementPercentagesReady_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GlobalAchievementPercentagesReady_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GlobalAchievementPercentagesReady_t>(data);
			}
		}

		// Token: 0x04000601 RID: 1537
		internal const int CallbackId = 1110;

		// Token: 0x04000602 RID: 1538
		internal ulong GameID;

		// Token: 0x04000603 RID: 1539
		internal Result Result;

		// Token: 0x020001EC RID: 492
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D28 RID: 7464 RVA: 0x000626AC File Offset: 0x000608AC
			public static implicit operator GlobalAchievementPercentagesReady_t(GlobalAchievementPercentagesReady_t.PackSmall d)
			{
				return new GlobalAchievementPercentagesReady_t
				{
					GameID = d.GameID,
					Result = d.Result
				};
			}

			// Token: 0x04000A5F RID: 2655
			internal ulong GameID;

			// Token: 0x04000A60 RID: 2656
			internal Result Result;
		}
	}
}
