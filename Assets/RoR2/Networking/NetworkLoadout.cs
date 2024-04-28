using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C47 RID: 3143
	public class NetworkLoadout : NetworkBehaviour
	{
		// Token: 0x140000E5 RID: 229
		// (add) Token: 0x06004718 RID: 18200 RVA: 0x001259FC File Offset: 0x00123BFC
		// (remove) Token: 0x06004719 RID: 18201 RVA: 0x00125A34 File Offset: 0x00123C34
		public event Action onLoadoutUpdated;

		// Token: 0x0600471A RID: 18202 RVA: 0x00125A69 File Offset: 0x00123C69
		public void CopyLoadout(Loadout dest)
		{
			this.loadout.Copy(dest);
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x00125A77 File Offset: 0x00123C77
		public void SetLoadout(Loadout src)
		{
			src.Copy(this.loadout);
			if (NetworkServer.active)
			{
				base.SetDirtyBit(1U);
			}
			else if (base.isLocalPlayer)
			{
				this.SendLoadoutClient();
			}
			this.OnLoadoutUpdated();
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x00125AAC File Offset: 0x00123CAC
		[Command]
		private void CmdSendLoadout(byte[] bytes)
		{
			NetworkReader reader = new NetworkReader(bytes);
			NetworkLoadout.temp.Deserialize(reader);
			this.SetLoadout(NetworkLoadout.temp);
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x00125AD8 File Offset: 0x00123CD8
		[Client]
		private void SendLoadoutClient()
		{
			if (!NetworkClient.active)
			{
				Debug.LogWarning("[Client] function 'System.Void RoR2.Networking.NetworkLoadout::SendLoadoutClient()' called on server");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			this.loadout.Serialize(networkWriter);
			this.CallCmdSendLoadout(networkWriter.ToArray());
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x00125B18 File Offset: 0x00123D18
		private void OnLoadoutUpdated()
		{
			Action action = this.onLoadoutUpdated;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x00125B2C File Offset: 0x00123D2C
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			uint num = base.syncVarDirtyBits;
			if (initialState)
			{
				num = 1U;
			}
			writer.WritePackedUInt32(num);
			if ((num & 1U) != 0U)
			{
				this.loadout.Serialize(writer);
			}
			return num > 0U;
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x00125B61 File Offset: 0x00123D61
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if ((reader.ReadPackedUInt32() & 1U) != 0U)
			{
				NetworkLoadout.temp.Deserialize(reader);
				if (!base.isLocalPlayer)
				{
					NetworkLoadout.temp.Copy(this.loadout);
					this.OnLoadoutUpdated();
				}
			}
		}

		// Token: 0x06004722 RID: 18210 RVA: 0x00125BAC File Offset: 0x00123DAC
		static NetworkLoadout()
		{
			NetworkBehaviour.RegisterCommandDelegate(typeof(NetworkLoadout), NetworkLoadout.kCmdCmdSendLoadout, new NetworkBehaviour.CmdDelegate(NetworkLoadout.InvokeCmdCmdSendLoadout));
			NetworkCRC.RegisterBehaviour("NetworkLoadout", 0);
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x00125BFC File Offset: 0x00123DFC
		protected static void InvokeCmdCmdSendLoadout(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdSendLoadout called on client.");
				return;
			}
			((NetworkLoadout)obj).CmdSendLoadout(reader.ReadBytesAndSize());
		}

		// Token: 0x06004725 RID: 18213 RVA: 0x00125C28 File Offset: 0x00123E28
		public void CallCmdSendLoadout(byte[] bytes)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdSendLoadout called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdSendLoadout(bytes);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)NetworkLoadout.kCmdCmdSendLoadout);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.WriteBytesFull(bytes);
			base.SendCommandInternal(networkWriter, 0, "CmdSendLoadout");
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040044BB RID: 17595
		private static readonly Loadout temp = new Loadout();

		// Token: 0x040044BC RID: 17596
		private readonly Loadout loadout = new Loadout();

		// Token: 0x040044BE RID: 17598
		private static int kCmdCmdSendLoadout = 1217513894;
	}
}
