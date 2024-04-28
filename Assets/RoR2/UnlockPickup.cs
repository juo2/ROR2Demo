using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008DE RID: 2270
	[RequireComponent(typeof(EntityLocator))]
	public class UnlockPickup : MonoBehaviour
	{
		// Token: 0x060032F1 RID: 13041 RVA: 0x000D6A92 File Offset: 0x000D4C92
		private void FixedUpdate()
		{
			this.stopWatch += Time.fixedDeltaTime;
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x000D6AA8 File Offset: 0x000D4CA8
		private void GrantPickup(GameObject activator)
		{
			if (Run.instance)
			{
				Util.PlaySound(UnlockPickup.itemPickupSoundString, activator);
				string text = this.unlockableName;
				if (!this.unlockableDef && !string.IsNullOrEmpty(text))
				{
					this.unlockableDef = UnlockableCatalog.GetUnlockableDef(text);
				}
				Run.instance.GrantUnlockToAllParticipatingPlayers(this.unlockableDef);
				string pickupToken = "???";
				if (this.unlockableDef)
				{
					pickupToken = this.unlockableDef.nameToken;
				}
				Chat.SendBroadcastChat(new Chat.PlayerPickupChatMessage
				{
					subjectAsCharacterBody = activator.GetComponent<CharacterBody>(),
					baseToken = "PLAYER_PICKUP",
					pickupToken = pickupToken,
					pickupColor = ColorCatalog.GetColor(ColorCatalog.ColorIndex.Unlockable),
					pickupQuantity = 1U
				});
				this.consumed = true;
				GameObject obj = base.gameObject;
				EntityLocator component = base.GetComponent<EntityLocator>();
				if (component.entity)
				{
					obj = component.entity;
				}
				UnityEngine.Object.Destroy(obj);
			}
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x0009C5F1 File Offset: 0x0009A7F1
		private static bool BodyHasPickupPermission(CharacterBody body)
		{
			return (body.masterObject ? body.masterObject.GetComponent<PlayerCharacterMasterController>() : null) && body.inventory;
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x000D6B94 File Offset: 0x000D4D94
		private void OnTriggerStay(Collider other)
		{
			if (NetworkServer.active && this.stopWatch >= this.waitDuration && !this.consumed)
			{
				CharacterBody component = other.GetComponent<CharacterBody>();
				if (component)
				{
					TeamComponent component2 = component.GetComponent<TeamComponent>();
					if (component2 && component2.teamIndex == TeamIndex.Player && component.inventory)
					{
						this.GrantPickup(component.gameObject);
					}
				}
			}
		}

		// Token: 0x04003402 RID: 13314
		public static string itemPickupSoundString = "Play_UI_item_pickup";

		// Token: 0x04003403 RID: 13315
		private bool consumed;

		// Token: 0x04003404 RID: 13316
		private float stopWatch;

		// Token: 0x04003405 RID: 13317
		public float waitDuration = 0.5f;

		// Token: 0x04003406 RID: 13318
		public string displayNameToken;

		// Token: 0x04003407 RID: 13319
		[Obsolete("'unlockableName' will be discontinued. Use 'unlockableDef' instead.", false)]
		[Tooltip("'unlockableName' will be discontinued. Use 'unlockableDef' instead.")]
		public string unlockableName;

		// Token: 0x04003408 RID: 13320
		public UnlockableDef unlockableDef;
	}
}
