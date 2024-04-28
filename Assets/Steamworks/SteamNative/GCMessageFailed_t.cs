using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000128 RID: 296
	internal struct GCMessageFailed_t
	{
		// Token: 0x0600095F RID: 2399 RVA: 0x0003131C File Offset: 0x0002F51C
		internal static GCMessageFailed_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (GCMessageFailed_t.PackSmall)Marshal.PtrToStructure(p, typeof(GCMessageFailed_t.PackSmall));
			}
			return (GCMessageFailed_t)Marshal.PtrToStructure(p, typeof(GCMessageFailed_t));
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00031355 File Offset: 0x0002F555
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(GCMessageFailed_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(GCMessageFailed_t));
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00031380 File Offset: 0x0002F580
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
						ResultA = new Callback.VTableWinThis.ResultD(GCMessageFailed_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(GCMessageFailed_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(GCMessageFailed_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(GCMessageFailed_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(GCMessageFailed_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(GCMessageFailed_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(GCMessageFailed_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(GCMessageFailed_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(GCMessageFailed_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(GCMessageFailed_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(GCMessageFailed_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(GCMessageFailed_t.OnGetSize)
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
				CallbackId = 1702
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 1702);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00031686 File Offset: 0x0002F886
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			GCMessageFailed_t.OnResult(param);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0003168E File Offset: 0x0002F88E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			GCMessageFailed_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00031698 File Offset: 0x0002F898
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return GCMessageFailed_t.OnGetSize();
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0003169F File Offset: 0x0002F89F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return GCMessageFailed_t.StructSize();
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x000316A6 File Offset: 0x0002F8A6
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			GCMessageFailed_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x000316B8 File Offset: 0x0002F8B8
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			GCMessageFailed_t data = GCMessageFailed_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<GCMessageFailed_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<GCMessageFailed_t>(data);
			}
		}

		// Token: 0x0400077A RID: 1914
		internal const int CallbackId = 1702;

		// Token: 0x0200024C RID: 588
		internal struct PackSmall
		{
			// Token: 0x06001D88 RID: 7560 RVA: 0x00063E1C File Offset: 0x0006201C
			public static implicit operator GCMessageFailed_t(GCMessageFailed_t.PackSmall d)
			{
				return default(GCMessageFailed_t);
			}
		}
	}
}
