using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2.UI
{
	// Token: 0x02000DAB RID: 3499
	[RequireComponent(typeof(MPButton))]
	public class UserProfileListElementController : MonoBehaviour
	{
		// Token: 0x0600502E RID: 20526 RVA: 0x0014BE37 File Offset: 0x0014A037
		private void Awake()
		{
			this.button = base.GetComponent<MPButton>();
			this.button.onClick.AddListener(new UnityAction(this.InformListControllerOfSelection));
		}

		// Token: 0x0600502F RID: 20527 RVA: 0x0014BE61 File Offset: 0x0014A061
		private void InformListControllerOfSelection()
		{
			if (!this.userProfile.isCorrupted)
			{
				this.listController.SendProfileSelection(this.userProfile);
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06005030 RID: 20528 RVA: 0x0014BE81 File Offset: 0x0014A081
		// (set) Token: 0x06005031 RID: 20529 RVA: 0x0014BE8C File Offset: 0x0014A08C
		public UserProfile userProfile
		{
			get
			{
				return this._userProfile;
			}
			set
			{
				if (this._userProfile == value)
				{
					return;
				}
				this._userProfile = value;
				string sourceText = "???";
				uint num = 0U;
				if (this._userProfile != null)
				{
					sourceText = this._userProfile.name;
					num = this._userProfile.totalLoginSeconds;
				}
				if (this.nameLabel)
				{
					this.nameLabel.SetText(sourceText, true);
				}
				if (this.playTimeLabel)
				{
					TimeSpan timeSpan = TimeSpan.FromSeconds(num);
					this.playTimeLabel.SetText(string.Format("{0}:{1:D2}", (uint)timeSpan.TotalHours, (uint)timeSpan.Minutes), true);
				}
			}
		}

		// Token: 0x04004CD8 RID: 19672
		public TextMeshProUGUI nameLabel;

		// Token: 0x04004CD9 RID: 19673
		private MPButton button;

		// Token: 0x04004CDA RID: 19674
		public TextMeshProUGUI playTimeLabel;

		// Token: 0x04004CDB RID: 19675
		[NonSerialized]
		public UserProfileListController listController;

		// Token: 0x04004CDC RID: 19676
		private UserProfile _userProfile;
	}
}
