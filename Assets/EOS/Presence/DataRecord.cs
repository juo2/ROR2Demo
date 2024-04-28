using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200020B RID: 523
	public class DataRecord : ISettable
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0000EA38 File Offset: 0x0000CC38
		// (set) Token: 0x06000D9F RID: 3487 RVA: 0x0000EA40 File Offset: 0x0000CC40
		public string Key { get; set; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0000EA49 File Offset: 0x0000CC49
		// (set) Token: 0x06000DA1 RID: 3489 RVA: 0x0000EA51 File Offset: 0x0000CC51
		public string Value { get; set; }

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0000EA5C File Offset: 0x0000CC5C
		internal void Set(DataRecordInternal? other)
		{
			if (other != null)
			{
				this.Key = other.Value.Key;
				this.Value = other.Value.Value;
			}
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0000EA9C File Offset: 0x0000CC9C
		public void Set(object other)
		{
			this.Set(other as DataRecordInternal?);
		}
	}
}
