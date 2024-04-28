using System;

// Token: 0x02000004 RID: 4
public class SplitMix64
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002058 File Offset: 0x00000258
	public ulong Next()
	{
		ulong num = this.x += 11400714819323198485UL;
		ulong num2 = (num ^ num >> 30) * 13787848793156543929UL;
		ulong num3 = (num2 ^ num2 >> 27) * 10723151780598845931UL;
		return num3 ^ num3 >> 31;
	}

	// Token: 0x04000001 RID: 1
	public ulong x;
}
