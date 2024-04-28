using System;
using System.Collections.Generic;
using Facepunch.Steamworks;
using JetBrains.Annotations;
using RoR2.ConVar;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C56 RID: 3158
	public class SteamNetworkConnection : NetworkConnection
	{
		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x0600477A RID: 18298 RVA: 0x001270B6 File Offset: 0x001252B6
		// (set) Token: 0x0600477B RID: 18299 RVA: 0x001270BE File Offset: 0x001252BE
		public CSteamID myId { get; private set; }

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x0600477C RID: 18300 RVA: 0x001270C7 File Offset: 0x001252C7
		// (set) Token: 0x0600477D RID: 18301 RVA: 0x001270CF File Offset: 0x001252CF
		public CSteamID steamId { get; private set; }

		// Token: 0x0600477E RID: 18302 RVA: 0x00124E20 File Offset: 0x00123020
		public SteamNetworkConnection()
		{
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x001270D8 File Offset: 0x001252D8
		public SteamNetworkConnection([NotNull] BaseSteamworks steamworks, CSteamID endpointId)
		{
			Client client = steamworks as Client;
			ulong value;
			if (client == null)
			{
				Server server = steamworks as Server;
				value = ((server != null) ? server.SteamId : 0UL);
			}
			else
			{
				value = client.SteamId;
			}
			this.myId = new CSteamID(value);
			this.steamId = endpointId;
			this.steamworks = steamworks;
			this.networking = steamworks.Networking;
			this.networking.CloseSession(endpointId.steamValue);
			SteamNetworkConnection.instancesList.Add(this);
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x00127151 File Offset: 0x00125351
		public bool SendConnectionRequest()
		{
			return this.networking.SendP2PPacket(this.steamId.steamValue, null, 0, Networking.SendType.Reliable, 0);
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x00127170 File Offset: 0x00125370
		public override bool TransportSend(byte[] bytes, int numBytes, int channelId, out byte error)
		{
			if (this.ignore)
			{
				error = 0;
				return true;
			}
			this.logNetworkMessages = SteamNetworkConnection.cvNetP2PLogMessages.value;
			if (this.steamId == this.myId)
			{
				this.TransportReceive(bytes, numBytes, channelId);
				error = 0;
				if (SteamNetworkConnection.cvNetP2PDebugTransport.value)
				{
					Debug.LogFormat("SteamNetworkConnection.TransportSend steamId=self numBytes={1} channelId={2}", new object[]
					{
						numBytes,
						channelId
					});
				}
				return true;
			}
			Networking.SendType eP2PSendType = Networking.SendType.Reliable;
			QosType qos = PlatformSystems.networkManager.connectionConfig.Channels[channelId].QOS;
			if (qos == QosType.Unreliable || qos == QosType.UnreliableFragmented || qos == QosType.UnreliableSequenced)
			{
				eP2PSendType = Networking.SendType.Unreliable;
			}
			if (this.networking.SendP2PPacket(this.steamId.steamValue, bytes, numBytes, eP2PSendType, 0))
			{
				error = 0;
				if (SteamNetworkConnection.cvNetP2PDebugTransport.value)
				{
					Debug.LogFormat("SteamNetworkConnection.TransportSend steamId={0} numBytes={1} channelId={2} error={3}", new object[]
					{
						this.steamId.value,
						numBytes,
						channelId,
						error
					});
				}
				return true;
			}
			error = 1;
			if (SteamNetworkConnection.cvNetP2PDebugTransport.value)
			{
				Debug.LogFormat("SteamNetworkConnection.TransportSend steamId={0} numBytes={1} channelId={2} error={3}", new object[]
				{
					this.steamId.value,
					numBytes,
					channelId,
					error
				});
			}
			return false;
		}

		// Token: 0x06004782 RID: 18306 RVA: 0x001272D0 File Offset: 0x001254D0
		public override void TransportReceive(byte[] bytes, int numBytes, int channelId)
		{
			if (this.ignore)
			{
				return;
			}
			this.logNetworkMessages = SteamNetworkConnection.cvNetP2PLogMessages.value;
			base.TransportReceive(bytes, numBytes, channelId);
		}

		// Token: 0x06004783 RID: 18307 RVA: 0x001272F4 File Offset: 0x001254F4
		protected override void Dispose(bool disposing)
		{
			SteamNetworkConnection.instancesList.Remove(this);
			if (this.networking != null && this.steamId.steamValue != 0UL)
			{
				this.networking.CloseSession(this.steamId.steamValue);
				this.steamId = CSteamID.nil;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06004784 RID: 18308 RVA: 0x0012734C File Offset: 0x0012554C
		public static SteamNetworkConnection Find(BaseSteamworks owner, CSteamID endpoint)
		{
			for (int i = 0; i < SteamNetworkConnection.instancesList.Count; i++)
			{
				SteamNetworkConnection steamNetworkConnection = SteamNetworkConnection.instancesList[i];
				if (steamNetworkConnection.steamId == endpoint && owner == steamNetworkConnection.steamworks)
				{
					return steamNetworkConnection;
				}
			}
			return null;
		}

		// Token: 0x040044F1 RID: 17649
		private BaseSteamworks steamworks;

		// Token: 0x040044F2 RID: 17650
		private Networking networking;

		// Token: 0x040044F3 RID: 17651
		private static List<SteamNetworkConnection> instancesList = new List<SteamNetworkConnection>();

		// Token: 0x040044F4 RID: 17652
		public bool ignore;

		// Token: 0x040044F5 RID: 17653
		public uint rtt;

		// Token: 0x040044F6 RID: 17654
		public static BoolConVar cvNetP2PDebugTransport = new BoolConVar("net_p2p_debug_transport", ConVarFlags.None, "0", "Allows p2p transport information to print to the console.");

		// Token: 0x040044F7 RID: 17655
		private static BoolConVar cvNetP2PLogMessages = new BoolConVar("net_p2p_log_messages", ConVarFlags.None, "0", "Enables logging of network messages.");
	}
}
