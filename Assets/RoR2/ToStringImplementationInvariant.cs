using System;

// Token: 0x02000092 RID: 146
public struct ToStringImplementationInvariant : IToStringImplementation<int>, IToStringImplementation<uint>, IToStringImplementation<long>, IToStringImplementation<ulong>, IToStringImplementation<short>, IToStringImplementation<ushort>, IToStringImplementation<float>, IToStringImplementation<double>
{
	// Token: 0x06000288 RID: 648 RVA: 0x0000AB56 File Offset: 0x00008D56
	public string DoToString(in int input)
	{
		return TextSerialization.ToStringInvariant(input);
	}

	// Token: 0x06000289 RID: 649 RVA: 0x0000AB5F File Offset: 0x00008D5F
	public string DoToString(in uint input)
	{
		return TextSerialization.ToStringInvariant(input);
	}

	// Token: 0x0600028A RID: 650 RVA: 0x0000AB68 File Offset: 0x00008D68
	public string DoToString(in long input)
	{
		return TextSerialization.ToStringInvariant(input);
	}

	// Token: 0x0600028B RID: 651 RVA: 0x0000AB71 File Offset: 0x00008D71
	public string DoToString(in ulong input)
	{
		return TextSerialization.ToStringInvariant(input);
	}

	// Token: 0x0600028C RID: 652 RVA: 0x0000AB7A File Offset: 0x00008D7A
	public string DoToString(in short input)
	{
		return TextSerialization.ToStringInvariant(input);
	}

	// Token: 0x0600028D RID: 653 RVA: 0x0000AB83 File Offset: 0x00008D83
	public string DoToString(in ushort input)
	{
		return TextSerialization.ToStringInvariant(input);
	}

	// Token: 0x0600028E RID: 654 RVA: 0x0000AB8C File Offset: 0x00008D8C
	public string DoToString(in float input)
	{
		return TextSerialization.ToStringInvariant(input);
	}

	// Token: 0x0600028F RID: 655 RVA: 0x0000AB95 File Offset: 0x00008D95
	public string DoToString(in double input)
	{
		return TextSerialization.ToStringInvariant(input);
	}

	// Token: 0x06000290 RID: 656 RVA: 0x0000AB9E File Offset: 0x00008D9E
	public string DoToString(in decimal input)
	{
		return TextSerialization.ToStringInvariant(input);
	}

	// Token: 0x06000291 RID: 657 RVA: 0x0000ABAB File Offset: 0x00008DAB
	string IToStringImplementation<int>.DoToString(in int input)
	{
		return this.DoToString(input);
	}

	// Token: 0x06000292 RID: 658 RVA: 0x0000ABB4 File Offset: 0x00008DB4
	string IToStringImplementation<uint>.DoToString(in uint input)
	{
		return this.DoToString(input);
	}

	// Token: 0x06000293 RID: 659 RVA: 0x0000ABBD File Offset: 0x00008DBD
	string IToStringImplementation<long>.DoToString(in long input)
	{
		return this.DoToString(input);
	}

	// Token: 0x06000294 RID: 660 RVA: 0x0000ABC6 File Offset: 0x00008DC6
	string IToStringImplementation<ulong>.DoToString(in ulong input)
	{
		return this.DoToString(input);
	}

	// Token: 0x06000295 RID: 661 RVA: 0x0000ABCF File Offset: 0x00008DCF
	string IToStringImplementation<short>.DoToString(in short input)
	{
		return this.DoToString(input);
	}

	// Token: 0x06000296 RID: 662 RVA: 0x0000ABD8 File Offset: 0x00008DD8
	string IToStringImplementation<ushort>.DoToString(in ushort input)
	{
		return this.DoToString(input);
	}

	// Token: 0x06000297 RID: 663 RVA: 0x0000ABE1 File Offset: 0x00008DE1
	string IToStringImplementation<float>.DoToString(in float input)
	{
		return this.DoToString(input);
	}

	// Token: 0x06000298 RID: 664 RVA: 0x0000ABEA File Offset: 0x00008DEA
	string IToStringImplementation<double>.DoToString(in double input)
	{
		return this.DoToString(input);
	}
}
