using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D7C RID: 3452
	public class ScoreboardStrip : MonoBehaviour
	{
		// Token: 0x06004F28 RID: 20264 RVA: 0x001479A8 File Offset: 0x00145BA8
		public void SetMaster(CharacterMaster newMaster)
		{
			if (this.master == newMaster)
			{
				return;
			}
			this.userBody = null;
			this.master = newMaster;
			if (this.master)
			{
				this.userBody = this.master.GetBody();
				this.userPlayerCharacterMasterController = this.master.GetComponent<PlayerCharacterMasterController>();
				this.itemInventoryDisplay.SetSubscribedInventory(this.master.inventory);
				this.equipmentIcon.targetInventory = this.master.inventory;
				this.UpdateMoneyText();
			}
			if (this.userAvatar && this.userAvatar.isActiveAndEnabled)
			{
				this.userAvatar.SetFromMaster(newMaster);
			}
			this.nameLabel.text = Util.GetBestMasterName(this.master);
			this.classIcon.texture = this.FindMasterPortrait();
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x00147A80 File Offset: 0x00145C80
		private void UpdateMoneyText()
		{
			if (this.master)
			{
				this.moneyText.text = string.Format("${0}", this.master.money);
			}
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x00147AB4 File Offset: 0x00145CB4
		private void Update()
		{
			this.UpdateMoneyText();
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x00147ABC File Offset: 0x00145CBC
		private Texture FindMasterPortrait()
		{
			if (this.userBody)
			{
				return this.userBody.portraitIcon;
			}
			if (this.master)
			{
				GameObject bodyPrefab = this.master.bodyPrefab;
				if (bodyPrefab)
				{
					CharacterBody component = bodyPrefab.GetComponent<CharacterBody>();
					if (component)
					{
						return component.portraitIcon;
					}
				}
			}
			return null;
		}

		// Token: 0x04004BDA RID: 19418
		public ItemInventoryDisplay itemInventoryDisplay;

		// Token: 0x04004BDB RID: 19419
		public EquipmentIcon equipmentIcon;

		// Token: 0x04004BDC RID: 19420
		public SocialUserIcon userAvatar;

		// Token: 0x04004BDD RID: 19421
		public TextMeshProUGUI nameLabel;

		// Token: 0x04004BDE RID: 19422
		public RawImage classIcon;

		// Token: 0x04004BDF RID: 19423
		public TextMeshProUGUI moneyText;

		// Token: 0x04004BE0 RID: 19424
		private CharacterMaster master;

		// Token: 0x04004BE1 RID: 19425
		private CharacterBody userBody;

		// Token: 0x04004BE2 RID: 19426
		private PlayerCharacterMasterController userPlayerCharacterMasterController;
	}
}
