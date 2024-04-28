using System;
using System.Collections.Generic;
using System.Linq;
using HG;
using JetBrains.Annotations;
using RoR2.ContentManagement;
using RoR2.Modding;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000918 RID: 2328
	public static class GameModeCatalog
	{
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060034AC RID: 13484 RVA: 0x000DE99F File Offset: 0x000DCB9F
		public static int gameModeCount
		{
			get
			{
				return GameModeCatalog.indexToPrefabComponents.Length;
			}
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x000DE9A8 File Offset: 0x000DCBA8
		[SystemInitializer(new Type[]
		{
			typeof(RuleCatalog)
		})]
		private static void LoadGameModes()
		{
			GameModeCatalog.SetGameModes((from v in ContentManager.gameModePrefabs
			select v.GetComponent<Run>()).ToArray<Run>());
			GameModeCatalog.availability.MakeAvailable();
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x000DE9E8 File Offset: 0x000DCBE8
		private static void SetGameModes(Run[] newGameModePrefabComponents)
		{
			Run[] array = GameModeCatalog.indexToPrefabComponents;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].gameModeIndex = GameModeIndex.Invalid;
			}
			ArrayUtils.CloneTo<Run>(newGameModePrefabComponents, ref GameModeCatalog.indexToPrefabComponents);
			Array.Sort<Run>(newGameModePrefabComponents, (Run a, Run b) => string.CompareOrdinal(a.name, b.name));
			GameModeCatalog.nameToIndex.Clear();
			GameModeCatalog.nameToPrefabComponents.Clear();
			Array.Resize<string>(ref GameModeCatalog.indexToName, GameModeCatalog.indexToPrefabComponents.Length);
			int j = 0;
			int num = GameModeCatalog.indexToPrefabComponents.Length;
			while (j < num)
			{
				Run run = GameModeCatalog.indexToPrefabComponents[j];
				string name = run.gameObject.name;
				string key = name + "(Clone)";
				GameModeCatalog.nameToIndex.Add(name, j);
				GameModeCatalog.nameToIndex.Add(key, j);
				GameModeCatalog.nameToPrefabComponents.Add(name, run);
				GameModeCatalog.nameToPrefabComponents.Add(key, run);
				run.gameModeIndex = (GameModeIndex)j;
				GameModeCatalog.indexToName[j] = name;
				j++;
			}
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x000DEAE8 File Offset: 0x000DCCE8
		[CanBeNull]
		public static Run FindGameModePrefabComponent([NotNull] string name)
		{
			Run result;
			GameModeCatalog.nameToPrefabComponents.TryGetValue(name, out result);
			return result;
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x000DEB04 File Offset: 0x000DCD04
		[CanBeNull]
		public static Run GetGameModePrefabComponent(GameModeIndex index)
		{
			return ArrayUtils.GetSafe<Run>(GameModeCatalog.indexToPrefabComponents, (int)index);
		}

		// Token: 0x060034B1 RID: 13489 RVA: 0x000DEB14 File Offset: 0x000DCD14
		public static GameModeIndex FindGameModeIndex([NotNull] string name)
		{
			int result;
			if (GameModeCatalog.nameToIndex.TryGetValue(name, out result))
			{
				return (GameModeIndex)result;
			}
			return GameModeIndex.Invalid;
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x000DEB33 File Offset: 0x000DCD33
		[CanBeNull]
		public static string GetGameModeName(GameModeIndex index)
		{
			return ArrayUtils.GetSafe<string>(GameModeCatalog.indexToName, (int)index);
		}

		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x060034B3 RID: 13491 RVA: 0x000DEB40 File Offset: 0x000DCD40
		// (remove) Token: 0x060034B4 RID: 13492 RVA: 0x000026ED File Offset: 0x000008ED
		[Obsolete("Use IContentPackProvider instead.")]
		public static event Action<List<GameObject>> getAdditionalEntries
		{
			add
			{
				LegacyModContentPackProvider.instance.HandleLegacyGetAdditionalEntries<GameObject>("RoR2.GameModeCatalog.getAdditionalEntries", value, LegacyModContentPackProvider.instance.registrationContentPack.gameModePrefabs);
			}
			remove
			{
			}
		}

		// Token: 0x040035A5 RID: 13733
		private static readonly Dictionary<string, int> nameToIndex = new Dictionary<string, int>();

		// Token: 0x040035A6 RID: 13734
		private static Run[] indexToPrefabComponents = Array.Empty<Run>();

		// Token: 0x040035A7 RID: 13735
		private static string[] indexToName = Array.Empty<string>();

		// Token: 0x040035A8 RID: 13736
		private static readonly Dictionary<string, Run> nameToPrefabComponents = new Dictionary<string, Run>();

		// Token: 0x040035A9 RID: 13737
		public static ResourceAvailability availability;
	}
}
