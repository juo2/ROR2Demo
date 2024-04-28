using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000A2 RID: 162
	public class Stat : ISettable
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00006A2F File Offset: 0x00004C2F
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x00006A37 File Offset: 0x00004C37
		public string Name { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00006A40 File Offset: 0x00004C40
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x00006A48 File Offset: 0x00004C48
		public DateTimeOffset? StartTime { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x00006A51 File Offset: 0x00004C51
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x00006A59 File Offset: 0x00004C59
		public DateTimeOffset? EndTime { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00006A62 File Offset: 0x00004C62
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x00006A6A File Offset: 0x00004C6A
		public int Value { get; set; }

		// Token: 0x060005BE RID: 1470 RVA: 0x00006A74 File Offset: 0x00004C74
		internal void Set(StatInternal? other)
		{
			if (other != null)
			{
				this.Name = other.Value.Name;
				this.StartTime = other.Value.StartTime;
				this.EndTime = other.Value.EndTime;
				this.Value = other.Value.Value;
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00006ADE File Offset: 0x00004CDE
		public void Set(object other)
		{
			this.Set(other as StatInternal?);
		}
	}
}
