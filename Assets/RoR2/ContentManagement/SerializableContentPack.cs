using System;
using System.Collections.Generic;
using System.Linq;
using EntityStates;
using RoR2.Skills;
using UnityEngine;

namespace RoR2.ContentManagement
{
	// Token: 0x02000E35 RID: 3637
	[CreateAssetMenu(menuName = "RoR2/SerializableContentPack")]
	public class SerializableContentPack : ScriptableObject
	{
		// Token: 0x06005382 RID: 21378 RVA: 0x00159358 File Offset: 0x00157558
		public ContentPack CreateV1_1_1_4_ContentPack()
		{
			ContentPack contentPack = new ContentPack();
			contentPack.bodyPrefabs.Add(this.bodyPrefabs);
			contentPack.masterPrefabs.Add(this.masterPrefabs);
			contentPack.projectilePrefabs.Add(this.projectilePrefabs);
			contentPack.gameModePrefabs.Add(this.gameModePrefabs);
			contentPack.networkedObjectPrefabs.Add(this.networkedObjectPrefabs);
			contentPack.skillDefs.Add(this.skillDefs);
			contentPack.skillFamilies.Add(this.skillFamilies);
			contentPack.sceneDefs.Add(this.sceneDefs);
			contentPack.itemDefs.Add(this.itemDefs);
			contentPack.equipmentDefs.Add(this.equipmentDefs);
			contentPack.buffDefs.Add(this.buffDefs);
			contentPack.eliteDefs.Add(this.eliteDefs);
			contentPack.unlockableDefs.Add(this.unlockableDefs);
			contentPack.survivorDefs.Add(this.survivorDefs);
			contentPack.artifactDefs.Add(this.artifactDefs);
			contentPack.effectDefs.Add((from asset in this.effectDefs
			select new EffectDef(asset)).ToArray<EffectDef>());
			contentPack.surfaceDefs.Add(this.surfaceDefs);
			contentPack.networkSoundEventDefs.Add(this.networkSoundEventDefs);
			contentPack.musicTrackDefs.Add(this.musicTrackDefs);
			contentPack.gameEndingDefs.Add(this.gameEndingDefs);
			contentPack.entityStateConfigurations.Add(this.entityStateConfigurations);
			List<Type> list = new List<Type>();
			for (int i = 0; i < this.entityStateTypes.Length; i++)
			{
				Type stateType = this.entityStateTypes[i].stateType;
				if (stateType != null)
				{
					list.Add(stateType);
				}
				else
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"SerializableContentPack \"",
						base.name,
						"\" could not resolve type with name \"",
						this.entityStateTypes[i].typeName,
						"\". The type will not be available in the content pack."
					}));
				}
			}
			contentPack.entityStateTypes.Add(list.ToArray());
			return contentPack;
		}

		// Token: 0x06005383 RID: 21379 RVA: 0x0015958D File Offset: 0x0015778D
		public virtual ContentPack CreateContentPack()
		{
			return this.CreateV1_1_1_4_ContentPack();
		}

		// Token: 0x04004F86 RID: 20358
		public GameObject[] bodyPrefabs = Array.Empty<GameObject>();

		// Token: 0x04004F87 RID: 20359
		public GameObject[] masterPrefabs = Array.Empty<GameObject>();

		// Token: 0x04004F88 RID: 20360
		public GameObject[] projectilePrefabs = Array.Empty<GameObject>();

		// Token: 0x04004F89 RID: 20361
		public GameObject[] gameModePrefabs = Array.Empty<GameObject>();

		// Token: 0x04004F8A RID: 20362
		public GameObject[] networkedObjectPrefabs = Array.Empty<GameObject>();

		// Token: 0x04004F8B RID: 20363
		public SkillDef[] skillDefs = Array.Empty<SkillDef>();

		// Token: 0x04004F8C RID: 20364
		public SkillFamily[] skillFamilies = Array.Empty<SkillFamily>();

		// Token: 0x04004F8D RID: 20365
		public SceneDef[] sceneDefs = Array.Empty<SceneDef>();

		// Token: 0x04004F8E RID: 20366
		public ItemDef[] itemDefs = Array.Empty<ItemDef>();

		// Token: 0x04004F8F RID: 20367
		public EquipmentDef[] equipmentDefs = Array.Empty<EquipmentDef>();

		// Token: 0x04004F90 RID: 20368
		public BuffDef[] buffDefs = Array.Empty<BuffDef>();

		// Token: 0x04004F91 RID: 20369
		public EliteDef[] eliteDefs = Array.Empty<EliteDef>();

		// Token: 0x04004F92 RID: 20370
		public UnlockableDef[] unlockableDefs = Array.Empty<UnlockableDef>();

		// Token: 0x04004F93 RID: 20371
		public SurvivorDef[] survivorDefs = Array.Empty<SurvivorDef>();

		// Token: 0x04004F94 RID: 20372
		public ArtifactDef[] artifactDefs = Array.Empty<ArtifactDef>();

		// Token: 0x04004F95 RID: 20373
		public GameObject[] effectDefs = Array.Empty<GameObject>();

		// Token: 0x04004F96 RID: 20374
		public SurfaceDef[] surfaceDefs = Array.Empty<SurfaceDef>();

		// Token: 0x04004F97 RID: 20375
		public NetworkSoundEventDef[] networkSoundEventDefs = Array.Empty<NetworkSoundEventDef>();

		// Token: 0x04004F98 RID: 20376
		public MusicTrackDef[] musicTrackDefs = Array.Empty<MusicTrackDef>();

		// Token: 0x04004F99 RID: 20377
		public GameEndingDef[] gameEndingDefs = Array.Empty<GameEndingDef>();

		// Token: 0x04004F9A RID: 20378
		public EntityStateConfiguration[] entityStateConfigurations = Array.Empty<EntityStateConfiguration>();

		// Token: 0x04004F9B RID: 20379
		public SerializableEntityStateType[] entityStateTypes = Array.Empty<SerializableEntityStateType>();
	}
}
