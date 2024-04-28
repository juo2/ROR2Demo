using System;

namespace RoR2.DirectionalSearch
{
	// Token: 0x02000C9F RID: 3231
	public struct PickupSearchFilter : IGenericDirectionalSearchFilter<GenericPickupController>
	{
		// Token: 0x060049D5 RID: 18901 RVA: 0x0012F3A7 File Offset: 0x0012D5A7
		public bool PassesFilter(GenericPickupController genericPickupController)
		{
			return !this.requireTransmutable || PickupTransmutationManager.GetAvailableGroupFromPickupIndex(genericPickupController.pickupIndex) != null;
		}

		// Token: 0x04004672 RID: 18034
		public bool requireTransmutable;
	}
}
