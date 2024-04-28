using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoR2.UI
{
	// Token: 0x02000D94 RID: 3476
	public class SteamLobbyOwnerFilter : MonoBehaviour
	{
		// Token: 0x06004F96 RID: 20374 RVA: 0x00149348 File Offset: 0x00147548
		private void Start()
		{
			this.Refresh(true);
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x00149351 File Offset: 0x00147551
		private void Update()
		{
			this.Refresh(false);
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x0014935C File Offset: 0x0014755C
		private void Refresh(bool forceRefresh = false)
		{
			bool ownsLobby = PlatformSystems.lobbyManager.ownsLobby;
			if (ownsLobby != this.wasOn || forceRefresh)
			{
				for (int i = 0; i < this.objectsToFilter.Length; i++)
				{
					this.objectsToFilter[i].SetActive(ownsLobby);
				}
				for (int j = 0; j < this.buttonsToFilter.Length; j++)
				{
					this.buttonsToFilter[j].interactable = ownsLobby;
				}
			}
			this.wasOn = ownsLobby;
		}

		// Token: 0x04004C3A RID: 19514
		public Button[] buttonsToFilter;

		// Token: 0x04004C3B RID: 19515
		public GameObject[] objectsToFilter;

		// Token: 0x04004C3C RID: 19516
		private bool wasOn;
	}
}
