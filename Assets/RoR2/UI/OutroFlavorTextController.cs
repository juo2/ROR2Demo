using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000CAB RID: 3243
	public class OutroFlavorTextController : MonoBehaviour
	{
		// Token: 0x060049FA RID: 18938 RVA: 0x0012FDD8 File Offset: 0x0012DFD8
		protected void Start()
		{
			this.UpdateFlavorText();
		}

		// Token: 0x060049FB RID: 18939 RVA: 0x0012FDE0 File Offset: 0x0012DFE0
		protected void UpdateFlavorText()
		{
			string text = null;
			MPEventSystem eventSystem = base.GetComponent<MPEventSystemLocator>().eventSystem;
			LocalUser localUser = (eventSystem != null) ? eventSystem.localUser : null;
			GameOverController instance = GameOverController.instance;
			RunReport runReport = (instance != null) ? instance.runReport : null;
			if (localUser != null && runReport != null)
			{
				RunReport.PlayerInfo playerInfo = runReport.FindPlayerInfo(localUser);
				if (playerInfo != null)
				{
					SurvivorDef survivorDef = SurvivorCatalog.GetSurvivorDef(SurvivorCatalog.GetSurvivorIndexFromBodyIndex(playerInfo.bodyIndex));
					if (survivorDef)
					{
						if (playerInfo.isDead)
						{
							text = survivorDef.mainEndingEscapeFailureFlavorToken;
						}
						else
						{
							text = survivorDef.outroFlavorToken;
						}
					}
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "GENERIC_OUTRO_FLAVOR";
			}
			if (this.languageTextMeshController)
			{
				this.languageTextMeshController.token = text;
			}
		}

		// Token: 0x040046C1 RID: 18113
		public LanguageTextMeshController languageTextMeshController;
	}
}
