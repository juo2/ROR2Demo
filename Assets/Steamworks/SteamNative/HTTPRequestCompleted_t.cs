using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000DC RID: 220
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTTPRequestCompleted_t
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x000207E8 File Offset: 0x0001E9E8
		internal static HTTPRequestCompleted_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (HTTPRequestCompleted_t.PackSmall)Marshal.PtrToStructure(p, typeof(HTTPRequestCompleted_t.PackSmall));
			}
			return (HTTPRequestCompleted_t)Marshal.PtrToStructure(p, typeof(HTTPRequestCompleted_t));
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00020821 File Offset: 0x0001EA21
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(HTTPRequestCompleted_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(HTTPRequestCompleted_t));
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0002084C File Offset: 0x0001EA4C
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
						ResultA = new Callback.VTableWinThis.ResultD(HTTPRequestCompleted_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(HTTPRequestCompleted_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(HTTPRequestCompleted_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(HTTPRequestCompleted_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(HTTPRequestCompleted_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(HTTPRequestCompleted_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(HTTPRequestCompleted_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(HTTPRequestCompleted_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(HTTPRequestCompleted_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(HTTPRequestCompleted_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(HTTPRequestCompleted_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(HTTPRequestCompleted_t.OnGetSize)
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
				CallbackId = 2101
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 2101);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00020B52 File Offset: 0x0001ED52
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			HTTPRequestCompleted_t.OnResult(param);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00020B5A File Offset: 0x0001ED5A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			HTTPRequestCompleted_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00020B64 File Offset: 0x0001ED64
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return HTTPRequestCompleted_t.OnGetSize();
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00020B6B File Offset: 0x0001ED6B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return HTTPRequestCompleted_t.StructSize();
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00020B72 File Offset: 0x0001ED72
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			HTTPRequestCompleted_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00020B84 File Offset: 0x0001ED84
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			HTTPRequestCompleted_t data = HTTPRequestCompleted_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<HTTPRequestCompleted_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<HTTPRequestCompleted_t>(data);
			}
		}

		// Token: 0x04000640 RID: 1600
		internal const int CallbackId = 2101;

		// Token: 0x04000641 RID: 1601
		internal uint Request;

		// Token: 0x04000642 RID: 1602
		internal ulong ContextValue;

		// Token: 0x04000643 RID: 1603
		[MarshalAs(UnmanagedType.I1)]
		internal bool RequestSuccessful;

		// Token: 0x04000644 RID: 1604
		internal HTTPStatusCode StatusCode;

		// Token: 0x04000645 RID: 1605
		internal uint BodySize;

		// Token: 0x02000200 RID: 512
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D3C RID: 7484 RVA: 0x00062AB4 File Offset: 0x00060CB4
			public static implicit operator HTTPRequestCompleted_t(HTTPRequestCompleted_t.PackSmall d)
			{
				return new HTTPRequestCompleted_t
				{
					Request = d.Request,
					ContextValue = d.ContextValue,
					RequestSuccessful = d.RequestSuccessful,
					StatusCode = d.StatusCode,
					BodySize = d.BodySize
				};
			}

			// Token: 0x04000A8B RID: 2699
			internal uint Request;

			// Token: 0x04000A8C RID: 2700
			internal ulong ContextValue;

			// Token: 0x04000A8D RID: 2701
			[MarshalAs(UnmanagedType.I1)]
			internal bool RequestSuccessful;

			// Token: 0x04000A8E RID: 2702
			internal HTTPStatusCode StatusCode;

			// Token: 0x04000A8F RID: 2703
			internal uint BodySize;
		}
	}
}
