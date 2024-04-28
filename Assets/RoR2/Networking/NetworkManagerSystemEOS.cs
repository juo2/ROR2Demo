using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;
using Epic.OnlineServices.P2P;
using Facepunch.Steamworks;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C5A RID: 3162
	public class NetworkManagerSystemEOS : NetworkManagerSystem
	{
		// Token: 0x0600478D RID: 18317 RVA: 0x0012764C File Offset: 0x0012584C
		protected override void Start()
		{
			this.myUserId = null;
			foreach (QosType value in base.channels)
			{
				base.connectionConfig.AddChannel(value);
			}
			base.connectionConfig.PacketSize = 1200;
			base.FireOnStartGlobalEvent();
		}

		// Token: 0x0600478E RID: 18318 RVA: 0x001276C4 File Offset: 0x001258C4
		protected override void Update()
		{
			base.UpdateTime(ref this._unpredictedServerFrameTime, ref this._unpredictedServerFrameTimeSmoothed, ref this.unpredictedServerFrameTimeVelocity, Time.deltaTime);
			this.EnsureDesiredHost();
			this.UpdateServer();
			base.UpdateClient();
			this.UpdateNetworkReceiveLoop();
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x001276FB File Offset: 0x001258FB
		public void ResetSequencing()
		{
			this.packetsRecieved = 0;
			this.sequenceNumber.Clear();
			this.outOfOrderPackets.Clear();
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x0012771C File Offset: 0x0012591C
		private void UpdateNetworkReceiveLoop()
		{
			if (NetworkManagerSystemEOS.P2pInterface == null)
			{
				return;
			}
			byte maxValue = byte.MaxValue;
			this.packetsRecieved = 0;
			int num = 0;
			bool flag;
			do
			{
				num++;
				flag = false;
				ProductUserId incomingUserId;
				SocketId socketId;
				byte[] array;
				Result result = NetworkManagerSystemEOS.P2pInterface.ReceivePacket(new ReceivePacketOptions
				{
					LocalUserId = EOSLoginManager.loggedInProductId,
					MaxDataSizeBytes = (uint)base.connectionConfig.PacketSize
				}, out incomingUserId, out socketId, out maxValue, out array);
				if (this.ProcessData(result, incomingUserId, array, array.Length, 0))
				{
					flag = true;
				}
			}
			while (flag);
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x0012779C File Offset: 0x0012599C
		private bool ProcessData(Result result, ProductUserId incomingUserId, byte[] buf, int dataSize, int channelId)
		{
			if (result == Result.Success)
			{
				EOSNetworkConnection eosnetworkConnection = EOSNetworkConnection.Find(this.myUserId, incomingUserId);
				if (eosnetworkConnection != null)
				{
					eosnetworkConnection.TransportReceive(buf, dataSize, 0);
				}
				else
				{
					Debug.LogFormat("Rejecting data from sender: Not associated with a registered connection. id={0} dataLength={1}", new object[]
					{
						incomingUserId,
						dataSize
					});
				}
				return true;
			}
			if (result == Result.NotFound || result == Result.InvalidParameters)
			{
				return false;
			}
			Debug.LogError("P2PInterface ReceivePacket returned a failure: " + result.ToString());
			return false;
		}

		// Token: 0x06004792 RID: 18322 RVA: 0x00127814 File Offset: 0x00125A14
		protected override void EnsureDesiredHost()
		{
			if (false | this.serverShuttingDown | base.clientIsConnecting | (NetworkServer.active && NetworkManagerSystem.isLoadingScene) | (!NetworkClient.active && NetworkManagerSystem.isLoadingScene))
			{
				return;
			}
			bool isAnyUserSignedIn = LocalUserManager.isAnyUserSignedIn;
			if (base.desiredHost.isRemote && !isAnyUserSignedIn)
			{
				return;
			}
			if (this.isNetworkActive && !this.actedUponDesiredHost && !base.desiredHost.DescribesCurrentHost())
			{
				base.Disconnect();
				return;
			}
			if (this.actedUponDesiredHost)
			{
				return;
			}
			int maxPlayers = RoR2Application.maxPlayers;
			if (base.desiredHost.hostType == HostDescription.HostType.Self)
			{
				if (NetworkServer.active)
				{
					return;
				}
				this.actedUponDesiredHost = true;
				base.maxConnections = base.desiredHost.hostingParameters.maxPlayers;
				NetworkServer.dontListen = !base.desiredHost.hostingParameters.listen;
				if (!isAnyUserSignedIn)
				{
					base.StartServer(base.connectionConfig, maxPlayers);
				}
				else
				{
					this.StartHost(base.connectionConfig, maxPlayers);
				}
			}
			if (base.desiredHost.hostType == HostDescription.HostType.EOS && Time.unscaledTime - this.lastDesiredHostSetTime >= 0f)
			{
				this.actedUponDesiredHost = true;
				CSteamID cid = base.desiredHost.userID.CID;
				this.StartClient(cid.egsValue);
			}
			if (base.desiredHost.hostType == HostDescription.HostType.IPv4 && Time.unscaledTime - this.lastDesiredHostSetTime >= 0f)
			{
				this.actedUponDesiredHost = true;
				Debug.LogFormat("Attempting connection. ip={0} port={1}", new object[]
				{
					base.desiredHost.addressPortPair.address,
					base.desiredHost.addressPortPair.port
				});
				NetworkManagerSystem.singleton.networkAddress = base.desiredHost.addressPortPair.address;
				NetworkManagerSystem.singleton.networkPort = (int)base.desiredHost.addressPortPair.port;
				NetworkManagerSystem.singleton.StartClient(this.matchInfo, base.connectionConfig);
			}
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x00127A0C File Offset: 0x00125C0C
		public override void ForceCloseAllConnections()
		{
			using (IEnumerator<NetworkConnection> enumerator = NetworkServer.connections.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EOSNetworkConnection eosnetworkConnection;
					if ((eosnetworkConnection = (enumerator.Current as EOSNetworkConnection)) != null)
					{
						NetworkManagerSystemEOS.P2pInterface.CloseConnection(new CloseConnectionOptions
						{
							LocalUserId = eosnetworkConnection.LocalUserID,
							RemoteUserId = eosnetworkConnection.RemoteUserID,
							SocketId = NetworkManagerSystemEOS.socketId
						});
					}
				}
			}
			NetworkClient client = this.client;
			EOSNetworkConnection eosnetworkConnection2;
			if ((eosnetworkConnection2 = (((client != null) ? client.connection : null) as EOSNetworkConnection)) != null)
			{
				NetworkManagerSystemEOS.P2pInterface.CloseConnection(new CloseConnectionOptions
				{
					LocalUserId = eosnetworkConnection2.LocalUserID,
					RemoteUserId = eosnetworkConnection2.RemoteUserID,
					SocketId = NetworkManagerSystemEOS.socketId
				});
			}
			this.myUserId = null;
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x00127AE4 File Offset: 0x00125CE4
		public override NetworkConnection GetClient(UserID clientID)
		{
			if (!NetworkServer.active)
			{
				return null;
			}
			if (clientID.CID.egsValue == this.myUserId && NetworkServer.connections.Count > 0)
			{
				return NetworkServer.connections[0];
			}
			using (IEnumerator<NetworkConnection> enumerator = NetworkServer.connections.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EOSNetworkConnection eosnetworkConnection;
					if ((eosnetworkConnection = (enumerator.Current as EOSNetworkConnection)) != null && eosnetworkConnection.RemoteUserID == clientID.CID.egsValue)
					{
						return eosnetworkConnection;
					}
				}
			}
			Debug.LogError("Client not found");
			return null;
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x00127B98 File Offset: 0x00125D98
		public override void OnServerConnect(NetworkConnection conn)
		{
			base.OnServerConnect(conn);
			if (NetworkUser.readOnlyInstancesList.Count >= base.maxConnections)
			{
				base.ServerKickClient(conn, new NetworkManagerSystem.SimpleLocalizedKickReason("KICK_REASON_SERVERFULL", Array.Empty<string>()));
				return;
			}
			base.FireServerConnectGlobalEvent(conn);
		}

		// Token: 0x06004796 RID: 18326 RVA: 0x00127BD4 File Offset: 0x00125DD4
		public override void OnServerDisconnect(NetworkConnection conn)
		{
			base.FireServerDisconnectGlobalEvent(conn);
			if (conn.clientOwnedObjects != null)
			{
				foreach (NetworkInstanceId netId in new HashSet<NetworkInstanceId>(conn.clientOwnedObjects))
				{
					GameObject gameObject = NetworkServer.FindLocalObject(netId);
					if (gameObject != null && gameObject.GetComponent<CharacterMaster>())
					{
						NetworkIdentity component = gameObject.GetComponent<NetworkIdentity>();
						if (component && component.clientAuthorityOwner == conn)
						{
							component.RemoveClientAuthority(conn);
						}
					}
				}
			}
			List<PlayerController> playerControllers = conn.playerControllers;
			for (int i = 0; i < playerControllers.Count; i++)
			{
				NetworkUser component2 = playerControllers[i].gameObject.GetComponent<NetworkUser>();
				if (component2)
				{
					Chat.SendPlayerDisconnectedMessage(component2);
				}
			}
			EOSNetworkConnection eosnetworkConnection;
			if ((eosnetworkConnection = (conn as EOSNetworkConnection)) != null)
			{
				Debug.Log(string.Format("Closing connection with RemoteUserID: {0}", eosnetworkConnection.RemoteUserID));
			}
			base.OnServerDisconnect(conn);
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x00127CDC File Offset: 0x00125EDC
		public override void OnStartServer()
		{
			base.OnStartServer();
			ServerManagerBase<EOSServerManager>.StartServer();
			this.InitP2P();
			NetworkMessageHandlerAttribute.RegisterServerMessages();
			base.InitializeTime();
			this.serverNetworkSessionInstance = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkSession"));
			base.FireStartServerGlobalEvent();
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x00127D18 File Offset: 0x00125F18
		public override void OnStopServer()
		{
			base.FireStopServerGlobalEvent();
			for (int i = 0; i < NetworkServer.connections.Count; i++)
			{
				NetworkConnection networkConnection = NetworkServer.connections[i];
				if (networkConnection != null)
				{
					this.OnServerDisconnect(networkConnection);
				}
			}
			UnityEngine.Object.Destroy(this.serverNetworkSessionInstance);
			this.serverNetworkSessionInstance = null;
			ServerManagerBase<EOSServerManager>.StopServer();
			this.myUserId = null;
			base.OnStopServer();
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x00127D7C File Offset: 0x00125F7C
		public override void ServerBanClient(NetworkConnection conn)
		{
			EOSNetworkConnection eosnetworkConnection;
			if ((eosnetworkConnection = (conn as EOSNetworkConnection)) != null)
			{
				this.epicIdBanList.Add(eosnetworkConnection.RemoteUserID);
			}
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x00127DA4 File Offset: 0x00125FA4
		protected override NetworkUserId AddPlayerIdFromPlatform(NetworkConnection conn, NetworkManagerSystem.AddPlayerMessage message, byte playerControllerId)
		{
			NetworkUserId result = NetworkUserId.FromIp(conn.address, playerControllerId);
			ClientAuthData clientAuthData = ServerAuthManager.FindAuthData(conn);
			CSteamID? csteamID = (clientAuthData != null) ? new CSteamID?(clientAuthData.steamId) : null;
			if (csteamID != null)
			{
				if (csteamID.Value.isSteam)
				{
					result = NetworkUserId.FromId(csteamID.Value.steamValue, playerControllerId);
				}
				if (csteamID.Value.isEGS)
				{
					result = new NetworkUserId(csteamID.Value.egsValue.ToString(), playerControllerId);
				}
			}
			return result;
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x00127E3C File Offset: 0x0012603C
		protected override void KickClient(NetworkConnection conn, NetworkManagerSystem.BaseKickReason reason)
		{
			EOSNetworkConnection eosnetworkConnection;
			if ((eosnetworkConnection = (conn as EOSNetworkConnection)) != null)
			{
				eosnetworkConnection.ignore = true;
			}
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x00127E5A File Offset: 0x0012605A
		public override void ServerHandleClientDisconnect(NetworkConnection conn)
		{
			this.OnServerDisconnect(conn);
			conn.InvokeHandlerNoData(33);
			conn.Disconnect();
			conn.Dispose();
			if (conn is EOSNetworkConnection)
			{
				NetworkServer.RemoveExternalConnection(conn.connectionId);
			}
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void UpdateServer()
		{
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x00127E8B File Offset: 0x0012608B
		public override void OnStartClient(NetworkClient newClient)
		{
			base.OnStartClient(newClient);
			this.InitP2P();
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x00127E9C File Offset: 0x0012609C
		public override void OnClientDisconnect(NetworkConnection conn)
		{
			EOSNetworkConnection eosnetworkConnection;
			if ((eosnetworkConnection = (conn as EOSNetworkConnection)) != null)
			{
				Debug.LogFormat("Closing connection with remote ID: {0}", new object[]
				{
					eosnetworkConnection.RemoteUserID
				});
			}
			base.OnClientDisconnect(conn);
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x00127ED4 File Offset: 0x001260D4
		protected override NetworkManagerSystem.AddPlayerMessage CreateClientAddPlayerMessage()
		{
			NetworkManagerSystem.AddPlayerMessage result;
			if (this.client != null)
			{
				result = new NetworkManagerSystem.AddPlayerMessage
				{
					id = new UserID(new CSteamID(EOSLoginManager.loggedInProductId)),
					steamAuthTicketData = Array.Empty<byte>()
				};
			}
			else
			{
				result = new NetworkManagerSystem.AddPlayerMessage
				{
					id = default(UserID),
					steamAuthTicketData = Array.Empty<byte>()
				};
			}
			return result;
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void UpdateCheckInactiveConnections()
		{
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x00127F2F File Offset: 0x0012612F
		protected override void StartClient(UserID serverID)
		{
			this.StartClient(serverID.CID.egsValue);
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x00127F43 File Offset: 0x00126143
		protected override void PlatformAuth(ref ClientAuthData authData, NetworkConnection conn)
		{
			authData.steamId = new CSteamID(EOSLoginManager.loggedInProductId);
			authData.authTicket = Client.Instance.Auth.GetAuthSessionTicket().Data;
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x00127F74 File Offset: 0x00126174
		private void StartClient(ProductUserId remoteUserId)
		{
			if (!NetworkServer.active)
			{
				NetworkManager.networkSceneName = "";
			}
			string text = "";
			if (this.isNetworkActive)
			{
				text += "isNetworkActive ";
			}
			if (NetworkClient.active)
			{
				text += "NetworkClient.active ";
			}
			if (NetworkServer.active)
			{
				text += "NetworkClient.active ";
			}
			if (NetworkManagerSystem.isLoadingScene)
			{
				text += "isLoadingScene ";
			}
			if (text != "")
			{
				Debug.Log(text);
				RoR2Application.onNextUpdate += delegate()
				{
				};
			}
			EOSNetworkConnection eosnetworkConnection = new EOSNetworkConnection(EOSLoginManager.loggedInProductId, remoteUserId);
			EOSNetworkClient eosnetworkClient = new EOSNetworkClient(eosnetworkConnection);
			eosnetworkClient.Configure(base.connectionConfig, 1);
			base.UseExternalClient(eosnetworkClient);
			eosnetworkClient.Connect();
			Debug.LogFormat("Initiating connection to server {0}...", new object[]
			{
				remoteUserId
			});
			if (!eosnetworkConnection.SendConnectionRequest())
			{
				Debug.LogFormat("Failed to send connection request to server {0}.", new object[]
				{
					remoteUserId
				});
			}
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x0012807C File Offset: 0x0012627C
		public override bool IsConnectedToServer(UserID serverID)
		{
			if (this.client == null || !this.client.connection.isConnected)
			{
				return false;
			}
			EOSNetworkConnection eosnetworkConnection;
			if ((eosnetworkConnection = (this.client.connection as EOSNetworkConnection)) != null)
			{
				return eosnetworkConnection.RemoteUserID == serverID.CID.egsValue;
			}
			return this.client.connection.address == "localServer" && serverID.CID == base.serverP2PId;
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060047A6 RID: 18342 RVA: 0x00128100 File Offset: 0x00126300
		// (set) Token: 0x060047A7 RID: 18343 RVA: 0x00128107 File Offset: 0x00126307
		public static P2PInterface P2pInterface { get; private set; }

		// Token: 0x060047A8 RID: 18344 RVA: 0x00128110 File Offset: 0x00126310
		private void OnConnectionRequested(OnIncomingConnectionRequestInfo connectionRequestInfo)
		{
			bool flag = false;
			if (connectionRequestInfo.SocketId.SocketName == NetworkManagerSystemEOS.socketId.SocketName)
			{
				EOSNetworkClient eosnetworkClient;
				if (NetworkServer.active)
				{
					flag = (!NetworkServer.dontListen && !this.epicIdBanList.Contains(connectionRequestInfo.RemoteUserId) && !base.IsServerAtMaxConnections());
				}
				else if ((eosnetworkClient = (this.client as EOSNetworkClient)) != null && eosnetworkClient.eosConnection.RemoteUserID == connectionRequestInfo.RemoteUserId)
				{
					flag = true;
				}
			}
			string arg = flag ? "accepted" : "rejected";
			Debug.Log(string.Format("Incoming connection from Product User ID {0}: {1}", connectionRequestInfo.RemoteUserId, arg));
			if (flag)
			{
				NetworkManagerSystemEOS.P2pInterface.AcceptConnection(new AcceptConnectionOptions
				{
					LocalUserId = connectionRequestInfo.LocalUserId,
					RemoteUserId = connectionRequestInfo.RemoteUserId,
					SocketId = NetworkManagerSystemEOS.socketId
				});
				this.CreateServerP2PConnectionWithPeer(connectionRequestInfo.RemoteUserId);
			}
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x001281FC File Offset: 0x001263FC
		public void CreateServerP2PConnectionWithPeer(ProductUserId peer)
		{
			EOSNetworkConnection eosnetworkConnection = new EOSNetworkConnection(this.myUserId, peer);
			eosnetworkConnection.ForceInitialize(NetworkServer.hostTopology);
			int num = -1;
			ReadOnlyCollection<NetworkConnection> connections = NetworkServer.connections;
			for (int i = 1; i < connections.Count; i++)
			{
				if (connections[i] == null)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				num = connections.Count;
			}
			eosnetworkConnection.connectionId = num;
			NetworkServer.AddExternalConnection(eosnetworkConnection);
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.StartMessage(32);
			networkWriter.FinishMessage();
			eosnetworkConnection.SendWriter(networkWriter, QosChannelIndex.defaultReliable.intVal);
		}

		// Token: 0x060047AA RID: 18346 RVA: 0x0012828C File Offset: 0x0012648C
		protected void InitP2P()
		{
			if (NetworkManagerSystemEOS.P2pInterface == null)
			{
				NetworkManagerSystemEOS.P2pInterface = EOSPlatformManager.GetPlatformInterface().GetP2PInterface();
				AddNotifyPeerConnectionRequestOptions options = new AddNotifyPeerConnectionRequestOptions
				{
					LocalUserId = EOSLoginManager.loggedInProductId,
					SocketId = NetworkManagerSystemEOS.socketId
				};
				OnIncomingConnectionRequestCallback connectionRequestHandler = new OnIncomingConnectionRequestCallback(this.OnConnectionRequested);
				NetworkManagerSystemEOS.P2pInterface.AddNotifyPeerConnectionRequest(options, null, connectionRequestHandler);
				AddNotifyPeerConnectionClosedOptions options2 = new AddNotifyPeerConnectionClosedOptions
				{
					LocalUserId = EOSLoginManager.loggedInProductId,
					SocketId = NetworkManagerSystemEOS.socketId
				};
				OnRemoteConnectionClosedCallback connectionClosedHandler = new OnRemoteConnectionClosedCallback(this.OnConnectionClosed);
				NetworkManagerSystemEOS.P2pInterface.AddNotifyPeerConnectionClosed(options2, null, connectionClosedHandler);
			}
			this.myUserId = EOSLoginManager.loggedInProductId;
			base.serverP2PId = new CSteamID(this.myUserId);
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x00128340 File Offset: 0x00126540
		private void OnConnectionClosed(OnRemoteConnectionClosedInfo connectionRequestInfo)
		{
			Debug.Log(string.Format("Close connection mesasge received for Product User ID {0}, reason = {1}", connectionRequestInfo.RemoteUserId, connectionRequestInfo.Reason.ToString()));
			EOSNetworkConnection eosnetworkConnection = EOSNetworkConnection.Find(this.myUserId, connectionRequestInfo.RemoteUserId);
			if (eosnetworkConnection == null)
			{
				Debug.Log(string.Format("Unable to find connection for remote user id: {0} when attempting to close!", connectionRequestInfo.RemoteUserId));
				return;
			}
			if (this.client != null && this.client.connection == eosnetworkConnection)
			{
				eosnetworkConnection.InvokeHandlerNoData(33);
				eosnetworkConnection.Disconnect();
				eosnetworkConnection.Dispose();
			}
			if (NetworkServer.active && NetworkServer.connections.IndexOf(eosnetworkConnection) != -1)
			{
				this.ServerHandleClientDisconnect(eosnetworkConnection);
			}
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x001283E8 File Offset: 0x001265E8
		protected override void PlatformClientSetPlayers(ConCommandArgs args)
		{
			if (this.client != null && this.client.connection != null)
			{
				base.ClientSetPlayers(this.client.connection);
			}
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void PlatformConnectP2P(ConCommandArgs args)
		{
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x00128410 File Offset: 0x00126610
		protected override void PlatformDisconnect(ConCommandArgs args)
		{
			NetworkManagerSystem.singleton.desiredHost = HostDescription.none;
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x00128424 File Offset: 0x00126624
		protected override void PlatformConnect(ConCommandArgs args)
		{
			AddressPortPair argAddressPortPair = args.GetArgAddressPortPair(0);
			if (!NetworkManagerSystem.singleton)
			{
				return;
			}
			NetworkManagerSystem.EnsureNetworkManagerNotBusy();
			Debug.LogFormat("Parsed address={0} port={1}. Setting desired host.", new object[]
			{
				argAddressPortPair.address,
				argAddressPortPair.port
			});
			NetworkManagerSystem.singleton.desiredHost = new HostDescription(argAddressPortPair);
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x00128484 File Offset: 0x00126684
		protected override void PlatformHost(ConCommandArgs args)
		{
			if (!NetworkManagerSystem.singleton)
			{
				return;
			}
			bool argBool = args.GetArgBool(0);
			if (PlatformSystems.lobbyManager.isInLobby && !PlatformSystems.lobbyManager.ownsLobby)
			{
				return;
			}
			bool flag = false;
			if (NetworkServer.active)
			{
				Debug.Log("Server already running.");
				flag = true;
			}
			if (!flag)
			{
				int maxPlayers = NetworkManagerSystem.SvMaxPlayersConVar.instance.intValue;
				if (PlatformSystems.lobbyManager.isInLobby)
				{
					LobbyDetailsInfo lobbyDetailsInfo;
					maxPlayers = (int)(((PlatformSystems.lobbyManager as EOSLobbyManager).CurrentLobbyDetails.CopyInfo(new LobbyDetailsCopyInfoOptions(), out lobbyDetailsInfo) == Result.Success) ? lobbyDetailsInfo.MaxMembers : 0U);
				}
				NetworkManagerSystem.singleton.desiredHost = new HostDescription(new HostDescription.HostingParameters
				{
					listen = argBool,
					maxPlayers = maxPlayers
				});
			}
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x0012853F File Offset: 0x0012673F
		protected override void PlatformGetP2PSessionState(ConCommandArgs args)
		{
			NetworkManagerSystemEOS.CheckSteamworks();
			args.GetArgSteamID(0);
			NetworkManagerSystem.singleton;
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x0012855C File Offset: 0x0012675C
		protected override void PlatformKick(ConCommandArgs args)
		{
			NetworkManagerSystemEOS.CheckSteamworks();
			UserID clientId = new UserID(args.GetArgSteamID(0));
			NetworkConnection client = NetworkManagerSystem.singleton.GetClient(clientId);
			if (client != null)
			{
				NetworkManagerSystem.singleton.ServerKickClient(client, new NetworkManagerSystem.SimpleLocalizedKickReason("KICK_REASON_KICK", Array.Empty<string>()));
			}
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x001285A8 File Offset: 0x001267A8
		protected override void PlatformBan(ConCommandArgs args)
		{
			NetworkManagerSystemEOS.CheckSteamworks();
			UserID clientId = new UserID(args.GetArgSteamID(0));
			NetworkConnection client = NetworkManagerSystem.singleton.GetClient(clientId);
			if (client != null)
			{
				NetworkManagerSystem.singleton.ServerBanClient(client);
				NetworkManagerSystem.singleton.ServerKickClient(client, new NetworkManagerSystem.SimpleLocalizedKickReason("KICK_REASON_BAN", Array.Empty<string>()));
			}
		}

		// Token: 0x060047B4 RID: 18356 RVA: 0x001285FD File Offset: 0x001267FD
		public static void CheckSteamworks()
		{
			if (Client.Instance == null)
			{
				throw new ConCommandException("Steamworks not available.");
			}
		}

		// Token: 0x060047B5 RID: 18357 RVA: 0x000026ED File Offset: 0x000008ED
		public override void CreateLocalLobby()
		{
		}

		// Token: 0x0400451F RID: 17695
		public ProductUserId myUserId;

		// Token: 0x04004520 RID: 17696
		private int packetsRecieved;

		// Token: 0x04004521 RID: 17697
		private Dictionary<ulong, byte> sequenceNumber = new Dictionary<ulong, byte>();

		// Token: 0x04004522 RID: 17698
		private Dictionary<ulong, List<Tuple<byte, byte[], uint>>> outOfOrderPackets = new Dictionary<ulong, List<Tuple<byte, byte[], uint>>>();

		// Token: 0x04004523 RID: 17699
		private List<ProductUserId> epicIdBanList = new List<ProductUserId>();

		// Token: 0x04004525 RID: 17701
		public static SocketId socketId = new SocketId
		{
			SocketName = "RoR2EOS"
		};
	}
}
