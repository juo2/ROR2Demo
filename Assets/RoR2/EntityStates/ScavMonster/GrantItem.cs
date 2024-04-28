using System;
using RoR2;
using UnityEngine.Networking;

namespace EntityStates.ScavMonster
{
	// Token: 0x020001D9 RID: 473
	public class GrantItem : BaseState
	{
		// Token: 0x06000877 RID: 2167 RVA: 0x00023DB7 File Offset: 0x00021FB7
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				this.GrantPickupServer(this.dropPickup, this.itemsToGrant);
			}
			if (base.isAuthority)
			{
				this.outer.SetNextState(new ExitSit());
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00023DF0 File Offset: 0x00021FF0
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			writer.Write(this.dropPickup);
			writer.WritePackedUInt32((uint)this.itemsToGrant);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00023E11 File Offset: 0x00022011
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			this.dropPickup = reader.ReadPickupIndex();
			this.itemsToGrant = (int)reader.ReadPackedUInt32();
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00023E34 File Offset: 0x00022034
		private void GrantPickupServer(PickupIndex pickupIndex, int countToGrant)
		{
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			if (pickupDef == null)
			{
				return;
			}
			ItemIndex itemIndex = pickupDef.itemIndex;
			if (ItemCatalog.GetItemDef(itemIndex) == null)
			{
				return;
			}
			base.characterBody.inventory.GiveItem(itemIndex, countToGrant);
			Chat.SendBroadcastChat(new Chat.PlayerPickupChatMessage
			{
				subjectAsCharacterBody = base.characterBody,
				baseToken = "MONSTER_PICKUP",
				pickupToken = pickupDef.nameToken,
				pickupColor = pickupDef.baseColor,
				pickupQuantity = (uint)this.itemsToGrant
			});
		}

		// Token: 0x040009F8 RID: 2552
		public PickupIndex dropPickup;

		// Token: 0x040009F9 RID: 2553
		public int itemsToGrant;
	}
}
