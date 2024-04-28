using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008F3 RID: 2291
	public class VoidRaidGauntletEntranceController : NetworkBehaviour
	{
		// Token: 0x0600338A RID: 13194 RVA: 0x000D9417 File Offset: 0x000D7617
		[Server]
		public void SetGauntletIndex(int newGauntletIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.VoidRaidGauntletEntranceController::SetGauntletIndex(System.Int32)' called on client");
				return;
			}
			this.NetworkgauntletIndex = newGauntletIndex;
			this.UpdateGauntletIndex(this.gauntletIndex);
			this.CallRpcUpdateGauntletIndex(this.gauntletIndex);
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x000D944D File Offset: 0x000D764D
		private void OnEnable()
		{
			if (this.entranceZone)
			{
				this.entranceZone.onBodyTeleport += this.OnBodyTeleport;
			}
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x000D9473 File Offset: 0x000D7673
		private void OnDisable()
		{
			if (this.entranceZone)
			{
				this.entranceZone.onBodyTeleport -= this.OnBodyTeleport;
			}
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x000D949C File Offset: 0x000D769C
		private void OnBodyTeleport(CharacterBody body)
		{
			if (Util.HasEffectiveAuthority(body.gameObject) && body.masterObject.GetComponent<PlayerCharacterMasterController>() && VoidRaidGauntletController.instance)
			{
				body.healthComponent.CallCmdHealFull();
				body.healthComponent.CallCmdRechargeShieldFull();
				VoidRaidGauntletController.instance.OnAuthorityPlayerEnter();
			}
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x000D94F4 File Offset: 0x000D76F4
		private void UpdateGauntletIndex(int newGauntletIndex)
		{
			if (newGauntletIndex != this.gauntletIndex)
			{
				this.NetworkgauntletIndex = newGauntletIndex;
			}
			if (this.entranceZone && VoidRaidGauntletController.instance)
			{
				VoidRaidGauntletController.instance.PointZoneToGauntlet(newGauntletIndex, this.entranceZone);
			}
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000D9530 File Offset: 0x000D7730
		[ClientRpc]
		private void RpcUpdateGauntletIndex(int newGauntletIndex)
		{
			this.UpdateGauntletIndex(newGauntletIndex);
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06003392 RID: 13202 RVA: 0x000D953C File Offset: 0x000D773C
		// (set) Token: 0x06003393 RID: 13203 RVA: 0x000D954F File Offset: 0x000D774F
		public int NetworkgauntletIndex
		{
			get
			{
				return this.gauntletIndex;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.UpdateGauntletIndex(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<int>(value, ref this.gauntletIndex, 1U);
			}
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x000D958E File Offset: 0x000D778E
		protected static void InvokeRpcRpcUpdateGauntletIndex(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcUpdateGauntletIndex called on server.");
				return;
			}
			((VoidRaidGauntletEntranceController)obj).RpcUpdateGauntletIndex((int)reader.ReadPackedUInt32());
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x000D95B8 File Offset: 0x000D77B8
		public void CallRpcUpdateGauntletIndex(int newGauntletIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcUpdateGauntletIndex called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)VoidRaidGauntletEntranceController.kRpcRpcUpdateGauntletIndex);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.WritePackedUInt32((uint)newGauntletIndex);
			this.SendRPCInternal(networkWriter, 0, "RpcUpdateGauntletIndex");
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x000D962B File Offset: 0x000D782B
		static VoidRaidGauntletEntranceController()
		{
			NetworkBehaviour.RegisterRpcDelegate(typeof(VoidRaidGauntletEntranceController), VoidRaidGauntletEntranceController.kRpcRpcUpdateGauntletIndex, new NetworkBehaviour.CmdDelegate(VoidRaidGauntletEntranceController.InvokeRpcRpcUpdateGauntletIndex));
			NetworkCRC.RegisterBehaviour("VoidRaidGauntletEntranceController", 0);
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x000D9668 File Offset: 0x000D7868
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.WritePackedUInt32((uint)this.gauntletIndex);
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
				writer.WritePackedUInt32((uint)this.gauntletIndex);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x000D96D4 File Offset: 0x000D78D4
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.gauntletIndex = (int)reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.UpdateGauntletIndex((int)reader.ReadPackedUInt32());
			}
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04003491 RID: 13457
		[SerializeField]
		private MapZone entranceZone;

		// Token: 0x04003492 RID: 13458
		[SyncVar(hook = "UpdateGauntletIndex")]
		private int gauntletIndex;

		// Token: 0x04003493 RID: 13459
		private static int kRpcRpcUpdateGauntletIndex = -330092625;
	}
}
