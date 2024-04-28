using System;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020006E3 RID: 1763
	public class FrogController : MonoBehaviour
	{
		// Token: 0x060022DC RID: 8924 RVA: 0x000969B8 File Offset: 0x00094BB8
		public void Pet(Interactor interactor)
		{
			this.petCount++;
			if (!string.IsNullOrEmpty(this.petChatToken))
			{
				Chat.SendBroadcastChat(new SubjectChatMessage
				{
					subjectAsCharacterBody = interactor.GetComponent<CharacterBody>(),
					baseToken = this.petChatToken
				});
			}
			if (this.petCount >= this.maxPets && this.portalSpawner)
			{
				this.portalSpawner.AttemptSpawnPortalServer();
			}
		}

		// Token: 0x040027F4 RID: 10228
		[SerializeField]
		private int maxPets;

		// Token: 0x040027F5 RID: 10229
		[SerializeField]
		private PortalSpawner portalSpawner;

		// Token: 0x040027F6 RID: 10230
		[SerializeField]
		private PurchaseInteraction purchaseInteraction;

		// Token: 0x040027F7 RID: 10231
		[SerializeField]
		private string petChatToken;

		// Token: 0x040027F8 RID: 10232
		private int petCount;
	}
}
