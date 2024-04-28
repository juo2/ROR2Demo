using System;
using System.Collections.Generic;
using System.Linq;
using Epic.OnlineServices;
using Epic.OnlineServices.P2P;
using RoR2.ConVar;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C3C RID: 3132
	public class EOSNetworkConnection : NetworkConnection
	{
		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060046D5 RID: 18133 RVA: 0x00124E07 File Offset: 0x00123007
		public ProductUserId LocalUserID { get; }

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060046D6 RID: 18134 RVA: 0x00124E0F File Offset: 0x0012300F
		// (set) Token: 0x060046D7 RID: 18135 RVA: 0x00124E17 File Offset: 0x00123017
		public ProductUserId RemoteUserID { get; private set; }

		// Token: 0x060046D8 RID: 18136 RVA: 0x00124E20 File Offset: 0x00123020
		public EOSNetworkConnection()
		{
		}

		// Token: 0x060046D9 RID: 18137 RVA: 0x00124E28 File Offset: 0x00123028
		public EOSNetworkConnection(ProductUserId localUserID, ProductUserId remoteUserID)
		{
			this.LocalUserID = localUserID;
			this.RemoteUserID = remoteUserID;
			EOSNetworkConnection.instancesList.Add(this);
		}

		// Token: 0x060046DA RID: 18138 RVA: 0x00124E4C File Offset: 0x0012304C
		public bool SendConnectionRequest()
		{
			return NetworkManagerSystemEOS.P2pInterface.SendPacket(new SendPacketOptions
			{
				LocalUserId = this.LocalUserID,
				RemoteUserId = this.RemoteUserID,
				AllowDelayedDelivery = true,
				Channel = 0,
				Data = null,
				Reliability = PacketReliability.ReliableOrdered,
				SocketId = NetworkManagerSystemEOS.socketId
			}) == Result.Success;
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x00124EAC File Offset: 0x001230AC
		public override bool TransportSend(byte[] bytes, int numBytes, int channelId, out byte error)
		{
			if (this.ignore)
			{
				error = 0;
				return true;
			}
			this.logNetworkMessages = EOSNetworkConnection.cvNetP2PLogMessages.value;
			if (this.LocalUserID == this.RemoteUserID)
			{
				this.TransportReceive(bytes, numBytes, channelId);
				error = 0;
				if (EOSNetworkConnection.cvNetP2PDebugTransport.value)
				{
					Debug.Log(string.Format("EOSNetworkConnection.TransportSend {0}=self {1}={2} {3}={4}", new object[]
					{
						"RemoteUserID",
						"numBytes",
						numBytes,
						"channelId",
						channelId
					}));
				}
				return true;
			}
			QosType qos = NetworkManagerSystem.singleton.connectionConfig.Channels[channelId].QOS;
			PacketReliability reliability;
			if (qos == QosType.Unreliable || qos == QosType.UnreliableFragmented || qos == QosType.UnreliableSequenced)
			{
				reliability = PacketReliability.UnreliableUnordered;
			}
			else
			{
				reliability = PacketReliability.ReliableOrdered;
			}
			byte[] array = new byte[numBytes];
			Array.Copy(bytes, 0, array, 0, numBytes);
			SendPacketOptions options = new SendPacketOptions
			{
				AllowDelayedDelivery = true,
				Data = array,
				Channel = 0,
				LocalUserId = this.LocalUserID,
				RemoteUserId = this.RemoteUserID,
				SocketId = NetworkManagerSystemEOS.socketId,
				Reliability = reliability
			};
			if (NetworkManagerSystemEOS.P2pInterface.SendPacket(options) == Result.Success)
			{
				error = 0;
				return true;
			}
			error = 1;
			return false;
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x00124FE0 File Offset: 0x001231E0
		public override void TransportReceive(byte[] bytes, int numBytes, int channelId)
		{
			if (this.ignore)
			{
				return;
			}
			this.logNetworkMessages = EOSNetworkConnection.cvNetP2PLogMessages.value;
			base.TransportReceive(bytes, numBytes, channelId);
		}

		// Token: 0x060046DD RID: 18141 RVA: 0x00125004 File Offset: 0x00123204
		protected override void Dispose(bool disposing)
		{
			EOSNetworkConnection.instancesList.Remove(this);
			P2PInterface p2PInterface = EOSPlatformManager.GetPlatformInterface().GetP2PInterface();
			if (p2PInterface != null)
			{
				p2PInterface.CloseConnection(new CloseConnectionOptions
				{
					LocalUserId = this.LocalUserID,
					RemoteUserId = this.RemoteUserID,
					SocketId = NetworkManagerSystemEOS.socketId
				});
			}
			base.Dispose(disposing);
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x00125068 File Offset: 0x00123268
		public static EOSNetworkConnection Find(ProductUserId owner, ProductUserId endpoint)
		{
			return (from instance in EOSNetworkConnection.instancesList
			where instance.RemoteUserID == endpoint
			select instance).FirstOrDefault((EOSNetworkConnection instance) => owner == instance.LocalUserID);
		}

		// Token: 0x04004497 RID: 17559
		private static List<EOSNetworkConnection> instancesList = new List<EOSNetworkConnection>();

		// Token: 0x04004498 RID: 17560
		public bool ignore;

		// Token: 0x04004499 RID: 17561
		public static BoolConVar cvNetP2PDebugTransport = new BoolConVar("net_p2p_debug_transport", ConVarFlags.None, "0", "Allows p2p transport information to print to the console.");

		// Token: 0x0400449A RID: 17562
		private static BoolConVar cvNetP2PLogMessages = new BoolConVar("net_p2p_log_messages", ConVarFlags.None, "0", "Enables logging of network messages.");
	}
}
