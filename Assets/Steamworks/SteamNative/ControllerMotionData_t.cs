using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x020000E1 RID: 225
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ControllerMotionData_t
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x00021432 File Offset: 0x0001F632
		internal static ControllerMotionData_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (ControllerMotionData_t.PackSmall)Marshal.PtrToStructure(p, typeof(ControllerMotionData_t.PackSmall));
			}
			return (ControllerMotionData_t)Marshal.PtrToStructure(p, typeof(ControllerMotionData_t));
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0002146B File Offset: 0x0001F66B
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(ControllerMotionData_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(ControllerMotionData_t));
		}

		// Token: 0x04000654 RID: 1620
		internal float RotQuatX;

		// Token: 0x04000655 RID: 1621
		internal float RotQuatY;

		// Token: 0x04000656 RID: 1622
		internal float RotQuatZ;

		// Token: 0x04000657 RID: 1623
		internal float RotQuatW;

		// Token: 0x04000658 RID: 1624
		internal float PosAccelX;

		// Token: 0x04000659 RID: 1625
		internal float PosAccelY;

		// Token: 0x0400065A RID: 1626
		internal float PosAccelZ;

		// Token: 0x0400065B RID: 1627
		internal float RotVelX;

		// Token: 0x0400065C RID: 1628
		internal float RotVelY;

		// Token: 0x0400065D RID: 1629
		internal float RotVelZ;

		// Token: 0x02000205 RID: 517
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D41 RID: 7489 RVA: 0x00062C04 File Offset: 0x00060E04
			public static implicit operator ControllerMotionData_t(ControllerMotionData_t.PackSmall d)
			{
				return new ControllerMotionData_t
				{
					RotQuatX = d.RotQuatX,
					RotQuatY = d.RotQuatY,
					RotQuatZ = d.RotQuatZ,
					RotQuatW = d.RotQuatW,
					PosAccelX = d.PosAccelX,
					PosAccelY = d.PosAccelY,
					PosAccelZ = d.PosAccelZ,
					RotVelX = d.RotVelX,
					RotVelY = d.RotVelY,
					RotVelZ = d.RotVelZ
				};
			}

			// Token: 0x04000A9C RID: 2716
			internal float RotQuatX;

			// Token: 0x04000A9D RID: 2717
			internal float RotQuatY;

			// Token: 0x04000A9E RID: 2718
			internal float RotQuatZ;

			// Token: 0x04000A9F RID: 2719
			internal float RotQuatW;

			// Token: 0x04000AA0 RID: 2720
			internal float PosAccelX;

			// Token: 0x04000AA1 RID: 2721
			internal float PosAccelY;

			// Token: 0x04000AA2 RID: 2722
			internal float PosAccelZ;

			// Token: 0x04000AA3 RID: 2723
			internal float RotVelX;

			// Token: 0x04000AA4 RID: 2724
			internal float RotVelY;

			// Token: 0x04000AA5 RID: 2725
			internal float RotVelZ;
		}
	}
}
