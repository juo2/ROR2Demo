using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000119 RID: 281
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GSPolicyResponse_t
	{
		// Token: 0x060008D3 RID: 2259 RVA: 0x0002D8E4 File Offset: 0x0002BAE4
		internal static GSPolicyResponse_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GSPolicyResponse_t.PackSmall)Marshal.PtrToStructure(p, typeof(GSPolicyResponse_t.PackSmall));
			}
			return (GSPolicyResponse_t)Marshal.PtrToStructure(p, typeof(GSPolicyResponse_t));
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0002D91D File Offset: 0x0002BB1D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GSPolicyResponse_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GSPolicyResponse_t));
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0002D948 File Offset: 0x0002BB48
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
						ResultA = new Callback.VTableWinThis.ResultD(GSPolicyResponse_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GSPolicyResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GSPolicyResponse_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GSPolicyResponse_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GSPolicyResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GSPolicyResponse_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GSPolicyResponse_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GSPolicyResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GSPolicyResponse_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GSPolicyResponse_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GSPolicyResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GSPolicyResponse_t.OnGetSize)
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
				CallbackId = 115
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 115);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0002DC48 File Offset: 0x0002BE48
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GSPolicyResponse_t.OnResult(param);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0002DC50 File Offset: 0x0002BE50
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GSPolicyResponse_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0002DC5A File Offset: 0x0002BE5A
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GSPolicyResponse_t.OnGetSize();
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0002DC61 File Offset: 0x0002BE61
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GSPolicyResponse_t.StructSize();
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002DC68 File Offset: 0x0002BE68
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GSPolicyResponse_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0002DC78 File Offset: 0x0002BE78
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GSPolicyResponse_t data = GSPolicyResponse_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GSPolicyResponse_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GSPolicyResponse_t>(data);
			}
		}

		// Token: 0x0400074D RID: 1869
		internal const int CallbackId = 115;

		// Token: 0x0400074E RID: 1870
		internal byte Secure;

		// Token: 0x0200023D RID: 573
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D79 RID: 7545 RVA: 0x00063B38 File Offset: 0x00061D38
			public static implicit operator GSPolicyResponse_t(GSPolicyResponse_t.PackSmall d)
			{
				return new GSPolicyResponse_t
				{
					Secure = d.Secure
				};
			}

			// Token: 0x04000B63 RID: 2915
			internal byte Secure;
		}
	}
}
