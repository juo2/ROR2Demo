using System;
using System.Collections;
using RoR2.ContentManagement;

namespace RoR2
{
	// Token: 0x0200056E RID: 1390
	public class DLC1Content : IContentPackProvider
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600191D RID: 6429 RVA: 0x0006CA5D File Offset: 0x0006AC5D
		public string identifier
		{
			get
			{
				return "RoR2.DLC1";
			}
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x0006CA64 File Offset: 0x0006AC64
		public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
		{
			this.contentPack.identifier = this.identifier;
			AddressablesLoadHelper loadHelper = AddressablesLoadHelper.CreateUsingDefaultResourceLocator(DLC1Content.addressablesLabel);
			loadHelper.AddContentPackLoadOperation(this.contentPack);
			loadHelper.AddGenericOperation(delegate()
			{
				ContentLoadHelper.PopulateTypeFields<ItemDef>(typeof(DLC1Content.Items), this.contentPack.itemDefs, null);
				ContentLoadHelper.PopulateTypeFields<ItemRelationshipType>(typeof(DLC1Content.ItemRelationshipTypes), this.contentPack.itemRelationshipTypes, null);
				ContentLoadHelper.PopulateTypeFields<EquipmentDef>(typeof(DLC1Content.Equipment), this.contentPack.equipmentDefs, null);
				ContentLoadHelper.PopulateTypeFields<BuffDef>(typeof(DLC1Content.Buffs), this.contentPack.buffDefs, (string fieldName) => "bd" + fieldName);
				ContentLoadHelper.PopulateTypeFields<EliteDef>(typeof(DLC1Content.Elites), this.contentPack.eliteDefs, (string fieldName) => "ed" + fieldName);
				ContentLoadHelper.PopulateTypeFields<SurvivorDef>(typeof(DLC1Content.Survivors), this.contentPack.survivorDefs, null);
				ContentLoadHelper.PopulateTypeFields<GameEndingDef>(typeof(DLC1Content.GameEndings), this.contentPack.gameEndingDefs, null);
				ContentLoadHelper.PopulateTypeFields<MiscPickupDef>(typeof(DLC1Content.MiscPickups), this.contentPack.miscPickupDefs, null);
			}, 0.05f);
			while (loadHelper.coroutine.MoveNext())
			{
				args.ReportProgress(loadHelper.progress.value);
				yield return loadHelper.coroutine.Current;
			}
			yield break;
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x0006CA7A File Offset: 0x0006AC7A
		public IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
		{
			ContentPack.Copy(this.contentPack, args.output);
			yield break;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0006CA90 File Offset: 0x0006AC90
		public IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
		{
			yield break;
		}

		// Token: 0x04001EF3 RID: 7923
		private ContentPack contentPack = new ContentPack();

		// Token: 0x04001EF4 RID: 7924
		private static readonly string addressablesLabel = "ContentPack:RoR2.DLC1";

		// Token: 0x0200056F RID: 1391
		public static class Items
		{
			// Token: 0x04001EF5 RID: 7925
			public static ItemDef MoveSpeedOnKill;

			// Token: 0x04001EF6 RID: 7926
			public static ItemDef HealingPotion;

			// Token: 0x04001EF7 RID: 7927
			public static ItemDef HealingPotionConsumed;

			// Token: 0x04001EF8 RID: 7928
			public static ItemDef PermanentDebuffOnHit;

			// Token: 0x04001EF9 RID: 7929
			public static ItemDef AttackSpeedAndMoveSpeed;

			// Token: 0x04001EFA RID: 7930
			public static ItemDef CritDamage;

			// Token: 0x04001EFB RID: 7931
			public static ItemDef BearVoid;

			// Token: 0x04001EFC RID: 7932
			public static ItemDef MushroomVoid;

			// Token: 0x04001EFD RID: 7933
			public static ItemDef CloverVoid;

			// Token: 0x04001EFE RID: 7934
			public static ItemDef StrengthenBurn;

			// Token: 0x04001EFF RID: 7935
			public static ItemDef GummyCloneIdentifier;

			// Token: 0x04001F00 RID: 7936
			public static ItemDef RegeneratingScrap;

			// Token: 0x04001F01 RID: 7937
			public static ItemDef RegeneratingScrapConsumed;

			// Token: 0x04001F02 RID: 7938
			public static ItemDef BleedOnHitVoid;

			// Token: 0x04001F03 RID: 7939
			public static ItemDef CritGlassesVoid;

			// Token: 0x04001F04 RID: 7940
			public static ItemDef TreasureCacheVoid;

			// Token: 0x04001F05 RID: 7941
			public static ItemDef SlowOnHitVoid;

			// Token: 0x04001F06 RID: 7942
			public static ItemDef MissileVoid;

			// Token: 0x04001F07 RID: 7943
			public static ItemDef ChainLightningVoid;

			// Token: 0x04001F08 RID: 7944
			public static ItemDef ExtraLifeVoid;

			// Token: 0x04001F09 RID: 7945
			public static ItemDef ExtraLifeVoidConsumed;

			// Token: 0x04001F0A RID: 7946
			public static ItemDef EquipmentMagazineVoid;

			// Token: 0x04001F0B RID: 7947
			public static ItemDef ExplodeOnDeathVoid;

			// Token: 0x04001F0C RID: 7948
			public static ItemDef FragileDamageBonus;

			// Token: 0x04001F0D RID: 7949
			public static ItemDef FragileDamageBonusConsumed;

			// Token: 0x04001F0E RID: 7950
			public static ItemDef OutOfCombatArmor;

			// Token: 0x04001F0F RID: 7951
			public static ItemDef ScrapWhiteSuppressed;

			// Token: 0x04001F10 RID: 7952
			public static ItemDef ScrapGreenSuppressed;

			// Token: 0x04001F11 RID: 7953
			public static ItemDef ScrapRedSuppressed;

			// Token: 0x04001F12 RID: 7954
			public static ItemDef MoreMissile;

			// Token: 0x04001F13 RID: 7955
			public static ItemDef ImmuneToDebuff;

			// Token: 0x04001F14 RID: 7956
			public static ItemDef RandomEquipmentTrigger;

			// Token: 0x04001F15 RID: 7957
			public static ItemDef PrimarySkillShuriken;

			// Token: 0x04001F16 RID: 7958
			public static ItemDef RandomlyLunar;

			// Token: 0x04001F17 RID: 7959
			public static ItemDef GoldOnHurt;

			// Token: 0x04001F18 RID: 7960
			public static ItemDef HalfAttackSpeedHalfCooldowns;

			// Token: 0x04001F19 RID: 7961
			public static ItemDef HalfSpeedDoubleHealth;

			// Token: 0x04001F1A RID: 7962
			public static ItemDef FreeChest;

			// Token: 0x04001F1B RID: 7963
			public static ItemDef ConvertCritChanceToCritDamage;

			// Token: 0x04001F1C RID: 7964
			public static ItemDef ElementalRingVoid;

			// Token: 0x04001F1D RID: 7965
			public static ItemDef LunarSun;

			// Token: 0x04001F1E RID: 7966
			public static ItemDef DroneWeapons;

			// Token: 0x04001F1F RID: 7967
			public static ItemDef DroneWeaponsBoost;

			// Token: 0x04001F20 RID: 7968
			public static ItemDef DroneWeaponsDisplay1;

			// Token: 0x04001F21 RID: 7969
			public static ItemDef DroneWeaponsDisplay2;

			// Token: 0x04001F22 RID: 7970
			public static ItemDef VoidmanPassiveItem;

			// Token: 0x04001F23 RID: 7971
			public static ItemDef MinorConstructOnKill;

			// Token: 0x04001F24 RID: 7972
			public static ItemDef VoidMegaCrabItem;
		}

		// Token: 0x02000570 RID: 1392
		public static class ItemRelationshipTypes
		{
			// Token: 0x04001F25 RID: 7973
			public static ItemRelationshipType ContagiousItem;
		}

		// Token: 0x02000571 RID: 1393
		public static class Equipment
		{
			// Token: 0x04001F26 RID: 7974
			public static EquipmentDef Molotov;

			// Token: 0x04001F27 RID: 7975
			public static EquipmentDef VendingMachine;

			// Token: 0x04001F28 RID: 7976
			public static EquipmentDef BossHunter;

			// Token: 0x04001F29 RID: 7977
			public static EquipmentDef BossHunterConsumed;

			// Token: 0x04001F2A RID: 7978
			public static EquipmentDef GummyClone;

			// Token: 0x04001F2B RID: 7979
			public static EquipmentDef MultiShopCard;

			// Token: 0x04001F2C RID: 7980
			public static EquipmentDef LunarPortalOnUse;

			// Token: 0x04001F2D RID: 7981
			public static EquipmentDef EliteVoidEquipment;
		}

		// Token: 0x02000572 RID: 1394
		public static class Buffs
		{
			// Token: 0x04001F2E RID: 7982
			public static BuffDef ElementalRingVoidReady;

			// Token: 0x04001F2F RID: 7983
			public static BuffDef ElementalRingVoidCooldown;

			// Token: 0x04001F30 RID: 7984
			public static BuffDef BearVoidReady;

			// Token: 0x04001F31 RID: 7985
			public static BuffDef BearVoidCooldown;

			// Token: 0x04001F32 RID: 7986
			public static BuffDef KillMoveSpeed;

			// Token: 0x04001F33 RID: 7987
			public static BuffDef PermanentDebuff;

			// Token: 0x04001F34 RID: 7988
			public static BuffDef MushroomVoidActive;

			// Token: 0x04001F35 RID: 7989
			public static BuffDef StrongerBurn;

			// Token: 0x04001F36 RID: 7990
			public static BuffDef Fracture;

			// Token: 0x04001F37 RID: 7991
			public static BuffDef OutOfCombatArmorBuff;

			// Token: 0x04001F38 RID: 7992
			public static BuffDef PrimarySkillShurikenBuff;

			// Token: 0x04001F39 RID: 7993
			public static BuffDef Blinded;

			// Token: 0x04001F3A RID: 7994
			public static BuffDef EliteEarth;

			// Token: 0x04001F3B RID: 7995
			public static BuffDef EliteVoid;

			// Token: 0x04001F3C RID: 7996
			public static BuffDef JailerTether;

			// Token: 0x04001F3D RID: 7997
			public static BuffDef JailerSlow;

			// Token: 0x04001F3E RID: 7998
			public static BuffDef VoidRaidCrabWardWipeFog;

			// Token: 0x04001F3F RID: 7999
			public static BuffDef VoidSurvivorCorruptMode;

			// Token: 0x04001F40 RID: 8000
			public static BuffDef ImmuneToDebuffReady;

			// Token: 0x04001F41 RID: 8001
			public static BuffDef ImmuneToDebuffCooldown;
		}

		// Token: 0x02000573 RID: 1395
		public static class Elites
		{
			// Token: 0x04001F42 RID: 8002
			public static EliteDef Earth;

			// Token: 0x04001F43 RID: 8003
			public static EliteDef EarthHonor;

			// Token: 0x04001F44 RID: 8004
			public static EliteDef Void;
		}

		// Token: 0x02000574 RID: 1396
		public static class GameEndings
		{
			// Token: 0x04001F45 RID: 8005
			public static GameEndingDef VoidEnding;
		}

		// Token: 0x02000575 RID: 1397
		public static class Survivors
		{
			// Token: 0x04001F46 RID: 8006
			public static SurvivorDef Railgunner;
		}

		// Token: 0x02000576 RID: 1398
		public static class MiscPickups
		{
			// Token: 0x04001F47 RID: 8007
			public static MiscPickupDef VoidCoin;
		}
	}
}
