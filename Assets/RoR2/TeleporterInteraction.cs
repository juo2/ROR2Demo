using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using EntityStates;
using RoR2.CharacterAI;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008B9 RID: 2233
	[RequireComponent(typeof(SceneExitController))]
	[RequireComponent(typeof(HoldoutZoneController))]
	public sealed class TeleporterInteraction : NetworkBehaviour, IInteractable
	{
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060031C7 RID: 12743 RVA: 0x000D2FC9 File Offset: 0x000D11C9
		// (set) Token: 0x060031C8 RID: 12744 RVA: 0x000D2FD1 File Offset: 0x000D11D1
		public HoldoutZoneController holdoutZoneController { get; private set; }

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060031C9 RID: 12745 RVA: 0x000D2FDA File Offset: 0x000D11DA
		// (set) Token: 0x060031CA RID: 12746 RVA: 0x000D2FE2 File Offset: 0x000D11E2
		public SceneExitController sceneExitController { get; private set; }

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060031CB RID: 12747 RVA: 0x000D2FEB File Offset: 0x000D11EB
		private TeleporterInteraction.BaseTeleporterState currentState
		{
			get
			{
				return this.mainStateMachine.state as TeleporterInteraction.BaseTeleporterState;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060031CC RID: 12748 RVA: 0x000D2FFD File Offset: 0x000D11FD
		public TeleporterInteraction.ActivationState activationState
		{
			get
			{
				TeleporterInteraction.BaseTeleporterState currentState = this.currentState;
				if (currentState == null)
				{
					return TeleporterInteraction.ActivationState.Idle;
				}
				return currentState.backwardsCompatibleActivationState;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060031CD RID: 12749 RVA: 0x000D3010 File Offset: 0x000D1210
		// (set) Token: 0x060031CE RID: 12750 RVA: 0x000D3018 File Offset: 0x000D1218
		public bool locked
		{
			get
			{
				return this._locked;
			}
			set
			{
				this.Network_locked = value;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060031CF RID: 12751 RVA: 0x000D3021 File Offset: 0x000D1221
		public bool isIdle
		{
			get
			{
				return this.currentState is TeleporterInteraction.IdleState;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x000D3031 File Offset: 0x000D1231
		public bool isIdleToCharging
		{
			get
			{
				return this.currentState is TeleporterInteraction.IdleToChargingState;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060031D1 RID: 12753 RVA: 0x000D3041 File Offset: 0x000D1241
		public bool isInFinalSequence
		{
			get
			{
				return this.currentState is TeleporterInteraction.FinishedState;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060031D2 RID: 12754 RVA: 0x000D3051 File Offset: 0x000D1251
		public bool isCharging
		{
			get
			{
				return this.currentState is TeleporterInteraction.ChargingState;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060031D3 RID: 12755 RVA: 0x000D3061 File Offset: 0x000D1261
		public bool isCharged
		{
			get
			{
				return this.activationState >= TeleporterInteraction.ActivationState.Charged;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060031D4 RID: 12756 RVA: 0x000D306F File Offset: 0x000D126F
		public float chargeFraction
		{
			get
			{
				return this.holdoutZoneController.charge;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060031D5 RID: 12757 RVA: 0x000D307C File Offset: 0x000D127C
		// (set) Token: 0x060031D6 RID: 12758 RVA: 0x000D3084 File Offset: 0x000D1284
		public int shrineBonusStacks { get; set; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060031D7 RID: 12759 RVA: 0x000D308D File Offset: 0x000D128D
		private int chargePercent
		{
			get
			{
				return this.holdoutZoneController.displayChargePercent;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060031D8 RID: 12760 RVA: 0x000D309A File Offset: 0x000D129A
		// (set) Token: 0x060031D9 RID: 12761 RVA: 0x000D30A1 File Offset: 0x000D12A1
		public static TeleporterInteraction instance { get; private set; }

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060031DA RID: 12762 RVA: 0x000D30A9 File Offset: 0x000D12A9
		// (set) Token: 0x060031DB RID: 12763 RVA: 0x000D30B1 File Offset: 0x000D12B1
		public bool shouldAttemptToSpawnShopPortal
		{
			get
			{
				return this._shouldAttemptToSpawnShopPortal;
			}
			set
			{
				if (this._shouldAttemptToSpawnShopPortal == value)
				{
					return;
				}
				this.Network_shouldAttemptToSpawnShopPortal = value;
				if (this._shouldAttemptToSpawnShopPortal && NetworkServer.active)
				{
					Chat.SendBroadcastChat(new Chat.SimpleChatMessage
					{
						baseToken = "PORTAL_SHOP_WILL_OPEN"
					});
				}
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060031DC RID: 12764 RVA: 0x000D30E8 File Offset: 0x000D12E8
		// (set) Token: 0x060031DD RID: 12765 RVA: 0x000D30F0 File Offset: 0x000D12F0
		public bool shouldAttemptToSpawnGoldshoresPortal
		{
			get
			{
				return this._shouldAttemptToSpawnGoldshoresPortal;
			}
			set
			{
				if (this._shouldAttemptToSpawnGoldshoresPortal == value)
				{
					return;
				}
				this.Network_shouldAttemptToSpawnGoldshoresPortal = value;
				if (this._shouldAttemptToSpawnGoldshoresPortal && NetworkServer.active)
				{
					Chat.SendBroadcastChat(new Chat.SimpleChatMessage
					{
						baseToken = "PORTAL_GOLDSHORES_WILL_OPEN"
					});
				}
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060031DE RID: 12766 RVA: 0x000D3127 File Offset: 0x000D1327
		// (set) Token: 0x060031DF RID: 12767 RVA: 0x000D312F File Offset: 0x000D132F
		public bool shouldAttemptToSpawnMSPortal
		{
			get
			{
				return this._shouldAttemptToSpawnMSPortal;
			}
			set
			{
				if (this._shouldAttemptToSpawnMSPortal == value)
				{
					return;
				}
				this.Network_shouldAttemptToSpawnMSPortal = value;
				if (this._shouldAttemptToSpawnMSPortal && NetworkServer.active)
				{
					Chat.SendBroadcastChat(new Chat.SimpleChatMessage
					{
						baseToken = "PORTAL_MS_WILL_OPEN"
					});
				}
			}
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000D3166 File Offset: 0x000D1366
		private void OnSyncShouldAttemptToSpawnShopPortal(bool newValue)
		{
			this.Network_shouldAttemptToSpawnShopPortal = newValue;
			if (this.modelChildLocator)
			{
				this.modelChildLocator.FindChild("ShopPortalIndicator").gameObject.SetActive(newValue);
			}
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000D3197 File Offset: 0x000D1397
		private void OnSyncShouldAttemptToSpawnGoldshoresPortal(bool newValue)
		{
			this.Network_shouldAttemptToSpawnGoldshoresPortal = newValue;
			if (this.modelChildLocator)
			{
				this.modelChildLocator.FindChild("GoldshoresPortalIndicator").gameObject.SetActive(newValue);
			}
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x000D31C8 File Offset: 0x000D13C8
		private void OnSyncShouldAttemptToSpawnMSPortal(bool newValue)
		{
			this.Network_shouldAttemptToSpawnMSPortal = newValue;
			if (this.modelChildLocator)
			{
				this.modelChildLocator.FindChild("MSPortalIndicator").gameObject.SetActive(newValue);
			}
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000D31FC File Offset: 0x000D13FC
		private void Awake()
		{
			this.modelChildLocator = base.GetComponent<ModelLocator>().modelTransform.GetComponent<ChildLocator>();
			this.holdoutZoneController = base.GetComponent<HoldoutZoneController>();
			this.sceneExitController = base.GetComponent<SceneExitController>();
			this.bossShrineIndicator = this.modelChildLocator.FindChild("BossShrineSymbol").gameObject;
			this.bossGroup = base.GetComponent<BossGroup>();
			if (NetworkServer.active && this.bossDirector)
			{
				this.bossDirector.onSpawnedServer.AddListener(new UnityAction<GameObject>(this.OnBossDirectorSpawnedMonsterServer));
			}
			this.teleporterPositionIndicator = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/PositionIndicators/TeleporterChargingPositionIndicator"), base.transform.position, Quaternion.identity).GetComponent<PositionIndicator>();
			this.teleporterPositionIndicator.targetTransform = base.transform;
			this.teleporterChargeIndicatorController = this.teleporterPositionIndicator.GetComponent<ChargeIndicatorController>();
			this.teleporterChargeIndicatorController.holdoutZoneController = this.holdoutZoneController;
			this.teleporterPositionIndicator.gameObject.SetActive(false);
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x000D32FC File Offset: 0x000D14FC
		private void OnDestroy()
		{
			if (this.teleporterPositionIndicator)
			{
				UnityEngine.Object.Destroy(this.teleporterPositionIndicator.gameObject);
				this.teleporterPositionIndicator = null;
				this.teleporterChargeIndicatorController = null;
			}
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000D3329 File Offset: 0x000D1529
		private void OnEnable()
		{
			TeleporterInteraction.instance = SingletonHelper.Assign<TeleporterInteraction>(TeleporterInteraction.instance, this);
			InstanceTracker.Add<TeleporterInteraction>(this);
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000D3341 File Offset: 0x000D1541
		private void OnDisable()
		{
			InstanceTracker.Remove<TeleporterInteraction>(this);
			TeleporterInteraction.instance = SingletonHelper.Unassign<TeleporterInteraction>(TeleporterInteraction.instance, this);
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000D335C File Offset: 0x000D155C
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.rng = new Xoroshiro128Plus(Run.instance.stageRng.nextUlong);
				Run.instance.PickNextStageSceneFromCurrentSceneDestinations();
				float nextNormalizedFloat = this.rng.nextNormalizedFloat;
				float num = this.baseShopSpawnChance / (float)(Run.instance.shopPortalCount + 1);
				this.shouldAttemptToSpawnShopPortal = (nextNormalizedFloat < num);
				int stageClearCount = Run.instance.stageClearCount;
				if ((stageClearCount + 1) % Run.stagesPerLoop == 3 && stageClearCount > Run.stagesPerLoop && !Run.instance.GetEventFlag("NoMysterySpace"))
				{
					this.shouldAttemptToSpawnMSPortal = true;
				}
			}
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x000D33F9 File Offset: 0x000D15F9
		public void FixedUpdate()
		{
			this.bossShrineIndicator.SetActive(this.showExtraBossesIndicator && !this.isCharged);
			this.teleporterPositionIndicator.gameObject.SetActive(!this.isIdle);
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000D3433 File Offset: 0x000D1633
		public string GetContextString(Interactor activator)
		{
			TeleporterInteraction.BaseTeleporterState currentState = this.currentState;
			if (currentState == null)
			{
				return null;
			}
			return currentState.GetContextString(activator);
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000D3447 File Offset: 0x000D1647
		public Interactability GetInteractability(Interactor activator)
		{
			if (this.locked)
			{
				return Interactability.Disabled;
			}
			TeleporterInteraction.BaseTeleporterState currentState = this.currentState;
			if (currentState == null)
			{
				return Interactability.Disabled;
			}
			return currentState.GetInteractability(activator);
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000D3465 File Offset: 0x000D1665
		public void OnInteractionBegin(Interactor activator)
		{
			this.CallRpcClientOnActivated(activator.gameObject);
			TeleporterInteraction.BaseTeleporterState currentState = this.currentState;
			if (currentState == null)
			{
				return;
			}
			currentState.OnInteractionBegin(activator);
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public bool ShouldShowOnScanner()
		{
			return true;
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public bool ShouldIgnoreSpherecastForInteractibility(Interactor activator)
		{
			return false;
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x000D3484 File Offset: 0x000D1684
		[ClientRpc]
		private void RpcClientOnActivated(GameObject activatorObject)
		{
			Util.PlaySound("Play_env_teleporter_active_button", base.gameObject);
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x000D3497 File Offset: 0x000D1697
		private void UpdateMonstersClear()
		{
			this.monstersCleared = !this.bossGroup.enabled;
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x000D34B0 File Offset: 0x000D16B0
		[Server]
		public void AddShrineStack()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.TeleporterInteraction::AddShrineStack()' called on client");
				return;
			}
			if (this.activationState <= TeleporterInteraction.ActivationState.IdleToCharging)
			{
				BossGroup bossGroup = this.bossGroup;
				int num = bossGroup.bonusRewardCount + 1;
				bossGroup.bonusRewardCount = num;
				num = this.shrineBonusStacks;
				this.shrineBonusStacks = num + 1;
				this.NetworkshowExtraBossesIndicator = true;
			}
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x000D3507 File Offset: 0x000D1707
		[Obsolete]
		public bool IsInChargingRange(CharacterBody body)
		{
			return this.holdoutZoneController.IsBodyInChargingRadius(body);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000D3518 File Offset: 0x000D1718
		private void OnBossDirectorSpawnedMonsterServer(GameObject masterObject)
		{
			if (this.chargeActivatorServer)
			{
				TeleporterInteraction.<>c__DisplayClass85_0 CS$<>8__locals1 = new TeleporterInteraction.<>c__DisplayClass85_0();
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.ai = masterObject.GetComponent<BaseAI>();
				if (!CS$<>8__locals1.ai)
				{
					return;
				}
				CS$<>8__locals1.ai.onBodyDiscovered += CS$<>8__locals1.<OnBossDirectorSpawnedMonsterServer>g__AiOnBodyDiscovered|0;
			}
		}

		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x060031F3 RID: 12787 RVA: 0x000D3570 File Offset: 0x000D1770
		// (remove) Token: 0x060031F4 RID: 12788 RVA: 0x000D35A4 File Offset: 0x000D17A4
		public static event Action<TeleporterInteraction> onTeleporterBeginChargingGlobal;

		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x060031F5 RID: 12789 RVA: 0x000D35D8 File Offset: 0x000D17D8
		// (remove) Token: 0x060031F6 RID: 12790 RVA: 0x000D360C File Offset: 0x000D180C
		public static event Action<TeleporterInteraction> onTeleporterChargedGlobal;

		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x060031F7 RID: 12791 RVA: 0x000D3640 File Offset: 0x000D1840
		// (remove) Token: 0x060031F8 RID: 12792 RVA: 0x000D3674 File Offset: 0x000D1874
		public static event Action<TeleporterInteraction> onTeleporterFinishGlobal;

		// Token: 0x060031F9 RID: 12793 RVA: 0x000D36A8 File Offset: 0x000D18A8
		[Server]
		private void AttemptToSpawnAllEligiblePortals()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.TeleporterInteraction::AttemptToSpawnAllEligiblePortals()' called on client");
				return;
			}
			if (this.shouldAttemptToSpawnShopPortal)
			{
				this.AttemptToSpawnShopPortal();
			}
			if (this.shouldAttemptToSpawnGoldshoresPortal)
			{
				this.AttemptToSpawnGoldshoresPortal();
			}
			if (this.shouldAttemptToSpawnMSPortal)
			{
				this.AttemptToSpawnMSPortal();
			}
			PortalSpawner[] array = this.portalSpawners;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].AttemptSpawnPortalServer();
			}
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x000D3714 File Offset: 0x000D1914
		private bool AttemptSpawnPortal(SpawnCard portalSpawnCard, float minDistance, float maxDistance, string successChatToken)
		{
			GameObject exists = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(portalSpawnCard, new DirectorPlacementRule
			{
				minDistance = minDistance,
				maxDistance = maxDistance,
				placementMode = DirectorPlacementRule.PlacementMode.Approximate,
				position = base.transform.position,
				spawnOnTarget = base.transform
			}, this.rng));
			if (exists)
			{
				Chat.SendBroadcastChat(new Chat.SimpleChatMessage
				{
					baseToken = successChatToken
				});
			}
			return exists;
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x000D3790 File Offset: 0x000D1990
		[Server]
		private void AttemptToSpawnShopPortal()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.TeleporterInteraction::AttemptToSpawnShopPortal()' called on client");
				return;
			}
			if (this.AttemptSpawnPortal(this.shopPortalSpawnCard, 0f, 30f, "PORTAL_SHOP_OPEN"))
			{
				Run.instance.shopPortalCount++;
			}
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x000D37E1 File Offset: 0x000D19E1
		[Server]
		private void AttemptToSpawnGoldshoresPortal()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.TeleporterInteraction::AttemptToSpawnGoldshoresPortal()' called on client");
				return;
			}
			this.AttemptSpawnPortal(this.goldshoresPortalSpawnCard, 10f, 40f, "PORTAL_GOLDSHORES_OPEN");
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x000D3814 File Offset: 0x000D1A14
		[Server]
		private void AttemptToSpawnMSPortal()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.TeleporterInteraction::AttemptToSpawnMSPortal()' called on client");
				return;
			}
			this.AttemptSpawnPortal(this.msPortalSpawnCard, 10f, 40f, "PORTAL_MS_OPEN");
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06003200 RID: 12800 RVA: 0x000D385C File Offset: 0x000D1A5C
		// (set) Token: 0x06003201 RID: 12801 RVA: 0x000D386F File Offset: 0x000D1A6F
		public bool Network_locked
		{
			get
			{
				return this._locked;
			}
			[param: In]
			set
			{
				base.SetSyncVar<bool>(value, ref this._locked, 1U);
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06003202 RID: 12802 RVA: 0x000D3884 File Offset: 0x000D1A84
		// (set) Token: 0x06003203 RID: 12803 RVA: 0x000D3897 File Offset: 0x000D1A97
		public bool Network_shouldAttemptToSpawnShopPortal
		{
			get
			{
				return this._shouldAttemptToSpawnShopPortal;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnSyncShouldAttemptToSpawnShopPortal(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<bool>(value, ref this._shouldAttemptToSpawnShopPortal, 2U);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06003204 RID: 12804 RVA: 0x000D38D8 File Offset: 0x000D1AD8
		// (set) Token: 0x06003205 RID: 12805 RVA: 0x000D38EB File Offset: 0x000D1AEB
		public bool Network_shouldAttemptToSpawnGoldshoresPortal
		{
			get
			{
				return this._shouldAttemptToSpawnGoldshoresPortal;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnSyncShouldAttemptToSpawnGoldshoresPortal(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<bool>(value, ref this._shouldAttemptToSpawnGoldshoresPortal, 4U);
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06003206 RID: 12806 RVA: 0x000D392C File Offset: 0x000D1B2C
		// (set) Token: 0x06003207 RID: 12807 RVA: 0x000D393F File Offset: 0x000D1B3F
		public bool Network_shouldAttemptToSpawnMSPortal
		{
			get
			{
				return this._shouldAttemptToSpawnMSPortal;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnSyncShouldAttemptToSpawnMSPortal(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<bool>(value, ref this._shouldAttemptToSpawnMSPortal, 8U);
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06003208 RID: 12808 RVA: 0x000D3980 File Offset: 0x000D1B80
		// (set) Token: 0x06003209 RID: 12809 RVA: 0x000D3993 File Offset: 0x000D1B93
		public bool NetworkshowExtraBossesIndicator
		{
			get
			{
				return this.showExtraBossesIndicator;
			}
			[param: In]
			set
			{
				base.SetSyncVar<bool>(value, ref this.showExtraBossesIndicator, 16U);
			}
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x000D39A7 File Offset: 0x000D1BA7
		protected static void InvokeRpcRpcClientOnActivated(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcClientOnActivated called on server.");
				return;
			}
			((TeleporterInteraction)obj).RpcClientOnActivated(reader.ReadGameObject());
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x000D39D0 File Offset: 0x000D1BD0
		public void CallRpcClientOnActivated(GameObject activatorObject)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcClientOnActivated called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)TeleporterInteraction.kRpcRpcClientOnActivated);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(activatorObject);
			this.SendRPCInternal(networkWriter, 0, "RpcClientOnActivated");
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x000D3A43 File Offset: 0x000D1C43
		static TeleporterInteraction()
		{
			NetworkBehaviour.RegisterRpcDelegate(typeof(TeleporterInteraction), TeleporterInteraction.kRpcRpcClientOnActivated, new NetworkBehaviour.CmdDelegate(TeleporterInteraction.InvokeRpcRpcClientOnActivated));
			NetworkCRC.RegisterBehaviour("TeleporterInteraction", 0);
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x000D3A80 File Offset: 0x000D1C80
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this._locked);
				writer.Write(this._shouldAttemptToSpawnShopPortal);
				writer.Write(this._shouldAttemptToSpawnGoldshoresPortal);
				writer.Write(this._shouldAttemptToSpawnMSPortal);
				writer.Write(this.showExtraBossesIndicator);
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
				writer.Write(this._locked);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this._shouldAttemptToSpawnShopPortal);
			}
			if ((base.syncVarDirtyBits & 4U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this._shouldAttemptToSpawnGoldshoresPortal);
			}
			if ((base.syncVarDirtyBits & 8U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this._shouldAttemptToSpawnMSPortal);
			}
			if ((base.syncVarDirtyBits & 16U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.showExtraBossesIndicator);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x000D3BE8 File Offset: 0x000D1DE8
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this._locked = reader.ReadBoolean();
				this._shouldAttemptToSpawnShopPortal = reader.ReadBoolean();
				this._shouldAttemptToSpawnGoldshoresPortal = reader.ReadBoolean();
				this._shouldAttemptToSpawnMSPortal = reader.ReadBoolean();
				this.showExtraBossesIndicator = reader.ReadBoolean();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this._locked = reader.ReadBoolean();
			}
			if ((num & 2) != 0)
			{
				this.OnSyncShouldAttemptToSpawnShopPortal(reader.ReadBoolean());
			}
			if ((num & 4) != 0)
			{
				this.OnSyncShouldAttemptToSpawnGoldshoresPortal(reader.ReadBoolean());
			}
			if ((num & 8) != 0)
			{
				this.OnSyncShouldAttemptToSpawnMSPortal(reader.ReadBoolean());
			}
			if ((num & 16) != 0)
			{
				this.showExtraBossesIndicator = reader.ReadBoolean();
			}
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400332B RID: 13099
		[Header("Component and Object References")]
		public CombatDirector bonusDirector;

		// Token: 0x0400332C RID: 13100
		public CombatDirector bossDirector;

		// Token: 0x0400332D RID: 13101
		public EntityStateMachine mainStateMachine;

		// Token: 0x0400332E RID: 13102
		public OutsideInteractableLocker outsideInteractableLocker;

		// Token: 0x0400332F RID: 13103
		[Header("Interactability")]
		public string beginContextString;

		// Token: 0x04003330 RID: 13104
		public string exitContextString;

		// Token: 0x04003331 RID: 13105
		private BossGroup bossGroup;

		// Token: 0x04003332 RID: 13106
		private ChildLocator modelChildLocator;

		// Token: 0x04003335 RID: 13109
		[SyncVar]
		private bool _locked;

		// Token: 0x04003337 RID: 13111
		private PositionIndicator teleporterPositionIndicator;

		// Token: 0x04003338 RID: 13112
		private ChargeIndicatorController teleporterChargeIndicatorController;

		// Token: 0x0400333A RID: 13114
		private Color originalTeleporterColor;

		// Token: 0x0400333B RID: 13115
		private GameObject bossShrineIndicator;

		// Token: 0x0400333C RID: 13116
		[SyncVar(hook = "OnSyncShouldAttemptToSpawnShopPortal")]
		private bool _shouldAttemptToSpawnShopPortal;

		// Token: 0x0400333D RID: 13117
		[SyncVar(hook = "OnSyncShouldAttemptToSpawnGoldshoresPortal")]
		private bool _shouldAttemptToSpawnGoldshoresPortal;

		// Token: 0x0400333E RID: 13118
		[SyncVar(hook = "OnSyncShouldAttemptToSpawnMSPortal")]
		private bool _shouldAttemptToSpawnMSPortal;

		// Token: 0x0400333F RID: 13119
		private Xoroshiro128Plus rng;

		// Token: 0x04003340 RID: 13120
		private GameObject chargeActivatorServer;

		// Token: 0x04003341 RID: 13121
		private bool monstersCleared;

		// Token: 0x04003342 RID: 13122
		[SyncVar]
		private bool showExtraBossesIndicator;

		// Token: 0x04003346 RID: 13126
		public SpawnCard shopPortalSpawnCard;

		// Token: 0x04003347 RID: 13127
		public SpawnCard goldshoresPortalSpawnCard;

		// Token: 0x04003348 RID: 13128
		public SpawnCard msPortalSpawnCard;

		// Token: 0x04003349 RID: 13129
		public PortalSpawner[] portalSpawners;

		// Token: 0x0400334A RID: 13130
		public float baseShopSpawnChance = 0.375f;

		// Token: 0x0400334B RID: 13131
		private static int kRpcRpcClientOnActivated = 1157394167;

		// Token: 0x020008BA RID: 2234
		public enum ActivationState
		{
			// Token: 0x0400334D RID: 13133
			Idle,
			// Token: 0x0400334E RID: 13134
			IdleToCharging,
			// Token: 0x0400334F RID: 13135
			Charging,
			// Token: 0x04003350 RID: 13136
			Charged,
			// Token: 0x04003351 RID: 13137
			Finished
		}

		// Token: 0x020008BB RID: 2235
		private abstract class BaseTeleporterState : BaseState
		{
			// Token: 0x1700049A RID: 1178
			// (get) Token: 0x06003210 RID: 12816 RVA: 0x000D3CBD File Offset: 0x000D1EBD
			// (set) Token: 0x06003211 RID: 12817 RVA: 0x000D3CC5 File Offset: 0x000D1EC5
			private protected TeleporterInteraction teleporterInteraction { protected get; private set; }

			// Token: 0x06003212 RID: 12818 RVA: 0x000D3CCE File Offset: 0x000D1ECE
			public override void OnEnter()
			{
				base.OnEnter();
				this.teleporterInteraction = base.GetComponent<TeleporterInteraction>();
				this.teleporterInteraction.holdoutZoneController.enabled = this.shouldEnableChargingSphere;
			}

			// Token: 0x06003213 RID: 12819 RVA: 0x0000CF8A File Offset: 0x0000B18A
			public virtual Interactability GetInteractability(Interactor activator)
			{
				return Interactability.Disabled;
			}

			// Token: 0x06003214 RID: 12820 RVA: 0x00003BE8 File Offset: 0x00001DE8
			public virtual string GetContextString(Interactor activator)
			{
				return null;
			}

			// Token: 0x06003215 RID: 12821 RVA: 0x000026ED File Offset: 0x000008ED
			public virtual void OnInteractionBegin(Interactor activator)
			{
			}

			// Token: 0x06003216 RID: 12822 RVA: 0x000D3CF8 File Offset: 0x000D1EF8
			protected void SetChildActive(string childLocatorName, bool newActive)
			{
				Transform transform = this.teleporterInteraction.modelChildLocator.FindChild(childLocatorName);
				if (transform)
				{
					transform.gameObject.SetActive(newActive);
				}
			}

			// Token: 0x1700049B RID: 1179
			// (get) Token: 0x06003217 RID: 12823
			public abstract TeleporterInteraction.ActivationState backwardsCompatibleActivationState { get; }

			// Token: 0x1700049C RID: 1180
			// (get) Token: 0x06003218 RID: 12824 RVA: 0x0000CF8A File Offset: 0x0000B18A
			protected virtual bool shouldEnableChargingSphere
			{
				get
				{
					return false;
				}
			}
		}

		// Token: 0x020008BC RID: 2236
		private class IdleState : TeleporterInteraction.BaseTeleporterState
		{
			// Token: 0x0600321A RID: 12826 RVA: 0x0000EE13 File Offset: 0x0000D013
			public override Interactability GetInteractability(Interactor activator)
			{
				return Interactability.Available;
			}

			// Token: 0x0600321B RID: 12827 RVA: 0x000D3D2B File Offset: 0x000D1F2B
			public override string GetContextString(Interactor activator)
			{
				return Language.GetString(base.teleporterInteraction.beginContextString);
			}

			// Token: 0x0600321C RID: 12828 RVA: 0x000D3D40 File Offset: 0x000D1F40
			public override void OnInteractionBegin(Interactor activator)
			{
				Chat.SendBroadcastChat(new SubjectChatMessage
				{
					subjectAsCharacterBody = activator.GetComponent<CharacterBody>(),
					baseToken = "PLAYER_ACTIVATED_TELEPORTER"
				});
				if (base.teleporterInteraction.showExtraBossesIndicator)
				{
					Chat.SendBroadcastChat(new Chat.SimpleChatMessage
					{
						baseToken = "SHRINE_BOSS_BEGIN_TRIAL"
					});
				}
				base.teleporterInteraction.chargeActivatorServer = activator.gameObject;
				this.outer.SetNextState(new TeleporterInteraction.IdleToChargingState());
			}

			// Token: 0x1700049D RID: 1181
			// (get) Token: 0x0600321D RID: 12829 RVA: 0x0000CF8A File Offset: 0x0000B18A
			public override TeleporterInteraction.ActivationState backwardsCompatibleActivationState
			{
				get
				{
					return TeleporterInteraction.ActivationState.Idle;
				}
			}
		}

		// Token: 0x020008BD RID: 2237
		private class IdleToChargingState : TeleporterInteraction.BaseTeleporterState
		{
			// Token: 0x0600321F RID: 12831 RVA: 0x000D3DB9 File Offset: 0x000D1FB9
			public override void OnEnter()
			{
				base.OnEnter();
				base.SetChildActive("IdleToChargingEffect", true);
				base.SetChildActive("PPVolume", true);
			}

			// Token: 0x06003220 RID: 12832 RVA: 0x000D3DD9 File Offset: 0x000D1FD9
			public override void OnExit()
			{
				base.SetChildActive("IdleToChargingEffect", false);
				base.OnExit();
			}

			// Token: 0x06003221 RID: 12833 RVA: 0x000D3DED File Offset: 0x000D1FED
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (base.fixedAge > 3f && NetworkServer.active)
				{
					this.outer.SetNextState(new TeleporterInteraction.ChargingState());
				}
			}

			// Token: 0x1700049E RID: 1182
			// (get) Token: 0x06003222 RID: 12834 RVA: 0x0000B4B7 File Offset: 0x000096B7
			public override TeleporterInteraction.ActivationState backwardsCompatibleActivationState
			{
				get
				{
					return TeleporterInteraction.ActivationState.IdleToCharging;
				}
			}
		}

		// Token: 0x020008BE RID: 2238
		private class ChargingState : TeleporterInteraction.BaseTeleporterState
		{
			// Token: 0x1700049F RID: 1183
			// (get) Token: 0x06003224 RID: 12836 RVA: 0x000D3E19 File Offset: 0x000D2019
			private CombatDirector bonusDirector
			{
				get
				{
					return base.teleporterInteraction.bonusDirector;
				}
			}

			// Token: 0x170004A0 RID: 1184
			// (get) Token: 0x06003225 RID: 12837 RVA: 0x000D3E26 File Offset: 0x000D2026
			private CombatDirector bossDirector
			{
				get
				{
					return base.teleporterInteraction.bossDirector;
				}
			}

			// Token: 0x170004A1 RID: 1185
			// (get) Token: 0x06003226 RID: 12838 RVA: 0x0000B4B7 File Offset: 0x000096B7
			protected override bool shouldEnableChargingSphere
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06003227 RID: 12839 RVA: 0x000D3E34 File Offset: 0x000D2034
			public override void OnEnter()
			{
				base.OnEnter();
				Action<TeleporterInteraction> onTeleporterBeginChargingGlobal = TeleporterInteraction.onTeleporterBeginChargingGlobal;
				if (onTeleporterBeginChargingGlobal != null)
				{
					onTeleporterBeginChargingGlobal(base.teleporterInteraction);
				}
				if (NetworkServer.active)
				{
					if (this.bonusDirector)
					{
						this.bonusDirector.enabled = true;
					}
					if (this.bossDirector)
					{
						this.bossDirector.enabled = true;
						this.bossDirector.monsterCredit += (float)((int)(600f * Mathf.Pow(Run.instance.compensatedDifficultyCoefficient, 0.5f) * (float)(1 + base.teleporterInteraction.shrineBonusStacks)));
						this.bossDirector.currentSpawnTarget = base.gameObject;
						this.bossDirector.SetNextSpawnAsBoss();
					}
					if (DirectorCore.instance)
					{
						CombatDirector[] components = DirectorCore.instance.GetComponents<CombatDirector>();
						if (components.Length != 0)
						{
							CombatDirector[] array = components;
							for (int i = 0; i < array.Length; i++)
							{
								array[i].enabled = false;
							}
						}
					}
					if (base.teleporterInteraction.outsideInteractableLocker)
					{
						base.teleporterInteraction.outsideInteractableLocker.enabled = true;
					}
					ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(TeamIndex.Player);
					for (int j = 0; j < teamMembers.Count; j++)
					{
						TeamComponent teamComponent = teamMembers[j];
						CharacterBody body = teamComponent.body;
						if (body)
						{
							CharacterMaster master = teamComponent.body.master;
							if (master)
							{
								int itemCount = master.inventory.GetItemCount(RoR2Content.Items.WardOnLevel);
								if (itemCount > 0)
								{
									GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/WarbannerWard"), body.transform.position, Quaternion.identity);
									gameObject.GetComponent<TeamFilter>().teamIndex = TeamIndex.Player;
									gameObject.GetComponent<BuffWard>().Networkradius = 8f + 8f * (float)itemCount;
									NetworkServer.Spawn(gameObject);
								}
							}
						}
					}
				}
				base.SetChildActive("ChargingEffect", true);
			}

			// Token: 0x06003228 RID: 12840 RVA: 0x000D4010 File Offset: 0x000D2210
			public override void OnExit()
			{
				if (NetworkServer.active)
				{
					if (base.teleporterInteraction.outsideInteractableLocker)
					{
						base.teleporterInteraction.outsideInteractableLocker.enabled = false;
					}
					if (this.bossDirector)
					{
						this.bossDirector.enabled = false;
					}
					if (this.bonusDirector)
					{
						this.bonusDirector.enabled = false;
					}
				}
				base.SetChildActive("ChargingEffect", false);
				base.OnExit();
			}

			// Token: 0x06003229 RID: 12841 RVA: 0x000D408C File Offset: 0x000D228C
			public override void FixedUpdate()
			{
				base.FixedUpdate();
				if (NetworkServer.active)
				{
					if (base.teleporterInteraction.holdoutZoneController.charge >= 1f)
					{
						if (this.bonusDirector)
						{
							this.bonusDirector.enabled = false;
						}
						if (base.teleporterInteraction.monstersCleared)
						{
							if (this.bossDirector)
							{
								this.bossDirector.enabled = false;
							}
							this.outer.SetNextState(new TeleporterInteraction.ChargedState());
						}
					}
					if (base.teleporterInteraction.outsideInteractableLocker)
					{
						base.teleporterInteraction.outsideInteractableLocker.radius = base.teleporterInteraction.holdoutZoneController.currentRadius;
					}
				}
				if (SceneWeatherController.instance)
				{
					SceneWeatherController.instance.weatherLerp = SceneWeatherController.instance.weatherLerpOverChargeTime.Evaluate((float)base.teleporterInteraction.chargePercent * 0.01f);
				}
				base.teleporterInteraction.UpdateMonstersClear();
			}

			// Token: 0x0600322A RID: 12842 RVA: 0x0000B4B7 File Offset: 0x000096B7
			public override Interactability GetInteractability(Interactor activator)
			{
				return Interactability.ConditionsNotMet;
			}

			// Token: 0x170004A2 RID: 1186
			// (get) Token: 0x0600322B RID: 12843 RVA: 0x0000EE13 File Offset: 0x0000D013
			public override TeleporterInteraction.ActivationState backwardsCompatibleActivationState
			{
				get
				{
					return TeleporterInteraction.ActivationState.Charging;
				}
			}
		}

		// Token: 0x020008BF RID: 2239
		private class ChargedState : TeleporterInteraction.BaseTeleporterState
		{
			// Token: 0x0600322D RID: 12845 RVA: 0x000D4184 File Offset: 0x000D2384
			public override void OnEnter()
			{
				base.OnEnter();
				base.teleporterInteraction.teleporterPositionIndicator.GetComponent<ChargeIndicatorController>().isCharged = true;
				base.SetChildActive("ChargedEffect", true);
				if (NetworkServer.active)
				{
					base.teleporterInteraction.AttemptToSpawnAllEligiblePortals();
				}
				Action<TeleporterInteraction> onTeleporterChargedGlobal = TeleporterInteraction.onTeleporterChargedGlobal;
				if (onTeleporterChargedGlobal == null)
				{
					return;
				}
				onTeleporterChargedGlobal(base.teleporterInteraction);
			}

			// Token: 0x0600322E RID: 12846 RVA: 0x000D41E0 File Offset: 0x000D23E0
			public override void OnExit()
			{
				base.SetChildActive("ChargedEffect", false);
				base.OnExit();
			}

			// Token: 0x0600322F RID: 12847 RVA: 0x0000F997 File Offset: 0x0000DB97
			public override void FixedUpdate()
			{
				base.FixedUpdate();
			}

			// Token: 0x06003230 RID: 12848 RVA: 0x000D41F4 File Offset: 0x000D23F4
			public override Interactability GetInteractability(Interactor activator)
			{
				if (!base.teleporterInteraction.monstersCleared)
				{
					return Interactability.ConditionsNotMet;
				}
				return Interactability.Available;
			}

			// Token: 0x06003231 RID: 12849 RVA: 0x000D4206 File Offset: 0x000D2406
			public override string GetContextString(Interactor activator)
			{
				return Language.GetString(base.teleporterInteraction.exitContextString);
			}

			// Token: 0x06003232 RID: 12850 RVA: 0x000D4218 File Offset: 0x000D2418
			public override void OnInteractionBegin(Interactor activator)
			{
				this.outer.SetNextState(new TeleporterInteraction.FinishedState());
			}

			// Token: 0x170004A3 RID: 1187
			// (get) Token: 0x06003233 RID: 12851 RVA: 0x00014F2E File Offset: 0x0001312E
			public override TeleporterInteraction.ActivationState backwardsCompatibleActivationState
			{
				get
				{
					return TeleporterInteraction.ActivationState.Charged;
				}
			}
		}

		// Token: 0x020008C0 RID: 2240
		private class FinishedState : TeleporterInteraction.BaseTeleporterState
		{
			// Token: 0x06003235 RID: 12853 RVA: 0x000D422A File Offset: 0x000D242A
			public override void OnEnter()
			{
				base.OnEnter();
				if (NetworkServer.active)
				{
					base.teleporterInteraction.sceneExitController.Begin();
				}
				Action<TeleporterInteraction> onTeleporterFinishGlobal = TeleporterInteraction.onTeleporterFinishGlobal;
				if (onTeleporterFinishGlobal == null)
				{
					return;
				}
				onTeleporterFinishGlobal(base.teleporterInteraction);
			}

			// Token: 0x170004A4 RID: 1188
			// (get) Token: 0x06003236 RID: 12854 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
			public override TeleporterInteraction.ActivationState backwardsCompatibleActivationState
			{
				get
				{
					return TeleporterInteraction.ActivationState.Finished;
				}
			}
		}
	}
}
