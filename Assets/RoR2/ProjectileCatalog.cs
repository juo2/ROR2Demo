using System;
using System.Collections.Generic;
using HG;
using RoR2.ContentManagement;
using RoR2.Modding;
using RoR2.Projectile;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020009F9 RID: 2553
	public static class ProjectileCatalog
	{
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06003B0E RID: 15118 RVA: 0x000F497F File Offset: 0x000F2B7F
		public static int projectilePrefabCount
		{
			get
			{
				return ProjectileCatalog.projectilePrefabs.Length;
			}
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x000F4988 File Offset: 0x000F2B88
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			ProjectileCatalog.SetProjectilePrefabs(ContentManager.projectilePrefabs);
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x000F4994 File Offset: 0x000F2B94
		private static void SetProjectilePrefabs(GameObject[] newProjectilePrefabs)
		{
			ProjectileController[] array = ProjectileCatalog.projectilePrefabProjectileControllerComponents;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].catalogIndex = -1;
			}
			ArrayUtils.CloneTo<GameObject>(newProjectilePrefabs, ref ProjectileCatalog.projectilePrefabs);
			Array.Sort<GameObject>(ProjectileCatalog.projectilePrefabs, (GameObject a, GameObject b) => string.CompareOrdinal(a.name, b.name));
			int num = 256;
			if (ProjectileCatalog.projectilePrefabs.Length > num)
			{
				Debug.LogErrorFormat("Cannot have more than {0} projectile prefabs defined, which is over the limit for {1}. Check comments at error source for details.", new object[]
				{
					num,
					typeof(byte).Name
				});
				for (int j = num; j < ProjectileCatalog.projectilePrefabs.Length; j++)
				{
					Debug.LogErrorFormat("Could not register projectile [{0}/{1}]=\"{2}\"", new object[]
					{
						j,
						num - 1,
						ProjectileCatalog.projectilePrefabs[j].name
					});
				}
			}
			Array.Resize<ProjectileController>(ref ProjectileCatalog.projectilePrefabProjectileControllerComponents, ProjectileCatalog.projectilePrefabs.Length);
			Array.Resize<string>(ref ProjectileCatalog.projectileNames, ProjectileCatalog.projectilePrefabs.Length);
			for (int k = 0; k < ProjectileCatalog.projectilePrefabs.Length; k++)
			{
				GameObject gameObject = ProjectileCatalog.projectilePrefabs[k];
				ProjectileController component = gameObject.GetComponent<ProjectileController>();
				component.catalogIndex = k;
				ProjectileCatalog.projectilePrefabProjectileControllerComponents[k] = component;
				ProjectileCatalog.projectileNames[k] = gameObject.name;
			}
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x000F4ADF File Offset: 0x000F2CDF
		public static int GetProjectileIndex(GameObject projectileObject)
		{
			if (projectileObject)
			{
				return ProjectileCatalog.GetProjectileIndex(projectileObject.GetComponent<ProjectileController>());
			}
			return -1;
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x000F4AF6 File Offset: 0x000F2CF6
		public static int GetProjectileIndex(ProjectileController projectileController)
		{
			if (!projectileController)
			{
				return -1;
			}
			return projectileController.catalogIndex;
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x000F4B08 File Offset: 0x000F2D08
		public static GameObject GetProjectilePrefab(int projectileIndex)
		{
			return ArrayUtils.GetSafe<GameObject>(ProjectileCatalog.projectilePrefabs, projectileIndex);
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x000F4B15 File Offset: 0x000F2D15
		public static ProjectileController GetProjectilePrefabProjectileControllerComponent(int projectileIndex)
		{
			return ArrayUtils.GetSafe<ProjectileController>(ProjectileCatalog.projectilePrefabProjectileControllerComponents, projectileIndex);
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x000F4B22 File Offset: 0x000F2D22
		public static int FindProjectileIndex(string projectileName)
		{
			return Array.IndexOf<string>(ProjectileCatalog.projectileNames, projectileName);
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x000F4B30 File Offset: 0x000F2D30
		[ConCommand(commandName = "dump_projectile_map", flags = ConVarFlags.None, helpText = "Dumps the map between indices and projectile prefabs.")]
		private static void DumpProjectileMap(ConCommandArgs args)
		{
			string[] array = new string[ProjectileCatalog.projectilePrefabs.Length];
			for (int i = 0; i < ProjectileCatalog.projectilePrefabs.Length; i++)
			{
				array[i] = string.Format("[{0}] = {1}", i, ProjectileCatalog.projectilePrefabs[i].name);
			}
			Debug.Log(string.Join("\n", array));
		}

		// Token: 0x140000D0 RID: 208
		// (add) Token: 0x06003B17 RID: 15127 RVA: 0x000F4B8B File Offset: 0x000F2D8B
		// (remove) Token: 0x06003B18 RID: 15128 RVA: 0x000026ED File Offset: 0x000008ED
		[Obsolete("Use IContentPackProvider instead.")]
		public static event Action<List<GameObject>> getAdditionalEntries
		{
			add
			{
				LegacyModContentPackProvider.instance.HandleLegacyGetAdditionalEntries<GameObject>("RoR2.ProjectileCatalog.getAdditionalEntries", value, LegacyModContentPackProvider.instance.registrationContentPack.projectilePrefabs);
			}
			remove
			{
			}
		}

		// Token: 0x040039CE RID: 14798
		private static GameObject[] projectilePrefabs = Array.Empty<GameObject>();

		// Token: 0x040039CF RID: 14799
		private static ProjectileController[] projectilePrefabProjectileControllerComponents = Array.Empty<ProjectileController>();

		// Token: 0x040039D0 RID: 14800
		private static string[] projectileNames = Array.Empty<string>();
	}
}
