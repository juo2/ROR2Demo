using System;
using System.Text;
using HG;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000CFC RID: 3324
	public class EquipmentIcon : MonoBehaviour
	{
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06004BBC RID: 19388 RVA: 0x00137530 File Offset: 0x00135730
		public bool hasEquipment
		{
			get
			{
				return this.currentDisplayData.hasEquipment;
			}
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x00137540 File Offset: 0x00135740
		private void SetDisplayData(EquipmentIcon.DisplayData newDisplayData)
		{
			if (!this.currentDisplayData.isReady && newDisplayData.isReady)
			{
				this.DoStockFlash();
			}
			if (this.displayRoot)
			{
				this.displayRoot.SetActive(!newDisplayData.hideEntireDisplay);
			}
			if (newDisplayData.stock > this.currentDisplayData.stock)
			{
				Util.PlaySound("Play_item_proc_equipMag", RoR2Application.instance.gameObject);
				this.DoStockFlash();
			}
			if (this.isReadyPanelObject)
			{
				this.isReadyPanelObject.SetActive(newDisplayData.isReady);
			}
			if (this.isAutoCastPanelObject)
			{
				if (this.targetInventory)
				{
					this.isAutoCastPanelObject.SetActive(this.targetInventory.GetItemCount(RoR2Content.Items.AutoCastEquipment) > 0);
				}
				else
				{
					this.isAutoCastPanelObject.SetActive(false);
				}
			}
			if (this.iconImage)
			{
				Texture texture = null;
				Color color = Color.clear;
				if (newDisplayData.equipmentDef != null)
				{
					color = ((newDisplayData.stock > 0) ? Color.white : Color.gray);
					texture = newDisplayData.equipmentDef.pickupIconTexture;
				}
				this.iconImage.texture = texture;
				this.iconImage.color = color;
			}
			if (this.cooldownText)
			{
				this.cooldownText.gameObject.SetActive(newDisplayData.showCooldown);
				if (newDisplayData.cooldownValue != this.currentDisplayData.cooldownValue)
				{
					StringBuilder stringBuilder = HG.StringBuilderPool.RentStringBuilder();
					stringBuilder.AppendInt(newDisplayData.cooldownValue, 1U, uint.MaxValue);
					this.cooldownText.SetText(stringBuilder);
					HG.StringBuilderPool.ReturnStringBuilder(stringBuilder);
				}
			}
			if (this.stockText)
			{
				if (newDisplayData.hasEquipment && (newDisplayData.maxStock > 1 || newDisplayData.stock > 1))
				{
					this.stockText.gameObject.SetActive(true);
					StringBuilder stringBuilder2 = HG.StringBuilderPool.RentStringBuilder();
					stringBuilder2.AppendInt(newDisplayData.stock, 1U, uint.MaxValue);
					this.stockText.SetText(stringBuilder2);
					HG.StringBuilderPool.ReturnStringBuilder(stringBuilder2);
				}
				else
				{
					this.stockText.gameObject.SetActive(false);
				}
			}
			string titleToken = null;
			string bodyToken = null;
			Color titleColor = Color.white;
			Color gray = Color.gray;
			if (newDisplayData.equipmentDef != null)
			{
				titleToken = newDisplayData.equipmentDef.nameToken;
				bodyToken = newDisplayData.equipmentDef.pickupToken;
				titleColor = ColorCatalog.GetColor(newDisplayData.equipmentDef.colorIndex);
			}
			if (this.tooltipProvider)
			{
				this.tooltipProvider.titleToken = titleToken;
				this.tooltipProvider.titleColor = titleColor;
				this.tooltipProvider.bodyToken = bodyToken;
				this.tooltipProvider.bodyColor = gray;
			}
			this.currentDisplayData = newDisplayData;
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x001377E4 File Offset: 0x001359E4
		private void DoReminderFlash()
		{
			if (this.reminderFlashPanelObject)
			{
				AnimateUIAlpha component = this.reminderFlashPanelObject.GetComponent<AnimateUIAlpha>();
				if (component)
				{
					component.time = 0f;
				}
				this.reminderFlashPanelObject.SetActive(true);
			}
			this.equipmentReminderTimer = 5f;
		}

		// Token: 0x06004BBF RID: 19391 RVA: 0x00137834 File Offset: 0x00135A34
		private void DoStockFlash()
		{
			this.DoReminderFlash();
			if (this.stockFlashPanelObject)
			{
				AnimateUIAlpha component = this.stockFlashPanelObject.GetComponent<AnimateUIAlpha>();
				if (component)
				{
					component.time = 0f;
				}
				this.stockFlashPanelObject.SetActive(true);
			}
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x00137880 File Offset: 0x00135A80
		private EquipmentIcon.DisplayData GenerateDisplayData()
		{
			EquipmentIcon.DisplayData result = default(EquipmentIcon.DisplayData);
			EquipmentIndex equipmentIndex = EquipmentIndex.None;
			if (this.targetInventory)
			{
				EquipmentState equipmentState;
				if (this.displayAlternateEquipment)
				{
					equipmentState = this.targetInventory.alternateEquipmentState;
					result.hideEntireDisplay = (this.targetInventory.GetEquipmentSlotCount() <= 1);
				}
				else
				{
					equipmentState = this.targetInventory.currentEquipmentState;
					result.hideEntireDisplay = false;
				}
				Run.FixedTimeStamp now = Run.FixedTimeStamp.now;
				Run.FixedTimeStamp chargeFinishTime = equipmentState.chargeFinishTime;
				equipmentIndex = equipmentState.equipmentIndex;
				result.cooldownValue = (chargeFinishTime.isInfinity ? 0 : Mathf.CeilToInt(chargeFinishTime.timeUntilClamped));
				result.stock = (int)equipmentState.charges;
				result.maxStock = (this.targetEquipmentSlot ? this.targetEquipmentSlot.maxStock : 1);
			}
			else if (this.displayAlternateEquipment)
			{
				result.hideEntireDisplay = true;
			}
			result.equipmentDef = EquipmentCatalog.GetEquipmentDef(equipmentIndex);
			return result;
		}

		// Token: 0x06004BC1 RID: 19393 RVA: 0x0013796C File Offset: 0x00135B6C
		private void Update()
		{
			this.SetDisplayData(this.GenerateDisplayData());
			this.equipmentReminderTimer -= Time.deltaTime;
			if (this.currentDisplayData.isReady && this.equipmentReminderTimer < 0f && this.currentDisplayData.equipmentDef != null)
			{
				this.DoReminderFlash();
			}
		}

		// Token: 0x0400486F RID: 18543
		public Inventory targetInventory;

		// Token: 0x04004870 RID: 18544
		public EquipmentSlot targetEquipmentSlot;

		// Token: 0x04004871 RID: 18545
		public GameObject displayRoot;

		// Token: 0x04004872 RID: 18546
		public PlayerCharacterMasterController playerCharacterMasterController;

		// Token: 0x04004873 RID: 18547
		public RawImage iconImage;

		// Token: 0x04004874 RID: 18548
		public TextMeshProUGUI cooldownText;

		// Token: 0x04004875 RID: 18549
		public TextMeshProUGUI stockText;

		// Token: 0x04004876 RID: 18550
		public GameObject stockFlashPanelObject;

		// Token: 0x04004877 RID: 18551
		public GameObject reminderFlashPanelObject;

		// Token: 0x04004878 RID: 18552
		public GameObject isReadyPanelObject;

		// Token: 0x04004879 RID: 18553
		public GameObject isAutoCastPanelObject;

		// Token: 0x0400487A RID: 18554
		public TooltipProvider tooltipProvider;

		// Token: 0x0400487B RID: 18555
		public bool displayAlternateEquipment;

		// Token: 0x0400487C RID: 18556
		private int previousStockCount;

		// Token: 0x0400487D RID: 18557
		private float equipmentReminderTimer;

		// Token: 0x0400487E RID: 18558
		private EquipmentIcon.DisplayData currentDisplayData;

		// Token: 0x02000CFD RID: 3325
		private struct DisplayData
		{
			// Token: 0x170006DD RID: 1757
			// (get) Token: 0x06004BC3 RID: 19395 RVA: 0x001379CA File Offset: 0x00135BCA
			public bool isReady
			{
				get
				{
					return this.stock > 0;
				}
			}

			// Token: 0x170006DE RID: 1758
			// (get) Token: 0x06004BC4 RID: 19396 RVA: 0x001379D5 File Offset: 0x00135BD5
			public bool hasEquipment
			{
				get
				{
					return this.equipmentDef != null;
				}
			}

			// Token: 0x170006DF RID: 1759
			// (get) Token: 0x06004BC5 RID: 19397 RVA: 0x001379E3 File Offset: 0x00135BE3
			public bool showCooldown
			{
				get
				{
					return !this.isReady && this.hasEquipment;
				}
			}

			// Token: 0x0400487F RID: 18559
			public EquipmentDef equipmentDef;

			// Token: 0x04004880 RID: 18560
			public int cooldownValue;

			// Token: 0x04004881 RID: 18561
			public int stock;

			// Token: 0x04004882 RID: 18562
			public int maxStock;

			// Token: 0x04004883 RID: 18563
			public bool hideEntireDisplay;
		}
	}
}
