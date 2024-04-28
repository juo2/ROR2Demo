using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000091 RID: 145
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GamepadTextInputDismissed_t
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x0000F8D0 File Offset: 0x0000DAD0
		internal static GamepadTextInputDismissed_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GamepadTextInputDismissed_t.PackSmall)Marshal.PtrToStructure(p, typeof(GamepadTextInputDismissed_t.PackSmall));
			}
			return (GamepadTextInputDismissed_t)Marshal.PtrToStructure(p, typeof(GamepadTextInputDismissed_t));
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000F909 File Offset: 0x0000DB09
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GamepadTextInputDismissed_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GamepadTextInputDismissed_t));
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000F934 File Offset: 0x0000DB34
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
						ResultA = new Callback.VTableWinThis.ResultD(GamepadTextInputDismissed_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GamepadTextInputDismissed_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GamepadTextInputDismissed_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GamepadTextInputDismissed_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GamepadTextInputDismissed_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GamepadTextInputDismissed_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GamepadTextInputDismissed_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GamepadTextInputDismissed_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GamepadTextInputDismissed_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GamepadTextInputDismissed_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GamepadTextInputDismissed_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GamepadTextInputDismissed_t.OnGetSize)
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
				CallbackId = 714
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 714);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000FC3A File Offset: 0x0000DE3A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GamepadTextInputDismissed_t.OnResult(param);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000FC42 File Offset: 0x0000DE42
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GamepadTextInputDismissed_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000FC4C File Offset: 0x0000DE4C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GamepadTextInputDismissed_t.OnGetSize();
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000FC53 File Offset: 0x0000DE53
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GamepadTextInputDismissed_t.StructSize();
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000FC5A File Offset: 0x0000DE5A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GamepadTextInputDismissed_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000FC6C File Offset: 0x0000DE6C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GamepadTextInputDismissed_t data = GamepadTextInputDismissed_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GamepadTextInputDismissed_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GamepadTextInputDismissed_t>(data);
			}
		}

		// Token: 0x040004FE RID: 1278
		internal const int CallbackId = 714;

		// Token: 0x040004FF RID: 1279
		[MarshalAs(UnmanagedType.I1)]
		internal bool Submitted;

		// Token: 0x04000500 RID: 1280
		internal uint SubmittedText;

		// Token: 0x020001B5 RID: 437
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CF1 RID: 7409 RVA: 0x0006170C File Offset: 0x0005F90C
			public static implicit operator GamepadTextInputDismissed_t(GamepadTextInputDismissed_t.PackSmall d)
			{
				return new GamepadTextInputDismissed_t
				{
					Submitted = d.Submitted,
					SubmittedText = d.SubmittedText
				};
			}

			// Token: 0x0400098E RID: 2446
			[MarshalAs(UnmanagedType.I1)]
			internal bool Submitted;

			// Token: 0x0400098F RID: 2447
			internal uint SubmittedText;
		}
	}
}
