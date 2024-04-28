using System;

namespace RoR2
{
	// Token: 0x0200064E RID: 1614
	public interface IChestBehavior
	{
		// Token: 0x06001F48 RID: 8008
		bool HasRolledPickup(PickupIndex pickupIndex);

		// Token: 0x06001F49 RID: 8009
		void Roll();

		// Token: 0x06001F4A RID: 8010
		void ItemDrop();
	}
}
