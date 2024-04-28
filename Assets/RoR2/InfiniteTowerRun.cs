using System;
using System.Runtime.InteropServices;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006EA RID: 1770
	public class InfiniteTowerRun : Run
	{
		// Token: 0x14000044 RID: 68
		// (add) Token: 0x060022FB RID: 8955 RVA: 0x00096E1C File Offset: 0x0009501C
		// (remove) Token: 0x060022FC RID: 8956 RVA: 0x00096E50 File Offset: 0x00095050
		public static event Action<InfiniteTowerWaveController> onWaveInitialized;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x060022FD RID: 8957 RVA: 0x00096E84 File Offset: 0x00095084
		// (remove) Token: 0x060022FE RID: 8958 RVA: 0x00096EB8 File Offset: 0x000950B8
		public static event Action<InfiniteTowerWaveController> onAllEnemiesDefeatedServer;

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x00096EEB File Offset: 0x000950EB
		public int waveIndex
		{
			get
			{
				return this._waveIndex;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x00096EF3 File Offset: 0x000950F3
		public InfiniteTowerWaveController waveController
		{
			get
			{
				return this._waveController;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override bool spawnWithPod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06002302 RID: 8962 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override bool canFamilyEventTrigger
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override bool autoGenerateSpawnPoints
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x00096EFB File Offset: 0x000950FB
		public GameObject waveInstance
		{
			get
			{
				return Util.FindNetworkObject(this.waveInstanceId);
			}
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x00096F08 File Offset: 0x00095108
		public override GameObject InstantiateUi(Transform uiRoot)
		{
			base.InstantiateUi(uiRoot);
			if (this._waveController)
			{
				this._waveController.InstantiateUi(base.uiInstance.transform);
			}
			return base.uiInstance;
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x00096F3C File Offset: 0x0009513C
		public override void OverrideRuleChoices(RuleChoiceMask mustInclude, RuleChoiceMask mustExclude, ulong runSeed)
		{
			base.OverrideRuleChoices(mustInclude, mustExclude, base.seed);
			ItemIndex itemIndex = (ItemIndex)0;
			ItemIndex itemCount = (ItemIndex)ItemCatalog.itemCount;
			while (itemIndex < itemCount)
			{
				ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
				bool flag = Array.IndexOf<ItemDef>(this.blacklistedItems, itemDef) != -1;
				if (!flag)
				{
					foreach (ItemTag tag in this.blacklistedTags)
					{
						if (itemDef.ContainsTag(tag))
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					RuleDef ruleDef = RuleCatalog.FindRuleDef("Items." + itemDef.name);
					RuleChoiceDef ruleChoiceDef = (ruleDef != null) ? ruleDef.FindChoice("Off") : null;
					if (ruleChoiceDef != null)
					{
						base.ForceChoice(mustInclude, mustExclude, ruleChoiceDef);
					}
				}
				itemIndex++;
			}
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x00096FF8 File Offset: 0x000951F8
		[Server]
		public void ResetSafeWard()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerRun::ResetSafeWard()' called on client");
				return;
			}
			if (this.safeWardController)
			{
				if (this.fogDamageController)
				{
					this.fogDamageController.RemoveSafeZone(this.safeWardController.safeZone);
				}
				this.safeWardController.SelfDestruct();
			}
			this.SpawnSafeWard(this.safeWardCard, new DirectorPlacementRule
			{
				placementMode = DirectorPlacementRule.PlacementMode.Random
			});
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x00097070 File Offset: 0x00095270
		[Server]
		public void MoveSafeWard()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerRun::MoveSafeWard()' called on client");
				return;
			}
			if (this.safeWardController)
			{
				this.safeWardController.RandomizeLocation(this.safeWardRng);
				this.safeWardController.onActivated += this.OnSafeWardActivated;
			}
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x000970C7 File Offset: 0x000952C7
		public bool IsStageTransitionWave()
		{
			return this.stageTransitionPeriod == 0 || this.waveIndex % this.stageTransitionPeriod == 0;
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x000970E3 File Offset: 0x000952E3
		public override Vector3 FindSafeTeleportPosition(CharacterBody characterBody, Transform targetDestination)
		{
			if (this.safeWardController && !targetDestination)
			{
				return base.FindSafeTeleportPosition(characterBody, this.safeWardController.transform);
			}
			return base.FindSafeTeleportPosition(characterBody, targetDestination);
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x00097115 File Offset: 0x00095315
		public override Vector3 FindSafeTeleportPosition(CharacterBody characterBody, Transform targetDestination, float idealMinDistance, float idealMaxDistance)
		{
			if (this.safeWardController && !targetDestination)
			{
				return base.FindSafeTeleportPosition(characterBody, this.safeWardController.transform, idealMinDistance, idealMaxDistance);
			}
			return base.FindSafeTeleportPosition(characterBody, targetDestination, idealMinDistance, idealMaxDistance);
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x00097150 File Offset: 0x00095350
		[Server]
		private void OnSafeWardActivated(InfiniteTowerSafeWardController safeWard)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerRun::OnSafeWardActivated(RoR2.InfiniteTowerSafeWardController)' called on client");
				return;
			}
			this.safeWardController.onActivated -= this.OnSafeWardActivated;
			if (this._waveController)
			{
				this._waveController.ForceFinish();
				this.CleanUpCurrentWave();
			}
			this.BeginNextWave();
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x000971B0 File Offset: 0x000953B0
		[Server]
		private void AdvanceWave()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerRun::AdvanceWave()' called on client");
				return;
			}
			this.Network_waveIndex = this._waveIndex + 1;
			if (this._waveIndex % this.enemyItemPeriod == 0)
			{
				InfiniteTowerRun.EnemyItemEntry[] array = this.enemyItemPattern;
				int num = this.enemyItemPatternIndex;
				this.enemyItemPatternIndex = num + 1;
				InfiniteTowerRun.EnemyItemEntry enemyItemEntry = array[num % this.enemyItemPattern.Length];
				if (!enemyItemEntry.dropTable)
				{
					return;
				}
				PickupIndex pickupIndex = enemyItemEntry.dropTable.GenerateDrop(this.enemyItemRng);
				if (pickupIndex != PickupIndex.none)
				{
					PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
					if (pickupDef != null)
					{
						this.enemyInventory.GiveItem(pickupDef.itemIndex, enemyItemEntry.stacks);
						Chat.SendBroadcastChat(new Chat.PlayerPickupChatMessage
						{
							baseToken = "INFINITETOWER_ADD_ITEM",
							pickupToken = pickupDef.nameToken,
							pickupColor = pickupDef.baseColor
						});
					}
				}
			}
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x00097298 File Offset: 0x00095498
		[Server]
		private void BeginNextWave()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerRun::BeginNextWave()' called on client");
				return;
			}
			this.AdvanceWave();
			GameObject original = this.defaultWavePrefab;
			foreach (InfiniteTowerWaveCategory infiniteTowerWaveCategory in this.waveCategories)
			{
				if (infiniteTowerWaveCategory.IsAvailable(this))
				{
					original = infiniteTowerWaveCategory.SelectWavePrefab(this, this.waveRng);
					break;
				}
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original, base.transform);
			NetworkServer.Spawn(gameObject);
			this.NetworkwaveInstanceId = gameObject.GetComponent<NetworkIdentity>().netId;
			this.RecalculateDifficultyCoefficentInternal();
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x00097326 File Offset: 0x00095526
		protected override void Start()
		{
			Stage.onServerStageBegin += this.OnServerStageBegin;
			Stage.onServerStageComplete += this.OnServerStageComplete;
			SceneDirector.onPrePopulateSceneServer += this.OnPrePopulateSceneServer;
			base.Start();
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x00097364 File Offset: 0x00095564
		protected override void OnDestroy()
		{
			Stage.onServerStageBegin -= this.OnServerStageBegin;
			Stage.onServerStageComplete -= this.OnServerStageComplete;
			SceneDirector.onPrePopulateSceneServer -= this.OnPrePopulateSceneServer;
			if (this.safeWardController)
			{
				UnityEngine.Object.Destroy(this.safeWardController.gameObject);
				this.safeWardController = null;
			}
			this.CleanUpCurrentWave();
			base.OnDestroy();
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000026ED File Offset: 0x000008ED
		private void OnServerStageBegin(Stage stage)
		{
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000973D4 File Offset: 0x000955D4
		private void OnServerStageComplete(Stage stage)
		{
			this.PerformStageCleanUp();
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x000973DC File Offset: 0x000955DC
		private void OnPrePopulateSceneServer(SceneDirector sceneDirector)
		{
			this.PerformStageCleanUp();
			if (this.fogDamagePrefab)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.fogDamagePrefab, Stage.instance.transform);
				NetworkServer.Spawn(gameObject);
				this.fogDamageController = gameObject.GetComponent<FogDamageController>();
			}
			sceneDirector.interactableCredit = this.interactableCredits;
			DirectorPlacementRule placementRule = new DirectorPlacementRule
			{
				placementMode = DirectorPlacementRule.PlacementMode.Random
			};
			this.SpawnSafeWard(this.initialSafeWardCard, placementRule);
			if (this.safeWardController)
			{
				Vector3 position = this.safeWardController.transform.position;
				NodeGraph nodeGraph = SceneInfo.instance.GetNodeGraph(MapNodeGroup.GraphType.Ground);
				foreach (NodeGraph.NodeIndex nodeIndex in nodeGraph.FindNodesInRangeWithFlagConditions(position, 0f, this.spawnMaxRadius, HullMask.Human, NodeFlags.None, NodeFlags.NoCharacterSpawn, false))
				{
					Vector3 position2;
					if (nodeGraph.GetNodePosition(nodeIndex, out position2))
					{
						SpawnPoint.AddSpawnPoint(position2, Quaternion.LookRotation(position, Vector3.up));
					}
				}
			}
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x000974E4 File Offset: 0x000956E4
		protected override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && this.waveInstance && this.waveController && this.waveController.isFinished && !this.IsStageTransitionWave())
			{
				this.CleanUpCurrentWave();
				this.BeginNextWave();
			}
			if (this.waveInstance)
			{
				if (!this._waveController)
				{
					this.InitializeWaveController();
					return;
				}
			}
			else
			{
				this._waveController = null;
			}
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x00097564 File Offset: 0x00095764
		protected override void RecalculateDifficultyCoefficentInternal()
		{
			DifficultyDef difficultyDef = DifficultyCatalog.GetDifficultyDef(base.selectedDifficulty);
			float num = 1.5f * (float)this.waveIndex;
			float num2 = 0.0506f * difficultyDef.scalingValue;
			float num3 = Mathf.Pow(1.02f, (float)this.waveIndex);
			this.difficultyCoefficient = (1f + num2 * num) * num3;
			this.compensatedDifficultyCoefficient = this.difficultyCoefficient;
			base.ambientLevel = Mathf.Min((this.difficultyCoefficient - 1f) / 0.33f + 1f, 9999f);
			int ambientLevelFloor = base.ambientLevelFloor;
			base.ambientLevelFloor = Mathf.FloorToInt(base.ambientLevel);
			if (ambientLevelFloor != base.ambientLevelFloor && ambientLevelFloor != 0 && base.ambientLevelFloor > ambientLevelFloor)
			{
				base.OnAmbientLevelUp();
			}
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x00097628 File Offset: 0x00095828
		private void InitializeWaveController()
		{
			this._waveController = this.waveInstance.GetComponent<InfiniteTowerWaveController>();
			if (this._waveController)
			{
				if (NetworkServer.active)
				{
					this._waveController.Initialize(this._waveIndex, this.enemyInventory, this.safeWardController.gameObject);
				}
				if (base.uiInstance)
				{
					this._waveController.InstantiateUi(base.uiInstance.transform);
				}
				this._waveController.PlayBeginSound();
				this._waveController.defaultEnemyIndicatorPrefab = this.defaultWaveEnemyIndicatorPrefab;
				this._waveController.onAllEnemiesDefeatedServer += this.OnWaveAllEnemiesDefeatedServer;
				Action<InfiniteTowerWaveController> action = InfiniteTowerRun.onWaveInitialized;
				if (action == null)
				{
					return;
				}
				action(this._waveController);
			}
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x000976EC File Offset: 0x000958EC
		private void OnWaveAllEnemiesDefeatedServer(InfiniteTowerWaveController wc)
		{
			Action<InfiniteTowerWaveController> action = InfiniteTowerRun.onAllEnemiesDefeatedServer;
			if (action != null)
			{
				action(wc);
			}
			if (!base.isGameOverServer)
			{
				foreach (PlayerCharacterMasterController playerCharacterMasterController in PlayerCharacterMasterController.instances)
				{
					CharacterMaster master = playerCharacterMasterController.master;
					if (playerCharacterMasterController.isConnected && master.IsDeadAndOutOfLivesServer())
					{
						Vector3 vector = master.deathFootPosition;
						if (this.safeWardController)
						{
							vector = (TeleportHelper.FindSafeTeleportDestination(this.safeWardController.transform.position, master.bodyPrefab.GetComponent<CharacterBody>(), RoR2Application.rng) ?? vector);
						}
						master.Respawn(vector, Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f));
						CharacterBody body = master.GetBody();
						if (body)
						{
							body.AddTimedBuff(RoR2Content.Buffs.Immune, 3f);
							foreach (EntityStateMachine entityStateMachine in body.GetComponents<EntityStateMachine>())
							{
								entityStateMachine.initialStateType = entityStateMachine.mainStateType;
							}
							if (this.playerRespawnEffectPrefab)
							{
								EffectManager.SpawnEffect(this.playerRespawnEffectPrefab, new EffectData
								{
									origin = vector,
									rotation = body.transform.rotation
								}, true);
							}
						}
					}
				}
				if (this.IsStageTransitionWave())
				{
					base.PickNextStageSceneFromCurrentSceneDestinations();
					DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(this.stageTransitionPortalCard, new DirectorPlacementRule
					{
						minDistance = 0f,
						maxDistance = this.stageTransitionPortalMaxDistance,
						placementMode = DirectorPlacementRule.PlacementMode.Approximate,
						position = this.safeWardController.transform.position,
						spawnOnTarget = this.safeWardController.transform
					}, this.safeWardRng));
					Chat.SendBroadcastChat(new Chat.SimpleChatMessage
					{
						baseToken = this.stageTransitionChatToken
					});
					if (this.safeWardController)
					{
						this.safeWardController.WaitForPortal();
					}
				}
			}
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool ShouldUpdateRunStopwatch()
		{
			return true;
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x00097914 File Offset: 0x00095B14
		protected override void OnSeedSet()
		{
			this.waveRng = new Xoroshiro128Plus(base.seed ^ 14312UL);
			this.enemyItemRng = new Xoroshiro128Plus(base.seed ^ 1535UL);
			this.safeWardRng = new Xoroshiro128Plus(base.seed ^ 769876UL);
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x0009796C File Offset: 0x00095B6C
		private void CleanUpCurrentWave()
		{
			if (this._waveController)
			{
				this._waveController.onAllEnemiesDefeatedServer -= this.OnWaveAllEnemiesDefeatedServer;
				this._waveController = null;
			}
			if (this.waveInstance)
			{
				UnityEngine.Object.Destroy(this.waveInstance);
			}
			this.NetworkwaveInstanceId = NetworkInstanceId.Invalid;
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x000979C8 File Offset: 0x00095BC8
		private void SpawnSafeWard(InteractableSpawnCard spawnCard, DirectorPlacementRule placementRule)
		{
			GameObject gameObject = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(spawnCard, placementRule, this.safeWardRng));
			if (gameObject)
			{
				NetworkServer.Spawn(gameObject);
				this.safeWardController = gameObject.GetComponent<InfiniteTowerSafeWardController>();
				if (this.safeWardController)
				{
					this.safeWardController.onActivated += this.OnSafeWardActivated;
				}
				HoldoutZoneController component = gameObject.GetComponent<HoldoutZoneController>();
				if (component)
				{
					component.calcAccumulatedCharge += this.CalcHoldoutZoneCharge;
				}
				if (this.fogDamageController)
				{
					this.fogDamageController.AddSafeZone(this.safeWardController.safeZone);
					return;
				}
			}
			else
			{
				Debug.LogError("Unable to spawn safe ward instance.  Are there any ground nodes?");
			}
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x00097A7C File Offset: 0x00095C7C
		private void CalcHoldoutZoneCharge(ref float charge)
		{
			if (this.waveController)
			{
				float num = this.waveController.GetNormalizedProgress();
				if (this.waveController.GetSquadCount() > 0)
				{
					num = Mathf.Min(num, 0.99f);
				}
				charge = num;
				return;
			}
			charge = 0f;
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x00097AC7 File Offset: 0x00095CC7
		[Server]
		public override void HandlePlayerFirstEntryAnimation(CharacterBody body, Vector3 spawnPosition, Quaternion spawnRotation)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerRun::HandlePlayerFirstEntryAnimation(RoR2.CharacterBody,UnityEngine.Vector3,UnityEngine.Quaternion)' called on client");
				return;
			}
			body.SetBodyStateToPreferredInitialState();
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x00097AE4 File Offset: 0x00095CE4
		private void PerformStageCleanUp()
		{
			this.safeWardController = null;
			if (this.fogDamageController)
			{
				UnityEngine.Object.Destroy(this.fogDamageController.gameObject);
			}
			this.CleanUpCurrentWave();
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06002321 RID: 8993 RVA: 0x00097B10 File Offset: 0x00095D10
		// (set) Token: 0x06002322 RID: 8994 RVA: 0x00097B23 File Offset: 0x00095D23
		public int Network_waveIndex
		{
			get
			{
				return this._waveIndex;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this._waveIndex, 64U);
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x00097B38 File Offset: 0x00095D38
		// (set) Token: 0x06002324 RID: 8996 RVA: 0x00097B4B File Offset: 0x00095D4B
		public NetworkInstanceId NetworkwaveInstanceId
		{
			get
			{
				return this.waveInstanceId;
			}
			[param: In]
			set
			{
				base.SetSyncVar<NetworkInstanceId>(value, ref this.waveInstanceId, 128U);
			}
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x00097B60 File Offset: 0x00095D60
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool flag = base.OnSerialize(writer, forceAll);
			if (forceAll)
			{
				writer.WritePackedUInt32((uint)this._waveIndex);
				writer.Write(this.waveInstanceId);
				return true;
			}
			bool flag2 = false;
			if ((base.syncVarDirtyBits & 64U) != 0U)
			{
				if (!flag2)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag2 = true;
				}
				writer.WritePackedUInt32((uint)this._waveIndex);
			}
			if ((base.syncVarDirtyBits & 128U) != 0U)
			{
				if (!flag2)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag2 = true;
				}
				writer.Write(this.waveInstanceId);
			}
			if (!flag2)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag2 || flag;
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x00097C18 File Offset: 0x00095E18
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			base.OnDeserialize(reader, initialState);
			if (initialState)
			{
				this._waveIndex = (int)reader.ReadPackedUInt32();
				this.waveInstanceId = reader.ReadNetworkId();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 64) != 0)
			{
				this._waveIndex = (int)reader.ReadPackedUInt32();
			}
			if ((num & 128) != 0)
			{
				this.waveInstanceId = reader.ReadNetworkId();
			}
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x0007597B File Offset: 0x00073B7B
		public override void PreStartClient()
		{
			base.PreStartClient();
		}

		// Token: 0x040027FD RID: 10237
		private const ulong waveRngSalt = 14312UL;

		// Token: 0x040027FE RID: 10238
		private const ulong enemyItemRngSalt = 1535UL;

		// Token: 0x040027FF RID: 10239
		private const ulong safeWardRngSalt = 769876UL;

		// Token: 0x04002802 RID: 10242
		[Header("Infinite Tower Settings")]
		[Tooltip("If all else fails, use this wave prefab")]
		[SerializeField]
		private GameObject defaultWavePrefab;

		// Token: 0x04002803 RID: 10243
		[Tooltip("Selects a wave from the first available category in this list")]
		[SerializeField]
		private InfiniteTowerWaveCategory[] waveCategories;

		// Token: 0x04002804 RID: 10244
		[Tooltip("Use this indicator for enemies by default")]
		[SerializeField]
		private GameObject defaultWaveEnemyIndicatorPrefab;

		// Token: 0x04002805 RID: 10245
		[Tooltip("The repeating pattern of drop tables to use when selecting items for the enemy team")]
		[SerializeField]
		private InfiniteTowerRun.EnemyItemEntry[] enemyItemPattern;

		// Token: 0x04002806 RID: 10246
		[Tooltip("The number of waves before you give the enemy team the next item in the pattern (e.g., \"every Nth wave\")")]
		[SerializeField]
		private int enemyItemPeriod;

		// Token: 0x04002807 RID: 10247
		[SerializeField]
		[Tooltip("The reference inventory we use to store which items enemies should get")]
		private Inventory enemyInventory;

		// Token: 0x04002808 RID: 10248
		[SerializeField]
		[Tooltip("The number of waves before you transition to the next stage (e.g., \"every Nth wave\").")]
		private int stageTransitionPeriod;

		// Token: 0x04002809 RID: 10249
		[Tooltip("Spawn card for the stage transition portal")]
		[SerializeField]
		private InteractableSpawnCard stageTransitionPortalCard;

		// Token: 0x0400280A RID: 10250
		[Tooltip("Maximum spawn distance for the stage transition portal.")]
		[SerializeField]
		private float stageTransitionPortalMaxDistance;

		// Token: 0x0400280B RID: 10251
		[SerializeField]
		[Tooltip("The chat message that's broadcasted when spawning the stage transition portal.")]
		private string stageTransitionChatToken;

		// Token: 0x0400280C RID: 10252
		[SerializeField]
		[Tooltip("The prefab with the FogDamageController attached")]
		private GameObject fogDamagePrefab;

		// Token: 0x0400280D RID: 10253
		[SerializeField]
		[Tooltip("The maximum distance to spawn players from the safe ward")]
		private float spawnMaxRadius;

		// Token: 0x0400280E RID: 10254
		[SerializeField]
		[Tooltip("Spawn card for the safe ward that is spawned at the beginning of the run")]
		private InteractableSpawnCard initialSafeWardCard;

		// Token: 0x0400280F RID: 10255
		[Tooltip("Spawn card for the safe wards (after the first one)")]
		[SerializeField]
		private InteractableSpawnCard safeWardCard;

		// Token: 0x04002810 RID: 10256
		[SerializeField]
		[Tooltip("The effect to spawn when a player is revived at the end of a wave")]
		private GameObject playerRespawnEffectPrefab;

		// Token: 0x04002811 RID: 10257
		[SerializeField]
		[Tooltip("The number of credits the SceneDirector uses to spawn interactables")]
		private int interactableCredits;

		// Token: 0x04002812 RID: 10258
		[SerializeField]
		[Tooltip("Remove all items with these tags from the item pools")]
		private ItemTag[] blacklistedTags;

		// Token: 0x04002813 RID: 10259
		[Tooltip("Remove these items from the pool")]
		[SerializeField]
		private ItemDef[] blacklistedItems;

		// Token: 0x04002814 RID: 10260
		[SyncVar]
		private int _waveIndex;

		// Token: 0x04002815 RID: 10261
		[SyncVar]
		private NetworkInstanceId waveInstanceId;

		// Token: 0x04002816 RID: 10262
		private InfiniteTowerWaveController _waveController;

		// Token: 0x04002817 RID: 10263
		private Xoroshiro128Plus waveRng;

		// Token: 0x04002818 RID: 10264
		private Xoroshiro128Plus enemyItemRng;

		// Token: 0x04002819 RID: 10265
		private Xoroshiro128Plus safeWardRng;

		// Token: 0x0400281A RID: 10266
		private int enemyItemPatternIndex;

		// Token: 0x0400281B RID: 10267
		private InfiniteTowerSafeWardController safeWardController;

		// Token: 0x0400281C RID: 10268
		private FogDamageController fogDamageController;

		// Token: 0x020006EB RID: 1771
		[Serializable]
		public struct EnemyItemEntry
		{
			// Token: 0x0400281D RID: 10269
			public PickupDropTable dropTable;

			// Token: 0x0400281E RID: 10270
			public int stacks;
		}
	}
}
