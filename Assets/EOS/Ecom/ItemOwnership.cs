using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000469 RID: 1129
	public class ItemOwnership : ISettable
	{
		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x0001D49F File Offset: 0x0001B69F
		// (set) Token: 0x06001B8D RID: 7053 RVA: 0x0001D4A7 File Offset: 0x0001B6A7
		public string Id { get; set; }

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x0001D4B0 File Offset: 0x0001B6B0
		// (set) Token: 0x06001B8F RID: 7055 RVA: 0x0001D4B8 File Offset: 0x0001B6B8
		public OwnershipStatus OwnershipStatus { get; set; }

		// Token: 0x06001B90 RID: 7056 RVA: 0x0001D4C4 File Offset: 0x0001B6C4
		internal void Set(ItemOwnershipInternal? other)
		{
			if (other != null)
			{
				this.Id = other.Value.Id;
				this.OwnershipStatus = other.Value.OwnershipStatus;
			}
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0001D504 File Offset: 0x0001B704
		public void Set(object other)
		{
			this.Set(other as ItemOwnershipInternal?);
		}
	}
}
