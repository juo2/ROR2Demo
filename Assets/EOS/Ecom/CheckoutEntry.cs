using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000439 RID: 1081
	public class CheckoutEntry : ISettable
	{
		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x0001BE54 File Offset: 0x0001A054
		// (set) Token: 0x06001A68 RID: 6760 RVA: 0x0001BE5C File Offset: 0x0001A05C
		public string OfferId { get; set; }

		// Token: 0x06001A69 RID: 6761 RVA: 0x0001BE68 File Offset: 0x0001A068
		internal void Set(CheckoutEntryInternal? other)
		{
			if (other != null)
			{
				this.OfferId = other.Value.OfferId;
			}
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x0001BE93 File Offset: 0x0001A093
		public void Set(object other)
		{
			this.Set(other as CheckoutEntryInternal?);
		}
	}
}
