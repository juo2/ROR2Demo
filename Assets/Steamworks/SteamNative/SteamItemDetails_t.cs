using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x0200010C RID: 268
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamItemDetails_t
	{
		// Token: 0x06000862 RID: 2146 RVA: 0x0002AA04 File Offset: 0x00028C04
		internal static SteamItemDetails_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (SteamItemDetails_t.PackSmall)Marshal.PtrToStructure(p, typeof(SteamItemDetails_t.PackSmall));
			}
			return (SteamItemDetails_t)Marshal.PtrToStructure(p, typeof(SteamItemDetails_t));
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0002AA3D File Offset: 0x00028C3D
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(SteamItemDetails_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(SteamItemDetails_t));
		}

		// Token: 0x04000721 RID: 1825
		internal ulong ItemId;

		// Token: 0x04000722 RID: 1826
		internal int Definition;

		// Token: 0x04000723 RID: 1827
		internal ushort Quantity;

		// Token: 0x04000724 RID: 1828
		internal ushort Flags;

		// Token: 0x02000230 RID: 560
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D6C RID: 7532 RVA: 0x00063868 File Offset: 0x00061A68
			public static implicit operator SteamItemDetails_t(SteamItemDetails_t.PackSmall d)
			{
				return new SteamItemDetails_t
				{
					ItemId = d.ItemId,
					Definition = d.Definition,
					Quantity = d.Quantity,
					Flags = d.Flags
				};
			}

			// Token: 0x04000B43 RID: 2883
			internal ulong ItemId;

			// Token: 0x04000B44 RID: 2884
			internal int Definition;

			// Token: 0x04000B45 RID: 2885
			internal ushort Quantity;

			// Token: 0x04000B46 RID: 2886
			internal ushort Flags;
		}
	}
}
