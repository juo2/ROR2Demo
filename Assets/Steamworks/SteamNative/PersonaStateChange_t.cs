using System;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;

namespace SteamNative
{
	// Token: 0x0200007C RID: 124
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PersonaStateChange_t
	{
		// Token: 0x0600037A RID: 890 RVA: 0x0000A73A File Offset: 0x0000893A
		internal static PersonaStateChange_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (PersonaStateChange_t.PackSmall)Marshal.PtrToStructure(p, typeof(PersonaStateChange_t.PackSmall));
			}
			return (PersonaStateChange_t)Marshal.PtrToStructure(p, typeof(PersonaStateChange_t));
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000A773 File Offset: 0x00008973
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(PersonaStateChange_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(PersonaStateChange_t));
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000A79C File Offset: 0x0000899C
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
						ResultA = new Callback.VTableWinThis.ResultD(PersonaStateChange_t.OnResultThis),
						ResultB = new Callback.VTableWinThis.ResultWithInfoD(PersonaStateChange_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableWinThis.GetSizeD(PersonaStateChange_t.OnGetSizeThis)
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
						ResultA = new Callback.VTableThis.ResultD(PersonaStateChange_t.OnResultThis),
						ResultB = new Callback.VTableThis.ResultWithInfoD(PersonaStateChange_t.OnResultWithInfoThis),
						GetSize = new Callback.VTableThis.GetSizeD(PersonaStateChange_t.OnGetSizeThis)
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
					ResultA = new Callback.VTableWin.ResultD(PersonaStateChange_t.OnResult),
					ResultB = new Callback.VTableWin.ResultWithInfoD(PersonaStateChange_t.OnResultWithInfo),
					GetSize = new Callback.VTableWin.GetSizeD(PersonaStateChange_t.OnGetSize)
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
					ResultA = new Callback.VTable.ResultD(PersonaStateChange_t.OnResult),
					ResultB = new Callback.VTable.ResultWithInfoD(PersonaStateChange_t.OnResultWithInfo),
					GetSize = new Callback.VTable.GetSizeD(PersonaStateChange_t.OnGetSize)
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
				CallbackId = 304
			}, GCHandleType.Pinned);
			steamworks.native.api.SteamAPI_RegisterCallback(callbackHandle.PinnedCallback.AddrOfPinnedObject(), 304);
			steamworks.RegisterCallbackHandle(callbackHandle);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000AAA2 File Offset: 0x00008CA2
		[MonoPInvokeCallback]
		internal static void OnResultThis(IntPtr self, IntPtr param)
		{
			PersonaStateChange_t.OnResult(param);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000AAAA File Offset: 0x00008CAA
		[MonoPInvokeCallback]
		internal static void OnResultWithInfoThis(IntPtr self, IntPtr param, bool failure, SteamAPICall_t call)
		{
			PersonaStateChange_t.OnResultWithInfo(param, failure, call);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000AAB4 File Offset: 0x00008CB4
		[MonoPInvokeCallback]
		internal static int OnGetSizeThis(IntPtr self)
		{
			return PersonaStateChange_t.OnGetSize();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000AABB File Offset: 0x00008CBB
		[MonoPInvokeCallback]
		internal static int OnGetSize()
		{
			return PersonaStateChange_t.StructSize();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000AAC2 File Offset: 0x00008CC2
		[MonoPInvokeCallback]
		internal static void OnResult(IntPtr param)
		{
			PersonaStateChange_t.OnResultWithInfo(param, false, 0UL);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000AAD4 File Offset: 0x00008CD4
		[MonoPInvokeCallback]
		internal static void OnResultWithInfo(IntPtr param, bool failure, SteamAPICall_t call)
		{
			if (failure)
			{
				return;
			}
			PersonaStateChange_t data = PersonaStateChange_t.FromPointer(param);
			if (Client.Instance != null)
			{
				Client.Instance.OnCallback<PersonaStateChange_t>(data);
			}
			if (Server.Instance != null)
			{
				Server.Instance.OnCallback<PersonaStateChange_t>(data);
			}
		}

		// Token: 0x040004B7 RID: 1207
		internal const int CallbackId = 304;

		// Token: 0x040004B8 RID: 1208
		internal ulong SteamID;

		// Token: 0x040004B9 RID: 1209
		internal int ChangeFlags;

		// Token: 0x020001A0 RID: 416
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CDC RID: 7388 RVA: 0x00061298 File Offset: 0x0005F498
			public static implicit operator PersonaStateChange_t(PersonaStateChange_t.PackSmall d)
			{
				return new PersonaStateChange_t
				{
					SteamID = d.SteamID,
					ChangeFlags = d.ChangeFlags
				};
			}

			// Token: 0x0400095C RID: 2396
			internal ulong SteamID;

			// Token: 0x0400095D RID: 2397
			internal int ChangeFlags;
		}
	}
}
