using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x0200097D RID: 2429
	[Serializable]
	public struct PackedUnitVector3
	{
		// Token: 0x0600371C RID: 14108 RVA: 0x000E89C4 File Offset: 0x000E6BC4
		static PackedUnitVector3()
		{
			for (int i = 0; i < PackedUnitVector3.uvAdjustment.Length; i++)
			{
				int num = i >> 7;
				int num2 = i & 127;
				if (num + num2 >= 127)
				{
					num = 127 - num;
					num2 = 127 - num2;
				}
				Vector3 vector = new Vector3((float)num, (float)num2, (float)(126 - num - num2));
				PackedUnitVector3.uvAdjustment[i] = 1f / vector.magnitude;
			}
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x000E8A32 File Offset: 0x000E6C32
		public PackedUnitVector3(ushort value)
		{
			this.value = value;
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x000E8A3C File Offset: 0x000E6C3C
		public PackedUnitVector3(Vector3 src)
		{
			this.value = 0;
			if (src.x < 0f)
			{
				this.value |= 32768;
				src.x = -src.x;
			}
			if (src.y < 0f)
			{
				this.value |= 16384;
				src.y = -src.y;
			}
			if (src.z < 0f)
			{
				this.value |= 8192;
				src.z = -src.z;
			}
			float num = 126f / (src.x + src.y + src.z);
			int num2 = (int)(src.x * num);
			int num3 = (int)(src.y * num);
			if (num2 >= 64)
			{
				num2 = 127 - num2;
				num3 = 127 - num3;
			}
			this.value |= (ushort)(num2 << 7);
			this.value |= (ushort)num3;
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x000E8B3C File Offset: 0x000E6D3C
		public Vector3 Unpack()
		{
			int num = (this.value & 8064) >> 7;
			int num2 = (int)(this.value & 127);
			if (num + num2 >= 127)
			{
				num = 127 - num;
				num2 = 127 - num2;
			}
			float num3 = PackedUnitVector3.uvAdjustment[(int)(this.value & 8191)];
			Vector3 vector = new Vector3(num3 * (float)num, num3 * (float)num2, num3 * (float)(126 - num - num2));
			if ((this.value & 32768) != 0)
			{
				vector.x = -vector.x;
			}
			if ((this.value & 16384) != 0)
			{
				vector.y = -vector.y;
			}
			if ((this.value & 8192) != 0)
			{
				vector.z = -vector.z;
			}
			return vector;
		}

		// Token: 0x04003781 RID: 14209
		[SerializeField]
		public ushort value;

		// Token: 0x04003782 RID: 14210
		private static readonly float[] uvAdjustment = new float[8192];

		// Token: 0x04003783 RID: 14211
		private const ushort signMask = 57344;

		// Token: 0x04003784 RID: 14212
		private const ushort invSignMask = 8191;

		// Token: 0x04003785 RID: 14213
		private const ushort xSignMask = 32768;

		// Token: 0x04003786 RID: 14214
		private const ushort ySignMask = 16384;

		// Token: 0x04003787 RID: 14215
		private const ushort zSignMask = 8192;

		// Token: 0x04003788 RID: 14216
		private const ushort topMask = 8064;

		// Token: 0x04003789 RID: 14217
		private const ushort bottomMask = 127;
	}
}
