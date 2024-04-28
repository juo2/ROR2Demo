using System;
using System.Collections.Generic;
using RoR2.Networking;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000638 RID: 1592
	public class CharacterMasterNotificationQueue : MonoBehaviour
	{
		// Token: 0x06001E80 RID: 7808 RVA: 0x00082F50 File Offset: 0x00081150
		public static void PushPickupNotification(CharacterMaster characterMaster, PickupIndex pickupIndex)
		{
			if (!characterMaster.hasAuthority)
			{
				Debug.LogError("Can't PushPickupNotification for " + Util.GetBestMasterName(characterMaster) + " because they aren't local.");
				return;
			}
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			ItemIndex itemIndex = pickupDef.itemIndex;
			if (itemIndex != ItemIndex.None)
			{
				CharacterMasterNotificationQueue.PushItemNotification(characterMaster, itemIndex);
				return;
			}
			EquipmentIndex equipmentIndex = pickupDef.equipmentIndex;
			if (equipmentIndex != EquipmentIndex.None)
			{
				CharacterMasterNotificationQueue.PushEquipmentNotification(characterMaster, equipmentIndex);
				return;
			}
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x00082FB0 File Offset: 0x000811B0
		public static void PushItemNotification(CharacterMaster characterMaster, ItemIndex itemIndex)
		{
			if (!characterMaster.hasAuthority)
			{
				Debug.LogError("Can't PushItemNotification for " + Util.GetBestMasterName(characterMaster) + " because they aren't local.");
				return;
			}
			CharacterMasterNotificationQueue notificationQueueForMaster = CharacterMasterNotificationQueue.GetNotificationQueueForMaster(characterMaster);
			if (notificationQueueForMaster && itemIndex != ItemIndex.None)
			{
				ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
				if (itemDef == null || itemDef.hidden)
				{
					return;
				}
				float duration = 6f;
				if (characterMaster.inventory.GetItemCount(itemIndex) > 1)
				{
					duration = 6f;
				}
				notificationQueueForMaster.PushNotification(new CharacterMasterNotificationQueue.NotificationInfo(ItemCatalog.GetItemDef(itemIndex), null), duration);
			}
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x0008303C File Offset: 0x0008123C
		public static void PushEquipmentNotification(CharacterMaster characterMaster, EquipmentIndex equipmentIndex)
		{
			if (!characterMaster.hasAuthority)
			{
				Debug.LogError("Can't PushEquipmentNotification for " + Util.GetBestMasterName(characterMaster) + " because they aren't local.");
				return;
			}
			CharacterMasterNotificationQueue notificationQueueForMaster = CharacterMasterNotificationQueue.GetNotificationQueueForMaster(characterMaster);
			if (notificationQueueForMaster && equipmentIndex != EquipmentIndex.None)
			{
				notificationQueueForMaster.PushNotification(new CharacterMasterNotificationQueue.NotificationInfo(EquipmentCatalog.GetEquipmentDef(equipmentIndex), null), 6f);
			}
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x00083098 File Offset: 0x00081298
		public static void PushArtifactNotification(CharacterMaster characterMaster, ArtifactDef artifactDef)
		{
			if (!characterMaster.hasAuthority)
			{
				Debug.LogError("Can't PushArtifactNotification for " + Util.GetBestMasterName(characterMaster) + " because they aren't local.");
				return;
			}
			CharacterMasterNotificationQueue notificationQueueForMaster = CharacterMasterNotificationQueue.GetNotificationQueueForMaster(characterMaster);
			if (notificationQueueForMaster)
			{
				notificationQueueForMaster.PushNotification(new CharacterMasterNotificationQueue.NotificationInfo(artifactDef, null), 6f);
			}
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x000830EC File Offset: 0x000812EC
		public static void PushEquipmentTransformNotification(CharacterMaster characterMaster, EquipmentIndex oldIndex, EquipmentIndex newIndex, CharacterMasterNotificationQueue.TransformationType transformationType)
		{
			if (!characterMaster.hasAuthority)
			{
				Debug.LogError("Can't PushEquipmentTransformNotification for " + Util.GetBestMasterName(characterMaster) + " because they aren't local.");
				return;
			}
			CharacterMasterNotificationQueue notificationQueueForMaster = CharacterMasterNotificationQueue.GetNotificationQueueForMaster(characterMaster);
			if (notificationQueueForMaster && oldIndex != EquipmentIndex.None && newIndex != EquipmentIndex.None)
			{
				object equipmentDef = EquipmentCatalog.GetEquipmentDef(oldIndex);
				CharacterMasterNotificationQueue.TransformationInfo transformation = new CharacterMasterNotificationQueue.TransformationInfo(transformationType, equipmentDef);
				CharacterMasterNotificationQueue.NotificationInfo info = new CharacterMasterNotificationQueue.NotificationInfo(EquipmentCatalog.GetEquipmentDef(newIndex), transformation);
				notificationQueueForMaster.PushNotification(info, 6f);
			}
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x0008315C File Offset: 0x0008135C
		public static void PushItemTransformNotification(CharacterMaster characterMaster, ItemIndex oldIndex, ItemIndex newIndex, CharacterMasterNotificationQueue.TransformationType transformationType)
		{
			if (!characterMaster.hasAuthority)
			{
				Debug.LogError("Can't PushItemTransformNotification for " + Util.GetBestMasterName(characterMaster) + " because they aren't local.");
				return;
			}
			CharacterMasterNotificationQueue notificationQueueForMaster = CharacterMasterNotificationQueue.GetNotificationQueueForMaster(characterMaster);
			if (notificationQueueForMaster && oldIndex != ItemIndex.None && newIndex != ItemIndex.None)
			{
				object itemDef = ItemCatalog.GetItemDef(oldIndex);
				CharacterMasterNotificationQueue.TransformationInfo transformation = new CharacterMasterNotificationQueue.TransformationInfo(transformationType, itemDef);
				CharacterMasterNotificationQueue.NotificationInfo info = new CharacterMasterNotificationQueue.NotificationInfo(ItemCatalog.GetItemDef(newIndex), transformation);
				notificationQueueForMaster.PushNotification(info, 6f);
			}
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x000831CC File Offset: 0x000813CC
		public static CharacterMasterNotificationQueue GetNotificationQueueForMaster(CharacterMaster master)
		{
			if (master != null)
			{
				CharacterMasterNotificationQueue characterMasterNotificationQueue = master.GetComponent<CharacterMasterNotificationQueue>();
				if (!characterMasterNotificationQueue)
				{
					characterMasterNotificationQueue = master.gameObject.AddComponent<CharacterMasterNotificationQueue>();
					characterMasterNotificationQueue.master = master;
				}
				return characterMasterNotificationQueue;
			}
			return null;
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x00083207 File Offset: 0x00081407
		public static void SendTransformNotification(CharacterMaster characterMaster, ItemIndex oldIndex, ItemIndex newIndex, CharacterMasterNotificationQueue.TransformationType transformationType)
		{
			CharacterMasterNotificationQueue.SendTransformNotificationInternal(characterMaster, PickupCatalog.FindPickupIndex(oldIndex), PickupCatalog.FindPickupIndex(newIndex), transformationType);
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x0008321C File Offset: 0x0008141C
		public static void SendTransformNotification(CharacterMaster characterMaster, EquipmentIndex oldIndex, EquipmentIndex newIndex, CharacterMasterNotificationQueue.TransformationType transformationType)
		{
			CharacterMasterNotificationQueue.SendTransformNotificationInternal(characterMaster, PickupCatalog.FindPickupIndex(oldIndex), PickupCatalog.FindPickupIndex(newIndex), transformationType);
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x00083234 File Offset: 0x00081434
		private static void SendTransformNotificationInternal(CharacterMaster characterMaster, PickupIndex oldIndex, PickupIndex newIndex, CharacterMasterNotificationQueue.TransformationType transformationType)
		{
			if (NetworkServer.active)
			{
				CharacterMasterNotificationQueue.TransformNotificationMessage msg = new CharacterMasterNotificationQueue.TransformNotificationMessage
				{
					masterGameObject = characterMaster.gameObject,
					oldIndex = oldIndex,
					newIndex = newIndex,
					transformationType = transformationType
				};
				NetworkServer.SendByChannelToAll(78, msg, QosChannelIndex.chat.intVal);
				return;
			}
			Debug.LogError("Can't SendTransformNotification if this isn't the server.");
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x00083290 File Offset: 0x00081490
		[NetworkMessageHandler(msgType = 78, client = true)]
		private static void HandleTransformNotification(NetworkMessage netMsg)
		{
			netMsg.ReadMessage<CharacterMasterNotificationQueue.TransformNotificationMessage>(CharacterMasterNotificationQueue.transformNotificationMessageInstance);
			if (CharacterMasterNotificationQueue.transformNotificationMessageInstance.masterGameObject)
			{
				CharacterMaster component = CharacterMasterNotificationQueue.transformNotificationMessageInstance.masterGameObject.GetComponent<CharacterMaster>();
				if (component && component.hasAuthority)
				{
					PickupDef pickupDef = PickupCatalog.GetPickupDef(CharacterMasterNotificationQueue.transformNotificationMessageInstance.oldIndex);
					PickupDef pickupDef2 = PickupCatalog.GetPickupDef(CharacterMasterNotificationQueue.transformNotificationMessageInstance.newIndex);
					if (pickupDef != null && pickupDef2 != null)
					{
						if (pickupDef2.equipmentIndex != EquipmentIndex.None)
						{
							CharacterMasterNotificationQueue.PushEquipmentTransformNotification(component, pickupDef.equipmentIndex, pickupDef2.equipmentIndex, CharacterMasterNotificationQueue.transformNotificationMessageInstance.transformationType);
						}
						else if (pickupDef2.itemIndex != ItemIndex.None)
						{
							CharacterMasterNotificationQueue.PushItemTransformNotification(component, pickupDef.itemIndex, pickupDef2.itemIndex, CharacterMasterNotificationQueue.transformNotificationMessageInstance.transformationType);
						}
					}
					else
					{
						Debug.LogError(string.Format("Can't handle transform notification for pickup indices:  {0} -> {1}", CharacterMasterNotificationQueue.transformNotificationMessageInstance.oldIndex, CharacterMasterNotificationQueue.transformNotificationMessageInstance.newIndex));
					}
				}
			}
			CharacterMasterNotificationQueue.transformNotificationMessageInstance.masterGameObject = null;
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06001E8B RID: 7819 RVA: 0x00083390 File Offset: 0x00081590
		// (remove) Token: 0x06001E8C RID: 7820 RVA: 0x000833C8 File Offset: 0x000815C8
		public event Action<CharacterMasterNotificationQueue> onCurrentNotificationChanged;

		// Token: 0x06001E8D RID: 7821 RVA: 0x00083400 File Offset: 0x00081600
		public void FixedUpdate()
		{
			if (this.GetCurrentNotificationT() > 1f)
			{
				this.notifications.RemoveAt(0);
				if (this.notifications.Count > 0)
				{
					this.notifications[0].startTime = Run.instance.fixedTime;
				}
				Action<CharacterMasterNotificationQueue> action = this.onCurrentNotificationChanged;
				if (action == null)
				{
					return;
				}
				action(this);
			}
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x00083460 File Offset: 0x00081660
		public CharacterMasterNotificationQueue.NotificationInfo GetCurrentNotification()
		{
			if (this.notifications.Count > 0)
			{
				return this.notifications[0].notification;
			}
			return null;
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x00083484 File Offset: 0x00081684
		private void PushNotification(CharacterMasterNotificationQueue.NotificationInfo info, float duration)
		{
			if (this.notifications.Count == 0 || this.notifications[this.notifications.Count - 1].notification != info)
			{
				this.notifications.Add(new CharacterMasterNotificationQueue.TimedNotificationInfo
				{
					notification = info,
					startTime = Run.instance.fixedTime,
					duration = duration
				});
				if (this.notifications.Count == 1)
				{
					Action<CharacterMasterNotificationQueue> action = this.onCurrentNotificationChanged;
					if (action == null)
					{
						return;
					}
					action(this);
				}
			}
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x00083510 File Offset: 0x00081710
		public float GetCurrentNotificationT()
		{
			if (this.notifications.Count > 0)
			{
				CharacterMasterNotificationQueue.TimedNotificationInfo timedNotificationInfo = this.notifications[0];
				return (Run.instance.fixedTime - timedNotificationInfo.startTime) / timedNotificationInfo.duration;
			}
			return 0f;
		}

		// Token: 0x0400241E RID: 9246
		public const float firstNotificationLengthSeconds = 6f;

		// Token: 0x0400241F RID: 9247
		public const float repeatNotificationLengthSeconds = 6f;

		// Token: 0x04002420 RID: 9248
		private static readonly CharacterMasterNotificationQueue.TransformNotificationMessage transformNotificationMessageInstance = new CharacterMasterNotificationQueue.TransformNotificationMessage();

		// Token: 0x04002421 RID: 9249
		private CharacterMaster master;

		// Token: 0x04002422 RID: 9250
		private List<CharacterMasterNotificationQueue.TimedNotificationInfo> notifications = new List<CharacterMasterNotificationQueue.TimedNotificationInfo>();

		// Token: 0x02000639 RID: 1593
		public enum TransformationType
		{
			// Token: 0x04002425 RID: 9253
			Default,
			// Token: 0x04002426 RID: 9254
			ContagiousVoid,
			// Token: 0x04002427 RID: 9255
			CloverVoid,
			// Token: 0x04002428 RID: 9256
			Suppressed,
			// Token: 0x04002429 RID: 9257
			LunarSun,
			// Token: 0x0400242A RID: 9258
			RegeneratingScrapRegen
		}

		// Token: 0x0200063A RID: 1594
		public class TransformationInfo
		{
			// Token: 0x06001E93 RID: 7827 RVA: 0x00083578 File Offset: 0x00081778
			public static bool operator ==(CharacterMasterNotificationQueue.TransformationInfo lhs, CharacterMasterNotificationQueue.TransformationInfo rhs)
			{
				bool flag = lhs == null;
				bool flag2 = rhs == null;
				return (flag && flag2) || (!flag && !flag2 && lhs.previousData == rhs.previousData && lhs.transformationType == rhs.transformationType);
			}

			// Token: 0x06001E94 RID: 7828 RVA: 0x000835BA File Offset: 0x000817BA
			public static bool operator !=(CharacterMasterNotificationQueue.TransformationInfo lhs, CharacterMasterNotificationQueue.TransformationInfo rhs)
			{
				return !(lhs == rhs);
			}

			// Token: 0x06001E95 RID: 7829 RVA: 0x000835C6 File Offset: 0x000817C6
			public TransformationInfo(CharacterMasterNotificationQueue.TransformationType transformationType, object previousData)
			{
				this.transformationType = transformationType;
				this.previousData = previousData;
			}

			// Token: 0x0400242B RID: 9259
			public readonly CharacterMasterNotificationQueue.TransformationType transformationType;

			// Token: 0x0400242C RID: 9260
			public readonly object previousData;
		}

		// Token: 0x0200063B RID: 1595
		public class NotificationInfo
		{
			// Token: 0x06001E96 RID: 7830 RVA: 0x000835DC File Offset: 0x000817DC
			public static bool operator ==(CharacterMasterNotificationQueue.NotificationInfo lhs, CharacterMasterNotificationQueue.NotificationInfo rhs)
			{
				bool flag = lhs == null;
				bool flag2 = rhs == null;
				return (flag && flag2) || (!flag && !flag2 && lhs.data == rhs.data && lhs.transformation == rhs.transformation);
			}

			// Token: 0x06001E97 RID: 7831 RVA: 0x00083621 File Offset: 0x00081821
			public static bool operator !=(CharacterMasterNotificationQueue.NotificationInfo lhs, CharacterMasterNotificationQueue.NotificationInfo rhs)
			{
				return !(lhs == rhs);
			}

			// Token: 0x06001E98 RID: 7832 RVA: 0x0008362D File Offset: 0x0008182D
			public NotificationInfo(object data, CharacterMasterNotificationQueue.TransformationInfo transformation = null)
			{
				this.data = data;
				this.transformation = transformation;
			}

			// Token: 0x0400242D RID: 9261
			public readonly object data;

			// Token: 0x0400242E RID: 9262
			public readonly CharacterMasterNotificationQueue.TransformationInfo transformation;
		}

		// Token: 0x0200063C RID: 1596
		private class TimedNotificationInfo
		{
			// Token: 0x0400242F RID: 9263
			public CharacterMasterNotificationQueue.NotificationInfo notification;

			// Token: 0x04002430 RID: 9264
			public float startTime;

			// Token: 0x04002431 RID: 9265
			public float duration;
		}

		// Token: 0x0200063D RID: 1597
		private class TransformNotificationMessage : MessageBase
		{
			// Token: 0x06001E9B RID: 7835 RVA: 0x00083643 File Offset: 0x00081843
			public override void Serialize(NetworkWriter writer)
			{
				writer.Write(this.masterGameObject);
				GeneratedNetworkCode._WritePickupIndex_None(writer, this.oldIndex);
				GeneratedNetworkCode._WritePickupIndex_None(writer, this.newIndex);
				writer.Write((int)this.transformationType);
			}

			// Token: 0x06001E9C RID: 7836 RVA: 0x00083675 File Offset: 0x00081875
			public override void Deserialize(NetworkReader reader)
			{
				this.masterGameObject = reader.ReadGameObject();
				this.oldIndex = GeneratedNetworkCode._ReadPickupIndex_None(reader);
				this.newIndex = GeneratedNetworkCode._ReadPickupIndex_None(reader);
				this.transformationType = (CharacterMasterNotificationQueue.TransformationType)reader.ReadInt32();
			}

			// Token: 0x04002432 RID: 9266
			public GameObject masterGameObject;

			// Token: 0x04002433 RID: 9267
			public PickupIndex oldIndex;

			// Token: 0x04002434 RID: 9268
			public PickupIndex newIndex;

			// Token: 0x04002435 RID: 9269
			public CharacterMasterNotificationQueue.TransformationType transformationType;
		}
	}
}
