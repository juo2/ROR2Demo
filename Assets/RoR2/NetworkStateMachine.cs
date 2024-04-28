using System;
using System.Collections.Generic;
using EntityStates;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007D4 RID: 2004
	[DisallowMultipleComponent]
	public class NetworkStateMachine : NetworkBehaviour
	{
		// Token: 0x06002AAE RID: 10926 RVA: 0x000B7AF0 File Offset: 0x000B5CF0
		private void Awake()
		{
			this.networkIdentity = base.GetComponent<NetworkIdentity>();
			for (int i = 0; i < this.stateMachines.Length; i++)
			{
				EntityStateMachine entityStateMachine = this.stateMachines[i];
				entityStateMachine.networkIndex = i;
				entityStateMachine.networker = this;
				entityStateMachine.networkIdentity = this.networkIdentity;
			}
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000B7B40 File Offset: 0x000B5D40
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			if (initialState)
			{
				for (int i = 0; i < this.stateMachines.Length; i++)
				{
					EntityStateMachine entityStateMachine = this.stateMachines[i];
					writer.Write(EntityStateCatalog.GetStateIndex(entityStateMachine.state.GetType()));
					entityStateMachine.state.OnSerialize(writer);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000B7B94 File Offset: 0x000B5D94
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				for (int i = 0; i < this.stateMachines.Length; i++)
				{
					EntityStateMachine entityStateMachine = this.stateMachines[i];
					EntityStateIndex entityStateIndex = reader.ReadEntityStateIndex();
					if (!base.hasAuthority)
					{
						EntityState entityState = EntityStateCatalog.InstantiateState(entityStateIndex);
						if (entityState != null)
						{
							entityState.outer = entityStateMachine;
							entityState.OnDeserialize(reader);
							if (!this.stateMachines[i])
							{
								Debug.LogErrorFormat("State machine [{0}] on object {1} is not set! incoming state = {2}", new object[]
								{
									i,
									base.gameObject,
									entityState.GetType()
								});
							}
							entityStateMachine.SetNextState(entityState);
						}
					}
				}
			}
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x000B7C2C File Offset: 0x000B5E2C
		[NetworkMessageHandler(msgType = 48, client = true, server = true)]
		public static void HandleSetEntityState(NetworkMessage netMsg)
		{
			NetworkIdentity networkIdentity = netMsg.reader.ReadNetworkIdentity();
			byte b = netMsg.reader.ReadByte();
			EntityStateIndex entityStateIndex = netMsg.reader.ReadEntityStateIndex();
			if (networkIdentity == null)
			{
				return;
			}
			NetworkStateMachine component = networkIdentity.gameObject.GetComponent<NetworkStateMachine>();
			if (component == null || (int)b >= component.stateMachines.Length)
			{
				return;
			}
			EntityStateMachine entityStateMachine = component.stateMachines[(int)b];
			if (entityStateMachine == null)
			{
				return;
			}
			if (networkIdentity.isServer)
			{
				HashSet<NetworkInstanceId> clientOwnedObjects = netMsg.conn.clientOwnedObjects;
				if (clientOwnedObjects == null || !clientOwnedObjects.Contains(networkIdentity.netId))
				{
					return;
				}
			}
			else if (networkIdentity.hasAuthority)
			{
				return;
			}
			EntityState entityState = EntityStateCatalog.InstantiateState(entityStateIndex);
			if (entityState == null)
			{
				return;
			}
			entityState.outer = entityStateMachine;
			entityState.OnDeserialize(netMsg.reader);
			entityStateMachine.SetState(entityState);
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x000B7CFC File Offset: 0x000B5EFC
		public void SendSetEntityState(int stateMachineIndex)
		{
			if (!NetworkServer.active && !base.hasAuthority)
			{
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			EntityStateMachine entityStateMachine = this.stateMachines[stateMachineIndex];
			networkWriter.StartMessage(48);
			networkWriter.Write(this.networkIdentity);
			networkWriter.Write((byte)stateMachineIndex);
			networkWriter.Write(EntityStateCatalog.GetStateIndex(entityStateMachine.state.GetType()));
			entityStateMachine.state.OnSerialize(networkWriter);
			networkWriter.FinishMessage();
			if (NetworkServer.active)
			{
				NetworkServer.SendWriterToReady(base.gameObject, networkWriter, this.GetNetworkChannel());
				return;
			}
			if (ClientScene.readyConnection != null)
			{
				ClientScene.readyConnection.SendWriter(networkWriter, this.GetNetworkChannel());
			}
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x000B7DA0 File Offset: 0x000B5FA0
		private void OnValidate()
		{
			for (int i = 0; i < this.stateMachines.Length; i++)
			{
				if (!this.stateMachines[i])
				{
					Debug.LogErrorFormat("{0} has a blank entry for NetworkStateMachine!", new object[]
					{
						base.gameObject
					});
				}
			}
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002DAB RID: 11691
		[SerializeField]
		[Tooltip("The sibling state machine components to network.")]
		private EntityStateMachine[] stateMachines = Array.Empty<EntityStateMachine>();

		// Token: 0x04002DAC RID: 11692
		private NetworkIdentity networkIdentity;
	}
}
