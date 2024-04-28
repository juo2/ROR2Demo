using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using HG;
using RoR2.CharacterAI;
using RoR2.ConVar;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008A6 RID: 2214
	public class Stage : NetworkBehaviour
	{
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x000D0454 File Offset: 0x000CE654
		// (set) Token: 0x06003109 RID: 12553 RVA: 0x000D045B File Offset: 0x000CE65B
		public static Stage instance { get; private set; }

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x000D0463 File Offset: 0x000CE663
		// (set) Token: 0x0600310B RID: 12555 RVA: 0x000D0475 File Offset: 0x000CE675
		public Run.FixedTimeStamp entryTime
		{
			get
			{
				return Run.FixedTimeStamp.zero + this._entryTime;
			}
			private set
			{
				this.Network_entryTime = value - Run.FixedTimeStamp.zero;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x000D0488 File Offset: 0x000CE688
		// (set) Token: 0x0600310D RID: 12557 RVA: 0x000D0490 File Offset: 0x000CE690
		public float entryStopwatchValue
		{
			get
			{
				return this._entryStopwatchValue;
			}
			private set
			{
				this.Network_entryStopwatchValue = value;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600310E RID: 12558 RVA: 0x000D0499 File Offset: 0x000CE699
		// (set) Token: 0x0600310F RID: 12559 RVA: 0x000D04A1 File Offset: 0x000CE6A1
		public float entryDifficultyCoefficient
		{
			get
			{
				return this._entryDifficultyCoefficient;
			}
			private set
			{
				this.Network_entryDifficultyCoefficient = value;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06003110 RID: 12560 RVA: 0x000D04AA File Offset: 0x000CE6AA
		// (set) Token: 0x06003111 RID: 12561 RVA: 0x000D04B2 File Offset: 0x000CE6B2
		public BodyIndex singleMonsterTypeBodyIndex
		{
			get
			{
				return (BodyIndex)this._singleMonsterTypeBodyIndex;
			}
			set
			{
				this.Network_singleMonsterTypeBodyIndex = (int)value;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x000D04BB File Offset: 0x000CE6BB
		// (set) Token: 0x06003113 RID: 12563 RVA: 0x000D04C3 File Offset: 0x000CE6C3
		public SceneDef sceneDef { get; private set; }

		// Token: 0x06003114 RID: 12564 RVA: 0x000D04CC File Offset: 0x000CE6CC
		private void Start()
		{
			this.sceneDef = SceneCatalog.GetSceneDefForCurrentScene();
			if (NetworkServer.active)
			{
				this.entryTime = Run.FixedTimeStamp.now;
				this.entryStopwatchValue = Run.instance.GetRunStopwatch();
				this.entryDifficultyCoefficient = Run.instance.difficultyCoefficient;
				this.RespawnAllNPCs();
				this.BeginServer();
			}
			if (NetworkClient.active)
			{
				this.RespawnLocalPlayers();
			}
			Action<Stage> action = Stage.onStageStartGlobal;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000D0540 File Offset: 0x000CE740
		private void RespawnAllNPCs()
		{
			if (this.sceneDef.suppressNpcEntry)
			{
				return;
			}
			ReadOnlyCollection<CharacterMaster> readOnlyInstancesList = CharacterMaster.readOnlyInstancesList;
			Transform playerSpawnTransform = this.GetPlayerSpawnTransform();
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				CharacterMaster characterMaster = readOnlyInstancesList[i];
				if (characterMaster && !characterMaster.GetComponent<PlayerCharacterMasterController>() && !characterMaster.GetBodyObject() && Util.IsDontDestroyOnLoad(characterMaster.gameObject))
				{
					Vector3 vector = Vector3.zero;
					Quaternion rotation = Quaternion.identity;
					if (playerSpawnTransform)
					{
						vector = playerSpawnTransform.position;
						rotation = playerSpawnTransform.rotation;
						BaseAI component = readOnlyInstancesList[i].GetComponent<BaseAI>();
						CharacterBody component2 = readOnlyInstancesList[i].bodyPrefab.GetComponent<CharacterBody>();
						if (component && component2)
						{
							NodeGraph desiredSpawnNodeGraph = component.GetDesiredSpawnNodeGraph();
							if (desiredSpawnNodeGraph)
							{
								List<NodeGraph.NodeIndex> list = CollectionPool<NodeGraph.NodeIndex, List<NodeGraph.NodeIndex>>.RentCollection();
								desiredSpawnNodeGraph.FindNodesInRange(vector, 10f, 100f, (HullMask)(1 << (int)component2.hullClassification), list);
								if ((float)list.Count > 0f)
								{
									desiredSpawnNodeGraph.GetNodePosition(list[UnityEngine.Random.Range(0, list.Count)], out vector);
								}
								list = CollectionPool<NodeGraph.NodeIndex, List<NodeGraph.NodeIndex>>.ReturnCollection(list);
							}
						}
					}
					readOnlyInstancesList[i].Respawn(vector, rotation);
				}
			}
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000D06A0 File Offset: 0x000CE8A0
		[Client]
		public void RespawnLocalPlayers()
		{
			if (!NetworkClient.active)
			{
				Debug.LogWarning("[Client] function 'System.Void RoR2.Stage::RespawnLocalPlayers()' called on server");
				return;
			}
			if (this.sceneDef.suppressPlayerEntry)
			{
				return;
			}
			ReadOnlyCollection<NetworkUser> readOnlyInstancesList = NetworkUser.readOnlyInstancesList;
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				NetworkUser networkUser = readOnlyInstancesList[i];
				CharacterMaster characterMaster = null;
				if (networkUser.isLocalPlayer && networkUser.masterObject)
				{
					characterMaster = networkUser.masterObject.GetComponent<CharacterMaster>();
				}
				if (characterMaster)
				{
					characterMaster.CallCmdRespawn("");
				}
			}
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000D0725 File Offset: 0x000CE925
		private void OnEnable()
		{
			Stage.instance = SingletonHelper.Assign<Stage>(Stage.instance, this);
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000D0737 File Offset: 0x000CE937
		private void OnDisable()
		{
			Stage.instance = SingletonHelper.Unassign<Stage>(Stage.instance, this);
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000D074C File Offset: 0x000CE94C
		[Server]
		public Transform GetPlayerSpawnTransform()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'UnityEngine.Transform RoR2.Stage::GetPlayerSpawnTransform()' called on client");
				return null;
			}
			SpawnPoint spawnPoint = SpawnPoint.ConsumeSpawnPoint();
			if (spawnPoint)
			{
				return spawnPoint.transform;
			}
			return null;
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000D0790 File Offset: 0x000CE990
		[Server]
		public void RespawnCharacter(CharacterMaster characterMaster)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Stage::RespawnCharacter(RoR2.CharacterMaster)' called on client");
				return;
			}
			if (!characterMaster)
			{
				return;
			}
			Transform playerSpawnTransform = this.GetPlayerSpawnTransform();
			Vector3 vector = Vector3.zero;
			Quaternion quaternion = Quaternion.identity;
			if (playerSpawnTransform)
			{
				vector = playerSpawnTransform.position;
				quaternion = playerSpawnTransform.rotation;
			}
			characterMaster.Respawn(vector, quaternion);
			if (characterMaster.GetComponent<PlayerCharacterMasterController>())
			{
				this.spawnedAnyPlayer = true;
			}
			if (this.usePod)
			{
				Run.instance.HandlePlayerFirstEntryAnimation(characterMaster.GetBody(), vector, quaternion);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x000D081B File Offset: 0x000CEA1B
		// (set) Token: 0x0600311C RID: 12572 RVA: 0x000D082D File Offset: 0x000CEA2D
		public Run.FixedTimeStamp stageAdvanceTime
		{
			get
			{
				return Run.FixedTimeStamp.zero + this._stageAdvanceTime;
			}
			private set
			{
				this.Network_stageAdvanceTime = value - Run.FixedTimeStamp.zero;
			}
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000D0840 File Offset: 0x000CEA40
		[Server]
		public void BeginAdvanceStage(SceneDef destinationStage)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Stage::BeginAdvanceStage(RoR2.SceneDef)' called on client");
				return;
			}
			this.stageAdvanceTime = Run.FixedTimeStamp.now + 0.75f;
			this.nextStage = destinationStage;
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600311E RID: 12574 RVA: 0x000D0873 File Offset: 0x000CEA73
		// (set) Token: 0x0600311F RID: 12575 RVA: 0x000D087B File Offset: 0x000CEA7B
		public bool completed { get; private set; }

		// Token: 0x06003120 RID: 12576 RVA: 0x000D0884 File Offset: 0x000CEA84
		[Server]
		private void BeginServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Stage::BeginServer()' called on client");
				return;
			}
			Action<Stage> action = Stage.onServerStageBegin;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000D08AB File Offset: 0x000CEAAB
		[Server]
		public void CompleteServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Stage::CompleteServer()' called on client");
				return;
			}
			if (this.completed)
			{
				return;
			}
			this.completed = true;
			Action<Stage> action = Stage.onServerStageComplete;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x06003122 RID: 12578 RVA: 0x000D08E4 File Offset: 0x000CEAE4
		// (remove) Token: 0x06003123 RID: 12579 RVA: 0x000D0918 File Offset: 0x000CEB18
		public static event Action<Stage> onServerStageBegin;

		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x06003124 RID: 12580 RVA: 0x000D094C File Offset: 0x000CEB4C
		// (remove) Token: 0x06003125 RID: 12581 RVA: 0x000D0980 File Offset: 0x000CEB80
		public static event Action<Stage> onServerStageComplete;

		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x06003126 RID: 12582 RVA: 0x000D09B4 File Offset: 0x000CEBB4
		// (remove) Token: 0x06003127 RID: 12583 RVA: 0x000D09E8 File Offset: 0x000CEBE8
		public static event Action<Stage> onStageStartGlobal;

		// Token: 0x06003128 RID: 12584 RVA: 0x000D0A1C File Offset: 0x000CEC1C
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				if (this.nextStage && this.stageAdvanceTime.hasPassed)
				{
					SceneDef nextScene = this.nextStage;
					this.nextStage = null;
					Run.instance.AdvanceStage(nextScene);
				}
				if (this.spawnedAnyPlayer && this.stageAdvanceTime.isInfinity && !Run.instance.isGameOverServer)
				{
					ReadOnlyCollection<PlayerCharacterMasterController> instances = PlayerCharacterMasterController.instances;
					bool flag = false;
					for (int i = 0; i < instances.Count; i++)
					{
						PlayerCharacterMasterController playerCharacterMasterController = instances[i];
						if (playerCharacterMasterController.isConnected && playerCharacterMasterController.preventGameOver)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						Run.instance.BeginGameOver(RoR2Content.GameEndings.StandardLoss);
					}
				}
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06003129 RID: 12585 RVA: 0x000D0ADC File Offset: 0x000CECDC
		// (set) Token: 0x0600312A RID: 12586 RVA: 0x000D0AE4 File Offset: 0x000CECE4
		public bool scavPackDroppedServer { get; set; }

		// Token: 0x0600312D RID: 12589 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x000D0B5C File Offset: 0x000CED5C
		// (set) Token: 0x0600312F RID: 12591 RVA: 0x000D0B6F File Offset: 0x000CED6F
		public float Network_entryTime
		{
			get
			{
				return this._entryTime;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this._entryTime, 1U);
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06003130 RID: 12592 RVA: 0x000D0B84 File Offset: 0x000CED84
		// (set) Token: 0x06003131 RID: 12593 RVA: 0x000D0B97 File Offset: 0x000CED97
		public float Network_entryStopwatchValue
		{
			get
			{
				return this._entryStopwatchValue;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this._entryStopwatchValue, 2U);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06003132 RID: 12594 RVA: 0x000D0BAC File Offset: 0x000CEDAC
		// (set) Token: 0x06003133 RID: 12595 RVA: 0x000D0BBF File Offset: 0x000CEDBF
		public float Network_entryDifficultyCoefficient
		{
			get
			{
				return this._entryDifficultyCoefficient;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this._entryDifficultyCoefficient, 4U);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06003134 RID: 12596 RVA: 0x000D0BD4 File Offset: 0x000CEDD4
		// (set) Token: 0x06003135 RID: 12597 RVA: 0x000D0BE7 File Offset: 0x000CEDE7
		public int Network_singleMonsterTypeBodyIndex
		{
			get
			{
				return this._singleMonsterTypeBodyIndex;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this._singleMonsterTypeBodyIndex, 8U);
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06003136 RID: 12598 RVA: 0x000D0BFC File Offset: 0x000CEDFC
		// (set) Token: 0x06003137 RID: 12599 RVA: 0x000D0C0F File Offset: 0x000CEE0F
		public float Network_stageAdvanceTime
		{
			get
			{
				return this._stageAdvanceTime;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this._stageAdvanceTime, 16U);
			}
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000D0C24 File Offset: 0x000CEE24
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this._entryTime);
				writer.Write(this._entryStopwatchValue);
				writer.Write(this._entryDifficultyCoefficient);
				writer.WritePackedUInt32((uint)this._singleMonsterTypeBodyIndex);
				writer.Write(this._stageAdvanceTime);
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
				writer.Write(this._entryTime);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this._entryStopwatchValue);
			}
			if ((base.syncVarDirtyBits & 4U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this._entryDifficultyCoefficient);
			}
			if ((base.syncVarDirtyBits & 8U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32((uint)this._singleMonsterTypeBodyIndex);
			}
			if ((base.syncVarDirtyBits & 16U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this._stageAdvanceTime);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000D0D8C File Offset: 0x000CEF8C
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this._entryTime = reader.ReadSingle();
				this._entryStopwatchValue = reader.ReadSingle();
				this._entryDifficultyCoefficient = reader.ReadSingle();
				this._singleMonsterTypeBodyIndex = (int)reader.ReadPackedUInt32();
				this._stageAdvanceTime = reader.ReadSingle();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this._entryTime = reader.ReadSingle();
			}
			if ((num & 2) != 0)
			{
				this._entryStopwatchValue = reader.ReadSingle();
			}
			if ((num & 4) != 0)
			{
				this._entryDifficultyCoefficient = reader.ReadSingle();
			}
			if ((num & 8) != 0)
			{
				this._singleMonsterTypeBodyIndex = (int)reader.ReadPackedUInt32();
			}
			if ((num & 16) != 0)
			{
				this._stageAdvanceTime = reader.ReadSingle();
			}
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040032A8 RID: 12968
		[SyncVar]
		private float _entryTime;

		// Token: 0x040032A9 RID: 12969
		[SyncVar]
		private float _entryStopwatchValue;

		// Token: 0x040032AA RID: 12970
		[SyncVar]
		private float _entryDifficultyCoefficient;

		// Token: 0x040032AB RID: 12971
		[SyncVar]
		private int _singleMonsterTypeBodyIndex = -1;

		// Token: 0x040032AD RID: 12973
		private bool spawnedAnyPlayer;

		// Token: 0x040032AE RID: 12974
		[NonSerialized]
		public bool usePod = Run.instance && Run.instance.spawnWithPod && Stage.stage1PodConVar.value;

		// Token: 0x040032AF RID: 12975
		private static BoolConVar stage1PodConVar = new BoolConVar("stage1_pod", ConVarFlags.Cheat, "1", "Whether or not to use the pod when spawning on the first stage.");

		// Token: 0x040032B0 RID: 12976
		[SyncVar]
		private float _stageAdvanceTime = float.PositiveInfinity;

		// Token: 0x040032B1 RID: 12977
		public const float stageAdvanceTransitionDuration = 0.5f;

		// Token: 0x040032B2 RID: 12978
		public const float stageAdvanceTransitionDelay = 0.75f;

		// Token: 0x040032B3 RID: 12979
		private SceneDef nextStage;
	}
}
