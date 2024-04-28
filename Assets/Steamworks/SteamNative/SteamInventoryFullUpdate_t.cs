using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200010E RID: 270
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamInventoryFullUpdate_t
	{
		// Token: 0x0600086D RID: 2157 RVA: 0x0002AE3C File Offset: 0x0002903C
		internal static SteamInventoryFullUpdate_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamInventoryFullUpdate_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamInventoryFullUpdate_t.PackSmall));
			}
			return (SteamInventoryFullUpdate_t)Marshal.PtrToStructure(p, typeof(SteamInventoryFullUpdate_t));
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0002AE75 File Offset: 0x00029075
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamInventoryFullUpdate_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamInventoryFullUpdate_t));
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0002AEA0 File Offset: 0x000290A0
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
						ResultA = new Callback.VTableWinThis.ResultD(SteamInventoryFullUpdate_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SteamInventoryFullUpdate_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SteamInventoryFullUpdate_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SteamInventoryFullUpdate_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SteamInventoryFullUpdate_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SteamInventoryFullUpdate_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SteamInventoryFullUpdate_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SteamInventoryFullUpdate_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SteamInventoryFullUpdate_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SteamInventoryFullUpdate_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SteamInventoryFullUpdate_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SteamInventoryFullUpdate_t.OnGetSize)
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
				CallbackId = 4701
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4701);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0002B1A6 File Offset: 0x000293A6
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SteamInventoryFullUpdate_t.OnResult(param);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0002B1AE File Offset: 0x000293AE
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SteamInventoryFullUpdate_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0002B1B8 File Offset: 0x000293B8
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SteamInventoryFullUpdate_t.OnGetSize();
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0002B1BF File Offset: 0x000293BF
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SteamInventoryFullUpdate_t.StructSize();
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0002B1C6 File Offset: 0x000293C6
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SteamInventoryFullUpdate_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0002B1D8 File Offset: 0x000293D8
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SteamInventoryFullUpdate_t data = SteamInventoryFullUpdate_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SteamInventoryFullUpdate_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SteamInventoryFullUpdate_t>(data);
			}
		}

		// Token: 0x04000728 RID: 1832
		internal const int CallbackId = 4701;

		// Token: 0x04000729 RID: 1833
		internal int Handle;

		// Token: 0x02000232 RID: 562
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D6E RID: 7534 RVA: 0x000638E4 File Offset: 0x00061AE4
			public static implicit operator SteamInventoryFullUpdate_t(SteamInventoryFullUpdate_t.PackSmall d)
			{
				return new SteamInventoryFullUpdate_t
				{
					Handle = d.Handle
				};
			}

			// Token: 0x04000B49 RID: 2889
			internal int Handle;
		}
	}
}
