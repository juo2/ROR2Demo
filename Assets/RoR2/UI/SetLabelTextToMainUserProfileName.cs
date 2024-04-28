using System;
using TMPro;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D81 RID: 3457
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class SetLabelTextToMainUserProfileName : MonoBehaviour
	{
		// Token: 0x06004F3A RID: 20282 RVA: 0x00147DC4 File Offset: 0x00145FC4
		private void Awake()
		{
			this.label = base.GetComponent<TextMeshProUGUI>();
		}

		// Token: 0x06004F3B RID: 20283 RVA: 0x00147DD2 File Offset: 0x00145FD2
		private void OnEnable()
		{
			this.Apply();
		}

		// Token: 0x06004F3C RID: 20284 RVA: 0x00147DDC File Offset: 0x00145FDC
		private void Apply()
		{
			LocalUser localUser = LocalUserManager.FindLocalUser(0);
			if (localUser != null)
			{
				string name = localUser.userProfile.name;
				this.label.text = string.Format(Language.GetString("TITLE_PROFILE"), name);
				return;
			}
			this.label.text = "NO USER";
		}

		// Token: 0x06004F3D RID: 20285 RVA: 0x00147E2B File Offset: 0x0014602B
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			Language.onCurrentLanguageChanged += SetLabelTextToMainUserProfileName.OnCurrentLanguageChanged;
		}

		// Token: 0x06004F3E RID: 20286 RVA: 0x00147E40 File Offset: 0x00146040
		private static void OnCurrentLanguageChanged()
		{
			SetLabelTextToMainUserProfileName[] array = UnityEngine.Object.FindObjectsOfType<SetLabelTextToMainUserProfileName>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Apply();
			}
		}

		// Token: 0x04004BEF RID: 19439
		private TextMeshProUGUI label;
	}
}
