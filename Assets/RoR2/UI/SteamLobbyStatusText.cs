using System;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D95 RID: 3477
	[RequireComponent(typeof(LanguageTextMeshController))]
	public class SteamLobbyStatusText : MonoBehaviour
	{
		// Token: 0x06004F9A RID: 20378 RVA: 0x001493CD File Offset: 0x001475CD
		private void Start()
		{
			this.languageTextMeshController = base.GetComponent<LanguageTextMeshController>();
		}

		// Token: 0x06004F9B RID: 20379 RVA: 0x001493DC File Offset: 0x001475DC
		private void Update()
		{
			LobbyType currentLobbyType = PlatformSystems.lobbyManager.currentLobbyType;
			for (int i = 0; i < LobbyUserList.lobbyStateChoices.Length; i++)
			{
				if (currentLobbyType == LobbyUserList.lobbyStateChoices[i].lobbyType)
				{
					this.languageTextMeshController.token = LobbyUserList.lobbyStateChoices[i].token;
					return;
				}
			}
		}

		// Token: 0x04004C3D RID: 19517
		private LanguageTextMeshController languageTextMeshController;
	}
}
