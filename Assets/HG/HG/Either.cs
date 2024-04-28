using System;

namespace HG
{
	// Token: 0x0200000B RID: 11
	public readonly struct Either<TA, TB>
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002BBB File Offset: 0x00000DBB
		public Either(TA a)
		{
			this.a = a;
			this.b = default(TB);
			this.isA = true;
			this.isB = false;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002BDE File Offset: 0x00000DDE
		public Either(TB b)
		{
			this.a = default(TA);
			this.b = b;
			this.isA = false;
			this.isB = false;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002C01 File Offset: 0x00000E01
		public static implicit operator Either<TA, TB>(in TA a)
		{
			return new Either<TA, TB>(a);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002C0E File Offset: 0x00000E0E
		public static implicit operator Either<TA, TB>(in TB b)
		{
			return new Either<TA, TB>(b);
		}

		// Token: 0x0400000E RID: 14
		public readonly TA a;

		// Token: 0x0400000F RID: 15
		public readonly TB b;

		// Token: 0x04000010 RID: 16
		public readonly bool isA;

		// Token: 0x04000011 RID: 17
		public readonly bool isB;
	}
}
