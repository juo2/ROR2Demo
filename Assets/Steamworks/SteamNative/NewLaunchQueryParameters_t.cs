using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000126 RID: 294
	internal struct NewLaunchQueryParameters_t
	{
		// Token: 0x0600094D RID: 2381 RVA: 0x00030B6C File Offset: 0x0002ED6C
		internal static NewLaunchQueryParameters_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (NewLaunchQueryParameters_t.PackSmall)Marshal.PtrToStructure(p, typeof(NewLaunchQueryParameters_t.PackSmall));
			}
			return (NewLaunchQueryParameters_t)Marshal.PtrToStructure(p, typeof(NewLaunchQueryParameters_t));
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00030BA5 File Offset: 0x0002EDA5
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(NewLaunchQueryParameters_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(NewLaunchQueryParameters_t));
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00030BD0 File Offset: 0x0002EDD0
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
						ResultA = new Callback.VTableWinThis.ResultD(NewLaunchQueryParameters_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(NewLaunchQueryParameters_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(NewLaunchQueryParameters_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(NewLaunchQueryParameters_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(NewLaunchQueryParameters_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(NewLaunchQueryParameters_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(NewLaunchQueryParameters_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(NewLaunchQueryParameters_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(NewLaunchQueryParameters_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(NewLaunchQueryParameters_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(NewLaunchQueryParameters_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(NewLaunchQueryParameters_t.OnGetSize)
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
				CallbackId = 1014
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1014);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00030ED6 File Offset: 0x0002F0D6
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			NewLaunchQueryParameters_t.OnResult(param);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00030EDE File Offset: 0x0002F0DE
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			NewLaunchQueryParameters_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00030EE8 File Offset: 0x0002F0E8
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return NewLaunchQueryParameters_t.OnGetSize();
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00030EEF File Offset: 0x0002F0EF
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return NewLaunchQueryParameters_t.StructSize();
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00030EF6 File Offset: 0x0002F0F6
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			NewLaunchQueryParameters_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00030F08 File Offset: 0x0002F108
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			NewLaunchQueryParameters_t data = NewLaunchQueryParameters_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<NewLaunchQueryParameters_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<NewLaunchQueryParameters_t>(data);
			}
		}

		// Token: 0x04000777 RID: 1911
		internal const int CallbackId = 1014;

		// Token: 0x0200024A RID: 586
		internal struct PackSmall
		{
			// Token: 0x06001D86 RID: 7558 RVA: 0x00063DE0 File Offset: 0x00061FE0
			public static implicit operator NewLaunchQueryParameters_t(NewLaunchQueryParameters_t.PackSmall d)
			{
				return default(NewLaunchQueryParameters_t);
			}
		}
	}
}
