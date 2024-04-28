using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020002FE RID: 766
	public class AttributeData : ISettable
	{
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x00014450 File Offset: 0x00012650
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x00014458 File Offset: 0x00012658
		public string Key { get; set; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x00014461 File Offset: 0x00012661
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x00014469 File Offset: 0x00012669
		public AttributeDataValue Value { get; set; }

		// Token: 0x06001314 RID: 4884 RVA: 0x00014474 File Offset: 0x00012674
		internal void Set(AttributeDataInternal? other)
		{
			if (other != null)
			{
				this.Key = other.Value.Key;
				this.Value = other.Value.Value;
			}
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000144B4 File Offset: 0x000126B4
		public void Set(object other)
		{
			this.Set(other as AttributeDataInternal?);
		}
	}
}
