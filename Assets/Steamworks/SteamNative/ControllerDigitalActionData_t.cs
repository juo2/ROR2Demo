using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x020000E0 RID: 224
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ControllerDigitalActionData_t
	{
		// Token: 0x060006F1 RID: 1777 RVA: 0x000213D1 File Offset: 0x0001F5D1
		internal static ControllerDigitalActionData_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (ControllerDigitalActionData_t.PackSmall)Marshal.PtrToStructure(p, typeof(ControllerDigitalActionData_t.PackSmall));
			}
			return (ControllerDigitalActionData_t)Marshal.PtrToStructure(p, typeof(ControllerDigitalActionData_t));
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0002140A File Offset: 0x0001F60A
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(ControllerDigitalActionData_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(ControllerDigitalActionData_t));
		}

		// Token: 0x04000652 RID: 1618
		[MarshalAs(UnmanagedType.I1)]
		internal bool BState;

		// Token: 0x04000653 RID: 1619
		[MarshalAs(UnmanagedType.I1)]
		internal bool BActive;

		// Token: 0x02000204 RID: 516
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D40 RID: 7488 RVA: 0x00062BD4 File Offset: 0x00060DD4
			public static implicit operator ControllerDigitalActionData_t(ControllerDigitalActionData_t.PackSmall d)
			{
				return new ControllerDigitalActionData_t
				{
					BState = d.BState,
					BActive = d.BActive
				};
			}

			// Token: 0x04000A9A RID: 2714
			[MarshalAs(UnmanagedType.I1)]
			internal bool BState;

			// Token: 0x04000A9B RID: 2715
			[MarshalAs(UnmanagedType.I1)]
			internal bool BActive;
		}
	}
}
