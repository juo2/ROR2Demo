using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x02000070 RID: 112
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CallbackMsg_t
	{
		// Token: 0x06000321 RID: 801 RVA: 0x00008358 File Offset: 0x00006558
		internal static CallbackMsg_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (CallbackMsg_t.PackSmall)Marshal.PtrToStructure(p, typeof(CallbackMsg_t.PackSmall));
			}
			return (CallbackMsg_t)Marshal.PtrToStructure(p, typeof(CallbackMsg_t));
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00008391 File Offset: 0x00006591
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(CallbackMsg_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(CallbackMsg_t));
		}

		// Token: 0x04000490 RID: 1168
		internal int SteamUser;

		// Token: 0x04000491 RID: 1169
		internal int Callback;

		// Token: 0x04000492 RID: 1170
		internal IntPtr ParamPtr;

		// Token: 0x04000493 RID: 1171
		internal int ParamCount;

		// Token: 0x02000194 RID: 404
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001CD0 RID: 7376 RVA: 0x00060FFC File Offset: 0x0005F1FC
			public static implicit operator CallbackMsg_t(CallbackMsg_t.PackSmall d)
			{
				return new CallbackMsg_t
				{
					SteamUser = d.SteamUser,
					Callback = d.Callback,
					ParamPtr = d.ParamPtr,
					ParamCount = d.ParamCount
				};
			}

			// Token: 0x0400093E RID: 2366
			internal int SteamUser;

			// Token: 0x0400093F RID: 2367
			internal int Callback;

			// Token: 0x04000940 RID: 2368
			internal IntPtr ParamPtr;

			// Token: 0x04000941 RID: 2369
			internal int ParamCount;
		}
	}
}
