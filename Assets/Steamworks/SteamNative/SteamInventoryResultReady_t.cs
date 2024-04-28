using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200010D RID: 269
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamInventoryResultReady_t
	{
		// Token: 0x06000864 RID: 2148 RVA: 0x0002AA65 File Offset: 0x00028C65
		internal static SteamInventoryResultReady_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamInventoryResultReady_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamInventoryResultReady_t.PackSmall));
			}
			return (SteamInventoryResultReady_t)Marshal.PtrToStructure(p, typeof(SteamInventoryResultReady_t));
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0002AA9E File Offset: 0x00028C9E
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamInventoryResultReady_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamInventoryResultReady_t));
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0002AAC8 File Offset: 0x00028CC8
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
						ResultA = new Callback.VTableWinThis.ResultD(SteamInventoryResultReady_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SteamInventoryResultReady_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SteamInventoryResultReady_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SteamInventoryResultReady_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SteamInventoryResultReady_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SteamInventoryResultReady_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SteamInventoryResultReady_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SteamInventoryResultReady_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SteamInventoryResultReady_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SteamInventoryResultReady_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SteamInventoryResultReady_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SteamInventoryResultReady_t.OnGetSize)
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
				CallbackId = 4700
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4700);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0002ADCE File Offset: 0x00028FCE
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SteamInventoryResultReady_t.OnResult(param);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0002ADD6 File Offset: 0x00028FD6
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SteamInventoryResultReady_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0002ADE0 File Offset: 0x00028FE0
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SteamInventoryResultReady_t.OnGetSize();
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0002ADE7 File Offset: 0x00028FE7
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SteamInventoryResultReady_t.StructSize();
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0002ADEE File Offset: 0x00028FEE
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SteamInventoryResultReady_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0002AE00 File Offset: 0x00029000
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SteamInventoryResultReady_t data = SteamInventoryResultReady_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SteamInventoryResultReady_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SteamInventoryResultReady_t>(data);
			}
		}

		// Token: 0x04000725 RID: 1829
		internal const int CallbackId = 4700;

		// Token: 0x04000726 RID: 1830
		internal int Handle;

		// Token: 0x04000727 RID: 1831
		internal Result Result;

		// Token: 0x02000231 RID: 561
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D6D RID: 7533 RVA: 0x000638B4 File Offset: 0x00061AB4
			public static implicit operator SteamInventoryResultReady_t(SteamInventoryResultReady_t.PackSmall d)
			{
				return new SteamInventoryResultReady_t
				{
					Handle = d.Handle,
					Result = d.Result
				};
			}

			// Token: 0x04000B47 RID: 2887
			internal int Handle;

			// Token: 0x04000B48 RID: 2888
			internal Result Result;
		}
	}
}
