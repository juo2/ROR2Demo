using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000AC RID: 172
	public class ActiveSessionInfo : ISettable
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00006EBD File Offset: 0x000050BD
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x00006EC5 File Offset: 0x000050C5
		public string SessionName { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00006ECE File Offset: 0x000050CE
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x00006ED6 File Offset: 0x000050D6
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00006EDF File Offset: 0x000050DF
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x00006EE7 File Offset: 0x000050E7
		public OnlineSessionState State { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00006EF0 File Offset: 0x000050F0
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x00006EF8 File Offset: 0x000050F8
		public SessionDetailsInfo SessionDetails { get; set; }

		// Token: 0x060005F2 RID: 1522 RVA: 0x00006F04 File Offset: 0x00005104
		internal void Set(ActiveSessionInfoInternal? other)
		{
			if (other != null)
			{
				this.SessionName = other.Value.SessionName;
				this.LocalUserId = other.Value.LocalUserId;
				this.State = other.Value.State;
				this.SessionDetails = other.Value.SessionDetails;
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00006F6E File Offset: 0x0000516E
		public void Set(object other)
		{
			this.Set(other as ActiveSessionInfoInternal?);
		}
	}
}
