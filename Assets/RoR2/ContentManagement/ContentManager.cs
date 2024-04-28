using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HG;
using HG.Coroutines;
using RoR2.EntitlementManagement;
using RoR2.ExpansionManagement;
using RoR2.Skills;
using UnityEngine;

namespace RoR2.ContentManagement
{
	// Token: 0x02000E18 RID: 3608
	public static class ContentManager
	{
		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06005298 RID: 21144 RVA: 0x00156E66 File Offset: 0x00155066
		public static ItemDef[] itemDefs
		{
			get
			{
				return ContentManager._itemDefs;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06005299 RID: 21145 RVA: 0x00156E6D File Offset: 0x0015506D
		public static ItemTierDef[] itemTierDefs
		{
			get
			{
				return ContentManager._itemTierDefs;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x0600529A RID: 21146 RVA: 0x00156E74 File Offset: 0x00155074
		public static ItemRelationshipProvider[] itemRelationshipProviders
		{
			get
			{
				return ContentManager._itemRelationshipProviders;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x0600529B RID: 21147 RVA: 0x00156E7B File Offset: 0x0015507B
		public static ItemRelationshipType[] itemRelationshipTypes
		{
			get
			{
				return ContentManager._itemRelationshipTypes;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600529C RID: 21148 RVA: 0x00156E82 File Offset: 0x00155082
		public static EquipmentDef[] equipmentDefs
		{
			get
			{
				return ContentManager._equipmentDefs;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x0600529D RID: 21149 RVA: 0x00156E89 File Offset: 0x00155089
		public static BuffDef[] buffDefs
		{
			get
			{
				return ContentManager._buffDefs;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x0600529E RID: 21150 RVA: 0x00156E90 File Offset: 0x00155090
		public static EliteDef[] eliteDefs
		{
			get
			{
				return ContentManager._eliteDefs;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x0600529F RID: 21151 RVA: 0x00156E97 File Offset: 0x00155097
		public static UnlockableDef[] unlockableDefs
		{
			get
			{
				return ContentManager._unlockableDefs;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060052A0 RID: 21152 RVA: 0x00156E9E File Offset: 0x0015509E
		public static SurvivorDef[] survivorDefs
		{
			get
			{
				return ContentManager._survivorDefs;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060052A1 RID: 21153 RVA: 0x00156EA5 File Offset: 0x001550A5
		public static GameObject[] bodyPrefabs
		{
			get
			{
				return ContentManager._bodyPrefabs;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060052A2 RID: 21154 RVA: 0x00156EAC File Offset: 0x001550AC
		public static GameObject[] masterPrefabs
		{
			get
			{
				return ContentManager._masterPrefabs;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060052A3 RID: 21155 RVA: 0x00156EB3 File Offset: 0x001550B3
		public static ArtifactDef[] artifactDefs
		{
			get
			{
				return ContentManager._artifactDefs;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060052A4 RID: 21156 RVA: 0x00156EBA File Offset: 0x001550BA
		public static EffectDef[] effectDefs
		{
			get
			{
				return ContentManager._effectDefs;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060052A5 RID: 21157 RVA: 0x00156EC1 File Offset: 0x001550C1
		public static SkillDef[] skillDefs
		{
			get
			{
				return ContentManager._skillDefs;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060052A6 RID: 21158 RVA: 0x00156EC8 File Offset: 0x001550C8
		public static SkillFamily[] skillFamilies
		{
			get
			{
				return ContentManager._skillFamilies;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060052A7 RID: 21159 RVA: 0x00156ECF File Offset: 0x001550CF
		public static SurfaceDef[] surfaceDefs
		{
			get
			{
				return ContentManager._surfaceDefs;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060052A8 RID: 21160 RVA: 0x00156ED6 File Offset: 0x001550D6
		public static SceneDef[] sceneDefs
		{
			get
			{
				return ContentManager._sceneDefs;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060052A9 RID: 21161 RVA: 0x00156EDD File Offset: 0x001550DD
		public static GameObject[] projectilePrefabs
		{
			get
			{
				return ContentManager._projectilePrefabs;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x060052AA RID: 21162 RVA: 0x00156EE4 File Offset: 0x001550E4
		public static NetworkSoundEventDef[] networkSoundEventDefs
		{
			get
			{
				return ContentManager._networkSoundEventDefs;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060052AB RID: 21163 RVA: 0x00156EEB File Offset: 0x001550EB
		public static MusicTrackDef[] musicTrackDefs
		{
			get
			{
				return ContentManager._musicTrackDefs;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060052AC RID: 21164 RVA: 0x00156EF2 File Offset: 0x001550F2
		public static GameObject[] networkedObjectPrefabs
		{
			get
			{
				return ContentManager._networkedObjectPrefabs;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060052AD RID: 21165 RVA: 0x00156EF9 File Offset: 0x001550F9
		public static GameObject[] gameModePrefabs
		{
			get
			{
				return ContentManager._gameModePrefabs;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x060052AE RID: 21166 RVA: 0x00156F00 File Offset: 0x00155100
		public static GameEndingDef[] gameEndingDefs
		{
			get
			{
				return ContentManager._gameEndingDefs;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x060052AF RID: 21167 RVA: 0x00156F07 File Offset: 0x00155107
		public static EntityStateConfiguration[] entityStateConfigurations
		{
			get
			{
				return ContentManager._entityStateConfigurations;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x060052B0 RID: 21168 RVA: 0x00156F0E File Offset: 0x0015510E
		public static Type[] entityStateTypes
		{
			get
			{
				return ContentManager._entityStateTypes;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x060052B1 RID: 21169 RVA: 0x00156F15 File Offset: 0x00155115
		public static ExpansionDef[] expansionDefs
		{
			get
			{
				return ContentManager._expansionDefs;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x060052B2 RID: 21170 RVA: 0x00156F1C File Offset: 0x0015511C
		public static EntitlementDef[] entitlementDefs
		{
			get
			{
				return ContentManager._entitlementDefs;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x060052B3 RID: 21171 RVA: 0x00156F23 File Offset: 0x00155123
		public static MiscPickupDef[] miscPickupDefs
		{
			get
			{
				return ContentManager._miscPickupDefs;
			}
		}

		// Token: 0x060052B4 RID: 21172 RVA: 0x00156F2A File Offset: 0x0015512A
		public static IEnumerator LoadContentPacks(IProgress<float> progressReceiver)
		{
			ContentManager.<>c__DisplayClass86_0 CS$<>8__locals1 = new ContentManager.<>c__DisplayClass86_0();
			CS$<>8__locals1.progressReceiver = progressReceiver;
			CS$<>8__locals1.contentPackProviders = new List<IContentPackProvider>();
			ContentManager.CollectContentPackProvidersDelegate collectContentPackProvidersDelegate = ContentManager.collectContentPackProviders;
			if (collectContentPackProvidersDelegate != null)
			{
				collectContentPackProvidersDelegate(new ContentManager.AddContentPackProviderDelegate(CS$<>8__locals1.<LoadContentPacks>g__AddContentPackProvider|0));
			}
			ContentManager.collectContentPackProviders = null;
			Debug.Log("LoadContentPacks() start");
			ContentManager.ContentPackLoader loader = new ContentManager.ContentPackLoader(CS$<>8__locals1.contentPackProviders);
			yield return loader.InitialLoad(new ReadableProgress<float>(delegate(float newValue)
			{
				CS$<>8__locals1.progressReceiver.Report(Util.Remap(newValue, 0f, 1f, 0f, 0.8f));
			}));
			yield return loader.LoadContentPacks(new ReadableProgress<float>(delegate(float newValue)
			{
				CS$<>8__locals1.progressReceiver.Report(Util.Remap(newValue, 0f, 1f, 0.8f, 0.95f));
			}));
			yield return loader.RunCleanup(new ReadableProgress<float>(delegate(float newValue)
			{
				CS$<>8__locals1.progressReceiver.Report(Util.Remap(newValue, 0f, 1f, 0.95f, 1f));
			}));
			Debug.Log("LoadContentPacks() end");
			ContentManager.SetContentPacks(loader.output);
			yield break;
		}

		// Token: 0x060052B5 RID: 21173 RVA: 0x00156F3C File Offset: 0x0015513C
		private static void SetContentPacks(List<ReadOnlyContentPack> newContentPacks)
		{
			ContentManager.<>c__DisplayClass87_0 CS$<>8__locals1;
			CS$<>8__locals1.newContentPacks = newContentPacks;
			ContentManager.allLoadedContentPacks = CS$<>8__locals1.newContentPacks.ToArray();
			ContentManager.<SetContentPacks>g__SetAssets|87_0<ItemDef>(ref ContentManager._itemDefs, (ReadOnlyContentPack contentPack) => contentPack.itemDefs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<ItemTierDef>(ref ContentManager._itemTierDefs, (ReadOnlyContentPack contentPack) => contentPack.itemTierDefs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<ItemRelationshipProvider>(ref ContentManager._itemRelationshipProviders, (ReadOnlyContentPack contentPack) => contentPack.itemRelationshipProviders, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<ItemRelationshipType>(ref ContentManager._itemRelationshipTypes, (ReadOnlyContentPack contentPack) => contentPack.itemRelationshipTypes, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<EquipmentDef>(ref ContentManager._equipmentDefs, (ReadOnlyContentPack contentPack) => contentPack.equipmentDefs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<BuffDef>(ref ContentManager._buffDefs, (ReadOnlyContentPack contentPack) => contentPack.buffDefs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<EliteDef>(ref ContentManager._eliteDefs, (ReadOnlyContentPack contentPack) => contentPack.eliteDefs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<UnlockableDef>(ref ContentManager._unlockableDefs, (ReadOnlyContentPack contentPack) => contentPack.unlockableDefs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<SurvivorDef>(ref ContentManager._survivorDefs, (ReadOnlyContentPack contentPack) => contentPack.survivorDefs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<GameObject>(ref ContentManager._bodyPrefabs, (ReadOnlyContentPack contentPack) => contentPack.bodyPrefabs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<GameObject>(ref ContentManager._masterPrefabs, (ReadOnlyContentPack contentPack) => contentPack.masterPrefabs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<ArtifactDef>(ref ContentManager._artifactDefs, (ReadOnlyContentPack contentPack) => contentPack.artifactDefs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<EffectDef>(ref ContentManager._effectDefs, (ReadOnlyContentPack contentPack) => contentPack.effectDefs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<SkillDef>(ref ContentManager._skillDefs, (ReadOnlyContentPack contentPack) => contentPack.skillDefs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<SkillFamily>(ref ContentManager._skillFamilies, (ReadOnlyContentPack contentPack) => contentPack.skillFamilies, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<SurfaceDef>(ref ContentManager._surfaceDefs, (ReadOnlyContentPack contentPack) => contentPack.surfaceDefs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<SceneDef>(ref ContentManager._sceneDefs, (ReadOnlyContentPack contentPack) => contentPack.sceneDefs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<GameObject>(ref ContentManager._projectilePrefabs, (ReadOnlyContentPack contentPack) => contentPack.projectilePrefabs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<NetworkSoundEventDef>(ref ContentManager._networkSoundEventDefs, (ReadOnlyContentPack contentPack) => contentPack.networkSoundEventDefs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<MusicTrackDef>(ref ContentManager._musicTrackDefs, (ReadOnlyContentPack contentPack) => contentPack.musicTrackDefs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<GameObject>(ref ContentManager._networkedObjectPrefabs, (ReadOnlyContentPack contentPack) => contentPack.networkedObjectPrefabs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<GameObject>(ref ContentManager._gameModePrefabs, (ReadOnlyContentPack contentPack) => contentPack.gameModePrefabs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<GameEndingDef>(ref ContentManager._gameEndingDefs, (ReadOnlyContentPack contentPack) => contentPack.gameEndingDefs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<Type>(ref ContentManager._entityStateTypes, (ReadOnlyContentPack contentPack) => contentPack.entityStateTypes, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<EntityStateConfiguration>(ref ContentManager._entityStateConfigurations, (ReadOnlyContentPack contentPack) => contentPack.entityStateConfigurations, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<ExpansionDef>(ref ContentManager._expansionDefs, (ReadOnlyContentPack contentPack) => contentPack.expansionDefs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<EntitlementDef>(ref ContentManager._entitlementDefs, (ReadOnlyContentPack contentPack) => contentPack.entitlementDefs, ref CS$<>8__locals1);
			ContentManager.<SetContentPacks>g__SetAssets|87_0<MiscPickupDef>(ref ContentManager._miscPickupDefs, (ReadOnlyContentPack contentPack) => contentPack.miscPickupDefs, ref CS$<>8__locals1);
			Action<ReadOnlyArray<ReadOnlyContentPack>> action = ContentManager.onContentPacksAssigned;
			if (action == null)
			{
				return;
			}
			action(ContentManager.allLoadedContentPacks);
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060052B6 RID: 21174 RVA: 0x0015742E File Offset: 0x0015562E
		// (set) Token: 0x060052B7 RID: 21175 RVA: 0x00157435 File Offset: 0x00155635
		public static ReadOnlyArray<ReadOnlyContentPack> allLoadedContentPacks { get; private set; } = Array.Empty<ReadOnlyContentPack>();

		// Token: 0x060052B8 RID: 21176 RVA: 0x00157440 File Offset: 0x00155640
		public static ReadOnlyContentPack? FindContentPack(string contentPackIdentifier)
		{
			for (int i = 0; i < ContentManager.allLoadedContentPacks.Length; i++)
			{
				ref readonly ReadOnlyContentPack ptr = ref ContentManager.allLoadedContentPacks[i];
				if (string.Equals(ptr.identifier, contentPackIdentifier, StringComparison.Ordinal))
				{
					return new ReadOnlyContentPack?(ptr);
				}
			}
			return null;
		}

		// Token: 0x14000111 RID: 273
		// (add) Token: 0x060052B9 RID: 21177 RVA: 0x00157498 File Offset: 0x00155698
		// (remove) Token: 0x060052BA RID: 21178 RVA: 0x001574CC File Offset: 0x001556CC
		public static event ContentManager.CollectContentPackProvidersDelegate collectContentPackProviders;

		// Token: 0x14000112 RID: 274
		// (add) Token: 0x060052BB RID: 21179 RVA: 0x00157500 File Offset: 0x00155700
		// (remove) Token: 0x060052BC RID: 21180 RVA: 0x00157534 File Offset: 0x00155734
		public static event Action<ReadOnlyArray<ReadOnlyContentPack>> onContentPacksAssigned;

		// Token: 0x060052BE RID: 21182 RVA: 0x00157578 File Offset: 0x00155778
		[CompilerGenerated]
		internal static void <SetContentPacks>g__SetAssets|87_0<T>(ref T[] field, Func<ReadOnlyContentPack, IEnumerable<T>> selector, ref ContentManager.<>c__DisplayClass87_0 A_2)
		{
			field = A_2.newContentPacks.SelectMany(selector).ToArray<T>();
		}

		// Token: 0x04004EE8 RID: 20200
		private static ContentPack[] contentPacks;

		// Token: 0x04004EE9 RID: 20201
		public static ItemDef[] _itemDefs;

		// Token: 0x04004EEA RID: 20202
		public static ItemTierDef[] _itemTierDefs;

		// Token: 0x04004EEB RID: 20203
		private static ItemRelationshipProvider[] _itemRelationshipProviders;

		// Token: 0x04004EEC RID: 20204
		private static ItemRelationshipType[] _itemRelationshipTypes;

		// Token: 0x04004EED RID: 20205
		public static EquipmentDef[] _equipmentDefs;

		// Token: 0x04004EEE RID: 20206
		public static BuffDef[] _buffDefs;

		// Token: 0x04004EEF RID: 20207
		public static EliteDef[] _eliteDefs;

		// Token: 0x04004EF0 RID: 20208
		public static UnlockableDef[] _unlockableDefs;

		// Token: 0x04004EF1 RID: 20209
		public static SurvivorDef[] _survivorDefs;

		// Token: 0x04004EF2 RID: 20210
		public static GameObject[] _bodyPrefabs;

		// Token: 0x04004EF3 RID: 20211
		public static GameObject[] _masterPrefabs;

		// Token: 0x04004EF4 RID: 20212
		public static ArtifactDef[] _artifactDefs;

		// Token: 0x04004EF5 RID: 20213
		public static EffectDef[] _effectDefs;

		// Token: 0x04004EF6 RID: 20214
		public static SkillDef[] _skillDefs;

		// Token: 0x04004EF7 RID: 20215
		public static SkillFamily[] _skillFamilies;

		// Token: 0x04004EF8 RID: 20216
		public static SurfaceDef[] _surfaceDefs;

		// Token: 0x04004EF9 RID: 20217
		public static SceneDef[] _sceneDefs;

		// Token: 0x04004EFA RID: 20218
		public static GameObject[] _projectilePrefabs;

		// Token: 0x04004EFB RID: 20219
		public static NetworkSoundEventDef[] _networkSoundEventDefs;

		// Token: 0x04004EFC RID: 20220
		public static MusicTrackDef[] _musicTrackDefs;

		// Token: 0x04004EFD RID: 20221
		public static GameObject[] _networkedObjectPrefabs;

		// Token: 0x04004EFE RID: 20222
		public static GameObject[] _gameModePrefabs;

		// Token: 0x04004EFF RID: 20223
		public static GameEndingDef[] _gameEndingDefs;

		// Token: 0x04004F00 RID: 20224
		public static EntityStateConfiguration[] _entityStateConfigurations;

		// Token: 0x04004F01 RID: 20225
		public static Type[] _entityStateTypes;

		// Token: 0x04004F02 RID: 20226
		public static ExpansionDef[] _expansionDefs;

		// Token: 0x04004F03 RID: 20227
		public static EntitlementDef[] _entitlementDefs;

		// Token: 0x04004F04 RID: 20228
		private static MiscPickupDef[] _miscPickupDefs;

		// Token: 0x02000E19 RID: 3609
		private class ContentPackLoader
		{
			// Token: 0x060052BF RID: 21183 RVA: 0x00157590 File Offset: 0x00155790
			public ContentPackLoader(List<IContentPackProvider> contentPackProviders)
			{
				this.contentPackLoadInfos = new ContentPackLoadInfo[contentPackProviders.Count];
				for (int i = 0; i < contentPackProviders.Count; i++)
				{
					string identifier = contentPackProviders[i].identifier;
					this.contentPackLoadInfos[i] = new ContentPackLoadInfo
					{
						index = i,
						contentPackProviderIdentifier = identifier,
						contentPackProvider = contentPackProviders[i],
						previousContentPack = new ContentPack
						{
							identifier = identifier
						},
						retries = 0
					};
				}
			}

			// Token: 0x060052C0 RID: 21184 RVA: 0x00157630 File Offset: 0x00155830
			private IEnumerator DoPerContentPackProviderCoroutine(IProgress<float> progressReceiver, ContentManager.ContentPackLoader.PerContentPackAction action)
			{
				ReadOnlyArray<ContentPackLoadInfo> readOnlyPeers = ArrayUtils.Clone<ContentPackLoadInfo>(this.contentPackLoadInfos);
				ParallelProgressCoroutine parallelProgressCoroutine = new ParallelProgressCoroutine(progressReceiver);
				for (int i = 0; i < this.contentPackLoadInfos.Length; i++)
				{
					ReadableProgress<float> readableProgress = new ReadableProgress<float>();
					IEnumerator coroutine = action(ref this.contentPackLoadInfos[i], readOnlyPeers, readableProgress);
					parallelProgressCoroutine.Add(coroutine, readableProgress);
				}
				return parallelProgressCoroutine;
			}

			// Token: 0x060052C1 RID: 21185 RVA: 0x0015768E File Offset: 0x0015588E
			public IEnumerator InitialLoad(IProgress<float> progressReceiver)
			{
				yield return this.DoPerContentPackProviderCoroutine(progressReceiver, new ContentManager.ContentPackLoader.PerContentPackAction(ContentManager.ContentPackLoader.<>c.<>9.<InitialLoad>g__StartLoadCoroutine|4_0));
				yield break;
			}

			// Token: 0x060052C2 RID: 21186 RVA: 0x001576A4 File Offset: 0x001558A4
			public IEnumerator StepGenerateContentPacks(IProgress<float> progressReceiver, IProgress<bool> completionReceiver, int retriesRemaining)
			{
				ContentManager.ContentPackLoader.<>c__DisplayClass5_0 CS$<>8__locals1 = new ContentManager.ContentPackLoader.<>c__DisplayClass5_0();
				CS$<>8__locals1.retriesRemaining = retriesRemaining;
				CS$<>8__locals1.newContentPacks = new ContentPack[this.contentPackLoadInfos.Length];
				for (int i = 0; i < CS$<>8__locals1.newContentPacks.Length; i++)
				{
					CS$<>8__locals1.newContentPacks[i] = new ContentPack
					{
						identifier = this.contentPackLoadInfos[i].contentPackProviderIdentifier
					};
				}
				yield return this.DoPerContentPackProviderCoroutine(progressReceiver, new ContentManager.ContentPackLoader.PerContentPackAction(CS$<>8__locals1.<StepGenerateContentPacks>g__StartGeneratorCoroutine|0));
				bool flag = true;
				for (int j = 0; j < this.contentPackLoadInfos.Length; j++)
				{
					ContentPack contentPack = CS$<>8__locals1.newContentPacks[j];
					contentPack.identifier = this.contentPackLoadInfos[j].contentPackProviderIdentifier;
					if (!this.contentPackLoadInfos[j].previousContentPack.Equals(contentPack))
					{
						flag = false;
						ContentPackLoadInfo[] array = this.contentPackLoadInfos;
						int num = j;
						array[num].retries = array[num].retries + 1;
						this.contentPackLoadInfos[j].previousContentPack = contentPack;
					}
				}
				if (flag)
				{
					completionReceiver.Report(true);
				}
				yield break;
			}

			// Token: 0x060052C3 RID: 21187 RVA: 0x001576C8 File Offset: 0x001558C8
			public IEnumerator LoadContentPacks(IProgress<float> progressReceiver)
			{
				int maxRetries = 10;
				bool complete = false;
				ReadableProgress<bool> completionReceiver = new ReadableProgress<bool>(delegate(bool result)
				{
					complete = result;
				});
				int num;
				for (int i = 0; i < maxRetries; i = num)
				{
					yield return this.StepGenerateContentPacks(progressReceiver, completionReceiver, maxRetries - i - 1);
					if (complete)
					{
						break;
					}
					num = i + 1;
				}
				for (int j = 0; j < this.contentPackLoadInfos.Length; j++)
				{
					this.output.Add(this.contentPackLoadInfos[j].previousContentPack);
				}
				yield break;
			}

			// Token: 0x060052C4 RID: 21188 RVA: 0x001576DE File Offset: 0x001558DE
			public IEnumerator RunCleanup(IProgress<float> progressReceiver)
			{
				yield return this.DoPerContentPackProviderCoroutine(progressReceiver, new ContentManager.ContentPackLoader.PerContentPackAction(this.<RunCleanup>g__StartFinalizeCoroutine|7_0));
				yield break;
			}

			// Token: 0x060052C5 RID: 21189 RVA: 0x001576F4 File Offset: 0x001558F4
			[CompilerGenerated]
			private IEnumerator <RunCleanup>g__StartFinalizeCoroutine|7_0(ref ContentPackLoadInfo loadInfo, ReadOnlyArray<ContentPackLoadInfo> readOnlyPeers, IProgress<float> providedProgressReceiver)
			{
				FinalizeAsyncArgs args = new FinalizeAsyncArgs(providedProgressReceiver, readOnlyPeers, this.contentPackLoadInfos[loadInfo.index].previousContentPack);
				return loadInfo.contentPackProvider.FinalizeAsync(args);
			}

			// Token: 0x04004F08 RID: 20232
			private ContentPackLoadInfo[] contentPackLoadInfos;

			// Token: 0x04004F09 RID: 20233
			public List<ReadOnlyContentPack> output = new List<ReadOnlyContentPack>();

			// Token: 0x02000E1A RID: 3610
			// (Invoke) Token: 0x060052C7 RID: 21191
			private delegate IEnumerator PerContentPackAction(ref ContentPackLoadInfo contentPackLoadInfo, ReadOnlyArray<ContentPackLoadInfo> readOnlyPeers, IProgress<float> progressReceiver);
		}

		// Token: 0x02000E22 RID: 3618
		// (Invoke) Token: 0x060052EA RID: 21226
		public delegate void AddContentPackProviderDelegate(IContentPackProvider contentPackProvider);

		// Token: 0x02000E23 RID: 3619
		// (Invoke) Token: 0x060052EE RID: 21230
		public delegate void CollectContentPackProvidersDelegate(ContentManager.AddContentPackProviderDelegate addContentPackProvider);
	}
}
