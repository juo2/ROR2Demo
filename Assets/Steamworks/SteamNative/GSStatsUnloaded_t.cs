using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000121 RID: 289
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GSStatsUnloaded_t
	{
		// Token: 0x06000920 RID: 2336 RVA: 0x0002F83C File Offset: 0x0002DA3C
		internal static GSStatsUnloaded_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GSStatsUnloaded_t.PackSmall)Marshal.PtrToStructure(p, typeof(GSStatsUnloaded_t.PackSmall));
			}
			return (GSStatsUnloaded_t)Marshal.PtrToStructure(p, typeof(GSStatsUnloaded_t));
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0002F875 File Offset: 0x0002DA75
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GSStatsUnloaded_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GSStatsUnloaded_t));
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0002F8A0 File Offset: 0x0002DAA0
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
						ResultA = new Callback.VTableWinThis.ResultD(GSStatsUnloaded_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GSStatsUnloaded_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GSStatsUnloaded_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GSStatsUnloaded_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GSStatsUnloaded_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GSStatsUnloaded_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GSStatsUnloaded_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GSStatsUnloaded_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GSStatsUnloaded_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GSStatsUnloaded_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GSStatsUnloaded_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GSStatsUnloaded_t.OnGetSize)
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
				CallbackId = 1108
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1108);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0002FBA6 File Offset: 0x0002DDA6
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GSStatsUnloaded_t.OnResult(param);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0002FBAE File Offset: 0x0002DDAE
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GSStatsUnloaded_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0002FBB8 File Offset: 0x0002DDB8
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GSStatsUnloaded_t.OnGetSize();
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0002FBBF File Offset: 0x0002DDBF
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GSStatsUnloaded_t.StructSize();
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0002FBC6 File Offset: 0x0002DDC6
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GSStatsUnloaded_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0002FBD8 File Offset: 0x0002DDD8
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GSStatsUnloaded_t data = GSStatsUnloaded_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GSStatsUnloaded_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GSStatsUnloaded_t>(data);
			}
		}

		// Token: 0x0400076F RID: 1903
		internal const int CallbackId = 1108;

		// Token: 0x04000770 RID: 1904
		internal ulong SteamIDUser;

		// Token: 0x02000245 RID: 581
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D81 RID: 7553 RVA: 0x00063D44 File Offset: 0x00061F44
			public static implicit operator GSStatsUnloaded_t(GSStatsUnloaded_t.PackSmall d)
			{
				return new GSStatsUnloaded_t
				{
					SteamIDUser = d.SteamIDUser
				};
			}

			// Token: 0x04000B7D RID: 2941
			internal ulong SteamIDUser;
		}
	}
}
