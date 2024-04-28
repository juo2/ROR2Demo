using System;
using System.Collections;
using RoR2.ContentManagement;

namespace RoR2
{
	// Token: 0x020004AF RID: 1199
	public class JunkContent : IContentPackProvider
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x00060888 File Offset: 0x0005EA88
		public string identifier
		{
			get
			{
				return "RoR2.Junk";
			}
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x0006088F File Offset: 0x0005EA8F
		public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
		{
			this.contentPack.identifier = this.identifier;
			AddressablesLoadHelper loadHelper = AddressablesLoadHelper.CreateUsingDefaultResourceLocator("ContentPack:RoR2.Junk");
			loadHelper.AddContentPackLoadOperation(this.contentPack);
			loadHelper.AddGenericOperation(delegate()
			{
				ContentLoadHelper.PopulateTypeFields<ItemDef>(typeof(JunkContent.Items), this.contentPack.itemDefs, null);
				ContentLoadHelper.PopulateTypeFields<EquipmentDef>(typeof(JunkContent.Equipment), this.contentPack.equipmentDefs, null);
				ContentLoadHelper.PopulateTypeFields<BuffDef>(typeof(JunkContent.Buffs), this.contentPack.buffDefs, (string fieldName) => "bd" + fieldName);
				ContentLoadHelper.PopulateTypeFields<EliteDef>(typeof(JunkContent.Elites), this.contentPack.eliteDefs, (string fieldName) => "ed" + fieldName);
			}, 0.05f);
			while (loadHelper.coroutine.MoveNext())
			{
				args.ReportProgress(loadHelper.progress.value);
				yield return loadHelper.coroutine.Current;
			}
			yield break;
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x000608A5 File Offset: 0x0005EAA5
		public IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
		{
			ContentPack.Copy(this.contentPack, args.output);
			yield break;
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x000608BB File Offset: 0x0005EABB
		public IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
		{
			yield break;
		}

		// Token: 0x04001B7F RID: 7039
		private ContentPack contentPack = new ContentPack();

		// Token: 0x020004B0 RID: 1200
		public static class Items
		{
			// Token: 0x04001B80 RID: 7040
			public static ItemDef AACannon;

			// Token: 0x04001B81 RID: 7041
			public static ItemDef PlasmaCore;

			// Token: 0x04001B82 RID: 7042
			public static ItemDef CooldownOnCrit;

			// Token: 0x04001B83 RID: 7043
			public static ItemDef TempestOnKill;

			// Token: 0x04001B84 RID: 7044
			public static ItemDef WarCryOnCombat;

			// Token: 0x04001B85 RID: 7045
			public static ItemDef PlantOnHit;

			// Token: 0x04001B86 RID: 7046
			public static ItemDef MageAttunement;

			// Token: 0x04001B87 RID: 7047
			public static ItemDef BurnNearby;

			// Token: 0x04001B88 RID: 7048
			public static ItemDef CritHeal;

			// Token: 0x04001B89 RID: 7049
			public static ItemDef Incubator;

			// Token: 0x04001B8A RID: 7050
			public static ItemDef SkullCounter;
		}

		// Token: 0x020004B1 RID: 1201
		public static class Equipment
		{
			// Token: 0x04001B8B RID: 7051
			public static EquipmentDef SoulJar;

			// Token: 0x04001B8C RID: 7052
			public static EquipmentDef EliteYellowEquipment;

			// Token: 0x04001B8D RID: 7053
			public static EquipmentDef EliteGoldEquipment;

			// Token: 0x04001B8E RID: 7054
			public static EquipmentDef GhostGun;

			// Token: 0x04001B8F RID: 7055
			public static EquipmentDef OrbitalLaser;

			// Token: 0x04001B90 RID: 7056
			public static EquipmentDef Enigma;

			// Token: 0x04001B91 RID: 7057
			public static EquipmentDef SoulCorruptor;
		}

		// Token: 0x020004B2 RID: 1202
		public static class Buffs
		{
			// Token: 0x04001B92 RID: 7058
			public static BuffDef EnrageAncientWisp;

			// Token: 0x04001B93 RID: 7059
			public static BuffDef LightningShield;

			// Token: 0x04001B94 RID: 7060
			public static BuffDef Slow30;

			// Token: 0x04001B95 RID: 7061
			public static BuffDef EngiTeamShield;

			// Token: 0x04001B96 RID: 7062
			public static BuffDef GoldEmpowered;

			// Token: 0x04001B97 RID: 7063
			public static BuffDef LoaderOvercharged;

			// Token: 0x04001B98 RID: 7064
			public static BuffDef LoaderPylonPowered;

			// Token: 0x04001B99 RID: 7065
			public static BuffDef Deafened;

			// Token: 0x04001B9A RID: 7066
			public static BuffDef MeatRegenBoost;

			// Token: 0x04001B9B RID: 7067
			public static BuffDef BodyArmor;
		}

		// Token: 0x020004B3 RID: 1203
		public static class Elites
		{
			// Token: 0x04001B9C RID: 7068
			public static EliteDef Gold;
		}
	}
}
