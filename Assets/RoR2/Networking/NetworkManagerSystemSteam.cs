using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Facepunch.Steamworks;
using RoR2.ConVar;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C6F RID: 3183
	public class NetworkManagerSystemSteam : NetworkManagerSystem
	{
		// Token: 0x060048A6 RID: 18598 RVA: 0x0012AF00 File Offset: 0x00129100
		protected override void Start()
		{
			foreach (QosType value in base.channels)
			{
				base.connectionConfig.AddChannel(value);
			}
			base.connectionConfig.PacketSize = 1200;
			base.FireOnStartGlobalEvent();
		}

		// Token: 0x060048A7 RID: 18599 RVA: 0x0012AF70 File Offset: 0x00129170
		protected override void Update()
		{
			base.UpdateTime(ref this._unpredictedServerFrameTime, ref this._unpredictedServerFrameTimeSmoothed, ref this.unpredictedServerFrameTimeVelocity, Time.deltaTime);
			this.EnsureDesiredHost();
			this.UpdateServer();
			base.UpdateClient();
		}

		// Token: 0x060048A8 RID: 18600 RVA: 0x0012AFA4 File Offset: 0x001291A4
		protected override void EnsureDesiredHost()
		{
			if ((false | this.serverShuttingDown | base.clientIsConnecting | (NetworkServer.active && NetworkManagerSystem.isLoadingScene) | (!NetworkClient.active && NetworkManagerSystem.isLoadingScene)) || !SystemInitializerAttribute.hasExecuted)
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
			if (!this.actedUponDesiredHost)
			{
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
						base.StartServer(base.connectionConfig, 4);
					}
					else
					{
						this.StartHost(base.connectionConfig, 4);
					}
				}
				if (base.desiredHost.hostType == HostDescription.HostType.Steam && Time.unscaledTime - this.lastDesiredHostSetTime >= 0f)
				{
					this.actedUponDesiredHost = true;
					this.StartClient(base.desiredHost.userID);
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
					NetworkManagerSystem.singleton.StartClient();
				}
			}
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x0012B184 File Offset: 0x00129384
		public override void ForceCloseAllConnections()
		{
			Client instance = Client.Instance;
			Networking networking = (instance != null) ? instance.Networking : null;
			if (networking == null)
			{
				return;
			}
			using (IEnumerator<NetworkConnection> enumerator = NetworkServer.connections.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SteamNetworkConnection steamNetworkConnection;
					if ((steamNetworkConnection = (enumerator.Current as SteamNetworkConnection)) != null)
					{
						networking.CloseSession(steamNetworkConnection.steamId.steamValue);
					}
				}
			}
			NetworkClient client = this.client;
			SteamNetworkConnection steamNetworkConnection2;
			if ((steamNetworkConnection2 = (((client != null) ? client.connection : null) as SteamNetworkConnection)) != null)
			{
				networking.CloseSession(steamNetworkConnection2.steamId.steamValue);
			}
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x0012B228 File Offset: 0x00129428
		public override NetworkConnection GetClient(UserID clientID)
		{
			if (!NetworkServer.active)
			{
				Debug.Log("Server is not active.");
				return null;
			}
			if (clientID.CID.steamValue == Client.Instance.SteamId && NetworkServer.connections.Count > 0)
			{
				return NetworkServer.connections[0];
			}
			foreach (NetworkConnection networkConnection in NetworkServer.connections)
			{
				SteamNetworkConnection steamNetworkConnection;
				if ((steamNetworkConnection = (networkConnection as SteamNetworkConnection)) != null)
				{
					if (steamNetworkConnection.steamId.steamValue == clientID.CID.steamValue)
					{
						return steamNetworkConnection;
					}
				}
				else
				{
					Debug.Log(string.Format("Skipping connection ({0})", networkConnection.GetType()));
				}
			}
			Debug.LogError("Client not found");
			return null;
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x00127B98 File Offset: 0x00125D98
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

		// Token: 0x060048AC RID: 18604 RVA: 0x0012B2F8 File Offset: 0x001294F8
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
			if (conn is SteamNetworkConnection)
			{
				Debug.LogFormat("Closing connection with steamId {0}", new object[]
				{
					((SteamNetworkConnection)conn).steamId.steamValue
				});
			}
			base.OnServerDisconnect(conn);
		}

		// Token: 0x060048AD RID: 18605 RVA: 0x0012B40C File Offset: 0x0012960C
		public override void InitPlatformServer()
		{
			ServerManagerBase<SteamworksServerManager>.StartServer();
			this.InitP2P();
		}

		// Token: 0x060048AE RID: 18606 RVA: 0x0012B41C File Offset: 0x0012961C
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
			ServerManagerBase<SteamworksServerManager>.StopServer();
			base.OnStopServer();
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x0012B478 File Offset: 0x00129678
		public override void ServerBanClient(NetworkConnection conn)
		{
			SteamNetworkConnection steamNetworkConnection;
			if ((steamNetworkConnection = (conn as SteamNetworkConnection)) != null)
			{
				this.steamIdBanList.Add(steamNetworkConnection.steamId);
			}
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x0012B4A0 File Offset: 0x001296A0
		protected override NetworkUserId AddPlayerIdFromPlatform(NetworkConnection conn, NetworkManagerSystem.AddPlayerMessage message, byte playerControllerId)
		{
			NetworkUserId result = NetworkUserId.FromIp(conn.address, playerControllerId);
			ClientAuthData clientAuthData = ServerAuthManager.FindAuthData(conn);
			CSteamID csteamID = (clientAuthData != null) ? clientAuthData.steamId : CSteamID.nil;
			if (csteamID != CSteamID.nil)
			{
				result = NetworkUserId.FromId(csteamID.steamValue, playerControllerId);
			}
			return result;
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x0012B4EC File Offset: 0x001296EC
		protected override void KickClient(NetworkConnection conn, NetworkManagerSystem.BaseKickReason reason)
		{
			SteamNetworkConnection steamNetworkConnection;
			if ((steamNetworkConnection = (conn as SteamNetworkConnection)) != null)
			{
				steamNetworkConnection.ignore = true;
			}
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x0012B50A File Offset: 0x0012970A
		public override void ServerHandleClientDisconnect(NetworkConnection conn)
		{
			this.OnServerDisconnect(conn);
			conn.InvokeHandlerNoData(33);
			conn.Disconnect();
			conn.Dispose();
			if (conn is SteamNetworkConnection)
			{
				NetworkServer.RemoveExternalConnection(conn.connectionId);
			}
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x0012B53C File Offset: 0x0012973C
		protected override void UpdateServer()
		{
			if (NetworkServer.active)
			{
				ReadOnlyCollection<NetworkConnection> connections = NetworkServer.connections;
				for (int i = connections.Count - 1; i >= 0; i--)
				{
					SteamNetworkConnection steamNetworkConnection;
					if ((steamNetworkConnection = (connections[i] as SteamNetworkConnection)) != null)
					{
						Networking.P2PSessionState p2PSessionState = default(Networking.P2PSessionState);
						if (Client.Instance.Networking.GetP2PSessionState(steamNetworkConnection.steamId.steamValue, ref p2PSessionState) && p2PSessionState.Connecting == 0 && p2PSessionState.ConnectionActive == 0)
						{
							this.ServerHandleClientDisconnect(steamNetworkConnection);
						}
					}
				}
			}
		}

		// Token: 0x060048B4 RID: 18612 RVA: 0x0012B5B6 File Offset: 0x001297B6
		public override void OnStartClient(NetworkClient newClient)
		{
			base.OnStartClient(newClient);
			this.InitP2P();
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x0012B5C5 File Offset: 0x001297C5
		public override void OnClientConnect(NetworkConnection conn)
		{
			base.OnClientConnect(conn);
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x0012B5D0 File Offset: 0x001297D0
		public override void OnClientDisconnect(NetworkConnection conn)
		{
			SteamNetworkConnection steamNetworkConnection;
			if ((steamNetworkConnection = (conn as SteamNetworkConnection)) != null)
			{
				Debug.LogFormat("Closing connection with steamId {0}", new object[]
				{
					steamNetworkConnection.steamId
				});
			}
			base.OnClientDisconnect(conn);
		}

		// Token: 0x060048B7 RID: 18615 RVA: 0x0012B60C File Offset: 0x0012980C
		protected override NetworkManagerSystem.AddPlayerMessage CreateClientAddPlayerMessage()
		{
			NetworkManagerSystem.AddPlayerMessage result;
			if (Client.Instance != null)
			{
				result = new NetworkManagerSystem.AddPlayerMessage
				{
					id = new UserID(Client.Instance.SteamId),
					steamAuthTicketData = Client.Instance.Auth.GetAuthSessionTicket().Data
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

		// Token: 0x060048B8 RID: 18616 RVA: 0x0012B678 File Offset: 0x00129878
		protected override void UpdateCheckInactiveConnections()
		{
			NetworkClient client = this.client;
			if (((client != null) ? client.connection : null) is SteamNetworkConnection)
			{
				Networking.P2PSessionState p2PSessionState = default(Networking.P2PSessionState);
				if (Client.Instance.Networking.GetP2PSessionState(((SteamNetworkConnection)this.client.connection).steamId.steamValue, ref p2PSessionState) && p2PSessionState.Connecting == 0 && p2PSessionState.ConnectionActive == 0)
				{
					this.client.connection.InvokeHandlerNoData(33);
					base.StopClient();
				}
			}
		}

		// Token: 0x060048B9 RID: 18617 RVA: 0x0012B6FC File Offset: 0x001298FC
		protected override void PlatformAuth(ref ClientAuthData authData, NetworkConnection conn)
		{
			authData.steamId = new CSteamID(Client.Instance.SteamId);
			authData.authTicket = Client.Instance.Auth.GetAuthSessionTicket().Data;
		}

		// Token: 0x060048BA RID: 18618 RVA: 0x0012B730 File Offset: 0x00129930
		protected override void StartClient(UserID serverID)
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
			SteamNetworkConnection steamNetworkConnection = new SteamNetworkConnection(Client.Instance, serverID.CID);
			SteamNetworkClient steamNetworkClient = new SteamNetworkClient(steamNetworkConnection);
			steamNetworkClient.Configure(base.connectionConfig, 1);
			base.UseExternalClient(steamNetworkClient);
			steamNetworkClient.Connect();
			Debug.LogFormat("Initiating connection to server {0}...", new object[]
			{
				serverID.CID.steamValue
			});
			if (!steamNetworkConnection.SendConnectionRequest())
			{
				Debug.LogFormat("Failed to send connection request to server {0}.", new object[]
				{
					serverID.CID.steamValue
				});
			}
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x0012B85C File Offset: 0x00129A5C
		public override bool IsConnectedToServer(UserID serverID)
		{
			if (this.client == null || !this.client.connection.isConnected || Client.Instance == null)
			{
				return false;
			}
			SteamNetworkConnection steamNetworkConnection;
			if ((steamNetworkConnection = (this.client.connection as SteamNetworkConnection)) != null)
			{
				return steamNetworkConnection.steamId == serverID.CID;
			}
			return this.client.connection.address == "localServer" && serverID.CID == base.serverP2PId;
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x0012B8E1 File Offset: 0x00129AE1
		public static bool IsMemberInSteamLobby(CSteamID steamId)
		{
			return Client.Instance.Lobby.UserIsInCurrentLobby(steamId.steamValue);
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x0012B8F8 File Offset: 0x00129AF8
		private static CSteamID GetSteamworksSteamId(BaseSteamworks steamworks)
		{
			Client client;
			if ((client = (steamworks as Client)) != null)
			{
				return new CSteamID(client.SteamId);
			}
			Server server;
			if ((server = (steamworks as Server)) != null)
			{
				return new CSteamID(server.SteamId);
			}
			return CSteamID.nil;
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x0012B938 File Offset: 0x00129B38
		protected void InitP2P()
		{
			Server instance = Server.Instance;
			Networking networking = (instance != null) ? instance.Networking : null;
			Client instance2 = Client.Instance;
			Networking networking2 = (instance2 != null) ? instance2.Networking : null;
			if (networking != null)
			{
				networking.OnIncomingConnection = new Func<ulong, bool>(this.OnSteamServerP2PIncomingConnection);
				networking.OnConnectionFailed = new Action<ulong, Networking.SessionError>(this.OnSteamServerP2PConnectionFailed);
				networking.OnP2PData = new Networking.OnRecievedP2PData(this.OnSteamServerP2PData);
				for (int i = 0; i < base.connectionConfig.ChannelCount; i++)
				{
					networking.SetListenChannel(i, true);
				}
			}
			if (networking2 != null)
			{
				networking2.OnIncomingConnection = new Func<ulong, bool>(this.OnSteamClientP2PIncomingConnection);
				networking2.OnConnectionFailed = new Action<ulong, Networking.SessionError>(this.OnSteamClientP2PConnectionFailed);
				networking2.OnP2PData = new Networking.OnRecievedP2PData(this.OnSteamClientP2PData);
				for (int j = 0; j < base.connectionConfig.ChannelCount; j++)
				{
					networking2.SetListenChannel(j, true);
				}
			}
			this.clientSteamworks = Client.Instance;
			this.serverSteamworks = null;
			if (NetworkServer.active)
			{
				this.serverSteamworks = ((NetworkManagerSystemSteam.cvSteamP2PUseSteamServer.value ? Server.Instance : null) ?? Client.Instance);
			}
			base.clientP2PId = NetworkManagerSystemSteam.GetSteamworksSteamId(this.clientSteamworks);
			base.serverP2PId = NetworkManagerSystemSteam.GetSteamworksSteamId(this.serverSteamworks);
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x0012BA75 File Offset: 0x00129C75
		private bool OnSteamServerP2PIncomingConnection(ulong senderSteamId)
		{
			return this.OnIncomingP2PConnection(Server.Instance, new CSteamID(senderSteamId));
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x0012BA88 File Offset: 0x00129C88
		private void OnSteamServerP2PConnectionFailed(ulong senderSteamId, Networking.SessionError sessionError)
		{
			this.OnP2PConnectionFailed(Server.Instance, new CSteamID(senderSteamId), sessionError);
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x0012BA9C File Offset: 0x00129C9C
		private void OnSteamServerP2PData(ulong senderSteamId, byte[] data, int dataLength, int channel)
		{
			this.OnP2PData(Server.Instance, new CSteamID(senderSteamId), data, dataLength, channel);
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x0012BAB3 File Offset: 0x00129CB3
		private bool OnSteamClientP2PIncomingConnection(ulong senderSteamId)
		{
			return this.OnIncomingP2PConnection(Client.Instance, new CSteamID(senderSteamId));
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x0012BAC6 File Offset: 0x00129CC6
		private void OnSteamClientP2PConnectionFailed(ulong senderSteamId, Networking.SessionError sessionError)
		{
			this.OnP2PConnectionFailed(Client.Instance, new CSteamID(senderSteamId), sessionError);
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x0012BADA File Offset: 0x00129CDA
		private void OnSteamClientP2PData(ulong senderSteamId, byte[] data, int dataLength, int channel)
		{
			this.OnP2PData(Client.Instance, new CSteamID(senderSteamId), data, dataLength, channel);
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x0012BAF4 File Offset: 0x00129CF4
		private bool OnIncomingP2PConnection(BaseSteamworks receiver, CSteamID senderSteamId)
		{
			bool flag = false;
			if (receiver == this.serverSteamworks)
			{
				if (NetworkServer.active)
				{
					flag = (!NetworkServer.dontListen && !this.steamIdBanList.Contains(senderSteamId) && !base.IsServerAtMaxConnections());
				}
				else if (this.client is SteamNetworkClient && ((SteamNetworkClient)this.client).steamConnection.steamId == senderSteamId)
				{
					flag = true;
				}
			}
			Debug.LogFormat("Incoming Steamworks connection from Steam ID {0}: {1}", new object[]
			{
				senderSteamId,
				flag ? "accepted" : "rejected"
			});
			if (flag)
			{
				this.CreateServerP2PConnectionWithPeer(receiver, senderSteamId);
			}
			return flag;
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x0012BB9C File Offset: 0x00129D9C
		private void OnP2PConnectionFailed(BaseSteamworks receiver, CSteamID senderSteamId, Networking.SessionError sessionError)
		{
			Debug.LogFormat("NetworkManagerSystem.OnClientP2PConnectionFailed steamId={0} sessionError={1}", new object[]
			{
				senderSteamId,
				sessionError
			});
			SteamNetworkConnection steamNetworkConnection = SteamNetworkConnection.Find(receiver, senderSteamId);
			if (steamNetworkConnection != null)
			{
				if (this.client != null && this.client.connection == steamNetworkConnection)
				{
					steamNetworkConnection.InvokeHandlerNoData(33);
					steamNetworkConnection.Disconnect();
					steamNetworkConnection.Dispose();
				}
				if (NetworkServer.active && NetworkServer.connections.IndexOf(steamNetworkConnection) != -1)
				{
					this.ServerHandleClientDisconnect(steamNetworkConnection);
				}
			}
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x0012BC20 File Offset: 0x00129E20
		private void OnP2PData(BaseSteamworks receiver, CSteamID senderSteamId, byte[] data, int dataLength, int channel)
		{
			if (SteamNetworkConnection.cvNetP2PDebugTransport.value)
			{
				Debug.LogFormat("Received packet from {0} dataLength={1} channel={2}", new object[]
				{
					senderSteamId,
					dataLength,
					channel
				});
			}
			SteamNetworkConnection steamNetworkConnection = SteamNetworkConnection.Find(receiver, senderSteamId);
			if (steamNetworkConnection != null)
			{
				steamNetworkConnection.TransportReceive(data, dataLength, 0);
				return;
			}
			Debug.LogFormat("Rejecting data from sender: Not associated with a registered connection. steamid={0} dataLength={1}", new object[]
			{
				senderSteamId,
				data
			});
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x0012BC9C File Offset: 0x00129E9C
		public void CreateServerP2PConnectionWithPeer(BaseSteamworks steamworks, CSteamID peer)
		{
			SteamNetworkConnection steamNetworkConnection = new SteamNetworkConnection(steamworks, peer);
			steamNetworkConnection.ForceInitialize(NetworkServer.hostTopology);
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
			steamNetworkConnection.connectionId = num;
			NetworkServer.AddExternalConnection(steamNetworkConnection);
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.StartMessage(32);
			networkWriter.FinishMessage();
			steamNetworkConnection.SendWriter(networkWriter, QosChannelIndex.defaultReliable.intVal);
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x001283E8 File Offset: 0x001265E8
		protected override void PlatformClientSetPlayers(ConCommandArgs args)
		{
			if (this.client != null && this.client.connection != null)
			{
				base.ClientSetPlayers(this.client.connection);
			}
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x0012BD28 File Offset: 0x00129F28
		protected override void PlatformConnectP2P(ConCommandArgs args)
		{
			NetworkManagerSystemSteam.CheckSteamworks();
			UserID userID;
			userID.CID = args.GetArgSteamID(0);
			if (Client.Instance.Lobby.IsValid && !PlatformSystems.lobbyManager.ownsLobby && userID.CID != PlatformSystems.lobbyManager.newestLobbyData.serverId)
			{
				Debug.LogFormat("Cannot connect to server {0}: Server is not the one specified by the current steam lobby.", new object[]
				{
					userID
				});
				return;
			}
			if (base.clientP2PId == userID.CID)
			{
				return;
			}
			NetworkManagerSystem.singleton.desiredHost = new HostDescription(userID, HostDescription.HostType.Steam);
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x00128410 File Offset: 0x00126610
		protected override void PlatformDisconnect(ConCommandArgs args)
		{
			NetworkManagerSystem.singleton.desiredHost = HostDescription.none;
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x0012BDC4 File Offset: 0x00129FC4
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

		// Token: 0x060048CD RID: 18637 RVA: 0x0012BE24 File Offset: 0x0012A024
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
					maxPlayers = PlatformSystems.lobbyManager.newestLobbyData.totalMaxPlayers;
				}
				NetworkManagerSystem.singleton.desiredHost = new HostDescription(new HostDescription.HostingParameters
				{
					listen = argBool,
					maxPlayers = maxPlayers
				});
			}
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x0012BEC8 File Offset: 0x0012A0C8
		protected override void PlatformGetP2PSessionState(ConCommandArgs args)
		{
			NetworkManagerSystemSteam.CheckSteamworks();
			CSteamID argSteamID = args.GetArgSteamID(0);
			if (!NetworkManagerSystem.singleton)
			{
				return;
			}
			Networking.P2PSessionState p2PSessionState = default(Networking.P2PSessionState);
			if (Client.Instance.Networking.GetP2PSessionState(argSteamID.steamValue, ref p2PSessionState))
			{
				Debug.LogFormat("ConnectionActive={0}\nConnecting={1}\nP2PSessionError={2}\nUsingRelay={3}\nBytesQueuedForSend={4}\nPacketsQueuedForSend={5}\nRemoteIP={6}\nRemotePort={7}", new object[]
				{
					p2PSessionState.ConnectionActive,
					p2PSessionState.Connecting,
					p2PSessionState.P2PSessionError,
					p2PSessionState.UsingRelay,
					p2PSessionState.BytesQueuedForSend,
					p2PSessionState.PacketsQueuedForSend,
					p2PSessionState.RemoteIP,
					p2PSessionState.RemotePort
				});
				return;
			}
			Debug.LogFormat("Could not get p2p session info for steamId={0}", new object[]
			{
				argSteamID
			});
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x0012BFB0 File Offset: 0x0012A1B0
		protected override void PlatformKick(ConCommandArgs args)
		{
			NetworkManagerSystemSteam.CheckSteamworks();
			UserID clientId = new UserID(args.GetArgSteamID(0));
			NetworkConnection client = NetworkManagerSystem.singleton.GetClient(clientId);
			if (client != null)
			{
				NetworkManagerSystem.singleton.ServerKickClient(client, new NetworkManagerSystem.SimpleLocalizedKickReason("KICK_REASON_KICK", Array.Empty<string>()));
			}
		}

		// Token: 0x060048D0 RID: 18640 RVA: 0x0012BFFC File Offset: 0x0012A1FC
		protected override void PlatformBan(ConCommandArgs args)
		{
			NetworkManagerSystemSteam.CheckSteamworks();
			UserID clientId = new UserID(args.GetArgSteamID(0));
			NetworkConnection client = NetworkManagerSystem.singleton.GetClient(clientId);
			if (client != null)
			{
				NetworkManagerSystem.singleton.ServerBanClient(client);
				NetworkManagerSystem.singleton.ServerKickClient(client, new NetworkManagerSystem.SimpleLocalizedKickReason("KICK_REASON_BAN", Array.Empty<string>()));
			}
		}

		// Token: 0x060048D1 RID: 18641 RVA: 0x001285FD File Offset: 0x001267FD
		public static void CheckSteamworks()
		{
			if (Client.Instance == null)
			{
				throw new ConCommandException("Steamworks not available.");
			}
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x000026ED File Offset: 0x000008ED
		public override void CreateLocalLobby()
		{
		}

		// Token: 0x04004583 RID: 17795
		private BaseSteamworks clientSteamworks;

		// Token: 0x04004584 RID: 17796
		private BaseSteamworks serverSteamworks;

		// Token: 0x04004585 RID: 17797
		protected List<CSteamID> steamIdBanList = new List<CSteamID>();

		// Token: 0x04004586 RID: 17798
		private static readonly BoolConVar cvSteamP2PUseSteamServer = new BoolConVar("steam_p2p_use_steam_server", ConVarFlags.None, "0", "Whether or not to use the Steam server interface to receive network traffic. Setting to false will cause the traffic to be handled by the Steam client interface instead. Only takes effect on server startup.");
	}
}
