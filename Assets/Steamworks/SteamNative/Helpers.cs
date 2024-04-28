using System;
using System.Text;

namespace SteamNative
{
	// Token: 0x02000056 RID: 86
	internal static class Helpers
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000022BC File Offset: 0x000004BC
		public static StringBuilder TakeStringBuilder()
		{
			if (Helpers.StringBuilderPool == null)
			{
				Helpers.StringBuilderPool = new StringBuilder[8];
				for (int i = 0; i < Helpers.StringBuilderPool.Length; i++)
				{
					Helpers.StringBuilderPool[i] = new StringBuilder(4096);
				}
			}
			Helpers.StringBuilderPoolIndex++;
			if (Helpers.StringBuilderPoolIndex >= Helpers.StringBuilderPool.Length)
			{
				Helpers.StringBuilderPoolIndex = 0;
			}
			Helpers.StringBuilderPool[Helpers.StringBuilderPoolIndex].Capacity = 4096;
			Helpers.StringBuilderPool[Helpers.StringBuilderPoolIndex].Length = 0;
			return Helpers.StringBuilderPool[Helpers.StringBuilderPoolIndex];
		}

		// Token: 0x0400045D RID: 1117
		private static StringBuilder[] StringBuilderPool;

		// Token: 0x0400045E RID: 1118
		private static int StringBuilderPoolIndex;
	}
}
