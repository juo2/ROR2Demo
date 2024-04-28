using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D55 RID: 3413
	public class NotificationUIController : MonoBehaviour
	{
		// Token: 0x06004E3B RID: 20027 RVA: 0x00142DD6 File Offset: 0x00140FD6
		public void OnEnable()
		{
			CharacterMaster.onCharacterMasterLost += this.OnCharacterMasterLost;
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x00142DE9 File Offset: 0x00140FE9
		public void OnDisable()
		{
			CharacterMaster.onCharacterMasterLost -= this.OnCharacterMasterLost;
			this.CleanUpCurrentMaster();
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x00142E04 File Offset: 0x00141004
		public void Update()
		{
			if (this.hud.targetMaster != this.targetMaster)
			{
				this.SetTargetMaster(this.hud.targetMaster);
			}
			if (this.currentNotification && this.notificationQueue)
			{
				this.currentNotification.SetNotificationT(this.notificationQueue.GetCurrentNotificationT());
			}
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x00142E6C File Offset: 0x0014106C
		private void SetUpNotification(CharacterMasterNotificationQueue.NotificationInfo notificationInfo)
		{
			object obj;
			if (notificationInfo.transformation != null)
			{
				GameObject original = this.genericTransformationNotificationPrefab;
				switch (notificationInfo.transformation.transformationType)
				{
				case CharacterMasterNotificationQueue.TransformationType.ContagiousVoid:
					if (this.contagiousVoidTransformationNotificationPrefab)
					{
						original = this.contagiousVoidTransformationNotificationPrefab;
					}
					break;
				case CharacterMasterNotificationQueue.TransformationType.CloverVoid:
					if (this.cloverVoidTransformationNotificationPrefab)
					{
						original = this.cloverVoidTransformationNotificationPrefab;
					}
					break;
				case CharacterMasterNotificationQueue.TransformationType.Suppressed:
					if (this.suppressedTransformationNotificationPrefab)
					{
						original = this.suppressedTransformationNotificationPrefab;
					}
					break;
				case CharacterMasterNotificationQueue.TransformationType.LunarSun:
					if (this.lunarSunTransformationNotificationPrefab)
					{
						original = this.lunarSunTransformationNotificationPrefab;
					}
					break;
				case CharacterMasterNotificationQueue.TransformationType.RegeneratingScrapRegen:
					if (this.regeneratingScrapRegenTransformationNotificationPrefab)
					{
						original = this.regeneratingScrapRegenTransformationNotificationPrefab;
					}
					break;
				}
				this.currentNotification = UnityEngine.Object.Instantiate<GameObject>(original).GetComponent<GenericNotification>();
				obj = notificationInfo.transformation.previousData;
				if (obj != null)
				{
					ItemDef itemDef;
					if ((itemDef = (obj as ItemDef)) == null)
					{
						EquipmentDef equipmentDef;
						if ((equipmentDef = (obj as EquipmentDef)) != null)
						{
							EquipmentDef previousEquipment = equipmentDef;
							this.currentNotification.SetPreviousEquipment(previousEquipment);
						}
					}
					else
					{
						ItemDef previousItem = itemDef;
						this.currentNotification.SetPreviousItem(previousItem);
					}
				}
			}
			else
			{
				this.currentNotification = UnityEngine.Object.Instantiate<GameObject>(this.genericNotificationPrefab).GetComponent<GenericNotification>();
			}
			obj = notificationInfo.data;
			if (obj != null)
			{
				ItemDef itemDef;
				if ((itemDef = (obj as ItemDef)) == null)
				{
					EquipmentDef equipmentDef;
					if ((equipmentDef = (obj as EquipmentDef)) == null)
					{
						ArtifactDef artifactDef;
						if ((artifactDef = (obj as ArtifactDef)) != null)
						{
							ArtifactDef artifact = artifactDef;
							this.currentNotification.SetArtifact(artifact);
						}
					}
					else
					{
						EquipmentDef equipment = equipmentDef;
						this.currentNotification.SetEquipment(equipment);
					}
				}
				else
				{
					ItemDef item = itemDef;
					this.currentNotification.SetItem(item);
				}
			}
			this.currentNotification.GetComponent<RectTransform>().SetParent(base.GetComponent<RectTransform>(), false);
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x00143012 File Offset: 0x00141212
		private void OnCurrentNotificationChanged(CharacterMasterNotificationQueue notificationQueue)
		{
			this.ShowCurrentNotification(notificationQueue);
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x0014301C File Offset: 0x0014121C
		private void ShowCurrentNotification(CharacterMasterNotificationQueue notificationQueue)
		{
			this.DestroyCurrentNotification();
			CharacterMasterNotificationQueue.NotificationInfo notificationInfo = notificationQueue.GetCurrentNotification();
			if (notificationInfo != null)
			{
				this.SetUpNotification(notificationInfo);
			}
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x00143046 File Offset: 0x00141246
		private void DestroyCurrentNotification()
		{
			if (this.currentNotification)
			{
				UnityEngine.Object.Destroy(this.currentNotification.gameObject);
				this.currentNotification = null;
			}
		}

		// Token: 0x06004E42 RID: 20034 RVA: 0x0014306C File Offset: 0x0014126C
		private void SetTargetMaster(CharacterMaster newMaster)
		{
			this.DestroyCurrentNotification();
			this.CleanUpCurrentMaster();
			this.targetMaster = newMaster;
			if (newMaster)
			{
				this.notificationQueue = CharacterMasterNotificationQueue.GetNotificationQueueForMaster(newMaster);
				this.notificationQueue.onCurrentNotificationChanged += this.OnCurrentNotificationChanged;
				this.ShowCurrentNotification(this.notificationQueue);
			}
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x001430C3 File Offset: 0x001412C3
		private void OnCharacterMasterLost(CharacterMaster master)
		{
			if (master == this.targetMaster)
			{
				this.CleanUpCurrentMaster();
			}
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x001430D4 File Offset: 0x001412D4
		private void CleanUpCurrentMaster()
		{
			if (this.notificationQueue)
			{
				this.notificationQueue.onCurrentNotificationChanged -= this.OnCurrentNotificationChanged;
			}
			this.notificationQueue = null;
			this.targetMaster = null;
		}

		// Token: 0x04004AEA RID: 19178
		[SerializeField]
		private HUD hud;

		// Token: 0x04004AEB RID: 19179
		[SerializeField]
		private GameObject genericNotificationPrefab;

		// Token: 0x04004AEC RID: 19180
		[SerializeField]
		private GameObject genericTransformationNotificationPrefab;

		// Token: 0x04004AED RID: 19181
		[SerializeField]
		private GameObject contagiousVoidTransformationNotificationPrefab;

		// Token: 0x04004AEE RID: 19182
		[SerializeField]
		private GameObject suppressedTransformationNotificationPrefab;

		// Token: 0x04004AEF RID: 19183
		[SerializeField]
		private GameObject cloverVoidTransformationNotificationPrefab;

		// Token: 0x04004AF0 RID: 19184
		[SerializeField]
		private GameObject lunarSunTransformationNotificationPrefab;

		// Token: 0x04004AF1 RID: 19185
		[SerializeField]
		private GameObject regeneratingScrapRegenTransformationNotificationPrefab;

		// Token: 0x04004AF2 RID: 19186
		private GenericNotification currentNotification;

		// Token: 0x04004AF3 RID: 19187
		private CharacterMasterNotificationQueue notificationQueue;

		// Token: 0x04004AF4 RID: 19188
		private CharacterMaster targetMaster;
	}
}
