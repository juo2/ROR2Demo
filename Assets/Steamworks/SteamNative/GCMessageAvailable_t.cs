using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000127 RID: 295
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GCMessageAvailable_t
	{
		// Token: 0x06000956 RID: 2390 RVA: 0x00030F44 File Offset: 0x0002F144
		internal static GCMessageAvailable_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GCMessageAvailable_t.PackSmall)Marshal.PtrToStructure(p, typeof(GCMessageAvailable_t.PackSmall));
			}
			return (GCMessageAvailable_t)Marshal.PtrToStructure(p, typeof(GCMessageAvailable_t));
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00030F7D File Offset: 0x0002F17D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GCMessageAvailable_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GCMessageAvailable_t));
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00030FA8 File Offset: 0x0002F1A8
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
						ResultA = new Callback.VTableWinThis.ResultD(GCMessageAvailable_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GCMessageAvailable_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GCMessageAvailable_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GCMessageAvailable_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GCMessageAvailable_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GCMessageAvailable_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GCMessageAvailable_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GCMessageAvailable_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GCMessageAvailable_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GCMessageAvailable_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GCMessageAvailable_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GCMessageAvailable_t.OnGetSize)
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
				CallbackId = 1701
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1701);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x000312AE File Offset: 0x0002F4AE
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GCMessageAvailable_t.OnResult(param);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x000312B6 File Offset: 0x0002F4B6
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GCMessageAvailable_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x000312C0 File Offset: 0x0002F4C0
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GCMessageAvailable_t.OnGetSize();
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x000312C7 File Offset: 0x0002F4C7
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GCMessageAvailable_t.StructSize();
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000312CE File Offset: 0x0002F4CE
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GCMessageAvailable_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x000312E0 File Offset: 0x0002F4E0
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GCMessageAvailable_t data = GCMessageAvailable_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GCMessageAvailable_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GCMessageAvailable_t>(data);
			}
		}

		// Token: 0x04000778 RID: 1912
		internal const int CallbackId = 1701;

		// Token: 0x04000779 RID: 1913
		internal uint MessageSize;

		// Token: 0x0200024B RID: 587
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D87 RID: 7559 RVA: 0x00063DF8 File Offset: 0x00061FF8
			public static implicit operator GCMessageAvailable_t(GCMessageAvailable_t.PackSmall d)
			{
				return new GCMessageAvailable_t
				{
					MessageSize = d.MessageSize
				};
			}

			// Token: 0x04000B80 RID: 2944
			internal uint MessageSize;
		}
	}
}
