using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x020000DF RID: 223
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ControllerAnalogActionData_t
	{
		// Token: 0x060006EF RID: 1775 RVA: 0x00021370 File Offset: 0x0001F570
		internal static ControllerAnalogActionData_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (ControllerAnalogActionData_t.PackSmall)Marshal.PtrToStructure(p, typeof(ControllerAnalogActionData_t.PackSmall));
			}
			return (ControllerAnalogActionData_t)Marshal.PtrToStructure(p, typeof(ControllerAnalogActionData_t));
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x000213A9 File Offset: 0x0001F5A9
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(ControllerAnalogActionData_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(ControllerAnalogActionData_t));
		}

		// Token: 0x0400064E RID: 1614
		internal ControllerSourceMode EMode;

		// Token: 0x0400064F RID: 1615
		internal float X;

		// Token: 0x04000650 RID: 1616
		internal float Y;

		// Token: 0x04000651 RID: 1617
		[MarshalAs(UnmanagedType.I1)]
		internal bool BActive;

		// Token: 0x02000203 RID: 515
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D3F RID: 7487 RVA: 0x00062B88 File Offset: 0x00060D88
			public static implicit operator ControllerAnalogActionData_t(ControllerAnalogActionData_t.PackSmall d)
			{
				return new ControllerAnalogActionData_t
				{
					EMode = d.EMode,
					X = d.X,
					Y = d.Y,
					BActive = d.BActive
				};
			}

			// Token: 0x04000A96 RID: 2710
			internal ControllerSourceMode EMode;

			// Token: 0x04000A97 RID: 2711
			internal float X;

			// Token: 0x04000A98 RID: 2712
			internal float Y;

			// Token: 0x04000A99 RID: 2713
			[MarshalAs(UnmanagedType.I1)]
			internal bool BActive;
		}
	}
}
