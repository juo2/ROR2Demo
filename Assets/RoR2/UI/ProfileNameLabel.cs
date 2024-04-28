using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D6C RID: 3436
	[RequireComponent(typeof(MPEventSystemLocator))]
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class ProfileNameLabel : MonoBehaviour
	{
		// Token: 0x06004EC4 RID: 20164 RVA: 0x0014563D File Offset: 0x0014383D
		private void Awake()
		{
			this.eventSystemLocator = base.GetComponent<MPEventSystemLocator>();
			this.label = base.GetComponent<TextMeshProUGUI>();
		}

		// Token: 0x06004EC5 RID: 20165 RVA: 0x00145658 File Offset: 0x00143858
		private void LateUpdate()
		{
			MPEventSystem eventSystem = this.eventSystemLocator.eventSystem;
			string text;
			if (eventSystem == null)
			{
				text = null;
			}
			else
			{
				LocalUser localUser = eventSystem.localUser;
				text = ((localUser != null) ? localUser.userProfile.name : null);
			}
			string a = text ?? string.Empty;
			if (a != this.currentUserName)
			{
				this.currentUserName = a;
				this.label.text = Language.GetStringFormatted(this.token, new object[]
				{
					this.currentUserName
				});
			}
		}

		// Token: 0x04004B77 RID: 19319
		public string token;

		// Token: 0x04004B78 RID: 19320
		private MPEventSystemLocator eventSystemLocator;

		// Token: 0x04004B79 RID: 19321
		private TextMeshProUGUI label;

		// Token: 0x04004B7A RID: 19322
		private string currentUserName;
	}
}
