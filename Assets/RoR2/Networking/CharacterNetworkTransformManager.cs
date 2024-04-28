using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Networking
{
	// Token: 0x02000C32 RID: 3122
	public class CharacterNetworkTransformManager : MonoBehaviour
	{
		// Token: 0x060046A2 RID: 18082 RVA: 0x00124216 File Offset: 0x00122416
		private void Awake()
		{
			CharacterNetworkTransformManager.instance = this;
		}

		// Token: 0x060046A3 RID: 18083 RVA: 0x0012421E File Offset: 0x0012241E
		[NetworkMessageHandler(msgType = 51, client = true, server = true)]
		private static void HandleTransformUpdates(NetworkMessage netMsg)
		{
			if (CharacterNetworkTransformManager.instance)
			{
				CharacterNetworkTransformManager.instance.HandleTransformUpdatesInternal(netMsg);
			}
		}

		// Token: 0x060046A4 RID: 18084 RVA: 0x00124238 File Offset: 0x00122438
		private void HandleTransformUpdatesInternal(NetworkMessage netMsg)
		{
			uint num = (uint)netMsg.reader.ReadByte();
			float filteredClientRttFixed = PlatformSystems.networkManager.filteredClientRttFixed;
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				netMsg.ReadMessage<CharacterNetworkTransformManager.CharacterUpdateMessage>(this.currentInMessage);
				GameObject gameObject = this.currentInMessage.gameObject;
				if (gameObject && (!NetworkServer.active || gameObject.GetComponent<NetworkIdentity>().clientAuthorityOwner == netMsg.conn))
				{
					CharacterNetworkTransform component = gameObject.GetComponent<CharacterNetworkTransform>();
					if (component && !component.hasEffectiveAuthority)
					{
						CharacterNetworkTransform.Snapshot snapshot = new CharacterNetworkTransform.Snapshot
						{
							serverTime = this.currentInMessage.timestamp,
							position = this.currentInMessage.newPosition,
							moveVector = this.currentInMessage.moveVector,
							aimDirection = this.currentInMessage.aimDirection,
							rotation = this.currentInMessage.rotation,
							isGrounded = this.currentInMessage.isGrounded,
							groundNormal = this.currentInMessage.groundNormal
						};
						if (NetworkClient.active)
						{
							snapshot.serverTime += filteredClientRttFixed;
						}
						component.PushSnapshot(snapshot);
						if (NetworkServer.active)
						{
							this.snapshotQueue.Enqueue(new CharacterNetworkTransformManager.NetSnapshot
							{
								gameObject = component.gameObject,
								snapshot = snapshot
							});
						}
					}
				}
				num2++;
			}
		}

		// Token: 0x060046A5 RID: 18085 RVA: 0x001243AC File Offset: 0x001225AC
		private void ProcessQueue()
		{
			if (this.snapshotQueue.Count == 0)
			{
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.StartMessage(51);
			int num = Mathf.Min(Mathf.FloorToInt((float)(1000 - networkWriter.Position) / (float)CharacterNetworkTransformManager.CharacterUpdateMessage.maxNetworkSize), this.snapshotQueue.Count);
			networkWriter.Write((byte)num);
			for (int i = 0; i < num; i++)
			{
				CharacterNetworkTransformManager.NetSnapshot netSnapshot = this.snapshotQueue.Dequeue();
				this.currentOutMessage.gameObject = netSnapshot.gameObject;
				this.currentOutMessage.newPosition = netSnapshot.snapshot.position;
				this.currentOutMessage.aimDirection = netSnapshot.snapshot.aimDirection;
				this.currentOutMessage.moveVector = netSnapshot.snapshot.moveVector;
				this.currentOutMessage.rotation = netSnapshot.snapshot.rotation;
				this.currentOutMessage.isGrounded = netSnapshot.snapshot.isGrounded;
				this.currentOutMessage.timestamp = netSnapshot.snapshot.serverTime;
				this.currentOutMessage.groundNormal = netSnapshot.snapshot.groundNormal;
				networkWriter.Write(this.currentOutMessage);
			}
			networkWriter.FinishMessage();
			if (NetworkServer.active)
			{
				NetworkServer.SendWriterToReady(null, networkWriter, QosChannelIndex.characterTransformUnreliable.intVal);
				return;
			}
			if (ClientScene.readyConnection != null)
			{
				ClientScene.readyConnection.SendWriter(networkWriter, QosChannelIndex.characterTransformUnreliable.intVal);
			}
		}

		// Token: 0x060046A6 RID: 18086 RVA: 0x00124518 File Offset: 0x00122718
		private void FixedUpdate()
		{
			if (!NetworkManager.singleton)
			{
				return;
			}
			ReadOnlyCollection<CharacterNetworkTransform> readOnlyInstancesList = CharacterNetworkTransform.readOnlyInstancesList;
			float fixedTime = Time.fixedTime;
			for (int i = 0; i < readOnlyInstancesList.Count; i++)
			{
				CharacterNetworkTransform characterNetworkTransform = readOnlyInstancesList[i];
				if (characterNetworkTransform.hasEffectiveAuthority && fixedTime - characterNetworkTransform.lastPositionTransmitTime > characterNetworkTransform.positionTransmitInterval)
				{
					characterNetworkTransform.lastPositionTransmitTime = fixedTime;
					this.snapshotQueue.Enqueue(new CharacterNetworkTransformManager.NetSnapshot
					{
						gameObject = characterNetworkTransform.gameObject,
						snapshot = characterNetworkTransform.newestNetSnapshot
					});
				}
			}
			while (this.snapshotQueue.Count > 0)
			{
				this.ProcessQueue();
			}
		}

		// Token: 0x0400446F RID: 17519
		private static CharacterNetworkTransformManager instance;

		// Token: 0x04004470 RID: 17520
		private CharacterNetworkTransformManager.CharacterUpdateMessage currentInMessage = new CharacterNetworkTransformManager.CharacterUpdateMessage();

		// Token: 0x04004471 RID: 17521
		private CharacterNetworkTransformManager.CharacterUpdateMessage currentOutMessage = new CharacterNetworkTransformManager.CharacterUpdateMessage();

		// Token: 0x04004472 RID: 17522
		private readonly Queue<CharacterNetworkTransformManager.NetSnapshot> snapshotQueue = new Queue<CharacterNetworkTransformManager.NetSnapshot>();

		// Token: 0x02000C33 RID: 3123
		private class CharacterUpdateMessage : MessageBase
		{
			// Token: 0x060046A8 RID: 18088 RVA: 0x001245E4 File Offset: 0x001227E4
			public override void Serialize(NetworkWriter writer)
			{
				base.Serialize(writer);
				byte b = 0;
				bool flag = this.rotation != Quaternion.identity;
				if (flag)
				{
					b |= CharacterNetworkTransformManager.CharacterUpdateMessage.nonIdentityRotationBit;
				}
				if (this.isGrounded)
				{
					b |= CharacterNetworkTransformManager.CharacterUpdateMessage.isGroundedBit;
				}
				writer.Write(b);
				writer.Write(this.timestamp);
				writer.Write(this.gameObject);
				writer.Write(this.newPosition);
				writer.Write(new PackedUnitVector3(this.aimDirection));
				writer.Write(this.moveVector);
				if (flag)
				{
					writer.Write(this.rotation);
				}
				if (this.isGrounded)
				{
					writer.Write(new PackedUnitVector3(this.groundNormal));
				}
			}

			// Token: 0x060046A9 RID: 18089 RVA: 0x00124698 File Offset: 0x00122898
			public override void Deserialize(NetworkReader reader)
			{
				base.Deserialize(reader);
				byte b = reader.ReadByte();
				bool flag = (b & CharacterNetworkTransformManager.CharacterUpdateMessage.nonIdentityRotationBit) > 0;
				this.isGrounded = ((b & CharacterNetworkTransformManager.CharacterUpdateMessage.isGroundedBit) > 0);
				this.timestamp = reader.ReadSingle();
				this.gameObject = reader.ReadGameObject();
				this.newPosition = reader.ReadVector3();
				this.aimDirection = reader.ReadPackedUnitVector3().Unpack();
				this.moveVector = reader.ReadVector3();
				if (flag)
				{
					this.rotation = reader.ReadQuaternion();
				}
				else
				{
					this.rotation = Quaternion.identity;
				}
				if (this.isGrounded)
				{
					this.groundNormal = reader.ReadPackedUnitVector3().Unpack();
				}
			}

			// Token: 0x04004473 RID: 17523
			private static readonly int byteSize = 1;

			// Token: 0x04004474 RID: 17524
			private static readonly int floatSize = 4;

			// Token: 0x04004475 RID: 17525
			private static readonly int vector3Size = CharacterNetworkTransformManager.CharacterUpdateMessage.floatSize * 3;

			// Token: 0x04004476 RID: 17526
			private static readonly int packedUint32MaxSize = 5;

			// Token: 0x04004477 RID: 17527
			private static readonly int gameObjectSize = CharacterNetworkTransformManager.CharacterUpdateMessage.packedUint32MaxSize;

			// Token: 0x04004478 RID: 17528
			private static readonly int packedUnitVector3Size = 2;

			// Token: 0x04004479 RID: 17529
			private static readonly int quaternionSize = CharacterNetworkTransformManager.CharacterUpdateMessage.floatSize * 4;

			// Token: 0x0400447A RID: 17530
			public static readonly int maxNetworkSize = CharacterNetworkTransformManager.CharacterUpdateMessage.byteSize + CharacterNetworkTransformManager.CharacterUpdateMessage.floatSize + CharacterNetworkTransformManager.CharacterUpdateMessage.gameObjectSize + CharacterNetworkTransformManager.CharacterUpdateMessage.vector3Size + CharacterNetworkTransformManager.CharacterUpdateMessage.packedUnitVector3Size + CharacterNetworkTransformManager.CharacterUpdateMessage.vector3Size + CharacterNetworkTransformManager.CharacterUpdateMessage.quaternionSize + CharacterNetworkTransformManager.CharacterUpdateMessage.packedUnitVector3Size;

			// Token: 0x0400447B RID: 17531
			private static readonly byte nonIdentityRotationBit = 2;

			// Token: 0x0400447C RID: 17532
			private static readonly byte isGroundedBit = 4;

			// Token: 0x0400447D RID: 17533
			public float timestamp;

			// Token: 0x0400447E RID: 17534
			public GameObject gameObject;

			// Token: 0x0400447F RID: 17535
			public Vector3 newPosition;

			// Token: 0x04004480 RID: 17536
			public Vector3 aimDirection;

			// Token: 0x04004481 RID: 17537
			public Vector3 moveVector;

			// Token: 0x04004482 RID: 17538
			public Quaternion rotation;

			// Token: 0x04004483 RID: 17539
			public bool isGrounded;

			// Token: 0x04004484 RID: 17540
			public Vector3 groundNormal;
		}

		// Token: 0x02000C34 RID: 3124
		public struct NetSnapshot
		{
			// Token: 0x04004485 RID: 17541
			public GameObject gameObject;

			// Token: 0x04004486 RID: 17542
			public CharacterNetworkTransform.Snapshot snapshot;
		}
	}
}
