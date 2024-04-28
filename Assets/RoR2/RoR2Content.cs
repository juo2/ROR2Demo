using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using HG;
using RoR2.ContentManagement;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000A05 RID: 2565
	public class RoR2Content : IContentPackProvider
	{
		// Token: 0x06003B3D RID: 15165 RVA: 0x000F5879 File Offset: 0x000F3A79
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			RoR2Content.mixEnemyMonsterCards = LegacyResourcesAPI.Load<DirectorCardCategorySelection>("DirectorCardCategorySelections/dccsMixEnemy");
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06003B3F RID: 15167 RVA: 0x000F58A8 File Offset: 0x000F3AA8
		public string identifier
		{
			get
			{
				return "RoR2.BaseContent";
			}
		}

		// Token: 0x06003B40 RID: 15168 RVA: 0x000F58AF File Offset: 0x000F3AAF
		public IEnumerator LoadStaticContentAsync(LoadStaticContentAsyncArgs args)
		{
			this.contentPack.identifier = this.identifier;
			AddressablesLoadHelper loadHelper = AddressablesLoadHelper.CreateUsingDefaultResourceLocator("ContentPack:RoR2.BaseContent");
			loadHelper.AddContentPackLoadOperation(this.contentPack);
			loadHelper.AddGenericOperation(delegate()
			{
				ContentLoadHelper.PopulateTypeFields<ArtifactDef>(typeof(RoR2Content.Artifacts), this.contentPack.artifactDefs, null);
				ContentLoadHelper.PopulateTypeFields<ItemDef>(typeof(RoR2Content.Items), this.contentPack.itemDefs, null);
				ContentLoadHelper.PopulateTypeFields<EquipmentDef>(typeof(RoR2Content.Equipment), this.contentPack.equipmentDefs, null);
				ContentLoadHelper.PopulateTypeFields<BuffDef>(typeof(RoR2Content.Buffs), this.contentPack.buffDefs, (string fieldName) => "bd" + fieldName);
				ContentLoadHelper.PopulateTypeFields<EliteDef>(typeof(RoR2Content.Elites), this.contentPack.eliteDefs, (string fieldName) => "ed" + fieldName);
				ContentLoadHelper.PopulateTypeFields<GameEndingDef>(typeof(RoR2Content.GameEndings), this.contentPack.gameEndingDefs, null);
				ContentLoadHelper.PopulateTypeFields<SurvivorDef>(typeof(RoR2Content.Survivors), this.contentPack.survivorDefs, null);
				ContentLoadHelper.PopulateTypeFields<MiscPickupDef>(typeof(RoR2Content.MiscPickups), this.contentPack.miscPickupDefs, null);
			}, 0.04f);
			loadHelper.AddGenericOperation(delegate()
			{
				this.contentPack.effectDefs.Find("CoinEmitter").cullMethod = ((EffectData effectData) => SettingsConVars.cvExpAndMoneyEffects.value);
			}, 0.01f);
			while (loadHelper.coroutine.MoveNext())
			{
				args.ReportProgress(loadHelper.progress.value);
				yield return loadHelper.coroutine.Current;
			}
			yield break;
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x000F58C5 File Offset: 0x000F3AC5
		public IEnumerator GenerateContentPackAsync(GetContentPackAsyncArgs args)
		{
			ContentPack.Copy(this.contentPack, args.output);
			int minEclipseLevel = 1;
			int maxEclipseLevel = 8;
			List<UnlockableDef> list = new List<UnlockableDef>();
			foreach (ContentPackLoadInfo contentPackLoadInfo in args.peerLoadInfos)
			{
				ReadOnlyNamedAssetCollection<SurvivorDef> survivorDefs = contentPackLoadInfo.previousContentPack.survivorDefs;
				int length = args.output.unlockableDefs.Length;
				foreach (SurvivorDef survivorDef in survivorDefs)
				{
					UnlockableDef[] array;
					if (!this.eclipseUnlockableCache.TryGetValue(survivorDef, out array))
					{
						array = RoR2Content.CreateEclipseUnlockablesForSurvivor(survivorDef, minEclipseLevel, maxEclipseLevel);
						this.eclipseUnlockableCache[survivorDef] = array;
					}
					foreach (UnlockableDef item in array)
					{
						list.Add(item);
					}
				}
			}
			args.output.unlockableDefs.Add(list.ToArray());
			args.ReportProgress(1f);
			yield break;
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x000F58DB File Offset: 0x000F3ADB
		public IEnumerator FinalizeAsync(FinalizeAsyncArgs args)
		{
			this.eclipseUnlockableCache.Clear();
			args.ReportProgress(1f);
			yield break;
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x000F58F4 File Offset: 0x000F3AF4
		private static UnlockableDef[] CreateEclipseUnlockablesForSurvivor(SurvivorDef survivorDef, int minEclipseLevel, int maxEclipseLevel)
		{
			UnlockableDef[] array = new UnlockableDef[maxEclipseLevel - minEclipseLevel + 1];
			for (int i = minEclipseLevel + 1; i <= maxEclipseLevel + 1; i++)
			{
				array[i - (minEclipseLevel + 1)] = RoR2Content.CreateEclipseUnlockableForSurvivor(survivorDef, i);
			}
			return array;
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x000F592C File Offset: 0x000F3B2C
		private static UnlockableDef CreateEclipseUnlockableForSurvivor(SurvivorDef survivorDef, int eclipseLevel)
		{
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			string cachedName = stringBuilder.Clear().Append("Eclipse.").Append(survivorDef.cachedName).Append(".").AppendInt(eclipseLevel, 1U, uint.MaxValue).ToString();
			UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
			unlockableDef.cachedName = cachedName;
			unlockableDef.hidden = true;
			HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
			return unlockableDef;
		}

		// Token: 0x04003A02 RID: 14850
		public static DirectorCardCategorySelection mixEnemyMonsterCards;

		// Token: 0x04003A03 RID: 14851
		private ContentPack contentPack = new ContentPack();

		// Token: 0x04003A04 RID: 14852
		private Dictionary<SurvivorDef, UnlockableDef[]> eclipseUnlockableCache = new Dictionary<SurvivorDef, UnlockableDef[]>();

		// Token: 0x02000A06 RID: 2566
		public static class Artifacts
		{
			// Token: 0x17000589 RID: 1417
			// (get) Token: 0x06003B47 RID: 15175 RVA: 0x000F5AE8 File Offset: 0x000F3CE8
			public static ArtifactDef glassArtifactDef
			{
				get
				{
					return RoR2Content.Artifacts.Glass;
				}
			}

			// Token: 0x1700058A RID: 1418
			// (get) Token: 0x06003B48 RID: 15176 RVA: 0x000F5AEF File Offset: 0x000F3CEF
			public static ArtifactDef bombArtifactDef
			{
				get
				{
					return RoR2Content.Artifacts.Bomb;
				}
			}

			// Token: 0x1700058B RID: 1419
			// (get) Token: 0x06003B49 RID: 15177 RVA: 0x000F5AF6 File Offset: 0x000F3CF6
			public static ArtifactDef sacrificeArtifactDef
			{
				get
				{
					return RoR2Content.Artifacts.Sacrifice;
				}
			}

			// Token: 0x1700058C RID: 1420
			// (get) Token: 0x06003B4A RID: 15178 RVA: 0x000F5AFD File Offset: 0x000F3CFD
			public static ArtifactDef enigmaArtifactDef
			{
				get
				{
					return RoR2Content.Artifacts.Enigma;
				}
			}

			// Token: 0x1700058D RID: 1421
			// (get) Token: 0x06003B4B RID: 15179 RVA: 0x000F5B04 File Offset: 0x000F3D04
			public static ArtifactDef eliteOnlyArtifactDef
			{
				get
				{
					return RoR2Content.Artifacts.EliteOnly;
				}
			}

			// Token: 0x1700058E RID: 1422
			// (get) Token: 0x06003B4C RID: 15180 RVA: 0x000F5B0B File Offset: 0x000F3D0B
			public static ArtifactDef randomSurvivorOnRespawnArtifactDef
			{
				get
				{
					return RoR2Content.Artifacts.RandomSurvivorOnRespawn;
				}
			}

			// Token: 0x1700058F RID: 1423
			// (get) Token: 0x06003B4D RID: 15181 RVA: 0x000F5B12 File Offset: 0x000F3D12
			public static ArtifactDef weakAssKneesArtifactDef
			{
				get
				{
					return RoR2Content.Artifacts.WeakAssKnees;
				}
			}

			// Token: 0x17000590 RID: 1424
			// (get) Token: 0x06003B4E RID: 15182 RVA: 0x000F5B19 File Offset: 0x000F3D19
			public static ArtifactDef wispOnDeath
			{
				get
				{
					return RoR2Content.Artifacts.WispOnDeath;
				}
			}

			// Token: 0x17000591 RID: 1425
			// (get) Token: 0x06003B4F RID: 15183 RVA: 0x000F5B20 File Offset: 0x000F3D20
			public static ArtifactDef singleMonsterTypeArtifactDef
			{
				get
				{
					return RoR2Content.Artifacts.SingleMonsterType;
				}
			}

			// Token: 0x17000592 RID: 1426
			// (get) Token: 0x06003B50 RID: 15184 RVA: 0x000F5B27 File Offset: 0x000F3D27
			public static ArtifactDef mixEnemyArtifactDef
			{
				get
				{
					return RoR2Content.Artifacts.MixEnemy;
				}
			}

			// Token: 0x17000593 RID: 1427
			// (get) Token: 0x06003B51 RID: 15185 RVA: 0x000F5B2E File Offset: 0x000F3D2E
			public static ArtifactDef shadowCloneArtifactDef
			{
				get
				{
					return RoR2Content.Artifacts.ShadowClone;
				}
			}

			// Token: 0x17000594 RID: 1428
			// (get) Token: 0x06003B52 RID: 15186 RVA: 0x000F5B35 File Offset: 0x000F3D35
			public static ArtifactDef teamDeathArtifactDef
			{
				get
				{
					return RoR2Content.Artifacts.TeamDeath;
				}
			}

			// Token: 0x17000595 RID: 1429
			// (get) Token: 0x06003B53 RID: 15187 RVA: 0x000F5B3C File Offset: 0x000F3D3C
			public static ArtifactDef swarmsArtifactDef
			{
				get
				{
					return RoR2Content.Artifacts.Swarms;
				}
			}

			// Token: 0x17000596 RID: 1430
			// (get) Token: 0x06003B54 RID: 15188 RVA: 0x000F5B43 File Offset: 0x000F3D43
			public static ArtifactDef commandArtifactDef
			{
				get
				{
					return RoR2Content.Artifacts.Command;
				}
			}

			// Token: 0x17000597 RID: 1431
			// (get) Token: 0x06003B55 RID: 15189 RVA: 0x000F5B4A File Offset: 0x000F3D4A
			public static ArtifactDef monsterTeamGainsItemsArtifactDef
			{
				get
				{
					return RoR2Content.Artifacts.MonsterTeamGainsItems;
				}
			}

			// Token: 0x17000598 RID: 1432
			// (get) Token: 0x06003B56 RID: 15190 RVA: 0x000F5B51 File Offset: 0x000F3D51
			public static ArtifactDef friendlyFireArtifactDef
			{
				get
				{
					return RoR2Content.Artifacts.FriendlyFire;
				}
			}

			// Token: 0x04003A05 RID: 14853
			public static ArtifactDef Glass;

			// Token: 0x04003A06 RID: 14854
			public static ArtifactDef Bomb;

			// Token: 0x04003A07 RID: 14855
			public static ArtifactDef Sacrifice;

			// Token: 0x04003A08 RID: 14856
			public static ArtifactDef Enigma;

			// Token: 0x04003A09 RID: 14857
			public static ArtifactDef EliteOnly;

			// Token: 0x04003A0A RID: 14858
			public static ArtifactDef RandomSurvivorOnRespawn;

			// Token: 0x04003A0B RID: 14859
			public static ArtifactDef WeakAssKnees;

			// Token: 0x04003A0C RID: 14860
			public static ArtifactDef WispOnDeath;

			// Token: 0x04003A0D RID: 14861
			public static ArtifactDef SingleMonsterType;

			// Token: 0x04003A0E RID: 14862
			public static ArtifactDef MixEnemy;

			// Token: 0x04003A0F RID: 14863
			public static ArtifactDef ShadowClone;

			// Token: 0x04003A10 RID: 14864
			public static ArtifactDef TeamDeath;

			// Token: 0x04003A11 RID: 14865
			public static ArtifactDef Swarms;

			// Token: 0x04003A12 RID: 14866
			public static ArtifactDef Command;

			// Token: 0x04003A13 RID: 14867
			public static ArtifactDef MonsterTeamGainsItems;

			// Token: 0x04003A14 RID: 14868
			public static ArtifactDef FriendlyFire;
		}

		// Token: 0x02000A07 RID: 2567
		public static class Items
		{
			// Token: 0x04003A15 RID: 14869
			public static ItemDef Syringe;

			// Token: 0x04003A16 RID: 14870
			public static ItemDef Bear;

			// Token: 0x04003A17 RID: 14871
			public static ItemDef Behemoth;

			// Token: 0x04003A18 RID: 14872
			public static ItemDef Missile;

			// Token: 0x04003A19 RID: 14873
			public static ItemDef ExplodeOnDeath;

			// Token: 0x04003A1A RID: 14874
			public static ItemDef Dagger;

			// Token: 0x04003A1B RID: 14875
			public static ItemDef Tooth;

			// Token: 0x04003A1C RID: 14876
			public static ItemDef CritGlasses;

			// Token: 0x04003A1D RID: 14877
			public static ItemDef Hoof;

			// Token: 0x04003A1E RID: 14878
			public static ItemDef Feather;

			// Token: 0x04003A1F RID: 14879
			public static ItemDef ChainLightning;

			// Token: 0x04003A20 RID: 14880
			public static ItemDef Seed;

			// Token: 0x04003A21 RID: 14881
			public static ItemDef Icicle;

			// Token: 0x04003A22 RID: 14882
			public static ItemDef GhostOnKill;

			// Token: 0x04003A23 RID: 14883
			public static ItemDef Mushroom;

			// Token: 0x04003A24 RID: 14884
			public static ItemDef Crowbar;

			// Token: 0x04003A25 RID: 14885
			public static ItemDef LevelBonus;

			// Token: 0x04003A26 RID: 14886
			public static ItemDef AttackSpeedOnCrit;

			// Token: 0x04003A27 RID: 14887
			public static ItemDef BleedOnHit;

			// Token: 0x04003A28 RID: 14888
			public static ItemDef SprintOutOfCombat;

			// Token: 0x04003A29 RID: 14889
			public static ItemDef FallBoots;

			// Token: 0x04003A2A RID: 14890
			public static ItemDef WardOnLevel;

			// Token: 0x04003A2B RID: 14891
			public static ItemDef Phasing;

			// Token: 0x04003A2C RID: 14892
			public static ItemDef HealOnCrit;

			// Token: 0x04003A2D RID: 14893
			public static ItemDef HealWhileSafe;

			// Token: 0x04003A2E RID: 14894
			public static ItemDef PersonalShield;

			// Token: 0x04003A2F RID: 14895
			public static ItemDef EquipmentMagazine;

			// Token: 0x04003A30 RID: 14896
			public static ItemDef NovaOnHeal;

			// Token: 0x04003A31 RID: 14897
			public static ItemDef ShockNearby;

			// Token: 0x04003A32 RID: 14898
			public static ItemDef Infusion;

			// Token: 0x04003A33 RID: 14899
			public static ItemDef Clover;

			// Token: 0x04003A34 RID: 14900
			public static ItemDef Medkit;

			// Token: 0x04003A35 RID: 14901
			public static ItemDef Bandolier;

			// Token: 0x04003A36 RID: 14902
			public static ItemDef BounceNearby;

			// Token: 0x04003A37 RID: 14903
			public static ItemDef IgniteOnKill;

			// Token: 0x04003A38 RID: 14904
			public static ItemDef StunChanceOnHit;

			// Token: 0x04003A39 RID: 14905
			public static ItemDef Firework;

			// Token: 0x04003A3A RID: 14906
			public static ItemDef LunarDagger;

			// Token: 0x04003A3B RID: 14907
			public static ItemDef GoldOnHit;

			// Token: 0x04003A3C RID: 14908
			public static ItemDef WarCryOnMultiKill;

			// Token: 0x04003A3D RID: 14909
			public static ItemDef BoostHp;

			// Token: 0x04003A3E RID: 14910
			public static ItemDef BoostDamage;

			// Token: 0x04003A3F RID: 14911
			public static ItemDef ShieldOnly;

			// Token: 0x04003A40 RID: 14912
			public static ItemDef AlienHead;

			// Token: 0x04003A41 RID: 14913
			public static ItemDef Talisman;

			// Token: 0x04003A42 RID: 14914
			public static ItemDef Knurl;

			// Token: 0x04003A43 RID: 14915
			public static ItemDef BeetleGland;

			// Token: 0x04003A44 RID: 14916
			public static ItemDef CrippleWardOnLevel;

			// Token: 0x04003A45 RID: 14917
			public static ItemDef SprintBonus;

			// Token: 0x04003A46 RID: 14918
			public static ItemDef SecondarySkillMagazine;

			// Token: 0x04003A47 RID: 14919
			public static ItemDef StickyBomb;

			// Token: 0x04003A48 RID: 14920
			public static ItemDef TreasureCache;

			// Token: 0x04003A49 RID: 14921
			public static ItemDef BossDamageBonus;

			// Token: 0x04003A4A RID: 14922
			public static ItemDef SprintArmor;

			// Token: 0x04003A4B RID: 14923
			public static ItemDef IceRing;

			// Token: 0x04003A4C RID: 14924
			public static ItemDef FireRing;

			// Token: 0x04003A4D RID: 14925
			public static ItemDef SlowOnHit;

			// Token: 0x04003A4E RID: 14926
			public static ItemDef ExtraLife;

			// Token: 0x04003A4F RID: 14927
			public static ItemDef ExtraLifeConsumed;

			// Token: 0x04003A50 RID: 14928
			public static ItemDef UtilitySkillMagazine;

			// Token: 0x04003A51 RID: 14929
			public static ItemDef HeadHunter;

			// Token: 0x04003A52 RID: 14930
			public static ItemDef KillEliteFrenzy;

			// Token: 0x04003A53 RID: 14931
			public static ItemDef RepeatHeal;

			// Token: 0x04003A54 RID: 14932
			public static ItemDef Ghost;

			// Token: 0x04003A55 RID: 14933
			public static ItemDef HealthDecay;

			// Token: 0x04003A56 RID: 14934
			public static ItemDef AutoCastEquipment;

			// Token: 0x04003A57 RID: 14935
			public static ItemDef IncreaseHealing;

			// Token: 0x04003A58 RID: 14936
			public static ItemDef JumpBoost;

			// Token: 0x04003A59 RID: 14937
			public static ItemDef DrizzlePlayerHelper;

			// Token: 0x04003A5A RID: 14938
			public static ItemDef ExecuteLowHealthElite;

			// Token: 0x04003A5B RID: 14939
			public static ItemDef EnergizedOnEquipmentUse;

			// Token: 0x04003A5C RID: 14940
			public static ItemDef BarrierOnOverHeal;

			// Token: 0x04003A5D RID: 14941
			public static ItemDef TonicAffliction;

			// Token: 0x04003A5E RID: 14942
			public static ItemDef TitanGoldDuringTP;

			// Token: 0x04003A5F RID: 14943
			public static ItemDef SprintWisp;

			// Token: 0x04003A60 RID: 14944
			public static ItemDef BarrierOnKill;

			// Token: 0x04003A61 RID: 14945
			public static ItemDef ArmorReductionOnHit;

			// Token: 0x04003A62 RID: 14946
			public static ItemDef TPHealingNova;

			// Token: 0x04003A63 RID: 14947
			public static ItemDef NearbyDamageBonus;

			// Token: 0x04003A64 RID: 14948
			public static ItemDef LunarUtilityReplacement;

			// Token: 0x04003A65 RID: 14949
			public static ItemDef MonsoonPlayerHelper;

			// Token: 0x04003A66 RID: 14950
			public static ItemDef Thorns;

			// Token: 0x04003A67 RID: 14951
			public static ItemDef FlatHealth;

			// Token: 0x04003A68 RID: 14952
			public static ItemDef Pearl;

			// Token: 0x04003A69 RID: 14953
			public static ItemDef ShinyPearl;

			// Token: 0x04003A6A RID: 14954
			public static ItemDef BonusGoldPackOnKill;

			// Token: 0x04003A6B RID: 14955
			public static ItemDef LaserTurbine;

			// Token: 0x04003A6C RID: 14956
			public static ItemDef LunarPrimaryReplacement;

			// Token: 0x04003A6D RID: 14957
			public static ItemDef NovaOnLowHealth;

			// Token: 0x04003A6E RID: 14958
			public static ItemDef LunarTrinket;

			// Token: 0x04003A6F RID: 14959
			public static ItemDef InvadingDoppelganger;

			// Token: 0x04003A70 RID: 14960
			public static ItemDef CutHp;

			// Token: 0x04003A71 RID: 14961
			public static ItemDef ArtifactKey;

			// Token: 0x04003A72 RID: 14962
			public static ItemDef ArmorPlate;

			// Token: 0x04003A73 RID: 14963
			public static ItemDef Squid;

			// Token: 0x04003A74 RID: 14964
			public static ItemDef DeathMark;

			// Token: 0x04003A75 RID: 14965
			public static ItemDef Plant;

			// Token: 0x04003A76 RID: 14966
			public static ItemDef FocusConvergence;

			// Token: 0x04003A77 RID: 14967
			public static ItemDef BoostAttackSpeed;

			// Token: 0x04003A78 RID: 14968
			public static ItemDef AdaptiveArmor;

			// Token: 0x04003A79 RID: 14969
			public static ItemDef CaptainDefenseMatrix;

			// Token: 0x04003A7A RID: 14970
			public static ItemDef FireballsOnHit;

			// Token: 0x04003A7B RID: 14971
			public static ItemDef LightningStrikeOnHit;

			// Token: 0x04003A7C RID: 14972
			public static ItemDef BleedOnHitAndExplode;

			// Token: 0x04003A7D RID: 14973
			public static ItemDef SiphonOnLowHealth;

			// Token: 0x04003A7E RID: 14974
			public static ItemDef MonstersOnShrineUse;

			// Token: 0x04003A7F RID: 14975
			public static ItemDef RandomDamageZone;

			// Token: 0x04003A80 RID: 14976
			public static ItemDef ScrapWhite;

			// Token: 0x04003A81 RID: 14977
			public static ItemDef ScrapGreen;

			// Token: 0x04003A82 RID: 14978
			public static ItemDef ScrapRed;

			// Token: 0x04003A83 RID: 14979
			public static ItemDef ScrapYellow;

			// Token: 0x04003A84 RID: 14980
			public static ItemDef LunarBadLuck;

			// Token: 0x04003A85 RID: 14981
			public static ItemDef BoostEquipmentRecharge;

			// Token: 0x04003A86 RID: 14982
			public static ItemDef LunarSecondaryReplacement;

			// Token: 0x04003A87 RID: 14983
			public static ItemDef LunarSpecialReplacement;

			// Token: 0x04003A88 RID: 14984
			public static ItemDef TeamSizeDamageBonus;

			// Token: 0x04003A89 RID: 14985
			public static ItemDef RoboBallBuddy;

			// Token: 0x04003A8A RID: 14986
			public static ItemDef ParentEgg;

			// Token: 0x04003A8B RID: 14987
			public static ItemDef SummonedEcho;

			// Token: 0x04003A8C RID: 14988
			public static ItemDef MinionLeash;

			// Token: 0x04003A8D RID: 14989
			public static ItemDef UseAmbientLevel;

			// Token: 0x04003A8E RID: 14990
			public static ItemDef TeleportWhenOob;

			// Token: 0x04003A8F RID: 14991
			public static ItemDef MinHealthPercentage;
		}

		// Token: 0x02000A08 RID: 2568
		public static class Equipment
		{
			// Token: 0x04003A90 RID: 14992
			public static EquipmentDef CommandMissile;

			// Token: 0x04003A91 RID: 14993
			public static EquipmentDef Fruit;

			// Token: 0x04003A92 RID: 14994
			public static EquipmentDef Meteor;

			// Token: 0x04003A93 RID: 14995
			[TargetAssetName("EliteFireEquipment")]
			public static EquipmentDef AffixRed;

			// Token: 0x04003A94 RID: 14996
			[TargetAssetName("EliteLightningEquipment")]
			public static EquipmentDef AffixBlue;

			// Token: 0x04003A95 RID: 14997
			[TargetAssetName("EliteIceEquipment")]
			public static EquipmentDef AffixWhite;

			// Token: 0x04003A96 RID: 14998
			[TargetAssetName("ElitePoisonEquipment")]
			public static EquipmentDef AffixPoison;

			// Token: 0x04003A97 RID: 14999
			public static EquipmentDef Blackhole;

			// Token: 0x04003A98 RID: 15000
			public static EquipmentDef CritOnUse;

			// Token: 0x04003A99 RID: 15001
			public static EquipmentDef DroneBackup;

			// Token: 0x04003A9A RID: 15002
			public static EquipmentDef BFG;

			// Token: 0x04003A9B RID: 15003
			public static EquipmentDef Jetpack;

			// Token: 0x04003A9C RID: 15004
			public static EquipmentDef Lightning;

			// Token: 0x04003A9D RID: 15005
			public static EquipmentDef GoldGat;

			// Token: 0x04003A9E RID: 15006
			public static EquipmentDef PassiveHealing;

			// Token: 0x04003A9F RID: 15007
			public static EquipmentDef LunarPotion;

			// Token: 0x04003AA0 RID: 15008
			public static EquipmentDef BurnNearby;

			// Token: 0x04003AA1 RID: 15009
			public static EquipmentDef Scanner;

			// Token: 0x04003AA2 RID: 15010
			public static EquipmentDef CrippleWard;

			// Token: 0x04003AA3 RID: 15011
			public static EquipmentDef Gateway;

			// Token: 0x04003AA4 RID: 15012
			public static EquipmentDef Tonic;

			// Token: 0x04003AA5 RID: 15013
			public static EquipmentDef QuestVolatileBattery;

			// Token: 0x04003AA6 RID: 15014
			public static EquipmentDef Cleanse;

			// Token: 0x04003AA7 RID: 15015
			public static EquipmentDef FireBallDash;

			// Token: 0x04003AA8 RID: 15016
			[TargetAssetName("EliteHauntedEquipment")]
			public static EquipmentDef AffixHaunted;

			// Token: 0x04003AA9 RID: 15017
			public static EquipmentDef GainArmor;

			// Token: 0x04003AAA RID: 15018
			public static EquipmentDef Saw;

			// Token: 0x04003AAB RID: 15019
			public static EquipmentDef Recycle;

			// Token: 0x04003AAC RID: 15020
			public static EquipmentDef LifestealOnHit;

			// Token: 0x04003AAD RID: 15021
			public static EquipmentDef TeamWarCry;

			// Token: 0x04003AAE RID: 15022
			public static EquipmentDef DeathProjectile;

			// Token: 0x04003AAF RID: 15023
			[TargetAssetName("EliteEchoEquipment")]
			public static EquipmentDef AffixEcho;

			// Token: 0x04003AB0 RID: 15024
			[TargetAssetName("EliteLunarEquipment")]
			public static EquipmentDef AffixLunar;
		}

		// Token: 0x02000A09 RID: 2569
		public static class Buffs
		{
			// Token: 0x04003AB1 RID: 15025
			public static BuffDef Slow50;

			// Token: 0x04003AB2 RID: 15026
			public static BuffDef ArmorBoost;

			// Token: 0x04003AB3 RID: 15027
			public static BuffDef AttackSpeedOnCrit;

			// Token: 0x04003AB4 RID: 15028
			public static BuffDef HiddenInvincibility;

			// Token: 0x04003AB5 RID: 15029
			public static BuffDef OnFire;

			// Token: 0x04003AB6 RID: 15030
			public static BuffDef Warbanner;

			// Token: 0x04003AB7 RID: 15031
			public static BuffDef Cloak;

			// Token: 0x04003AB8 RID: 15032
			public static BuffDef CloakSpeed;

			// Token: 0x04003AB9 RID: 15033
			public static BuffDef FullCrit;

			// Token: 0x04003ABA RID: 15034
			[TargetAssetName("bdElitePoison")]
			public static BuffDef AffixPoison;

			// Token: 0x04003ABB RID: 15035
			public static BuffDef EngiShield;

			// Token: 0x04003ABC RID: 15036
			public static BuffDef TeslaField;

			// Token: 0x04003ABD RID: 15037
			public static BuffDef WarCryBuff;

			// Token: 0x04003ABE RID: 15038
			public static BuffDef Energized;

			// Token: 0x04003ABF RID: 15039
			public static BuffDef BeetleJuice;

			// Token: 0x04003AC0 RID: 15040
			public static BuffDef BugWings;

			// Token: 0x04003AC1 RID: 15041
			public static BuffDef MedkitHeal;

			// Token: 0x04003AC2 RID: 15042
			public static BuffDef ClayGoo;

			// Token: 0x04003AC3 RID: 15043
			public static BuffDef Immune;

			// Token: 0x04003AC4 RID: 15044
			public static BuffDef Cripple;

			// Token: 0x04003AC5 RID: 15045
			public static BuffDef Slow80;

			// Token: 0x04003AC6 RID: 15046
			public static BuffDef Slow60;

			// Token: 0x04003AC7 RID: 15047
			[TargetAssetName("bdEliteFire")]
			public static BuffDef AffixRed;

			// Token: 0x04003AC8 RID: 15048
			[TargetAssetName("bdEliteLightning")]
			public static BuffDef AffixBlue;

			// Token: 0x04003AC9 RID: 15049
			public static BuffDef NoCooldowns;

			// Token: 0x04003ACA RID: 15050
			[TargetAssetName("bdEliteIce")]
			public static BuffDef AffixWhite;

			// Token: 0x04003ACB RID: 15051
			public static BuffDef TonicBuff;

			// Token: 0x04003ACC RID: 15052
			public static BuffDef HealingDisabled;

			// Token: 0x04003ACD RID: 15053
			public static BuffDef Weak;

			// Token: 0x04003ACE RID: 15054
			public static BuffDef Entangle;

			// Token: 0x04003ACF RID: 15055
			[TargetAssetName("bdEliteHaunted")]
			public static BuffDef AffixHaunted;

			// Token: 0x04003AD0 RID: 15056
			public static BuffDef Pulverized;

			// Token: 0x04003AD1 RID: 15057
			public static BuffDef PulverizeBuildup;

			// Token: 0x04003AD2 RID: 15058
			[TargetAssetName("bdEliteHauntedRecipient")]
			public static BuffDef AffixHauntedRecipient;

			// Token: 0x04003AD3 RID: 15059
			public static BuffDef Intangible;

			// Token: 0x04003AD4 RID: 15060
			public static BuffDef ElephantArmorBoost;

			// Token: 0x04003AD5 RID: 15061
			public static BuffDef NullifyStack;

			// Token: 0x04003AD6 RID: 15062
			public static BuffDef Nullified;

			// Token: 0x04003AD7 RID: 15063
			public static BuffDef Bleeding;

			// Token: 0x04003AD8 RID: 15064
			public static BuffDef SuperBleed;

			// Token: 0x04003AD9 RID: 15065
			public static BuffDef Poisoned;

			// Token: 0x04003ADA RID: 15066
			public static BuffDef WhipBoost;

			// Token: 0x04003ADB RID: 15067
			public static BuffDef Blight;

			// Token: 0x04003ADC RID: 15068
			public static BuffDef DeathMark;

			// Token: 0x04003ADD RID: 15069
			public static BuffDef CrocoRegen;

			// Token: 0x04003ADE RID: 15070
			public static BuffDef MercExpose;

			// Token: 0x04003ADF RID: 15071
			public static BuffDef LifeSteal;

			// Token: 0x04003AE0 RID: 15072
			public static BuffDef PowerBuff;

			// Token: 0x04003AE1 RID: 15073
			public static BuffDef LunarShell;

			// Token: 0x04003AE2 RID: 15074
			public static BuffDef TeamWarCry;

			// Token: 0x04003AE3 RID: 15075
			public static BuffDef PermanentCurse;

			// Token: 0x04003AE4 RID: 15076
			public static BuffDef ElementalRingsReady;

			// Token: 0x04003AE5 RID: 15077
			public static BuffDef ElementalRingsCooldown;

			// Token: 0x04003AE6 RID: 15078
			public static BuffDef LunarSecondaryRoot;

			// Token: 0x04003AE7 RID: 15079
			public static BuffDef LunarDetonationCharge;

			// Token: 0x04003AE8 RID: 15080
			public static BuffDef Overheat;

			// Token: 0x04003AE9 RID: 15081
			public static BuffDef Fruiting;

			// Token: 0x04003AEA RID: 15082
			public static BuffDef BanditSkull;

			// Token: 0x04003AEB RID: 15083
			[TargetAssetName("bdEliteEcho")]
			public static BuffDef AffixEcho;

			// Token: 0x04003AEC RID: 15084
			public static BuffDef LaserTurbineKillCharge;

			// Token: 0x04003AED RID: 15085
			[TargetAssetName("bdEliteLunar")]
			public static BuffDef AffixLunar;

			// Token: 0x04003AEE RID: 15086
			public static BuffDef SmallArmorBoost;

			// Token: 0x04003AEF RID: 15087
			public static BuffDef VoidFogMild;

			// Token: 0x04003AF0 RID: 15088
			public static BuffDef VoidFogStrong;
		}

		// Token: 0x02000A0A RID: 2570
		public static class Elites
		{
			// Token: 0x04003AF1 RID: 15089
			public static EliteDef Fire;

			// Token: 0x04003AF2 RID: 15090
			public static EliteDef FireHonor;

			// Token: 0x04003AF3 RID: 15091
			public static EliteDef Lightning;

			// Token: 0x04003AF4 RID: 15092
			public static EliteDef LightningHonor;

			// Token: 0x04003AF5 RID: 15093
			public static EliteDef Ice;

			// Token: 0x04003AF6 RID: 15094
			public static EliteDef IceHonor;

			// Token: 0x04003AF7 RID: 15095
			public static EliteDef Poison;

			// Token: 0x04003AF8 RID: 15096
			public static EliteDef Haunted;

			// Token: 0x04003AF9 RID: 15097
			public static EliteDef Echo;

			// Token: 0x04003AFA RID: 15098
			public static EliteDef Lunar;
		}

		// Token: 0x02000A0B RID: 2571
		public static class GameEndings
		{
			// Token: 0x04003AFB RID: 15099
			public static GameEndingDef StandardLoss;

			// Token: 0x04003AFC RID: 15100
			public static GameEndingDef ObliterationEnding;

			// Token: 0x04003AFD RID: 15101
			public static GameEndingDef LimboEnding;

			// Token: 0x04003AFE RID: 15102
			public static GameEndingDef MainEnding;

			// Token: 0x04003AFF RID: 15103
			public static GameEndingDef PrismaticTrialEnding;
		}

		// Token: 0x02000A0C RID: 2572
		public static class Survivors
		{
			// Token: 0x04003B00 RID: 15104
			public static SurvivorDef Commando;

			// Token: 0x04003B01 RID: 15105
			public static SurvivorDef Engi;

			// Token: 0x04003B02 RID: 15106
			public static SurvivorDef Huntress;

			// Token: 0x04003B03 RID: 15107
			public static SurvivorDef Mage;

			// Token: 0x04003B04 RID: 15108
			public static SurvivorDef Merc;

			// Token: 0x04003B05 RID: 15109
			public static SurvivorDef Toolbot;

			// Token: 0x04003B06 RID: 15110
			public static SurvivorDef Treebot;

			// Token: 0x04003B07 RID: 15111
			public static SurvivorDef Loader;

			// Token: 0x04003B08 RID: 15112
			public static SurvivorDef Croco;

			// Token: 0x04003B09 RID: 15113
			public static SurvivorDef Captain;

			// Token: 0x04003B0A RID: 15114
			public static SurvivorDef Bandit2;
		}

		// Token: 0x02000A0D RID: 2573
		public static class MiscPickups
		{
			// Token: 0x04003B0B RID: 15115
			public static MiscPickupDef LunarCoin;
		}
	}
}
