using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000797 RID: 1943
	[RequireComponent(typeof(CharacterBody))]
	public class LoaderStaticChargeComponent : NetworkBehaviour, IOnDamageDealtServerReceiver, IOnTakeDamageServerReceiver
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06002903 RID: 10499 RVA: 0x000B1DCE File Offset: 0x000AFFCE
		public float charge
		{
			get
			{
				return this._charge;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06002904 RID: 10500 RVA: 0x000B1DD6 File Offset: 0x000AFFD6
		public float chargeFraction
		{
			get
			{
				return this.charge / this.maxCharge;
			}
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x000B1DE5 File Offset: 0x000AFFE5
		private void Awake()
		{
			this.characterBody = base.GetComponent<CharacterBody>();
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x000B1DF3 File Offset: 0x000AFFF3
		public void OnDamageDealtServer(DamageReport damageReport)
		{
			this.AddChargeServer(damageReport.damageDealt);
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x000B1DF3 File Offset: 0x000AFFF3
		public void OnTakeDamageServer(DamageReport damageReport)
		{
			this.AddChargeServer(damageReport.damageDealt);
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x000B1E04 File Offset: 0x000B0004
		[Server]
		private void AddChargeServer(float additionalCharge)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.LoaderStaticChargeComponent::AddChargeServer(System.Single)' called on client");
				return;
			}
			float num = this._charge + additionalCharge;
			if (num > this.maxCharge)
			{
				num = this.maxCharge;
			}
			this.Network_charge = num;
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x000B1E46 File Offset: 0x000B0046
		[Server]
		private void ConsumeChargeInternal()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.LoaderStaticChargeComponent::ConsumeChargeInternal()' called on client");
				return;
			}
			this.SetState(LoaderStaticChargeComponent.State.Drain);
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x000B1E64 File Offset: 0x000B0064
		public void ConsumeChargeAuthority()
		{
			if (NetworkServer.active)
			{
				this.ConsumeChargeInternal();
				return;
			}
			this.CallCmdConsumeCharge();
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x000B1E7C File Offset: 0x000B007C
		private void SetState(LoaderStaticChargeComponent.State newState)
		{
			if (this.state == newState)
			{
				return;
			}
			if (this.state == LoaderStaticChargeComponent.State.Drain && NetworkServer.active)
			{
				this.characterBody.RemoveBuff(JunkContent.Buffs.LoaderOvercharged);
			}
			this.state = newState;
			if (this.state == LoaderStaticChargeComponent.State.Drain && NetworkServer.active)
			{
				this.characterBody.AddBuff(JunkContent.Buffs.LoaderOvercharged);
			}
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x000B1EDC File Offset: 0x000B00DC
		private void FixedUpdate()
		{
			if (NetworkServer.active && this.state == LoaderStaticChargeComponent.State.Drain)
			{
				this.Network_charge = this._charge - Time.fixedDeltaTime * this.consumptionRate;
				if (this._charge <= 0f)
				{
					this.Network_charge = 0f;
					this.SetState(LoaderStaticChargeComponent.State.Idle);
				}
			}
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x000B1F31 File Offset: 0x000B0131
		[Command]
		private void CmdConsumeCharge()
		{
			this.ConsumeChargeInternal();
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06002910 RID: 10512 RVA: 0x000B1F58 File Offset: 0x000B0158
		// (set) Token: 0x06002911 RID: 10513 RVA: 0x000B1F6B File Offset: 0x000B016B
		public float Network_charge
		{
			get
			{
				return this._charge;
			}
			[param: In]
			set
			{
				base.SetSyncVar<float>(value, ref this._charge, 1U);
			}
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x000B1F7F File Offset: 0x000B017F
		protected static void InvokeCmdCmdConsumeCharge(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("Command CmdConsumeCharge called on client.");
				return;
			}
			((LoaderStaticChargeComponent)obj).CmdConsumeCharge();
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x000B1FA4 File Offset: 0x000B01A4
		public void CallCmdConsumeCharge()
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("Command function CmdConsumeCharge called on server.");
				return;
			}
			if (base.isServer)
			{
				this.CmdConsumeCharge();
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)5));
			networkWriter.WritePackedUInt32((uint)LoaderStaticChargeComponent.kCmdCmdConsumeCharge);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			base.SendCommandInternal(networkWriter, 0, "CmdConsumeCharge");
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x000B2020 File Offset: 0x000B0220
		static LoaderStaticChargeComponent()
		{
			NetworkBehaviour.RegisterCommandDelegate(typeof(LoaderStaticChargeComponent), LoaderStaticChargeComponent.kCmdCmdConsumeCharge, new NetworkBehaviour.CmdDelegate(LoaderStaticChargeComponent.InvokeCmdCmdConsumeCharge));
			NetworkCRC.RegisterBehaviour("LoaderStaticChargeComponent", 0);
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x000B205C File Offset: 0x000B025C
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this._charge);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this._charge);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x000B20C8 File Offset: 0x000B02C8
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this._charge = reader.ReadSingle();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this._charge = reader.ReadSingle();
			}
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002C7E RID: 11390
		public float maxCharge = 100f;

		// Token: 0x04002C7F RID: 11391
		public float consumptionRate = 10f;

		// Token: 0x04002C80 RID: 11392
		[SyncVar]
		private float _charge;

		// Token: 0x04002C81 RID: 11393
		private CharacterBody characterBody;

		// Token: 0x04002C82 RID: 11394
		private LoaderStaticChargeComponent.State state;

		// Token: 0x04002C83 RID: 11395
		private static int kCmdCmdConsumeCharge = -261598328;

		// Token: 0x02000798 RID: 1944
		private enum State
		{
			// Token: 0x04002C85 RID: 11397
			Idle,
			// Token: 0x04002C86 RID: 11398
			Drain
		}
	}
}
