using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000D5 RID: 213
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct VolumeHasChanged_t
	{
		// Token: 0x06000695 RID: 1685 RVA: 0x0001ED00 File Offset: 0x0001CF00
		internal static VolumeHasChanged_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (VolumeHasChanged_t.PackSmall)Marshal.PtrToStructure(p, typeof(VolumeHasChanged_t.PackSmall));
			}
			return (VolumeHasChanged_t)Marshal.PtrToStructure(p, typeof(VolumeHasChanged_t));
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001ED39 File Offset: 0x0001CF39
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(VolumeHasChanged_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(VolumeHasChanged_t));
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0001ED64 File Offset: 0x0001CF64
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
						ResultA = new Callback.VTableWinThis.ResultD(VolumeHasChanged_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(VolumeHasChanged_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(VolumeHasChanged_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(VolumeHasChanged_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(VolumeHasChanged_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(VolumeHasChanged_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(VolumeHasChanged_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(VolumeHasChanged_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(VolumeHasChanged_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(VolumeHasChanged_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(VolumeHasChanged_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(VolumeHasChanged_t.OnGetSize)
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
				CallbackId = 4002
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4002);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001F06A File Offset: 0x0001D26A
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			VolumeHasChanged_t.OnResult(param);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001F072 File Offset: 0x0001D272
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			VolumeHasChanged_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001F07C File Offset: 0x0001D27C
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return VolumeHasChanged_t.OnGetSize();
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001F083 File Offset: 0x0001D283
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return VolumeHasChanged_t.StructSize();
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001F08A File Offset: 0x0001D28A
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			VolumeHasChanged_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001F09C File Offset: 0x0001D29C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			VolumeHasChanged_t data = VolumeHasChanged_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<VolumeHasChanged_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<VolumeHasChanged_t>(data);
			}
		}

		// Token: 0x04000632 RID: 1586
		internal const int CallbackId = 4002;

		// Token: 0x04000633 RID: 1587
		internal float NewVolume;

		// Token: 0x020001F9 RID: 505
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D35 RID: 7477 RVA: 0x000629B8 File Offset: 0x00060BB8
			public static implicit operator VolumeHasChanged_t(VolumeHasChanged_t.PackSmall d)
			{
				return new VolumeHasChanged_t
				{
					NewVolume = d.NewVolume
				};
			}

			// Token: 0x04000A84 RID: 2692
			internal float NewVolume;
		}
	}
}
