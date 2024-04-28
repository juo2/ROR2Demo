using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000CD RID: 205
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterActivationCodeResponse_t
	{
		// Token: 0x06000653 RID: 1619 RVA: 0x0001D198 File Offset: 0x0001B398
		internal static RegisterActivationCodeResponse_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (RegisterActivationCodeResponse_t.PackSmall)Marshal.PtrToStructure(p, typeof(RegisterActivationCodeResponse_t.PackSmall));
			}
			return (RegisterActivationCodeResponse_t)Marshal.PtrToStructure(p, typeof(RegisterActivationCodeResponse_t));
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001D1D1 File Offset: 0x0001B3D1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(RegisterActivationCodeResponse_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(RegisterActivationCodeResponse_t));
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001D1FC File Offset: 0x0001B3FC
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
						ResultA = new Callback.VTableWinThis.ResultD(RegisterActivationCodeResponse_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(RegisterActivationCodeResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(RegisterActivationCodeResponse_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(RegisterActivationCodeResponse_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(RegisterActivationCodeResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(RegisterActivationCodeResponse_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(RegisterActivationCodeResponse_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(RegisterActivationCodeResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(RegisterActivationCodeResponse_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(RegisterActivationCodeResponse_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(RegisterActivationCodeResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(RegisterActivationCodeResponse_t.OnGetSize)
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
				CallbackId = 1008
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1008);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001D502 File Offset: 0x0001B702
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			RegisterActivationCodeResponse_t.OnResult(param);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001D50A File Offset: 0x0001B70A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			RegisterActivationCodeResponse_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001D514 File Offset: 0x0001B714
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return RegisterActivationCodeResponse_t.OnGetSize();
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001D51B File Offset: 0x0001B71B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return RegisterActivationCodeResponse_t.StructSize();
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001D522 File Offset: 0x0001B722
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			RegisterActivationCodeResponse_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001D534 File Offset: 0x0001B734
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			RegisterActivationCodeResponse_t data = RegisterActivationCodeResponse_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<RegisterActivationCodeResponse_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<RegisterActivationCodeResponse_t>(data);
			}
		}

		// Token: 0x04000610 RID: 1552
		internal const int CallbackId = 1008;

		// Token: 0x04000611 RID: 1553
		internal RegisterActivationCodeResult Result;

		// Token: 0x04000612 RID: 1554
		internal uint PackageRegistered;

		// Token: 0x020001F1 RID: 497
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D2D RID: 7469 RVA: 0x000627A0 File Offset: 0x000609A0
			public static implicit operator RegisterActivationCodeResponse_t(RegisterActivationCodeResponse_t.PackSmall d)
			{
				return new RegisterActivationCodeResponse_t
				{
					Result = d.Result,
					PackageRegistered = d.PackageRegistered
				};
			}

			// Token: 0x04000A69 RID: 2665
			internal RegisterActivationCodeResult Result;

			// Token: 0x04000A6A RID: 2666
			internal uint PackageRegistered;
		}
	}
}
