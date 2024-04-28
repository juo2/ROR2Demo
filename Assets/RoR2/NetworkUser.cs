using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Rewired;
using RoR2.Networking;
using RoR2.Stats;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007D7 RID: 2007
	[RequireComponent(typeof(NetworkLoadout))]
	public class NetworkUser : NetworkBehaviour
	{
		// Token: 0x06002AE7 RID: 10983 RVA: 0x000B867C File Offset: 0x000B687C
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init()
		{
			UserProfile.onUnlockableGranted += delegate(UserProfile userProfile, UnlockableDef unlockableDef)
			{
				if (NetworkClient.active)
				{
					NetworkUser networkUser = NetworkUser.FindNetworkUserByUserProfile(userProfile);
					if (networkUser)
					{
						networkUser.SendServerUnlockables();
					}
				}
			};
			UserProfile.onLoadoutChangedGlobal += delegate(UserProfile userProfile)
			{
				if (NetworkClient.active)
				{
					NetworkUser networkUser = NetworkUser.FindNetworkUserByUserProfile(userProfile);
					if (networkUser)
					{
						networkUser.PullLoadoutFromUserProfile();
					}
				}
			};
			UserProfile.onSurvivorPreferenceChangedGlobal += delegate(UserProfile userProfile)
			{
				if (NetworkClient.active)
				{
					NetworkUser networkUser = NetworkUser.FindNetworkUserByUserProfile(userProfile);
					if (networkUser)
					{
						networkUser.SetSurvivorPreferenceClient(userProfile.GetSurvivorPreference());
					}
				}
			};
			Stage.onStageStartGlobal += delegate(Stage stage)
			{
				if (NetworkServer.active)
				{
					NetworkUser.serverCurrentStage = stage.netId;
				}
				foreach (NetworkUser networkUser in NetworkUser.localPlayers)
				{
					networkUser.CallCmdAcknowledgeStage(stage.netId);
				}
			};
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000B8719 File Offset: 0x000B6919
		private void OnEnable()
		{
			NetworkUser.instancesList.Add(this);
			NetworkUser.NetworkUserGenericDelegate networkUserGenericDelegate = NetworkUser.onNetworkUserDiscovered;
			if (networkUserGenericDelegate == null)
			{
				return;
			}
			networkUserGenericDelegate(this);
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x000B8736 File Offset: 0x000B6936
		private void OnDisable()
		{
			NetworkUser.NetworkUserGenericDelegate networkUserGenericDelegate = NetworkUser.onNetworkUserLost;
			if (networkUserGenericDelegate != null)
			{
				networkUserGenericDelegate(this);
			}
			NetworkUser.instancesList.Remove(this);
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06002AEA RID: 10986 RVA: 0x000B8755 File Offset: 0x000B6955
		// (set) Token: 0x06002AEB RID: 10987 RVA: 0x000B875D File Offset: 0x000B695D
		public NetworkLoadout networkLoadout { get; private set; }

		// Token: 0x06002AEC RID: 10988 RVA: 0x000B8766 File Offset: 0x000B6966
		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			this.networkLoadout = base.GetComponent<NetworkLoadout>();
			this.networkLoadout.onLoadoutUpdated += this.OnLoadoutUpdated;
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x000B8796 File Offset: 0x000B6996
		private void OnLoadoutUpdated()
		{
			Action<NetworkUser> action = NetworkUser.onLoadoutChangedGlobal;
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x000B87A8 File Offset: 0x000B69A8
		private void PullLoadoutFromUserProfile()
		{
			LocalUser localUser = this.localUser;
			UserProfile userProfile = (localUser != null) ? localUser.userProfile : null;
			if (userProfile == null)
			{
				return;
			}
			Loadout loadout = Loadout.RequestInstance();
			userProfile.CopyLoadout(loadout);
			this.networkLoadout.SetLoadout(loadout);
			Loadout.ReturnInstance(loadout);
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x000B87EC File Offset: 0x000B69EC
		private void Start()
		{
			if (base.isLocalPlayer)
			{
				LocalUser localUser = LocalUserManager.FindLocalUser((int)base.playerControllerId);
				if (localUser != null)
				{
					localUser.LinkNetworkUser(this);
				}
				this.PullLoadoutFromUserProfile();
				if (!Run.instance)
				{
					this.SetSurvivorPreferenceClient(this.localUser.userProfile.GetSurvivorPreference());
				}
			}
			if (Run.instance)
			{
				Run.instance.OnUserAdded(this);
			}
			if (NetworkClient.active)
			{
				this.SyncLunarCoinsToServer();
				this.SendServerUnlockables();
			}
			this.OnLoadoutUpdated();
			NetworkUser.NetworkUserGenericDelegate networkUserGenericDelegate = NetworkUser.onPostNetworkUserStart;
			if (networkUserGenericDelegate != null)
			{
				networkUserGenericDelegate(this);
			}
			if (base.isLocalPlayer && Stage.instance)
			{
				this.CallCmdAcknowledgeStage(Stage.instance.netId);
			}
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x000B88A5 File Offset: 0x000B6AA5
		private void OnDestroy()
		{
			NetworkUser.localPlayers.Remove(this);
			Run instance = Run.instance;
			if (instance != null)
			{
				instance.OnUserRemoved(this);
			}
			LocalUser localUser = this.localUser;
			if (localUser == null)
			{
				return;
			}
			localUser.UnlinkNetworkUser();
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x000B88D4 File Offset: 0x000B6AD4
		public override void OnStartLocalPlayer()
		{
			base.OnStartLocalPlayer();
			NetworkUser.localPlayers.Add(this);
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x000B88E7 File Offset: 0x000B6AE7
		public override void OnStartClient()
		{
			this.UpdateUserName();
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x000B88EF File Offset: 0x000B6AEF
		// (set) Token: 0x06002AF4 RID: 10996 RVA: 0x000B88F7 File Offset: 0x000B6AF7
		public NetworkUserId id
		{
			get
			{
				return this._id;
			}
			set
			{
				if (this._id.Equals(value))
				{
					return;
				}
				this.Network_id = value;
				this.UpdateUserName();
			}
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x000B8915 File Offset: 0x000B6B15
		private void OnSyncId(NetworkUserId newId)
		{
			this.id = newId;
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x000B8920 File Offset: 0x000B6B20
		public bool authed
		{
			get
			{
				return this.id.HasValidValue();
			}
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x000B893B File Offset: 0x000B6B3B
		private void OnSyncMasterObjectId(NetworkInstanceId newValue)
		{
			this._masterObject = null;
			this.Network_masterObjectId = newValue;
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x000B894B File Offset: 0x000B6B4B
		public Player inputPlayer
		{
			get
			{
				LocalUser localUser = this.localUser;
				if (localUser == null)
				{
					return null;
				}
				return localUser.inputPlayer;
			}
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x000B8960 File Offset: 0x000B6B60
		public NetworkPlayerName GetNetworkPlayerName()
		{
			return new NetworkPlayerName
			{
				nameOverride = ((this.id.strValue != null) ? this.id.strValue : null),
				steamId = ((!string.IsNullOrEmpty(this.id.strValue)) ? default(CSteamID) : new CSteamID(this.id.value))
			};
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x000B89CC File Offset: 0x000B6BCC
		public uint lunarCoins
		{
			get
			{
				if (this.localUser != null)
				{
					return this.localUser.userProfile.coins;
				}
				return this.netLunarCoins;
			}
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x000B89ED File Offset: 0x000B6BED
		[Server]
		public void DeductLunarCoins(uint count)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.NetworkUser::DeductLunarCoins(System.UInt32)' called on client");
				return;
			}
			this.NetworknetLunarCoins = HGMath.UintSafeSubtract(this.netLunarCoins, count);
			this.CallRpcDeductLunarCoins(count);
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x000B8A1D File Offset: 0x000B6C1D
		[ClientRpc]
		private void RpcDeductLunarCoins(uint count)
		{
			if (this.localUser == null)
			{
				return;
			}
			this.localUser.userProfile.coins = HGMath.UintSafeSubtract(this.localUser.userProfile.coins, count);
			this.SyncLunarCoinsToServer();
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000B8A54 File Offset: 0x000B6C54
		[Server]
		public void AwardLunarCoins(uint count)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.NetworkUser::AwardLunarCoins(System.UInt32)' called on client");
				return;
			}
			this.NetworknetLunarCoins = HGMath.UintSafeAdd(this.netLunarCoins, count);
			this.CallRpcAwardLunarCoins(count);
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x000B8A84 File Offset: 0x000B6C84
		[ClientRpc]
		private void RpcAwardLunarCoins(uint count)
		{
			if (this.localUser == null)
			{
				return;
			}
			this.localUser.userProfile.coins = HGMath.UintSafeAdd(this.localUser.userProfile.coins, count);
			this.localUser.userProfile.totalCollectedCoins = HGMath.UintSafeAdd(this.localUser.userProfile.totalCollectedCoins, count);
			this.SyncLunarCoinsToServer();
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000B8AEC File Offset: 0x000B6CEC
		[Client]
		private void SyncLunarCoinsToServer()
		{
			if (!NetworkClient.active)
			{
				Debug.LogWarning("[Client] function 'System.Void RoR2.NetworkUser::SyncLunarCoinsToServer()' called on server");
				return;
			}
			if (this.localUser == null)
			{
				return;
			}
			this.CallCmdSetNetLunarCoins(this.localUser.userProfile.coins);
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000B8B22 File Offset: 0x000B6D22
		[Command]
		private void CmdSetNetLunarCoins(uint newNetLunarCoins)
		{
			this.NetworknetLunarCoins = newNetLunarCoins;
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06002B01 RID: 11009 RVA: 0x000B8B2B File Offset: 0x000B6D2B
		public CharacterMaster master
		{
			get
			{
				return this.cachedMaster.Get(this.masterObject);
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x000B8B3E File Offset: 0x000B6D3E
		public PlayerCharacterMasterController masterController
		{
			get
			{
				return this.cachedPlayerCharacterMasterController.Get(this.masterObject);
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06002B03 RID: 11011 RVA: 0x000B8B51 File Offset: 0x000B6D51
		public PlayerStatsComponent masterPlayerStatsComponent
		{
			get
			{
				return this.cachedPlayerStatsComponent.Get(this.masterObject);
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x000B8B64 File Offset: 0x000B6D64
		// (set) Token: 0x06002B05 RID: 11013 RVA: 0x000B8B8A File Offset: 0x000B6D8A
		public GameObject masterObject
		{
			get
			{
				if (!this._masterObject)
				{
					this._masterObject = Util.FindNetworkObject(this._masterObjectId);
				}
				return this._masterObject;
			}
			set
			{
				if (value)
				{
					this.Network_masterObjectId = value.GetComponent<NetworkIdentity>().netId;
					this._masterObject = value;
					return;
				}
				this.Network_masterObjectId = NetworkInstanceId.Invalid;
				this._masterObject = null;
			}
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x000B8BC0 File Offset: 0x000B6DC0
		public CharacterBody GetCurrentBody()
		{
			CharacterMaster master = this.master;
			if (master)
			{
				return master.GetBody();
			}
			return null;
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06002B07 RID: 11015 RVA: 0x000B8BE4 File Offset: 0x000B6DE4
		public bool isParticipating
		{
			get
			{
				return this.masterObject;
			}
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000B8BF1 File Offset: 0x000B6DF1
		[Server]
		public void CopyLoadoutFromMaster()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.NetworkUser::CopyLoadoutFromMaster()' called on client");
				return;
			}
			this.networkLoadout.SetLoadout(this.master.loadout);
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000B8C20 File Offset: 0x000B6E20
		[Server]
		public void CopyLoadoutToMaster()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.NetworkUser::CopyLoadoutToMaster()' called on client");
				return;
			}
			Loadout loadout = Loadout.RequestInstance();
			this.networkLoadout.CopyLoadout(loadout);
			this.master.SetLoadoutServer(loadout);
			Loadout.ReturnInstance(loadout);
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x000B8C68 File Offset: 0x000B6E68
		private void SetBodyPreference(BodyIndex newBodyIndexPreference)
		{
			Debug.Log(string.Format("Changinging body preference for {0} ({1}) from {2} to {3}", new object[]
			{
				this.GetNetworkPlayerName().GetResolvedName(),
				this.id,
				this.bodyIndexPreference,
				newBodyIndexPreference
			}));
			this.NetworkbodyIndexPreference = newBodyIndexPreference;
			NetworkUser.NetworkUserGenericDelegate networkUserGenericDelegate = NetworkUser.onNetworkUserBodyPreferenceChanged;
			if (networkUserGenericDelegate == null)
			{
				return;
			}
			networkUserGenericDelegate(this);
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x000B8CD7 File Offset: 0x000B6ED7
		[Command]
		public void CmdSetBodyPreference(BodyIndex newBodyIndexPreference)
		{
			this.SetBodyPreference(newBodyIndexPreference);
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x000B8CE0 File Offset: 0x000B6EE0
		[Client]
		public void SetSurvivorPreferenceClient(SurvivorDef survivorDef)
		{
			if (!NetworkClient.active)
			{
				Debug.LogWarning("[Client] function 'System.Void RoR2.NetworkUser::SetSurvivorPreferenceClient(RoR2.SurvivorDef)' called on server");
				return;
			}
			if (!survivorDef)
			{
				throw new ArgumentException("Provided object is null or invalid", "survivorDef");
			}
			BodyIndex bodyIndexFromSurvivorIndex = SurvivorCatalog.GetBodyIndexFromSurvivorIndex(survivorDef ? survivorDef.survivorIndex : SurvivorIndex.None);
			Debug.Log(string.Format("SetSurvivorPreferenceClient survivorIndex={0}, bodyIndex={1}", survivorDef.survivorIndex, bodyIndexFromSurvivorIndex));
			this.CallCmdSetBodyPreference(bodyIndexFromSurvivorIndex);
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000B8D58 File Offset: 0x000B6F58
		public SurvivorDef GetSurvivorPreference()
		{
			return SurvivorCatalog.GetSurvivorDef(SurvivorCatalog.GetSurvivorIndexFromBodyIndex(this.bodyIndexPreference));
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06002B0E RID: 11022 RVA: 0x000B8D6A File Offset: 0x000B6F6A
		public bool isSplitScreenExtraPlayer
		{
			get
			{
				return this.id.subId > 0;
			}
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000B8D7C File Offset: 0x000B6F7C
		private void Update()
		{
			if (this.localUser != null)
			{
				if (Time.timeScale != 0f)
				{
					this.secondAccumulator += Time.unscaledDeltaTime;
				}
				if (this.secondAccumulator >= 1f)
				{
					this.secondAccumulator -= 1f;
					if (Run.instance)
					{
						this.localUser.userProfile.totalRunSeconds += 1U;
						if (this.masterObject)
						{
							CharacterMaster component = this.masterObject.GetComponent<CharacterMaster>();
							if (component && component.hasBody)
							{
								this.localUser.userProfile.totalAliveSeconds += 1U;
							}
						}
					}
				}
			}
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000B8E38 File Offset: 0x000B7038
		public void UpdateUserName()
		{
			this.userName = this.GetNetworkPlayerName().GetResolvedName();
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000B8E59 File Offset: 0x000B7059
		[Command]
		public void CmdSendConsoleCommand(string commandName, string[] args)
		{
			Console.instance.RunClientCmd(this, commandName, args);
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000B8E68 File Offset: 0x000B7068
		[Client]
		public void SendServerUnlockables()
		{
			if (!NetworkClient.active)
			{
				Debug.LogWarning("[Client] function 'System.Void RoR2.NetworkUser::SendServerUnlockables()' called on server");
				return;
			}
			if (this.localUser != null)
			{
				int unlockableCount = this.localUser.userProfile.statSheet.GetUnlockableCount();
				UnlockableIndex[] array = new UnlockableIndex[unlockableCount];
				for (int i = 0; i < unlockableCount; i++)
				{
					array[i] = this.localUser.userProfile.statSheet.GetUnlockableIndex(i);
				}
				this.CallCmdSendNewUnlockables(array);
			}
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000B8EDC File Offset: 0x000B70DC
		[Command]
		private void CmdSendNewUnlockables(UnlockableIndex[] newUnlockableIndices)
		{
			this.unlockables.Clear();
			this.debugUnlockablesList.Clear();
			int i = 0;
			int num = newUnlockableIndices.Length;
			while (i < num)
			{
				UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef(newUnlockableIndices[i]);
				if (unlockableDef != null)
				{
					this.unlockables.Add(unlockableDef);
					this.debugUnlockablesList.Add(unlockableDef.cachedName);
				}
				i++;
			}
			NetworkUser.NetworkUserGenericDelegate networkUserGenericDelegate = NetworkUser.onNetworkUserUnlockablesUpdated;
			if (networkUserGenericDelegate == null)
			{
				return;
			}
			networkUserGenericDelegate(this);
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000B8F4E File Offset: 0x000B714E
		[Server]
		public void ServerRequestUnlockables()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.NetworkUser::ServerRequestUnlockables()' called on client");
				return;
			}
			this.CallRpcRequestUnlockables();
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000B8F6B File Offset: 0x000B716B
		[ClientRpc]
		private void RpcRequestUnlockables()
		{
			if (Util.HasEffectiveAuthority(base.gameObject))
			{
				this.SendServerUnlockables();
			}
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x000B8F80 File Offset: 0x000B7180
		[Command]
		public void CmdReportAchievement(string achievementNameToken)
		{
			Chat.SendBroadcastChat(new Chat.SubjectFormatChatMessage
			{
				baseToken = "ACHIEVEMENT_UNLOCKED_MESSAGE",
				subjectAsNetworkUser = this,
				paramTokens = new string[]
				{
					achievementNameToken
				}
			});
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000B8FBC File Offset: 0x000B71BC
		[Command]
		public void CmdReportUnlock(UnlockableIndex unlockIndex)
		{
			Debug.LogFormat("NetworkUser.CmdReportUnlock({0})", new object[]
			{
				unlockIndex
			});
			UnlockableDef unlockableDef = UnlockableCatalog.GetUnlockableDef(unlockIndex);
			if (unlockableDef != null)
			{
				this.ServerHandleUnlock(unlockableDef);
			}
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000B8FFC File Offset: 0x000B71FC
		[Server]
		public void ServerHandleUnlock([NotNull] UnlockableDef unlockableDef)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.NetworkUser::ServerHandleUnlock(RoR2.UnlockableDef)' called on client");
				return;
			}
			Debug.LogFormat("NetworkUser.ServerHandleUnlock({0})", new object[]
			{
				unlockableDef.cachedName
			});
			if (this.masterObject)
			{
				PlayerStatsComponent component = this.masterObject.GetComponent<PlayerStatsComponent>();
				if (component)
				{
					component.currentStats.AddUnlockable(unlockableDef);
					component.ForceNextTransmit();
				}
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06002B19 RID: 11033 RVA: 0x000B906A File Offset: 0x000B726A
		// (set) Token: 0x06002B1A RID: 11034 RVA: 0x000B9072 File Offset: 0x000B7272
		public bool serverIsClientLoaded { get; private set; }

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06002B1B RID: 11035 RVA: 0x000B907B File Offset: 0x000B727B
		// (set) Token: 0x06002B1C RID: 11036 RVA: 0x000B9084 File Offset: 0x000B7284
		private NetworkInstanceId serverLastStageAcknowledgedByClient
		{
			get
			{
				return this._serverLastStageAcknowledgedByClient;
			}
			set
			{
				if (this._serverLastStageAcknowledgedByClient == value)
				{
					return;
				}
				this._serverLastStageAcknowledgedByClient = value;
				if (NetworkUser.serverCurrentStage == this._serverLastStageAcknowledgedByClient)
				{
					this.serverIsClientLoaded = true;
					Action<NetworkUser> action = NetworkUser.onNetworkUserLoadedSceneServer;
					if (action == null)
					{
						return;
					}
					action(this);
				}
			}
		}

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06002B1D RID: 11037 RVA: 0x000B90D0 File Offset: 0x000B72D0
		// (remove) Token: 0x06002B1E RID: 11038 RVA: 0x000B9104 File Offset: 0x000B7304
		public static event Action<NetworkUser> onNetworkUserLoadedSceneServer;

		// Token: 0x06002B1F RID: 11039 RVA: 0x000B9137 File Offset: 0x000B7337
		[Command]
		public void CmdAcknowledgeStage(NetworkInstanceId stageNetworkId)
		{
			this.serverLastStageAcknowledgedByClient = stageNetworkId;
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x000B9140 File Offset: 0x000B7340
		[Command]
		public void CmdSubmitVote(GameObject voteControllerGameObject, int choiceIndex)
		{
			if (!voteControllerGameObject)
			{
				return;
			}
			VoteController component = voteControllerGameObject.GetComponent<VoteController>();
			if (!component)
			{
				return;
			}
			component.ReceiveUserVote(this, choiceIndex);
		}

		// Token: 0x14000078 RID: 120
		// (add) Token: 0x06002B21 RID: 11041 RVA: 0x000B9170 File Offset: 0x000B7370
		// (remove) Token: 0x06002B22 RID: 11042 RVA: 0x000B91A4 File Offset: 0x000B73A4
		public static event Action<NetworkUser> onLoadoutChangedGlobal;

		// Token: 0x14000079 RID: 121
		// (add) Token: 0x06002B23 RID: 11043 RVA: 0x000B91D7 File Offset: 0x000B73D7
		// (remove) Token: 0x06002B24 RID: 11044 RVA: 0x000B91DF File Offset: 0x000B73DF
		[Obsolete("Use onPostNetworkUserStart instead", false)]
		public static event NetworkUser.NetworkUserGenericDelegate OnPostNetworkUserStart
		{
			add
			{
				NetworkUser.onPostNetworkUserStart += value;
			}
			remove
			{
				NetworkUser.onPostNetworkUserStart -= value;
			}
		}

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x06002B25 RID: 11045 RVA: 0x000B91E8 File Offset: 0x000B73E8
		// (remove) Token: 0x06002B26 RID: 11046 RVA: 0x000B921C File Offset: 0x000B741C
		public static event NetworkUser.NetworkUserGenericDelegate onPostNetworkUserStart;

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x06002B27 RID: 11047 RVA: 0x000B924F File Offset: 0x000B744F
		// (remove) Token: 0x06002B28 RID: 11048 RVA: 0x000B9257 File Offset: 0x000B7457
		[Obsolete("Use onNetworkUserUnlockablesUpdated instead", false)]
		public static event NetworkUser.NetworkUserGenericDelegate OnNetworkUserUnlockablesUpdated
		{
			add
			{
				NetworkUser.onNetworkUserUnlockablesUpdated += value;
			}
			remove
			{
				NetworkUser.onNetworkUserUnlockablesUpdated -= value;
			}
		}

		// Token: 0x1400007C RID: 124
		// (add) Token: 0x06002B29 RID: 11049 RVA: 0x000B9260 File Offset: 0x000B7460
		// (remove) Token: 0x06002B2A RID: 11050 RVA: 0x000B9294 File Offset: 0x000B7494
		public static event NetworkUser.NetworkUserGenericDelegate onNetworkUserUnlockablesUpdated;

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x06002B2B RID: 11051 RVA: 0x000B92C8 File Offset: 0x000B74C8
		// (remove) Token: 0x06002B2C RID: 11052 RVA: 0x000B92FC File Offset: 0x000B74FC
		public static event NetworkUser.NetworkUserGenericDelegate onNetworkUserDiscovered;

		// Token: 0x1400007E RID: 126
		// (add) Token: 0x06002B2D RID: 11053 RVA: 0x000B9330 File Offset: 0x000B7530
		// (remove) Token: 0x06002B2E RID: 11054 RVA: 0x000B9364 File Offset: 0x000B7564
		public static event NetworkUser.NetworkUserGenericDelegate onNetworkUserLost;

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x06002B2F RID: 11055 RVA: 0x000B9398 File Offset: 0x000B7598
		// (remove) Token: 0x06002B30 RID: 11056 RVA: 0x000B93CC File Offset: 0x000B75CC
		public static event NetworkUser.NetworkUserGenericDelegate onNetworkUserBodyPreferenceChanged;

		// Token: 0x06002B31 RID: 11057 RVA: 0x000B9400 File Offset: 0x000B7600
		public static bool AllParticipatingNetworkUsersReady()
		{
			ReadOnlyCollection<NetworkUser> readOnlyCollection = NetworkUser.readOnlyInstancesList;
			for (int i = 0; i < readOnlyCollection.Count; i++)
			{
				NetworkUser networkUser = readOnlyCollection[i];
				if (networkUser.isParticipating && !networkUser.connectionToClient.isReady)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x000B9444 File Offset: 0x000B7644
		[CanBeNull]
		private static NetworkUser FindNetworkUserByUserProfile([NotNull] UserProfile userProfile)
		{
			if (userProfile == null)
			{
				throw new ArgumentNullException("userProfile");
			}
			ReadOnlyCollection<LocalUser> readOnlyLocalUsersList = LocalUserManager.readOnlyLocalUsersList;
			for (int i = 0; i < readOnlyLocalUsersList.Count; i++)
			{
				LocalUser localUser = readOnlyLocalUsersList[i];
				if (localUser.userProfile == userProfile)
				{
					return localUser.currentNetworkUser;
				}
			}
			return null;
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x000B94D0 File Offset: 0x000B76D0
		static NetworkUser()
		{
			NetworkBehaviour.RegisterCommandDelegate(typeof(NetworkUser), NetworkUser.kCmdCmdSetNetLunarCoins, new NetworkBehaviour.CmdDelegate(NetworkUser.InvokeCmdCmdSetNetLunarCoins));
			NetworkUser.kCmdCmdSetBodyPreference = 234442470;
			NetworkBehaviour.RegisterCommandDelegate(typeof(NetworkUser), NetworkUser.kCmdCmdSetBodyPreference, new NetworkBehaviour.CmdDelegate(NetworkUser.InvokeCmdCmdSetBodyPreference));
			NetworkUser.kCmdCmdSendConsoleCommand = -1997680971;
			NetworkBehaviour.RegisterCommandDelegate(typeof(NetworkUser), NetworkUser.kCmdCmdSendConsoleCommand, new NetworkBehaviour.CmdDelegate(NetworkUser.InvokeCmdCmdSendConsoleCommand));
			NetworkUser.kCmdCmdSendNewUnlockables = 1855027350;
			NetworkBehaviour.RegisterCommandDelegate(typeof(NetworkUser), NetworkUser.kCmdCmdSendNewUnlockables, new NetworkBehaviour.CmdDelegate(NetworkUser.InvokeCmdCmdSendNewUnlockables));
			NetworkUser.kCmdCmdReportAchievement = -1674656990;
			NetworkBehaviour.RegisterCommandDelegate(typeof(NetworkUser), NetworkUser.kCmdCmdReportAchievement, new NetworkBehaviour.CmdDelegate(NetworkUser.InvokeCmdCmdReportAchievement));
			NetworkUser.kCmdCmdReportUnlock = -1831223439;
			NetworkBehaviour.RegisterCommandDelegate(typeof(NetworkUser), NetworkUser.kCmdCmdReportUnlock, new NetworkBehaviour.CmdDelegate(NetworkUser.InvokeCmdCmdReportUnlock));
			NetworkUser.kCmdCmdAcknowledgeStage = -2118585573;
			NetworkBehaviour.RegisterCommandDelegate(typeof(NetworkUser), NetworkUser.kCmdCmdAcknowledgeStage, new NetworkBehaviour.CmdDelegate(NetworkUser.InvokeCmdCmdAcknowledgeStage));
			NetworkUser.kCmdCmdSubmitVote = 329593659;
			NetworkBehaviour.RegisterCommandDelegate(typeof(NetworkUser), NetworkUser.kCmdCmdSubmitVote, new NetworkBehaviour.CmdDelegate(NetworkUser.InvokeCmdCmdSubmitVote));
			NetworkUser.kRpcRpcDeductLunarCoins = -1554352898;
			NetworkBehaviour.RegisterRpcDelegate(typeof(NetworkUser), NetworkUser.kRpcRpcDeductLunarCoins, new NetworkBehaviour.CmdDelegate(NetworkUser.InvokeRpcRpcDeductLunarCoins));
			NetworkUser.kRpcRpcAwardLunarCoins = -604060198;
			NetworkBehaviour.RegisterRpcDelegate(typeof(NetworkUser), NetworkUser.kRpcRpcAwardLunarCoins, new NetworkBehaviour.CmdDelegate(NetworkUser.InvokeRpcRpcAwardLunarCoins));
			NetworkUser.kRpcRpcRequestUnlockables = -1809653515;
			NetworkBehaviour.RegisterRpcDelegate(typeof(NetworkUser), NetworkUser.kRpcRpcRequestUnlockables, new NetworkBehaviour.CmdDelegate(NetworkUser.InvokeRpcRpcRequestUnlockables));
			NetworkCRC.RegisterBehaviour("NetworkUser", 0);
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x000B96EC File Offset: 0x000B78EC
		// (set) Token: 0x06002B37 RID: 11063 RVA: 0x000B96FF File Offset: 0x000B78FF
		public NetworkUserId Network_id
		{
			get
			{
				return this._id;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnSyncId(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<NetworkUserId>(value, ref this._id, 1U);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x000B9740 File Offset: 0x000B7940
		// (set) Token: 0x06002B39 RID: 11065 RVA: 0x000B9753 File Offset: 0x000B7953
		public byte NetworkrewiredPlayerId
		{
			get
			{
				return this.rewiredPlayerId;
			}
			[param: In]
			set
			{
				base.SetSyncVar<byte>(value, ref this.rewiredPlayerId, 2U);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x000B9768 File Offset: 0x000B7968
		// (set) Token: 0x06002B3B RID: 11067 RVA: 0x000B977B File Offset: 0x000B797B
		public NetworkInstanceId Network_masterObjectId
		{
			get
			{
				return this._masterObjectId;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnSyncMasterObjectId(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<NetworkInstanceId>(value, ref this._masterObjectId, 4U);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x000B97BC File Offset: 0x000B79BC
		// (set) Token: 0x06002B3D RID: 11069 RVA: 0x000B97CF File Offset: 0x000B79CF
		public Color32 NetworkuserColor
		{
			get
			{
				return this.userColor;
			}
			[param: In]
			set
			{
				base.SetSyncVar<Color32>(value, ref this.userColor, 8U);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x000B97E4 File Offset: 0x000B79E4
		// (set) Token: 0x06002B3F RID: 11071 RVA: 0x000B97F7 File Offset: 0x000B79F7
		public uint NetworknetLunarCoins
		{
			get
			{
				return this.netLunarCoins;
			}
			[param: In]
			set
			{
				base.SetSyncVar<uint>(value, ref this.netLunarCoins, 16U);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x000B980C File Offset: 0x000B7A0C
		// (set) Token: 0x06002B41 RID: 11073 RVA: 0x000B9820 File Offset: 0x000B7A20
		public BodyIndex NetworkbodyIndexPreference
		{
			get
			{
				return this.bodyIndexPreference;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.SetBodyPreference(value);
					base.syncVarHookGuard = false;
				}
				ulong newValueAsUlong = (ulong)((long)value);
				ulong fieldValueAsUlong = (ulong)((long)this.bodyIndexPreference);
				base.SetSyncVarEnum<BodyIndex>(value, newValueAsUlong, ref this.bodyIndexPreference, fieldValueAsUlong, 32U);
			}
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x000B987B File Offset: 0x000B7A7B
		protected static void InvokeCmdCmdSetNetLunarCoins(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdSetNetLunarCoins called on client.");
				return;
			}
			((NetworkUser)obj).CmdSetNetLunarCoins(reader.ReadPackedUInt32());
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x000B98A4 File Offset: 0x000B7AA4
		protected static void InvokeCmdCmdSetBodyPreference(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdSetBodyPreference called on client.");
				return;
			}
			((NetworkUser)obj).CmdSetBodyPreference((BodyIndex)reader.ReadInt32());
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x000B98CD File Offset: 0x000B7ACD
		protected static void InvokeCmdCmdSendConsoleCommand(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdSendConsoleCommand called on client.");
				return;
			}
			((NetworkUser)obj).CmdSendConsoleCommand(reader.ReadString(), GeneratedNetworkCode._ReadArrayString_None(reader));
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x000B98FC File Offset: 0x000B7AFC
		protected static void InvokeCmdCmdSendNewUnlockables(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdSendNewUnlockables called on client.");
				return;
			}
			((NetworkUser)obj).CmdSendNewUnlockables(GeneratedNetworkCode._ReadArrayUnlockableIndex_None(reader));
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000B9925 File Offset: 0x000B7B25
		protected static void InvokeCmdCmdReportAchievement(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdReportAchievement called on client.");
				return;
			}
			((NetworkUser)obj).CmdReportAchievement(reader.ReadString());
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x000B994E File Offset: 0x000B7B4E
		protected static void InvokeCmdCmdReportUnlock(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdReportUnlock called on client.");
				return;
			}
			((NetworkUser)obj).CmdReportUnlock((UnlockableIndex)reader.ReadInt32());
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x000B9977 File Offset: 0x000B7B77
		protected static void InvokeCmdCmdAcknowledgeStage(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdAcknowledgeStage called on client.");
				return;
			}
			((NetworkUser)obj).CmdAcknowledgeStage(reader.ReadNetworkId());
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x000B99A0 File Offset: 0x000B7BA0
		protected static void InvokeCmdCmdSubmitVote(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdSubmitVote called on client.");
				return;
			}
			((NetworkUser)obj).CmdSubmitVote(reader.ReadGameObject(), (int)reader.ReadPackedUInt32());
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x000B99D0 File Offset: 0x000B7BD0
		public void CallCmdSetNetLunarCoins(uint newNetLunarCoins)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdSetNetLunarCoins called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdSetNetLunarCoins(newNetLunarCoins);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)NetworkUser.kCmdCmdSetNetLunarCoins);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.WritePackedUInt32(newNetLunarCoins);
			base.SendCommandInternal(networkWriter, 0, "CmdSetNetLunarCoins");
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x000B9A5C File Offset: 0x000B7C5C
		public void CallCmdSetBodyPreference(BodyIndex newBodyIndexPreference)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdSetBodyPreference called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdSetBodyPreference(newBodyIndexPreference);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)NetworkUser.kCmdCmdSetBodyPreference);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write((int)newBodyIndexPreference);
			base.SendCommandInternal(networkWriter, 0, "CmdSetBodyPreference");
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000B9AE8 File Offset: 0x000B7CE8
		public void CallCmdSendConsoleCommand(string commandName, string[] args)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdSendConsoleCommand called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdSendConsoleCommand(commandName, args);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)NetworkUser.kCmdCmdSendConsoleCommand);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(commandName);
			GeneratedNetworkCode._WriteArrayString_None(networkWriter, args);
			base.SendCommandInternal(networkWriter, 0, "CmdSendConsoleCommand");
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x000B9B80 File Offset: 0x000B7D80
		public void CallCmdSendNewUnlockables(UnlockableIndex[] newUnlockableIndices)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdSendNewUnlockables called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdSendNewUnlockables(newUnlockableIndices);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)NetworkUser.kCmdCmdSendNewUnlockables);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			GeneratedNetworkCode._WriteArrayUnlockableIndex_None(networkWriter, newUnlockableIndices);
			base.SendCommandInternal(networkWriter, 0, "CmdSendNewUnlockables");
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x000B9C0C File Offset: 0x000B7E0C
		public void CallCmdReportAchievement(string achievementNameToken)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdReportAchievement called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdReportAchievement(achievementNameToken);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)NetworkUser.kCmdCmdReportAchievement);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(achievementNameToken);
			base.SendCommandInternal(networkWriter, 0, "CmdReportAchievement");
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x000B9C98 File Offset: 0x000B7E98
		public void CallCmdReportUnlock(UnlockableIndex unlockIndex)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdReportUnlock called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdReportUnlock(unlockIndex);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)NetworkUser.kCmdCmdReportUnlock);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write((int)unlockIndex);
			base.SendCommandInternal(networkWriter, 0, "CmdReportUnlock");
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000B9D24 File Offset: 0x000B7F24
		public void CallCmdAcknowledgeStage(NetworkInstanceId stageNetworkId)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdAcknowledgeStage called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdAcknowledgeStage(stageNetworkId);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)NetworkUser.kCmdCmdAcknowledgeStage);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(stageNetworkId);
			base.SendCommandInternal(networkWriter, 0, "CmdAcknowledgeStage");
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000B9DB0 File Offset: 0x000B7FB0
		public void CallCmdSubmitVote(GameObject voteControllerGameObject, int choiceIndex)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdSubmitVote called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdSubmitVote(voteControllerGameObject, choiceIndex);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)NetworkUser.kCmdCmdSubmitVote);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(voteControllerGameObject);
			networkWriter.WritePackedUInt32((uint)choiceIndex);
			base.SendCommandInternal(networkWriter, 0, "CmdSubmitVote");
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000B9E48 File Offset: 0x000B8048
		protected static void InvokeRpcRpcDeductLunarCoins(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcDeductLunarCoins called on server.");
				return;
			}
			((NetworkUser)obj).RpcDeductLunarCoins(reader.ReadPackedUInt32());
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000B9E71 File Offset: 0x000B8071
		protected static void InvokeRpcRpcAwardLunarCoins(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcAwardLunarCoins called on server.");
				return;
			}
			((NetworkUser)obj).RpcAwardLunarCoins(reader.ReadPackedUInt32());
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x000B9E9A File Offset: 0x000B809A
		protected static void InvokeRpcRpcRequestUnlockables(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcRequestUnlockables called on server.");
				return;
			}
			((NetworkUser)obj).RpcRequestUnlockables();
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000B9EC0 File Offset: 0x000B80C0
		public void CallRpcDeductLunarCoins(uint count)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcDeductLunarCoins called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)NetworkUser.kRpcRpcDeductLunarCoins);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.WritePackedUInt32(count);
			this.SendRPCInternal(networkWriter, 0, "RpcDeductLunarCoins");
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000B9F34 File Offset: 0x000B8134
		public void CallRpcAwardLunarCoins(uint count)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcAwardLunarCoins called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)NetworkUser.kRpcRpcAwardLunarCoins);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.WritePackedUInt32(count);
			this.SendRPCInternal(networkWriter, 0, "RpcAwardLunarCoins");
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x000B9FA8 File Offset: 0x000B81A8
		public void CallRpcRequestUnlockables()
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcRequestUnlockables called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)NetworkUser.kRpcRpcRequestUnlockables);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			this.SendRPCInternal(networkWriter, 0, "RpcRequestUnlockables");
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x000BA014 File Offset: 0x000B8214
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				GeneratedNetworkCode._WriteNetworkUserId_None(writer, this._id);
				writer.WritePackedUInt32((uint)this.rewiredPlayerId);
				writer.Write(this._masterObjectId);
				writer.Write(this.userColor);
				writer.WritePackedUInt32(this.netLunarCoins);
				writer.Write((int)this.bodyIndexPreference);
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
				GeneratedNetworkCode._WriteNetworkUserId_None(writer, this._id);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32((uint)this.rewiredPlayerId);
			}
			if ((base.syncVarDirtyBits & 4U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this._masterObjectId);
			}
			if ((base.syncVarDirtyBits & 8U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.userColor);
			}
			if ((base.syncVarDirtyBits & 16U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32(this.netLunarCoins);
			}
			if ((base.syncVarDirtyBits & 32U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write((int)this.bodyIndexPreference);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x000BA1BC File Offset: 0x000B83BC
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this._id = GeneratedNetworkCode._ReadNetworkUserId_None(reader);
				this.rewiredPlayerId = (byte)reader.ReadPackedUInt32();
				this._masterObjectId = reader.ReadNetworkId();
				this.userColor = reader.ReadColor32();
				this.netLunarCoins = reader.ReadPackedUInt32();
				this.bodyIndexPreference = (BodyIndex)reader.ReadInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.OnSyncId(GeneratedNetworkCode._ReadNetworkUserId_None(reader));
			}
			if ((num & 2) != 0)
			{
				this.rewiredPlayerId = (byte)reader.ReadPackedUInt32();
			}
			if ((num & 4) != 0)
			{
				this.OnSyncMasterObjectId(reader.ReadNetworkId());
			}
			if ((num & 8) != 0)
			{
				this.userColor = reader.ReadColor32();
			}
			if ((num & 16) != 0)
			{
				this.netLunarCoins = reader.ReadPackedUInt32();
			}
			if ((num & 32) != 0)
			{
				this.SetBodyPreference((BodyIndex)reader.ReadInt32());
			}
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002DBA RID: 11706
		private static readonly List<NetworkUser> instancesList = new List<NetworkUser>();

		// Token: 0x04002DBB RID: 11707
		public static readonly ReadOnlyCollection<NetworkUser> readOnlyInstancesList = new ReadOnlyCollection<NetworkUser>(NetworkUser.instancesList);

		// Token: 0x04002DBC RID: 11708
		private static readonly List<NetworkUser> localPlayers = new List<NetworkUser>();

		// Token: 0x04002DBD RID: 11709
		public static readonly ReadOnlyCollection<NetworkUser> readOnlyLocalPlayersList = new ReadOnlyCollection<NetworkUser>(NetworkUser.localPlayers);

		// Token: 0x04002DBF RID: 11711
		[SyncVar(hook = "OnSyncId")]
		private NetworkUserId _id;

		// Token: 0x04002DC0 RID: 11712
		[SyncVar]
		public byte rewiredPlayerId;

		// Token: 0x04002DC1 RID: 11713
		[SyncVar(hook = "OnSyncMasterObjectId")]
		private NetworkInstanceId _masterObjectId;

		// Token: 0x04002DC2 RID: 11714
		[CanBeNull]
		public LocalUser localUser;

		// Token: 0x04002DC3 RID: 11715
		public CameraRigController cameraRigController;

		// Token: 0x04002DC4 RID: 11716
		public string userName = "";

		// Token: 0x04002DC5 RID: 11717
		[SyncVar]
		public Color32 userColor = Color.red;

		// Token: 0x04002DC6 RID: 11718
		[SyncVar]
		private uint netLunarCoins;

		// Token: 0x04002DC7 RID: 11719
		private MemoizedGetComponent<CharacterMaster> cachedMaster;

		// Token: 0x04002DC8 RID: 11720
		private MemoizedGetComponent<PlayerCharacterMasterController> cachedPlayerCharacterMasterController;

		// Token: 0x04002DC9 RID: 11721
		private MemoizedGetComponent<PlayerStatsComponent> cachedPlayerStatsComponent;

		// Token: 0x04002DCA RID: 11722
		private GameObject _masterObject;

		// Token: 0x04002DCB RID: 11723
		[SyncVar(hook = "SetBodyPreference")]
		[NonSerialized]
		public BodyIndex bodyIndexPreference = BodyIndex.None;

		// Token: 0x04002DCC RID: 11724
		private float secondAccumulator;

		// Token: 0x04002DCD RID: 11725
		[NonSerialized]
		public List<UnlockableDef> unlockables = new List<UnlockableDef>();

		// Token: 0x04002DCE RID: 11726
		public List<string> debugUnlockablesList = new List<string>();

		// Token: 0x04002DCF RID: 11727
		private static NetworkInstanceId serverCurrentStage;

		// Token: 0x04002DD1 RID: 11729
		private NetworkInstanceId _serverLastStageAcknowledgedByClient;

		// Token: 0x04002DD9 RID: 11737
		private static int kRpcRpcDeductLunarCoins;

		// Token: 0x04002DDA RID: 11738
		private static int kRpcRpcAwardLunarCoins;

		// Token: 0x04002DDB RID: 11739
		private static int kCmdCmdSetNetLunarCoins = -934763456;

		// Token: 0x04002DDC RID: 11740
		private static int kCmdCmdSetBodyPreference;

		// Token: 0x04002DDD RID: 11741
		private static int kCmdCmdSendConsoleCommand;

		// Token: 0x04002DDE RID: 11742
		private static int kCmdCmdSendNewUnlockables;

		// Token: 0x04002DDF RID: 11743
		private static int kRpcRpcRequestUnlockables;

		// Token: 0x04002DE0 RID: 11744
		private static int kCmdCmdReportAchievement;

		// Token: 0x04002DE1 RID: 11745
		private static int kCmdCmdReportUnlock;

		// Token: 0x04002DE2 RID: 11746
		private static int kCmdCmdAcknowledgeStage;

		// Token: 0x04002DE3 RID: 11747
		private static int kCmdCmdSubmitVote;

		// Token: 0x020007D8 RID: 2008
		// (Invoke) Token: 0x06002B5C RID: 11100
		public delegate void NetworkUserGenericDelegate(NetworkUser networkUser);
	}
}
