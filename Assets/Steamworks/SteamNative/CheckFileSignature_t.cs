using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000090 RID: 144
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CheckFileSignature_t
	{
		// Token: 0x06000434 RID: 1076 RVA: 0x0000F4D8 File Offset: 0x0000D6D8
		internal static CheckFileSignature_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (CheckFileSignature_t.PackSmall)Marshal.PtrToStructure(p, typeof(CheckFileSignature_t.PackSmall));
			}
			return (CheckFileSignature_t)Marshal.PtrToStructure(p, typeof(CheckFileSignature_t));
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000F511 File Offset: 0x0000D711
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(CheckFileSignature_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(CheckFileSignature_t));
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000F539 File Offset: 0x0000D739
		internal static CallResult<CheckFileSignature_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<CheckFileSignature_t, bool> CallbackFunction)
		{
			return new CallResult<CheckFileSignature_t>(steamworks, call, CallbackFunction, new CallResult<CheckFileSignature_t>.ConvertFromPointer(CheckFileSignature_t.FromPointer), CheckFileSignature_t.StructSize(), 705);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000F55C File Offset: 0x0000D75C
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
						ResultA = new Callback.VTableWinThis.ResultD(CheckFileSignature_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(CheckFileSignature_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(CheckFileSignature_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(CheckFileSignature_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(CheckFileSignature_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(CheckFileSignature_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(CheckFileSignature_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(CheckFileSignature_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(CheckFileSignature_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(CheckFileSignature_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(CheckFileSignature_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(CheckFileSignature_t.OnGetSize)
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
				CallbackId = 705
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 705);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000F862 File Offset: 0x0000DA62
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			CheckFileSignature_t.OnResult(param);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000F86A File Offset: 0x0000DA6A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			CheckFileSignature_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000F874 File Offset: 0x0000DA74
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return CheckFileSignature_t.OnGetSize();
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000F87B File Offset: 0x0000DA7B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return CheckFileSignature_t.StructSize();
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000F882 File Offset: 0x0000DA82
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			CheckFileSignature_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000F894 File Offset: 0x0000DA94
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			CheckFileSignature_t data = CheckFileSignature_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<CheckFileSignature_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<CheckFileSignature_t>(data);
			}
		}

		// Token: 0x040004FC RID: 1276
		internal const int CallbackId = 705;

		// Token: 0x040004FD RID: 1277
		internal CheckFileSignature CheckFileSignature;

		// Token: 0x020001B4 RID: 436
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CF0 RID: 7408 RVA: 0x000616E8 File Offset: 0x0005F8E8
			public static implicit operator CheckFileSignature_t(CheckFileSignature_t.PackSmall d)
			{
				return new CheckFileSignature_t
				{
					CheckFileSignature = d.CheckFileSignature
				};
			}

			// Token: 0x0400098D RID: 2445
			internal CheckFileSignature CheckFileSignature;
		}
	}
}
