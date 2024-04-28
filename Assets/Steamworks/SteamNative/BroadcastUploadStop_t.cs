using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000112 RID: 274
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct BroadcastUploadStop_t
	{
		// Token: 0x06000894 RID: 2196 RVA: 0x0002BDFC File Offset: 0x00029FFC
		internal static BroadcastUploadStop_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (BroadcastUploadStop_t.PackSmall)Marshal.PtrToStructure(p, typeof(BroadcastUploadStop_t.PackSmall));
			}
			return (BroadcastUploadStop_t)Marshal.PtrToStructure(p, typeof(BroadcastUploadStop_t));
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0002BE35 File Offset: 0x0002A035
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(BroadcastUploadStop_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(BroadcastUploadStop_t));
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0002BE60 File Offset: 0x0002A060
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
						ResultA = new Callback.VTableWinThis.ResultD(BroadcastUploadStop_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(BroadcastUploadStop_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(BroadcastUploadStop_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(BroadcastUploadStop_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(BroadcastUploadStop_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(BroadcastUploadStop_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(BroadcastUploadStop_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(BroadcastUploadStop_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(BroadcastUploadStop_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(BroadcastUploadStop_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(BroadcastUploadStop_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(BroadcastUploadStop_t.OnGetSize)
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
				CallbackId = 4605
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 4605);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0002C166 File Offset: 0x0002A366
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			BroadcastUploadStop_t.OnResult(param);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0002C16E File Offset: 0x0002A36E
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			BroadcastUploadStop_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0002C178 File Offset: 0x0002A378
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return BroadcastUploadStop_t.OnGetSize();
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0002C17F File Offset: 0x0002A37F
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return BroadcastUploadStop_t.StructSize();
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0002C186 File Offset: 0x0002A386
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			BroadcastUploadStop_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0002C198 File Offset: 0x0002A398
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			BroadcastUploadStop_t data = BroadcastUploadStop_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<BroadcastUploadStop_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<BroadcastUploadStop_t>(data);
			}
		}

		// Token: 0x04000736 RID: 1846
		internal const int CallbackId = 4605;

		// Token: 0x04000737 RID: 1847
		internal BroadcastUploadResult Result;

		// Token: 0x02000236 RID: 566
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D72 RID: 7538 RVA: 0x000639C4 File Offset: 0x00061BC4
			public static implicit operator BroadcastUploadStop_t(BroadcastUploadStop_t.PackSmall d)
			{
				return new BroadcastUploadStop_t
				{
					Result = d.Result
				};
			}

			// Token: 0x04000B53 RID: 2899
			internal BroadcastUploadResult Result;
		}
	}
}
