using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace HG
{
	// Token: 0x02000017 RID: 23
	public static class StringBuilderPool
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x000046D1 File Offset: 0x000028D1
		[NotNull]
		public static StringBuilder RentStringBuilder()
		{
			if (StringBuilderPool.pool.Count == 0)
			{
				return new StringBuilder();
			}
			return StringBuilderPool.pool.Pop();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000046EF File Offset: 0x000028EF
		[CanBeNull]
		public static StringBuilder ReturnStringBuilder([NotNull] StringBuilder stringBuilder)
		{
			stringBuilder.Clear();
			StringBuilderPool.pool.Push(stringBuilder);
			return null;
		}

		// Token: 0x04000027 RID: 39
		private static readonly Stack<StringBuilder> pool = new Stack<StringBuilder>();
	}
}
