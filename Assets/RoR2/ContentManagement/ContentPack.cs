using System;
using JetBrains.Annotations;
using RoR2.EntitlementManagement;
using RoR2.ExpansionManagement;
using RoR2.Skills;
using UnityEngine;

namespace RoR2.ContentManagement
{
	// Token: 0x02000E28 RID: 3624
	public sealed class ContentPack
	{
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x0600531A RID: 21274 RVA: 0x00157ECA File Offset: 0x001560CA
		// (set) Token: 0x0600531B RID: 21275 RVA: 0x00157ED2 File Offset: 0x001560D2
		[NotNull]
		public string identifier
		{
			get
			{
				return this._identifier;
			}
			internal set
			{
				if (value == null)
				{
					throw new ArgumentNullException("'identifier' cannot be null.");
				}
				this._identifier = value;
			}
		}

		// Token: 0x0600531C RID: 21276 RVA: 0x00157EEC File Offset: 0x001560EC
		public ContentPack()
		{
			this.assetCollections = new object[]
			{
				this.bodyPrefabs,
				this.masterPrefabs,
				this.projectilePrefabs,
				this.gameModePrefabs,
				this.networkedObjectPrefabs,
				this.skillDefs,
				this.skillFamilies,
				this.sceneDefs,
				this.itemDefs,
				this.itemTierDefs,
				this.itemRelationshipProviders,
				this.itemRelationshipTypes,
				this.equipmentDefs,
				this.buffDefs,
				this.eliteDefs,
				this.unlockableDefs,
				this.survivorDefs,
				this.artifactDefs,
				this.effectDefs,
				this.surfaceDefs,
				this.networkSoundEventDefs,
				this.musicTrackDefs,
				this.gameEndingDefs,
				this.entityStateConfigurations,
				this.expansionDefs,
				this.entitlementDefs,
				this.miscPickupDefs,
				this.entityStateTypes
			};
		}

		// Token: 0x0600531D RID: 21277 RVA: 0x001581E6 File Offset: 0x001563E6
		public bool ValueEquals(ContentPack other)
		{
			return ContentPack.ValueEquals(this, other);
		}

		// Token: 0x0600531E RID: 21278 RVA: 0x001581F0 File Offset: 0x001563F0
		public static void Copy([NotNull] ContentPack src, [NotNull] ContentPack dest)
		{
			ContentPack contentPack = src;
			if (contentPack == null)
			{
				throw new ArgumentNullException("src");
			}
			src = contentPack;
			ContentPack contentPack2 = dest;
			if (contentPack2 == null)
			{
				throw new ArgumentNullException("dest");
			}
			dest = contentPack2;
			dest.identifier = src.identifier;
			src.bodyPrefabs.CopyTo(dest.bodyPrefabs);
			src.masterPrefabs.CopyTo(dest.masterPrefabs);
			src.projectilePrefabs.CopyTo(dest.projectilePrefabs);
			src.gameModePrefabs.CopyTo(dest.gameModePrefabs);
			src.networkedObjectPrefabs.CopyTo(dest.networkedObjectPrefabs);
			src.skillDefs.CopyTo(dest.skillDefs);
			src.skillFamilies.CopyTo(dest.skillFamilies);
			src.sceneDefs.CopyTo(dest.sceneDefs);
			src.itemDefs.CopyTo(dest.itemDefs);
			src.itemTierDefs.CopyTo(dest.itemTierDefs);
			src.itemRelationshipProviders.CopyTo(dest.itemRelationshipProviders);
			src.equipmentDefs.CopyTo(dest.equipmentDefs);
			src.buffDefs.CopyTo(dest.buffDefs);
			src.eliteDefs.CopyTo(dest.eliteDefs);
			src.unlockableDefs.CopyTo(dest.unlockableDefs);
			src.survivorDefs.CopyTo(dest.survivorDefs);
			src.artifactDefs.CopyTo(dest.artifactDefs);
			src.effectDefs.CopyTo(dest.effectDefs);
			src.surfaceDefs.CopyTo(dest.surfaceDefs);
			src.networkSoundEventDefs.CopyTo(dest.networkSoundEventDefs);
			src.musicTrackDefs.CopyTo(dest.musicTrackDefs);
			src.gameEndingDefs.CopyTo(dest.gameEndingDefs);
			src.entityStateConfigurations.CopyTo(dest.entityStateConfigurations);
			src.expansionDefs.CopyTo(dest.expansionDefs);
			src.entitlementDefs.CopyTo(dest.entitlementDefs);
			src.miscPickupDefs.CopyTo(dest.miscPickupDefs);
			src.entityStateTypes.CopyTo(dest.entityStateTypes);
		}

