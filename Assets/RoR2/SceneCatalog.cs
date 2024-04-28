using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HG;
using JetBrains.Annotations;
using RoR2.ContentManagement;
using RoR2.Modding;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RoR2
{
	// Token: 0x02000A32 RID: 2610
	public static class SceneCatalog
	{
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06003C43 RID: 15427 RVA: 0x000F9596 File Offset: 0x000F7796
		public static int sceneDefCount
		{
			get
			{
				return SceneCatalog.indexToSceneDef.Length;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06003C44 RID: 15428 RVA: 0x000F959F File Offset: 0x000F779F
		public static ReadOnlyArray<SceneDef> allSceneDefs
		{
			get
			{
				return SceneCatalog.indexToSceneDef;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06003C45 RID: 15429 RVA: 0x000F95AB File Offset: 0x000F77AB
		public static ReadOnlyArray<SceneDef> allStageSceneDefs
		{
			get
			{
				return SceneCatalog._stageSceneDefs;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06003C46 RID: 15430 RVA: 0x000F95B7 File Offset: 0x000F77B7
		public static ReadOnlyArray<string> allBaseSceneNames
		{
			get
			{
				return SceneCatalog._baseSceneNames;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06003C47 RID: 15431 RVA: 0x000F95C3 File Offset: 0x000F77C3
		// (set) Token: 0x06003C48 RID: 15432 RVA: 0x000F95CA File Offset: 0x000F77CA
		[NotNull]
		public static SceneDef mostRecentSceneDef { get; private set; }

		// Token: 0x06003C49 RID: 15433 RVA: 0x000F95D2 File Offset: 0x000F77D2
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			SceneManager.activeSceneChanged += SceneCatalog.OnActiveSceneChanged;
			SceneCatalog.SetSceneDefs(ContentManager.sceneDefs);
			SceneCatalog.availability.MakeAvailable();
		}

		// Token: 0x06003C4A RID: 15434 RVA: 0x000F95FC File Offset: 0x000F77FC
		private static void SetSceneDefs([NotNull] SceneDef[] newSceneDefs)
		{
			SceneDef[] array = SceneCatalog.indexToSceneDef;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].sceneDefIndex = SceneIndex.Invalid;
			}
			ArrayUtils.CloneTo<SceneDef>(newSceneDefs, ref SceneCatalog.indexToSceneDef);
			Array.Sort<SceneDef>(SceneCatalog.indexToSceneDef, (SceneDef a, SceneDef b) => string.CompareOrdinal(a.cachedName, b.cachedName));
			for (int j = 0; j < SceneCatalog.indexToSceneDef.Length; j++)
			{
				SceneCatalog.indexToSceneDef[j].sceneDefIndex = (SceneIndex)j;
			}
			SceneCatalog.nameToIndex.Clear();
			for (int k = 0; k < SceneCatalog.indexToSceneDef.Length; k++)
			{
				SceneDef sceneDef2 = SceneCatalog.indexToSceneDef[k];
				SceneCatalog.nameToIndex[sceneDef2.cachedName] = (SceneIndex)k;
			}
			SceneCatalog._stageSceneDefs = (from sceneDef in SceneCatalog.indexToSceneDef
			where sceneDef.sceneType == SceneType.Stage
			select sceneDef).ToArray<SceneDef>();
			SceneCatalog._baseSceneNames = (from sceneDef in SceneCatalog.indexToSceneDef
			select sceneDef.baseSceneName).Distinct<string>().ToArray<string>();
			SceneCatalog.currentSceneDef = SceneCatalog.GetSceneDefFromSceneName(SceneManager.GetActiveScene().name);
			SceneCatalog.mostRecentSceneDef = SceneCatalog.currentSceneDef;
		}

		// Token: 0x06003C4B RID: 15435 RVA: 0x000F973D File Offset: 0x000F793D
		private static void OnActiveSceneChanged(Scene oldScene, Scene newScene)
		{
			SceneCatalog.currentSceneDef = SceneCatalog.GetSceneDefFromSceneName(newScene.name);
			if (SceneCatalog.currentSceneDef != null)
			{
				SceneCatalog.mostRecentSceneDef = SceneCatalog.currentSceneDef;
				Action<SceneDef> action = SceneCatalog.onMostRecentSceneDefChanged;
				if (action == null)
				{
					return;
				}
				action(SceneCatalog.mostRecentSceneDef);
			}
		}

		// Token: 0x06003C4C RID: 15436 RVA: 0x000F9775 File Offset: 0x000F7975
		[NotNull]
		public static UnlockableDef GetUnlockableLogFromBaseSceneName([NotNull] string baseSceneName)
		{
			return UnlockableCatalog.GetUnlockableDef(string.Format(CultureInfo.InvariantCulture, "Logs.Stages.{0}", baseSceneName));
		}

		// Token: 0x06003C4D RID: 15437 RVA: 0x000F978C File Offset: 0x000F798C
		[CanBeNull]
		public static SceneDef GetSceneDefForCurrentScene()
		{
			return SceneCatalog.GetSceneDefFromScene(SceneManager.GetActiveScene());
		}

		// Token: 0x06003C4E RID: 15438 RVA: 0x000F9798 File Offset: 0x000F7998
		[CanBeNull]
		public static SceneDef GetSceneDef(SceneIndex sceneIndex)
		{
			return ArrayUtils.GetSafe<SceneDef>(SceneCatalog.indexToSceneDef, (int)sceneIndex);
		}

		// Token: 0x06003C4F RID: 15439 RVA: 0x000F97A8 File Offset: 0x000F79A8
		[CanBeNull]
		public static SceneDef GetSceneDefFromSceneName([NotNull] string sceneName)
		{
			SceneDef sceneDef = SceneCatalog.FindSceneDef(sceneName);
			if (sceneDef != null)
			{
				return sceneDef;
			}
			Debug.LogWarningFormat("Could not find scene with name \"{0}\".", new object[]
			{
				sceneName
			});
			return null;
		}

		// Token: 0x06003C50 RID: 15440 RVA: 0x000F97D6 File Offset: 0x000F79D6
		[CanBeNull]
		public static SceneDef GetSceneDefFromScene(Scene scene)
		{
			return SceneCatalog.GetSceneDefFromSceneName(scene.name);
		}

		// Token: 0x06003C51 RID: 15441 RVA: 0x000F97E4 File Offset: 0x000F79E4
		public static SceneIndex FindSceneIndex([NotNull] string sceneName)
		{
			SceneIndex result;
			if (SceneCatalog.nameToIndex.TryGetValue(sceneName, out result))
			{
				return result;
			}
			return SceneIndex.Invalid;
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x000F9803 File Offset: 0x000F7A03
		public static SceneDef FindSceneDef([NotNull] string sceneName)
		{
			return ArrayUtils.GetSafe<SceneDef>(SceneCatalog.indexToSceneDef, (int)SceneCatalog.FindSceneIndex(sceneName));
		}

		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x06003C53 RID: 15443 RVA: 0x000F9818 File Offset: 0x000F7A18
		// (remove) Token: 0x06003C54 RID: 15444 RVA: 0x000F984C File Offset: 0x000F7A4C
		public static event Action<SceneDef> onMostRecentSceneDefChanged;

		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x06003C55 RID: 15445 RVA: 0x000F987F File Offset: 0x000F7A7F
		// (remove) Token: 0x06003C56 RID: 15446 RVA: 0x000026ED File Offset: 0x000008ED
		[Obsolete("Use IContentPackProvider instead.")]
		public static event Action<List<SceneDef>> getAdditionalEntries
		{
			add
			{
				LegacyModContentPackProvider.instance.HandleLegacyGetAdditionalEntries<SceneDef>("RoR2.SceneCatalog.getAdditionalEntries", value, LegacyModContentPackProvider.instance.registrationContentPack.sceneDefs);
			}
			remove
			{
			}
		}

		// Token: 0x04003BA9 RID: 15273
		private static SceneDef[] indexToSceneDef = Array.Empty<SceneDef>();

		// Token: 0x04003BAA RID: 15274
		private static SceneDef[] _stageSceneDefs = Array.Empty<SceneDef>();

		// Token: 0x04003BAB RID: 15275
		private static string[] _baseSceneNames = Array.Empty<string>();

		// Token: 0x04003BAC RID: 15276
		private static readonly Dictionary<string, SceneIndex> nameToIndex = new Dictionary<string, SceneIndex>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04003BAD RID: 15277
		private static SceneDef currentSceneDef;

		// Token: 0x04003BAF RID: 15279
		public static ResourceAvailability availability;
	}
}
