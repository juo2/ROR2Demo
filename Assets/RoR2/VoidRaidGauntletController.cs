using System;
using System.Runtime.InteropServices;
using RoR2.Audio;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008EE RID: 2286
	public class VoidRaidGauntletController : NetworkBehaviour
	{
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x000D86F8 File Offset: 0x000D68F8
		// (set) Token: 0x0600335D RID: 13149 RVA: 0x000D86FF File Offset: 0x000D68FF
		public static VoidRaidGauntletController instance { get; private set; }

		// Token: 0x0600335E RID: 13150 RVA: 0x000D8707 File Offset: 0x000D6907
		public void SetCurrentDonutCombatDirectorEnabled(bool isEnabled)
		{
			VoidRaidGauntletController.DonutInfo donutInfo = this.currentDonut;
			if ((donutInfo != null) ? donutInfo.combatDirector : null)
			{
				this.currentDonut.combatDirector.enabled = isEnabled;
			}
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x000D8734 File Offset: 0x000D6934
		public bool TryOpenGauntlet(Vector3 entrancePosition, NetworkInstanceId bossMasterId)
		{
			if (this.gauntletIndex >= this.phaseEncounters.Length)
			{
				return false;
			}
			if (bossMasterId != NetworkInstanceId.Invalid)
			{
				ScriptedCombatEncounter scriptedCombatEncounter = this.phaseEncounters[this.gauntletIndex];
				if (!scriptedCombatEncounter || scriptedCombatEncounter.combatSquad.memberCount != 0 || !scriptedCombatEncounter.combatSquad.HasContainedMember(bossMasterId))
				{
					return false;
				}
			}
			int destinationGauntletIndex = this.gauntletIndex;
			this.previousGauntlet = this.currentGauntlet;
			this.previousDonut = this.currentDonut;
			if (this.previousDonut != null && this.previousDonut.combatDirector)
			{
				this.previousDonut.combatDirector.monsterCredit = 0f;
				this.previousDonut.combatDirector.enabled = false;
			}
			int num = this.gauntletIndex % this.followingDonuts.Length;
			this.currentDonut = this.followingDonuts[num];
			this.currentGauntlet = this.gauntlets[this.gauntletIndex % this.gauntlets.Length];
			this.gauntletIndex++;
			this.CallRpcTryShuffleData(this.rngSeed);
			if (this.currentDonut.root)
			{
				this.currentDonut.root.SetActive(true);
				this.CallRpcActivateDonut(num);
			}
			if (SceneInfo.instance != null)
			{
				VoidRaidGauntletController.GauntletInfo gauntletInfo = this.currentGauntlet;
				if (!string.IsNullOrEmpty((gauntletInfo != null) ? gauntletInfo.gateName : null))
				{
					SceneInfo.instance.SetGateState(this.currentGauntlet.gateName, true);
					VoidRaidGauntletController.GauntletInfo gauntletInfo2 = this.currentGauntlet;
					this.CallRpcActivateGate((gauntletInfo2 != null) ? gauntletInfo2.gateName : null);
				}
			}
			if (this.currentGauntlet.effectRoot && this.gauntletEffectPrefab)
			{
				EffectData effectData = new EffectData
				{
					origin = this.currentGauntlet.effectRoot.position,
					rotation = this.currentGauntlet.effectRoot.rotation
				};
				EffectManager.SpawnEffect(this.gauntletEffectPrefab, effectData, false);
			}
			VoidRaidGauntletController.DonutInfo donutInfo = this.previousDonut;
			if ((donutInfo != null) ? donutInfo.combatDirector : null)
			{
				this.previousDonut.combatDirector.enabled = false;
			}
			Xoroshiro128Plus rng = new Xoroshiro128Plus(this.rngSeed + (ulong)((long)this.gauntletIndex));
			DirectorPlacementRule placementRule = new DirectorPlacementRule
			{
				placementMode = DirectorPlacementRule.PlacementMode.NearestNode,
				position = entrancePosition
			};
			DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(this.gauntletExtranceSpawnCard, placementRule, rng);
			directorSpawnRequest.onSpawnedServer = delegate(SpawnCard.SpawnResult result)
			{
				this.OnEntranceSpawned(result, destinationGauntletIndex);
			};
			DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
			return true;
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x000D89BC File Offset: 0x000D6BBC
		private void OnEntranceSpawned(SpawnCard.SpawnResult result, int destinationGauntletIndex)
		{
			if (result.success)
			{
				this.CallRpcStartActiveSoundLoop();
				this.currentGauntlet.entranceZone = result.spawnedInstance.GetComponentInChildren<MapZone>();
				result.spawnedInstance.GetComponent<VoidRaidGauntletEntranceController>().SetGauntletIndex(destinationGauntletIndex);
			}
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x000D89F3 File Offset: 0x000D6BF3
		private void OnEnable()
		{
			VoidRaidGauntletController.instance = SingletonHelper.Assign<VoidRaidGauntletController>(VoidRaidGauntletController.instance, this);
			SceneDirector.onPreGeneratePlayerSpawnPointsServer += this.OnPreGeneratePlayerSpawnPointsServer;
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x000D8A16 File Offset: 0x000D6C16
		private void OnDisable()
		{
			this.StopActiveSoundLoop();
			SceneDirector.onPreGeneratePlayerSpawnPointsServer -= this.OnPreGeneratePlayerSpawnPointsServer;
			VoidRaidGauntletController.instance = SingletonHelper.Unassign<VoidRaidGauntletController>(VoidRaidGauntletController.instance, this);
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x000D8A3F File Offset: 0x000D6C3F
		private void OnPreGeneratePlayerSpawnPointsServer(SceneDirector sceneDirector, ref Action generationMethod)
		{
			generationMethod = new Action(this.GeneratePlayerSpawnPointsServer);
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x000D8A50 File Offset: 0x000D6C50
		private void GeneratePlayerSpawnPointsServer()
		{
			if (SceneInfo.instance && this.initialDonut != null && this.initialDonut.returnPoint)
			{
				Transform returnPoint = this.initialDonut.returnPoint;
				Vector3 position = returnPoint.position;
				NodeGraph groundNodes = SceneInfo.instance.groundNodes;
				if (!groundNodes)
				{
					Debug.LogError("VoidRaidGauntletController.GeneratePlayerSpawnPointsServer: No ground nodegraph found to place spawn points.", this);
					return;
				}
				NodeGraphSpider nodeGraphSpider = new NodeGraphSpider(groundNodes, HullMask.Human);
				nodeGraphSpider.AddNodeForNextStep(groundNodes.FindClosestNode(position, HullClassification.Human, float.PositiveInfinity));
				for (int i = 0; i < this.initialSpawnSpiderSteps; i++)
				{
					nodeGraphSpider.PerformStep();
					if (nodeGraphSpider.collectedSteps.Count > this.maxInitialSpawnPoints)
					{
						break;
					}
				}
				for (int j = 0; j < nodeGraphSpider.collectedSteps.Count; j++)
				{
					NodeGraphSpider.StepInfo stepInfo = nodeGraphSpider.collectedSteps[j];
					Vector3 position2;
					groundNodes.GetNodePosition(stepInfo.node, out position2);
					Quaternion rotation = returnPoint.rotation;
					SpawnPoint.AddSpawnPoint(position2, rotation);
				}
			}
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x000D8B54 File Offset: 0x000D6D54
		private void Start()
		{
			this.previousDonut = this.initialDonut;
			foreach (VoidRaidGauntletController.DonutInfo donutInfo in this.followingDonuts)
			{
				if ((donutInfo != null) ? donutInfo.root : null)
				{
					donutInfo.root.SetActive(false);
				}
			}
			if (NetworkServer.active)
			{
				this.TryShuffleData(Run.instance.stageRng.nextUlong);
				this.CallRpcTryShuffleData(this.rngSeed);
			}
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x000D8BCD File Offset: 0x000D6DCD
		public override void OnStartServer()
		{
			base.OnStartServer();
			this.CallRpcTryShuffleData(this.rngSeed);
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x000D8BE4 File Offset: 0x000D6DE4
		private void TryShuffleData(ulong seed)
		{
			this.NetworkrngSeed = seed;
			if (!this.hasShuffled)
			{
				Xoroshiro128Plus rng = new Xoroshiro128Plus(seed);
				Debug.Log(string.Format("Shuffling with seed {0}", seed));
				Util.ShuffleArray<VoidRaidGauntletController.DonutInfo>(this.followingDonuts, rng);
				Util.ShuffleArray<VoidRaidGauntletController.GauntletInfo>(this.gauntlets, rng);
				int num = 1;
				while (num < this.phaseEncounters.Length && num - 1 < this.followingDonuts.Length)
				{
					VoidRaidGauntletController.DonutInfo donutInfo = this.followingDonuts[num - 1];
					ScriptedCombatEncounter scriptedCombatEncounter = this.phaseEncounters[num];
					int encounterIndex = num;
					scriptedCombatEncounter.onBeginEncounter += delegate(ScriptedCombatEncounter argEncounter)
					{
						this.OnBeginEncounter(argEncounter, encounterIndex);
					};
					if (scriptedCombatEncounter.spawns.Length != 0)
					{
						scriptedCombatEncounter.spawns[0].explicitSpawnPosition = donutInfo.crabPosition;
					}
					num++;
				}
				int num2 = 0;
				while (num2 < this.gauntlets.Length && num2 < this.followingDonuts.Length)
				{
					VoidRaidGauntletController.GauntletInfo gauntletInfo = this.gauntlets[num2];
					VoidRaidGauntletController.DonutInfo donutInfo2 = this.followingDonuts[num2];
					Debug.Log("Pointing gauntlet " + gauntletInfo.gateName + " to " + donutInfo2.root.name);
					if (gauntletInfo.exitZone && donutInfo2.returnPoint)
					{
						gauntletInfo.exitZone.explicitDestination = donutInfo2.returnPoint;
						gauntletInfo.exitZone.destinationIdealRadius = donutInfo2.returnRadius;
					}
					num2++;
				}
				this.hasShuffled = true;
			}
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x000D8D64 File Offset: 0x000D6F64
		private void OnBeginEncounter(ScriptedCombatEncounter encounter, int encounterIndex)
		{
			while (this.gauntletIndex < encounterIndex)
			{
				this.TryOpenGauntlet(this.currentDonut.crabPosition.position, NetworkInstanceId.Invalid);
			}
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x000D8D90 File Offset: 0x000D6F90
		public void SpawnOutroPortal()
		{
			if (NetworkServer.active && this.currentDonut != null && this.currentDonut.returnPoint)
			{
				Xoroshiro128Plus rng = new Xoroshiro128Plus(this.rngSeed + 1UL);
				DirectorPlacementRule placementRule = new DirectorPlacementRule
				{
					placementMode = DirectorPlacementRule.PlacementMode.Approximate,
					minDistance = this.minOutroPortalDistance,
					maxDistance = this.maxOutroPortalDistance,
					spawnOnTarget = this.currentDonut.returnPoint
				};
				DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(this.outroPortalSpawnCard, placementRule, rng);
				DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
			}
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x000D8E1D File Offset: 0x000D701D
		[ClientRpc]
		private void RpcStartActiveSoundLoop()
		{
			if (this.gauntletActiveLoop)
			{
				this.gauntletActiveLoopPtr = LoopSoundManager.PlaySoundLoopLocal(RoR2Application.instance.gameObject, this.gauntletActiveLoop);
			}
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x000D8E47 File Offset: 0x000D7047
		[ClientRpc]
		private void RpcActivateGate(string gateName)
		{
			if (!string.IsNullOrEmpty(gateName))
			{
				SceneInfo.instance.SetGateState(gateName, true);
			}
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x000D8E60 File Offset: 0x000D7060
		[ClientRpc]
		private void RpcActivateDonut(int donutIndex)
		{
			if (donutIndex < this.followingDonuts.Length)
			{
				VoidRaidGauntletController.DonutInfo donutInfo = this.followingDonuts[donutIndex];
				if (donutInfo != null)
				{
					Debug.Log("Setting " + donutInfo.root.name + " to active");
					donutInfo.root.SetActive(true);
				}
			}
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x000D8EAF File Offset: 0x000D70AF
		[ClientRpc]
		private void RpcTryShuffleData(ulong seed)
		{
			this.TryShuffleData(seed);
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x000D8EB8 File Offset: 0x000D70B8
		private void StopActiveSoundLoop()
		{
			if (this.gauntletActiveLoop)
			{
				LoopSoundManager.StopSoundLoopLocal(this.gauntletActiveLoopPtr);
			}
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x000D8ED2 File Offset: 0x000D70D2
		private void SetSkyboxToGauntlet()
		{
			if (this.donutSkyboxObject)
			{
				this.donutSkyboxObject.SetActive(false);
			}
			if (this.gauntletSkyboxObject)
			{
				this.gauntletSkyboxObject.SetActive(true);
			}
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x000D8F06 File Offset: 0x000D7106
		private void SetSkyboxToDonut()
		{
			if (this.donutSkyboxObject)
			{
				this.donutSkyboxObject.SetActive(true);
			}
			if (this.gauntletSkyboxObject)
			{
				this.gauntletSkyboxObject.SetActive(false);
			}
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x000D8F3A File Offset: 0x000D713A
		public void OnAuthorityPlayerEnter()
		{
			this.SetSkyboxToGauntlet();
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x000D8F42 File Offset: 0x000D7142
		public void OnAuthorityPlayerExit()
		{
			this.SetSkyboxToDonut();
			this.StopActiveSoundLoop();
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x000D8F50 File Offset: 0x000D7150
		public void PointZoneToGauntlet(int destinationGauntletIndex, MapZone zone)
		{
			if (destinationGauntletIndex < this.gauntlets.Length)
			{
				VoidRaidGauntletController.GauntletInfo gauntletInfo = this.gauntlets[destinationGauntletIndex];
				zone.explicitDestination = gauntletInfo.startPoint;
				zone.destinationIdealRadius = gauntletInfo.startRadius;
			}
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06003376 RID: 13174 RVA: 0x000D8FA0 File Offset: 0x000D71A0
		// (set) Token: 0x06003377 RID: 13175 RVA: 0x000D8FB3 File Offset: 0x000D71B3
		public ulong NetworkrngSeed
		{
			get
			{
				return this.rngSeed;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.TryShuffleData(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<ulong>(value, ref this.rngSeed, 1U);
			}
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000D8FF2 File Offset: 0x000D71F2
		protected static void InvokeRpcRpcStartActiveSoundLoop(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcStartActiveSoundLoop called on server.");
				return;
			}
			((VoidRaidGauntletController)obj).RpcStartActiveSoundLoop();
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x000D9015 File Offset: 0x000D7215
		protected static void InvokeRpcRpcActivateGate(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcActivateGate called on server.");
				return;
			}
			((VoidRaidGauntletController)obj).RpcActivateGate(reader.ReadString());
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x000D903E File Offset: 0x000D723E
		protected static void InvokeRpcRpcActivateDonut(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcActivateDonut called on server.");
				return;
			}
			((VoidRaidGauntletController)obj).RpcActivateDonut((int)reader.ReadPackedUInt32());
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x000D9067 File Offset: 0x000D7267
		protected static void InvokeRpcRpcTryShuffleData(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcTryShuffleData called on server.");
				return;
			}
			((VoidRaidGauntletController)obj).RpcTryShuffleData(reader.ReadPackedUInt64());
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x000D9090 File Offset: 0x000D7290
		public void CallRpcStartActiveSoundLoop()
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcStartActiveSoundLoop called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)VoidRaidGauntletController.kRpcRpcStartActiveSoundLoop);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			this.SendRPCInternal(networkWriter, 0, "RpcStartActiveSoundLoop");
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x000D90FC File Offset: 0x000D72FC
		public void CallRpcActivateGate(string gateName)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcActivateGate called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)VoidRaidGauntletController.kRpcRpcActivateGate);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(gateName);
			this.SendRPCInternal(networkWriter, 0, "RpcActivateGate");
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000D9170 File Offset: 0x000D7370
		public void CallRpcActivateDonut(int donutIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcActivateDonut called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)VoidRaidGauntletController.kRpcRpcActivateDonut);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.WritePackedUInt32((uint)donutIndex);
			this.SendRPCInternal(networkWriter, 0, "RpcActivateDonut");
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x000D91E4 File Offset: 0x000D73E4
		public void CallRpcTryShuffleData(ulong seed)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcTryShuffleData called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)VoidRaidGauntletController.kRpcRpcTryShuffleData);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.WritePackedUInt64(seed);
			this.SendRPCInternal(networkWriter, 0, "RpcTryShuffleData");
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x000D9258 File Offset: 0x000D7458
		static VoidRaidGauntletController()
		{
			NetworkBehaviour.RegisterRpcDelegate(typeof(VoidRaidGauntletController), VoidRaidGauntletController.kRpcRpcStartActiveSoundLoop, new NetworkBehaviour.CmdDelegate(VoidRaidGauntletController.InvokeRpcRpcStartActiveSoundLoop));
			VoidRaidGauntletController.kRpcRpcActivateGate = -1148984728;
			NetworkBehaviour.RegisterRpcDelegate(typeof(VoidRaidGauntletController), VoidRaidGauntletController.kRpcRpcActivateGate, new NetworkBehaviour.CmdDelegate(VoidRaidGauntletController.InvokeRpcRpcActivateGate));
			VoidRaidGauntletController.kRpcRpcActivateDonut = -1261146843;
			NetworkBehaviour.RegisterRpcDelegate(typeof(VoidRaidGauntletController), VoidRaidGauntletController.kRpcRpcActivateDonut, new NetworkBehaviour.CmdDelegate(VoidRaidGauntletController.InvokeRpcRpcActivateDonut));
			VoidRaidGauntletController.kRpcRpcTryShuffleData = 20528402;
			NetworkBehaviour.RegisterRpcDelegate(typeof(VoidRaidGauntletController), VoidRaidGauntletController.kRpcRpcTryShuffleData, new NetworkBehaviour.CmdDelegate(VoidRaidGauntletController.InvokeRpcRpcTryShuffleData));
			NetworkCRC.RegisterBehaviour("VoidRaidGauntletController", 0);
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000D931C File Offset: 0x000D751C
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.WritePackedUInt64(this.rngSeed);
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
				writer.WritePackedUInt64(this.rngSeed);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x000D9388 File Offset: 0x000D7588
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.rngSeed = reader.ReadPackedUInt64();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.TryShuffleData(reader.ReadPackedUInt64());
			}
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04003466 RID: 13414
		[SerializeField]
		private VoidRaidGauntletController.DonutInfo initialDonut;

		// Token: 0x04003467 RID: 13415
		[SerializeField]
		private VoidRaidGauntletController.DonutInfo[] followingDonuts;

		// Token: 0x04003468 RID: 13416
		[SerializeField]
		private VoidRaidGauntletController.GauntletInfo[] gauntlets;

		// Token: 0x04003469 RID: 13417
		[SerializeField]
		private BuffDef requiredBuffToKill;

		// Token: 0x0400346A RID: 13418
		[SerializeField]
		private GameObject donutSkyboxObject;

		// Token: 0x0400346B RID: 13419
		[SerializeField]
		private GameObject gauntletSkyboxObject;

		// Token: 0x0400346C RID: 13420
		[SerializeField]
		private GameObject gauntletEffectPrefab;

		// Token: 0x0400346D RID: 13421
		[SerializeField]
		private int initialSpawnSpiderSteps = 4;

		// Token: 0x0400346E RID: 13422
		[SerializeField]
		private int maxInitialSpawnPoints = 16;

		// Token: 0x0400346F RID: 13423
		[SerializeField]
		public InteractableSpawnCard outroPortalSpawnCard;

		// Token: 0x04003470 RID: 13424
		[SerializeField]
		public float minOutroPortalDistance;

		// Token: 0x04003471 RID: 13425
		[SerializeField]
		public float maxOutroPortalDistance;

		// Token: 0x04003472 RID: 13426
		[SerializeField]
		private InteractableSpawnCard gauntletExtranceSpawnCard;

		// Token: 0x04003473 RID: 13427
		[SerializeField]
		private ScriptedCombatEncounter[] phaseEncounters;

		// Token: 0x04003474 RID: 13428
		[SerializeField]
		private LoopSoundDef gauntletActiveLoop;

		// Token: 0x04003475 RID: 13429
		private VoidRaidGauntletController.GauntletInfo previousGauntlet;

		// Token: 0x04003476 RID: 13430
		private VoidRaidGauntletController.DonutInfo previousDonut;

		// Token: 0x04003477 RID: 13431
		private VoidRaidGauntletController.GauntletInfo currentGauntlet;

		// Token: 0x04003478 RID: 13432
		private VoidRaidGauntletController.DonutInfo currentDonut;

		// Token: 0x04003479 RID: 13433
		private CharacterBody bossBody;

		// Token: 0x0400347A RID: 13434
		private LoopSoundManager.SoundLoopPtr gauntletActiveLoopPtr;

		// Token: 0x0400347B RID: 13435
		[SyncVar(hook = "TryShuffleData")]
		private ulong rngSeed;

		// Token: 0x0400347C RID: 13436
		private bool hasShuffled;

		// Token: 0x0400347D RID: 13437
		private int gauntletIndex;

		// Token: 0x0400347E RID: 13438
		private static int kRpcRpcStartActiveSoundLoop = -755317963;

		// Token: 0x0400347F RID: 13439
		private static int kRpcRpcActivateGate;

		// Token: 0x04003480 RID: 13440
		private static int kRpcRpcActivateDonut;

		// Token: 0x04003481 RID: 13441
		private static int kRpcRpcTryShuffleData;

		// Token: 0x020008EF RID: 2287
		[Serializable]
		public class GauntletInfo
		{
			// Token: 0x04003482 RID: 13442
			public Transform startPoint;

			// Token: 0x04003483 RID: 13443
			public Transform effectRoot;

			// Token: 0x04003484 RID: 13444
			public float startRadius = 100f;

			// Token: 0x04003485 RID: 13445
			public MapZone exitZone;

			// Token: 0x04003486 RID: 13446
			public MapZone entranceZone;

			// Token: 0x04003487 RID: 13447
			public string gateName;
		}

		// Token: 0x020008F0 RID: 2288
		[Serializable]
		public class DonutInfo
		{
			// Token: 0x04003488 RID: 13448
			public GameObject root;

			// Token: 0x04003489 RID: 13449
			public Transform returnPoint;

			// Token: 0x0400348A RID: 13450
			public float returnRadius = 1000f;

			// Token: 0x0400348B RID: 13451
			public Transform crabPosition;

			// Token: 0x0400348C RID: 13452
			public CombatDirector combatDirector;
		}
	}
}
