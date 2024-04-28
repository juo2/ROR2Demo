using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020005BC RID: 1468
	public class AchievementGranter : NetworkBehaviour
	{
		// Token: 0x06001A92 RID: 6802 RVA: 0x00071FCC File Offset: 0x000701CC
		[ClientRpc]
		public void RpcGrantAchievement(string achievementName)
		{
			foreach (LocalUser user in LocalUserManager.readOnlyLocalUsersList)
			{
				AchievementManager.GetUserAchievementManager(user).GrantAchievement(AchievementManager.GetAchievementDef(achievementName));
			}
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00072020 File Offset: 0x00070220
		protected static void InvokeRpcRpcGrantAchievement(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcGrantAchievement called on server.");
				return;
			}
			((AchievementGranter)obj).RpcGrantAchievement(reader.ReadString());
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x0007204C File Offset: 0x0007024C
		public void CallRpcGrantAchievement(string achievementName)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcGrantAchievement called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)AchievementGranter.kRpcRpcGrantAchievement);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(achievementName);
			this.SendRPCInternal(networkWriter, 0, "RpcGrantAchievement");
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x000720BF File Offset: 0x000702BF
		static AchievementGranter()
		{
			NetworkBehaviour.RegisterRpcDelegate(typeof(AchievementGranter), AchievementGranter.kRpcRpcGrantAchievement, new NetworkBehaviour.CmdDelegate(AchievementGranter.InvokeRpcRpcGrantAchievement));
			NetworkCRC.RegisterBehaviour("AchievementGranter", 0);
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x000720FC File Offset: 0x000702FC
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040020A2 RID: 8354
		private static int kRpcRpcGrantAchievement = -180752285;
	}
}
