using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000019 RID: 25
	public class PageQuery : ISettable
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000289 RID: 649 RVA: 0x000037EB File Offset: 0x000019EB
		// (set) Token: 0x0600028A RID: 650 RVA: 0x000037F3 File Offset: 0x000019F3
		public int StartIndex { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600028B RID: 651 RVA: 0x000037FC File Offset: 0x000019FC
		// (set) Token: 0x0600028C RID: 652 RVA: 0x00003804 File Offset: 0x00001A04
		public int MaxCount { get; set; }

		// Token: 0x0600028D RID: 653 RVA: 0x00003810 File Offset: 0x00001A10
		internal void Set(PageQueryInternal? other)
		{
			if (other != null)
			{
				this.StartIndex = other.Value.StartIndex;
				this.MaxCount = other.Value.MaxCount;
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00003850 File Offset: 0x00001A50
		public void Set(object other)
		{
			this.Set(other as PageQueryInternal?);
		}
	}
}
