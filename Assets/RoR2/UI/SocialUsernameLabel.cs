using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D8E RID: 3470
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class SocialUsernameLabel : MonoBehaviour
	{
		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06004F78 RID: 20344 RVA: 0x00148E01 File Offset: 0x00147001
		// (set) Token: 0x06004F79 RID: 20345 RVA: 0x00148E09 File Offset: 0x00147009
		public UserID userId
		{
			get
			{
				return this._userId;
			}
			set
			{
				this._userId = value;
			}
		}

		// Token: 0x06004F7A RID: 20346 RVA: 0x00148E12 File Offset: 0x00147012
		private void Awake()
		{
			this.textMeshComponent = base.GetComponent<TextMeshProUGUI>();
		}

		// Token: 0x06004F7B RID: 20347 RVA: 0x00148E20 File Offset: 0x00147020
		public virtual void Refresh()
		{
			if (this.textMeshComponent != null)
			{
				this.textMeshComponent.text = PlatformSystems.lobbyManager.GetUserDisplayName(this._userId);
			}
		}

		// Token: 0x04004C26 RID: 19494
		protected TextMeshProUGUI textMeshComponent;

		// Token: 0x04004C27 RID: 19495
		private UserID _userId;

		// Token: 0x04004C28 RID: 19496
		public int subPlayerIndex;
	}
}