		// Token: 0x0600531F RID: 21279 RVA: 0x001583F8 File Offset: 0x001565F8
		public static bool ValueEquals([CanBeNull] ContentPack a, [CanBeNull] ContentPack b)
		{
			return a == null == (b == null) && (a == null || (a.identifier.Equals(b.identifier, StringComparison.Ordinal) && (a.bodyPrefabs.Equals(b.bodyPrefabs) && a.masterPrefabs.Equals(b.masterPrefabs) && a.projectilePrefabs.Equals(b.projectilePrefabs) && a.gameModePrefabs.Equals(b.gameModePrefabs) && a.networkedObjectPrefabs.Equals(b.networkedObjectPrefabs) && a.skillDefs.Equals(b.skillDefs) && a.skillFamilies.Equals(b.skillFamilies) && a.sceneDefs.Equals(b.sceneDefs) && a.itemDefs.Equals(b.itemDefs) && a.itemTierDefs.Equals(b.itemTierDefs) && a.itemRelationshipProviders.Equals(b.itemRelationshipProviders) && a.equipmentDefs.Equals(b.equipmentDefs) && a.buffDefs.Equals(b.buffDefs) && a.eliteDefs.Equals(b.eliteDefs) && a.unlockableDefs.Equals(b.unlockableDefs) && a.survivorDefs.Equals(b.survivorDefs) && a.artifactDefs.Equals(b.artifactDefs) && a.effectDefs.Equals(b.effectDefs) && a.surfaceDefs.Equals(b.surfaceDefs) && a.networkSoundEventDefs.Equals(b.networkSoundEventDefs) && a.musicTrackDefs.Equals(b.musicTrackDefs) && a.gameEndingDefs.Equals(b.gameEndingDefs) && a.entityStateConfigurations.Equals(b.entityStateConfigurations) && a.entityStateTypes.Equals(b.entityStateTypes) && a.expansionDefs.Equals(b.expansionDefs) && a.entitlementDefs.Equals(b.entitlementDefs)) && a.miscPickupDefs.Equals(b.miscPickupDefs)));
		}

