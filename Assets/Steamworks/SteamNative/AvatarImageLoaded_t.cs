using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x02000080 RID: 128
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct AvatarImageLoaded_t
	{
		// Token: 0x0600039E RID: 926 RVA: 0x0000B698 File Offset: 0x00009898
		internal static AvatarImageLoaded_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (AvatarImageLoaded_t.PackSmall)Marshal.PtrToStructure(p, typeof(AvatarImageLoaded_t.PackSmall));
			}
			return (AvatarImageLoaded_t)Marshal.PtrToStructure(p, typeof(AvatarImageLoaded_t));
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000B6D1 File Offset: 0x000098D1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(AvatarImageLoaded_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(AvatarImageLoaded_t));
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000B6FC File Offset: 0x000098FC
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
						ResultA = new Callback.VTableWinThis.ResultD(AvatarImageLoaded_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(AvatarImageLoaded_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(AvatarImageLoaded_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(AvatarImageLoaded_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(AvatarImageLoaded_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(AvatarImageLoaded_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(AvatarImageLoaded_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(AvatarImageLoaded_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(AvatarImageLoaded_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(AvatarImageLoaded_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(AvatarImageLoaded_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(AvatarImageLoaded_t.OnGetSize)
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
				CallbackId = 334
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 334);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000BA02 File Offset: 0x00009C02
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			AvatarImageLoaded_t.OnResult(param);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000BA0A File Offset: 0x00009C0A
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			AvatarImageLoaded_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000BA14 File Offset: 0x00009C14
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return AvatarImageLoaded_t.OnGetSize();
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000BA1B File Offset: 0x00009C1B
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return AvatarImageLoaded_t.StructSize();
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000BA22 File Offset: 0x00009C22
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			AvatarImageLoaded_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000BA34 File Offset: 0x00009C34
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			AvatarImageLoaded_t data = AvatarImageLoaded_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<AvatarImageLoaded_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<AvatarImageLoaded_t>(data);
			}
		}

		// Token: 0x040004C2 RID: 1218
		internal const int CallbackId = 334;

		// Token: 0x040004C3 RID: 1219
		internal ulong SteamID;

		// Token: 0x040004C4 RID: 1220
		internal int Image;

		// Token: 0x040004C5 RID: 1221
		internal int Wide;

		// Token: 0x040004C6 RID: 1222
		internal int Tall;

		// Token: 0x020001A4 RID: 420
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CE0 RID: 7392 RVA: 0x0006134C File Offset: 0x0005F54C
			public static implicit operator AvatarImageLoaded_t(AvatarImageLoaded_t.PackSmall d)
			{
				return new AvatarImageLoaded_t
				{
					SteamID = d.SteamID,
					Image = d.Image,
					Wide = d.Wide,
					Tall = d.Tall
				};
			}

			// Token: 0x04000963 RID: 2403
			internal ulong SteamID;

			// Token: 0x04000964 RID: 2404
			internal int Image;

			// Token: 0x04000965 RID: 2405
			internal int Wide;

			// Token: 0x04000966 RID: 2406
			internal int Tall;
		}
	}
}
