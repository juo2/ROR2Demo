using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Rewired;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D3C RID: 3388
	public class LocalUserSignInController : MonoBehaviour
	{
		// Token: 0x06004D42 RID: 19778 RVA: 0x0013F200 File Offset: 0x0013D400
		private void Start()
		{
			LocalUserSignInCardController[] componentsInChildren = base.GetComponentsInChildren<LocalUserSignInCardController>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				this.cards.Add(componentsInChildren[i]);
			}
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x0013F230 File Offset: 0x0013D430
		public bool AreAllCardsReady()
		{
			return this.cards.Any((LocalUserSignInCardController v) => v.rewiredPlayer != null && v.requestedUserProfile == null);
		}

		// Token: 0x06004D44 RID: 19780 RVA: 0x0013F25C File Offset: 0x0013D45C
		private void DoSignIn()
		{
			LocalUserManager.LocalUserInitializationInfo[] array = new LocalUserManager.LocalUserInitializationInfo[this.cards.Count((LocalUserSignInCardController v) => v.rewiredPlayer != null)];
			int index = 0;
			for (int i = 0; i < this.cards.Count; i++)
			{
				if (this.cards[i].rewiredPlayer != null)
				{
					array[index++] = new LocalUserManager.LocalUserInitializationInfo
					{
						player = this.cards[index].rewiredPlayer,
						profile = this.cards[index].requestedUserProfile
					};
				}
			}
			LocalUserManager.SetLocalUsers(array);
		}

		// Token: 0x06004D45 RID: 19781 RVA: 0x0013F310 File Offset: 0x0013D510
		private LocalUserSignInCardController FindCardAssociatedWithRewiredPlayer(Player rewiredPlayer)
		{
			for (int i = 0; i < this.cards.Count; i++)
			{
				if (this.cards[i].rewiredPlayer == rewiredPlayer)
				{
					return this.cards[i];
				}
			}
			return null;
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x0013F358 File Offset: 0x0013D558
		private LocalUserSignInCardController FindCardWithoutRewiredPlayer()
		{
			for (int i = 0; i < this.cards.Count; i++)
			{
				if (this.cards[i].rewiredPlayer == null)
				{
					return this.cards[i];
				}
			}
			return null;
		}

		// Token: 0x06004D47 RID: 19783 RVA: 0x0013F39C File Offset: 0x0013D59C
		private void Update()
		{
			IList<Player> players = ReInput.players.Players;
			for (int i = 0; i < players.Count; i++)
			{
				Player player = players[i];
				if (!(player.name == "PlayerMain"))
				{
					LocalUserSignInCardController localUserSignInCardController = this.FindCardAssociatedWithRewiredPlayer(player);
					if (localUserSignInCardController == null)
					{
						if (player.GetButtonDown(11))
						{
							LocalUserSignInCardController localUserSignInCardController2 = this.FindCardWithoutRewiredPlayer();
							if (localUserSignInCardController2 != null)
							{
								localUserSignInCardController2.rewiredPlayer = player;
							}
						}
					}
					else if (player.GetButtonDown(15) || !LocalUserSignInController.PlayerHasControllerConnected(player))
					{
						localUserSignInCardController.rewiredPlayer = null;
					}
				}
			}
			ReadOnlyCollection<LocalUser> readOnlyLocalUsersList = LocalUserManager.readOnlyLocalUsersList;
			int num = 4;
			while (this.cards.Count < num)
			{
				this.cards.Add(UnityEngine.Object.Instantiate<GameObject>(this.localUserCardPrefab, base.transform).GetComponent<LocalUserSignInCardController>());
			}
			while (this.cards.Count > num)
			{
				UnityEngine.Object.Destroy(this.cards[this.cards.Count - 1].gameObject);
				this.cards.RemoveAt(this.cards.Count - 1);
			}
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x0013F4B8 File Offset: 0x0013D6B8
		private static bool PlayerHasControllerConnected(Player player)
		{
			using (IEnumerator<Controller> enumerator = player.controllers.Controllers.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Controller controller = enumerator.Current;
					return true;
				}
			}
			return false;
		}

		// Token: 0x04004A48 RID: 19016
		public GameObject localUserCardPrefab;

		// Token: 0x04004A49 RID: 19017
		private readonly List<LocalUserSignInCardController> cards = new List<LocalUserSignInCardController>();
	}
}
