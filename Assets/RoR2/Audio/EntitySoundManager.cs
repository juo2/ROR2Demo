using System;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Audio
{
	// Token: 0x02000E54 RID: 3668
	public static class EntitySoundManager
	{
		// Token: 0x0600540A RID: 21514 RVA: 0x0015B0C0 File Offset: 0x001592C0
		[NetworkMessageHandler(client = true, server = false, msgType = 73)]
		private static void HandleMessage(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<EntitySoundManager.EntitySoundMessage>(EntitySoundManager.sharedMessage);
			if (EntitySoundManager.sharedMessage.networkIdentity)
			{
				EntitySoundManager.EmitSoundLocal(NetworkSoundEventCatalog.GetAkIdFromNetworkSoundEventIndex(EntitySoundManager.sharedMessage.networkSoundEventIndex), EntitySoundManager.sharedMessage.networkIdentity.gameObject);
			}
			EntitySoundManager.sharedMessage.Clear();
		}

		// Token: 0x0600540B RID: 21515 RVA: 0x0015B11C File Offset: 0x0015931C
		public static uint EmitSoundLocal(AkEventIdArg akEventId, GameObject gameObject)
		{
			if (akEventId == 0U)
			{
				return 0U;
			}
			return AkSoundEngine.PostEvent(akEventId, gameObject);
		}

		// Token: 0x0600540C RID: 21516 RVA: 0x0015B134 File Offset: 0x00159334
		public static void EmitSoundServer(AkEventIdArg akEventId, GameObject gameObject)
		{
			NetworkSoundEventIndex networkSoundEventIndex = NetworkSoundEventCatalog.FindNetworkSoundEventIndex(akEventId);
			if (networkSoundEventIndex == NetworkSoundEventIndex.Invalid)
			{
				Debug.LogWarningFormat("Cannot emit sound \"{0}\" on object \"{1}\": Event is not registered in NetworkSoundEventCatalog.", new object[]
				{
					akEventId.id,
					gameObject
				});
				return;
			}
			EntitySoundManager.EmitSoundServer(networkSoundEventIndex, gameObject);
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x0015B17C File Offset: 0x0015937C
		public static void EmitSoundServer(NetworkSoundEventIndex networkSoundEventIndex, GameObject gameObject)
		{
			NetworkIdentity component = gameObject.GetComponent<NetworkIdentity>();
			if (!component)
			{
				Debug.LogWarningFormat("Cannot emit sound \"{0}\" on object \"{1}\": Object has no NetworkIdentity.", new object[]
				{
					NetworkSoundEventCatalog.GetAkIdFromNetworkSoundEventIndex(networkSoundEventIndex),
					gameObject
				});
				return;
			}
			EntitySoundManager.EmitSoundServer(networkSoundEventIndex, component);
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x0015B1C2 File Offset: 0x001593C2
		public static void EmitSoundServer(NetworkSoundEventIndex networkSoundEventIndex, NetworkIdentity networkIdentity)
		{
			EntitySoundManager.sharedMessage.networkIdentity = networkIdentity;
			EntitySoundManager.sharedMessage.networkSoundEventIndex = networkSoundEventIndex;
			NetworkServer.SendByChannelToAll(EntitySoundManager.messageType, EntitySoundManager.sharedMessage, EntitySoundManager.channel.intVal);
			EntitySoundManager.sharedMessage.Clear();
		}

		// Token: 0x04004FE6 RID: 20454
		private static readonly EntitySoundManager.EntitySoundMessage sharedMessage = new EntitySoundManager.EntitySoundMessage();

		// Token: 0x04004FE7 RID: 20455
		private static readonly QosChannelIndex channel = QosChannelIndex.defaultReliable;

		// Token: 0x04004FE8 RID: 20456
		private static readonly short messageType = 73;

		// Token: 0x02000E55 RID: 3669
		private class EntitySoundMessage : MessageBase
		{
			// Token: 0x06005410 RID: 21520 RVA: 0x0015B21B File Offset: 0x0015941B
			public override void Serialize(NetworkWriter writer)
			{
				base.Serialize(writer);
				writer.WriteNetworkSoundEventIndex(this.networkSoundEventIndex);
				writer.Write(this.networkIdentity);
			}

			// Token: 0x06005411 RID: 21521 RVA: 0x0015B23C File Offset: 0x0015943C
			public override void Deserialize(NetworkReader reader)
			{
				base.Deserialize(reader);
				this.networkSoundEventIndex = reader.ReadNetworkSoundEventIndex();
				this.networkIdentity = reader.ReadNetworkIdentity();
			}

			// Token: 0x06005412 RID: 21522 RVA: 0x0015B25D File Offset: 0x0015945D
			public void Clear()
			{
				this.networkSoundEventIndex = NetworkSoundEventIndex.Invalid;
				this.networkIdentity = null;
			}

			// Token: 0x04004FE9 RID: 20457
			public NetworkSoundEventIndex networkSoundEventIndex;

			// Token: 0x04004FEA RID: 20458
			public NetworkIdentity networkIdentity;
		}
	}
}
