using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000108 RID: 264
	public class SessionDetailsAttribute : ISettable
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00008559 File Offset: 0x00006759
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x00008561 File Offset: 0x00006761
		public AttributeData Data { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0000856A File Offset: 0x0000676A
		// (set) Token: 0x060007B0 RID: 1968 RVA: 0x00008572 File Offset: 0x00006772
		public SessionAttributeAdvertisementType AdvertisementType { get; set; }

		// Token: 0x060007B1 RID: 1969 RVA: 0x0000857C File Offset: 0x0000677C
		internal void Set(SessionDetailsAttributeInternal? other)
		{
			if (other != null)
			{
				this.Data = other.Value.Data;
				this.AdvertisementType = other.Value.AdvertisementType;
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000085BC File Offset: 0x000067BC
		public void Set(object other)
		{
			this.Set(other as SessionDetailsAttributeInternal?);
		}
	}
}