		// Token: 0x06005320 RID: 21280 RVA: 0x0015866C File Offset: 0x0015686C
		private NamedAssetCollection FindAssetCollection(string collectionName)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(collectionName);
			if (num <= 2654186610U)
			{
				if (num <= 995884288U)
				{
					if (num <= 816892640U)
					{
						if (num != 366431354U)
						{
							if (num != 449669619U)
							{
								if (num == 816892640U)
								{
									if (collectionName == "networkSoundEventDefs")
									{
										return this.networkSoundEventDefs;
									}
								}
							}
							else if (collectionName == "gameModePrefabs")
							{
								return this.gameModePrefabs;
							}
						}
						else if (collectionName == "itemDefs")
						{
							return this.itemDefs;
						}
					}
					else if (num != 823068240U)
					{
						if (num != 846662765U)
						{
							if (num == 995884288U)
							{
								if (collectionName == "buffDefs")
								{
									return this.buffDefs;
								}
							}
						}
						else if (collectionName == "unlockableDefs")
						{
							return this.unlockableDefs;
						}
					}
					else if (collectionName == "surfaceDefs")
					{
						return this.surfaceDefs;
					}
				}
				else if (num <= 1922034408U)
				{
					if (num != 1526192451U)
					{
						if (num != 1571265176U)
						{
							if (num == 1922034408U)
							{
								if (collectionName == "eliteDefs")
								{
									return this.eliteDefs;
								}
							}
						}
						else if (collectionName == "gameEndingDefs")
						{
							return this.gameEndingDefs;
						}
					}
					else if (collectionName == "sceneDefs")
					{
						return this.sceneDefs;
					}
				}
				else if (num <= 2006909885U)
				{
					if (num != 1949807200U)
					{
						if (num == 2006909885U)
						{
							if (collectionName == "itemRelationshipTypes")
							{
								return this.itemRelationshipTypes;
							}
						}
					}
					else if (collectionName == "bodyPrefabs")
					{
						return this.bodyPrefabs;
					}
				}
				else if (num != 2595136755U)
				{
					if (num == 2654186610U)
					{
						if (collectionName == "skillFamilies")
						{
							return this.skillFamilies;
						}
					}
				}
				else if (collectionName == "artifactDefs")
				{
					return this.artifactDefs;
				}
			}
			else if (num <= 3728803370U)
			{
				if (num <= 3097615614U)
				{
					if (num != 2831726568U)
					{
						if (num != 2941314017U)
						{
							if (num == 3097615614U)
							{
								if (collectionName == "expansionDefs")
								{
									return this.expansionDefs;
								}
							}
						}
						else if (collectionName == "miscPickupDefs")
						{
							return this.miscPickupDefs;
						}
					}
					else if (collectionName == "effectDefs")
					{
						return this.effectDefs;
					}
				}
				else if (num <= 3289210774U)
				{
					if (num != 3232755834U)
					{
						if (num == 3289210774U)
						{
							if (collectionName == "itemRelationshipProviders")
							{
								return this.itemRelationshipProviders;
							}
						}
					}
					else if (collectionName == "masterPrefabs")
					{
						return this.masterPrefabs;
					}
				}
				else if (num != 3568280791U)
				{
					if (num == 3728803370U)
					{
						if (collectionName == "skillDefs")
						{
							return this.skillDefs;
						}
					}
				}
				else if (collectionName == "survivorDefs")
				{
					return this.survivorDefs;
				}
			}
			else if (num <= 3933408882U)
			{
				if (num != 3772577556U)
				{
					if (num != 3919559852U)
					{
						if (num == 3933408882U)
						{
							if (collectionName == "networkedObjectPrefabs")
							{
								return this.networkedObjectPrefabs;
							}
						}
					}
					else if (collectionName == "entitlementDefs")
					{
						return this.entitlementDefs;
					}
				}
				else if (collectionName == "entityStateConfigurations")
				{
					return this.entityStateConfigurations;
				}
			}
			else if (num <= 4222488475U)
			{
				if (num != 4065175505U)
				{
					if (num == 4222488475U)
					{
						if (collectionName == "musicTrackDefs")
						{
							return this.musicTrackDefs;
						}
					}
				}
				else if (collectionName == "projectilePrefabs")
				{
					return this.projectilePrefabs;
				}
			}
			else if (num != 4260365938U)
			{
				if (num == 4276543023U)
				{
					if (collectionName == "equipmentDefs")
					{
						return this.equipmentDefs;
					}
				}
			}
			else if (collectionName == "itemTierDefs")
			{
				return this.itemTierDefs;
			}
			return null;
		}

		// Token: 0x06005321 RID: 21281 RVA: 0x00158B28 File Offset: 0x00156D28
		public bool FindAsset(string collectionName, string assetName, out object result)
		{
			NamedAssetCollection namedAssetCollection = this.FindAssetCollection(collectionName);
			if (namedAssetCollection != null)
			{
				return namedAssetCollection.Find(assetName, out result);
			}
			result = null;
			return false;
		}

		// Token: 0x04004F4A RID: 20298
		[NotNull]
		private string _identifier = "UNIDENTIFIED";

		// Token: 0x04004F4B RID: 20299
		private static Func<GameObject, string> getGameObjectName = (GameObject obj) => obj.name;

		// Token: 0x04004F4C RID: 20300
		private static Func<Component, string> getComponentName = (Component obj) => obj.gameObject.name;

		// Token: 0x04004F4D RID: 20301
		private static Func<ScriptableObject, string> getScriptableObjectName = (ScriptableObject obj) => obj.name;

		// Token: 0x04004F4E RID: 20302
		private static Func<EffectDef, string> getEffectDefName = (EffectDef obj) => obj.prefabName;

		// Token: 0x04004F4F RID: 20303
		private static Func<Type, string> getTypeName = (Type obj) => obj.FullName;

		// Token: 0x04004F50 RID: 20304
		[NotNull]
		public NamedAssetCollection<GameObject> bodyPrefabs = new NamedAssetCollection<GameObject>(ContentPack.getGameObjectName);

		// Token: 0x04004F51 RID: 20305
		[NotNull]
		public NamedAssetCollection<GameObject> masterPrefabs = new NamedAssetCollection<GameObject>(ContentPack.getGameObjectName);

		// Token: 0x04004F52 RID: 20306
		[NotNull]
		public NamedAssetCollection<GameObject> projectilePrefabs = new NamedAssetCollection<GameObject>(ContentPack.getGameObjectName);

