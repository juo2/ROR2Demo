using System;
using EntityStates;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000888 RID: 2184
	[DisallowMultipleComponent]
	public class SetStateOnHurt : NetworkBehaviour, IOnTakeDamageServerReceiver
	{
		// Token: 0x06002FDC RID: 12252 RVA: 0x000CBB0C File Offset: 0x000C9D0C
		public static void SetStunOnObject(GameObject target, float duration)
		{
			SetStateOnHurt component = target.GetComponent<SetStateOnHurt>();
			if (component)
			{
				component.SetStun(duration);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06002FDD RID: 12253 RVA: 0x000AD287 File Offset: 0x000AB487
		private bool spawnedOverNetwork
		{
			get
			{
				return base.isServer;
			}
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x000CBB2F File Offset: 0x000C9D2F
		public override void OnStartAuthority()
		{
			base.OnStartAuthority();
			this.UpdateAuthority();
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x000CBB3D File Offset: 0x000C9D3D
		public override void OnStopAuthority()
		{
			base.OnStopAuthority();
			this.UpdateAuthority();
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x000CBB4B File Offset: 0x000C9D4B
		private void UpdateAuthority()
		{
			this.hasEffectiveAuthority = Util.HasEffectiveAuthority(base.gameObject);
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000CBB5E File Offset: 0x000C9D5E
		private void Start()
		{
			this.UpdateAuthority();
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000CBB68 File Offset: 0x000C9D68
		public void OnTakeDamageServer(DamageReport damageReport)
		{
			if (!this.targetStateMachine || !this.spawnedOverNetwork)
			{
				return;
			}
			HealthComponent victim = damageReport.victim;
			DamageInfo damageInfo = damageReport.damageInfo;
			CharacterMaster attackerMaster = damageReport.attackerMaster;
			int num = attackerMaster ? attackerMaster.inventory.GetItemCount(RoR2Content.Items.StunChanceOnHit) : 0;
			if (num > 0 && Util.CheckRoll(Util.ConvertAmplificationPercentageIntoReductionPercentage(SetStateOnHurt.stunChanceOnHitBaseChancePercent * (float)num * damageReport.damageInfo.procCoefficient), attackerMaster))
			{
				EffectManager.SimpleImpactEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/ImpactStunGrenade"), damageInfo.position, -damageInfo.force, true);
				this.SetStun(2f);
			}
			bool flag = damageInfo.procCoefficient >= Mathf.Epsilon;
			float damageDealt = damageReport.damageDealt;
			if (flag && this.canBeFrozen && (damageInfo.damageType & DamageType.Freeze2s) != DamageType.Generic)
			{
				this.SetFrozen(2f * damageInfo.procCoefficient);
				return;
			}
			if (!victim.isInFrozenState)
			{
				if (flag && this.canBeStunned && (damageInfo.damageType & DamageType.Shock5s) != DamageType.Generic)
				{
					this.SetShock(5f * damageReport.damageInfo.procCoefficient);
					return;
				}
				if (flag && this.canBeStunned && (damageInfo.damageType & DamageType.Stun1s) != DamageType.Generic)
				{
					this.SetStun(1f);
					return;
				}
				if (this.canBeHitStunned && damageDealt > victim.fullCombinedHealth * this.hitThreshold)
				{
					this.SetPain();
				}
			}
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x000CBCD0 File Offset: 0x000C9ED0
		[Server]
		public void SetStun(float duration)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.SetStateOnHurt::SetStun(System.Single)' called on client");
				return;
			}
			if (!this.canBeStunned)
			{
				return;
			}
			if (this.hasEffectiveAuthority)
			{
				this.SetStunInternal(duration);
				return;
			}
			this.CallRpcSetStun(duration);
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000CBD07 File Offset: 0x000C9F07
		[Server]
		public void SetFrozen(float duration)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.SetStateOnHurt::SetFrozen(System.Single)' called on client");
				return;
			}
			if (!this.canBeFrozen)
			{
				return;
			}
			if (this.hasEffectiveAuthority)
			{
				this.SetFrozenInternal(duration);
				return;
			}
			this.CallRpcSetFrozen(duration);
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x000CBD3E File Offset: 0x000C9F3E
		[Server]
		public void SetShock(float duration)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.SetStateOnHurt::SetShock(System.Single)' called on client");
				return;
			}
			if (!this.canBeStunned)
			{
				return;
			}
			if (this.hasEffectiveAuthority)
			{
				this.SetShockInternal(duration);
				return;
			}
			this.CallRpcSetShock(duration);
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x000CBD75 File Offset: 0x000C9F75
		[Server]
		public void SetPain()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.SetStateOnHurt::SetPain()' called on client");
				return;
			}
			if (!this.canBeHitStunned)
			{
				return;
			}
			if (this.hasEffectiveAuthority)
			{
				this.SetPainInternal();
				return;
			}
			this.CallRpcSetPain();
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x000CBDAA File Offset: 0x000C9FAA
		[Server]
		public void Cleanse()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.SetStateOnHurt::Cleanse()' called on client");
				return;
			}
			if (this.hasEffectiveAuthority)
			{
				this.CleanseInternal();
				return;
			}
			this.CallRpcCleanse();
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000CBDD6 File Offset: 0x000C9FD6
		[ClientRpc]
		private void RpcSetStun(float duration)
		{
			if (this.hasEffectiveAuthority)
			{
				this.SetStunInternal(duration);
			}
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000CBDE8 File Offset: 0x000C9FE8
		private void SetStunInternal(float duration)
		{
			if (this.targetStateMachine)
			{
				if (this.targetStateMachine.state is StunState)
				{
					StunState stunState = this.targetStateMachine.state as StunState;
					if (stunState.timeRemaining < duration)
					{
						stunState.ExtendStun(duration - stunState.timeRemaining);
					}
				}
				else
				{
					StunState stunState2 = new StunState();
					stunState2.stunDuration = duration;
					this.targetStateMachine.SetInterruptState(stunState2, InterruptPriority.Pain);
				}
			}
			EntityStateMachine[] array = this.idleStateMachine;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetNextStateToMain();
			}
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000CBE76 File Offset: 0x000CA076
		[ClientRpc]
		private void RpcSetFrozen(float duration)
		{
			if (this.hasEffectiveAuthority)
			{
				this.SetFrozenInternal(duration);
			}
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000CBE88 File Offset: 0x000CA088
		private void SetFrozenInternal(float duration)
		{
			if (this.targetStateMachine)
			{
				FrozenState frozenState = new FrozenState();
				frozenState.freezeDuration = duration;
				this.targetStateMachine.SetInterruptState(frozenState, InterruptPriority.Frozen);
			}
			EntityStateMachine[] array = this.idleStateMachine;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetNextState(new Idle());
			}
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000CBEDF File Offset: 0x000CA0DF
		[ClientRpc]
		private void RpcSetShock(float duration)
		{
			if (this.hasEffectiveAuthority)
			{
				this.SetShockInternal(duration);
			}
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000CBEF0 File Offset: 0x000CA0F0
		private void SetShockInternal(float duration)
		{
			if (this.targetStateMachine)
			{
				ShockState shockState = new ShockState();
				shockState.shockDuration = duration;
				this.targetStateMachine.SetInterruptState(shockState, InterruptPriority.Pain);
			}
			EntityStateMachine[] array = this.idleStateMachine;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetNextStateToMain();
			}
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000CBF42 File Offset: 0x000CA142
		[ClientRpc]
		private void RpcSetPain()
		{
			if (this.hasEffectiveAuthority)
			{
				this.SetPainInternal();
			}
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000CBF54 File Offset: 0x000CA154
		private void SetPainInternal()
		{
			if (this.targetStateMachine)
			{
				this.targetStateMachine.SetInterruptState(EntityStateCatalog.InstantiateState(this.hurtState), InterruptPriority.Pain);
			}
			EntityStateMachine[] array = this.idleStateMachine;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetNextStateToMain();
			}
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000CBFA3 File Offset: 0x000CA1A3
		[ClientRpc]
		private void RpcCleanse()
		{
			if (this.hasEffectiveAuthority)
			{
				this.CleanseInternal();
			}
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000CBFB4 File Offset: 0x000CA1B4
		private void CleanseInternal()
		{
			if (this.targetStateMachine && (this.targetStateMachine.state is FrozenState || this.targetStateMachine.state is StunState || this.targetStateMachine.state is ShockState))
			{
				this.targetStateMachine.SetInterruptState(EntityStateCatalog.InstantiateState(this.targetStateMachine.mainStateType), InterruptPriority.Frozen);
			}
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000CC050 File Offset: 0x000CA250
		static SetStateOnHurt()
		{
			NetworkBehaviour.RegisterRpcDelegate(typeof(SetStateOnHurt), SetStateOnHurt.kRpcRpcSetStun, new NetworkBehaviour.CmdDelegate(SetStateOnHurt.InvokeRpcRpcSetStun));
			SetStateOnHurt.kRpcRpcSetFrozen = 1781279215;
			NetworkBehaviour.RegisterRpcDelegate(typeof(SetStateOnHurt), SetStateOnHurt.kRpcRpcSetFrozen, new NetworkBehaviour.CmdDelegate(SetStateOnHurt.InvokeRpcRpcSetFrozen));
			SetStateOnHurt.kRpcRpcSetShock = -1316305549;
			NetworkBehaviour.RegisterRpcDelegate(typeof(SetStateOnHurt), SetStateOnHurt.kRpcRpcSetShock, new NetworkBehaviour.CmdDelegate(SetStateOnHurt.InvokeRpcRpcSetShock));
			SetStateOnHurt.kRpcRpcSetPain = 788726245;
			NetworkBehaviour.RegisterRpcDelegate(typeof(SetStateOnHurt), SetStateOnHurt.kRpcRpcSetPain, new NetworkBehaviour.CmdDelegate(SetStateOnHurt.InvokeRpcRpcSetPain));
			SetStateOnHurt.kRpcRpcCleanse = -339360280;
			NetworkBehaviour.RegisterRpcDelegate(typeof(SetStateOnHurt), SetStateOnHurt.kRpcRpcCleanse, new NetworkBehaviour.CmdDelegate(SetStateOnHurt.InvokeRpcRpcCleanse));
			NetworkCRC.RegisterBehaviour("SetStateOnHurt", 0);
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000CC148 File Offset: 0x000CA348
		protected static void InvokeRpcRpcSetStun(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcSetStun called on server.");
				return;
			}
			((SetStateOnHurt)obj).RpcSetStun(reader.ReadSingle());
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x000CC172 File Offset: 0x000CA372
		protected static void InvokeRpcRpcSetFrozen(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcSetFrozen called on server.");
				return;
			}
			((SetStateOnHurt)obj).RpcSetFrozen(reader.ReadSingle());
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000CC19C File Offset: 0x000CA39C
		protected static void InvokeRpcRpcSetShock(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcSetShock called on server.");
				return;
			}
			((SetStateOnHurt)obj).RpcSetShock(reader.ReadSingle());
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000CC1C6 File Offset: 0x000CA3C6
		protected static void InvokeRpcRpcSetPain(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcSetPain called on server.");
				return;
			}
			((SetStateOnHurt)obj).RpcSetPain();
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000CC1E9 File Offset: 0x000CA3E9
		protected static void InvokeRpcRpcCleanse(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcCleanse called on server.");
				return;
			}
			((SetStateOnHurt)obj).RpcCleanse();
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000CC20C File Offset: 0x000CA40C
		public void CallRpcSetStun(float duration)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcSetStun called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)SetStateOnHurt.kRpcRpcSetStun);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(duration);
			this.SendRPCInternal(networkWriter, 0, "RpcSetStun");
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000CC280 File Offset: 0x000CA480
		public void CallRpcSetFrozen(float duration)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcSetFrozen called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)SetStateOnHurt.kRpcRpcSetFrozen);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(duration);
			this.SendRPCInternal(networkWriter, 0, "RpcSetFrozen");
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000CC2F4 File Offset: 0x000CA4F4
		public void CallRpcSetShock(float duration)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcSetShock called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)SetStateOnHurt.kRpcRpcSetShock);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write(duration);
			this.SendRPCInternal(networkWriter, 0, "RpcSetShock");
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000CC368 File Offset: 0x000CA568
		public void CallRpcSetPain()
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcSetPain called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)SetStateOnHurt.kRpcRpcSetPain);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			this.SendRPCInternal(networkWriter, 0, "RpcSetPain");
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x000CC3D4 File Offset: 0x000CA5D4
		public void CallRpcCleanse()
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcCleanse called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)SetStateOnHurt.kRpcRpcCleanse);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			this.SendRPCInternal(networkWriter, 0, "RpcCleanse");
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000CC440 File Offset: 0x000CA640
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x0400319B RID: 12699
		[Tooltip("The percentage of their max HP they need to take to get stunned. Ranges from 0-1.")]
		public float hitThreshold = 0.1f;

		// Token: 0x0400319C RID: 12700
		[Tooltip("The state machine to set the state of when this character is hurt.")]
		public EntityStateMachine targetStateMachine;

		// Token: 0x0400319D RID: 12701
		[Tooltip("The state machine to set to idle when this character is hurt.")]
		public EntityStateMachine[] idleStateMachine;

		// Token: 0x0400319E RID: 12702
		[Tooltip("The state to enter when this character is hurt.")]
		public SerializableEntityStateType hurtState;

		// Token: 0x0400319F RID: 12703
		public bool canBeHitStunned = true;

		// Token: 0x040031A0 RID: 12704
		public bool canBeStunned = true;

		// Token: 0x040031A1 RID: 12705
		public bool canBeFrozen = true;

		// Token: 0x040031A2 RID: 12706
		private bool hasEffectiveAuthority = true;

		// Token: 0x040031A3 RID: 12707
		private static readonly float stunChanceOnHitBaseChancePercent = 5f;

		// Token: 0x040031A4 RID: 12708
		private static int kRpcRpcSetStun = 788834249;

		// Token: 0x040031A5 RID: 12709
		private static int kRpcRpcSetFrozen;

		// Token: 0x040031A6 RID: 12710
		private static int kRpcRpcSetShock;

		// Token: 0x040031A7 RID: 12711
		private static int kRpcRpcSetPain;

		// Token: 0x040031A8 RID: 12712
		private static int kRpcRpcCleanse;
	}
}
