using System;
using System.Collections.Generic;
using System.Net;
using Facepunch.Steamworks;
using RoR2.ConVar;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace RoR2
{
	// Token: 0x02000A75 RID: 2677
	internal sealed class SteamworksServerManager : ServerManagerBase<SteamworksServerManager>, IDisposable
	{
		// Token: 0x06003D87 RID: 15751 RVA: 0x000FE1CC File Offset: 0x000FC3CC
		public SteamworksServerManager()
		{
			string modDir = "Risk of Rain 2";
			string gameDesc = "Risk of Rain 2";
			this.steamworksServer = new Server(632360U, new ServerInit(modDir, gameDesc)
			{
				IpAddress = IPAddress.Any,
				Secure = true,
				VersionString = RoR2Application.GetBuildId(),
				GamePort = NetworkManagerSystem.SvPortConVar.instance.value,
				QueryPort = SteamworksServerManager.cvSteamServerQueryPort.value,
				SteamPort = SteamworksServerManager.cvSteamServerSteamPort.value,
				GameData = ServerManagerBase<SteamworksServerManager>.GetVersionGameDataString() + "," + NetworkModCompatibilityHelper.steamworksGameserverGameDataValue
			});
			Debug.LogFormat("steamworksServer.IsValid={0}", new object[]
			{
				this.steamworksServer.IsValid
			});
			if (!this.steamworksServer.IsValid)
			{
				this.Dispose();
				return;
			}
			this.steamworksServer.Auth.OnAuthChange = new Action<ulong, ulong, ServerAuth.Status>(this.OnAuthChange);
			this.steamworksServer.MaxPlayers = this.GetMaxPlayers();
			this.UpdateHostName(NetworkManagerSystem.SvHostNameConVar.instance.GetString());
			NetworkManagerSystem.SvHostNameConVar.instance.onValueChanged += this.UpdateHostName;
			this.UpdateMapName(SceneManager.GetActiveScene().name);
			NetworkManagerSystem.onServerSceneChangedGlobal += this.UpdateMapName;
			this.UpdatePassword(NetworkManagerSystem.SvPasswordConVar.instance.value);
			NetworkManagerSystem.SvPasswordConVar.instance.onValueChanged += this.UpdatePassword;
			this.steamworksServer.DedicatedServer = false;
			this.steamworksServer.AutomaticHeartbeats = SteamworksServerManager.SteamServerHeartbeatEnabledConVar.instance.value;
			this.steamworksServer.LogOnAnonymous();
			Debug.LogFormat("steamworksServer.LoggedOn={0}", new object[]
			{
				this.steamworksServer.LoggedOn
			});
			RoR2Application.onUpdate += this.Update;
			NetworkManagerSystem.onServerConnectGlobal += this.OnServerConnectClient;
			NetworkManagerSystem.onServerDisconnectGlobal += this.OnServerDisconnectClient;
			ServerAuthManager.onAuthDataReceivedFromClient += this.OnAuthDataReceivedFromClient;
			ServerAuthManager.onAuthExpired += this.OnAuthExpired;
			Run.onServerRunSetRuleBookGlobal += base.OnServerRunSetRuleBookGlobal;
			PreGameController.onPreGameControllerSetRuleBookServerGlobal += base.OnPreGameControllerSetRuleBookServerGlobal;
			NetworkUser.onNetworkUserDiscovered += this.OnNetworkUserDiscovered;
			NetworkUser.onNetworkUserLost += this.OnNetworkUserLost;
			this.steamworksServer.SetKey("Test", "Value");
			this.steamworksServer.SetKey("gameMode", PreGameController.GameModeConVar.instance.GetString());
			this.steamworksServer.SetKey("buildId", RoR2Application.GetBuildId());
			this.steamworksServer.SetKey("modHash", NetworkModCompatibilityHelper.networkModHash);
			this.ruleBookKvHelper = new KeyValueSplitter("ruleBook", 2048, 2048, new Action<string, string>(this.steamworksServer.SetKey));
			this.modListKvHelper = new KeyValueSplitter(NetworkModCompatibilityHelper.steamworksGameserverRulesBaseName, 2048, 2048, new Action<string, string>(this.steamworksServer.SetKey));
			this.modListKvHelper.SetValue(NetworkModCompatibilityHelper.steamworksGameserverGameRulesValue);
			this.steamworksServer.ForceHeartbeat();
			base.UpdateServerRuleBook();
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x000FE4F6 File Offset: 0x000FC6F6
		protected override void TagsStringUpdated()
		{
			base.TagsStringUpdated();
			this.steamworksServer.GameTags = base.tagsString;
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x000FE510 File Offset: 0x000FC710
		private void OnAuthExpired(NetworkConnection conn, ClientAuthData authData)
		{
			SteamNetworkConnection steamNetworkConnection = conn as SteamNetworkConnection;
			CSteamID? csteamID = (steamNetworkConnection != null) ? new CSteamID?(steamNetworkConnection.steamId) : ((authData != null) ? new CSteamID?(authData.steamId) : null);
			if (csteamID != null)
			{
				this.steamworksServer.Auth.EndSession(csteamID.Value.steamValue);
			}
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x000FE574 File Offset: 0x000FC774
		private void OnAuthDataReceivedFromClient(NetworkConnection conn, ClientAuthData authData)
		{
			CSteamID steamId = authData.steamId;
			SteamNetworkConnection steamNetworkConnection;
			if ((steamNetworkConnection = (conn as SteamNetworkConnection)) != null)
			{
				steamId = steamNetworkConnection.steamId;
			}
			this.steamworksServer.Auth.StartSession(authData.authTicket, steamId.steamValue);
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x000026ED File Offset: 0x000008ED
		private void OnServerConnectClient(NetworkConnection conn)
		{
		}

		// Token: 0x06003D8C RID: 15756 RVA: 0x000026ED File Offset: 0x000008ED
		private void OnServerDisconnectClient(NetworkConnection conn)
		{
		}

		// Token: 0x06003D8D RID: 15757 RVA: 0x000FE5B8 File Offset: 0x000FC7B8
		public override void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			Server server = this.steamworksServer;
			if (server != null)
			{
				server.Dispose();
			}
			this.steamworksServer = null;
			RoR2Application.onUpdate -= this.Update;
			NetworkManagerSystem.SvHostNameConVar.instance.onValueChanged -= this.UpdateHostName;
			NetworkManagerSystem.SvPasswordConVar.instance.onValueChanged -= this.UpdatePassword;
			NetworkManagerSystem.onServerSceneChangedGlobal -= this.UpdateMapName;
			NetworkManagerSystem.onServerConnectGlobal -= this.OnServerConnectClient;
			NetworkManagerSystem.onServerDisconnectGlobal -= this.OnServerDisconnectClient;
			ServerAuthManager.onAuthDataReceivedFromClient -= this.OnAuthDataReceivedFromClient;
			ServerAuthManager.onAuthExpired -= this.OnAuthExpired;
			Run.onServerRunSetRuleBookGlobal -= base.OnServerRunSetRuleBookGlobal;
			PreGameController.onPreGameControllerSetRuleBookServerGlobal -= base.OnPreGameControllerSetRuleBookServerGlobal;
			NetworkUser.onNetworkUserDiscovered -= this.OnNetworkUserDiscovered;
			NetworkUser.onNetworkUserLost -= this.OnNetworkUserLost;
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x000FE6C4 File Offset: 0x000FC8C4
		private int GetMaxPlayers()
		{
			return NetworkManagerSystem.singleton.maxConnections;
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x000FE6D0 File Offset: 0x000FC8D0
		private void OnNetworkUserLost(NetworkUser networkUser)
		{
			this.UpdateBotPlayerCount();
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x000FE6D0 File Offset: 0x000FC8D0
		private void OnNetworkUserDiscovered(NetworkUser networkUser)
		{
			this.UpdateBotPlayerCount();
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x000FE6D8 File Offset: 0x000FC8D8
		private void UpdateBotPlayerCount()
		{
			int num = 0;
			using (IEnumerator<NetworkUser> enumerator = NetworkUser.readOnlyInstancesList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.isSplitScreenExtraPlayer)
					{
						num++;
					}
				}
			}
			this.steamworksServer.BotCount = num;
		}

		// Token: 0x06003D92 RID: 15762 RVA: 0x000FE738 File Offset: 0x000FC938
		private void UpdateHostName(string newHostName)
		{
			this.steamworksServer.ServerName = newHostName;
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x000FE746 File Offset: 0x000FC946
		private void UpdateMapName(string sceneName)
		{
			this.steamworksServer.MapName = sceneName;
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x000FE754 File Offset: 0x000FC954
		private void UpdatePassword(string newPassword)
		{
			this.steamworksServer.Passworded = (newPassword.Length > 0);
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x000FE76A File Offset: 0x000FC96A
		private void OnAddressDiscovered()
		{
			Debug.Log("Steamworks Server IP discovered.");
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x000FE778 File Offset: 0x000FC978
		private void RefreshSteamServerPlayers()
		{
			foreach (NetworkUser networkUser in NetworkUser.readOnlyInstancesList)
			{
				ClientAuthData clientAuthData = ServerAuthManager.FindAuthData(networkUser.connectionToClient);
				if (clientAuthData != null)
				{
					this.steamworksServer.UpdatePlayer(clientAuthData.steamId.steamValue, networkUser.userName, 0);
				}
			}
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x000FE7EC File Offset: 0x000FC9EC
		protected override void Update()
		{
			this.steamworksServer.Update();
			this.playerUpdateTimer -= Time.unscaledDeltaTime;
			if (this.playerUpdateTimer <= 0f)
			{
				this.playerUpdateTimer = this.playerUpdateInterval;
				this.RefreshSteamServerPlayers();
			}
			if (this.address == null)
			{
				this.address = this.steamworksServer.PublicIp;
				if (this.address != null)
				{
					this.OnAddressDiscovered();
				}
			}
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x000FE85C File Offset: 0x000FCA5C
		private void OnAuthChange(ulong steamId, ulong ownerId, ServerAuth.Status status)
		{
			NetworkConnection networkConnection = ServerAuthManager.FindConnectionForSteamID(new CSteamID(steamId));
			if (networkConnection == null)
			{
				Debug.LogWarningFormat("SteamworksServerManager.OnAuthChange(steamId={0}, ownerId={1}, status={2}): Could not find connection for steamId.", new object[]
				{
					steamId,
					ownerId,
					status
				});
				return;
			}
			switch (status)
			{
			case ServerAuth.Status.OK:
				return;
			case ServerAuth.Status.UserNotConnectedToSteam:
			case ServerAuth.Status.NoLicenseOrExpired:
			case ServerAuth.Status.VACBanned:
			case ServerAuth.Status.LoggedInElseWhere:
			case ServerAuth.Status.VACCheckTimedOut:
			case ServerAuth.Status.AuthTicketCanceled:
			case ServerAuth.Status.AuthTicketInvalidAlreadyUsed:
			case ServerAuth.Status.AuthTicketInvalid:
			case ServerAuth.Status.PublisherIssuedBan:
				NetworkManagerSystem.singleton.ServerKickClient(networkConnection, new NetworkManagerSystem.SimpleLocalizedKickReason("KICK_REASON_STEAMWORKS_AUTH_FAILURE", new string[]
				{
					status.ToString()
				}));
				return;
			default:
				throw new ArgumentOutOfRangeException("status", status, null);
			}
		}

		// Token: 0x06003D99 RID: 15769 RVA: 0x000FE912 File Offset: 0x000FCB12
		[ConCommand(commandName = "steam_server_force_heartbeat", flags = ConVarFlags.None, helpText = "Forces the server to issue a heartbeat to the master server.")]
		private static void CCSteamServerForceHeartbeat(ConCommandArgs args)
		{
			SteamworksServerManager instance = ServerManagerBase<SteamworksServerManager>.instance;
			Server server = (instance != null) ? instance.steamworksServer : null;
			if (server == null)
			{
				throw new ConCommandException("No Steamworks server is running.");
			}
			server.ForceHeartbeat();
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x000FE938 File Offset: 0x000FCB38
		[ConCommand(commandName = "steam_server_print_info", flags = ConVarFlags.None, helpText = "Prints debug info about the currently hosted Steamworks server.")]
		private static void CCSteamServerPrintInfo(ConCommandArgs args)
		{
			SteamworksServerManager instance = ServerManagerBase<SteamworksServerManager>.instance;
			Server server = (instance != null) ? instance.steamworksServer : null;
			if (server == null)
			{
				throw new ConCommandException("No Steamworks server is running.");
			}
			Debug.Log("" + string.Format("IsValid={0}\n", server.IsValid) + string.Format("Product={0}\n", server.Product) + string.Format("ModDir={0}\n", server.ModDir) + string.Format("SteamId={0}\n", server.SteamId) + string.Format("DedicatedServer={0}\n", server.DedicatedServer) + string.Format("LoggedOn={0}\n", server.LoggedOn) + string.Format("ServerName={0}\n", server.ServerName) + string.Format("PublicIp={0}\n", server.PublicIp) + string.Format("Passworded={0}\n", server.Passworded) + string.Format("MaxPlayers={0}\n", server.MaxPlayers) + string.Format("BotCount={0}\n", server.BotCount) + string.Format("MapName={0}\n", server.MapName) + string.Format("GameDescription={0}\n", server.GameDescription) + string.Format("GameTags={0}\n", server.GameTags));
		}

		// Token: 0x04003C77 RID: 15479
		private Server steamworksServer;

		// Token: 0x04003C78 RID: 15480
		private IPAddress address;

		// Token: 0x04003C79 RID: 15481
		private static readonly SteamworksServerManager.SteamServerPortConVar cvSteamServerQueryPort = new SteamworksServerManager.SteamServerPortConVar("steam_server_query_port", ConVarFlags.Engine, "27016", "The port for queries.");

		// Token: 0x04003C7A RID: 15482
		private static readonly SteamworksServerManager.SteamServerPortConVar cvSteamServerSteamPort = new SteamworksServerManager.SteamServerPortConVar("steam_server_steam_port", ConVarFlags.Engine, "0", "The port for steam. 0 for a random port in the range 10000-60000.");

		// Token: 0x02000A76 RID: 2678
		private sealed class SteamServerHeartbeatEnabledConVar : BaseConVar
		{
			// Token: 0x170005C7 RID: 1479
			// (get) Token: 0x06003D9C RID: 15772 RVA: 0x000FEAF0 File Offset: 0x000FCCF0
			// (set) Token: 0x06003D9D RID: 15773 RVA: 0x000FEAF8 File Offset: 0x000FCCF8
			public bool value { get; private set; }

			// Token: 0x06003D9E RID: 15774 RVA: 0x00009F73 File Offset: 0x00008173
			public SteamServerHeartbeatEnabledConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003D9F RID: 15775 RVA: 0x000FEB04 File Offset: 0x000FCD04
			public override void SetString(string newValueString)
			{
				int num;
				if (TextSerialization.TryParseInvariant(newValueString, out num))
				{
					bool flag = num != 0;
					if (flag != this.value)
					{
						this.value = flag;
						if (ServerManagerBase<SteamworksServerManager>.instance != null)
						{
							ServerManagerBase<SteamworksServerManager>.instance.steamworksServer.AutomaticHeartbeats = this.value;
						}
					}
				}
			}

			// Token: 0x06003DA0 RID: 15776 RVA: 0x000FEB4C File Offset: 0x000FCD4C
			public override string GetString()
			{
				if (!this.value)
				{
					return "0";
				}
				return "1";
			}

			// Token: 0x04003C7B RID: 15483
			public static readonly SteamworksServerManager.SteamServerHeartbeatEnabledConVar instance = new SteamworksServerManager.SteamServerHeartbeatEnabledConVar("steam_server_heartbeat_enabled", ConVarFlags.Engine, null, "Whether or not this server issues any heartbeats to the Steam master server and effectively advertises it in the master server list. Default is 1 for dedicated servers, 0 for client builds.");
		}

		// Token: 0x02000A77 RID: 2679
		public class SteamServerPortConVar : BaseConVar
		{
			// Token: 0x170005C8 RID: 1480
			// (get) Token: 0x06003DA2 RID: 15778 RVA: 0x000FEB7A File Offset: 0x000FCD7A
			// (set) Token: 0x06003DA3 RID: 15779 RVA: 0x000FEB82 File Offset: 0x000FCD82
			public ushort value { get; private set; }

			// Token: 0x06003DA4 RID: 15780 RVA: 0x00009F73 File Offset: 0x00008173
			public SteamServerPortConVar(string name, ConVarFlags flags, string defaultValue, string helpText) : base(name, flags, defaultValue, helpText)
			{
			}

			// Token: 0x06003DA5 RID: 15781 RVA: 0x000FEB8C File Offset: 0x000FCD8C
			public override void SetString(string newValueString)
			{
				if (NetworkServer.active)
				{
					throw new ConCommandException("Cannot change this convar while the server is running.");
				}
				ushort value;
				if (TextSerialization.TryParseInvariant(newValueString, out value))
				{
					this.value = value;
				}
			}

			// Token: 0x06003DA6 RID: 15782 RVA: 0x000FEBBC File Offset: 0x000FCDBC
			public override string GetString()
			{
				return this.value.ToString();
			}
		}
	}
}
