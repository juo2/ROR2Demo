using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000116 RID: 278
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GSClientDeny_t
	{
		// Token: 0x060008B8 RID: 2232 RVA: 0x0002CD5C File Offset: 0x0002AF5C
		internal static GSClientDeny_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GSClientDeny_t.PackSmall)Marshal.PtrToStructure(p, typeof(GSClientDeny_t.PackSmall));
			}
			return (GSClientDeny_t)Marshal.PtrToStructure(p, typeof(GSClientDeny_t));
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0002CD95 File Offset: 0x0002AF95
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GSClientDeny_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GSClientDeny_t));
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0002CDC0 File Offset: 0x0002AFC0
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
						ResultA = new Callback.VTableWinThis.ResultD(GSClientDeny_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GSClientDeny_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GSClientDeny_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GSClientDeny_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GSClientDeny_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GSClientDeny_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GSClientDeny_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GSClientDeny_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GSClientDeny_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GSClientDeny_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GSClientDeny_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GSClientDeny_t.OnGetSize)
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
				CallbackId = 202
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 202);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0002D0C6 File Offset: 0x0002B2C6
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GSClientDeny_t.OnResult(param);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0002D0CE File Offset: 0x0002B2CE
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GSClientDeny_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0002D0D8 File Offset: 0x0002B2D8
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GSClientDeny_t.OnGetSize();
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0002D0DF File Offset: 0x0002B2DF
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GSClientDeny_t.StructSize();
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0002D0E6 File Offset: 0x0002B2E6
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GSClientDeny_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0002D0F8 File Offset: 0x0002B2F8
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GSClientDeny_t data = GSClientDeny_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GSClientDeny_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GSClientDeny_t>(data);
			}
		}

		// Token: 0x04000742 RID: 1858
		internal const int CallbackId = 202;

		// Token: 0x04000743 RID: 1859
		internal ulong SteamID;

		// Token: 0x04000744 RID: 1860
		internal DenyReason DenyReason;

		// Token: 0x04000745 RID: 1861
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string OptionalText;

		// Token: 0x0200023A RID: 570
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D76 RID: 7542 RVA: 0x00063A88 File Offset: 0x00061C88
			public static implicit operator GSClientDeny_t(GSClientDeny_t.PackSmall d)
			{
				return new GSClientDeny_t
				{
					SteamID = d.SteamID,
					DenyReason = d.DenyReason,
					OptionalText = d.OptionalText
				};
			}

			// Token: 0x04000B5B RID: 2907
			internal ulong SteamID;

			// Token: 0x04000B5C RID: 2908
			internal DenyReason DenyReason;

			// Token: 0x04000B5D RID: 2909
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string OptionalText;
		}
	}
}