		// Token: 0x04004F53 RID: 20307
		[NotNull]
		public NamedAssetCollection<GameObject> gameModePrefabs = new NamedAssetCollection<GameObject>(ContentPack.getGameObjectName);

		// Token: 0x04004F54 RID: 20308
		[NotNull]
		public NamedAssetCollection<GameObject> networkedObjectPrefabs = new NamedAssetCollection<GameObject>(ContentPack.getGameObjectName);

		// Token: 0x04004F55 RID: 20309
		[NotNull]
		public NamedAssetCollection<SkillDef> skillDefs = new NamedAssetCollection<SkillDef>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F56 RID: 20310
		[NotNull]
		public NamedAssetCollection<SkillFamily> skillFamilies = new NamedAssetCollection<SkillFamily>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F57 RID: 20311
		[NotNull]
		public NamedAssetCollection<SceneDef> sceneDefs = new NamedAssetCollection<SceneDef>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F58 RID: 20312
		[NotNull]
		public NamedAssetCollection<ItemDef> itemDefs = new NamedAssetCollection<ItemDef>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F59 RID: 20313
		[NotNull]
		public NamedAssetCollection<ItemTierDef> itemTierDefs = new NamedAssetCollection<ItemTierDef>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F5A RID: 20314
		[NotNull]
		public NamedAssetCollection<ItemRelationshipProvider> itemRelationshipProviders = new NamedAssetCollection<ItemRelationshipProvider>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F5B RID: 20315
		[NotNull]
		public NamedAssetCollection<ItemRelationshipType> itemRelationshipTypes = new NamedAssetCollection<ItemRelationshipType>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F5C RID: 20316
		[NotNull]
		public NamedAssetCollection<EquipmentDef> equipmentDefs = new NamedAssetCollection<EquipmentDef>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F5D RID: 20317
		[NotNull]
		public NamedAssetCollection<BuffDef> buffDefs = new NamedAssetCollection<BuffDef>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F5E RID: 20318
		[NotNull]
		public NamedAssetCollection<EliteDef> eliteDefs = new NamedAssetCollection<EliteDef>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F5F RID: 20319
		[NotNull]
		public NamedAssetCollection<UnlockableDef> unlockableDefs = new NamedAssetCollection<UnlockableDef>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F60 RID: 20320
		[NotNull]
		public NamedAssetCollection<SurvivorDef> survivorDefs = new NamedAssetCollection<SurvivorDef>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F61 RID: 20321
		[NotNull]
		public NamedAssetCollection<ArtifactDef> artifactDefs = new NamedAssetCollection<ArtifactDef>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F62 RID: 20322
		[NotNull]
		public NamedAssetCollection<EffectDef> effectDefs = new NamedAssetCollection<EffectDef>(ContentPack.getEffectDefName);

		// Token: 0x04004F63 RID: 20323
		[NotNull]
		public NamedAssetCollection<SurfaceDef> surfaceDefs = new NamedAssetCollection<SurfaceDef>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F64 RID: 20324
		[NotNull]
		public NamedAssetCollection<NetworkSoundEventDef> networkSoundEventDefs = new NamedAssetCollection<NetworkSoundEventDef>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F65 RID: 20325
		[NotNull]
		public NamedAssetCollection<MusicTrackDef> musicTrackDefs = new NamedAssetCollection<MusicTrackDef>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F66 RID: 20326
		[NotNull]
		public NamedAssetCollection<GameEndingDef> gameEndingDefs = new NamedAssetCollection<GameEndingDef>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F67 RID: 20327
		[NotNull]
		public NamedAssetCollection<EntityStateConfiguration> entityStateConfigurations = new NamedAssetCollection<EntityStateConfiguration>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F68 RID: 20328
		[NotNull]
		public NamedAssetCollection<ExpansionDef> expansionDefs = new NamedAssetCollection<ExpansionDef>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F69 RID: 20329
		[NotNull]
		public NamedAssetCollection<EntitlementDef> entitlementDefs = new NamedAssetCollection<EntitlementDef>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F6A RID: 20330
		[NotNull]
		public NamedAssetCollection<MiscPickupDef> miscPickupDefs = new NamedAssetCollection<MiscPickupDef>(ContentPack.getScriptableObjectName);

		// Token: 0x04004F6B RID: 20331
		[NotNull]
		public NamedAssetCollection<Type> entityStateTypes = new NamedAssetCollection<Type>(ContentPack.getTypeName);

		// Token: 0x04004F6C RID: 20332
		private object[] assetCollections;
	}
}
