using System;
using HG;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C4B RID: 3147
	public static class ServerAuthManager
	{
		// Token: 0x0600473A RID: 18234 RVA: 0x001260C1 File Offset: 0x001242C1
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			NetworkManagerSystem.onServerConnectGlobal += ServerAuthManager.OnConnectionDiscovered;
			NetworkManagerSystem.onServerDisconnectGlobal += ServerAuthManager.OnConnectionLost;
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x000026ED File Offset: 0x000008ED
		private static void OnConnectionDiscovered(NetworkConnection conn)
		{
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x001260E8 File Offset: 0x001242E8
		private static void OnConnectionLost(NetworkConnection conn)
		{
			for (int i = 0; i < ServerAuthManager.instanceCount; i++)
			{
				if (ServerAuthManager.instances[i].conn == conn)
				{
					Action<NetworkConnection, ClientAuthData> action = ServerAuthManager.onAuthExpired;
					if (action != null)
					{
						action(conn, ServerAuthManager.instances[i].authData);
					}
					ArrayUtils.ArrayRemoveAt<ServerAuthManager.KeyValue>(ServerAuthManager.instances, ref ServerAuthManager.instanceCount, i, 1);
					return;
				}
			}
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x0012614C File Offset: 0x0012434C
		public static ClientAuthData FindAuthData(NetworkConnection conn)
		{
			for (int i = 0; i < ServerAuthManager.instanceCount; i++)
			{
				if (ServerAuthManager.instances[i].conn == conn)
				{
					return ServerAuthManager.instances[i].authData;
				}
			}
			return null;
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x00126190 File Offset: 0x00124390
		public static NetworkConnection FindConnectionForSteamID(CSteamID steamId)
		{
			for (int i = 0; i < ServerAuthManager.instanceCount; i++)
			{
				if (ServerAuthManager.instances[i].authData.steamId == steamId)
				{
					return ServerAuthManager.instances[i].conn;
				}
			}
			return null;
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x001261DC File Offset: 0x001243DC
		[NetworkMessageHandler(client = false, server = true, msgType = 74)]
		private static void HandleSetClientAuth(NetworkMessage netMsg)
		{
			if (netMsg.conn == null)
			{
				Debug.LogWarning("ServerAuthManager.HandleSetClientAuth(): Connection is null.");
				return;
			}
			if (ServerAuthManager.FindAuthData(netMsg.conn) != null)
			{
				return;
			}
			bool flag = Util.ConnectionIsLocal(netMsg.conn);
			NetworkManagerSystem.BaseKickReason baseKickReason = null;
			try
			{
				ClientAuthData clientAuthData = netMsg.ReadMessage<ClientAuthData>();
				NetworkConnection networkConnection = ServerAuthManager.FindConnectionForSteamID(clientAuthData.steamId);
				if (networkConnection != null)
				{
					Debug.LogFormat("SteamID {0} is already claimed by connection [{1}]. Connection [{2}] rejected.", new object[]
					{
						clientAuthData.steamId,
						networkConnection,
						netMsg.conn
					});
					PlatformSystems.networkManager.ServerKickClient(netMsg.conn, new NetworkManagerSystem.SimpleLocalizedKickReason("KICK_REASON_ACCOUNT_ALREADY_ON_SERVER", Array.Empty<string>()));
					return;
				}
				ServerAuthManager.KeyValue keyValue = new ServerAuthManager.KeyValue(netMsg.conn, clientAuthData);
				ArrayUtils.ArrayAppend<ServerAuthManager.KeyValue>(ref ServerAuthManager.instances, ref ServerAuthManager.instanceCount, keyValue);
				string value = NetworkManagerSystem.SvPasswordConVar.instance.value;
				if (!flag && value.Length != 0 && !(clientAuthData.password == value))
				{
					Debug.LogFormat("Rejecting connection from [{0}]: {1}", new object[]
					{
						netMsg.conn,
						"Bad password."
					});
					baseKickReason = new NetworkManagerSystem.SimpleLocalizedKickReason("KICK_REASON_BADPASSWORD", Array.Empty<string>());
				}
				string version = clientAuthData.version;
				string buildId = RoR2Application.GetBuildId();
				if (!string.Equals(version, buildId, StringComparison.OrdinalIgnoreCase))
				{
					Debug.LogFormat("Rejecting connection from [{0}]: {1}", new object[]
					{
						netMsg.conn,
						"Bad version."
					});
					baseKickReason = new NetworkManagerSystem.SimpleLocalizedKickReason("KICK_REASON_BADVERSION", new string[]
					{
						version,
						buildId
					});
				}
				string modHash = clientAuthData.modHash;
				string networkModHash = NetworkModCompatibilityHelper.networkModHash;
				if (!string.Equals(modHash, networkModHash, StringComparison.OrdinalIgnoreCase))
				{
					Debug.LogFormat("Rejecting connection from [{0}]: {1}", new object[]
					{
						netMsg.conn,
						"Mod mismatch."
					});
					baseKickReason = new NetworkManagerSystem.ModMismatchKickReason(NetworkModCompatibilityHelper.networkModList);
				}
				Action<NetworkConnection, ClientAuthData> action = ServerAuthManager.onAuthDataReceivedFromClient;
				if (action != null)
				{
					action(keyValue.conn, keyValue.authData);
				}
			}
			catch
			{
				Debug.LogFormat("Rejecting connection from [{0}]: {1}", new object[]
				{
					netMsg.conn,
					"Malformed auth data."
				});
				baseKickReason = new NetworkManagerSystem.SimpleLocalizedKickReason("KICK_REASON_MALFORMED_AUTH_DATA", Array.Empty<string>());
			}
			if (baseKickReason != null)
			{
				PlatformSystems.networkManager.ServerKickClient(netMsg.conn, baseKickReason);
			}
		}

		// Token: 0x140000E6 RID: 230
		// (add) Token: 0x06004740 RID: 18240 RVA: 0x00126418 File Offset: 0x00124618
		// (remove) Token: 0x06004741 RID: 18241 RVA: 0x0012644C File Offset: 0x0012464C
		public static event Action<NetworkConnection, ClientAuthData> onAuthDataReceivedFromClient;

		// Token: 0x140000E7 RID: 231
		// (add) Token: 0x06004742 RID: 18242 RVA: 0x00126480 File Offset: 0x00124680
		// (remove) Token: 0x06004743 RID: 18243 RVA: 0x001264B4 File Offset: 0x001246B4
		public static event Action<NetworkConnection, ClientAuthData> onAuthExpired;

		// Token: 0x06004744 RID: 18244 RVA: 0x001264E8 File Offset: 0x001246E8
		[CanBeNull]
		public static ClientAuthData GetClientAuthData(NetworkConnection networkConnection)
		{
			for (int i = 0; i < ServerAuthManager.instances.Length; i++)
			{
				if (ServerAuthManager.instances[i].conn == networkConnection)
				{
					return ServerAuthManager.instances[i].authData;
				}
			}
			return null;
		}

		// Token: 0x040044D9 RID: 17625
		private static readonly int initialSize = 16;

		// Token: 0x040044DA RID: 17626
		public static ServerAuthManager.KeyValue[] instances = new ServerAuthManager.KeyValue[ServerAuthManager.initialSize];

		// Token: 0x040044DB RID: 17627
		private static int instanceCount = 0;

		// Token: 0x02000C4C RID: 3148
		public struct KeyValue
		{
			// Token: 0x06004746 RID: 18246 RVA: 0x0012654A File Offset: 0x0012474A
			public KeyValue(NetworkConnection conn, ClientAuthData authData)
			{
				this.conn = conn;
				this.authData = authData;
			}

			// Token: 0x040044DE RID: 17630
			public readonly NetworkConnection conn;

			// Token: 0x040044DF RID: 17631
			public readonly ClientAuthData authData;
		}
	}
}
