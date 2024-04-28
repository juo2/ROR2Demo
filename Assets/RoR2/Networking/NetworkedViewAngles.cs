using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C48 RID: 3144
	public class NetworkedViewAngles : NetworkBehaviour
	{
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06004727 RID: 18215 RVA: 0x00125CB2 File Offset: 0x00123EB2
		// (set) Token: 0x06004728 RID: 18216 RVA: 0x00125CBA File Offset: 0x00123EBA
		public bool hasEffectiveAuthority { get; private set; }

		// Token: 0x06004729 RID: 18217 RVA: 0x00125CC3 File Offset: 0x00123EC3
		private void Awake()
		{
			this.networkIdentity = base.GetComponent<NetworkIdentity>();
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x00125CD4 File Offset: 0x00123ED4
		private void Update()
		{
			this.hasEffectiveAuthority = Util.HasEffectiveAuthority(this.networkIdentity);
			if (this.hasEffectiveAuthority)
			{
				this.networkDesiredViewAngles = this.viewAngles;
				return;
			}
			this.viewAngles = PitchYawPair.SmoothDamp(this.viewAngles, this.networkDesiredViewAngles, ref this.velocity, this.GetNetworkSendInterval() * this.bufferMultiplier, this.maxSmoothVelocity);
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x00125D37 File Offset: 0x00123F37
		public override float GetNetworkSendInterval()
		{
			return this.sendRate;
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x00125D40 File Offset: 0x00123F40
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				base.SetDirtyBit(1U);
			}
			this.hasEffectiveAuthority = Util.HasEffectiveAuthority(this.networkIdentity);
			if (this.hasEffectiveAuthority)
			{
				this.networkDesiredViewAngles = this.viewAngles;
				if (!NetworkServer.active)
				{
					this.sendTimer -= Time.deltaTime;
					if (this.sendTimer <= 0f)
					{
						this.CallCmdUpdateViewAngles(this.viewAngles.pitch, this.viewAngles.yaw);
						this.sendTimer = this.GetNetworkSendInterval();
					}
				}
			}
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x00125DCE File Offset: 0x00123FCE
		[Command(channel = 5)]
		public void CmdUpdateViewAngles(float pitch, float yaw)
		{
			this.networkDesiredViewAngles = new PitchYawPair(pitch, yaw);
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x00125DDD File Offset: 0x00123FDD
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			writer.Write(this.networkDesiredViewAngles);
			return true;
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x00125DEC File Offset: 0x00123FEC
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			PitchYawPair pitchYawPair = reader.ReadPitchYawPair();
			if (this.hasEffectiveAuthority)
			{
				return;
			}
			this.networkDesiredViewAngles = pitchYawPair;
			if (initialState)
			{
				this.viewAngles = pitchYawPair;
				this.velocity = PitchYawPair.zero;
			}
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x00125E4E File Offset: 0x0012404E
		protected static void InvokeCmdCmdUpdateViewAngles(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdUpdateViewAngles called on client.");
				return;
			}
			((NetworkedViewAngles)obj).CmdUpdateViewAngles(reader.ReadSingle(), reader.ReadSingle());
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x00125E80 File Offset: 0x00124080
		public void CallCmdUpdateViewAngles(float pitch, float yaw)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdUpdateViewAngles called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdUpdateViewAngles(pitch, yaw);
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)NetworkedViewAngles.kCmdCmdUpdateViewAngles);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(pitch);
			networkWriter.Write(yaw);
			base.SendCommandInternal(networkWriter, 5, "CmdUpdateViewAngles");
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x00125F18 File Offset: 0x00124118
		static NetworkedViewAngles()
		{
			NetworkBehaviour.RegisterCommandDelegate(typeof(NetworkedViewAngles), NetworkedViewAngles.kCmdCmdUpdateViewAngles, new NetworkBehaviour.CmdDelegate(NetworkedViewAngles.InvokeCmdCmdUpdateViewAngles));
			NetworkCRC.RegisterBehaviour("NetworkedViewAngles", 0);
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040044BF RID: 17599
		public PitchYawPair viewAngles;

		// Token: 0x040044C0 RID: 17600
		private PitchYawPair networkDesiredViewAngles;

		// Token: 0x040044C1 RID: 17601
		private PitchYawPair velocity;

		// Token: 0x040044C2 RID: 17602
		private NetworkIdentity networkIdentity;

		// Token: 0x040044C4 RID: 17604
		public float sendRate = 0.05f;

		// Token: 0x040044C5 RID: 17605
		public float bufferMultiplier = 3f;

		// Token: 0x040044C6 RID: 17606
		public float maxSmoothVelocity = 1440f;

		// Token: 0x040044C7 RID: 17607
		private float sendTimer;

		// Token: 0x040044C8 RID: 17608
		private static int kCmdCmdUpdateViewAngles = -1684781536;
	}
}
