using System;

// Token: 0x02000077 RID: 119
public struct MemoizedToString<TInput, TToString> where TInput : IEquatable<TInput> where TToString : struct, IToStringImplementation<!0>
{
	// Token: 0x06000212 RID: 530 RVA: 0x000097FC File Offset: 0x000079FC
	public string GetString(in TInput input)
	{
		TInput tinput = input;
		if (!tinput.Equals(this.lastInput))
		{
			this.lastInput = input;
			this.lastOutput = this.implementation.DoToString(this.lastInput);
		}
		return this.lastOutput;
	}

	// Token: 0x06000213 RID: 531 RVA: 0x00009854 File Offset: 0x00007A54
	public static MemoizedToString<TInput, TToString> GetNew()
	{
		return MemoizedToString<TInput, TToString>.defaultValue;
	}

	// Token: 0x06000214 RID: 532 RVA: 0x0000985C File Offset: 0x00007A5C
	static MemoizedToString()
	{
		TInput tinput = default(TInput);
		TToString ttoString = default(TToString);
		MemoizedToString<TInput, TToString>.defaultValue = new MemoizedToString<TInput, TToString>
		{
			lastInput = tinput,
			lastOutput = ttoString.DoToString(tinput),
			implementation = ttoString
		};
	}

	// Token: 0x040001E6 RID: 486
	private TInput lastInput;

	// Token: 0x040001E7 RID: 487
	private string lastOutput;

	// Token: 0x040001E8 RID: 488
	private TToString implementation;

	// Token: 0x040001E9 RID: 489
	private static readonly MemoizedToString<TInput, TToString> defaultValue;
}
