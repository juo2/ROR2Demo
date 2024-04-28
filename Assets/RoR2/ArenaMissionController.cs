using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using EntityStates;
using EntityStates.Missions.Arena.NullWard;
using RoR2.CharacterAI;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020005D0 RID: 1488
	[RequireComponent(typeof(Inventory))]
	[RequireComponent(typeof(EntityStateMachine))]
	public class ArenaMissionController : NetworkBehaviour
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x000737AC File Offset: 0x000719AC
		// (set) Token: 0x06001AEA RID: 6890 RVA: 0x000737B4 File Offset: 0x000719B4
		public int currentRound { get; private set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x000737BD File Offset: 0x000719BD
		// (set) Token: 0x06001AEC RID: 6892 RVA: 0x000737C5 File Offset: 0x000719C5
		public int clearedRounds
		{
			get
			{
				return this._clearedRounds;
			}
			private set
			{
				this.Network_clearedRounds = value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x000737CE File Offset: 0x000719CE
		// (set) Token: 0x06001AEE RID: 6894 RVA: 0x000737D5 File Offset: 0x000719D5
		public static ArenaMissionController instance { get; private set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x000737DD File Offset: 0x000719DD
		private float creditsThisRound
		{
			get
			{
				return (this.baseMonsterCredit + this.creditMultiplierPerRound * (float)(this.currentRound - 1)) * this.cachedDifficultyCoefficient;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x000737FD File Offset: 0x000719FD
		// (set) Token: 0x06001AF1 RID: 6897 RVA: 0x00073805 File Offset: 0x00071A05
		public Inventory inventory { get; private set; }

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06001AF2 RID: 6898 RVA: 0x00073810 File Offset: 0x00071A10
		// (remove) Token: 0x06001AF3 RID: 6899 RVA: 0x00073844 File Offset: 0x00071A44
		public static event Action onBeatArena;

		// Token: 0x06001AF4 RID: 6900 RVA: 0x00073877 File Offset: 0x00071A77
		private void Awake()
		{
			this.mainStateMachine = EntityStateMachine.FindByCustomName(base.gameObject, "Main");
			this.inventory = base.GetComponent<Inventory>();
			this.syncActiveMonsterBodies.InitializeBehaviour(this, ArenaMissionController.kListsyncActiveMonsterBodies);
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x000738AC File Offset: 0x00071AAC
		private void OnEnable()
		{
			ArenaMissionController.instance = SingletonHelper.Assign<ArenaMissionController>(ArenaMissionController.instance, this);
			Action action = ArenaMissionController.onInstanceChangedGlobal;
			if (action != null)
			{
				action();
			}
			SceneDirector.onPreGeneratePlayerSpawnPointsServer += this.OnPreGeneratePlayerSpawnPointsServer;
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x000738DF File Offset: 0x00071ADF
		private void OnDisable()
		{
			SceneDirector.onPreGeneratePlayerSpawnPointsServer -= this.OnPreGeneratePlayerSpawnPointsServer;
			ArenaMissionController.instance = SingletonHelper.Unassign<ArenaMissionController>(ArenaMissionController.instance, this);
			Action action = ArenaMissionController.onInstanceChangedGlobal;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00073911 File Offset: 0x00071B11
		private void OnPreGeneratePlayerSpawnPointsServer(SceneDirector sceneDirector, ref Action generationMethod)
		{
			generationMethod = new Action(this.GeneratePlayerSpawnPointsServer);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00073924 File Offset: 0x00071B24
		private void GeneratePlayerSpawnPointsServer()
		{
			if (this.nullWards.Length == 0)
			{
				return;
			}
			Vector3 position = this.nullWards[0].transform.position;
			NodeGraph groundNodes = SceneInfo.instance.groundNodes;
			NodeGraphSpider nodeGraphSpider = new NodeGraphSpider(SceneInfo.instance.groundNodes, HullMask.Human);
			nodeGraphSpider.AddNodeForNextStep(groundNodes.FindClosestNode(position, HullClassification.Human, float.PositiveInfinity));
			for (int i = 0; i < 4; i++)
			{
				nodeGraphSpider.PerformStep();
				if (nodeGraphSpider.collectedSteps.Count > 16)
				{
					break;
				}
			}
			for (int j = 0; j < nodeGraphSpider.collectedSteps.Count; j++)
			{
				NodeGraphSpider.StepInfo stepInfo = nodeGraphSpider.collectedSteps[j];
				SpawnPoint.AddSpawnPoint(groundNodes, stepInfo.node, this.rng);
			}
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x000739DC File Offset: 0x00071BDC
		[Server]
		public override void OnStartServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ArenaMissionController::OnStartServer()' called on client");
				return;
			}
			base.OnStartServer();
			this.fogDamageInstance = UnityEngine.Object.Instantiate<GameObject>(this.fogDamagePrefab);
			FogDamageController component = this.fogDamageInstance.GetComponent<FogDamageController>();
			foreach (GameObject gameObject in this.nullWards)
			{
				component.AddSafeZone(gameObject.GetComponent<SphereZone>());
			}
			NetworkServer.Spawn(this.fogDamageInstance);
			this.cachedDifficultyCoefficient = Run.instance.difficultyCoefficient;
			this.rng = new Xoroshiro128Plus((ulong)Run.instance.stageRng.nextUint);
			this.InitCombatDirectors();
			Util.ShuffleArray<GameObject>(this.nullWards, this.rng);
			this.ReadyNextNullWard();
			this.availableMonsterCards = Util.CreateReasonableDirectorCardSpawnList(this.baseMonsterCredit * this.cachedDifficultyCoefficient, this.maximumNumberToSpawnBeforeSkipping, this.minimumNumberToSpawnPerMonsterType);
			if (this.availableMonsterCards.Count == 0)
			{
				Debug.Log("No reasonable monsters could be found.");
				return;
			}
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00073AD8 File Offset: 0x00071CD8
		[Server]
		private void ReadyNextNullWard()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ArenaMissionController::ReadyNextNullWard()' called on client");
				return;
			}
			if (this.currentRound > this.nullWards.Length)
			{
				Debug.LogError("Out of null wards! Aborting.");
				return;
			}
			EntityStateMachine component = this.nullWards[this.currentRound].GetComponent<EntityStateMachine>();
			component.initialStateType = new SerializableEntityStateType(typeof(WardOnAndReady));
			component.SetNextState(new WardOnAndReady());
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x00073B48 File Offset: 0x00071D48
		[Server]
		private void InitCombatDirectors()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ArenaMissionController::InitCombatDirectors()' called on client");
				return;
			}
			for (int i = 0; i < this.combatDirectors.Length; i++)
			{
				CombatDirector combatDirector = this.combatDirectors[i];
				combatDirector.maximumNumberToSpawnBeforeSkipping = this.maximumNumberToSpawnBeforeSkipping;
				combatDirector.onSpawnedServer.AddListener(new UnityAction<GameObject>(this.ModifySpawnedMasters));
				combatDirector.spawnDistanceMultiplier = this.spawnDistanceMultiplier;
				combatDirector.eliteBias = this.eliteBias;
			}
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x00073BC0 File Offset: 0x00071DC0
		[Server]
		public void BeginRound()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ArenaMissionController::BeginRound()' called on client");
				return;
			}
			int currentRound = this.currentRound;
			this.currentRound = currentRound + 1;
			switch (this.currentRound)
			{
			case 1:
				this.AddMonsterType();
				break;
			case 2:
				this.AddItemStack();
				break;
			case 3:
				this.AddMonsterType();
				break;
			case 4:
				this.AddItemStack();
				break;
			case 5:
				this.AddMonsterType();
				break;
			case 6:
				this.AddItemStack();
				break;
			case 7:
				this.AddMonsterType();
				break;
			case 8:
				this.AddItemStack();
				break;
			case 9:
				this.AddItemStack();
				break;
			}
			int count = this.activeMonsterCards.Count;
			for (int i = 0; i < count; i++)
			{
				DirectorCard directorCard = this.activeMonsterCards[i];
				float num = this.creditsThisRound / (float)count;
				float creditMultiplier = this.creditMultiplierPerRound * (float)this.currentRound / (float)count;
				if (i > this.combatDirectors.Length)
				{
					Debug.LogError("Trying to activate more combat directors than available. Aborting.");
					return;
				}
				CombatDirector combatDirector = this.combatDirectors[i];
				combatDirector.monsterCredit += num;
				combatDirector.creditMultiplier = creditMultiplier;
				combatDirector.currentSpawnTarget = this.monsterSpawnPosition;
				combatDirector.OverrideCurrentMonsterCard(directorCard);
				combatDirector.monsterSpawnTimer = 0f;
				combatDirector.enabled = true;
				Debug.LogFormat("Enabling director {0} with {1} credits to spawn {2}", new object[]
				{
					i,
					num,
					directorCard.spawnCard.name
				});
			}
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x00073D44 File Offset: 0x00071F44
		[Server]
		public void ModifySpawnedMasters(GameObject targetGameObject)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ArenaMissionController::ModifySpawnedMasters(UnityEngine.GameObject)' called on client");
				return;
			}
			ArenaMissionController.<>c__DisplayClass57_0 CS$<>8__locals1 = new ArenaMissionController.<>c__DisplayClass57_0();
			CharacterMaster component = targetGameObject.GetComponent<CharacterMaster>();
			CS$<>8__locals1.ai = component.GetComponent<BaseAI>();
			if (CS$<>8__locals1.ai)
			{
				CS$<>8__locals1.ai.onBodyDiscovered += CS$<>8__locals1.<ModifySpawnedMasters>g__OnBodyDiscovered|0;
			}
			CharacterBody body = component.GetBody();
			if (body)
			{
				foreach (EntityStateMachine entityStateMachine in body.GetComponents<EntityStateMachine>())
				{
					entityStateMachine.initialStateType = entityStateMachine.mainStateType;
				}
			}
			component.inventory.AddItemsFrom(this.inventory);
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x00073DEC File Offset: 0x00071FEC
		[Server]
		public void EndRound()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ArenaMissionController::EndRound()' called on client");
				return;
			}
			int i = this.clearedRounds;
			this.clearedRounds = i + 1;
			if (this.currentRound < this.totalRoundsMax)
			{
				this.ReadyNextNullWard();
			}
			else
			{
				if (this.fogDamageInstance)
				{
					UnityEngine.Object.Destroy(this.fogDamageInstance);
					this.fogDamageInstance = null;
				}
				Action action = ArenaMissionController.onBeatArena;
				if (action != null)
				{
					action();
				}
				this.mainStateMachine.SetNextState(new ArenaMissionController.MissionCompleted());
				Chat.SendBroadcastChat(new Chat.SimpleChatMessage
				{
					baseToken = "ARENA_END"
				});
				PortalSpawner[] array = this.completionPortalSpawners;
				for (i = 0; i < array.Length; i++)
				{
					array[i].AttemptSpawnPortalServer();
				}
			}
			for (int j = 0; j < this.combatDirectors.Length; j++)
			{
				CombatDirector combatDirector = this.combatDirectors[j];
				combatDirector.enabled = false;
				combatDirector.monsterCredit = 0f;
			}
			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(TeamIndex.Monster);
			for (int k = teamMembers.Count - 1; k >= 0; k--)
			{
				teamMembers[k].body.healthComponent.Suicide(base.gameObject, base.gameObject, DamageType.VoidDeath);
			}
			int participatingPlayerCount = Run.instance.participatingPlayerCount;
			if (participatingPlayerCount != 0 && this.rewardSpawnPosition)
			{
				PickupIndex[] array2 = Array.Empty<PickupIndex>();
				int num = this.currentRound - 1;
				if (num < this.playerRewardOrder.Length)
				{
					PickupDropTable pickupDropTable = this.playerRewardOrder[num];
					array2 = ((pickupDropTable != null) ? pickupDropTable.GenerateUniqueDrops(this.numRewardOptions, this.rng) : null);
				}
				if (array2.Length != 0)
				{
					ItemTier itemTier = PickupCatalog.GetPickupDef(array2[0]).itemTier;
					int num2 = participatingPlayerCount;
					float angle = 360f / (float)num2;
					Vector3 vector = Quaternion.AngleAxis((float)UnityEngine.Random.Range(0, 360), Vector3.up) * (Vector3.up * 40f + Vector3.forward * 5f);
					Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
					int l = 0;
					while (l < num2)
					{
						PickupDropletController.CreatePickupDroplet(new GenericPickupController.CreatePickupInfo
						{
							pickerOptions = PickupPickerController.GenerateOptionsFromArray(array2),
							prefabOverride = this.pickupPrefab,
							position = this.rewardSpawnPosition.transform.position,
							rotation = Quaternion.identity,
							pickupIndex = PickupCatalog.FindPickupIndex(itemTier)
						}, this.rewardSpawnPosition.transform.position, vector);
						l++;
						vector = rotation * vector;
					}
				}
			}
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x00074080 File Offset: 0x00072280
		[Server]
		private void AddMonsterType()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ArenaMissionController::AddMonsterType()' called on client");
				return;
			}
			if (this.availableMonsterCards.Count == 0)
			{
				Debug.Log("Out of monster types! Aborting.");
				return;
			}
			int num = this.availableMonsterCards.EvaluateToChoiceIndex(this.rng.nextNormalizedFloat);
			DirectorCard value = this.availableMonsterCards.choices[num].value;
			this.activeMonsterCards.Add(value);
			SyncList<int> syncList = this.syncActiveMonsterBodies;
			CharacterMaster component = value.spawnCard.prefab.GetComponent<CharacterMaster>();
			int? num2;
			if (component == null)
			{
				num2 = null;
			}
			else
			{
				CharacterBody component2 = component.bodyPrefab.GetComponent<CharacterBody>();
				num2 = ((component2 != null) ? new BodyIndex?(component2.bodyIndex) : null);
			}
			syncList.Add(num2 ?? -1);
			this.availableMonsterCards.RemoveChoice(num);
			CharacterBody component3 = value.spawnCard.prefab.GetComponent<CharacterMaster>().bodyPrefab.GetComponent<CharacterBody>();
			Chat.SendBroadcastChat(new Chat.SubjectFormatChatMessage
			{
				baseToken = "ARENA_ADD_MONSTER",
				paramTokens = new string[]
				{
					component3.baseNameToken
				}
			});
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x000741B0 File Offset: 0x000723B0
		[Server]
		private void AddItemStack()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ArenaMissionController::AddItemStack()' called on client");
				return;
			}
			PickupIndex pickupIndex = PickupIndex.none;
			if (this.nextItemStackIndex < this.monsterItemStackOrder.Length)
			{
				PickupDropTable dropTable = this.monsterItemStackOrder[this.nextItemStackIndex].dropTable;
				if (dropTable)
				{
					pickupIndex = dropTable.GenerateDrop(this.rng);
				}
			}
			if (pickupIndex != PickupIndex.none)
			{
				PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
				ItemIndex itemIndex = pickupDef.itemIndex;
				this.inventory.GiveItem(itemIndex, this.monsterItemStackOrder[this.nextItemStackIndex].stacks);
				Chat.SendBroadcastChat(new Chat.PlayerPickupChatMessage
				{
					baseToken = "ARENA_ADD_ITEM",
					pickupToken = pickupDef.nameToken,
					pickupColor = pickupDef.baseColor
				});
			}
			this.nextItemStackIndex++;
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06001B01 RID: 6913 RVA: 0x00074294 File Offset: 0x00072494
		// (remove) Token: 0x06001B02 RID: 6914 RVA: 0x000742C8 File Offset: 0x000724C8
		public static event Action onInstanceChangedGlobal;

		// Token: 0x06001B04 RID: 6916 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x0007431C File Offset: 0x0007251C
		// (set) Token: 0x06001B06 RID: 6918 RVA: 0x0007432F File Offset: 0x0007252F
		public int Network_clearedRounds
		{
			get
			{
				return this._clearedRounds;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this._clearedRounds, 2U);
			}
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00074343 File Offset: 0x00072543
		protected static void InvokeSyncListsyncActiveMonsterBodies(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("SyncList syncActiveMonsterBodies called on server.");
				return;
			}
			((ArenaMissionController)obj).syncActiveMonsterBodies.HandleMsg(reader);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0007436C File Offset: 0x0007256C
		static ArenaMissionController()
		{
			NetworkBehaviour.RegisterSyncListDelegate(typeof(ArenaMissionController), ArenaMissionController.kListsyncActiveMonsterBodies, new NetworkBehaviour.CmdDelegate(ArenaMissionController.InvokeSyncListsyncActiveMonsterBodies));
			NetworkCRC.RegisterBehaviour("ArenaMissionController", 0);
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x000743A8 File Offset: 0x000725A8
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				SyncListInt.WriteInstance(writer, this.syncActiveMonsterBodies);
				writer.WritePackedUInt32((uint)this._clearedRounds);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				SyncListInt.WriteInstance(writer, this.syncActiveMonsterBodies);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32((uint)this._clearedRounds);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x00074454 File Offset: 0x00072654
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				SyncListInt.ReadReference(reader, this.syncActiveMonsterBodies);
				this._clearedRounds = (int)reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				SyncListInt.ReadReference(reader, this.syncActiveMonsterBodies);
			}
			if ((num & 2) != 0)
			{
				this._clearedRounds = (int)reader.ReadPackedUInt32();
			}
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002115 RID: 8469
		[Header("Behavior Values")]
		public float baseMonsterCredit;

		// Token: 0x04002116 RID: 8470
		public float creditMultiplierPerRound;

		// Token: 0x04002117 RID: 8471
		public int minimumNumberToSpawnPerMonsterType;

		// Token: 0x04002118 RID: 8472
		public int totalRoundsMax;

		// Token: 0x04002119 RID: 8473
		public int maximumNumberToSpawnBeforeSkipping;

		// Token: 0x0400211A RID: 8474
		public float spawnDistanceMultiplier;

		// Token: 0x0400211B RID: 8475
		public float eliteBias;

		// Token: 0x0400211C RID: 8476
		[Header("Cached Components")]
		public GameObject[] nullWards;

		// Token: 0x0400211D RID: 8477
		public GameObject monsterSpawnPosition;

		// Token: 0x0400211E RID: 8478
		public GameObject rewardSpawnPosition;

		// Token: 0x0400211F RID: 8479
		public CombatDirector[] combatDirectors;

		// Token: 0x04002120 RID: 8480
		public GameObject clearedEffect;

		// Token: 0x04002121 RID: 8481
		public GameObject killEffectPrefab;

		// Token: 0x04002122 RID: 8482
		public GameObject fogDamagePrefab;

		// Token: 0x04002123 RID: 8483
		public PortalSpawner[] completionPortalSpawners;

		// Token: 0x04002124 RID: 8484
		public ArenaMissionController.MonsterItemStackData[] monsterItemStackOrder;

		// Token: 0x04002125 RID: 8485
		public PickupDropTable[] playerRewardOrder;

		// Token: 0x04002126 RID: 8486
		[SerializeField]
		private int numRewardOptions;

		// Token: 0x04002127 RID: 8487
		[SerializeField]
		private GameObject pickupPrefab;

		// Token: 0x0400212A RID: 8490
		private EntityStateMachine mainStateMachine;

		// Token: 0x0400212C RID: 8492
		private Xoroshiro128Plus rng;

		// Token: 0x0400212D RID: 8493
		private List<DirectorCard> activeMonsterCards = new List<DirectorCard>();

		// Token: 0x0400212E RID: 8494
		public readonly SyncListInt syncActiveMonsterBodies = new SyncListInt();

		// Token: 0x04002130 RID: 8496
		private WeightedSelection<DirectorCard> availableMonsterCards;

		// Token: 0x04002131 RID: 8497
		private float cachedDifficultyCoefficient;

		// Token: 0x04002132 RID: 8498
		[SyncVar]
		private int _clearedRounds;

		// Token: 0x04002133 RID: 8499
		private int nextItemStackIndex;

		// Token: 0x04002134 RID: 8500
		private GameObject fogDamageInstance;

		// Token: 0x04002136 RID: 8502
		private static int kListsyncActiveMonsterBodies = 1496902198;

		// Token: 0x020005D1 RID: 1489
		public class ArenaMissionBaseState : EntityState
		{
			// Token: 0x170001CB RID: 459
			// (get) Token: 0x06001B0C RID: 6924 RVA: 0x0002BE84 File Offset: 0x0002A084
			protected ArenaMissionController arenaMissionController
			{
				get
				{
					return ArenaMissionController.instance;
				}
			}
		}

		// Token: 0x020005D2 RID: 1490
		public class MissionCompleted : ArenaMissionController.ArenaMissionBaseState
		{
			// Token: 0x06001B0E RID: 6926 RVA: 0x000744BA File Offset: 0x000726BA
			public override void OnEnter()
			{
				base.OnEnter();
				base.arenaMissionController.clearedEffect.SetActive(true);
			}
		}

		// Token: 0x020005D3 RID: 1491
		[Serializable]
		public struct MonsterItemStackData
		{
			// Token: 0x04002137 RID: 8503
			public PickupDropTable dropTable;

			// Token: 0x04002138 RID: 8504
			public int stacks;
		}
	}
}
