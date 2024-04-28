using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000113 RID: 275
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetVideoURLResult_t
	{
		// Token: 0x0600089D RID: 2205 RVA: 0x0002C1D4 File Offset: 0x0002A3D4
		internal static GetVideoURLResult_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GetVideoURLResult_t.PackSmall)Marshal.PtrToStructure(p, typeof(GetVideoURLResult_t.PackSmall));
			}
			return (GetVideoURLResult_t)Marshal.PtrToStructure(p, typeof(GetVideoURLResult_t));
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0002C20D File Offset: 0x0002A40D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GetVideoURLResult_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GetVideoURLResult_t));
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0002C238 File Offset: 0x0002A438
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
						ResultA = new Callback.VTableWinThis.ResultD(GetVideoURLResult_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GetVideoURLResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GetVideoURLResult_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GetVideoURLResult_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GetVideoURLResult_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GetVideoURLResult_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GetVideoURLResult_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GetVideoURLResult_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GetVideoURLResult_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GetVideoURLResult_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GetVideoURLResult_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GetVideoURLResult_t.OnGetSize)
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
				CallbackId = 4611
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4611);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0002C53E File Offset: 0x0002A73E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GetVideoURLResult_t.OnResult(param);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0002C546 File Offset: 0x0002A746
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GetVideoURLResult_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0002C550 File Offset: 0x0002A750
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GetVideoURLResult_t.OnGetSize();
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0002C557 File Offset: 0x0002A757
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GetVideoURLResult_t.StructSize();
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0002C55E File Offset: 0x0002A75E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GetVideoURLResult_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0002C570 File Offset: 0x0002A770
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GetVideoURLResult_t data = GetVideoURLResult_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GetVideoURLResult_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GetVideoURLResult_t>(data);
			}
		}

		// Token: 0x04000738 RID: 1848
		internal const int CallbackId = 4611;

		// Token: 0x04000739 RID: 1849
		internal Result Result;

		// Token: 0x0400073A RID: 1850
		internal uint VideoAppID;

		// Token: 0x0400073B RID: 1851
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string URL;

		// Token: 0x02000237 RID: 567
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D73 RID: 7539 RVA: 0x000639E8 File Offset: 0x00061BE8
			public static implicit operator GetVideoURLResult_t(GetVideoURLResult_t.PackSmall d)
			{
				return new GetVideoURLResult_t
				{
					Result = d.Result,
					VideoAppID = d.VideoAppID,
					URL = d.URL
				};
			}

			// Token: 0x04000B54 RID: 2900
			internal Result Result;

			// Token: 0x04000B55 RID: 2901
			internal uint VideoAppID;

			// Token: 0x04000B56 RID: 2902
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			internal string URL;
		}
	}
}
