using System;
using System.Collections.Generic;
using HG;
using RoR2.ContentManagement;
using RoR2.Modding;

namespace RoR2
{
	// Token: 0x0200059F RID: 1439
	public static class EliteCatalog
	{
		// Token: 0x060019FA RID: 6650 RVA: 0x0007074D File Offset: 0x0006E94D
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			EliteCatalog.SetEliteDefs(ContentManager.eliteDefs);
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x0007075C File Offset: 0x0006E95C
		private static void SetEliteDefs(EliteDef[] newEliteDefs)
		{
			EliteCatalog.eliteDefs = ArrayUtils.Clone<EliteDef>(newEliteDefs);
			for (EliteIndex eliteIndex = (EliteIndex)0; eliteIndex < (EliteIndex)EliteCatalog.eliteDefs.Length; eliteIndex++)
			{
				EliteCatalog.RegisterElite(eliteIndex, EliteCatalog.eliteDefs[(int)eliteIndex]);
			}
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00070793 File Offset: 0x0006E993
		private static void RegisterElite(EliteIndex eliteIndex, EliteDef eliteDef)
		{
			eliteDef.eliteIndex = eliteIndex;
			EliteCatalog.eliteList.Add(eliteIndex);
			EliteCatalog.eliteDefs[(int)eliteIndex] = eliteDef;
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x000707AF File Offset: 0x0006E9AF
		public static EliteDef GetEliteDef(EliteIndex eliteIndex)
		{
			return ArrayUtils.GetSafe<EliteDef>(EliteCatalog.eliteDefs, (int)eliteIndex);
		}

		// Token: 0x04002049 RID: 8265
		public static List<EliteIndex> eliteList = new List<EliteIndex>();

		// Token: 0x0400204A RID: 8266
		private static EliteDef[] eliteDefs;

		// Token: 0x0400204B RID: 8267
		[Obsolete("Use IContentPackProvider instead.")]
		public static readonly CatalogModHelperProxy<EliteDef> modHelper = new CatalogModHelperProxy<EliteDef>("RoR2.EliteCatalog.modHelper", LegacyModContentPackProvider.instance.registrationContentPack.eliteDefs);
	}
}
