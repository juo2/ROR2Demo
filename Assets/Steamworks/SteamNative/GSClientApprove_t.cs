using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000115 RID: 277
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GSClientApprove_t
	{
		// Token: 0x060008AF RID: 2223 RVA: 0x0002C984 File Offset: 0x0002AB84
		internal static GSClientApprove_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GSClientApprove_t.PackSmall)Marshal.PtrToStructure(p, typeof(GSClientApprove_t.PackSmall));
			}
			return (GSClientApprove_t)Marshal.PtrToStructure(p, typeof(GSClientApprove_t));
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0002C9BD File Offset: 0x0002ABBD
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GSClientApprove_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GSClientApprove_t));
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0002C9E8 File Offset: 0x0002ABE8
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
						ResultA = new Callback.VTableWinThis.ResultD(GSClientApprove_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GSClientApprove_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GSClientApprove_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GSClientApprove_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GSClientApprove_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GSClientApprove_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GSClientApprove_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GSClientApprove_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GSClientApprove_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GSClientApprove_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GSClientApprove_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GSClientApprove_t.OnGetSize)
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
				CallbackId = 201
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 201);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0002CCEE File Offset: 0x0002AEEE
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GSClientApprove_t.OnResult(param);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0002CCF6 File Offset: 0x0002AEF6
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GSClientApprove_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0002CD00 File Offset: 0x0002AF00
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GSClientApprove_t.OnGetSize();
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0002CD07 File Offset: 0x0002AF07
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GSClientApprove_t.StructSize();
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0002CD0E File Offset: 0x0002AF0E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GSClientApprove_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0002CD20 File Offset: 0x0002AF20
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GSClientApprove_t data = GSClientApprove_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GSClientApprove_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GSClientApprove_t>(data);
			}
		}

		// Token: 0x0400073F RID: 1855
		internal const int CallbackId = 201;

		// Token: 0x04000740 RID: 1856
		internal ulong SteamID;

		// Token: 0x04000741 RID: 1857
		internal ulong OwnerSteamID;

		// Token: 0x02000239 RID: 569
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D75 RID: 7541 RVA: 0x00063A58 File Offset: 0x00061C58
			public static implicit operator GSClientApprove_t(GSClientApprove_t.PackSmall d)
			{
				return new GSClientApprove_t
				{
					SteamID = d.SteamID,
					OwnerSteamID = d.OwnerSteamID
				};
			}

			// Token: 0x04000B59 RID: 2905
			internal ulong SteamID;

			// Token: 0x04000B5A RID: 2906
			internal ulong OwnerSteamID;
		}
	}
}
