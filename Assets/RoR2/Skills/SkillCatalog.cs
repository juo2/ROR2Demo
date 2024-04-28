using System;
using System.Collections.Generic;
using HG;
using RoR2.ContentManagement;
using RoR2.Modding;

namespace RoR2.Skills
{
	// Token: 0x02000C16 RID: 3094
	public static class SkillCatalog
	{
		// Token: 0x060045F0 RID: 17904 RVA: 0x00122268 File Offset: 0x00120468
		public static SkillDef GetSkillDef(int skillDefIndex)
		{
			return ArrayUtils.GetSafe<SkillDef>(SkillCatalog._allSkillDefs, skillDefIndex);
		}

		// Token: 0x060045F1 RID: 17905 RVA: 0x00122275 File Offset: 0x00120475
		public static string GetSkillName(int skillDefIndex)
		{
			return ArrayUtils.GetSafe<string>(SkillCatalog._allSkillNames, skillDefIndex);
		}

		// Token: 0x060045F2 RID: 17906 RVA: 0x00122282 File Offset: 0x00120482
		public static SkillFamily GetSkillFamily(int skillFamilyIndex)
		{
			return ArrayUtils.GetSafe<SkillFamily>(SkillCatalog._allSkillFamilies, skillFamilyIndex);
		}

		// Token: 0x060045F3 RID: 17907 RVA: 0x0012228F File Offset: 0x0012048F
		public static string GetSkillFamilyName(int skillFamilyIndex)
		{
			return ArrayUtils.GetSafe<string>(SkillCatalog._allSkillFamilyNames, skillFamilyIndex);
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060045F4 RID: 17908 RVA: 0x0012229C File Offset: 0x0012049C
		public static IEnumerable<SkillDef> allSkillDefs
		{
			get
			{
				return SkillCatalog._allSkillDefs;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060045F5 RID: 17909 RVA: 0x001222A3 File Offset: 0x001204A3
		public static IEnumerable<SkillFamily> allSkillFamilies
		{
			get
			{
				return SkillCatalog._allSkillFamilies;
			}
		}

		// Token: 0x060045F6 RID: 17910 RVA: 0x001222AC File Offset: 0x001204AC
		public static int FindSkillIndexByName(string skillName)
		{
			for (int i = 0; i < SkillCatalog._allSkillDefs.Length; i++)
			{
				if (SkillCatalog._allSkillDefs[i].skillName == skillName)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060045F7 RID: 17911 RVA: 0x001222E2 File Offset: 0x001204E2
		[SystemInitializer(new Type[]
		{
			typeof(BodyCatalog)
		})]
		private static void Init()
		{
			SkillCatalog.SetSkillDefs(ContentManager.skillDefs);
			SkillCatalog.SetSkillFamilies(ContentManager.skillFamilies);
			SkillCatalog.skillsDefined.MakeAvailable();
		}

		// Token: 0x060045F8 RID: 17912 RVA: 0x00122304 File Offset: 0x00120504
		private static void SetSkillDefs(SkillDef[] newSkillDefs)
		{
			SkillDef[] allSkillDefs = SkillCatalog._allSkillDefs;
			for (int i = 0; i < allSkillDefs.Length; i++)
			{
				allSkillDefs[i].skillIndex = -1;
			}
			ArrayUtils.CloneTo<SkillDef>(newSkillDefs, ref SkillCatalog._allSkillDefs);
			Array.Resize<string>(ref SkillCatalog._allSkillNames, SkillCatalog._allSkillDefs.Length);
			for (int j = 0; j < SkillCatalog._allSkillDefs.Length; j++)
			{
				SkillCatalog._allSkillDefs[j].skillIndex = j;
				SkillCatalog._allSkillNames[j] = SkillCatalog._allSkillDefs[j].name;
			}
		}

		// Token: 0x060045F9 RID: 17913 RVA: 0x0012237C File Offset: 0x0012057C
		private static void SetSkillFamilies(SkillFamily[] newSkillFamilies)
		{
			SkillFamily[] allSkillFamilies = SkillCatalog._allSkillFamilies;
			for (int i = 0; i < allSkillFamilies.Length; i++)
			{
				allSkillFamilies[i].catalogIndex = -1;
			}
			ArrayUtils.CloneTo<SkillFamily>(newSkillFamilies, ref SkillCatalog._allSkillFamilies);
			Array.Resize<string>(ref SkillCatalog._allSkillFamilyNames, SkillCatalog._allSkillDefs.Length);
			for (int j = 0; j < SkillCatalog._allSkillFamilies.Length; j++)
			{
				SkillCatalog._allSkillFamilies[j].catalogIndex = j;
				SkillCatalog._allSkillFamilyNames[j] = SkillCatalog._allSkillFamilies[j].name;
			}
		}

		// Token: 0x140000DF RID: 223
		// (add) Token: 0x060045FA RID: 17914 RVA: 0x001223F4 File Offset: 0x001205F4
		// (remove) Token: 0x060045FB RID: 17915 RVA: 0x000026ED File Offset: 0x000008ED
		[Obsolete("Use IContentPackProvider instead.")]
		public static event Action<List<SkillDef>> getAdditionalSkillDefs
		{
			add
			{
				LegacyModContentPackProvider.instance.HandleLegacyGetAdditionalEntries<SkillDef>("RoR2.Skills.SkillCatalog.getAdditionalSkillDefs", value, LegacyModContentPackProvider.instance.registrationContentPack.skillDefs);
			}
			remove
			{
			}
		}

		// Token: 0x140000E0 RID: 224
		// (add) Token: 0x060045FC RID: 17916 RVA: 0x00122415 File Offset: 0x00120615
		// (remove) Token: 0x060045FD RID: 17917 RVA: 0x000026ED File Offset: 0x000008ED
		[Obsolete("Use IContentPackProvider instead.")]
		public static event Action<List<SkillFamily>> getAdditionalSkillFamilies
		{
			add
			{
				LegacyModContentPackProvider.instance.HandleLegacyGetAdditionalEntries<SkillFamily>("RoR2.Skills.SkillCatalog.getAdditionalSkillFamilies", value, LegacyModContentPackProvider.instance.registrationContentPack.skillFamilies);
			}
			remove
			{
			}
		}

		// Token: 0x040043EC RID: 17388
		private static SkillDef[] _allSkillDefs = Array.Empty<SkillDef>();

		// Token: 0x040043ED RID: 17389
		private static string[] _allSkillNames = Array.Empty<string>();

		// Token: 0x040043EE RID: 17390
		private static SkillFamily[] _allSkillFamilies = Array.Empty<SkillFamily>();

		// Token: 0x040043EF RID: 17391
		private static string[] _allSkillFamilyNames = Array.Empty<string>();

		// Token: 0x040043F0 RID: 17392
		public static ResourceAvailability skillsDefined;
	}
}
