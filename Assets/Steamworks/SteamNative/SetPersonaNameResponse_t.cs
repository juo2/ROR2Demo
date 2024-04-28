using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200008D RID: 141
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetPersonaNameResponse_t
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0000E930 File Offset: 0x0000CB30
		internal static SetPersonaNameResponse_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SetPersonaNameResponse_t.PackSmall)Marshal.PtrToStructure(p, typeof(SetPersonaNameResponse_t.PackSmall));
			}
			return (SetPersonaNameResponse_t)Marshal.PtrToStructure(p, typeof(SetPersonaNameResponse_t));
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000E969 File Offset: 0x0000CB69
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SetPersonaNameResponse_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SetPersonaNameResponse_t));
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000E991 File Offset: 0x0000CB91
		internal static CallResult<SetPersonaNameResponse_t> CallResult(BaseSteamworks steamworks, SteamAPICall_t call, Action<SetPersonaNameResponse_t, bool> CallbackFunction)
		{
			return new CallResult<SetPersonaNameResponse_t>(steamworks, call, CallbackFunction, new CallResult<SetPersonaNameResponse_t>.ConvertFromPointer(SetPersonaNameResponse_t.FromPointer), SetPersonaNameResponse_t.StructSize(), 347);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
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
						ResultA = new Callback.VTableWinThis.ResultD(SetPersonaNameResponse_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(SetPersonaNameResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(SetPersonaNameResponse_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(SetPersonaNameResponse_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(SetPersonaNameResponse_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(SetPersonaNameResponse_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(SetPersonaNameResponse_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(SetPersonaNameResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(SetPersonaNameResponse_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(SetPersonaNameResponse_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(SetPersonaNameResponse_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(SetPersonaNameResponse_t.OnGetSize)
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
				CallbackId = 347
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 347);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000ECBA File Offset: 0x0000CEBA
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			SetPersonaNameResponse_t.OnResult(param);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000ECC2 File Offset: 0x0000CEC2
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			SetPersonaNameResponse_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000ECCC File Offset: 0x0000CECC
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return SetPersonaNameResponse_t.OnGetSize();
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000ECD3 File Offset: 0x0000CED3
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return SetPersonaNameResponse_t.StructSize();
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000ECDA File Offset: 0x0000CEDA
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			SetPersonaNameResponse_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			SetPersonaNameResponse_t data = SetPersonaNameResponse_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<SetPersonaNameResponse_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<SetPersonaNameResponse_t>(data);
			}
		}

		// Token: 0x040004F2 RID: 1266
		internal const int CallbackId = 347;

		// Token: 0x040004F3 RID: 1267
		[MarshalAs(UnmanagedType.I1)]
		internal bool Success;

		// Token: 0x040004F4 RID: 1268
		[MarshalAs(UnmanagedType.I1)]
		internal bool LocalSuccess;

		// Token: 0x040004F5 RID: 1269
		internal Result Result;

		// Token: 0x020001B1 RID: 433
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CED RID: 7405 RVA: 0x00061644 File Offset: 0x0005F844
			public static implicit operator SetPersonaNameResponse_t(SetPersonaNameResponse_t.PackSmall d)
			{
				return new SetPersonaNameResponse_t
				{
					Success = d.Success,
					LocalSuccess = d.LocalSuccess,
					Result = d.Result
				};
			}

			// Token: 0x04000986 RID: 2438
			[MarshalAs(UnmanagedType.I1)]
			internal bool Success;

			// Token: 0x04000987 RID: 2439
			[MarshalAs(UnmanagedType.I1)]
			internal bool LocalSuccess;

			// Token: 0x04000988 RID: 2440
			internal Result Result;
		}
	}
}
