using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000072 RID: 114
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamServersDisconnected_t
	{
		// Token: 0x0600032C RID: 812 RVA: 0x00008788 File Offset: 0x00006988
		internal static SteamServersDisconnected_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamServersDisconnected_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamServersDisconnected_t.PackSmall));
			}
			return (SteamServersDisconnected_t)Marshal.PtrToStructure(p, typeof(SteamServersDisconnected_t));
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000087C1 File Offset: 0x000069C1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamServersDisconnected_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamServersDisconnected_t));
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000087EC File Offset: 0x000069EC
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
						ResultA = new Callback.VTableWinThis.ResultD(SteamServersDisconnected_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SteamServersDisconnected_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SteamServersDisconnected_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SteamServersDisconnected_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SteamServersDisconnected_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SteamServersDisconnected_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SteamServersDisconnected_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SteamServersDisconnected_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SteamServersDisconnected_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SteamServersDisconnected_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SteamServersDisconnected_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SteamServersDisconnected_t.OnGetSize)
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
				CallbackId = 103
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 103);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00008AEC File Offset: 0x00006CEC
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SteamServersDisconnected_t.OnResult(param);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00008AF4 File Offset: 0x00006CF4
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SteamServersDisconnected_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00008AFE File Offset: 0x00006CFE
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SteamServersDisconnected_t.OnGetSize();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00008B05 File Offset: 0x00006D05
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SteamServersDisconnected_t.StructSize();
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00008B0C File Offset: 0x00006D0C
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SteamServersDisconnected_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00008B1C File Offset: 0x00006D1C
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SteamServersDisconnected_t data = SteamServersDisconnected_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SteamServersDisconnected_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SteamServersDisconnected_t>(data);
			}
		}

		// Token: 0x04000497 RID: 1175
		internal const int CallbackId = 103;

		// Token: 0x04000498 RID: 1176
		internal Result Result;

		// Token: 0x02000196 RID: 406
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CD2 RID: 7378 RVA: 0x00061078 File Offset: 0x0005F278
			public static implicit operator SteamServersDisconnected_t(SteamServersDisconnected_t.PackSmall d)
			{
				return new SteamServersDisconnected_t
				{
					Result = d.Result
				};
			}

			// Token: 0x04000944 RID: 2372
			internal Result Result;
		}
	}
}
