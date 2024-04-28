using System;
using System.Collections.Generic;
using RoR2.EntitlementManagement;
using RoR2.ExpansionManagement;
using RoR2.Projectile;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.ContentManagement
{
	// Token: 0x02000DFF RID: 3583
	public static class AddressablesLabels
	{
		// Token: 0x06005234 RID: 21044 RVA: 0x00154F6C File Offset: 0x0015316C
		// Note: this type is marked as 'beforefieldinit'.
		static AddressablesLabels()
		{
			Dictionary<Type, string> dictionary = new Dictionary<Type, string>();
			Type typeFromHandle = typeof(CharacterBody);
			dictionary[typeFromHandle] = AddressablesLabels.characterBody;
			Type typeFromHandle2 = typeof(CharacterMaster);
			dictionary[typeFromHandle2] = AddressablesLabels.characterMaster;
			Type typeFromHandle3 = typeof(ProjectileController);
			dictionary[typeFromHandle3] = AddressablesLabels.projectile;
			Type typeFromHandle4 = typeof(Run);
			dictionary[typeFromHandle4] = AddressablesLabels.gameMode;
			Type typeFromHandle5 = typeof(NetworkIdentity);
			dictionary[typeFromHandle5] = AddressablesLabels.networkedObject;
			Type typeFromHandle6 = typeof(EffectComponent);
			dictionary[typeFromHandle6] = AddressablesLabels.effect;
			AddressablesLabels.componentTypeLabels = dictionary;
			Dictionary<Type, string> dictionary2 = new Dictionary<Type, string>();
			typeFromHandle6 = typeof(SkillFamily);
			dictionary2[typeFromHandle6] = AddressablesLabels.skillFamily;
			typeFromHandle5 = typeof(SkillDef);
			dictionary2[typeFromHandle5] = AddressablesLabels.skillDef;
			typeFromHandle4 = typeof(UnlockableDef);
			dictionary2[typeFromHandle4] = AddressablesLabels.unlockableDef;
			typeFromHandle3 = typeof(SurfaceDef);
			dictionary2[typeFromHandle3] = AddressablesLabels.surfaceDef;
			typeFromHandle2 = typeof(SceneDef);
			dictionary2[typeFromHandle2] = AddressablesLabels.sceneDef;
			typeFromHandle = typeof(NetworkSoundEventDef);
			dictionary2[typeFromHandle] = AddressablesLabels.networkSoundEventDef;
			Type typeFromHandle7 = typeof(MusicTrackDef);
			dictionary2[typeFromHandle7] = AddressablesLabels.musicTrackDef;
			Type typeFromHandle8 = typeof(GameEndingDef);
			dictionary2[typeFromHandle8] = AddressablesLabels.gameEndingDef;
			Type typeFromHandle9 = typeof(ItemDef);
			dictionary2[typeFromHandle9] = AddressablesLabels.itemDef;
			Type typeFromHandle10 = typeof(ItemTierDef);
			dictionary2[typeFromHandle10] = AddressablesLabels.itemTierDef;
			Type typeFromHandle11 = typeof(EquipmentDef);
			dictionary2[typeFromHandle11] = AddressablesLabels.equipmentDef;
			Type typeFromHandle12 = typeof(BuffDef);
			dictionary2[typeFromHandle12] = AddressablesLabels.buffDef;
			Type typeFromHandle13 = typeof(EliteDef);
			dictionary2[typeFromHandle13] = AddressablesLabels.eliteDef;
			Type typeFromHandle14 = typeof(SurvivorDef);
			dictionary2[typeFromHandle14] = AddressablesLabels.survivorDef;
			Type typeFromHandle15 = typeof(ArtifactDef);
			dictionary2[typeFromHandle15] = AddressablesLabels.artifactDef;
			Type typeFromHandle16 = typeof(EntityStateConfiguration);
			dictionary2[typeFromHandle16] = AddressablesLabels.entityStateConfiguration;
			Type typeFromHandle17 = typeof(ExpansionDef);
			dictionary2[typeFromHandle17] = AddressablesLabels.expansionDef;
			Type typeFromHandle18 = typeof(EntitlementDef);
			dictionary2[typeFromHandle18] = AddressablesLabels.entitlementDef;
			Type typeFromHandle19 = typeof(Shader);
			dictionary2[typeFromHandle19] = AddressablesLabels.shader;
			Type typeFromHandle20 = typeof(ItemRelationshipProvider);
			dictionary2[typeFromHandle20] = AddressablesLabels.itemRelationshipProvider;
			Type typeFromHandle21 = typeof(ItemRelationshipType);
			dictionary2[typeFromHandle21] = AddressablesLabels.itemRelationshipType;
			Type typeFromHandle22 = typeof(MiscPickupDef);
			dictionary2[typeFromHandle22] = AddressablesLabels.miscPickupDef;
			AddressablesLabels.assetTypeLabels = dictionary2;
		}

		// Token: 0x04004E74 RID: 20084
		public static readonly string characterBody = "CharacterBody";

		// Token: 0x04004E75 RID: 20085
		public static readonly string characterMaster = "CharacterMaster";

		// Token: 0x04004E76 RID: 20086
		public static readonly string projectile = "Projectile";

		// Token: 0x04004E77 RID: 20087
		public static readonly string gameMode = "GameMode";

		// Token: 0x04004E78 RID: 20088
		public static readonly string networkedObject = "NetworkedObject";

		// Token: 0x04004E79 RID: 20089
		public static readonly string effect = "Effect";

		// Token: 0x04004E7A RID: 20090
		public static readonly string skillFamily = "SkillFamily";

		// Token: 0x04004E7B RID: 20091
		public static readonly string skillDef = "SkillDef";

		// Token: 0x04004E7C RID: 20092
		public static readonly string unlockableDef = "UnlockableDef";

		// Token: 0x04004E7D RID: 20093
		public static readonly string surfaceDef = "SurfaceDef";

		// Token: 0x04004E7E RID: 20094
		public static readonly string sceneDef = "SceneDef";

		// Token: 0x04004E7F RID: 20095
		public static readonly string networkSoundEventDef = "NetworkSoundEventDef";

		// Token: 0x04004E80 RID: 20096
		public static readonly string musicTrackDef = "MusicTrackDef";

		// Token: 0x04004E81 RID: 20097
		public static readonly string gameEndingDef = "GameEndingDef";

		// Token: 0x04004E82 RID: 20098
		public static readonly string itemDef = "ItemDef";

		// Token: 0x04004E83 RID: 20099
		public static readonly string itemTierDef = "ItemTierDef";

		// Token: 0x04004E84 RID: 20100
		public static readonly string equipmentDef = "EquipmentDef";

		// Token: 0x04004E85 RID: 20101
		public static readonly string buffDef = "BuffDef";

		// Token: 0x04004E86 RID: 20102
		public static readonly string eliteDef = "EliteDef";

		// Token: 0x04004E87 RID: 20103
		public static readonly string survivorDef = "SurvivorDef";

		// Token: 0x04004E88 RID: 20104
		public static readonly string artifactDef = "ArtifactDef";

		// Token: 0x04004E89 RID: 20105
		public static readonly string entityStateConfiguration = "EntityStateConfiguration";

		// Token: 0x04004E8A RID: 20106
		public static readonly string expansionDef = "ExpansionDef";

		// Token: 0x04004E8B RID: 20107
		public static readonly string entitlementDef = "EntitlementDef";

		// Token: 0x04004E8C RID: 20108
		public static readonly string shader = "Shader";

		// Token: 0x04004E8D RID: 20109
		public static readonly string itemRelationshipType = "ItemRelationshipType";

		// Token: 0x04004E8E RID: 20110
		public static readonly string itemRelationshipProvider = "ItemRelationshipProvider";

		// Token: 0x04004E8F RID: 20111
		public static readonly string miscPickupDef = "MiscPickupDef";

		// Token: 0x04004E90 RID: 20112
		public static readonly IReadOnlyDictionary<Type, string> componentTypeLabels;

		// Token: 0x04004E91 RID: 20113
		public static readonly IReadOnlyDictionary<Type, string> assetTypeLabels;
	}
}
