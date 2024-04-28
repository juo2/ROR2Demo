using System;
using System.Runtime.CompilerServices;
using System.Text;
using HG;
using JetBrains.Annotations;

// Token: 0x0200008E RID: 142
[Obsolete("Use HG.StringBuilderPool instead.", false)]
public static class StringBuilderPool
{
	// Token: 0x0600025E RID: 606 RVA: 0x0000A8F8 File Offset: 0x00008AF8
	[NotNull]
	[Obsolete("Use HG.StringBuilderPool instead.", false)]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static StringBuilder RentStringBuilder()
	{
		return HG.StringBuilderPool.RentStringBuilder();
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0000A8FF File Offset: 0x00008AFF
	[Obsolete("Use HG.StringBuilderPool instead.", false)]
	[CanBeNull]
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static StringBuilder ReturnStringBuilder([NotNull] StringBuilder stringBuilder)
	{
		return HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
	}
}
