using System;
using JetBrains.Annotations;

namespace HG
{
	// Token: 0x0200000F RID: 15
	public struct GenericMemoizer<TInput, TOutput> where TInput : IEquatable<TInput>
	{
		// Token: 0x0600006B RID: 107 RVA: 0x0000363C File Offset: 0x0000183C
		public GenericMemoizer([NotNull] GenericMemoizer<TInput, TOutput>.Func func)
		{
			this.func = func;
			this.lastInput = default(TInput);
			this.lastOutput = func(this.lastInput);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003664 File Offset: 0x00001864
		public TOutput Evaluate(in TInput input)
		{
			TInput tinput = input;
			if (!tinput.Equals(this.lastInput))
			{
				this.lastInput = input;
				this.lastOutput = this.func(input);
			}
			return this.lastOutput;
		}

		// Token: 0x04000019 RID: 25
		private readonly GenericMemoizer<TInput, TOutput>.Func func;

		// Token: 0x0400001A RID: 26
		private TInput lastInput;

		// Token: 0x0400001B RID: 27
		private TOutput lastOutput;

		// Token: 0x02000025 RID: 37
		// (Invoke) Token: 0x0600013C RID: 316
		public delegate TOutput Func(in TInput input);
	}
}
