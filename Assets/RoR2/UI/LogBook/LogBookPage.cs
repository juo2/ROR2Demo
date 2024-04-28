using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI.LogBook
{
	// Token: 0x02000DF3 RID: 3571
	public class LogBookPage : MonoBehaviour
	{
		// Token: 0x060051F7 RID: 20983 RVA: 0x001530A8 File Offset: 0x001512A8
		public void SetEntry(UserProfile userProfile, Entry entry)
		{
			PageBuilder pageBuilder = this.pageBuilder;
			if (pageBuilder != null)
			{
				pageBuilder.Destroy();
			}
			this.pageBuilder = new PageBuilder();
			this.pageBuilder.container = this.contentContainer;
			this.pageBuilder.entry = entry;
			this.pageBuilder.userProfile = userProfile;
			Action<PageBuilder> pageBuilderMethod = entry.pageBuilderMethod;
			if (pageBuilderMethod != null)
			{
				pageBuilderMethod(this.pageBuilder);
			}
			this.iconImage.texture = entry.iconTexture;
			this.titleText.text = entry.GetDisplayName(userProfile);
			this.categoryText.text = entry.GetCategoryDisplayName(userProfile);
			this.modelPanel.modelPrefab = entry.modelPrefab;
			this.modelPanel.transform.parent.parent.gameObject.SetActive(entry.modelPrefab);
		}

		// Token: 0x04004E50 RID: 20048
		public RawImage iconImage;

		// Token: 0x04004E51 RID: 20049
		public ModelPanel modelPanel;

		// Token: 0x04004E52 RID: 20050
		public TextMeshProUGUI titleText;

		// Token: 0x04004E53 RID: 20051
		public TextMeshProUGUI categoryText;

		// Token: 0x04004E54 RID: 20052
		public TextMeshProUGUI pageNumberText;

		// Token: 0x04004E55 RID: 20053
		public RectTransform contentContainer;

		// Token: 0x04004E56 RID: 20054
		private PageBuilder pageBuilder;
	}
}
