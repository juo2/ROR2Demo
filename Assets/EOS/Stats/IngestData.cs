using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x02000094 RID: 148
	public class IngestData : ISettable
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x000064D5 File Offset: 0x000046D5
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x000064DD File Offset: 0x000046DD
		public string StatName { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x000064E6 File Offset: 0x000046E6
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x000064EE File Offset: 0x000046EE
		public int IngestAmount { get; set; }

		// Token: 0x0600055A RID: 1370 RVA: 0x000064F8 File Offset: 0x000046F8
		internal void Set(IngestDataInternal? other)
		{
			if (other != null)
			{
				this.StatName = other.Value.StatName;
				this.IngestAmount = other.Value.IngestAmount;
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00006538 File Offset: 0x00004738
		public void Set(object other)
		{
			this.Set(other as IngestDataInternal?);
		}
	}
}
