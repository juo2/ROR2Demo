using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x02000002 RID: 2
	[StructLayout(LayoutKind.Sequential)]
	internal class Callback
	{
		// Token: 0x04000001 RID: 1
		public IntPtr vTablePtr;

		// Token: 0x04000002 RID: 2
		public byte CallbackFlags;

		// Token: 0x04000003 RID: 3
		public int CallbackId;

		// Token: 0x02000187 RID: 391
		internal enum Flags : byte
		{
			// Token: 0x04000922 RID: 2338
			Registered = 1,
			// Token: 0x04000923 RID: 2339
			GameServer
		}

		// Token: 0x02000188 RID: 392
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public class VTable
		{
			// Token: 0x04000924 RID: 2340
			public Callback.VTable.ResultD ResultA;

			// Token: 0x04000925 RID: 2341
			public Callback.VTable.ResultWithInfoD ResultB;

			// Token: 0x04000926 RID: 2342
			public Callback.VTable.GetSizeD GetSize;

			// Token: 0x020002A8 RID: 680
			// (Invoke) Token: 0x06001F85 RID: 8069
			[UnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ResultD(IntPtr pvParam);

			// Token: 0x020002A9 RID: 681
			// (Invoke) Token: 0x06001F89 RID: 8073
			[UnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ResultWithInfoD(IntPtr pvParam, bool bIOFailure, SteamAPICall_t hSteamAPICall);

			// Token: 0x020002AA RID: 682
			// (Invoke) Token: 0x06001F8D RID: 8077
			[UnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate int GetSizeD();
		}

		// Token: 0x02000189 RID: 393
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public class VTableWin
		{
			// Token: 0x04000927 RID: 2343
			public Callback.VTableWin.ResultWithInfoD ResultB;

			// Token: 0x04000928 RID: 2344
			public Callback.VTableWin.ResultD ResultA;

			// Token: 0x04000929 RID: 2345
			public Callback.VTableWin.GetSizeD GetSize;

			// Token: 0x020002AB RID: 683
			// (Invoke) Token: 0x06001F91 RID: 8081
			[UnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ResultD(IntPtr pvParam);

			// Token: 0x020002AC RID: 684
			// (Invoke) Token: 0x06001F95 RID: 8085
			[UnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate void ResultWithInfoD(IntPtr pvParam, bool bIOFailure, SteamAPICall_t hSteamAPICall);

			// Token: 0x020002AD RID: 685
			// (Invoke) Token: 0x06001F99 RID: 8089
			[UnmanagedFunctionPointer(CallingConvention.StdCall)]
			public delegate int GetSizeD();
		}

		// Token: 0x0200018A RID: 394
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public class VTableThis
		{
			// Token: 0x0400092A RID: 2346
			public Callback.VTableThis.ResultD ResultA;

			// Token: 0x0400092B RID: 2347
			public Callback.VTableThis.ResultWithInfoD ResultB;

			// Token: 0x0400092C RID: 2348
			public Callback.VTableThis.GetSizeD GetSize;

			// Token: 0x020002AE RID: 686
			// (Invoke) Token: 0x06001F9D RID: 8093
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate void ResultD(IntPtr thisptr, IntPtr pvParam);

			// Token: 0x020002AF RID: 687
			// (Invoke) Token: 0x06001FA1 RID: 8097
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate void ResultWithInfoD(IntPtr thisptr, IntPtr pvParam, bool bIOFailure, SteamAPICall_t hSteamAPICall);

			// Token: 0x020002B0 RID: 688
			// (Invoke) Token: 0x06001FA5 RID: 8101
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate int GetSizeD(IntPtr thisptr);
		}

		// Token: 0x0200018B RID: 395
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public class VTableWinThis
		{
			// Token: 0x0400092D RID: 2349
			public Callback.VTableWinThis.ResultWithInfoD ResultB;

			// Token: 0x0400092E RID: 2350
			public Callback.VTableWinThis.ResultD ResultA;

			// Token: 0x0400092F RID: 2351
			public Callback.VTableWinThis.GetSizeD GetSize;

			// Token: 0x020002B1 RID: 689
			// (Invoke) Token: 0x06001FA9 RID: 8105
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate void ResultD(IntPtr thisptr, IntPtr pvParam);

			// Token: 0x020002B2 RID: 690
			// (Invoke) Token: 0x06001FAD RID: 8109
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate void ResultWithInfoD(IntPtr thisptr, IntPtr pvParam, bool bIOFailure, SteamAPICall_t hSteamAPICall);

			// Token: 0x020002B3 RID: 691
			// (Invoke) Token: 0x06001FB1 RID: 8113
			[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
			public delegate int GetSizeD(IntPtr thisptr);
		}
	}
}
