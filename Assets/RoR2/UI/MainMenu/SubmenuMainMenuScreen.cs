using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoR2.UI.MainMenu
{
	// Token: 0x02000DCD RID: 3533
	public class SubmenuMainMenuScreen : BaseMainMenuScreen
	{
		// Token: 0x060050EB RID: 20715 RVA: 0x0014E8E3 File Offset: 0x0014CAE3
		public override void OnEnter(MainMenuController mainMenuController)
		{
			base.OnEnter(mainMenuController);
			this.submenuPanelInstance = UnityEngine.Object.Instantiate<GameObject>(this.submenuPanelPrefab, base.transform);
		}

		// Token: 0x060050EC RID: 20716 RVA: 0x0014E903 File Offset: 0x0014CB03
		public override void OnExit(MainMenuController mainMenuController)
		{
			UnityEngine.Object.Destroy(this.submenuPanelInstance);
			base.OnExit(mainMenuController);
		}

		// Token: 0x060050ED RID: 20717 RVA: 0x0014E917 File Offset: 0x0014CB17
		public new void Update()
		{
			if (!this.submenuPanelInstance && this.myMainMenuController)
			{
				this.myMainMenuController.SetDesiredMenuScreen(this.myMainMenuController.titleMenuScreen);
			}
		}

		// Token: 0x04004D81 RID: 19841
		[FormerlySerializedAs("settingsPanelPrefab")]
		public GameObject submenuPanelPrefab;

		// Token: 0x04004D82 RID: 19842
		private GameObject submenuPanelInstance;
	}
}
