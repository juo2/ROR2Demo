using System;
using JetBrains.Annotations;
using RoR2.ContentManagement;

namespace RoR2.Modding
{
	// Token: 0x02000B61 RID: 2913
	public class RoR2Mod
	{
		// Token: 0x0600423B RID: 16955 RVA: 0x001126F7 File Offset: 0x001108F7
		public RoR2Mod([NotNull] Mod mod)
		{
			this.mod = mod;
		}

		// Token: 0x04004051 RID: 16465
		[NotNull]
		public readonly Mod mod;

		// Token: 0x04004052 RID: 16466
		public IContentPackProvider contentPackProvider;
	}
}
