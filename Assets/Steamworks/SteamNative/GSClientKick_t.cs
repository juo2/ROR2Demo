using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000117 RID: 279
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GSClientKick_t
	{
		// Token: 0x060008C1 RID: 2241 RVA: 0x0002D134 File Offset: 0x0002B334
		internal static GSClientKick_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GSClientKick_t.PackSmall)Marshal.PtrToStructure(p, typeof(GSClientKick_t.PackSmall));
			}
			return (GSClientKick_t)Marshal.PtrToStructure(p, typeof(GSClientKick_t));
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0002D16D File Offset: 0x0002B36D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GSClientKick_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GSClientKick_t));
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0002D198 File Offset: 0x0002B398
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
						ResultA = new Callback.VTableWinThis.ResultD(GSClientKick_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GSClientKick_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GSClientKick_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GSClientKick_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GSClientKick_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GSClientKick_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GSClientKick_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GSClientKick_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GSClientKick_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GSClientKick_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GSClientKick_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GSClientKick_t.OnGetSize)
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
				CallbackId = 203
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 203);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0002D49E File Offset: 0x0002B69E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GSClientKick_t.OnResult(param);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0002D4A6 File Offset: 0x0002B6A6
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GSClientKick_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0002D4B0 File Offset: 0x0002B6B0
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GSClientKick_t.OnGetSize();
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0002D4B7 File Offset: 0x0002B6B7
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GSClientKick_t.StructSize();
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0002D4BE File Offset: 0x0002B6BE
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GSClientKick_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x0002D4D0 File Offset: 0x0002B6D0
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GSClientKick_t data = GSClientKick_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GSClientKick_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GSClientKick_t>(data);
			}
		}

		// Token: 0x04000746 RID: 1862
		internal const int CallbackId = 203;

		// Token: 0x04000747 RID: 1863
		internal ulong SteamID;

		// Token: 0x04000748 RID: 1864
		internal DenyReason DenyReason;

		// Token: 0x0200023B RID: 571
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D77 RID: 7543 RVA: 0x00063AC8 File Offset: 0x00061CC8
			public static implicit operator GSClientKick_t(GSClientKick_t.PackSmall d)
			{
				return new GSClientKick_t
				{
					SteamID = d.SteamID,
					DenyReason = d.DenyReason
				};
			}

			// Token: 0x04000B5E RID: 2910
			internal ulong SteamID;

			// Token: 0x04000B5F RID: 2911
			internal DenyReason DenyReason;
		}
	}
}
