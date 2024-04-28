using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using EntityStates;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020005D9 RID: 1497
	public class ArtifactTrialMissionController : NetworkBehaviour
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06001B1E RID: 6942 RVA: 0x00074690 File Offset: 0x00072890
		public ArtifactDef currentArtifact
		{
			get
			{
				return ArtifactCatalog.GetArtifactDef((ArtifactIndex)this.currentArtifactIndex);
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06001B1F RID: 6943 RVA: 0x000746A0 File Offset: 0x000728A0
		// (remove) Token: 0x06001B20 RID: 6944 RVA: 0x000746D4 File Offset: 0x000728D4
		public static event Action<ArtifactTrialMissionController, DamageReport> onShellTakeDamageServer;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06001B21 RID: 6945 RVA: 0x00074708 File Offset: 0x00072908
		// (remove) Token: 0x06001B22 RID: 6946 RVA: 0x0007473C File Offset: 0x0007293C
		public static event Action<ArtifactTrialMissionController, DamageReport> onShellDeathServer;

		// Token: 0x06001B23 RID: 6947 RVA: 0x00074770 File Offset: 0x00072970
		private void TrySetCurrentArtifact(int newArtifactIndex)
		{
			Debug.LogFormat("TrySetCurrentArtifact currentArtifactIndex={0} newArtifactIndex={1}", new object[]
			{
				this.currentArtifactIndex,
				newArtifactIndex
			});
			if (newArtifactIndex == this.currentArtifactIndex)
			{
				return;
			}
			base.syncVarHookGuard = true;
			this.SetCurrentArtifact(newArtifactIndex);
			base.syncVarHookGuard = false;
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x000747C3 File Offset: 0x000729C3
		private void SetCurrentArtifact(int newArtifactIndex)
		{
			if (this.currentArtifact)
			{
				this.OnCurrentArtifactLost(this.currentArtifact);
			}
			this.NetworkcurrentArtifactIndex = newArtifactIndex;
			if (this.currentArtifact)
			{
				this.OnCurrentArtifactDiscovered(this.currentArtifact);
			}
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x000747FE File Offset: 0x000729FE
		private void Awake()
		{
			if (NetworkServer.active && ArtifactTrialMissionController.trialArtifact)
			{
				this.TrySetCurrentArtifact((int)ArtifactTrialMissionController.trialArtifact.artifactIndex);
				ArtifactTrialMissionController.trialArtifact = null;
			}
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x00074829 File Offset: 0x00072A29
		private void OnDestroy()
		{
			this.TrySetCurrentArtifact(-1);
			GlobalEventManager.onServerDamageDealt -= this.OnServerDamageDealt;
			if (NetworkServer.active)
			{
				ArtifactTrialMissionController.RemoveAllMissionKeys();
			}
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x00074850 File Offset: 0x00072A50
		public override void OnStartServer()
		{
			base.OnStartServer();
			this.rng = new Xoroshiro128Plus(Run.instance.stageRng.nextUlong);
			GlobalEventManager.onServerDamageDealt += this.OnServerDamageDealt;
			GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeathGlobal;
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0007489F File Offset: 0x00072A9F
		public override void OnStartClient()
		{
			base.OnStartClient();
			if (!NetworkServer.active)
			{
				this.SetCurrentArtifact(this.currentArtifactIndex);
			}
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x000748BC File Offset: 0x00072ABC
		private void OnCurrentArtifactDiscovered(ArtifactDef artifactDef)
		{
			if (NetworkServer.active && artifactDef)
			{
				this.artifactWasEnabled = RunArtifactManager.instance.IsArtifactEnabled(artifactDef);
				RunArtifactManager.instance.SetArtifactEnabledServer(artifactDef, true);
				if (this.artifactPickup)
				{
					this.artifactPickup.NetworkpickupIndex = PickupCatalog.FindPickupIndex(artifactDef.artifactIndex);
				}
			}
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x00074918 File Offset: 0x00072B18
		private void OnCurrentArtifactLost(ArtifactDef artifactDef)
		{
			if (NetworkServer.active)
			{
				if (artifactDef && RunArtifactManager.instance)
				{
					RunArtifactManager.instance.SetArtifactEnabledServer(artifactDef, this.artifactWasEnabled);
				}
				if (this.artifactPickup)
				{
					this.artifactPickup.NetworkpickupIndex = PickupIndex.none;
				}
			}
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x0007496E File Offset: 0x00072B6E
		private void OnServerDamageDealt(DamageReport damageReport)
		{
			if (damageReport.victimMaster == this.artifactShellMaster)
			{
				this.OnShellTakeDamageServer(damageReport);
			}
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x00074985 File Offset: 0x00072B85
		private void OnCharacterDeathGlobal(DamageReport damageReport)
		{
			if (damageReport.victimMaster == this.artifactShellMaster)
			{
				this.OnShellDeathServer(damageReport);
			}
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x0007499C File Offset: 0x00072B9C
		private void OnShellTakeDamageServer(DamageReport damageReport)
		{
			ArtifactTrialMissionController.RemoveAllMissionKeys();
			Action<ArtifactTrialMissionController, DamageReport> action = ArtifactTrialMissionController.onShellTakeDamageServer;
			if (action == null)
			{
				return;
			}
			action(this, damageReport);
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x000749B4 File Offset: 0x00072BB4
		private void OnShellDeathServer(DamageReport damageReport)
		{
			Action<ArtifactTrialMissionController, DamageReport> action = ArtifactTrialMissionController.onShellDeathServer;
			if (action == null)
			{
				return;
			}
			action(this, damageReport);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x000749C8 File Offset: 0x00072BC8
		public PickupIndex GenerateDrop()
		{
			if (this.keyDropTable)
			{
				Debug.Log("Generating key drop.");
				PickupIndex pickupIndex = this.keyDropTable.GenerateDrop(this.rng);
				Debug.LogFormat("itemIndex = {0}, isValid = {1}, pickupNameToken = {2}", new object[]
				{
					pickupIndex.itemIndex,
					pickupIndex.isValid,
					pickupIndex.GetPickupNameToken()
				});
				return this.keyDropTable.GenerateDrop(this.rng);
			}
			Debug.LogError("Failed to generate key drop!");
			return PickupIndex.none;
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x00074A58 File Offset: 0x00072C58
		public static void RemoveAllMissionKeys()
		{
			ItemIndex itemIndex = RoR2Content.Items.ArtifactKey.itemIndex;
			PickupIndex pickupIndex = PickupCatalog.FindPickupIndex(itemIndex);
			foreach (CharacterMaster characterMaster in CharacterMaster.readOnlyInstancesList)
			{
				int itemCount = characterMaster.inventory.GetItemCount(itemIndex);
				if (itemCount > 0)
				{
					characterMaster.inventory.RemoveItem(itemIndex, itemCount);
				}
			}
			List<GenericPickupController> instancesList = InstanceTracker.GetInstancesList<GenericPickupController>();
			for (int i = instancesList.Count - 1; i >= 0; i--)
			{
				GenericPickupController genericPickupController = instancesList[i];
				if (genericPickupController.pickupIndex == pickupIndex)
				{
					UnityEngine.Object.Destroy(genericPickupController.gameObject);
				}
			}
			List<PickupPickerController> instancesList2 = InstanceTracker.GetInstancesList<PickupPickerController>();
			for (int j = instancesList2.Count - 1; j >= 0; j--)
			{
				PickupPickerController pickupPickerController = instancesList2[j];
				if (pickupPickerController.IsChoiceAvailable(pickupIndex))
				{
					UnityEngine.Object.Destroy(pickupPickerController.gameObject);
				}
			}
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x00074B74 File Offset: 0x00072D74
		// (set) Token: 0x06001B34 RID: 6964 RVA: 0x00074B87 File Offset: 0x00072D87
		public int NetworkcurrentArtifactIndex
		{
			get
			{
				return this.currentArtifactIndex;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.TrySetCurrentArtifact(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<int>(value, ref this.currentArtifactIndex, 1U);
			}
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x00074BC8 File Offset: 0x00072DC8
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.WritePackedUInt32((uint)this.currentArtifactIndex);
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
				writer.WritePackedUInt32((uint)this.currentArtifactIndex);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x00074C34 File Offset: 0x00072E34
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.currentArtifactIndex = (int)reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.TrySetCurrentArtifact((int)reader.ReadPackedUInt32());
			}
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002143 RID: 8515
		[Header("Artifact")]
		public GenericPickupController artifactPickup;

		// Token: 0x04002144 RID: 8516
		public CharacterMaster artifactShellMaster;

		// Token: 0x04002145 RID: 8517
		public GameObject destroyDisplayRingObject;

		// Token: 0x04002146 RID: 8518
		[Header("Intro Cutscene")]
		public ForcedCamera introCameraController;

		// Token: 0x04002147 RID: 8519
		[Header("Mission Key Parameters")]
		public Transform initialKeyLocation;

		// Token: 0x04002148 RID: 8520
		public float chanceForKeyDrop = 0.04f;

		// Token: 0x04002149 RID: 8521
		public PickupDropTable keyDropTable;

		// Token: 0x0400214A RID: 8522
		[Header("Exit Portal")]
		public GameObject exitPortalPrefab;

		// Token: 0x0400214B RID: 8523
		public Transform exitPortalLocation;

		// Token: 0x0400214C RID: 8524
		[Header("Combat")]
		public CombatDirector[] combatDirectors;

		// Token: 0x0400214D RID: 8525
		public BossGroup bossGroup;

		// Token: 0x0400214E RID: 8526
		public static ArtifactDef trialArtifact;

		// Token: 0x0400214F RID: 8527
		[SyncVar(hook = "TrySetCurrentArtifact")]
		private int currentArtifactIndex = -1;

		// Token: 0x04002152 RID: 8530
		private bool artifactWasEnabled;

		// Token: 0x04002153 RID: 8531
		private Xoroshiro128Plus rng;

		// Token: 0x020005DA RID: 1498
		private class ArtifactTrialMissionControllerBaseState : EntityState
		{
			// Token: 0x170001CF RID: 463
			// (get) Token: 0x06001B38 RID: 6968 RVA: 0x00074C75 File Offset: 0x00072E75
			// (set) Token: 0x06001B39 RID: 6969 RVA: 0x00074C7D File Offset: 0x00072E7D
			private protected ArtifactTrialMissionController missionController { protected get; private set; }

			// Token: 0x170001D0 RID: 464
			// (get) Token: 0x06001B3A RID: 6970 RVA: 0x0000CF8A File Offset: 0x0000B18A
			protected virtual bool shouldEnableCombatDirector
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001D1 RID: 465
			// (get) Token: 0x06001B3B RID: 6971 RVA: 0x0000CF8A File Offset: 0x0000B18A
			protected virtual bool shouldEnableBossGroup
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001D2 RID: 466
			// (get) Token: 0x06001B3C RID: 6972 RVA: 0x00074C86 File Offset: 0x00072E86
			protected virtual bool shouldAllowMonsters
			{
				get
				{
					return this.shouldEnableCombatDirector;
				}
			}

			// Token: 0x06001B3D RID: 6973 RVA: 0x00074C90 File Offset: 0x00072E90
			public override void OnEnter()
			{
				base.OnEnter();
				this.missionController = base.GetComponent<ArtifactTrialMissionController>();
				if (NetworkServer.active)
				{
					CombatDirector[] combatDirectors = this.missionController.combatDirectors;
					for (int i = 0; i < combatDirectors.Length; i++)
					{
						combatDirectors[i].enabled = this.shouldEnableCombatDirector;
					}
					if (!this.shouldAllowMonsters)
					{
						for (int j = CharacterMaster.readOnlyInstancesList.Count - 1; j >= 0; j--)
						{
							CharacterMaster characterMaster = CharacterMaster.readOnlyInstancesList[j];
							if (characterMaster.teamIndex == TeamIndex.Monster && characterMaster != this.missionController.artifactShellMaster)
							{
								characterMaster.TrueKill();
							}
						}
					}
				}
				if (this.missionController.bossGroup)
				{
					this.missionController.bossGroup.enabled = this.shouldEnableBossGroup;
				}
			}

			// Token: 0x06001B3E RID: 6974 RVA: 0x00074D4E File Offset: 0x00072F4E
			public override void OnExit()
			{
				this.missionController = null;
				base.OnExit();
			}
		}

		// Token: 0x020005DB RID: 1499
		private class WaitForPlayersState : ArtifactTrialMissionController.ArtifactTrialMissionControllerBaseState
		{
			// Token: 0x06001B40 RID: 6976 RVA: 0x00074D5D File Offset: 0x00072F5D
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active && NetworkUser.AllParticipatingNetworkUsersReady())
				{
					this.outer.SetNextState(new ArtifactTrialMissionController.IntroState());
					return;
				}
			}

			// Token: 0x06001B41 RID: 6977 RVA: 0x00074D84 File Offset: 0x00072F84
			public override void OnEnter()
			{
				base.OnEnter();
				FadeToBlackManager.fadeCount++;
				FadeToBlackManager.ForceFullBlack();
			}

			// Token: 0x06001B42 RID: 6978 RVA: 0x00074D9D File Offset: 0x00072F9D
			public override void OnExit()
			{
				FadeToBlackManager.fadeCount--;
				base.OnExit();
			}
		}

		// Token: 0x020005DC RID: 1500
		private class IntroState : ArtifactTrialMissionController.ArtifactTrialMissionControllerBaseState
		{
			// Token: 0x06001B44 RID: 6980 RVA: 0x00074DB9 File Offset: 0x00072FB9
			public override void OnEnter()
			{
				base.OnEnter();
				ArtifactTrialMissionController missionController = base.missionController;
				this.cameraController = ((missionController != null) ? missionController.introCameraController : null);
				if (this.cameraController)
				{
					this.cameraController.gameObject.SetActive(true);
				}
			}

			// Token: 0x06001B45 RID: 6981 RVA: 0x00074DF7 File Offset: 0x00072FF7
			public override void OnExit()
			{
				if (this.cameraController)
				{
					this.cameraController.enabled = false;
					this.cameraController = null;
				}
				base.OnExit();
			}

			// Token: 0x06001B46 RID: 6982 RVA: 0x00074E1F File Offset: 0x0007301F
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active && !this.cameraController.enabled)
				{
					this.outer.SetNextState(new ArtifactTrialMissionController.SetupState());
					return;
				}
			}

			// Token: 0x04002155 RID: 8533
			private ForcedCamera cameraController;
		}

		// Token: 0x020005DD RID: 1501
		private class SetupState : ArtifactTrialMissionController.ArtifactTrialMissionControllerBaseState
		{
			// Token: 0x06001B48 RID: 6984 RVA: 0x00074E4C File Offset: 0x0007304C
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active)
				{
					CharacterBody body = base.missionController.artifactShellMaster.GetBody();
					if (!body || body.healthComponent.combinedHealthFraction < 1f)
					{
						this.outer.SetNextState(new ArtifactTrialMissionController.PreCombatState());
						return;
					}
					if (!this.keyPickupInstance)
					{
						this.keyRespawnTimer -= Time.deltaTime;
						if (this.keyRespawnTimer < 0f)
						{
							this.keyRespawnTimer = 30f;
							GenericPickupController.CreatePickupInfo createPickupInfo = default(GenericPickupController.CreatePickupInfo);
							createPickupInfo.pickupIndex = PickupCatalog.FindPickupIndex(RoR2Content.Items.ArtifactKey.itemIndex);
							createPickupInfo.position = base.missionController.initialKeyLocation.position;
							createPickupInfo.rotation = Quaternion.identity;
							this.keyPickupInstance = GenericPickupController.CreatePickup(createPickupInfo).gameObject;
						}
					}
				}
				if (!this.pushedNotification && base.fixedAge > ArtifactTrialMissionController.SetupState.delayBeforePushingNotification && base.fixedAge > 1.5f)
				{
					this.pushedNotification = true;
					foreach (CharacterMaster characterMaster in CharacterMaster.readOnlyInstancesList)
					{
						if (characterMaster.playerCharacterMasterController && characterMaster.playerCharacterMasterController.networkUserObject && characterMaster.playerCharacterMasterController.networkUserObject.GetComponent<NetworkIdentity>().isLocalPlayer)
						{
							CharacterMasterNotificationQueue.PushArtifactNotification(characterMaster, base.missionController.currentArtifact);
						}
					}
				}
			}

			// Token: 0x04002156 RID: 8534
			public static float delayBeforePushingNotification;

			// Token: 0x04002157 RID: 8535
			private float keyRespawnTimer;

			// Token: 0x04002158 RID: 8536
			private GameObject keyPickupInstance;

			// Token: 0x04002159 RID: 8537
			private bool pushedNotification;
		}

		// Token: 0x020005DE RID: 1502
		private class PreCombatState : ArtifactTrialMissionController.ArtifactTrialMissionControllerBaseState
		{
			// Token: 0x170001D3 RID: 467
			// (get) Token: 0x06001B4A RID: 6986 RVA: 0x0000B4B7 File Offset: 0x000096B7
			protected override bool shouldAllowMonsters
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B4B RID: 6987 RVA: 0x00074FE0 File Offset: 0x000731E0
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active && base.fixedAge >= ArtifactTrialMissionController.PreCombatState.baseDuration)
				{
					this.outer.SetNextState(new ArtifactTrialMissionController.CombatState());
				}
			}

			// Token: 0x0400215A RID: 8538
			private static float baseDuration = 1f;
		}

		// Token: 0x020005DF RID: 1503
		private class CombatState : ArtifactTrialMissionController.ArtifactTrialMissionControllerBaseState
		{
			// Token: 0x170001D4 RID: 468
			// (get) Token: 0x06001B4E RID: 6990 RVA: 0x0000B4B7 File Offset: 0x000096B7
			protected override bool shouldEnableCombatDirector
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170001D5 RID: 469
			// (get) Token: 0x06001B4F RID: 6991 RVA: 0x0000B4B7 File Offset: 0x000096B7
			protected override bool shouldEnableBossGroup
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06001B50 RID: 6992 RVA: 0x00075018 File Offset: 0x00073218
			public override void OnEnter()
			{
				base.OnEnter();
				if (NetworkServer.active)
				{
					GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeathGlobal;
				}
			}

			// Token: 0x06001B51 RID: 6993 RVA: 0x00075038 File Offset: 0x00073238
			private void OnCharacterDeathGlobal(DamageReport damageReport)
			{
				if (Util.CheckRoll(Util.GetExpAdjustedDropChancePercent(base.missionController.chanceForKeyDrop * 100f, damageReport.victim.gameObject), 0f, null))
				{
					Debug.LogFormat("Creating artifact key pickup droplet.", Array.Empty<object>());
					PickupDropletController.CreatePickupDroplet(base.missionController.GenerateDrop(), damageReport.victimBody.corePosition, Vector3.up * 20f);
				}
			}

			// Token: 0x06001B52 RID: 6994 RVA: 0x000750AC File Offset: 0x000732AC
			public override void OnExit()
			{
				GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeathGlobal;
				base.OnExit();
			}

			// Token: 0x06001B53 RID: 6995 RVA: 0x000750C8 File Offset: 0x000732C8
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active && (!base.missionController.artifactShellMaster || !base.missionController.artifactShellMaster.hasBody))
				{
					this.outer.SetNextState(new ArtifactTrialMissionController.WaitForRewardTaken());
				}
			}
		}

		// Token: 0x020005E0 RID: 1504
		private class WaitForRewardTaken : ArtifactTrialMissionController.ArtifactTrialMissionControllerBaseState
		{
			// Token: 0x06001B55 RID: 6997 RVA: 0x00075116 File Offset: 0x00073316
			public override void OnEnter()
			{
				base.OnEnter();
				if (base.missionController.destroyDisplayRingObject)
				{
					base.missionController.destroyDisplayRingObject.SetActive(true);
				}
			}

			// Token: 0x06001B56 RID: 6998 RVA: 0x00075144 File Offset: 0x00073344
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active && !base.missionController.artifactPickup)
				{
					this.timer -= Time.fixedDeltaTime;
					if (this.timer <= 0f)
					{
						this.outer.SetNextState(new ArtifactTrialMissionController.SpawnExitPortalAndIdle());
					}
				}
			}

			// Token: 0x0400215B RID: 8539
			private float timer = 3f;

			// Token: 0x0400215C RID: 8540
			protected bool shouldShowBossHealthBar = true;
		}

		// Token: 0x020005E1 RID: 1505
		private class SpawnExitPortalAndIdle : ArtifactTrialMissionController.ArtifactTrialMissionControllerBaseState
		{
			// Token: 0x06001B58 RID: 7000 RVA: 0x000751BC File Offset: 0x000733BC
			public override void OnEnter()
			{
				base.OnEnter();
				if (NetworkServer.active)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(base.missionController.exitPortalPrefab, base.missionController.exitPortalLocation.position, base.missionController.exitPortalLocation.rotation);
					gameObject.GetComponent<SceneExitController>().useRunNextStageScene = true;
					NetworkServer.Spawn(gameObject);
				}
			}
		}
	}
}
