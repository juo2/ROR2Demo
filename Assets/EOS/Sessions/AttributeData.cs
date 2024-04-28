using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000B4 RID: 180
	public class AttributeData : ISettable
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x000070D5 File Offset: 0x000052D5
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x000070DD File Offset: 0x000052DD
		public string Key { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x000070E6 File Offset: 0x000052E6
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x000070EE File Offset: 0x000052EE
		public AttributeDataValue Value { get; set; }

		// Token: 0x06000610 RID: 1552 RVA: 0x000070F8 File Offset: 0x000052F8
		internal void Set(AttributeDataInternal? other)
		{
			if (other != null)
			{
				this.Key = other.Value.Key;
				this.Value = other.Value.Value;
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00007138 File Offset: 0x00005338
		public void Set(object other)
		{
			this.Set(other as AttributeDataInternal?);
		}
	}
}
