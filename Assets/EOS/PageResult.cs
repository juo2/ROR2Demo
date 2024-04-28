using System;

namespace Epic.OnlineServices
{
	// Token: 0x0200001B RID: 27
	public class PageResult : ISettable
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000297 RID: 663 RVA: 0x000038B9 File Offset: 0x00001AB9
		// (set) Token: 0x06000298 RID: 664 RVA: 0x000038C1 File Offset: 0x00001AC1
		public int StartIndex { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000299 RID: 665 RVA: 0x000038CA File Offset: 0x00001ACA
		// (set) Token: 0x0600029A RID: 666 RVA: 0x000038D2 File Offset: 0x00001AD2
		public int Count { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600029B RID: 667 RVA: 0x000038DB File Offset: 0x00001ADB
		// (set) Token: 0x0600029C RID: 668 RVA: 0x000038E3 File Offset: 0x00001AE3
		public int TotalCount { get; set; }

		// Token: 0x0600029D RID: 669 RVA: 0x000038EC File Offset: 0x00001AEC
		internal void Set(PageResultInternal? other)
		{
			if (other != null)
			{
				this.StartIndex = other.Value.StartIndex;
				this.Count = other.Value.Count;
				this.TotalCount = other.Value.TotalCount;
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00003941 File Offset: 0x00001B41
		public void Set(object other)
		{
			this.Set(other as PageResultInternal?);
		}
	}
}
