using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000122 RID: 290
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ItemInstalled_t
	{
		// Token: 0x06000929 RID: 2345 RVA: 0x0002FC14 File Offset: 0x0002DE14
		internal static ItemInstalled_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (ItemInstalled_t.PackSmall)Marshal.PtrToStructure(p, typeof(ItemInstalled_t.PackSmall));
			}
			return (ItemInstalled_t)Marshal.PtrToStructure(p, typeof(ItemInstalled_t));
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0002FC4D File Offset: 0x0002DE4D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(ItemInstalled_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(ItemInstalled_t));
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x0002FC78 File Offset: 0x0002DE78
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
						ResultA = new Callback.VTableWinThis.ResultD(ItemInstalled_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(ItemInstalled_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(ItemInstalled_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(ItemInstalled_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(ItemInstalled_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(ItemInstalled_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(ItemInstalled_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(ItemInstalled_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(ItemInstalled_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(ItemInstalled_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(ItemInstalled_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(ItemInstalled_t.OnGetSize)
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
				CallbackId = 3405
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3405);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0002FF7E File Offset: 0x0002E17E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			ItemInstalled_t.OnResult(param);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0002FF86 File Offset: 0x0002E186
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			ItemInstalled_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0002FF90 File Offset: 0x0002E190
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return ItemInstalled_t.OnGetSize();
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0002FF97 File Offset: 0x0002E197
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return ItemInstalled_t.StructSize();
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0002FF9E File Offset: 0x0002E19E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			ItemInstalled_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0002FFB0 File Offset: 0x0002E1B0
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			ItemInstalled_t data = ItemInstalled_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<ItemInstalled_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<ItemInstalled_t>(data);
			}
		}

		// Token: 0x04000771 RID: 1905
		internal const int CallbackId = 3405;

		// Token: 0x04000772 RID: 1906
		internal uint AppID;

		// Token: 0x04000773 RID: 1907
		internal ulong PublishedFileId;

		// Token: 0x02000246 RID: 582
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D82 RID: 7554 RVA: 0x00063D68 File Offset: 0x00061F68
			public static implicit operator ItemInstalled_t(ItemInstalled_t.PackSmall d)
			{
				return new ItemInstalled_t
				{
					AppID = d.AppID,
					PublishedFileId = d.PublishedFileId
				};
			}

			// Token: 0x04000B7E RID: 2942
			internal uint AppID;

			// Token: 0x04000B7F RID: 2943
			internal ulong PublishedFileId;
		}
	}
}
