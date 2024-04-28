using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000123 RID: 291
	internal struct SteamInventoryDefinitionUpdate_t
	{
		// Token: 0x06000932 RID: 2354 RVA: 0x0002FFEC File Offset: 0x0002E1EC
		internal static SteamInventoryDefinitionUpdate_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamInventoryDefinitionUpdate_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamInventoryDefinitionUpdate_t.PackSmall));
			}
			return (SteamInventoryDefinitionUpdate_t)Marshal.PtrToStructure(p, typeof(SteamInventoryDefinitionUpdate_t));
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00030025 File Offset: 0x0002E225
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamInventoryDefinitionUpdate_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamInventoryDefinitionUpdate_t));
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00030050 File Offset: 0x0002E250
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
						ResultA = new Callback.VTableWinThis.ResultD(SteamInventoryDefinitionUpdate_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SteamInventoryDefinitionUpdate_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SteamInventoryDefinitionUpdate_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SteamInventoryDefinitionUpdate_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SteamInventoryDefinitionUpdate_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SteamInventoryDefinitionUpdate_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SteamInventoryDefinitionUpdate_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SteamInventoryDefinitionUpdate_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SteamInventoryDefinitionUpdate_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SteamInventoryDefinitionUpdate_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SteamInventoryDefinitionUpdate_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SteamInventoryDefinitionUpdate_t.OnGetSize)
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
				CallbackId = 4702
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4702);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00030356 File Offset: 0x0002E556
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SteamInventoryDefinitionUpdate_t.OnResult(param);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0003035E File Offset: 0x0002E55E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SteamInventoryDefinitionUpdate_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00030368 File Offset: 0x0002E568
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SteamInventoryDefinitionUpdate_t.OnGetSize();
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0003036F File Offset: 0x0002E56F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SteamInventoryDefinitionUpdate_t.StructSize();
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00030376 File Offset: 0x0002E576
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SteamInventoryDefinitionUpdate_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00030388 File Offset: 0x0002E588
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SteamInventoryDefinitionUpdate_t data = SteamInventoryDefinitionUpdate_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SteamInventoryDefinitionUpdate_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SteamInventoryDefinitionUpdate_t>(data);
			}
		}

		// Token: 0x04000774 RID: 1908
		internal const int CallbackId = 4702;

		// Token: 0x02000247 RID: 583
		internal struct PackSmall
		{
			// Token: 0x06001D83 RID: 7555 RVA: 0x00063D98 File Offset: 0x00061F98
			public static implicit operator SteamInventoryDefinitionUpdate_t(SteamInventoryDefinitionUpdate_t.PackSmall d)
			{
				return default(SteamInventoryDefinitionUpdate_t);
			}
		}
	}
}
