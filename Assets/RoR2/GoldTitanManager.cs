using System;
using System.Collections.Generic;
using HG;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000920 RID: 2336
	public static class GoldTitanManager
	{
		// Token: 0x060034D5 RID: 13525 RVA: 0x000DF990 File Offset: 0x000DDB90
		[SystemInitializer(new Type[]
		{
			typeof(ItemCatalog),
			typeof(MasterCatalog)
		})]
		private static void Init()
		{
			Run.onRunStartGlobal += GoldTitanManager.OnRunStartGlobal;
			Run.onRunDestroyGlobal += GoldTitanManager.OnRunDestroyGlobal;
			TeleporterInteraction.onTeleporterBeginChargingGlobal += GoldTitanManager.OnTeleporterBeginChargingGlobal;
			TeleporterInteraction.onTeleporterChargedGlobal += GoldTitanManager.OnTeleporterChargedGlobal;
			BossGroup.onBossGroupStartServer += GoldTitanManager.OnBossGroupStartServer;
			GoldTitanManager.goldTitanSpawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscTitanGoldAlly");
			ItemDef titanGoldDuringTP = RoR2Content.Items.TitanGoldDuringTP;
			GoldTitanManager.goldTitanItemIndex = ((titanGoldDuringTP != null) ? titanGoldDuringTP.itemIndex : ItemIndex.None);
			GoldTitanManager.brotherHurtMasterIndex = MasterCatalog.FindMasterIndex("BrotherHurtMaster");
		}

		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x060034D6 RID: 13526 RVA: 0x000DFA28 File Offset: 0x000DDC28
		// (remove) Token: 0x060034D7 RID: 13527 RVA: 0x000DFA5C File Offset: 0x000DDC5C
		private static event Action onChannelEnd;

		// Token: 0x060034D8 RID: 13528 RVA: 0x000DFA90 File Offset: 0x000DDC90
		private static void CalcTitanPowerAndBestTeam(out int totalItemCount, out TeamIndex teamIndex)
		{
			TeamIndex teamIndex2 = TeamIndex.None;
			int num = 0;
			totalItemCount = 0;
			for (TeamIndex teamIndex3 = TeamIndex.Neutral; teamIndex3 < TeamIndex.Count; teamIndex3 += 1)
			{
				int itemCountForTeam = Util.GetItemCountForTeam(teamIndex3, GoldTitanManager.goldTitanItemIndex, true, true);
				if (itemCountForTeam > num)
				{
					num = itemCountForTeam;
					teamIndex2 = teamIndex3;
				}
				totalItemCount += itemCountForTeam;
			}
			teamIndex = teamIndex2;
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x000DFAD0 File Offset: 0x000DDCD0
		private static void KillTitansInList(List<CharacterMaster> titansList)
		{
			try
			{
				foreach (CharacterMaster characterMaster in titansList)
				{
					if (characterMaster)
					{
						characterMaster.TrueKill();
					}
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x000DFB3C File Offset: 0x000DDD3C
		private static bool GoldTitanItemFilter(ItemIndex itemIndex)
		{
			return itemIndex == GoldTitanManager.goldTitanItemIndex;
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x0000CF8A File Offset: 0x0000B18A
		private static bool NoItemFilter(ItemIndex itemIndex)
		{
			return false;
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x0000B4B7 File Offset: 0x000096B7
		private static bool AllCharacterMastersFilter(CharacterMaster characterMaster)
		{
			return true;
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x000DFB48 File Offset: 0x000DDD48
		private static bool TryStartChannelingTitansServer(object channeler, Vector3 approximatePosition, Vector3? lookAtPosition = null, Action channelEndCallback = null)
		{
			GoldTitanManager.<>c__DisplayClass19_0 CS$<>8__locals1 = new GoldTitanManager.<>c__DisplayClass19_0();
			CS$<>8__locals1.lookAtPosition = lookAtPosition;
			int num;
			TeamIndex teamIndex;
			GoldTitanManager.CalcTitanPowerAndBestTeam(out num, out teamIndex);
			if (num <= 0)
			{
				return false;
			}
			CS$<>8__locals1.newTitans = CollectionPool<CharacterMaster, List<CharacterMaster>>.RentCollection();
			bool result;
			try
			{
				GoldTitanManager.<>c__DisplayClass19_1 CS$<>8__locals2 = new GoldTitanManager.<>c__DisplayClass19_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				DirectorPlacementRule placementRule = new DirectorPlacementRule
				{
					placementMode = DirectorPlacementRule.PlacementMode.NearestNode,
					minDistance = 20f,
					maxDistance = 130f,
					position = approximatePosition
				};
				DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(GoldTitanManager.goldTitanSpawnCard, placementRule, GoldTitanManager.rng);
				directorSpawnRequest.ignoreTeamMemberLimit = true;
				directorSpawnRequest.teamIndexOverride = new TeamIndex?(TeamIndex.Player);
				CS$<>8__locals2.currentBoostHpCoefficient = 1f;
				CS$<>8__locals2.currentBoostDamageCoefficient = 1f;
				CS$<>8__locals2.currentBoostHpCoefficient *= Mathf.Pow((float)num, 1f);
				CS$<>8__locals2.currentBoostDamageCoefficient *= Mathf.Pow((float)num, 0.5f);
				directorSpawnRequest.onSpawnedServer = new Action<SpawnCard.SpawnResult>(CS$<>8__locals2.<TryStartChannelingTitansServer>g__OnSpawnedServer|0);
				DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
				if (CS$<>8__locals2.CS$<>8__locals1.newTitans.Count > 0)
				{
					GoldTitanManager.EndChannelingTitansServer(GoldTitanManager.currentChanneler);
					GoldTitanManager.onChannelEnd = channelEndCallback;
					GoldTitanManager.currentChanneler = channeler;
					ListUtils.AddRange<CharacterMaster, List<CharacterMaster>>(GoldTitanManager.currentTitans, CS$<>8__locals2.CS$<>8__locals1.newTitans);
				}
				result = true;
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				GoldTitanManager.KillTitansInList(CS$<>8__locals1.newTitans);
				result = false;
			}
			finally
			{
				CollectionPool<CharacterMaster, List<CharacterMaster>>.ReturnCollection(CS$<>8__locals1.newTitans);
			}
			return result;
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x000DFCE0 File Offset: 0x000DDEE0
		private static void EndChannelingTitansServer(object channeler)
		{
			if (channeler == null)
			{
				return;
			}
			if (channeler == GoldTitanManager.currentChanneler)
			{
				GoldTitanManager.currentChanneler = null;
				GoldTitanManager.KillTitansInList(GoldTitanManager.currentTitans);
				GoldTitanManager.currentTitans.Clear();
				Action action = GoldTitanManager.onChannelEnd;
				GoldTitanManager.onChannelEnd = null;
				try
				{
					if (action != null)
					{
						action();
					}
				}
				catch (Exception message)
				{
					Debug.LogError(message);
				}
			}
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x000DFD44 File Offset: 0x000DDF44
		private static bool TryStartChannelingAgainstCombatSquadServer(CombatSquad combatSquad)
		{
			GoldTitanManager.<>c__DisplayClass21_0 CS$<>8__locals1 = new GoldTitanManager.<>c__DisplayClass21_0();
			CS$<>8__locals1.combatSquad = combatSquad;
			if (!CS$<>8__locals1.combatSquad)
			{
				return false;
			}
			List<Vector3> list = CollectionPool<Vector3, List<Vector3>>.RentCollection();
			List<Vector3> list2 = CollectionPool<Vector3, List<Vector3>>.RentCollection();
			bool result;
			try
			{
				CS$<>8__locals1.combatSquad.onDefeatedServer += CS$<>8__locals1.<TryStartChannelingAgainstCombatSquadServer>g__EndChannelingWhenDefeated|0;
				foreach (CharacterMaster characterMaster in CharacterMaster.readOnlyInstancesList)
				{
					CharacterBody body = characterMaster.GetBody();
					if (body && characterMaster.inventory.GetItemCount(GoldTitanManager.goldTitanItemIndex) > 0)
					{
						list2.Add(body.corePosition);
					}
				}
				foreach (CharacterMaster characterMaster2 in CS$<>8__locals1.combatSquad.readOnlyMembersList)
				{
					CharacterBody body2 = characterMaster2.GetBody();
					if (body2)
					{
						list.Add(body2.corePosition);
					}
				}
				if (list2.Count == 0 || list.Count == 0)
				{
					if (list2.Count == list.Count)
					{
						Vector3 position = CS$<>8__locals1.combatSquad.transform.position;
						list2.Add(position);
						list.Add(position);
					}
					else
					{
						List<Vector3> dest = (list2.Count == 0) ? list2 : list;
						List<Vector3> src = (list2.Count != 0) ? list2 : list;
						ListUtils.AddRange<Vector3, List<Vector3>>(dest, src);
					}
				}
				Vector3 vector = Vector3Utils.AveragePrecise<List<Vector3>>(list);
				Vector3 approximatePosition = Vector3.Lerp(Vector3Utils.AveragePrecise<List<Vector3>>(list2), vector, 0.15f);
				result = GoldTitanManager.TryStartChannelingTitansServer(CS$<>8__locals1.combatSquad, approximatePosition, new Vector3?(vector), delegate
				{
					CS$<>8__locals1.combatSquad.onDefeatedServer -= base.<TryStartChannelingAgainstCombatSquadServer>g__EndChannelingWhenDefeated|0;
				});
			}
			catch (Exception message)
			{
				Debug.LogError(message);
				result = false;
			}
			finally
			{
				CollectionPool<Vector3, List<Vector3>>.ReturnCollection(list2);
				CollectionPool<Vector3, List<Vector3>>.ReturnCollection(list);
			}
			return result;
		}

		// Token: 0x060034E0 RID: 13536 RVA: 0x000DFF60 File Offset: 0x000DE160
		private static void OnRunStartGlobal(Run run)
		{
			if (!NetworkServer.active)
			{
				return;
			}
			GoldTitanManager.rng.ResetSeed(run.seed + 88888888UL);
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x000DFF81 File Offset: 0x000DE181
		private static void OnRunDestroyGlobal(Run run)
		{
			if (!NetworkServer.active)
			{
				return;
			}
			GoldTitanManager.EndChannelingTitansServer(GoldTitanManager.currentChanneler);
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x000DFF98 File Offset: 0x000DE198
		private static void OnTeleporterBeginChargingGlobal(TeleporterInteraction teleporter)
		{
			if (!NetworkServer.active)
			{
				return;
			}
			GoldTitanManager.TryStartChannelingTitansServer(teleporter, teleporter.transform.position, null, null);
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x000DFFC9 File Offset: 0x000DE1C9
		private static void OnTeleporterChargedGlobal(TeleporterInteraction teleporter)
		{
			if (!NetworkServer.active)
			{
				return;
			}
			GoldTitanManager.EndChannelingTitansServer(teleporter);
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000DFFDC File Offset: 0x000DE1DC
		private static void OnBossGroupStartServer(BossGroup bossGroup)
		{
			GoldTitanManager.<>c__DisplayClass26_0 CS$<>8__locals1 = new GoldTitanManager.<>c__DisplayClass26_0();
			CS$<>8__locals1.combatSquad = bossGroup.combatSquad;
			bool flag = false;
			using (IEnumerator<CharacterMaster> enumerator = CS$<>8__locals1.combatSquad.readOnlyMembersList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.masterIndex == GoldTitanManager.brotherHurtMasterIndex)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				GoldTitanManager.<>c__DisplayClass26_1 CS$<>8__locals2 = new GoldTitanManager.<>c__DisplayClass26_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.timer = 2f;
				RoR2Application.onFixedUpdate += CS$<>8__locals2.<OnBossGroupStartServer>g__Check|0;
			}
		}

		// Token: 0x040035CA RID: 13770
		private static CharacterSpawnCard goldTitanSpawnCard;

		// Token: 0x040035CB RID: 13771
		private static ItemIndex goldTitanItemIndex;

		// Token: 0x040035CC RID: 13772
		private static MasterCatalog.MasterIndex brotherHurtMasterIndex;

		// Token: 0x040035CD RID: 13773
		private static readonly Xoroshiro128Plus rng = new Xoroshiro128Plus(0UL);

		// Token: 0x040035CE RID: 13774
		private static object currentChanneler;

		// Token: 0x040035CF RID: 13775
		private static readonly List<CharacterMaster> currentTitans = new List<CharacterMaster>();

		// Token: 0x040035D1 RID: 13777
		private static readonly Func<ItemIndex, bool> goldTitanItemFilterDelegate = new Func<ItemIndex, bool>(GoldTitanManager.GoldTitanItemFilter);

		// Token: 0x040035D2 RID: 13778
		private static readonly Func<ItemIndex, bool> noItemFilterDelegate = new Func<ItemIndex, bool>(GoldTitanManager.NoItemFilter);

		// Token: 0x040035D3 RID: 13779
		private static readonly Func<CharacterMaster, bool> allCharacterMastersFilterDelegate = new Func<CharacterMaster, bool>(GoldTitanManager.AllCharacterMastersFilter);

		// Token: 0x02000921 RID: 2337
		private class RemoveItemStealOnDeath : MonoBehaviour
		{
		}
	}
}
