using System;
using HG;
using RoR2.ContentManagement;

namespace RoR2.ExpansionManagement
{
	// Token: 0x02000C7D RID: 3197
	public static class ExpansionCatalog
	{
		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x0600493C RID: 18748 RVA: 0x0012DC5C File Offset: 0x0012BE5C
		public static ReadOnlyArray<ExpansionDef> expansionDefs
		{
			get
			{
				return ExpansionCatalog._expansionDefs;
			}
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x0012DC68 File Offset: 0x0012BE68
		private static void SetExpansions(ExpansionDef[] newExpansionsDefs)
		{
			ArrayUtils.CloneTo<ExpansionDef>(newExpansionsDefs, ref ExpansionCatalog._expansionDefs);
			for (int i = 0; i < ExpansionCatalog._expansionDefs.Length; i++)
			{
				ExpansionCatalog._expansionDefs[i].expansionIndex = (ExpansionIndex)i;
			}
		}

		// Token: 0x0600493E RID: 18750 RVA: 0x0012DC9F File Offset: 0x0012BE9F
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			ExpansionCatalog.SetExpansions(ContentManager.expansionDefs);
		}

		// Token: 0x0400460A RID: 17930
		private static ExpansionDef[] _expansionDefs = Array.Empty<ExpansionDef>();
	}
}
