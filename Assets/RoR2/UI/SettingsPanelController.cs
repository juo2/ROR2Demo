using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoR2.UI
{
	// Token: 0x02000D82 RID: 3458
	public class SettingsPanelController : MonoBehaviour
	{
		// Token: 0x06004F40 RID: 20288 RVA: 0x00147E69 File Offset: 0x00146069
		private void Start()
		{
			this.settingsControllers = base.GetComponentsInChildren<BaseSettingsControl>();
		}

		// Token: 0x06004F41 RID: 20289 RVA: 0x00147E78 File Offset: 0x00146078
		private void Update()
		{
			bool interactable = false;
			if (this.settingsControllers != null)
			{
				for (int i = 0; i < this.settingsControllers.Length; i++)
				{
					BaseSettingsControl baseSettingsControl = this.settingsControllers[i];
					if (baseSettingsControl && baseSettingsControl.hasBeenChanged)
					{
						interactable = true;
					}
				}
			}
			if (this.revertButton)
			{
				this.revertButton.interactable = interactable;
			}
		}

		// Token: 0x06004F42 RID: 20290 RVA: 0x00147ED8 File Offset: 0x001460D8
		public void RevertChanges()
		{
			if (base.isActiveAndEnabled)
			{
				for (int i = 0; i < this.settingsControllers.Length; i++)
				{
					this.settingsControllers[i].Revert();
				}
			}
		}

		// Token: 0x04004BF0 RID: 19440
		[FormerlySerializedAs("carouselControllers")]
		private BaseSettingsControl[] settingsControllers;

		// Token: 0x04004BF1 RID: 19441
		public MPButton revertButton;
	}
}
