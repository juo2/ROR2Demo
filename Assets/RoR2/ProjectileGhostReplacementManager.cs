using System;
using System.Collections.Generic;
using HG;
using RoR2.Projectile;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020009FB RID: 2555
	public static class ProjectileGhostReplacementManager
	{
		// Token: 0x06003B1D RID: 15133 RVA: 0x000F4BD8 File Offset: 0x000F2DD8
		public static GameObject FindProjectileGhostPrefab(ProjectileController projectileController)
		{
			ProjectileGhostReplacementManager.SkinGhostPair[] safe = ArrayUtils.GetSafe<ProjectileGhostReplacementManager.SkinGhostPair[]>(ProjectileGhostReplacementManager.projectileToSkinGhostPairs, projectileController.catalogIndex);
			if (safe != null && projectileController.owner)
			{
				CharacterBody component = projectileController.owner.GetComponent<CharacterBody>();
				if (component)
				{
					SkinDef bodySkinDef = SkinCatalog.GetBodySkinDef(component.bodyIndex, (int)component.skinIndex);
					if (bodySkinDef)
					{
						for (int i = 0; i < safe.Length; i++)
						{
							if (safe[i].skinDef == bodySkinDef)
							{
								return safe[i].projectileGhost;
							}
						}
					}
				}
			}
			return projectileController.ghostPrefab;
		}

		// Token: 0x06003B1E RID: 15134 RVA: 0x000F4C64 File Offset: 0x000F2E64
		[SystemInitializer(new Type[]
		{
			typeof(SkinCatalog),
			typeof(ProjectileCatalog)
		})]
		private static void Init()
		{
			ProjectileGhostReplacementManager.BuildTable();
		}

		// Token: 0x06003B1F RID: 15135 RVA: 0x000F4C6C File Offset: 0x000F2E6C
		private static void BuildTable()
		{
			List<SkinDef> list = new List<SkinDef>();
			SkinIndex skinIndex = (SkinIndex)0;
			SkinIndex skinCount = (SkinIndex)SkinCatalog.skinCount;
			while (skinIndex < skinCount)
			{
				SkinDef skinDef = SkinCatalog.GetSkinDef(skinIndex);
				if (skinDef.projectileGhostReplacements.Length != 0)
				{
					list.Add(skinDef);
				}
				skinIndex++;
			}
			ProjectileGhostReplacementManager.projectileToSkinGhostPairs = new ProjectileGhostReplacementManager.SkinGhostPair[ProjectileCatalog.projectilePrefabCount][];
			foreach (SkinDef skinDef2 in list)
			{
				SkinDef.ProjectileGhostReplacement[] projectileGhostReplacements = skinDef2.projectileGhostReplacements;
				for (int i = 0; i < skinDef2.projectileGhostReplacements.Length; i++)
				{
					SkinDef.ProjectileGhostReplacement projectileGhostReplacement = projectileGhostReplacements[i];
					int catalogIndex = projectileGhostReplacement.projectilePrefab.GetComponent<ProjectileController>().catalogIndex;
					if (ProjectileGhostReplacementManager.projectileToSkinGhostPairs[catalogIndex] == null)
					{
						ProjectileGhostReplacementManager.projectileToSkinGhostPairs[catalogIndex] = Array.Empty<ProjectileGhostReplacementManager.SkinGhostPair>();
					}
					ProjectileGhostReplacementManager.SkinGhostPair skinGhostPair = new ProjectileGhostReplacementManager.SkinGhostPair
					{
						projectileGhost = projectileGhostReplacement.projectileGhostReplacementPrefab,
						skinDef = skinDef2
					};
					ArrayUtils.ArrayAppend<ProjectileGhostReplacementManager.SkinGhostPair>(ref ProjectileGhostReplacementManager.projectileToSkinGhostPairs[catalogIndex], skinGhostPair);
				}
			}
		}

		// Token: 0x040039D3 RID: 14803
		private static ProjectileGhostReplacementManager.SkinGhostPair[][] projectileToSkinGhostPairs = Array.Empty<ProjectileGhostReplacementManager.SkinGhostPair[]>();

		// Token: 0x020009FC RID: 2556
		private struct SkinGhostPair
		{
			// Token: 0x040039D4 RID: 14804
			public SkinDef skinDef;

			// Token: 0x040039D5 RID: 14805
			public GameObject projectileGhost;
		}
	}
}
