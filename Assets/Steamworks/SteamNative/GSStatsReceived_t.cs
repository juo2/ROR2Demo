using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200011F RID: 287
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GSStatsReceived_t
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x0002F04C File Offset: 0x0002D24C
		internal static GSStatsReceived_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GSStatsReceived_t.PackSmall)Marshal.PtrToStructure(p, typeof(GSStatsReceived_t.PackSmall));
			}
			return (GSStatsReceived_t)Marshal.PtrToStructure(p, typeof(GSStatsReceived_t));
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0002F085 File Offset: 0x0002D285
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GSStatsReceived_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GSStatsReceived_t));
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0002F0AD File Offset: 0x0002D2AD
		internal static CallResult<GSStatsReceived_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<GSStatsReceived_t, bool> CallbackFunction)
		{
			return new CallResult<GSStatsReceived_t>(steamworks, call, CallbackFunction, new CallResult<GSStatsReceived_t>.ConvertFromPointer(GSStatsReceived_t.FromPointer), GSStatsReceived_t.StructSize(), 1800);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0002F0D0 File Offset: 0x0002D2D0
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
						ResultA = new Callback.VTableWinThis.ResultD(GSStatsReceived_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GSStatsReceived_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GSStatsReceived_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GSStatsReceived_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GSStatsReceived_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GSStatsReceived_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GSStatsReceived_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GSStatsReceived_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GSStatsReceived_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GSStatsReceived_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GSStatsReceived_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GSStatsReceived_t.OnGetSize)
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
				CallbackId = 1800
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1800);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x0002F3D6 File Offset: 0x0002D5D6
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GSStatsReceived_t.OnResult(param);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0002F3DE File Offset: 0x0002D5DE
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GSStatsReceived_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x0002F3E8 File Offset: 0x0002D5E8
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GSStatsReceived_t.OnGetSize();
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0002F3EF File Offset: 0x0002D5EF
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GSStatsReceived_t.StructSize();
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0002F3F6 File Offset: 0x0002D5F6
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GSStatsReceived_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0002F408 File Offset: 0x0002D608
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GSStatsReceived_t data = GSStatsReceived_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GSStatsReceived_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GSStatsReceived_t>(data);
			}
		}

		// Token: 0x04000769 RID: 1897
		internal const int CallbackId = 1800;

		// Token: 0x0400076A RID: 1898
		internal Result Result;

		// Token: 0x0400076B RID: 1899
		internal ulong SteamIDUser;

		// Token: 0x02000243 RID: 579
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D7F RID: 7551 RVA: 0x00063CE4 File Offset: 0x00061EE4
			public static implicit operator GSStatsReceived_t(GSStatsReceived_t.PackSmall d)
			{
				return new GSStatsReceived_t
				{
					Result = d.Result,
					SteamIDUser = d.SteamIDUser
				};
			}

			// Token: 0x04000B79 RID: 2937
			internal Result Result;

			// Token: 0x04000B7A RID: 2938
			internal ulong SteamIDUser;
		}
	}
}
