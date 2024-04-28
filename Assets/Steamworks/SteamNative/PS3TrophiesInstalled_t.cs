using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000CA RID: 202
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PS3TrophiesInstalled_t
	{
		// Token: 0x06000637 RID: 1591 RVA: 0x0001C5F0 File Offset: 0x0001A7F0
		internal static PS3TrophiesInstalled_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (PS3TrophiesInstalled_t.PackSmall)Marshal.PtrToStructure(p, typeof(PS3TrophiesInstalled_t.PackSmall));
			}
			return (PS3TrophiesInstalled_t)Marshal.PtrToStructure(p, typeof(PS3TrophiesInstalled_t));
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001C629 File Offset: 0x0001A829
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(PS3TrophiesInstalled_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(PS3TrophiesInstalled_t));
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001C654 File Offset: 0x0001A854
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
						ResultA = new Callback.VTableWinThis.ResultD(PS3TrophiesInstalled_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(PS3TrophiesInstalled_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(PS3TrophiesInstalled_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(PS3TrophiesInstalled_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(PS3TrophiesInstalled_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(PS3TrophiesInstalled_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(PS3TrophiesInstalled_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(PS3TrophiesInstalled_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(PS3TrophiesInstalled_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(PS3TrophiesInstalled_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(PS3TrophiesInstalled_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(PS3TrophiesInstalled_t.OnGetSize)
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
				CallbackId = 1112
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1112);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001C95A File Offset: 0x0001AB5A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			PS3TrophiesInstalled_t.OnResult(param);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001C962 File Offset: 0x0001AB62
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			PS3TrophiesInstalled_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001C96C File Offset: 0x0001AB6C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return PS3TrophiesInstalled_t.OnGetSize();
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001C973 File Offset: 0x0001AB73
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return PS3TrophiesInstalled_t.StructSize();
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001C97A File Offset: 0x0001AB7A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			PS3TrophiesInstalled_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001C98C File Offset: 0x0001AB8C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			PS3TrophiesInstalled_t data = PS3TrophiesInstalled_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<PS3TrophiesInstalled_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<PS3TrophiesInstalled_t>(data);
			}
		}

		// Token: 0x04000607 RID: 1543
		internal const int CallbackId = 1112;

		// Token: 0x04000608 RID: 1544
		internal ulong GameID;

		// Token: 0x04000609 RID: 1545
		internal Result Result;

		// Token: 0x0400060A RID: 1546
		internal ulong RequiredDiskSpace;

		// Token: 0x020001EE RID: 494
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D2A RID: 7466 RVA: 0x0006270C File Offset: 0x0006090C
			public static implicit operator PS3TrophiesInstalled_t(PS3TrophiesInstalled_t.PackSmall d)
			{
				return new PS3TrophiesInstalled_t
				{
					GameID = d.GameID,
					Result = d.Result,
					RequiredDiskSpace = d.RequiredDiskSpace
				};
			}

			// Token: 0x04000A63 RID: 2659
			internal ulong GameID;

			// Token: 0x04000A64 RID: 2660
			internal Result Result;

			// Token: 0x04000A65 RID: 2661
			internal ulong RequiredDiskSpace;
		}
	}
}
