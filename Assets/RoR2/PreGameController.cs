using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using RoR2.ConVar;
using RoR2.EntitlementManagement;
using RoR2.ExpansionManagement;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000821 RID: 2081
	[RequireComponent(typeof(NetworkRuleChoiceMask))]
	[RequireComponent(typeof(NetworkRuleBook))]
	public class PreGameController : NetworkBehaviour
	{
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06002D0E RID: 11534 RVA: 0x000C027F File Offset: 0x000BE47F
		// (set) Token: 0x06002D0F RID: 11535 RVA: 0x000C0286 File Offset: 0x000BE486
		public static PreGameController instance { get; private set; }

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06002D10 RID: 11536 RVA: 0x000C028E File Offset: 0x000BE48E
		public RuleChoiceMask resolvedRuleChoiceMask
		{
			get
			{
				return this.networkRuleChoiceMaskComponent.ruleChoiceMask;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06002D11 RID: 11537 RVA: 0x000C029B File Offset: 0x000BE49B
		public RuleBook readOnlyRuleBook
		{
			get
			{
				return this.networkRuleBookComponent.ruleBook;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06002D12 RID: 11538 RVA: 0x000C02A8 File Offset: 0x000BE4A8
		// (set) Token: 0x06002D13 RID: 11539 RVA: 0x000C02B0 File Offset: 0x000BE4B0
		public GameModeIndex gameModeIndex
		{
			get
			{
				return (GameModeIndex)this._gameModeIndex;
			}
			set
			{
				this.Network_gameModeIndex = (int)value;
			}
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000C02BC File Offset: 0x000BE4BC
		private void Awake()
		{
			this.lobbyBackgroundTimeToRefresh = 0f;
			this.networkRuleChoiceMaskComponent = base.GetComponent<NetworkRuleChoiceMask>();
			this.networkRuleBookComponent = base.GetComponent<NetworkRuleBook>();
			this.networkRuleBookComponent.onRuleBookUpdated += this.OnRuleBookUpdated;
			this.ruleBookBuffer = new RuleBook();
			this.serverAvailableChoiceMask = new RuleChoiceMask();
			this.unlockedChoiceMask = new RuleChoiceMask();
			this.dependencyChoiceMask = new RuleChoiceMask();
			this.entitlementChoiceMask = new RuleChoiceMask();
			this.choiceMaskBuffer = new RuleChoiceMask();
			this.requiredExpansionEnabledChoiceMask = new RuleChoiceMask();
			if (NetworkServer.active)
			{
				this.gameModeIndex = GameModeCatalog.FindGameModeIndex(PreGameController.GameModeConVar.instance.GetString());
				this.runSeed = GameModeCatalog.GetGameModePrefabComponent(this.gameModeIndex).GenerateSeedForNewRun();
			}
			bool isInSinglePlayer = RoR2Application.isInSinglePlayer;
			for (int i = 0; i < this.serverAvailableChoiceMask.length; i++)
			{
				RuleChoiceDef choiceDef = RuleCatalog.GetChoiceDef(i);
				this.serverAvailableChoiceMask[i] = (isInSinglePlayer ? choiceDef.availableInSinglePlayer : choiceDef.availableInMultiPlayer);
			}
			NetworkUser.OnPostNetworkUserStart += this.GenerateRuleVoteController;
			this.RefreshLobbyBackground();
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000C03D9 File Offset: 0x000BE5D9
		private void OnDestroy()
		{
			NetworkUser.OnPostNetworkUserStart -= this.GenerateRuleVoteController;
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000C03EC File Offset: 0x000BE5EC
		private void GenerateRuleVoteController(NetworkUser networkUser)
		{
			if (NetworkServer.active)
			{
				if (PreGameRuleVoteController.FindForUser(networkUser))
				{
					return;
				}
				if (!networkUser.isLocalPlayer && !PreGameController.cvSvAllowRuleVoting.value)
				{
					return;
				}
				PreGameRuleVoteController.CreateForNetworkUserServer(networkUser);
			}
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000C0420 File Offset: 0x000BE620
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.ResolveChoiceMask();
				Console.instance.SubmitCmd(null, "exec server_pregame", false);
				foreach (NetworkUser networkUser in NetworkUser.readOnlyInstancesList)
				{
					Debug.LogFormat("Attempting to generate PreGameVoteController for {0}", new object[]
					{
						networkUser.userName
					});
					this.GenerateRuleVoteController(networkUser);
				}
			}
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000C04A4 File Offset: 0x000BE6A4
		[Server]
		public bool ApplyChoice(int ruleChoiceIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Boolean RoR2.PreGameController::ApplyChoice(System.Int32)' called on client");
				return false;
			}
			if (!this.resolvedRuleChoiceMask[ruleChoiceIndex])
			{
				return false;
			}
			RuleChoiceDef choiceDef = RuleCatalog.GetChoiceDef(ruleChoiceIndex);
			if (this.readOnlyRuleBook.GetRuleChoice(choiceDef.ruleDef.globalIndex) == choiceDef)
			{
				return false;
			}
			this.ruleBookBuffer.Copy(this.readOnlyRuleBook);
			this.ruleBookBuffer.ApplyChoice(choiceDef);
			this.SetRuleBook(this.ruleBookBuffer);
			return true;
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x000C0523 File Offset: 0x000BE723
		private void SetRuleBook(RuleBook newRuleBook)
		{
			this.networkRuleBookComponent.SetRuleBook(newRuleBook);
			Action<PreGameController, RuleBook> action = PreGameController.onPreGameControllerSetRuleBookServerGlobal;
			if (action == null)
			{
				return;
			}
			action(this, this.readOnlyRuleBook);
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000C0547 File Offset: 0x000BE747
		private void OnRuleBookUpdated(NetworkRuleBook networkRuleBookComponent)
		{
			Action<PreGameController, RuleBook> action = PreGameController.onPreGameControllerSetRuleBookGlobal;
			if (action == null)
			{
				return;
			}
			action(this, networkRuleBookComponent.ruleBook);
		}

		// Token: 0x14000085 RID: 133
		// (add) Token: 0x06002D1B RID: 11547 RVA: 0x000C0560 File Offset: 0x000BE760
		// (remove) Token: 0x06002D1C RID: 11548 RVA: 0x000C0594 File Offset: 0x000BE794
		public static event Action<PreGameController, RuleBook> onPreGameControllerSetRuleBookServerGlobal;

		// Token: 0x14000086 RID: 134
		// (add) Token: 0x06002D1D RID: 11549 RVA: 0x000C05C8 File Offset: 0x000BE7C8
		// (remove) Token: 0x06002D1E RID: 11550 RVA: 0x000C05FC File Offset: 0x000BE7FC
		public static event Action<PreGameController, RuleBook> onPreGameControllerSetRuleBookGlobal;

		// Token: 0x06002D1F RID: 11551 RVA: 0x000C0630 File Offset: 0x000BE830
		[Server]
		public void EnforceValidRuleChoices()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PreGameController::EnforceValidRuleChoices()' called on client");
				return;
			}
			this.ruleBookBuffer.Copy(this.readOnlyRuleBook);
			for (int i = 0; i < RuleCatalog.ruleCount; i++)
			{
				RuleChoiceDef ruleChoice = this.ruleBookBuffer.GetRuleChoice(i);
				if (!this.resolvedRuleChoiceMask[ruleChoice])
				{
					RuleDef ruleDef = RuleCatalog.GetRuleDef(i);
					RuleChoiceDef choiceDef = ruleDef.choices[ruleDef.defaultChoiceIndex];
					int num = 0;
					int j = 0;
					int count = ruleDef.choices.Count;
					while (j < count)
					{
						if (this.resolvedRuleChoiceMask[ruleDef.choices[j]])
						{
							num++;
						}
						j++;
					}
					if (this.resolvedRuleChoiceMask[choiceDef] || num == 0)
					{
						this.ruleBookBuffer.ApplyChoice(choiceDef);
					}
					else
					{
						int k = 0;
						int count2 = ruleDef.choices.Count;
						while (k < count2)
						{
							if (this.resolvedRuleChoiceMask[ruleDef.choices[k]])
							{
								this.ruleBookBuffer.ApplyChoice(ruleDef.choices[k]);
								break;
							}
							k++;
						}
					}
				}
			}
			this.SetRuleBook(this.ruleBookBuffer);
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000C0770 File Offset: 0x000BE970
		private void TestRuleValues()
		{
			RuleBook ruleBook = new RuleBook();
			ruleBook.Copy(this.networkRuleBookComponent.ruleBook);
			RuleDef ruleDef = RuleCatalog.GetRuleDef(UnityEngine.Random.Range(0, RuleCatalog.ruleCount));
			RuleChoiceDef choiceDef = ruleDef.choices[UnityEngine.Random.Range(0, ruleDef.choices.Count)];
			ruleBook.ApplyChoice(choiceDef);
			this.SetRuleBook(ruleBook);
			base.Invoke("TestRuleValues", 0.5f);
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000C07E0 File Offset: 0x000BE9E0
		private void OnEnable()
		{
			PreGameController.instance = SingletonHelper.Assign<PreGameController>(PreGameController.instance, this);
			if (NetworkServer.active)
			{
				this.RecalculateModifierAvailability();
			}
			NetworkUser.OnNetworkUserUnlockablesUpdated += this.OnNetworkUserUnlockablesUpdatedCallback;
			NetworkUser.OnPostNetworkUserStart += this.OnPostNetworkUserStartCallback;
			NetworkUser.onNetworkUserBodyPreferenceChanged += this.OnNetworkUserBodyPreferenceChanged;
			NetworkUser.onNetworkUserLost += this.OnNetworkUserLost;
			if (NetworkClient.active)
			{
				foreach (NetworkUser networkUser in NetworkUser.readOnlyLocalPlayersList)
				{
					networkUser.SendServerUnlockables();
				}
			}
			if (NetworkServer.active)
			{
				foreach (NetworkUser networkUser2 in NetworkUser.readOnlyInstancesList)
				{
					networkUser2.ServerRequestUnlockables();
				}
			}
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000C08D0 File Offset: 0x000BEAD0
		private void OnDisable()
		{
			PreGameController.instance = SingletonHelper.Unassign<PreGameController>(PreGameController.instance, this);
			NetworkUser.OnPostNetworkUserStart -= this.OnPostNetworkUserStartCallback;
			NetworkUser.OnNetworkUserUnlockablesUpdated -= this.OnNetworkUserUnlockablesUpdatedCallback;
			NetworkUser.onNetworkUserBodyPreferenceChanged -= this.OnNetworkUserBodyPreferenceChanged;
			NetworkUser.onNetworkUserLost -= this.OnNetworkUserLost;
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06002D23 RID: 11555 RVA: 0x000C0931 File Offset: 0x000BEB31
		// (set) Token: 0x06002D24 RID: 11556 RVA: 0x000C0939 File Offset: 0x000BEB39
		private PreGameController.PregameState pregameState
		{
			get
			{
				return (PreGameController.PregameState)this.pregameStateInternal;
			}
			set
			{
				this.NetworkpregameStateInternal = (int)value;
			}
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x000C0942 File Offset: 0x000BEB42
		public bool IsCharacterSwitchingCurrentlyAllowed()
		{
			return this.pregameState == PreGameController.PregameState.Idle;
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x000C0950 File Offset: 0x000BEB50
		private void Update()
		{
			if (this.pregameState == PreGameController.PregameState.Launching)
			{
				if (PlatformSystems.networkManager.unpredictedServerFixedTime - this.launchStartTime >= 0.5f && NetworkServer.active)
				{
					this.StartRun();
				}
			}
			else
			{
				PreGameController.PregameState pregameState = this.pregameState;
			}
			if (NetworkServer.active)
			{
				this.lobbyBackgroundTimeToRefresh -= Time.deltaTime;
				if (this.lobbyBackgroundTimeToRefresh <= 0f)
				{
					this.lobbyBackgroundTimeToRefresh = 4f;
					this.CallRpcUpdateGameModeIndex(this._gameModeIndex);
				}
			}
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000C09D3 File Offset: 0x000BEBD3
		[Server]
		public void StartLaunch()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PreGameController::StartLaunch()' called on client");
				return;
			}
			if (this.pregameState == PreGameController.PregameState.Idle)
			{
				this.pregameState = PreGameController.PregameState.Launching;
				this.NetworklaunchStartTime = PlatformSystems.networkManager.unpredictedServerFixedTime;
			}
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x000C0A0C File Offset: 0x000BEC0C
		[Server]
		private void StartRun()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PreGameController::StartRun()' called on client");
				return;
			}
			ExpansionRequirementComponent component = PreGameController.GameModeConVar.instance.runPrefabComponent.GetComponent<ExpansionRequirementComponent>();
			if (!component || !component.requiredExpansion || EntitlementManager.networkUserEntitlementTracker.AnyUserHasEntitlement(component.requiredExpansion.requiredEntitlement))
			{
				this.pregameState = PreGameController.PregameState.Launched;
				NetworkSession.instance.BeginRun(PreGameController.GameModeConVar.instance.runPrefabComponent, this.readOnlyRuleBook, this.runSeed);
			}
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x000C0A92 File Offset: 0x000BEC92
		[ConCommand(commandName = "pregame_start_run", flags = ConVarFlags.SenderMustBeServer, helpText = "Begins a run out of pregame.")]
		private static void CCPregameStartRun(ConCommandArgs args)
		{
			if (PreGameController.instance)
			{
				PreGameController.instance.StartRun();
			}
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x000C0AAC File Offset: 0x000BECAC
		private static bool AnyUserHasUnlockable([NotNull] UnlockableDef unlockableDef)
		{
			ReadOnlyCollection<NetworkUser> readOnlyInstancesList = NetworkUser.readOnlyInstancesList;
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				if (readOnlyInstancesList[i].unlockables.Contains(unlockableDef))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x000C0AE8 File Offset: 0x000BECE8
		[ContextMenu("RecalculateModifierAvailability")]
		[Server]
		public void RecalculateModifierAvailability()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PreGameController::RecalculateModifierAvailability()' called on client");
				return;
			}
			for (int i = 0; i < RuleCatalog.choiceCount; i++)
			{
				RuleChoiceDef choiceDef = RuleCatalog.GetChoiceDef(i);
				this.unlockedChoiceMask[i] = (!choiceDef.requiredUnlockable || PreGameController.AnyUserHasUnlockable(choiceDef.requiredUnlockable));
				this.dependencyChoiceMask[i] = (choiceDef.requiredChoiceDef == null || this.readOnlyRuleBook.IsChoiceActive(choiceDef.requiredChoiceDef));
				this.entitlementChoiceMask[i] = (!choiceDef.requiredEntitlementDef || EntitlementManager.networkUserEntitlementTracker.AnyUserHasEntitlement(choiceDef.requiredEntitlementDef) || EntitlementManager.localUserEntitlementTracker.AnyUserHasEntitlement(choiceDef.requiredEntitlementDef));
				this.requiredExpansionEnabledChoiceMask[i] = (!choiceDef.requiredExpansionDef || this.readOnlyRuleBook.IsChoiceActive(choiceDef.requiredExpansionDef.enabledChoice));
			}
			this.ResolveChoiceMask();
			Action<PreGameController> action = PreGameController.onServerRecalculatedModifierAvailability;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x000C0BFC File Offset: 0x000BEDFC
		[Server]
		private void ResolveChoiceMask()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PreGameController::ResolveChoiceMask()' called on client");
				return;
			}
			RuleChoiceMask ruleChoiceMask = new RuleChoiceMask();
			RuleChoiceMask ruleChoiceMask2 = new RuleChoiceMask();
			Run gameModePrefabComponent = GameModeCatalog.GetGameModePrefabComponent(this.gameModeIndex);
			if (gameModePrefabComponent)
			{
				gameModePrefabComponent.OverrideRuleChoices(ruleChoiceMask, ruleChoiceMask2, this.runSeed);
			}
			for (int i = 0; i < this.choiceMaskBuffer.length; i++)
			{
				RuleChoiceDef choiceDef = RuleCatalog.GetChoiceDef(i);
				bool flag = ruleChoiceMask[i];
				bool flag2 = ruleChoiceMask2[i];
				bool flag3 = this.serverAvailableChoiceMask[i];
				bool flag4 = this.unlockedChoiceMask[i];
				bool flag5 = this.dependencyChoiceMask[i];
				bool flag6 = this.entitlementChoiceMask[i];
				bool flag7 = this.requiredExpansionEnabledChoiceMask[i];
				this.choiceMaskBuffer[i] = (flag || (!flag2 && flag3 && flag4 && flag5 && flag6 && flag7 && !choiceDef.excludeByDefault));
			}
			this.networkRuleChoiceMaskComponent.SetRuleChoiceMask(this.choiceMaskBuffer);
			this.EnforceValidRuleChoices();
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x000C0D15 File Offset: 0x000BEF15
		private void OnNetworkUserUnlockablesUpdatedCallback(NetworkUser networkUser)
		{
			if (NetworkServer.active)
			{
				this.RecalculateModifierAvailability();
			}
		}

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x06002D2E RID: 11566 RVA: 0x000C0D24 File Offset: 0x000BEF24
		// (remove) Token: 0x06002D2F RID: 11567 RVA: 0x000C0D58 File Offset: 0x000BEF58
		public static event Action<PreGameController> onServerRecalculatedModifierAvailability;

		// Token: 0x06002D30 RID: 11568 RVA: 0x000C0D8B File Offset: 0x000BEF8B
		private void OnPostNetworkUserStartCallback(NetworkUser networkUser)
		{
			if (NetworkServer.active)
			{
				networkUser.ServerRequestUnlockables();
			}
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x000C0D15 File Offset: 0x000BEF15
		private void OnNetworkUserBodyPreferenceChanged(NetworkUser networkUser)
		{
			if (NetworkServer.active)
			{
				this.RecalculateModifierAvailability();
			}
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x000C0D15 File Offset: 0x000BEF15
		private void OnNetworkUserLost(NetworkUser networkUser)
		{
			if (NetworkServer.active)
			{
				this.RecalculateModifierAvailability();
			}
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x000C0D9A File Offset: 0x000BEF9A
		private void UpdateGameModeIndex(int newIndex)
		{
			if (newIndex != this.currentLobbyBackgroundGameModeIndex)
			{
				this.Network_gameModeIndex = newIndex;
				this.RefreshLobbyBackground();
			}
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x000C0DB2 File Offset: 0x000BEFB2
		[ClientRpc]
		private void RpcUpdateGameModeIndex(int newIndex)
		{
			this.UpdateGameModeIndex(newIndex);
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x000C0DBC File Offset: 0x000BEFBC
		private void RefreshLobbyBackground()
		{
			if (this.lobbyBackground)
			{
				UnityEngine.Object.Destroy(this.lobbyBackground);
			}
			this.currentLobbyBackgroundGameModeIndex = this._gameModeIndex;
			this.lobbyBackground = UnityEngine.Object.Instantiate<GameObject>(GameModeCatalog.GetGameModePrefabComponent(this.gameModeIndex).lobbyBackgroundPrefab);
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000C0E08 File Offset: 0x000BF008
		[ConCommand(commandName = "pregame_set_rule_choice", flags = ConVarFlags.SenderMustBeServer, helpText = "Sets the specified choice during pregame. See the command \"rule_list_choices\" for possible options.")]
		public static void CCPregameSetRuleChoice(ConCommandArgs args)
		{
			string argString = args.GetArgString(0);
			RuleChoiceDef ruleChoiceDef = RuleCatalog.FindChoiceDef(argString);
			if (ruleChoiceDef == null)
			{
				throw new ConCommandException(string.Format("'{0}' is not a recognized rule choice name.", argString));
			}
			if (!PreGameController.instance)
			{
				throw new ConCommandException("This command cannot be issued outside the character select screen.");
			}
			if (PreGameController.instance.ApplyChoice(ruleChoiceDef.globalIndex))
			{
				PreGameController.instance.RecalculateModifierAvailability();
			}
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x000C0E80 File Offset: 0x000BF080
		static PreGameController()
		{
			NetworkBehaviour.RegisterRpcDelegate(typeof(PreGameController), PreGameController.kRpcRpcUpdateGameModeIndex, new NetworkBehaviour.CmdDelegate(PreGameController.InvokeRpcRpcUpdateGameModeIndex));
			NetworkCRC.RegisterBehaviour("PreGameController", 0);
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06002D3A RID: 11578 RVA: 0x000C0EE0 File Offset: 0x000BF0E0
		// (set) Token: 0x06002D3B RID: 11579 RVA: 0x000C0EF3 File Offset: 0x000BF0F3
		public int Network_gameModeIndex
		{
			get
			{
				return this._gameModeIndex;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.UpdateGameModeIndex(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<int>(value, ref this._gameModeIndex, 1U);
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06002D3C RID: 11580 RVA: 0x000C0F34 File Offset: 0x000BF134
		// (set) Token: 0x06002D3D RID: 11581 RVA: 0x000C0F47 File Offset: 0x000BF147
		public int NetworkpregameStateInternal
		{
			get
			{
				return this.pregameStateInternal;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this.pregameStateInternal, 2U);
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06002D3E RID: 11582 RVA: 0x000C0F5C File Offset: 0x000BF15C
		// (set) Token: 0x06002D3F RID: 11583 RVA: 0x000C0F6F File Offset: 0x000BF16F
		public float NetworklaunchStartTime
		{
			get
			{
				return this.launchStartTime;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this.launchStartTime, 4U);
			}
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x000C0F83 File Offset: 0x000BF183
		protected static void InvokeRpcRpcUpdateGameModeIndex(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcUpdateGameModeIndex called on server.");
				return;
			}
			((PreGameController)obj).RpcUpdateGameModeIndex((int)reader.ReadPackedUInt32());
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x000C0FAC File Offset: 0x000BF1AC
		public void CallRpcUpdateGameModeIndex(int newIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcUpdateGameModeIndex called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)PreGameController.kRpcRpcUpdateGameModeIndex);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.WritePackedUInt32((uint)newIndex);
			this.SendRPCInternal(networkWriter, 0, "RpcUpdateGameModeIndex");
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x000C1020 File Offset: 0x000BF220
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.WritePackedUInt32((uint)this._gameModeIndex);
				writer.WritePackedUInt32((uint)this.pregameStateInternal);
				writer.Write(this.launchStartTime);
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
				writer.WritePackedUInt32((uint)this._gameModeIndex);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32((uint)this.pregameStateInternal);
			}
			if ((base.syncVarDirtyBits & 4U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.launchStartTime);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x000C110C File Offset: 0x000BF30C
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this._gameModeIndex = (int)reader.ReadPackedUInt32();
				this.pregameStateInternal = (int)reader.ReadPackedUInt32();
				this.launchStartTime = reader.ReadSingle();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.UpdateGameModeIndex((int)reader.ReadPackedUInt32());
			}
			if ((num & 2) != 0)
			{
				this.pregameStateInternal = (int)reader.ReadPackedUInt32();
			}
			if ((num & 4) != 0)
			{
				this.launchStartTime = reader.ReadSingle();
			}
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002F3F RID: 12095
		private NetworkRuleChoiceMask networkRuleChoiceMaskComponent;

		// Token: 0x04002F40 RID: 12096
		private NetworkRuleBook networkRuleBookComponent;

		// Token: 0x04002F41 RID: 12097
		private RuleChoiceMask serverAvailableChoiceMask;

		// Token: 0x04002F42 RID: 12098
		public ulong runSeed;

		// Token: 0x04002F43 RID: 12099
		[SyncVar(hook = "UpdateGameModeIndex")]
		private int _gameModeIndex;

		// Token: 0x04002F44 RID: 12100
		private GameObject lobbyBackground;

		// Token: 0x04002F45 RID: 12101
		private int currentLobbyBackgroundGameModeIndex;

		// Token: 0x04002F46 RID: 12102
		private float lobbyBackgroundTimeToRefresh;

		// Token: 0x04002F47 RID: 12103
		private const float lobbyBackgroundTimeToRefreshInterval = 4f;

		// Token: 0x04002F48 RID: 12104
		private RuleBook ruleBookBuffer;

		// Token: 0x04002F4B RID: 12107
		[SyncVar]
		private int pregameStateInternal;

		// Token: 0x04002F4C RID: 12108
		private const float launchTransitionDuration = 0f;

		// Token: 0x04002F4D RID: 12109
		private GameObject gameModePrefab;

		// Token: 0x04002F4E RID: 12110
		[SyncVar]
		private float launchStartTime = float.PositiveInfinity;

		// Token: 0x04002F4F RID: 12111
		private RuleChoiceMask unlockedChoiceMask;

		// Token: 0x04002F50 RID: 12112
		private RuleChoiceMask dependencyChoiceMask;

		// Token: 0x04002F51 RID: 12113
		private RuleChoiceMask entitlementChoiceMask;

		// Token: 0x04002F52 RID: 12114
		private RuleChoiceMask requiredExpansionEnabledChoiceMask;

		// Token: 0x04002F53 RID: 12115
		private RuleChoiceMask choiceMaskBuffer;

		// Token: 0x04002F55 RID: 12117
		public static readonly BoolConVar cvSvAllowRuleVoting = new BoolConVar("sv_allow_rule_voting", ConVarFlags.None, "1", "Whether or not players are allowed to vote on rules.");

		// Token: 0x04002F56 RID: 12118
		private static int kRpcRpcUpdateGameModeIndex = -600953683;

		// Token: 0x02000822 RID: 2082
		private enum PregameState
		{
			// Token: 0x04002F58 RID: 12120
			Idle,
			// Token: 0x04002F59 RID: 12121
			Launching,
			// Token: 0x04002F5A RID: 12122
			Launched
		}

		// Token: 0x02000823 RID: 2083
		public class GameModeConVar : BaseConVar
		{
			// Token: 0x06002D45 RID: 11589 RVA: 0x00009F73 File Offset: 0x00008173
			public GameModeConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06002D46 RID: 11590 RVA: 0x000C1197 File Offset: 0x000BF397
			static GameModeConVar()
			{
				GameModeCatalog.availability.CallWhenAvailable(delegate
				{
					PreGameController.GameModeConVar.instance.runPrefabComponent = GameModeCatalog.FindGameModePrefabComponent(PreGameController.GameModeConVar.instance.GetString());
				});
			}

			// Token: 0x06002D47 RID: 11591 RVA: 0x000C11D0 File Offset: 0x000BF3D0
			public override void SetString(string newValue)
			{
				GameModeCatalog.availability.CallWhenAvailable(delegate
				{
					Run exists = GameModeCatalog.FindGameModePrefabComponent(newValue);
					if (!exists)
					{
						Debug.LogFormat("GameMode \"{0}\" does not exist.", new object[]
						{
							newValue
						});
						return;
					}
					this.runPrefabComponent = exists;
				});
			}

			// Token: 0x06002D48 RID: 11592 RVA: 0x000C1207 File Offset: 0x000BF407
			public override string GetString()
			{
				if (!this.runPrefabComponent)
				{
					return "ClassicRun";
				}
				return this.runPrefabComponent.gameObject.name;
			}

			// Token: 0x04002F5B RID: 12123
			public static readonly PreGameController.GameModeConVar instance = new PreGameController.GameModeConVar("gamemode", ConVarFlags.None, "ClassicRun", "Sets the specified game mode as the one to use in the next run.");

			// Token: 0x04002F5C RID: 12124
			public Run runPrefabComponent;
		}
	}
}
