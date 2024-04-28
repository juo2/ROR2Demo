using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x020000F3 RID: 243
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamAppInstalled_t
	{
		// Token: 0x06000795 RID: 1941 RVA: 0x00025434 File Offset: 0x00023634
		internal static SteamAppInstalled_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamAppInstalled_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamAppInstalled_t.PackSmall));
			}
			return (SteamAppInstalled_t)Marshal.PtrToStructure(p, typeof(SteamAppInstalled_t));
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0002546D File Offset: 0x0002366D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamAppInstalled_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamAppInstalled_t));
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00025498 File Offset: 0x00023698
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
						ResultA = new Callback.VTableWinThis.ResultD(SteamAppInstalled_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SteamAppInstalled_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SteamAppInstalled_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SteamAppInstalled_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SteamAppInstalled_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SteamAppInstalled_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SteamAppInstalled_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SteamAppInstalled_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SteamAppInstalled_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SteamAppInstalled_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SteamAppInstalled_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SteamAppInstalled_t.OnGetSize)
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
				CallbackId = 3901
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 3901);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0002579E File Offset: 0x0002399E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SteamAppInstalled_t.OnResult(param);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x000257A6 File Offset: 0x000239A6
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SteamAppInstalled_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000257B0 File Offset: 0x000239B0
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SteamAppInstalled_t.OnGetSize();
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x000257B7 File Offset: 0x000239B7
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SteamAppInstalled_t.StructSize();
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x000257BE File Offset: 0x000239BE
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SteamAppInstalled_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x000257D0 File Offset: 0x000239D0
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SteamAppInstalled_t data = SteamAppInstalled_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SteamAppInstalled_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SteamAppInstalled_t>(data);
			}
		}

		// Token: 0x040006B8 RID: 1720
		internal const int CallbackId = 3901;

		// Token: 0x040006B9 RID: 1721
		internal uint AppID;

		// Token: 0x02000217 RID: 535
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D53 RID: 7507 RVA: 0x000631F4 File Offset: 0x000613F4
			public static implicit operator SteamAppInstalled_t(SteamAppInstalled_t.PackSmall d)
			{
				return new SteamAppInstalled_t
				{
					AppID = d.AppID
				};
			}

			// Token: 0x04000AF0 RID: 2800
			internal uint AppID;
		}
	}
}
