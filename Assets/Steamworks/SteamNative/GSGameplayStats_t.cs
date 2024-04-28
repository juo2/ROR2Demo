using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200011A RID: 282
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GSGameplayStats_t
	{
		// Token: 0x060008DC RID: 2268 RVA: 0x0002DCB4 File Offset: 0x0002BEB4
		internal static GSGameplayStats_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GSGameplayStats_t.PackSmall)Marshal.PtrToStructure(p, typeof(GSGameplayStats_t.PackSmall));
			}
			return (GSGameplayStats_t)Marshal.PtrToStructure(p, typeof(GSGameplayStats_t));
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0002DCED File Offset: 0x0002BEED
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GSGameplayStats_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GSGameplayStats_t));
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002DD18 File Offset: 0x0002BF18
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
						ResultA = new Callback.VTableWinThis.ResultD(GSGameplayStats_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GSGameplayStats_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GSGameplayStats_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GSGameplayStats_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GSGameplayStats_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GSGameplayStats_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GSGameplayStats_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GSGameplayStats_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GSGameplayStats_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GSGameplayStats_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GSGameplayStats_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GSGameplayStats_t.OnGetSize)
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
				CallbackId = 207
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 207);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0002E01E File Offset: 0x0002C21E
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GSGameplayStats_t.OnResult(param);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0002E026 File Offset: 0x0002C226
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GSGameplayStats_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0002E030 File Offset: 0x0002C230
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GSGameplayStats_t.OnGetSize();
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0002E037 File Offset: 0x0002C237
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GSGameplayStats_t.StructSize();
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0002E03E File Offset: 0x0002C23E
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GSGameplayStats_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0002E050 File Offset: 0x0002C250
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GSGameplayStats_t data = GSGameplayStats_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GSGameplayStats_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GSGameplayStats_t>(data);
			}
		}

		// Token: 0x0400074F RID: 1871
		internal const int CallbackId = 207;

		// Token: 0x04000750 RID: 1872
		internal Result Result;

		// Token: 0x04000751 RID: 1873
		internal int Rank;

		// Token: 0x04000752 RID: 1874
		internal uint TotalConnects;

		// Token: 0x04000753 RID: 1875
		internal uint TotalMinutesPlayed;

		// Token: 0x0200023E RID: 574
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D7A RID: 7546 RVA: 0x00063B5C File Offset: 0x00061D5C
			public static implicit operator GSGameplayStats_t(GSGameplayStats_t.PackSmall d)
			{
				return new GSGameplayStats_t
				{
					Result = d.Result,
					Rank = d.Rank,
					TotalConnects = d.TotalConnects,
					TotalMinutesPlayed = d.TotalMinutesPlayed
				};
			}

			// Token: 0x04000B64 RID: 2916
			internal Result Result;

			// Token: 0x04000B65 RID: 2917
			internal int Rank;

			// Token: 0x04000B66 RID: 2918
			internal uint TotalConnects;

			// Token: 0x04000B67 RID: 2919
			internal uint TotalMinutesPlayed;
		}
	}
}
