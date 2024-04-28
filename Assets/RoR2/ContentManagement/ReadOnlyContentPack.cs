using System;
using JetBrains.Annotations;
using RoR2.EntitlementManagement;
using RoR2.ExpansionManagement;
using RoR2.Skills;
using UnityEngine;

namespace RoR2.ContentManagement
{
	// Token: 0x02000E2A RID: 3626
	public readonly struct ReadOnlyContentPack
	{
		// Token: 0x0600532A RID: 21290 RVA: 0x00158BEF File Offset: 0x00156DEF
		public ReadOnlyContentPack([NotNull] ContentPack src)
		{
			if (src == null)
			{
				throw new ArgumentNullException("src");
			}
			this.src = src;
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x0600532B RID: 21291 RVA: 0x00158C07 File Offset: 0x00156E07
		public bool isValid
		{
			get
			{
				return this.src != null;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x0600532C RID: 21292 RVA: 0x00158C12 File Offset: 0x00156E12
		public string identifier
		{
			get
			{
				return this.src.identifier;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x0600532D RID: 21293 RVA: 0x00158C1F File Offset: 0x00156E1F
		public ReadOnlyNamedAssetCollection<GameObject> bodyPrefabs
		{
			get
			{
				return this.src.bodyPrefabs;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600532E RID: 21294 RVA: 0x00158C31 File Offset: 0x00156E31
		public ReadOnlyNamedAssetCollection<GameObject> masterPrefabs
		{
			get
			{
				return this.src.masterPrefabs;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x0600532F RID: 21295 RVA: 0x00158C43 File Offset: 0x00156E43
		public ReadOnlyNamedAssetCollection<GameObject> projectilePrefabs
		{
			get
			{
				return this.src.projectilePrefabs;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06005330 RID: 21296 RVA: 0x00158C55 File Offset: 0x00156E55
		public ReadOnlyNamedAssetCollection<GameObject> gameModePrefabs
		{
			get
			{
				return this.src.gameModePrefabs;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06005331 RID: 21297 RVA: 0x00158C67 File Offset: 0x00156E67
		public ReadOnlyNamedAssetCollection<GameObject> networkedObjectPrefabs
		{
			get
			{
				return this.src.networkedObjectPrefabs;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06005332 RID: 21298 RVA: 0x00158C79 File Offset: 0x00156E79
		public ReadOnlyNamedAssetCollection<SkillDef> skillDefs
		{
			get
			{
				return this.src.skillDefs;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06005333 RID: 21299 RVA: 0x00158C8B File Offset: 0x00156E8B
		public ReadOnlyNamedAssetCollection<SkillFamily> skillFamilies
		{
			get
			{
				return this.src.skillFamilies;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06005334 RID: 21300 RVA: 0x00158C9D File Offset: 0x00156E9D
		public ReadOnlyNamedAssetCollection<SceneDef> sceneDefs
		{
			get
			{
				return this.src.sceneDefs;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06005335 RID: 21301 RVA: 0x00158CAF File Offset: 0x00156EAF
		public ReadOnlyNamedAssetCollection<ItemDef> itemDefs
		{
			get
			{
				return this.src.itemDefs;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06005336 RID: 21302 RVA: 0x00158CC1 File Offset: 0x00156EC1
		public ReadOnlyNamedAssetCollection<ItemTierDef> itemTierDefs
		{
			get
			{
				return this.src.itemTierDefs;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06005337 RID: 21303 RVA: 0x00158CD3 File Offset: 0x00156ED3
		public ReadOnlyNamedAssetCollection<ItemRelationshipProvider> itemRelationshipProviders
		{
			get
			{
				return this.src.itemRelationshipProviders;
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06005338 RID: 21304 RVA: 0x00158CE5 File Offset: 0x00156EE5
		public ReadOnlyNamedAssetCollection<ItemRelationshipType> itemRelationshipTypes
		{
			get
			{
				return this.src.itemRelationshipTypes;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06005339 RID: 21305 RVA: 0x00158CF7 File Offset: 0x00156EF7
		public ReadOnlyNamedAssetCollection<EquipmentDef> equipmentDefs
		{
			get
			{
				return this.src.equipmentDefs;
			}
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x0600533A RID: 21306 RVA: 0x00158D09 File Offset: 0x00156F09
		public ReadOnlyNamedAssetCollection<BuffDef> buffDefs
		{
			get
			{
				return this.src.buffDefs;
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x0600533B RID: 21307 RVA: 0x00158D1B File Offset: 0x00156F1B
		public ReadOnlyNamedAssetCollection<EliteDef> eliteDefs
		{
			get
			{
				return this.src.eliteDefs;
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x0600533C RID: 21308 RVA: 0x00158D2D File Offset: 0x00156F2D
		public ReadOnlyNamedAssetCollection<UnlockableDef> unlockableDefs
		{
			get
			{
				return this.src.unlockableDefs;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x0600533D RID: 21309 RVA: 0x00158D3F File Offset: 0x00156F3F
		public ReadOnlyNamedAssetCollection<SurvivorDef> survivorDefs
		{
			get
			{
				return this.src.survivorDefs;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x0600533E RID: 21310 RVA: 0x00158D51 File Offset: 0x00156F51
		public ReadOnlyNamedAssetCollection<ArtifactDef> artifactDefs
		{
			get
			{
				return this.src.artifactDefs;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x0600533F RID: 21311 RVA: 0x00158D63 File Offset: 0x00156F63
		public ReadOnlyNamedAssetCollection<EffectDef> effectDefs
		{
			get
			{
				return this.src.effectDefs;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06005340 RID: 21312 RVA: 0x00158D75 File Offset: 0x00156F75
		public ReadOnlyNamedAssetCollection<SurfaceDef> surfaceDefs
		{
			get
			{
				return this.src.surfaceDefs;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06005341 RID: 21313 RVA: 0x00158D87 File Offset: 0x00156F87
		public ReadOnlyNamedAssetCollection<NetworkSoundEventDef> networkSoundEventDefs
		{
			get
			{
				return this.src.networkSoundEventDefs;
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06005342 RID: 21314 RVA: 0x00158D99 File Offset: 0x00156F99
		public ReadOnlyNamedAssetCollection<MusicTrackDef> musicTrackDefs
		{
			get
			{
				return this.src.musicTrackDefs;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06005343 RID: 21315 RVA: 0x00158DAB File Offset: 0x00156FAB
		public ReadOnlyNamedAssetCollection<GameEndingDef> gameEndingDefs
		{
			get
			{
				return this.src.gameEndingDefs;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06005344 RID: 21316 RVA: 0x00158DBD File Offset: 0x00156FBD
		public ReadOnlyNamedAssetCollection<EntityStateConfiguration> entityStateConfigurations
		{
			get
			{
				return this.src.entityStateConfigurations;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06005345 RID: 21317 RVA: 0x00158DCF File Offset: 0x00156FCF
		public ReadOnlyNamedAssetCollection<ExpansionDef> expansionDefs
		{
			get
			{
				return this.src.expansionDefs;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06005346 RID: 21318 RVA: 0x00158DE1 File Offset: 0x00156FE1
		public ReadOnlyNamedAssetCollection<EntitlementDef> entitlementDefs
		{
			get
			{
				return this.src.entitlementDefs;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06005347 RID: 21319 RVA: 0x00158DF3 File Offset: 0x00156FF3
		public ReadOnlyNamedAssetCollection<MiscPickupDef> miscPickupDefs
		{
			get
			{
				return this.src.miscPickupDefs;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06005348 RID: 21320 RVA: 0x00158E05 File Offset: 0x00157005
		public ReadOnlyNamedAssetCollection<Type> entityStateTypes
		{
			get
			{
				return this.src.entityStateTypes;
			}
		}

		// Token: 0x06005349 RID: 21321 RVA: 0x00158E17 File Offset: 0x00157017
		public bool ValueEquals(in ReadOnlyContentPack other)
		{
			return this.src.ValueEquals(other.src);
		}

		// Token: 0x0600534A RID: 21322 RVA: 0x00158E2A File Offset: 0x0015702A
		public static implicit operator ReadOnlyContentPack([NotNull] ContentPack contentPack)
		{
			return new ReadOnlyContentPack(contentPack);
		}

		// Token: 0x0600534B RID: 21323 RVA: 0x00158E32 File Offset: 0x00157032
		public bool FindAsset(string collectionName, string assetName, out object result)
		{
			return this.src.FindAsset(collectionName, assetName, out result);
		}

		// Token: 0x04004F6E RID: 20334
		private readonly ContentPack src;
	}
}
