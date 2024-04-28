using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000E6 RID: 230
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SubmitItemUpdateResult_t
	{
		// Token: 0x06000714 RID: 1812 RVA: 0x000220BC File Offset: 0x000202BC
		internal static SubmitItemUpdateResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SubmitItemUpdateResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(SubmitItemUpdateResult_t.PackSmall));
			}
			return (SubmitItemUpdateResult_t)Marshal.PtrToStructure(p, typeof(SubmitItemUpdateResult_t));
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x000220F5 File Offset: 0x000202F5
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SubmitItemUpdateResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SubmitItemUpdateResult_t));
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0002211D File Offset: 0x0002031D
		internal static CallResult<SubmitItemUpdateResult_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<SubmitItemUpdateResult_t, bool> CallbackFunction)
		{
			return new CallResult<SubmitItemUpdateResult_t>(steamworks, call, CallbackFunction, new CallResult<SubmitItemUpdateResult_t>.ConvertFromPointer(SubmitItemUpdateResult_t.FromPointer), SubmitItemUpdateResult_t.StructSize(), 3404);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00022140 File Offset: 0x00020340
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
						ResultA = new Callback.VTableWinThis.ResultD(SubmitItemUpdateResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SubmitItemUpdateResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SubmitItemUpdateResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SubmitItemUpdateResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SubmitItemUpdateResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SubmitItemUpdateResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SubmitItemUpdateResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SubmitItemUpdateResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SubmitItemUpdateResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SubmitItemUpdateResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SubmitItemUpdateResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SubmitItemUpdateResult_t.OnGetSize)
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
				CallbackId = 3404
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3404);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00022446 File Offset: 0x00020646
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SubmitItemUpdateResult_t.OnResult(param);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0002244E File Offset: 0x0002064E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SubmitItemUpdateResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00022458 File Offset: 0x00020658
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SubmitItemUpdateResult_t.OnGetSize();
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0002245F File Offset: 0x0002065F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SubmitItemUpdateResult_t.StructSize();
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00022466 File Offset: 0x00020666
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SubmitItemUpdateResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00022478 File Offset: 0x00020678
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SubmitItemUpdateResult_t data = SubmitItemUpdateResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SubmitItemUpdateResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SubmitItemUpdateResult_t>(data);
			}
		}

		// Token: 0x04000685 RID: 1669
		internal const int CallbackId = 3404;

		// Token: 0x04000686 RID: 1670
		internal Result Result;

		// Token: 0x04000687 RID: 1671
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement;

		// Token: 0x04000688 RID: 1672
		internal ulong PublishedFileId;

		// Token: 0x0200020A RID: 522
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D46 RID: 7494 RVA: 0x00062ECC File Offset: 0x000610CC
			public static implicit operator SubmitItemUpdateResult_t(SubmitItemUpdateResult_t.PackSmall d)
			{
				return new SubmitItemUpdateResult_t
				{
					Result = d.Result,
					UserNeedsToAcceptWorkshopLegalAgreement = d.UserNeedsToAcceptWorkshopLegalAgreement,
					PublishedFileId = d.PublishedFileId
				};
			}

			// Token: 0x04000ACA RID: 2762
			internal Result Result;

			// Token: 0x04000ACB RID: 2763
			[MarshalAs(UnmanagedType.I1)]
			internal bool UserNeedsToAcceptWorkshopLegalAgreement;

			// Token: 0x04000ACC RID: 2764
			internal ulong PublishedFileId;
		}
	}
}
