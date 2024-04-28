using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200011C RID: 284
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GSReputation_t
	{
		// Token: 0x060008EE RID: 2286 RVA: 0x0002E464 File Offset: 0x0002C664
		internal static GSReputation_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GSReputation_t.PackSmall)Marshal.PtrToStructure(p, typeof(GSReputation_t.PackSmall));
			}
			return (GSReputation_t)Marshal.PtrToStructure(p, typeof(GSReputation_t));
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0002E49D File Offset: 0x0002C69D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GSReputation_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GSReputation_t));
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0002E4C5 File Offset: 0x0002C6C5
		internal static CallResult<GSReputation_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<GSReputation_t, bool> CallbackFunction)
		{
			return new CallResult<GSReputation_t>(steamworks, call, CallbackFunction, new CallResult<GSReputation_t>.ConvertFromPointer(GSReputation_t.FromPointer), GSReputation_t.StructSize(), 209);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0002E4E8 File Offset: 0x0002C6E8
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
						ResultA = new Callback.VTableWinThis.ResultD(GSReputation_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GSReputation_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GSReputation_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GSReputation_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GSReputation_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GSReputation_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GSReputation_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GSReputation_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GSReputation_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GSReputation_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GSReputation_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GSReputation_t.OnGetSize)
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
				CallbackId = 209
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 209);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0002E7EE File Offset: 0x0002C9EE
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GSReputation_t.OnResult(param);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0002E7F6 File Offset: 0x0002C9F6
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GSReputation_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0002E800 File Offset: 0x0002CA00
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GSReputation_t.OnGetSize();
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0002E807 File Offset: 0x0002CA07
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GSReputation_t.StructSize();
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0002E80E File Offset: 0x0002CA0E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GSReputation_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0002E820 File Offset: 0x0002CA20
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GSReputation_t data = GSReputation_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GSReputation_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GSReputation_t>(data);
			}
		}

		// Token: 0x04000759 RID: 1881
		internal const int CallbackId = 209;

		// Token: 0x0400075A RID: 1882
		internal Result Result;

		// Token: 0x0400075B RID: 1883
		internal uint ReputationScore;

		// Token: 0x0400075C RID: 1884
		[MarshalAs(UnmanagedType.I1)]
		internal bool Banned;

		// Token: 0x0400075D RID: 1885
		internal uint BannedIP;

		// Token: 0x0400075E RID: 1886
		internal ushort BannedPort;

		// Token: 0x0400075F RID: 1887
		internal ulong BannedGameID;

		// Token: 0x04000760 RID: 1888
		internal uint BanExpires;

		// Token: 0x02000240 RID: 576
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D7C RID: 7548 RVA: 0x00063BF4 File Offset: 0x00061DF4
			public static implicit operator GSReputation_t(GSReputation_t.PackSmall d)
			{
				return new GSReputation_t
				{
					Result = d.Result,
					ReputationScore = d.ReputationScore,
					Banned = d.Banned,
					BannedIP = d.BannedIP,
					BannedPort = d.BannedPort,
					BannedGameID = d.BannedGameID,
					BanExpires = d.BanExpires
				};
			}

			// Token: 0x04000B6C RID: 2924
			internal Result Result;

			// Token: 0x04000B6D RID: 2925
			internal uint ReputationScore;

			// Token: 0x04000B6E RID: 2926
			[MarshalAs(UnmanagedType.I1)]
			internal bool Banned;

			// Token: 0x04000B6F RID: 2927
			internal uint BannedIP;

			// Token: 0x04000B70 RID: 2928
			internal ushort BannedPort;

			// Token: 0x04000B71 RID: 2929
			internal ulong BannedGameID;

			// Token: 0x04000B72 RID: 2930
			internal uint BanExpires;
		}
	}
}
