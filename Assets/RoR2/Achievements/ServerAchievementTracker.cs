using System;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Achievements
{
	// Token: 0x02000EC8 RID: 3784
	[RequireComponent(typeof(NetworkUser))]
	public class ServerAchievementTracker : NetworkBehaviour
	{
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x0600562A RID: 22058 RVA: 0x0015F6AB File Offset: 0x0015D8AB
		// (set) Token: 0x0600562B RID: 22059 RVA: 0x0015F6B3 File Offset: 0x0015D8B3
		public NetworkUser networkUser { get; private set; }

		// Token: 0x0600562C RID: 22060 RVA: 0x0015F6BC File Offset: 0x0015D8BC
		private void Awake()
		{
			this.networkUser = base.GetComponent<NetworkUser>();
			this.maskBitArrayConverter = new SerializableBitArray(AchievementManager.serverAchievementCount);
			if (NetworkServer.active)
			{
				this.achievementTrackers = new BaseServerAchievement[AchievementManager.serverAchievementCount];
			}
			if (NetworkClient.active)
			{
				this.maskBuffer = new byte[this.maskBitArrayConverter.byteCount];
			}
		}

		// Token: 0x0600562D RID: 22061 RVA: 0x0015F719 File Offset: 0x0015D919
		private void Start()
		{
			if (this.networkUser.localUser != null)
			{
				UserAchievementManager userAchievementManager = AchievementManager.GetUserAchievementManager(this.networkUser.localUser);
				if (userAchievementManager == null)
				{
					return;
				}
				userAchievementManager.TransmitAchievementRequestsToServer();
			}
		}

		// Token: 0x0600562E RID: 22062 RVA: 0x0015F744 File Offset: 0x0015D944
		private void OnDestroy()
		{
			if (this.achievementTrackers != null)
			{
				int serverAchievementCount = AchievementManager.serverAchievementCount;
				for (int i = 0; i < serverAchievementCount; i++)
				{
					this.SetAchievementTracked(new ServerAchievementIndex
					{
						intValue = i
					}, false);
				}
			}
		}

		// Token: 0x0600562F RID: 22063 RVA: 0x0015F784 File Offset: 0x0015D984
		[Client]
		public void SendAchievementTrackerRequestsMaskToServer(bool[] serverAchievementsToTrackMask)
		{
			if (!NetworkClient.active)
			{
				Debug.LogWarning("[Client] function 'System.Void RoR2.Achievements.ServerAchievementTracker::SendAchievementTrackerRequestsMaskToServer(System.Boolean[])' called on server");
				return;
			}
			int serverAchievementCount = AchievementManager.serverAchievementCount;
			for (int i = 0; i < serverAchievementCount; i++)
			{
				this.maskBitArrayConverter[i] = serverAchievementsToTrackMask[i];
			}
			this.maskBitArrayConverter.GetBytes(this.maskBuffer);
			this.CallCmdSetAchievementTrackerRequests(this.maskBuffer);
		}

		// Token: 0x06005630 RID: 22064 RVA: 0x0015F7E4 File Offset: 0x0015D9E4
		[Command]
		private void CmdSetAchievementTrackerRequests(byte[] packedServerAchievementsToTrackMask)
		{
			int serverAchievementCount = AchievementManager.serverAchievementCount;
			if (packedServerAchievementsToTrackMask.Length << 3 < serverAchievementCount)
			{
				return;
			}
			for (int i = 0; i < serverAchievementCount; i++)
			{
				int num = i >> 3;
				int num2 = i & 7;
				this.SetAchievementTracked(new ServerAchievementIndex
				{
					intValue = i
				}, (packedServerAchievementsToTrackMask[num] >> num2 & 1) != 0);
			}
		}

		// Token: 0x06005631 RID: 22065 RVA: 0x0015F83C File Offset: 0x0015DA3C
		private void SetAchievementTracked(ServerAchievementIndex serverAchievementIndex, bool shouldTrack)
		{
			BaseServerAchievement baseServerAchievement = this.achievementTrackers[serverAchievementIndex.intValue];
			if (shouldTrack == (baseServerAchievement != null))
			{
				return;
			}
			if (shouldTrack)
			{
				BaseServerAchievement baseServerAchievement2 = BaseServerAchievement.Instantiate(serverAchievementIndex);
				baseServerAchievement2.serverAchievementTracker = this;
				this.achievementTrackers[serverAchievementIndex.intValue] = baseServerAchievement2;
				baseServerAchievement2.OnInstall();
				return;
			}
			baseServerAchievement.OnUninstall();
			this.achievementTrackers[serverAchievementIndex.intValue] = null;
		}

		// Token: 0x06005632 RID: 22066 RVA: 0x0015F89C File Offset: 0x0015DA9C
		[ClientRpc]
		public void RpcGrantAchievement(ServerAchievementIndex serverAchievementIndex)
		{
			LocalUser localUser = this.networkUser.localUser;
			if (localUser != null)
			{
				UserAchievementManager userAchievementManager = AchievementManager.GetUserAchievementManager(localUser);
				if (userAchievementManager == null)
				{
					return;
				}
				userAchievementManager.HandleServerAchievementCompleted(serverAchievementIndex);
			}
		}

		// Token: 0x06005634 RID: 22068 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06005635 RID: 22069 RVA: 0x0015F8C9 File Offset: 0x0015DAC9
		protected static void InvokeCmdCmdSetAchievementTrackerRequests(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdSetAchievementTrackerRequests called on client.");
				return;
			}
			((ServerAchievementTracker)obj).CmdSetAchievementTrackerRequests(reader.ReadBytesAndSize());
		}

		// Token: 0x06005636 RID: 22070 RVA: 0x0015F8F4 File Offset: 0x0015DAF4
		public void CallCmdSetAchievementTrackerRequests(byte[] packedServerAchievementsToTrackMask)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdSetAchievementTrackerRequests called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdSetAchievementTrackerRequests(packedServerAchievementsToTrackMask);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)ServerAchievementTracker.kCmdCmdSetAchievementTrackerRequests);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.WriteBytesFull(packedServerAchievementsToTrackMask);
			base.SendCommandInternal(networkWriter, 0, "CmdSetAchievementTrackerRequests");
		}

		// Token: 0x06005637 RID: 22071 RVA: 0x0015F97E File Offset: 0x0015DB7E
		protected static void InvokeRpcRpcGrantAchievement(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcGrantAchievement called on server.");
				return;
			}
			((ServerAchievementTracker)obj).RpcGrantAchievement(GeneratedNetworkCode._ReadServerAchievementIndex_None(reader));
		}

		// Token: 0x06005638 RID: 22072 RVA: 0x0015F9A8 File Offset: 0x0015DBA8
		public void CallRpcGrantAchievement(ServerAchievementIndex serverAchievementIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcGrantAchievement called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)ServerAchievementTracker.kRpcRpcGrantAchievement);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			GeneratedNetworkCode._WriteServerAchievementIndex_None(networkWriter, serverAchievementIndex);
			this.SendRPCInternal(networkWriter, 0, "RpcGrantAchievement");
		}

		// Token: 0x06005639 RID: 22073 RVA: 0x0015FA1C File Offset: 0x0015DC1C
		static ServerAchievementTracker()
		{
			NetworkBehaviour.RegisterCommandDelegate(typeof(ServerAchievementTracker), ServerAchievementTracker.kCmdCmdSetAchievementTrackerRequests, new NetworkBehaviour.CmdDelegate(ServerAchievementTracker.InvokeCmdCmdSetAchievementTrackerRequests));
			ServerAchievementTracker.kRpcRpcGrantAchievement = -1713740939;
			NetworkBehaviour.RegisterRpcDelegate(typeof(ServerAchievementTracker), ServerAchievementTracker.kRpcRpcGrantAchievement, new NetworkBehaviour.CmdDelegate(ServerAchievementTracker.InvokeRpcRpcGrantAchievement));
			NetworkCRC.RegisterBehaviour("ServerAchievementTracker", 0);
		}

		// Token: 0x0600563A RID: 22074 RVA: 0x0015FA8C File Offset: 0x0015DC8C
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x0600563B RID: 22075 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x0600563C RID: 22076 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400507D RID: 20605
		private BaseServerAchievement[] achievementTrackers;

		// Token: 0x0400507E RID: 20606
		private SerializableBitArray maskBitArrayConverter;

		// Token: 0x0400507F RID: 20607
		private byte[] maskBuffer;

		// Token: 0x04005080 RID: 20608
		private static int kCmdCmdSetAchievementTrackerRequests = 387052099;

		// Token: 0x04005081 RID: 20609
		private static int kRpcRpcGrantAchievement;
	}
}
