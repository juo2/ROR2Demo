using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using RoR2.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI.LogBook
{
	// Token: 0x02000DF4 RID: 3572
	public class PageBuilder
	{
		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x060051F9 RID: 20985 RVA: 0x00153181 File Offset: 0x00151381
		private StatSheet statSheet
		{
			get
			{
				return this.userProfile.statSheet;
			}
		}

		// Token: 0x060051FA RID: 20986 RVA: 0x00153190 File Offset: 0x00151390
		public void Destroy()
		{
			foreach (GameObject obj in this.managedObjects)
			{
				UnityEngine.Object.Destroy(obj);
			}
		}

		// Token: 0x060051FB RID: 20987 RVA: 0x001531E0 File Offset: 0x001513E0
		public void AddSimpleTextPanel(string text)
		{
			this.AddPrefabInstance(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/Logbook/SimpleTextPanel")).GetComponent<ChildLocator>().FindChild("MainLabel").GetComponent<TextMeshProUGUI>().text = text;
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x0015320C File Offset: 0x0015140C
		public GameObject AddPrefabInstance(GameObject prefab)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab, this.container);
			this.managedObjects.Add(gameObject);
			return gameObject;
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x00153233 File Offset: 0x00151433
		public void AddSimpleTextPanel(params string[] textLines)
		{
			this.AddSimpleTextPanel(string.Join("\n", textLines));
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x00153248 File Offset: 0x00151448
		public void AddSimplePickup(PickupIndex pickupIndex)
		{
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			ItemIndex itemIndex = (pickupDef != null) ? pickupDef.itemIndex : ItemIndex.None;
			EquipmentIndex equipmentIndex = (pickupDef != null) ? pickupDef.equipmentIndex : EquipmentIndex.None;
			string token = null;
			if (itemIndex != ItemIndex.None)
			{
				ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
				this.AddDescriptionPanel(Language.GetString(itemDef.descriptionToken));
				token = itemDef.loreToken;
				ulong statValueULong = this.statSheet.GetStatValueULong(PerItemStatDef.totalCollected.FindStatDef(itemIndex));
				ulong statValueULong2 = this.statSheet.GetStatValueULong(PerItemStatDef.highestCollected.FindStatDef(itemIndex));
				string stringFormatted = Language.GetStringFormatted("GENERIC_PREFIX_FOUND", new object[]
				{
					statValueULong
				});
				string stringFormatted2 = Language.GetStringFormatted("ITEM_PREFIX_STACKCOUNT", new object[]
				{
					statValueULong2
				});
				this.AddSimpleTextPanel(new string[]
				{
					stringFormatted,
					stringFormatted2
				});
			}
			else if (equipmentIndex != EquipmentIndex.None)
			{
				EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(equipmentIndex);
				this.AddDescriptionPanel(Language.GetString(equipmentDef.descriptionToken));
				token = equipmentDef.loreToken;
				string stringFormatted3 = Language.GetStringFormatted("EQUIPMENT_PREFIX_COOLDOWN", new object[]
				{
					equipmentDef.cooldown
				});
				string stringFormatted4 = Language.GetStringFormatted("EQUIPMENT_PREFIX_TOTALTIMEHELD", new object[]
				{
					this.statSheet.GetStatDisplayValue(PerEquipmentStatDef.totalTimeHeld.FindStatDef(equipmentIndex))
				});
				string stringFormatted5 = Language.GetStringFormatted("EQUIPMENT_PREFIX_USECOUNT", new object[]
				{
					this.statSheet.GetStatDisplayValue(PerEquipmentStatDef.totalTimesFired.FindStatDef(equipmentIndex))
				});
				this.AddSimpleTextPanel(stringFormatted3);
				this.AddSimpleTextPanel(new string[]
				{
					stringFormatted4,
					stringFormatted5
				});
			}
			this.AddNotesPanel(Language.IsTokenInvalid(token) ? Language.GetString("EARLY_ACCESS_LORE") : Language.GetString(token));
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x001533FF File Offset: 0x001515FF
		public void AddDescriptionPanel(string content)
		{
			this.AddSimpleTextPanel(Language.GetStringFormatted("DESCRIPTION_PREFIX_FORMAT", new object[]
			{
				content
			}));
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x0015341B File Offset: 0x0015161B
		public void AddNotesPanel(string content)
		{
			this.AddSimpleTextPanel(Language.GetStringFormatted("NOTES_PREFIX_FORMAT", new object[]
			{
				content
			}));
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x00153438 File Offset: 0x00151638
		public void AddBodyStatsPanel(CharacterBody bodyPrefabComponent)
		{
			float baseMaxHealth = bodyPrefabComponent.baseMaxHealth;
			float levelMaxHealth = bodyPrefabComponent.levelMaxHealth;
			float baseDamage = bodyPrefabComponent.baseDamage;
			float levelDamage = bodyPrefabComponent.levelDamage;
			float baseArmor = bodyPrefabComponent.baseArmor;
			float baseRegen = bodyPrefabComponent.baseRegen;
			float levelRegen = bodyPrefabComponent.levelRegen;
			float baseMoveSpeed = bodyPrefabComponent.baseMoveSpeed;
			this.AddSimpleTextPanel(string.Concat(new string[]
			{
				Language.GetStringFormatted("BODY_HEALTH_FORMAT", new object[]
				{
					Language.GetStringFormatted("BODY_STATS_FORMAT", new object[]
					{
						baseMaxHealth.ToString(),
						levelMaxHealth.ToString()
					})
				}),
				"\n",
				Language.GetStringFormatted("BODY_DAMAGE_FORMAT", new object[]
				{
					Language.GetStringFormatted("BODY_STATS_FORMAT", new object[]
					{
						baseDamage.ToString(),
						levelDamage.ToString()
					})
				}),
				"\n",
				(baseRegen >= Mathf.Epsilon) ? (Language.GetStringFormatted("BODY_REGEN_FORMAT", new object[]
				{
					Language.GetStringFormatted("BODY_STATS_FORMAT", new object[]
					{
						baseRegen.ToString(),
						levelRegen.ToString()
					})
				}) + "\n") : "",
				Language.GetStringFormatted("BODY_MOVESPEED_FORMAT", new object[]
				{
					baseMoveSpeed
				}),
				"\n",
				Language.GetStringFormatted("BODY_ARMOR_FORMAT", new object[]
				{
					baseArmor.ToString()
				})
			}));
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x001535AC File Offset: 0x001517AC
		public void AddMonsterPanel(CharacterBody bodyPrefabComponent)
		{
			ulong statValueULong = this.statSheet.GetStatValueULong(PerBodyStatDef.killsAgainst, bodyPrefabComponent.gameObject.name);
			ulong statValueULong2 = this.statSheet.GetStatValueULong(PerBodyStatDef.killsAgainstElite, bodyPrefabComponent.gameObject.name);
			ulong statValueULong3 = this.statSheet.GetStatValueULong(PerBodyStatDef.deathsFrom, bodyPrefabComponent.gameObject.name);
			string stringFormatted = Language.GetStringFormatted("MONSTER_PREFIX_KILLED", new object[]
			{
				statValueULong
			});
			string stringFormatted2 = Language.GetStringFormatted("MONSTER_PREFIX_ELITESKILLED", new object[]
			{
				statValueULong2
			});
			string stringFormatted3 = Language.GetStringFormatted("MONSTER_PREFIX_DEATH", new object[]
			{
				statValueULong3
			});
			this.AddSimpleTextPanel(new string[]
			{
				stringFormatted,
				stringFormatted2,
				stringFormatted3
			});
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x00153678 File Offset: 0x00151878
		public void AddSurvivorPanel(CharacterBody bodyPrefabComponent)
		{
			string statDisplayValue = this.statSheet.GetStatDisplayValue(PerBodyStatDef.longestRun.FindStatDef(bodyPrefabComponent.name));
			ulong statValueULong = this.statSheet.GetStatValueULong(PerBodyStatDef.timesPicked.FindStatDef(bodyPrefabComponent.name));
			ulong statValueULong2 = this.statSheet.GetStatValueULong(StatDef.totalGamesPlayed);
			double num = 0.0;
			if (statValueULong2 != 0UL)
			{
				num = statValueULong / statValueULong2 * 100.0;
			}
			PageBuilder.sharedStringBuilder.Clear();
			PageBuilder.sharedStringBuilder.AppendLine(Language.GetStringFormatted("SURVIVOR_PREFIX_LONGESTRUN", new object[]
			{
				statDisplayValue
			}));
			PageBuilder.sharedStringBuilder.AppendLine(Language.GetStringFormatted("SURVIVOR_PREFIX_TIMESPICKED", new object[]
			{
				statValueULong
			}));
			PageBuilder.sharedStringBuilder.AppendLine(Language.GetStringFormatted("SURVIVOR_PREFIX_PICKPERCENTAGE", new object[]
			{
				num
			}));
			this.AddSimpleTextPanel(PageBuilder.sharedStringBuilder.ToString());
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x0015376F File Offset: 0x0015196F
		public void AddSimpleBody(CharacterBody bodyPrefabComponent)
		{
			this.AddBodyStatsPanel(bodyPrefabComponent);
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x00153778 File Offset: 0x00151978
		public void AddBodyLore(CharacterBody characterBody)
		{
			bool flag = false;
			string token = "";
			string baseNameToken = characterBody.baseNameToken;
			if (!string.IsNullOrEmpty(baseNameToken))
			{
				token = baseNameToken.Replace("_NAME", "_LORE");
				if (!Language.IsTokenInvalid(token))
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.AddNotesPanel(Language.GetString(token));
				return;
			}
			this.AddNotesPanel(Language.GetString("EARLY_ACCESS_LORE"));
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x001537D8 File Offset: 0x001519D8
		public void AddStagePanel(SceneDef sceneDef)
		{
			string statDisplayValue = this.userProfile.statSheet.GetStatDisplayValue(PerStageStatDef.totalTimesVisited.FindStatDef(sceneDef.baseSceneName));
			string statDisplayValue2 = this.userProfile.statSheet.GetStatDisplayValue(PerStageStatDef.totalTimesCleared.FindStatDef(sceneDef.baseSceneName));
			string stringFormatted = Language.GetStringFormatted("STAGE_PREFIX_TOTALTIMESVISITED", new object[]
			{
				statDisplayValue
			});
			string stringFormatted2 = Language.GetStringFormatted("STAGE_PREFIX_TOTALTIMESCLEARED", new object[]
			{
				statDisplayValue2
			});
			PageBuilder.sharedStringBuilder.Clear();
			PageBuilder.sharedStringBuilder.Append(stringFormatted);
			PageBuilder.sharedStringBuilder.Append("\n");
			PageBuilder.sharedStringBuilder.Append(stringFormatted2);
			this.AddSimpleTextPanel(PageBuilder.sharedStringBuilder.ToString());
		}

		// Token: 0x06005207 RID: 20999 RVA: 0x00153894 File Offset: 0x00151A94
		public void AddPieChart(PieChartMeshController.SliceInfo[] sliceInfos)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/PieChartPanel"), this.container);
			gameObject.GetComponent<PieChartMeshController>().SetSlices(sliceInfos);
			this.managedObjects.Add(gameObject);
		}

		// Token: 0x06005208 RID: 21000 RVA: 0x001538D0 File Offset: 0x00151AD0
		public static void Stage(PageBuilder builder)
		{
			SceneDef sceneDef = (SceneDef)builder.entry.extraData;
			builder.AddStagePanel(sceneDef);
			builder.AddNotesPanel(Language.IsTokenInvalid(sceneDef.loreToken) ? Language.GetString("EARLY_ACCESS_LORE") : Language.GetString(sceneDef.loreToken));
		}

		// Token: 0x06005209 RID: 21001 RVA: 0x0015391F File Offset: 0x00151B1F
		public static void SimplePickup(PageBuilder builder)
		{
			builder.AddSimplePickup((PickupIndex)builder.entry.extraData);
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x00153937 File Offset: 0x00151B37
		public static void SimpleBody(PageBuilder builder)
		{
			builder.AddSimpleBody((CharacterBody)builder.entry.extraData);
		}

		// Token: 0x0600520B RID: 21003 RVA: 0x00153950 File Offset: 0x00151B50
		public static void MonsterBody(PageBuilder builder)
		{
			CharacterBody characterBody = (CharacterBody)builder.entry.extraData;
			builder.AddSimpleBody(characterBody);
			builder.AddMonsterPanel(characterBody);
			builder.AddBodyLore(characterBody);
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x00153984 File Offset: 0x00151B84
		public static void SurvivorBody(PageBuilder builder)
		{
			CharacterBody characterBody = (CharacterBody)builder.entry.extraData;
			builder.AddSimpleBody(characterBody);
			builder.AddSurvivorPanel(characterBody);
			builder.AddBodyLore(characterBody);
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x001539B8 File Offset: 0x00151BB8
		public static void StatsPanel(PageBuilder builder)
		{
			PageBuilder.<>c__DisplayClass26_0 CS$<>8__locals1 = new PageBuilder.<>c__DisplayClass26_0();
			UserProfile userProfile = (UserProfile)builder.entry.extraData;
			GameCompletionStatsHelper gameCompletionStatsHelper = new GameCompletionStatsHelper();
			CS$<>8__locals1.statSheet = userProfile.statSheet;
			CS$<>8__locals1.<StatsPanel>g__CalcAllBodyStatTotalDouble|2(PerBodyStatDef.totalTimeAlive);
			(double)CS$<>8__locals1.<StatsPanel>g__CalcAllBodyStatTotalULong|1(PerBodyStatDef.timesPicked);
			double value = (double)CS$<>8__locals1.<StatsPanel>g__CalcAllBodyStatTotalULong|1(PerBodyStatDef.totalWins);
			(double)CS$<>8__locals1.<StatsPanel>g__CalcAllBodyStatTotalULong|1(PerBodyStatDef.deathsAs);
			ChildLocator component = builder.AddPrefabInstance(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/Logbook/ProfileStatsPanel")).GetComponent<ChildLocator>();
			RectTransform rectTransform = (RectTransform)component.FindChild("CharacterPieChart");
			RectTransform rectTransform2 = (RectTransform)component.FindChild("CompletionBarPanel");
			RectTransform rectTransform3 = (RectTransform)component.FindChild("CompletionLabel");
			RectTransform rectTransform4 = (RectTransform)component.FindChild("CharacterStatsCarousel");
			RectTransform rectTransform5 = (RectTransform)component.FindChild("TotalsStatsList");
			RectTransform rectTransform6 = (RectTransform)component.FindChild("RecordsStatsList");
			RectTransform rectTransform7 = (RectTransform)component.FindChild("MiscStatsList");
			RectTransform rectTransform8 = (RectTransform)component.FindChild("CompletionStatsList");
			PageBuilder.<>c__DisplayClass26_1 CS$<>8__locals2 = new PageBuilder.<>c__DisplayClass26_1();
			CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
			CS$<>8__locals2.characterPieChartMeshController = rectTransform.GetComponent<PieChartMeshController>();
			PageBuilder.<>c__DisplayClass26_3 CS$<>8__locals3 = new PageBuilder.<>c__DisplayClass26_3();
			CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals2;
			CS$<>8__locals3.carousel = rectTransform4.GetComponent<CarouselNavigationController>();
			CS$<>8__locals3.statNames = new List<string>();
			CS$<>8__locals3.callbacks = new List<Action>();
			CS$<>8__locals3.<StatsPanel>g__AddPerBodyStat|9(PerBodyStatDef.totalWins);
			CS$<>8__locals3.<StatsPanel>g__AddPerBodyStat|9(PerBodyStatDef.timesPicked);
			CS$<>8__locals3.<StatsPanel>g__AddPerBodyStat|9(PerBodyStatDef.totalTimeAlive);
			CS$<>8__locals3.<StatsPanel>g__AddPerBodyStat|9(PerBodyStatDef.longestRun);
			CS$<>8__locals3.<StatsPanel>g__AddPerBodyStat|9(PerBodyStatDef.deathsAs);
			CS$<>8__locals3.<StatsPanel>g__AddPerBodyStat|9(PerBodyStatDef.damageDealtAs);
			CS$<>8__locals3.<StatsPanel>g__AddPerBodyStat|9(PerBodyStatDef.damageTakenAs);
			CS$<>8__locals3.<StatsPanel>g__AddPerBodyStat|9(PerBodyStatDef.damageDealtTo);
			CS$<>8__locals3.<StatsPanel>g__AddPerBodyStat|9(PerBodyStatDef.damageTakenFrom);
			CS$<>8__locals3.<StatsPanel>g__AddPerBodyStat|9(PerBodyStatDef.killsAgainst);
			CS$<>8__locals3.<StatsPanel>g__AddPerBodyStat|9(PerBodyStatDef.killsAgainstElite);
			CS$<>8__locals3.<StatsPanel>g__AddPerBodyStat|9(PerBodyStatDef.deathsFrom);
			CS$<>8__locals3.<StatsPanel>g__AddPerBodyStat|9(PerBodyStatDef.minionDamageDealtAs);
			CS$<>8__locals3.<StatsPanel>g__AddPerBodyStat|9(PerBodyStatDef.minionKillsAs);
			CS$<>8__locals3.<StatsPanel>g__AddPerBodyStat|9(PerBodyStatDef.killsAs);
			CS$<>8__locals3.carousel.onPageChangeSubmitted += CS$<>8__locals3.<StatsPanel>g__OnPageChangeSubmitted|10;
			CS$<>8__locals3.carousel.SetDisplayData(new CarouselNavigationController.DisplayData(CS$<>8__locals3.statNames.Count, 0));
			CS$<>8__locals3.<StatsPanel>g__OnPageChangeSubmitted|10(0);
			PageBuilder.<>c__DisplayClass26_5 CS$<>8__locals4;
			CS$<>8__locals4.statStripPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/Logbook/LogbookStatStrip");
			StatDef statDef = PerBodyStatDef.longestRun.FindStatDef(CS$<>8__locals1.statSheet.FindBodyWithHighestStat(PerBodyStatDef.longestRun)) ?? PerBodyStatDef.longestRun.FindStatDef(BodyCatalog.FindBodyIndex("CommandoBody"));
			CharacterBody bodyPrefabBodyComponent = BodyCatalog.GetBodyPrefabBodyComponent(CS$<>8__locals1.statSheet.FindBodyWithHighestStat(PerBodyStatDef.deathsFrom));
			EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(CS$<>8__locals1.statSheet.FindEquipmentWithHighestStat(PerEquipmentStatDef.totalTimeHeld));
			RectTransform rectTransform9 = rectTransform5;
			ValueTuple<string, string, Texture>[] array = new ValueTuple<string, string, Texture>[20];
			int num = 0;
			ValueTuple<string, string, Texture2D> valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalGamesPlayed);
			array[num] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num2 = 1;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalTimeAlive);
			array[num2] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num3 = 2;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalDeaths);
			array[num3] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			array[3] = new ValueTuple<string, string, Texture>(Language.GetString("STATNAME_TOTALWINS"), TextSerialization.ToStringNumeric(value), null);
			int num4 = 4;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalKills);
			array[num4] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num5 = 5;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalEliteKills);
			array[num5] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num6 = 6;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalDamageDealt);
			array[num6] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num7 = 7;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalStagesCompleted);
			array[num7] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num8 = 8;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalDamageTaken);
			array[num8] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num9 = 9;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalHealthHealed);
			array[num9] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num10 = 10;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.goldCollected);
			array[num10] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num11 = 11;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalDistanceTraveled);
			array[num11] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num12 = 12;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalPurchases);
			array[num12] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num13 = 13;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalBloodPurchases);
			array[num13] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num14 = 14;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalDronesPurchased);
			array[num14] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num15 = 15;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalTurretsPurchased);
			array[num15] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num16 = 16;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalCrocoInfectionsInflicted);
			array[num16] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num17 = 17;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalMinionDamageDealt);
			array[num17] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num18 = 18;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalMinionKills);
			array[num18] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num19 = 19;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.totalDeathsWhileBurning);
			array[num19] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			PageBuilder.<StatsPanel>g__SetStats|26_13(rectTransform9, array, ref CS$<>8__locals4);
			RectTransform rectTransform10 = rectTransform6;
			ValueTuple<string, string, Texture>[] array2 = new ValueTuple<string, string, Texture>[6];
			array2[0] = new ValueTuple<string, string, Texture>(Language.GetString("STATNAME_LONGESTRUN"), CS$<>8__locals1.statSheet.GetStatDisplayValue(statDef), null);
			int num20 = 1;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.highestStagesCompleted);
			array2[num20] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num21 = 2;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.highestLevel);
			array2[num21] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num22 = 3;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.highestDamageDealt);
			array2[num22] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num23 = 4;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.maxGoldCollected);
			array2[num23] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num24 = 5;
			valueTuple = CS$<>8__locals1.<StatsPanel>g__StatStripDataFromStatDef|14(StatDef.highestPurchases);
			array2[num24] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			PageBuilder.<StatsPanel>g__SetStats|26_13(rectTransform10, array2, ref CS$<>8__locals4);
			PageBuilder.<StatsPanel>g__SetStats|26_13(rectTransform7, new ValueTuple<string, string, Texture>[]
			{
				new ValueTuple<string, string, Texture>(Language.GetString("STATNAME_NEMESIS"), bodyPrefabBodyComponent ? Language.GetString(bodyPrefabBodyComponent.baseNameToken) : string.Empty, bodyPrefabBodyComponent ? bodyPrefabBodyComponent.portraitIcon : null),
				new ValueTuple<string, string, Texture>(Language.GetString("STATNAME_FAVORITEEQUIPMENT"), (equipmentDef != null) ? Language.GetString(equipmentDef.nameToken) : string.Empty, (equipmentDef != null) ? equipmentDef.pickupIconTexture : null)
			}, ref CS$<>8__locals4);
			RectTransform rectTransform11 = rectTransform8;
			ValueTuple<string, string, Texture>[] array3 = new ValueTuple<string, string, Texture>[5];
			int num25 = 0;
			valueTuple = PageBuilder.<StatsPanel>g__StatStripDataFromCompletionFraction|26_15("STATNAME_COMPLETION_ACHIEVEMENTS", gameCompletionStatsHelper.GetAchievementCompletion(userProfile));
			array3[num25] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num26 = 1;
			valueTuple = PageBuilder.<StatsPanel>g__StatStripDataFromCompletionFraction|26_15("STATNAME_COMPLETION_COLLECTIBLES", gameCompletionStatsHelper.GetCollectibleCompletion(userProfile));
			array3[num26] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num27 = 2;
			valueTuple = PageBuilder.<StatsPanel>g__StatStripDataFromCompletionFraction|26_15("STATNAME_COMPLETION_PICKUPDISCOVERY", gameCompletionStatsHelper.GetPickupEncounterCompletion(userProfile));
			array3[num27] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num28 = 3;
			valueTuple = PageBuilder.<StatsPanel>g__StatStripDataFromCompletionFraction|26_15("STATNAME_COMPLETION_SURVIVORSPICKED", gameCompletionStatsHelper.GetSurvivorPickCompletion(userProfile));
			array3[num28] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			int num29 = 4;
			valueTuple = PageBuilder.<StatsPanel>g__StatStripDataFromCompletionFraction|26_15("STATNAME_COMPLETION_SURVIVORSWON", gameCompletionStatsHelper.GetSurvivorWinCompletion(userProfile));
			array3[num29] = new ValueTuple<string, string, Texture>(valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
			PageBuilder.<StatsPanel>g__SetStats|26_13(rectTransform11, array3, ref CS$<>8__locals4);
			IntFraction totalCompletion = gameCompletionStatsHelper.GetTotalCompletion(userProfile);
			float num30 = (float)totalCompletion;
			Vector2 anchorMax = rectTransform2.anchorMax;
			anchorMax.x = num30;
			rectTransform2.anchorMax = anchorMax;
			rectTransform3.GetComponent<TMP_Text>().SetText(string.Format("{0:0%}", num30), true);
		}

		// Token: 0x0600520E RID: 21006 RVA: 0x0015433C File Offset: 0x0015253C
		public void AddRunReportPanel(RunReport runReport)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/UI/GameEndReportPanelScrolling"), this.container);
			gameObject.GetComponent<GameEndReportPanelController>().SetDisplayData(new GameEndReportPanelController.DisplayData
			{
				runReport = runReport,
				playerIndex = 0
			});
			gameObject.GetComponent<MPEventSystemProvider>().fallBackToMainEventSystem = true;
			this.managedObjects.Add(gameObject);
		}

		// Token: 0x0600520F RID: 21007 RVA: 0x0015439B File Offset: 0x0015259B
		public static void RunReportPanel(PageBuilder builder)
		{
			builder.AddRunReportPanel((RunReport)builder.entry.extraData);
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x001543D4 File Offset: 0x001525D4
		[CompilerGenerated]
		internal static Color <StatsPanel>g__GetBodyColor|26_0(BodyIndex bodyIndex)
		{
			CharacterBody bodyPrefabBodyComponent = BodyCatalog.GetBodyPrefabBodyComponent(bodyIndex);
			if (bodyPrefabBodyComponent.bodyColor != Color.clear)
			{
				return bodyPrefabBodyComponent.bodyColor;
			}
			string bodyName = BodyCatalog.GetBodyName(bodyIndex);
			ulong num = 0UL;
			for (int i = 0; i < bodyName.Length; i++)
			{
				num += (ulong)bodyName[i];
			}
			Xoroshiro128Plus xoroshiro128Plus = new Xoroshiro128Plus(num);
			return Color.HSVToRGB(xoroshiro128Plus.nextNormalizedFloat, xoroshiro128Plus.RangeFloat(0.5f, 1f), xoroshiro128Plus.RangeFloat(0.6f, 0.8f));
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x00154460 File Offset: 0x00152660
		[CompilerGenerated]
		internal static void <StatsPanel>g__SetStats|26_13(RectTransform container, [TupleElementNames(new string[]
		{
			"name",
			"value",
			"texture"
		})] ValueTuple<string, string, Texture>[] data, ref PageBuilder.<>c__DisplayClass26_5 A_2)
		{
			UIElementAllocator<ChildLocator> uielementAllocator = new UIElementAllocator<ChildLocator>(container, A_2.statStripPrefab, true, true);
			uielementAllocator.AllocateElements(data.Length);
			ReadOnlyCollection<ChildLocator> elements = uielementAllocator.elements;
			for (int i = 0; i < data.Length; i++)
			{
				ValueTuple<string, string, Texture> valueTuple = data[i];
				string item = valueTuple.Item1;
				string item2 = valueTuple.Item2;
				Texture item3 = valueTuple.Item3;
				ChildLocator childLocator = elements[i];
				childLocator.FindChild("NameLabel").GetComponent<TMP_Text>().SetText(item, true);
				childLocator.FindChild("ValueLabel").GetComponent<TMP_Text>().SetText("<color=#FFFF7F>" + item2 + "</color>", true);
				RawImage component = childLocator.FindChild("IconRawImage").GetComponent<RawImage>();
				component.transform.parent.gameObject.SetActive(item3);
				component.texture = item3;
			}
		}

		// Token: 0x06005214 RID: 21012 RVA: 0x00154530 File Offset: 0x00152730
		[CompilerGenerated]
		[return: TupleElementNames(new string[]
		{
			"name",
			"value",
			"texture"
		})]
		internal static ValueTuple<string, string, Texture2D> <StatsPanel>g__StatStripDataFromCompletionFraction|26_15(string displayToken, IntFraction completionFraction)
		{
			ValueTuple<string, string, Texture2D> result;
			result.Item1 = Language.GetString(displayToken);
			result.Item2 = Language.GetStringFormatted("STAT_COMPLETION_VALUE_FORMAT", new object[]
			{
				completionFraction.numerator,
				completionFraction.denominator,
				(float)completionFraction
			});
			result.Item3 = null;
			return result;
		}

		// Token: 0x04004E57 RID: 20055
		private static readonly StringBuilder sharedStringBuilder = new StringBuilder();

		// Token: 0x04004E58 RID: 20056
		public UserProfile userProfile;

		// Token: 0x04004E59 RID: 20057
		public RectTransform container;

		// Token: 0x04004E5A RID: 20058
		public Entry entry;

		// Token: 0x04004E5B RID: 20059
		public readonly List<GameObject> managedObjects = new List<GameObject>();
	}
}
