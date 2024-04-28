using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using HG;
using RoR2.ContentManagement;
using RoR2.ConVar;
using RoR2.UI;
using Unity;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace RoR2.Networking
{
	// Token: 0x02000C5C RID: 3164
	public abstract class NetworkManagerSystem : NetworkManager
	{
		// Token: 0x060047BB RID: 18363 RVA: 0x00128660 File Offset: 0x00126860
		static NetworkManagerSystem()
		{
			NetworkManagerSystem.loadingSceneAsyncFieldInfo = typeof(NetworkManager).GetField("s_LoadingSceneAsync", BindingFlags.Static | BindingFlags.NonPublic);
			if (NetworkManagerSystem.loadingSceneAsyncFieldInfo == null)
			{
				Debug.LogError("NetworkManager.s_LoadingSceneAsync field could not be found! Make sure to provide a proper implementation for this version of Unity.");
			}
		}

		// Token: 0x060047BC RID: 18364 RVA: 0x0012874C File Offset: 0x0012694C
		public virtual void Init(NetworkManagerConfiguration configurationComponent)
		{
			base.dontDestroyOnLoad = configurationComponent.DontDestroyOnLoad;
			base.runInBackground = configurationComponent.RunInBackground;
			base.logLevel = configurationComponent.LogLevel;
			base.offlineScene = configurationComponent.OfflineScene;
			base.onlineScene = configurationComponent.OnlineScene;
			base.playerPrefab = configurationComponent.PlayerPrefab;
			base.autoCreatePlayer = configurationComponent.AutoCreatePlayer;
			base.playerSpawnMethod = configurationComponent.PlayerSpawnMethod;
			base.spawnPrefabs.Clear();
			base.spawnPrefabs.AddRange(configurationComponent.SpawnPrefabs);
			base.customConfig = configurationComponent.CustomConfig;
			base.maxConnections = configurationComponent.MaxConnections;
			base.channels.Clear();
			base.channels.AddRange(configurationComponent.QosChannels);
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060047BD RID: 18365 RVA: 0x0012880C File Offset: 0x00126A0C
		protected static bool isLoadingScene
		{
			get
			{
				NetworkManager.IChangeSceneAsyncOperation changeSceneAsyncOperation = (NetworkManager.IChangeSceneAsyncOperation)NetworkManagerSystem.loadingSceneAsyncFieldInfo.GetValue(null);
				return changeSceneAsyncOperation != null && !changeSceneAsyncOperation.isDone;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060047BE RID: 18366 RVA: 0x00128838 File Offset: 0x00126A38
		public new static NetworkManagerSystem singleton
		{
			get
			{
				return (NetworkManagerSystem)NetworkManager.singleton;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060047BF RID: 18367 RVA: 0x00128844 File Offset: 0x00126A44
		public float unpredictedServerFixedTime
		{
			get
			{
				return this._unpredictedServerFixedTime;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060047C0 RID: 18368 RVA: 0x0012884C File Offset: 0x00126A4C
		public float unpredictedServerFixedTimeSmoothed
		{
			get
			{
				return this._unpredictedServerFixedTimeSmoothed;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060047C1 RID: 18369 RVA: 0x00128854 File Offset: 0x00126A54
		public float serverFixedTime
		{
			get
			{
				return this.unpredictedServerFixedTimeSmoothed + this.filteredClientRttFixed;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060047C2 RID: 18370 RVA: 0x00128863 File Offset: 0x00126A63
		public float unpredictedServerFrameTime
		{
			get
			{
				return this._unpredictedServerFrameTime;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060047C3 RID: 18371 RVA: 0x0012886B File Offset: 0x00126A6B
		public float unpredictedServerFrameTimeSmoothed
		{
			get
			{
				return this._unpredictedServerFrameTimeSmoothed;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060047C4 RID: 18372 RVA: 0x00128873 File Offset: 0x00126A73
		public float serverFrameTime
		{
			get
			{
				return this.unpredictedServerFrameTimeSmoothed + this.filteredClientRttFrame;
			}
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x00128884 File Offset: 0x00126A84
		protected void InitializeTime()
		{
			this._unpredictedServerFixedTime = 0f;
			this._unpredictedServerFixedTimeSmoothed = 0f;
			this.unpredictedServerFixedTimeVelocity = 1f;
			this._unpredictedServerFrameTime = 0f;
			this._unpredictedServerFrameTimeSmoothed = 0f;
			this.unpredictedServerFrameTimeVelocity = 1f;
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x001288D4 File Offset: 0x00126AD4
		protected void UpdateTime(ref float targetValue, ref float currentValue, ref float velocity, float deltaTime)
		{
			if (deltaTime <= 0f)
			{
				return;
			}
			targetValue += deltaTime;
			float num = (targetValue - currentValue) / deltaTime;
			float num2 = 1f;
			if (velocity == 0f || Mathf.Abs(num) > num2 * 3f)
			{
				currentValue = targetValue;
				velocity = num2;
				return;
			}
			currentValue += velocity * deltaTime;
			velocity = Mathf.MoveTowards(velocity, num, NetworkManagerSystem.cvNetTimeSmoothRate.value * deltaTime);
		}

		// Token: 0x060047C7 RID: 18375 RVA: 0x00128944 File Offset: 0x00126B44
		protected static NetworkUser[] GetConnectionNetworkUsers(NetworkConnection conn)
		{
			List<PlayerController> playerControllers = conn.playerControllers;
			NetworkUser[] array = new NetworkUser[playerControllers.Count];
			for (int i = 0; i < playerControllers.Count; i++)
			{
				array[i] = playerControllers[i].gameObject.GetComponent<NetworkUser>();
			}
			return array;
		}

		// Token: 0x060047C8 RID: 18376
		protected abstract void Start();

		// Token: 0x140000E8 RID: 232
		// (add) Token: 0x060047C9 RID: 18377 RVA: 0x0012898C File Offset: 0x00126B8C
		// (remove) Token: 0x060047CA RID: 18378 RVA: 0x001289C0 File Offset: 0x00126BC0
		public static event Action onStartGlobal;

		// Token: 0x060047CB RID: 18379 RVA: 0x001289F3 File Offset: 0x00126BF3
		protected void OnDestroy()
		{
			typeof(NetworkManager).GetMethod("OnDestroy", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(this, null);
		}

		// Token: 0x060047CC RID: 18380 RVA: 0x00128A13 File Offset: 0x00126C13
		public void FireOnStartGlobalEvent()
		{
			Action action = NetworkManagerSystem.onStartGlobal;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x060047CD RID: 18381 RVA: 0x00128A24 File Offset: 0x00126C24
		protected void FixedUpdate()
		{
			this.UpdateTime(ref this._unpredictedServerFixedTime, ref this._unpredictedServerFixedTimeSmoothed, ref this.unpredictedServerFixedTimeVelocity, Time.fixedDeltaTime);
			this.FixedUpdateServer();
			this.FixedUpdateClient();
			this.debugServerTime = this.unpredictedServerFixedTime;
			this.debugRTT = this.clientRttFrame;
		}

		// Token: 0x060047CE RID: 18382
		protected abstract void Update();

		// Token: 0x060047CF RID: 18383
		protected abstract void EnsureDesiredHost();

		// Token: 0x060047D0 RID: 18384
		public abstract void ForceCloseAllConnections();

		// Token: 0x060047D1 RID: 18385 RVA: 0x00128A74 File Offset: 0x00126C74
		public void StartSinglePlayer()
		{
			Debug.Log("Setting isSinglePlayer to true");
			NetworkServer.dontListen = true;
			this.desiredHost = default(HostDescription);
			this.isSinglePlayer = true;
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060047D2 RID: 18386 RVA: 0x00128AA7 File Offset: 0x00126CA7
		// (set) Token: 0x060047D3 RID: 18387 RVA: 0x00128AB0 File Offset: 0x00126CB0
		public HostDescription desiredHost
		{
			get
			{
				return this._desiredHost;
			}
			set
			{
				if (this._desiredHost.Equals(value))
				{
					return;
				}
				this._desiredHost = value;
				this.actedUponDesiredHost = false;
				this.lastDesiredHostSetTime = Time.unscaledTime;
				Debug.LogFormat("NetworkManagerSystem.desiredHost={0}", new object[]
				{
					this._desiredHost.ToString()
				});
			}
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x00128B0C File Offset: 0x00126D0C
		public void ResetDesiredHost()
		{
			this.actedUponDesiredHost = false;
			NetworkManagerSystem.singleton.desiredHost = default(HostDescription);
		}

		// Token: 0x060047D5 RID: 18389
		public abstract void CreateLocalLobby();

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060047D6 RID: 18390 RVA: 0x00128B33 File Offset: 0x00126D33
		// (set) Token: 0x060047D7 RID: 18391 RVA: 0x00128B3B File Offset: 0x00126D3B
		public bool clientHasConfirmedQuit { get; private set; }

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060047D8 RID: 18392 RVA: 0x00128B44 File Offset: 0x00126D44
		protected bool clientIsConnecting
		{
			get
			{
				NetworkClient client = this.client;
				return ((client != null) ? client.connection : null) != null && !this.client.isConnected;
			}
		}

		// Token: 0x140000E9 RID: 233
		// (add) Token: 0x060047D9 RID: 18393 RVA: 0x00128B6C File Offset: 0x00126D6C
		// (remove) Token: 0x060047DA RID: 18394 RVA: 0x00128BA0 File Offset: 0x00126DA0
		public static event Action<NetworkClient> onStartClientGlobal;

		// Token: 0x140000EA RID: 234
		// (add) Token: 0x060047DB RID: 18395 RVA: 0x00128BD4 File Offset: 0x00126DD4
		// (remove) Token: 0x060047DC RID: 18396 RVA: 0x00128C08 File Offset: 0x00126E08
		public static event Action onStopClientGlobal;

		// Token: 0x140000EB RID: 235
		// (add) Token: 0x060047DD RID: 18397 RVA: 0x00128C3C File Offset: 0x00126E3C
		// (remove) Token: 0x060047DE RID: 18398 RVA: 0x00128C70 File Offset: 0x00126E70
		public static event Action<NetworkConnection> onClientConnectGlobal;

		// Token: 0x140000EC RID: 236
		// (add) Token: 0x060047DF RID: 18399 RVA: 0x00128CA4 File Offset: 0x00126EA4
		// (remove) Token: 0x060047E0 RID: 18400 RVA: 0x00128CD8 File Offset: 0x00126ED8
		public static event Action<NetworkConnection> onClientDisconnectGlobal;

		// Token: 0x060047E1 RID: 18401 RVA: 0x00128D0C File Offset: 0x00126F0C
		public override void OnStartClient(NetworkClient newClient)
		{
			base.OnStartClient(newClient);
			this.InitializeTime();
			NetworkManagerSystem.<OnStartClient>g__RegisterPrefabs|69_0(ContentManager.bodyPrefabs);
			NetworkManagerSystem.<OnStartClient>g__RegisterPrefabs|69_0(ContentManager.masterPrefabs);
			NetworkManagerSystem.<OnStartClient>g__RegisterPrefabs|69_0(ContentManager.projectilePrefabs);
			NetworkManagerSystem.<OnStartClient>g__RegisterPrefabs|69_0(ContentManager.networkedObjectPrefabs);
			NetworkManagerSystem.<OnStartClient>g__RegisterPrefabs|69_0(ContentManager.gameModePrefabs);
			ClientScene.RegisterPrefab(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkSession"));
			ClientScene.RegisterPrefab(LegacyResourcesAPI.Load<GameObject>("Prefabs/Stage"));
			NetworkMessageHandlerAttribute.RegisterClientMessages(newClient);
			Action<NetworkClient> action = NetworkManagerSystem.onStartClientGlobal;
			if (action == null)
			{
				return;
			}
			action(newClient);
		}

		// Token: 0x060047E2 RID: 18402 RVA: 0x00128D8C File Offset: 0x00126F8C
		public override void OnStopClient()
		{
			try
			{
				Action action = NetworkManagerSystem.onStopClientGlobal;
				if (action != null)
				{
					action();
				}
			}
			catch (Exception message)
			{
				Debug.LogError(message);
			}
			foreach (NetworkClient networkClient in NetworkClient.allClients)
			{
				if (networkClient != null)
				{
					NetworkConnection connection = networkClient.connection;
					if (connection != null)
					{
						connection.Disconnect();
					}
				}
			}
			this.ForceCloseAllConnections();
			if (this.actedUponDesiredHost)
			{
				NetworkManagerSystem.singleton.desiredHost = HostDescription.none;
			}
			base.OnStopClient();
		}

		// Token: 0x060047E3 RID: 18403 RVA: 0x00128E34 File Offset: 0x00127034
		public override void OnClientConnect(NetworkConnection conn)
		{
			base.OnClientConnect(conn);
			this.clientRttFrame = 0f;
			this.filteredClientRttFixed = 0f;
			this.ClientSendAuth(conn);
			this.ClientSetPlayers(conn);
			Action<NetworkConnection> action = NetworkManagerSystem.onClientConnectGlobal;
			if (action == null)
			{
				return;
			}
			action(conn);
		}

		// Token: 0x060047E4 RID: 18404 RVA: 0x00128E71 File Offset: 0x00127071
		public override void OnClientDisconnect(NetworkConnection conn)
		{
			base.OnClientDisconnect(conn);
			Action<NetworkConnection> action = NetworkManagerSystem.onClientDisconnectGlobal;
			if (action == null)
			{
				return;
			}
			action(conn);
		}

		// Token: 0x060047E5 RID: 18405 RVA: 0x00128E8C File Offset: 0x0012708C
		public void ClientAddPlayer(short playerControllerId, NetworkConnection connection = null)
		{
			foreach (PlayerController playerController in ClientScene.localPlayers)
			{
				if (playerController.playerControllerId == playerControllerId && playerController.IsValid && playerController.gameObject)
				{
					Debug.LogFormat("Player {0} already added, aborting.", new object[]
					{
						playerControllerId
					});
					return;
				}
			}
			Debug.LogFormat("Adding local player controller {0} on connection {1}", new object[]
			{
				playerControllerId,
				connection
			});
			NetworkManagerSystem.AddPlayerMessage extraMessage = this.CreateClientAddPlayerMessage();
			ClientScene.AddPlayer(connection, playerControllerId, extraMessage);
		}

		// Token: 0x060047E6 RID: 18406
		protected abstract NetworkManagerSystem.AddPlayerMessage CreateClientAddPlayerMessage();

		// Token: 0x060047E7 RID: 18407 RVA: 0x00128F40 File Offset: 0x00127140
		protected void UpdateClient()
		{
			this.UpdateCheckInactiveConnections();
			NetworkClient client = this.client;
			if (((client != null) ? client.connection : null) != null)
			{
				this.filteredClientRttFrame = RttManager.GetConnectionFrameSmoothedRtt(this.client.connection);
				this.clientRttFrame = RttManager.GetConnectionRTT(this.client.connection);
			}
			bool flag = (this.client != null && !ClientScene.ready) || NetworkManagerSystem.isLoadingScene;
			if (NetworkManagerSystem.wasFading != flag)
			{
				if (flag)
				{
					FadeToBlackManager.fadeCount++;
				}
				else
				{
					FadeToBlackManager.fadeCount--;
				}
				NetworkManagerSystem.wasFading = flag;
			}
		}

		// Token: 0x060047E8 RID: 18408
		protected abstract void UpdateCheckInactiveConnections();

		// Token: 0x060047E9 RID: 18409
		protected abstract void StartClient(UserID serverID);

		// Token: 0x060047EA RID: 18410
		public abstract bool IsConnectedToServer(UserID serverID);

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060047EB RID: 18411 RVA: 0x00128FD6 File Offset: 0x001271D6
		// (set) Token: 0x060047EC RID: 18412 RVA: 0x00128FDE File Offset: 0x001271DE
		public float clientRttFixed { get; private set; }

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060047ED RID: 18413 RVA: 0x00128FE7 File Offset: 0x001271E7
		// (set) Token: 0x060047EE RID: 18414 RVA: 0x00128FEF File Offset: 0x001271EF
		public float clientRttFrame { get; private set; }

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060047EF RID: 18415 RVA: 0x00128FF8 File Offset: 0x001271F8
		// (set) Token: 0x060047F0 RID: 18416 RVA: 0x00129000 File Offset: 0x00127200
		public float filteredClientRttFixed { get; private set; }

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060047F1 RID: 18417 RVA: 0x00129009 File Offset: 0x00127209
		// (set) Token: 0x060047F2 RID: 18418 RVA: 0x00129011 File Offset: 0x00127211
		public float filteredClientRttFrame { get; private set; }

		// Token: 0x060047F3 RID: 18419 RVA: 0x0012901C File Offset: 0x0012721C
		private void FixedUpdateClient()
		{
			if (!NetworkClient.active || this.client == null)
			{
				return;
			}
			NetworkClient client = this.client;
			if (((client != null) ? client.connection : null) != null && this.client.connection.isConnected)
			{
				NetworkConnection connection = this.client.connection;
				this.filteredClientRttFixed = RttManager.GetConnectionFixedSmoothedRtt(connection);
				this.clientRttFixed = RttManager.GetConnectionRTT(connection);
				if (!Util.ConnectionIsLocal(connection))
				{
					RttManager.Ping(connection, QosChannelIndex.ping.intVal);
				}
			}
		}

		// Token: 0x060047F4 RID: 18420 RVA: 0x0012909C File Offset: 0x0012729C
		public override void OnClientSceneChanged(NetworkConnection conn)
		{
			string networkSceneName = NetworkManager.networkSceneName;
			List<string> list = new List<string>();
			bool flag = false;
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				string name = SceneManager.GetSceneAt(i).name;
				list.Add(name);
				if (name == networkSceneName)
				{
					flag = true;
				}
			}
			Debug.Log("OnClientSceneChanged networkSceneName=" + networkSceneName + " loadedScenes=" + string.Join(", ", list));
			if (!flag)
			{
				Debug.Log("OnClientSceneChanged skipped: scene specified by networkSceneName is not loaded.");
				return;
			}
			base.autoCreatePlayer = false;
			base.OnClientSceneChanged(conn);
			this.ClientSetPlayers(conn);
			FadeToBlackManager.ForceFullBlack();
		}

		// Token: 0x060047F5 RID: 18421 RVA: 0x00129134 File Offset: 0x00127334
		private void ClientSendAuth(NetworkConnection conn)
		{
			ClientAuthData clientAuthData = new ClientAuthData();
			this.PlatformAuth(ref clientAuthData, conn);
			clientAuthData.password = NetworkManagerSystem.cvClPassword.value;
			clientAuthData.version = RoR2Application.GetBuildId();
			clientAuthData.modHash = NetworkModCompatibilityHelper.networkModHash;
			clientAuthData.entitlements = PlatformSystems.entitlementsSystem.BuildEntitlements();
			conn.Send(74, clientAuthData);
		}

		// Token: 0x060047F6 RID: 18422
		protected abstract void PlatformAuth(ref ClientAuthData data, NetworkConnection conn);

		// Token: 0x060047F7 RID: 18423 RVA: 0x00129190 File Offset: 0x00127390
		protected void ClientSetPlayers(NetworkConnection conn)
		{
			ReadOnlyCollection<LocalUser> readOnlyLocalUsersList = LocalUserManager.readOnlyLocalUsersList;
			for (int i = 0; i < readOnlyLocalUsersList.Count; i++)
			{
				this.ClientAddPlayer((short)readOnlyLocalUsersList[i].id, conn);
			}
		}

		// Token: 0x060047F8 RID: 18424 RVA: 0x001291C8 File Offset: 0x001273C8
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void ClientInit()
		{
			SceneCatalog.onMostRecentSceneDefChanged += NetworkManagerSystem.ClientUpdateOfflineScene;
		}

		// Token: 0x060047F9 RID: 18425 RVA: 0x001291DB File Offset: 0x001273DB
		private static void ClientUpdateOfflineScene(SceneDef sceneDef)
		{
			if (NetworkManagerSystem.singleton && sceneDef.isOfflineScene)
			{
				NetworkManagerSystem.singleton.offlineScene = sceneDef.cachedName;
			}
		}

		// Token: 0x060047FA RID: 18426 RVA: 0x00129201 File Offset: 0x00127401
		protected static void EnsureNetworkManagerNotBusy()
		{
			if (!NetworkManagerSystem.singleton)
			{
				return;
			}
			if (NetworkManagerSystem.singleton.serverShuttingDown || NetworkManagerSystem.isLoadingScene)
			{
				throw new ConCommandException("NetworkManager is busy and cannot receive commands.");
			}
		}

		// Token: 0x060047FB RID: 18427 RVA: 0x0012922E File Offset: 0x0012742E
		[ConCommand(commandName = "client_set_players", flags = ConVarFlags.None, helpText = "Adds network players for all local players. Debug only.")]
		private static void CCClientSetPlayers(ConCommandArgs args)
		{
			if (NetworkManagerSystem.singleton)
			{
				NetworkManagerSystem.singleton.PlatformClientSetPlayers(args);
			}
		}

		// Token: 0x060047FC RID: 18428
		protected abstract void PlatformClientSetPlayers(ConCommandArgs args);

		// Token: 0x060047FD RID: 18429 RVA: 0x00129248 File Offset: 0x00127448
		[ConCommand(commandName = "ping", flags = ConVarFlags.None, helpText = "Prints the current round trip time from this client to the server and back.")]
		private static void CCPing(ConCommandArgs args)
		{
			if (NetworkManagerSystem.singleton)
			{
				NetworkManagerSystem singleton = NetworkManagerSystem.singleton;
				NetworkConnection networkConnection;
				if (singleton == null)
				{
					networkConnection = null;
				}
				else
				{
					NetworkClient client = singleton.client;
					networkConnection = ((client != null) ? client.connection : null);
				}
				NetworkConnection networkConnection2 = networkConnection;
				if (networkConnection2 != null)
				{
					Debug.LogFormat("rtt={0}ms smoothedFrame={1} smoothedFixed={2}", new object[]
					{
						RttManager.GetConnectionRTTInMilliseconds(networkConnection2),
						RttManager.GetConnectionFrameSmoothedRtt(networkConnection2),
						RttManager.GetConnectionFixedSmoothedRtt(networkConnection2)
					});
					return;
				}
				Debug.Log("No connection to server.");
			}
		}

		// Token: 0x060047FE RID: 18430 RVA: 0x001292C8 File Offset: 0x001274C8
		[ConCommand(commandName = "set_scene", flags = ConVarFlags.None, helpText = "Changes to the named scene.")]
		private static void CCSetScene(ConCommandArgs args)
		{
			string argString = args.GetArgString(0);
			if (!NetworkManagerSystem.singleton)
			{
				throw new ConCommandException("set_scene failed: NetworkManagerSystem is not available.");
			}
			SceneCatalog.GetSceneDefForCurrentScene();
			SceneDef sceneDefFromSceneName = SceneCatalog.GetSceneDefFromSceneName(argString);
			if (!sceneDefFromSceneName)
			{
				throw new ConCommandException("\"" + argString + "\" is not a valid scene.");
			}
			bool boolValue = Console.CheatsConVar.instance.boolValue;
			if (NetworkManager.singleton)
			{
				bool isNetworkActive = NetworkManager.singleton.isNetworkActive;
			}
			if (NetworkManager.singleton.isNetworkActive)
			{
				if (sceneDefFromSceneName.isOfflineScene)
				{
					throw new ConCommandException("Cannot switch to scene \"" + argString + "\": Cannot switch to offline-only scene while in a network session.");
				}
				if (!boolValue)
				{
					throw new ConCommandException("Cannot switch to scene \"" + argString + "\": Cheats must be enabled to switch between online-only scenes.");
				}
			}
			else if (!sceneDefFromSceneName.isOfflineScene)
			{
				throw new ConCommandException("Cannot switch to scene \"" + argString + "\": Cannot switch to online-only scene while not in a network session.");
			}
			if (NetworkServer.active)
			{
				Debug.LogFormat("Setting server scene to {0}", new object[]
				{
					argString
				});
				NetworkManagerSystem.singleton.ServerChangeScene(argString);
				return;
			}
			if (!NetworkClient.active)
			{
				Debug.LogFormat("Setting offline scene to {0}", new object[]
				{
					argString
				});
				NetworkManagerSystem.singleton.ServerChangeScene(argString);
				return;
			}
			throw new ConCommandException("Cannot change scene while connected to a remote server.");
		}

		// Token: 0x060047FF RID: 18431 RVA: 0x00129400 File Offset: 0x00127600
		[ConCommand(commandName = "scene_list", flags = ConVarFlags.None, helpText = "Prints a list of all available scene names.")]
		private static void CCSceneList(ConCommandArgs args)
		{
			if (NetworkManagerSystem.singleton)
			{
				string[] array = new string[SceneManager.sceneCountInBuildSettings];
				for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
				{
					array[i] = string.Format("[{0}]={1}", i, SceneUtility.GetScenePathByBuildIndex(i));
				}
				Debug.Log(string.Join("\n", array));
			}
		}

		// Token: 0x06004800 RID: 18432 RVA: 0x00129460 File Offset: 0x00127660
		[ConCommand(commandName = "dump_network_ids", flags = ConVarFlags.None, helpText = "Lists the network ids of all currently networked game objects.")]
		private static void CCDumpNetworkIDs(ConCommandArgs args)
		{
			if (NetworkManagerSystem.singleton)
			{
				List<NetworkIdentity> list = new List<NetworkIdentity>(UnityEngine.Object.FindObjectsOfType<NetworkIdentity>());
				Debug.Log(string.Format("Found {0} NetworkIdentity components", list.Count));
				list.Sort((NetworkIdentity lhs, NetworkIdentity rhs) => (int)(lhs.netId.Value - rhs.netId.Value));
				for (int i = 0; i < list.Count; i++)
				{
					Debug.LogFormat("{0}={1}", new object[]
					{
						list[i].netId.Value,
						list[i].gameObject.name
					});
				}
			}
		}

		// Token: 0x06004801 RID: 18433 RVA: 0x00129517 File Offset: 0x00127717
		[ConCommand(commandName = "disconnect", flags = ConVarFlags.None, helpText = "Disconnect from a server or shut down the current server.")]
		private static void CCDisconnect(ConCommandArgs args)
		{
			if (NetworkManagerSystem.singleton)
			{
				NetworkManagerSystem.singleton.PlatformDisconnect(args);
			}
		}

		// Token: 0x06004802 RID: 18434
		protected abstract void PlatformDisconnect(ConCommandArgs args);

		// Token: 0x06004803 RID: 18435 RVA: 0x00129530 File Offset: 0x00127730
		protected void Disconnect()
		{
			if (this.serverShuttingDown)
			{
				return;
			}
			if (NetworkManagerSystem.singleton.isNetworkActive)
			{
				Debug.Log("Network shutting down...");
				if (NetworkServer.active)
				{
					NetworkManagerSystem.singleton.RequestServerShutdown();
					return;
				}
				NetworkManagerSystem.singleton.StopClient();
			}
		}

		// Token: 0x06004804 RID: 18436 RVA: 0x0012956D File Offset: 0x0012776D
		[ConCommand(commandName = "connect", flags = ConVarFlags.None, helpText = "Connect to a server.")]
		private static void CCConnect(ConCommandArgs args)
		{
			if (NetworkManagerSystem.singleton)
			{
				NetworkManagerSystem.singleton.PlatformConnect(args);
			}
		}

		// Token: 0x06004805 RID: 18437
		protected abstract void PlatformConnect(ConCommandArgs args);

		// Token: 0x06004806 RID: 18438 RVA: 0x0012956D File Offset: 0x0012776D
		private static void CCConnectP2P(ConCommandArgs args)
		{
			if (NetworkManagerSystem.singleton)
			{
				NetworkManagerSystem.singleton.PlatformConnect(args);
			}
		}

		// Token: 0x06004807 RID: 18439
		protected abstract void PlatformConnectP2P(ConCommandArgs args);

		// Token: 0x06004808 RID: 18440 RVA: 0x00129586 File Offset: 0x00127786
		[ConCommand(commandName = "host", flags = ConVarFlags.None, helpText = "Host a server. First argument is whether or not to listen for incoming connections.")]
		private static void CCHost(ConCommandArgs args)
		{
			if (NetworkManagerSystem.singleton)
			{
				NetworkManagerSystem.singleton.PlatformHost(args);
			}
		}

		// Token: 0x06004809 RID: 18441
		protected abstract void PlatformHost(ConCommandArgs args);

		// Token: 0x0600480A RID: 18442 RVA: 0x0012959F File Offset: 0x0012779F
		[ConCommand(commandName = "steam_get_p2p_session_state")]
		private static void CCSGetP2PSessionState(ConCommandArgs args)
		{
			if (NetworkManagerSystem.singleton)
			{
				NetworkManagerSystem.singleton.PlatformGetP2PSessionState(args);
			}
		}

		// Token: 0x0600480B RID: 18443
		protected abstract void PlatformGetP2PSessionState(ConCommandArgs args);

		// Token: 0x0600480C RID: 18444 RVA: 0x001295B8 File Offset: 0x001277B8
		[ConCommand(commandName = "kick_steam", flags = ConVarFlags.SenderMustBeServer, helpText = "Kicks the user with the specified steam id from the server.")]
		private static void CCKickSteam(ConCommandArgs args)
		{
			if (NetworkManagerSystem.singleton)
			{
				NetworkManagerSystem.singleton.PlatformKick(args);
			}
		}

		// Token: 0x0600480D RID: 18445
		protected abstract void PlatformKick(ConCommandArgs args);

		// Token: 0x0600480E RID: 18446 RVA: 0x001295D1 File Offset: 0x001277D1
		[ConCommand(commandName = "ban_steam", flags = ConVarFlags.SenderMustBeServer, helpText = "Bans the user with the specified steam id from the server.")]
		private static void CCBan(ConCommandArgs args)
		{
			if (NetworkManagerSystem.singleton)
			{
				NetworkManagerSystem.singleton.PlatformBan(args);
			}
		}

		// Token: 0x0600480F RID: 18447
		protected abstract void PlatformBan(ConCommandArgs args);

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06004810 RID: 18448 RVA: 0x001295EA File Offset: 0x001277EA
		// (set) Token: 0x06004811 RID: 18449 RVA: 0x001295F2 File Offset: 0x001277F2
		public bool isHost { get; private set; }

		// Token: 0x140000ED RID: 237
		// (add) Token: 0x06004812 RID: 18450 RVA: 0x001295FC File Offset: 0x001277FC
		// (remove) Token: 0x06004813 RID: 18451 RVA: 0x00129630 File Offset: 0x00127830
		public static event Action onStartHostGlobal;

		// Token: 0x140000EE RID: 238
		// (add) Token: 0x06004814 RID: 18452 RVA: 0x00129664 File Offset: 0x00127864
		// (remove) Token: 0x06004815 RID: 18453 RVA: 0x00129698 File Offset: 0x00127898
		public static event Action onStopHostGlobal;

		// Token: 0x06004816 RID: 18454 RVA: 0x001296CB File Offset: 0x001278CB
		public virtual bool IsHost()
		{
			return this.isHost;
		}

		// Token: 0x06004817 RID: 18455 RVA: 0x001296D3 File Offset: 0x001278D3
		public override void OnStartHost()
		{
			base.OnStartHost();
			this.isHost = true;
			Action action = NetworkManagerSystem.onStartHostGlobal;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06004818 RID: 18456 RVA: 0x001296F1 File Offset: 0x001278F1
		public override void OnStopHost()
		{
			Action action = NetworkManagerSystem.onStopHostGlobal;
			if (action != null)
			{
				action();
			}
			this.isHost = false;
			base.OnStopHost();
		}

		// Token: 0x06004819 RID: 18457 RVA: 0x00129710 File Offset: 0x00127910
		[NetworkMessageHandler(client = true, server = false, msgType = 67)]
		private static void HandleKick(NetworkMessage netMsg)
		{
			NetworkManagerSystem.KickMessage kickMessage = netMsg.ReadMessage<NetworkManagerSystem.KickMessage>();
			Debug.LogFormat("Received kick message. Reason={0}", new object[]
			{
				kickMessage.kickReason
			});
			NetworkManagerSystem.singleton.StopClient();
			string text;
			object[] formatParams;
			kickMessage.TryGetDisplayTokenAndFormatParams(out text, out formatParams);
			SimpleDialogBox simpleDialogBox = SimpleDialogBox.Create(null);
			simpleDialogBox.headerToken = new SimpleDialogBox.TokenParamsPair("DISCONNECTED", Array.Empty<object>());
			simpleDialogBox.descriptionToken = new SimpleDialogBox.TokenParamsPair(text ?? string.Empty, formatParams);
			simpleDialogBox.AddCancelButton(CommonLanguageTokens.ok, Array.Empty<object>());
			simpleDialogBox.rootObject.transform.SetParent(RoR2Application.instance.mainCanvas.transform);
		}

		// Token: 0x0600481A RID: 18458 RVA: 0x001297B4 File Offset: 0x001279B4
		public static void HandleKick(string displayToken)
		{
			NetworkManagerSystem.singleton.StopClient();
			SimpleDialogBox simpleDialogBox = SimpleDialogBox.Create(null);
			simpleDialogBox.headerToken = new SimpleDialogBox.TokenParamsPair("DISCONNECTED", Array.Empty<object>());
			simpleDialogBox.descriptionToken = new SimpleDialogBox.TokenParamsPair(displayToken, Array.Empty<object>());
			simpleDialogBox.AddCancelButton(CommonLanguageTokens.ok, Array.Empty<object>());
			simpleDialogBox.rootObject.transform.SetParent(RoR2Application.instance.mainCanvas.transform);
		}

		// Token: 0x0600481B RID: 18459 RVA: 0x00129828 File Offset: 0x00127A28
		[NetworkMessageHandler(msgType = 54, client = true)]
		private static void HandleUpdateTime(NetworkMessage netMsg)
		{
			float num = netMsg.reader.ReadSingle();
			NetworkManagerSystem.singleton._unpredictedServerFixedTime = num;
			float num2 = Time.time - Time.fixedTime;
			NetworkManagerSystem.singleton._unpredictedServerFrameTime = num + num2;
		}

		// Token: 0x0600481C RID: 18460 RVA: 0x00129868 File Offset: 0x00127A68
		[NetworkMessageHandler(msgType = 64, client = true, server = true)]
		private static void HandleTest(NetworkMessage netMsg)
		{
			int num = netMsg.reader.ReadInt32();
			Debug.LogFormat("Received test packet. value={0}", new object[]
			{
				num
			});
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600481D RID: 18461 RVA: 0x0012989A File Offset: 0x00127A9A
		// (set) Token: 0x0600481E RID: 18462 RVA: 0x001298A2 File Offset: 0x00127AA2
		public CSteamID clientP2PId { get; protected set; } = CSteamID.nil;

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600481F RID: 18463 RVA: 0x001298AB File Offset: 0x00127AAB
		// (set) Token: 0x06004820 RID: 18464 RVA: 0x001298B3 File Offset: 0x00127AB3
		public CSteamID serverP2PId { get; protected set; } = CSteamID.nil;

		// Token: 0x140000EF RID: 239
		// (add) Token: 0x06004821 RID: 18465 RVA: 0x001298BC File Offset: 0x00127ABC
		// (remove) Token: 0x06004822 RID: 18466 RVA: 0x001298F0 File Offset: 0x00127AF0
		public static event Action onStartServerGlobal;

		// Token: 0x140000F0 RID: 240
		// (add) Token: 0x06004823 RID: 18467 RVA: 0x00129924 File Offset: 0x00127B24
		// (remove) Token: 0x06004824 RID: 18468 RVA: 0x00129958 File Offset: 0x00127B58
		public static event Action onStopServerGlobal;

		// Token: 0x140000F1 RID: 241
		// (add) Token: 0x06004825 RID: 18469 RVA: 0x0012998C File Offset: 0x00127B8C
		// (remove) Token: 0x06004826 RID: 18470 RVA: 0x001299C0 File Offset: 0x00127BC0
		public static event Action<NetworkConnection> onServerConnectGlobal;

		// Token: 0x140000F2 RID: 242
		// (add) Token: 0x06004827 RID: 18471 RVA: 0x001299F4 File Offset: 0x00127BF4
		// (remove) Token: 0x06004828 RID: 18472 RVA: 0x00129A28 File Offset: 0x00127C28
		public static event Action<NetworkConnection> onServerDisconnectGlobal;

		// Token: 0x140000F3 RID: 243
		// (add) Token: 0x06004829 RID: 18473 RVA: 0x00129A5C File Offset: 0x00127C5C
		// (remove) Token: 0x0600482A RID: 18474 RVA: 0x00129A90 File Offset: 0x00127C90
		public static event Action<string> onServerSceneChangedGlobal;

		// Token: 0x0600482B RID: 18475
		public abstract NetworkConnection GetClient(UserID clientId);

		// Token: 0x0600482C RID: 18476 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void InitPlatformServer()
		{
		}

		// Token: 0x0600482D RID: 18477 RVA: 0x00129AC3 File Offset: 0x00127CC3
		public override void OnStartServer()
		{
			base.OnStartServer();
			NetworkMessageHandlerAttribute.RegisterServerMessages();
			this.InitializeTime();
			this.serverNetworkSessionInstance = UnityEngine.Object.Instantiate<GameObject>(RoR2Application.instance.networkSessionPrefab);
			this.InitPlatformServer();
			Action action = NetworkManagerSystem.onStartServerGlobal;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600482E RID: 18478 RVA: 0x00129B00 File Offset: 0x00127D00
		public void FireStartServerGlobalEvent()
		{
			Action action = NetworkManagerSystem.onStartServerGlobal;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x00129B11 File Offset: 0x00127D11
		public override void OnStopServer()
		{
			base.OnStopServer();
		}

		// Token: 0x06004830 RID: 18480 RVA: 0x00129B19 File Offset: 0x00127D19
		public void FireStopServerGlobalEvent()
		{
			Action action = NetworkManagerSystem.onStopServerGlobal;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06004831 RID: 18481 RVA: 0x00129B2A File Offset: 0x00127D2A
		public override void OnServerConnect(NetworkConnection conn)
		{
			base.OnServerConnect(conn);
		}

		// Token: 0x06004832 RID: 18482 RVA: 0x00129B33 File Offset: 0x00127D33
		public void FireServerConnectGlobalEvent(NetworkConnection conn)
		{
			Action<NetworkConnection> action = NetworkManagerSystem.onServerConnectGlobal;
			if (action == null)
			{
				return;
			}
			action(conn);
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x00129B45 File Offset: 0x00127D45
		public override void OnServerDisconnect(NetworkConnection conn)
		{
			base.OnServerDisconnect(conn);
		}

		// Token: 0x06004834 RID: 18484 RVA: 0x00129B4E File Offset: 0x00127D4E
		public void FireServerDisconnectGlobalEvent(NetworkConnection conn)
		{
			Action<NetworkConnection> action = NetworkManagerSystem.onServerDisconnectGlobal;
			if (action == null)
			{
				return;
			}
			action(conn);
		}

		// Token: 0x06004835 RID: 18485
		public abstract void ServerHandleClientDisconnect(NetworkConnection conn);

		// Token: 0x06004836 RID: 18486
		public abstract void ServerBanClient(NetworkConnection conn);

		// Token: 0x06004837 RID: 18487 RVA: 0x00129B60 File Offset: 0x00127D60
		public void ServerKickClient(NetworkConnection conn, NetworkManagerSystem.BaseKickReason reason)
		{
			Debug.LogFormat("Kicking client on connection {0}: Reason {1}", new object[]
			{
				conn.connectionId,
				reason
			});
			conn.SendByChannel(67, new NetworkManagerSystem.KickMessage(reason), QosChannelIndex.defaultReliable.intVal);
			conn.FlushChannels();
			this.KickClient(conn, reason);
		}

		// Token: 0x06004838 RID: 18488
		protected abstract void KickClient(NetworkConnection conn, NetworkManagerSystem.BaseKickReason reason);

		// Token: 0x06004839 RID: 18489 RVA: 0x00129BB6 File Offset: 0x00127DB6
		public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
		{
			this.OnServerAddPlayer(conn, playerControllerId, null);
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x00129BC1 File Offset: 0x00127DC1
		public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
		{
			this.OnServerAddPlayerInternal(conn, playerControllerId, extraMessageReader);
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x00129BCC File Offset: 0x00127DCC
		private void OnServerAddPlayerInternal(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
		{
			if (base.playerPrefab == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("The PlayerPrefab is empty on the NetworkManager. Please setup a PlayerPrefab object.");
				}
				return;
			}
			if (base.playerPrefab.GetComponent<NetworkIdentity>() == null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("The PlayerPrefab does not have a NetworkIdentity. Please add a NetworkIdentity to the player prefab.");
				}
				return;
			}
			if ((int)playerControllerId < conn.playerControllers.Count && conn.playerControllers[(int)playerControllerId].IsValid && conn.playerControllers[(int)playerControllerId].gameObject != null)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("There is already a player at that playerControllerId for this connections.");
				}
				return;
			}
			if (NetworkUser.readOnlyInstancesList.Count >= base.maxConnections)
			{
				if (LogFilter.logError)
				{
					Debug.LogError("Cannot add any more players.)");
				}
				return;
			}
			if (extraMessageReader == null)
			{
				extraMessageReader = NetworkManagerSystem.DefaultNetworkReader;
			}
			NetworkManagerSystem.AddPlayerMessage message = extraMessageReader.ReadMessage<NetworkManagerSystem.AddPlayerMessage>();
			Transform startPosition = base.GetStartPosition();
			GameObject gameObject;
			if (startPosition != null)
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(base.playerPrefab, startPosition.position, startPosition.rotation);
			}
			else
			{
				gameObject = UnityEngine.Object.Instantiate<GameObject>(base.playerPrefab, Vector3.zero, Quaternion.identity);
			}
			Debug.LogFormat("NetworkManagerSystem.AddPlayerInternal(conn={0}, playerControllerId={1}, extraMessageReader={2}", new object[]
			{
				conn,
				playerControllerId,
				extraMessageReader
			});
			NetworkUser component = gameObject.GetComponent<NetworkUser>();
			Util.ConnectionIsLocal(conn);
			component.id = this.AddPlayerIdFromPlatform(conn, message, (byte)playerControllerId);
			Chat.SendPlayerConnectedMessage(component);
			NetworkServer.AddPlayerForConnection(conn, gameObject, playerControllerId);
		}

		// Token: 0x0600483C RID: 18492
		protected abstract NetworkUserId AddPlayerIdFromPlatform(NetworkConnection conn, NetworkManagerSystem.AddPlayerMessage message, byte playerControllerId);

		// Token: 0x0600483D RID: 18493
		protected abstract void UpdateServer();

		// Token: 0x0600483E RID: 18494 RVA: 0x00129D2C File Offset: 0x00127F2C
		private void FixedUpdateServer()
		{
			if (!NetworkServer.active)
			{
				return;
			}
			this.timeTransmitTimer -= Time.fixedDeltaTime;
			if (this.timeTransmitTimer <= 0f)
			{
				NetworkWriter networkWriter = new NetworkWriter();
				networkWriter.StartMessage(54);
				networkWriter.Write(this.unpredictedServerFixedTime);
				networkWriter.FinishMessage();
				NetworkServer.SendWriterToReady(null, networkWriter, QosChannelIndex.time.intVal);
				this.timeTransmitTimer += NetworkManagerSystem.svTimeTransmitInterval.value;
			}
			foreach (NetworkConnection networkConnection in NetworkServer.connections)
			{
				if (networkConnection != null && !Util.ConnectionIsLocal(networkConnection))
				{
					RttManager.Ping(networkConnection, QosChannelIndex.ping.intVal);
				}
			}
		}

		// Token: 0x0600483F RID: 18495 RVA: 0x00129DFC File Offset: 0x00127FFC
		public override void OnServerSceneChanged(string sceneName)
		{
			base.OnServerSceneChanged(sceneName);
			if (Run.instance)
			{
				Run.instance.OnServerSceneChanged(sceneName);
			}
			Action<string> action = NetworkManagerSystem.onServerSceneChangedGlobal;
			if (action != null)
			{
				action(sceneName);
			}
			while (NetworkManagerSystem.clientsReadyDuringLevelTransition.Count > 0)
			{
				NetworkConnection networkConnection = NetworkManagerSystem.clientsReadyDuringLevelTransition.Dequeue();
				try
				{
					if (networkConnection.isConnected)
					{
						this.OnServerReady(networkConnection);
					}
				}
				catch (Exception ex)
				{
					Debug.LogErrorFormat("OnServerReady could not be called for client: {0}", new object[]
					{
						ex.Message
					});
				}
			}
		}

		// Token: 0x06004840 RID: 18496 RVA: 0x00129E90 File Offset: 0x00128090
		protected bool IsServerAtMaxConnections()
		{
			ReadOnlyCollection<NetworkConnection> connections = NetworkServer.connections;
			if (connections.Count >= base.maxConnections)
			{
				int num = 0;
				for (int i = 0; i < connections.Count; i++)
				{
					if (connections[i] != null)
					{
						num++;
					}
				}
				return num >= base.maxConnections;
			}
			return false;
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x00129EE0 File Offset: 0x001280E0
		private NetworkUser FindNetworkUserForConnectionServer(NetworkConnection connection)
		{
			ReadOnlyCollection<NetworkUser> readOnlyInstancesList = NetworkUser.readOnlyInstancesList;
			int count = readOnlyInstancesList.Count;
			for (int i = 0; i < count; i++)
			{
				NetworkUser networkUser = readOnlyInstancesList[i];
				if (networkUser.connectionToClient == connection)
				{
					return networkUser;
				}
			}
			return null;
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x00129F1C File Offset: 0x0012811C
		public int GetConnectingClientCount()
		{
			int num = 0;
			ReadOnlyCollection<NetworkConnection> connections = NetworkServer.connections;
			int count = connections.Count;
			for (int i = 0; i < count; i++)
			{
				NetworkConnection networkConnection = connections[i];
				if (networkConnection != null && !this.FindNetworkUserForConnectionServer(networkConnection))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x00129F65 File Offset: 0x00128165
		public void RequestServerShutdown()
		{
			if (this.serverShuttingDown)
			{
				return;
			}
			this.serverShuttingDown = true;
			base.StartCoroutine(this.ServerShutdownCoroutine());
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x00129F84 File Offset: 0x00128184
		private IEnumerator ServerShutdownCoroutine()
		{
			Debug.Log("Server shutting down...");
			ReadOnlyCollection<NetworkConnection> connections = NetworkServer.connections;
			for (int i = connections.Count - 1; i >= 0; i--)
			{
				NetworkConnection networkConnection = connections[i];
				if (networkConnection != null && !Util.ConnectionIsLocal(networkConnection))
				{
					this.ServerKickClient(networkConnection, new NetworkManagerSystem.SimpleLocalizedKickReason("KICK_REASON_SERVERSHUTDOWN", Array.Empty<string>()));
				}
			}
			Debug.Log("Issued kick message to all remote clients.");
			float maxWait = 0.2f;
			float t = 0f;
			while (t < maxWait && !NetworkManagerSystem.<ServerShutdownCoroutine>g__CheckConnectionsEmpty|205_0())
			{
				yield return new WaitForEndOfFrame();
				t += Time.unscaledDeltaTime;
			}
			Debug.Log("Finished waiting for clients to disconnect.");
			if (this.client != null)
			{
				Debug.Log("StopHost()");
				base.StopHost();
			}
			else
			{
				Debug.Log("StopServer()");
				base.StopServer();
			}
			this.serverShuttingDown = false;
			Debug.Log("Server shutdown complete.");
			yield break;
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x00129F93 File Offset: 0x00128193
		private static void ServerHandleReady(NetworkMessage netMsg)
		{
			if (NetworkManagerSystem.isLoadingScene)
			{
				NetworkManagerSystem.clientsReadyDuringLevelTransition.Enqueue(netMsg.conn);
				Debug.Log("Client readied during a level transition! Queuing their request.");
				return;
			}
			NetworkManagerSystem.singleton.OnServerReady(netMsg.conn);
			Debug.Log("Client ready.");
		}

		// Token: 0x06004846 RID: 18502 RVA: 0x00129FD1 File Offset: 0x001281D1
		private void RegisterServerOverrideMessages()
		{
			NetworkServer.RegisterHandler(35, new NetworkMessageDelegate(NetworkManagerSystem.ServerHandleReady));
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x00129FE6 File Offset: 0x001281E6
		public override void ServerChangeScene(string newSceneName)
		{
			this.RegisterServerOverrideMessages();
			base.ServerChangeScene(newSceneName);
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x00129FF8 File Offset: 0x001281F8
		private static bool IsAddressablesKeyValid(string key, Type type)
		{
			using (IEnumerator<IResourceLocator> enumerator = Addressables.ResourceLocators.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IList<IResourceLocation> list;
					if (enumerator.Current.Locate(key, typeof(SceneInstance), out list) && list.Count > 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004849 RID: 18505 RVA: 0x0012A060 File Offset: 0x00128260
		protected override NetworkManager.IChangeSceneAsyncOperation ChangeSceneImplementation(string newSceneName)
		{
			NetworkManager.IChangeSceneAsyncOperation changeSceneAsyncOperation = null;
			StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
			NetworkManager.IChangeSceneAsyncOperation result;
			try
			{
				SceneDef sceneDef = SceneCatalog.FindSceneDef(newSceneName);
				if (sceneDef)
				{
					AssetReferenceScene sceneAddress = sceneDef.sceneAddress;
					string text = (sceneAddress != null) ? sceneAddress.AssetGUID : null;
					if (!string.IsNullOrEmpty(text))
					{
						if (NetworkManagerSystem.IsAddressablesKeyValid(text, typeof(SceneInstance)))
						{
							changeSceneAsyncOperation = new NetworkManagerSystem.AddressablesChangeSceneAsyncOperation(text, LoadSceneMode.Single, false);
						}
						else
						{
							stringBuilder.AppendLine("Scene address is invalid. sceneName=\"").Append(newSceneName).Append("\" sceneAddress=\"").Append(text).Append("\"").AppendLine();
						}
					}
				}
				if (changeSceneAsyncOperation == null)
				{
					changeSceneAsyncOperation = base.ChangeSceneImplementation(newSceneName);
					if (changeSceneAsyncOperation == null)
					{
						stringBuilder.Append("SceneManager.LoadSceneAsync(\"").Append(newSceneName).Append("\" failed.").AppendLine();
					}
				}
				result = changeSceneAsyncOperation;
			}
			finally
			{
				if (changeSceneAsyncOperation == null)
				{
					Debug.LogError(stringBuilder.ToString());
				}
				HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
			}
			return result;
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x0012A178 File Offset: 0x00128378
		[CompilerGenerated]
		internal static void <OnStartClient>g__RegisterPrefabs|69_0(GameObject[] prefabs)
		{
			for (int i = 0; i < prefabs.Length; i++)
			{
				ClientScene.RegisterPrefab(prefabs[i]);
			}
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x0012A1A0 File Offset: 0x001283A0
		[CompilerGenerated]
		internal static void <OnStartClient>g__RegisterPrefabsFromComponents|69_1(Component[] prefabComponents)
		{
			for (int i = 0; i < prefabComponents.Length; i++)
			{
				ClientScene.RegisterPrefab(prefabComponents[i].gameObject);
			}
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x0012A1CC File Offset: 0x001283CC
		[CompilerGenerated]
		internal static bool <ServerShutdownCoroutine>g__CheckConnectionsEmpty|205_0()
		{
			foreach (NetworkConnection networkConnection in NetworkServer.connections)
			{
				if (networkConnection != null && !Util.ConnectionIsLocal(networkConnection))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04004528 RID: 17704
		protected static readonly FieldInfo loadingSceneAsyncFieldInfo;

		// Token: 0x04004529 RID: 17705
		protected float _unpredictedServerFixedTime;

		// Token: 0x0400452A RID: 17706
		protected float _unpredictedServerFixedTimeSmoothed;

		// Token: 0x0400452B RID: 17707
		protected float unpredictedServerFixedTimeVelocity;

		// Token: 0x0400452C RID: 17708
		protected float _unpredictedServerFrameTime;

		// Token: 0x0400452D RID: 17709
		protected float _unpredictedServerFrameTimeSmoothed;

		// Token: 0x0400452E RID: 17710
		protected float unpredictedServerFrameTimeVelocity;

		// Token: 0x0400452F RID: 17711
		protected static FloatConVar cvNetTimeSmoothRate = new FloatConVar("net_time_smooth_rate", ConVarFlags.None, "1.05", "The smoothing rate for the network time.");

		// Token: 0x04004531 RID: 17713
		public float debugServerTime;

		// Token: 0x04004532 RID: 17714
		public float debugRTT;

		// Token: 0x04004533 RID: 17715
		protected bool isSinglePlayer;

		// Token: 0x04004534 RID: 17716
		protected bool actedUponDesiredHost;

		// Token: 0x04004535 RID: 17717
		protected float lastDesiredHostSetTime = float.NegativeInfinity;

		// Token: 0x04004536 RID: 17718
		protected HostDescription _desiredHost;

		// Token: 0x0400453C RID: 17724
		private static bool wasFading = false;

		// Token: 0x04004541 RID: 17729
		protected static readonly string[] sceneWhiteList = new string[]
		{
			"title",
			"crystalworld",
			"logbook"
		};

		// Token: 0x04004542 RID: 17730
		public static readonly StringConVar cvClPassword = new StringConVar("cl_password", ConVarFlags.None, "", "The password to use when joining a passworded server.");

		// Token: 0x0400454D RID: 17741
		protected GameObject serverNetworkSessionInstance;

		// Token: 0x0400454E RID: 17742
		private static NetworkReader DefaultNetworkReader = new NetworkReader();

		// Token: 0x0400454F RID: 17743
		private static readonly FloatConVar svTimeTransmitInterval = new FloatConVar("sv_time_transmit_interval", ConVarFlags.Cheat, 0.016666668f.ToString(), "How long it takes for the server to issue a time update to clients.");

		// Token: 0x04004550 RID: 17744
		private float timeTransmitTimer;

		// Token: 0x04004551 RID: 17745
		protected bool serverShuttingDown;

		// Token: 0x04004552 RID: 17746
		private static readonly Queue<NetworkConnection> clientsReadyDuringLevelTransition = new Queue<NetworkConnection>();

		// Token: 0x04004553 RID: 17747
		public static readonly StringConVar cvSvCustomTags = new StringConVar("sv_custom_tags", ConVarFlags.None, "", "Comma-delimited custom tags to report to the server browser.");

		// Token: 0x02000C5D RID: 3165
		private class NetLogLevelConVar : BaseConVar
		{
			// Token: 0x0600484E RID: 18510 RVA: 0x00009F73 File Offset: 0x00008173
			public NetLogLevelConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x0600484F RID: 18511 RVA: 0x0012A224 File Offset: 0x00128424
			public override void SetString(string newValue)
			{
				int currentLogLevel;
				if (TextSerialization.TryParseInvariant(newValue, out currentLogLevel))
				{
					LogFilter.currentLogLevel = currentLogLevel;
				}
			}

			// Token: 0x06004850 RID: 18512 RVA: 0x0012A241 File Offset: 0x00128441
			public override string GetString()
			{
				return TextSerialization.ToStringInvariant(LogFilter.currentLogLevel);
			}

			// Token: 0x04004554 RID: 17748
			private static NetworkManagerSystem.NetLogLevelConVar cvNetLogLevel = new NetworkManagerSystem.NetLogLevelConVar("net_loglevel", ConVarFlags.Engine, null, "Network log verbosity.");
		}

		// Token: 0x02000C5E RID: 3166
		private class SvListenConVar : BaseConVar
		{
			// Token: 0x06004852 RID: 18514 RVA: 0x00009F73 File Offset: 0x00008173
			public SvListenConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06004853 RID: 18515 RVA: 0x0012A268 File Offset: 0x00128468
			public override void SetString(string newValue)
			{
				if (NetworkServer.active)
				{
					Debug.Log("Can't change value of sv_listen while server is running.");
					return;
				}
				int num;
				if (TextSerialization.TryParseInvariant(newValue, out num))
				{
					NetworkServer.dontListen = (num == 0);
				}
			}

			// Token: 0x06004854 RID: 18516 RVA: 0x0012A29A File Offset: 0x0012849A
			public override string GetString()
			{
				if (!NetworkServer.dontListen)
				{
					return "1";
				}
				return "0";
			}

			// Token: 0x04004555 RID: 17749
			private static NetworkManagerSystem.SvListenConVar cvSvListen = new NetworkManagerSystem.SvListenConVar("sv_listen", ConVarFlags.Engine, null, "Whether or not the server will accept connections from other players.");
		}

		// Token: 0x02000C5F RID: 3167
		public class SvMaxPlayersConVar : BaseConVar
		{
			// Token: 0x06004856 RID: 18518 RVA: 0x00009F73 File Offset: 0x00008173
			public SvMaxPlayersConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06004857 RID: 18519 RVA: 0x0012A2C8 File Offset: 0x001284C8
			public override void SetString(string newValue)
			{
				if (NetworkServer.active)
				{
					throw new ConCommandException("Cannot change this convar while the server is running.");
				}
				int val;
				if (NetworkManager.singleton && TextSerialization.TryParseInvariant(newValue, out val))
				{
					NetworkManager.singleton.maxConnections = Math.Min(Math.Max(val, 1), RoR2Application.hardMaxPlayers);
				}
			}

			// Token: 0x06004858 RID: 18520 RVA: 0x0012A318 File Offset: 0x00128518
			public override string GetString()
			{
				if (!NetworkManager.singleton)
				{
					return "1";
				}
				return TextSerialization.ToStringInvariant(NetworkManager.singleton.maxConnections);
			}

			// Token: 0x1700068E RID: 1678
			// (get) Token: 0x06004859 RID: 18521 RVA: 0x0012A33B File Offset: 0x0012853B
			public int intValue
			{
				get
				{
					return NetworkManager.singleton.maxConnections;
				}
			}

			// Token: 0x04004556 RID: 17750
			public static readonly NetworkManagerSystem.SvMaxPlayersConVar instance = new NetworkManagerSystem.SvMaxPlayersConVar("sv_maxplayers", ConVarFlags.Engine, null, "Maximum number of players allowed.");
		}

		// Token: 0x02000C60 RID: 3168
		private class KickMessage : MessageBase
		{
			// Token: 0x0600485B RID: 18523 RVA: 0x000672CB File Offset: 0x000654CB
			public KickMessage()
			{
			}

			// Token: 0x0600485C RID: 18524 RVA: 0x0012A360 File Offset: 0x00128560
			public KickMessage(NetworkManagerSystem.BaseKickReason kickReason)
			{
				this.kickReason = kickReason;
			}

			// Token: 0x0600485D RID: 18525 RVA: 0x0012A370 File Offset: 0x00128570
			public override void Serialize(NetworkWriter writer)
			{
				base.Serialize(writer);
				NetworkManagerSystem.BaseKickReason baseKickReason = this.kickReason;
				string text = ((baseKickReason != null) ? baseKickReason.GetType().FullName : null) ?? string.Empty;
				writer.Write(text);
				if (text == string.Empty)
				{
					return;
				}
				this.kickReason.Serialize(writer);
			}

			// Token: 0x0600485E RID: 18526 RVA: 0x0012A3C8 File Offset: 0x001285C8
			public override void Deserialize(NetworkReader reader)
			{
				base.Deserialize(reader);
				string typeName = reader.ReadString();
				Type type = null;
				try
				{
					type = Type.GetType(typeName);
				}
				catch
				{
				}
				if (type == null || !typeof(NetworkManagerSystem.BaseKickReason).IsAssignableFrom(type) || type.IsAbstract)
				{
					this.kickReason = null;
					return;
				}
				this.kickReason = (NetworkManagerSystem.BaseKickReason)Activator.CreateInstance(type);
				this.kickReason.Deserialize(reader);
			}

			// Token: 0x0600485F RID: 18527 RVA: 0x0012A44C File Offset: 0x0012864C
			public bool TryGetDisplayTokenAndFormatParams(out string token, out object[] formatArgs)
			{
				if (this.kickReason == null)
				{
					token = null;
					formatArgs = null;
					return false;
				}
				this.kickReason.GetDisplayTokenAndFormatParams(out token, out formatArgs);
				return true;
			}

			// Token: 0x04004557 RID: 17751
			public NetworkManagerSystem.BaseKickReason kickReason;
		}

		// Token: 0x02000C61 RID: 3169
		public abstract class BaseKickReason : MessageBase
		{
			// Token: 0x06004860 RID: 18528 RVA: 0x000672CB File Offset: 0x000654CB
			public BaseKickReason()
			{
			}

			// Token: 0x06004861 RID: 18529
			public abstract void GetDisplayTokenAndFormatParams(out string token, out object[] formatArgs);
		}

		// Token: 0x02000C62 RID: 3170
		public class SimpleLocalizedKickReason : NetworkManagerSystem.BaseKickReason
		{
			// Token: 0x06004862 RID: 18530 RVA: 0x0012A46C File Offset: 0x0012866C
			public SimpleLocalizedKickReason()
			{
			}

			// Token: 0x06004863 RID: 18531 RVA: 0x0012A474 File Offset: 0x00128674
			public SimpleLocalizedKickReason(string baseToken, params string[] formatArgs)
			{
				this.baseToken = baseToken;
				this.formatArgs = formatArgs;
			}

			// Token: 0x06004864 RID: 18532 RVA: 0x0012A48C File Offset: 0x0012868C
			public override void GetDisplayTokenAndFormatParams(out string token, out object[] formatArgs)
			{
				token = this.baseToken;
				object[] array = this.formatArgs;
				formatArgs = array;
			}

			// Token: 0x06004865 RID: 18533 RVA: 0x0012A4AB File Offset: 0x001286AB
			public override void Serialize(NetworkWriter writer)
			{
				writer.Write(this.baseToken);
				GeneratedNetworkCode._WriteArrayString_None(writer, this.formatArgs);
			}

			// Token: 0x06004866 RID: 18534 RVA: 0x0012A4C5 File Offset: 0x001286C5
			public override void Deserialize(NetworkReader reader)
			{
				this.baseToken = reader.ReadString();
				this.formatArgs = GeneratedNetworkCode._ReadArrayString_None(reader);
			}

			// Token: 0x04004558 RID: 17752
			public string baseToken;

			// Token: 0x04004559 RID: 17753
			public string[] formatArgs;
		}

		// Token: 0x02000C63 RID: 3171
		public class ModMismatchKickReason : NetworkManagerSystem.BaseKickReason
		{
			// Token: 0x06004867 RID: 18535 RVA: 0x0012A4DF File Offset: 0x001286DF
			public ModMismatchKickReason()
			{
			}

			// Token: 0x06004868 RID: 18536 RVA: 0x0012A4F2 File Offset: 0x001286F2
			public ModMismatchKickReason(IEnumerable<string> serverModList)
			{
				this.serverModList = serverModList.ToArray<string>();
			}

			// Token: 0x06004869 RID: 18537 RVA: 0x0012A514 File Offset: 0x00128714
			public override void GetDisplayTokenAndFormatParams(out string token, out object[] formatArgs)
			{
				IEnumerable<string> networkModList = NetworkModCompatibilityHelper.networkModList;
				IEnumerable<string> values = networkModList.Except(this.serverModList);
				IEnumerable<string> values2 = this.serverModList.Except(networkModList);
				token = "KICK_REASON_MOD_MISMATCH";
				object[] array = new string[]
				{
					string.Join("\n", values),
					string.Join("\n", values2)
				};
				formatArgs = array;
			}

			// Token: 0x0600486A RID: 18538 RVA: 0x0012A56E File Offset: 0x0012876E
			public override void Serialize(NetworkWriter writer)
			{
				GeneratedNetworkCode._WriteArrayString_None(writer, this.serverModList);
			}

			// Token: 0x0600486B RID: 18539 RVA: 0x0012A57C File Offset: 0x0012877C
			public override void Deserialize(NetworkReader reader)
			{
				this.serverModList = GeneratedNetworkCode._ReadArrayString_None(reader);
			}

			// Token: 0x0400455A RID: 17754
			public string[] serverModList = Array.Empty<string>();
		}

		// Token: 0x02000C64 RID: 3172
		protected class AddPlayerMessage : MessageBase
		{
			// Token: 0x0600486D RID: 18541 RVA: 0x0012A58A File Offset: 0x0012878A
			public override void Serialize(NetworkWriter writer)
			{
				GeneratedNetworkCode._WriteUserID_None(writer, this.id);
				writer.WriteBytesFull(this.steamAuthTicketData);
			}

			// Token: 0x0600486E RID: 18542 RVA: 0x0012A5A4 File Offset: 0x001287A4
			public override void Deserialize(NetworkReader reader)
			{
				this.id = GeneratedNetworkCode._ReadUserID_None(reader);
				this.steamAuthTicketData = reader.ReadBytesAndSize();
			}

			// Token: 0x0400455B RID: 17755
			public UserID id;

			// Token: 0x0400455C RID: 17756
			public byte[] steamAuthTicketData;
		}

		// Token: 0x02000C65 RID: 3173
		public class SvHostNameConVar : BaseConVar
		{
			// Token: 0x140000F4 RID: 244
			// (add) Token: 0x0600486F RID: 18543 RVA: 0x0012A5C0 File Offset: 0x001287C0
			// (remove) Token: 0x06004870 RID: 18544 RVA: 0x0012A5F8 File Offset: 0x001287F8
			public event Action<string> onValueChanged;

			// Token: 0x06004871 RID: 18545 RVA: 0x0012A62D File Offset: 0x0012882D
			public SvHostNameConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06004872 RID: 18546 RVA: 0x0012A645 File Offset: 0x00128845
			public override void SetString(string newValue)
			{
				this.value = newValue;
				Action<string> action = this.onValueChanged;
				if (action == null)
				{
					return;
				}
				action(newValue);
			}

			// Token: 0x06004873 RID: 18547 RVA: 0x0012A65F File Offset: 0x0012885F
			public override string GetString()
			{
				return this.value;
			}

			// Token: 0x0400455D RID: 17757
			public static readonly NetworkManagerSystem.SvHostNameConVar instance = new NetworkManagerSystem.SvHostNameConVar("sv_hostname", ConVarFlags.None, "", "The public name to use for the server if hosting.");

			// Token: 0x0400455E RID: 17758
			private string value = "NAME";
		}

		// Token: 0x02000C66 RID: 3174
		public class SvPortConVar : BaseConVar
		{
			// Token: 0x1700068F RID: 1679
			// (get) Token: 0x06004875 RID: 18549 RVA: 0x0012A683 File Offset: 0x00128883
			public ushort value
			{
				get
				{
					if (!NetworkManagerSystem.singleton)
					{
						return 0;
					}
					return (ushort)NetworkManagerSystem.singleton.networkPort;
				}
			}

			// Token: 0x06004876 RID: 18550 RVA: 0x00009F73 File Offset: 0x00008173
			public SvPortConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06004877 RID: 18551 RVA: 0x0012A6A0 File Offset: 0x001288A0
			public override void SetString(string newValueString)
			{
				if (NetworkServer.active)
				{
					throw new ConCommandException("Cannot change this convar while the server is running.");
				}
				ushort networkPort;
				if (TextSerialization.TryParseInvariant(newValueString, out networkPort))
				{
					NetworkManagerSystem.singleton.networkPort = (int)networkPort;
				}
			}

			// Token: 0x06004878 RID: 18552 RVA: 0x0012A6D4 File Offset: 0x001288D4
			public override string GetString()
			{
				return this.value.ToString();
			}

			// Token: 0x04004560 RID: 17760
			public static readonly NetworkManagerSystem.SvPortConVar instance = new NetworkManagerSystem.SvPortConVar("sv_port", ConVarFlags.Engine, null, "The port to use for the server if hosting.");
		}

		// Token: 0x02000C67 RID: 3175
		public class SvIPConVar : BaseConVar
		{
			// Token: 0x0600487A RID: 18554 RVA: 0x00009F73 File Offset: 0x00008173
			public SvIPConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x0600487B RID: 18555 RVA: 0x0012A708 File Offset: 0x00128908
			public override void SetString(string newValueString)
			{
				if (NetworkServer.active)
				{
					throw new ConCommandException("Cannot change this convar while the server is running.");
				}
				NetworkManagerSystem.singleton.serverBindAddress = newValueString;
			}

			// Token: 0x0600487C RID: 18556 RVA: 0x0012A727 File Offset: 0x00128927
			public override string GetString()
			{
				if (!NetworkManagerSystem.singleton)
				{
					return string.Empty;
				}
				return NetworkManagerSystem.singleton.serverBindAddress;
			}

			// Token: 0x04004561 RID: 17761
			public static readonly NetworkManagerSystem.SvIPConVar instance = new NetworkManagerSystem.SvIPConVar("sv_ip", ConVarFlags.Engine, null, "The IP for the server to bind to if hosting.");
		}

		// Token: 0x02000C68 RID: 3176
		public class SvPasswordConVar : BaseConVar
		{
			// Token: 0x17000690 RID: 1680
			// (get) Token: 0x0600487E RID: 18558 RVA: 0x0012A75E File Offset: 0x0012895E
			// (set) Token: 0x0600487F RID: 18559 RVA: 0x0012A766 File Offset: 0x00128966
			public string value { get; private set; }

			// Token: 0x140000F5 RID: 245
			// (add) Token: 0x06004880 RID: 18560 RVA: 0x0012A770 File Offset: 0x00128970
			// (remove) Token: 0x06004881 RID: 18561 RVA: 0x0012A7A8 File Offset: 0x001289A8
			public event Action<string> onValueChanged;

			// Token: 0x06004882 RID: 18562 RVA: 0x00009F73 File Offset: 0x00008173
			public SvPasswordConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06004883 RID: 18563 RVA: 0x0012A7DD File Offset: 0x001289DD
			public override void SetString(string newValue)
			{
				if (newValue == null)
				{
					newValue = "";
				}
				if (this.value == newValue)
				{
					return;
				}
				this.value = newValue;
				Action<string> action = this.onValueChanged;
				if (action == null)
				{
					return;
				}
				action(this.value);
			}

			// Token: 0x06004884 RID: 18564 RVA: 0x0012A815 File Offset: 0x00128A15
			public override string GetString()
			{
				return this.value;
			}

			// Token: 0x04004562 RID: 17762
			public static readonly NetworkManagerSystem.SvPasswordConVar instance = new NetworkManagerSystem.SvPasswordConVar("sv_password", ConVarFlags.None, "", "The password to use for the server if hosting.");
		}

		// Token: 0x02000C69 RID: 3177
		protected class AddressablesChangeSceneAsyncOperation : NetworkManager.IChangeSceneAsyncOperation
		{
			// Token: 0x06004886 RID: 18566 RVA: 0x0012A83C File Offset: 0x00128A3C
			public AddressablesChangeSceneAsyncOperation(object key, LoadSceneMode loadMode, bool activateOnLoad)
			{
				this.srcOperation = Addressables.LoadSceneAsync(key, loadMode, activateOnLoad, 100);
				NetworkManagerSystem.AddressablesChangeSceneAsyncOperation.previousLoadOperation = new AsyncOperationHandle<SceneInstance>?(this.srcOperation);
				this.srcOperation.Completed += delegate(AsyncOperationHandle<SceneInstance> v)
				{
					this.srcOperationIsDone = true;
					this.sceneInstance = v.Result;
					this.ActivateIfReady();
				};
				this.allowSceneActivation = activateOnLoad;
			}

			// Token: 0x06004887 RID: 18567 RVA: 0x0012A88D File Offset: 0x00128A8D
			private void ActivateIfReady()
			{
				if (!this.srcOperationIsDone || this.isActivated)
				{
					return;
				}
				this.sceneInstance.ActivateAsync().completed += delegate(AsyncOperation _)
				{
					this.isDone = true;
				};
			}

			// Token: 0x17000691 RID: 1681
			// (get) Token: 0x06004888 RID: 18568 RVA: 0x0012A8BC File Offset: 0x00128ABC
			// (set) Token: 0x06004889 RID: 18569 RVA: 0x0012A8C4 File Offset: 0x00128AC4
			public bool isDone { get; private set; }

			// Token: 0x17000692 RID: 1682
			// (get) Token: 0x0600488A RID: 18570 RVA: 0x0012A8CD File Offset: 0x00128ACD
			// (set) Token: 0x0600488B RID: 18571 RVA: 0x0012A8D5 File Offset: 0x00128AD5
			public bool allowSceneActivation
			{
				get
				{
					return this._allowSceneActivation;
				}
				set
				{
					if (this._allowSceneActivation == value)
					{
						return;
					}
					this._allowSceneActivation = value;
					this.ActivateIfReady();
				}
			}

			// Token: 0x04004565 RID: 17765
			private static AsyncOperationHandle<SceneInstance>? previousLoadOperation;

			// Token: 0x04004566 RID: 17766
			private AsyncOperationHandle<SceneInstance> srcOperation;

			// Token: 0x04004567 RID: 17767
			private bool srcOperationIsDone;

			// Token: 0x04004568 RID: 17768
			private SceneInstance sceneInstance;

			// Token: 0x04004569 RID: 17769
			private bool isActivated;

			// Token: 0x0400456B RID: 17771
			private bool _allowSceneActivation;
		}
	}
}
