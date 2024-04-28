using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200046B RID: 1131
	public class KeyImageInfo : ISettable
	{
		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x0001D594 File Offset: 0x0001B794
		// (set) Token: 0x06001B9B RID: 7067 RVA: 0x0001D59C File Offset: 0x0001B79C
		public string Type { get; set; }

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x0001D5A5 File Offset: 0x0001B7A5
		// (set) Token: 0x06001B9D RID: 7069 RVA: 0x0001D5AD File Offset: 0x0001B7AD
		public string Url { get; set; }

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x0001D5B6 File Offset: 0x0001B7B6
		// (set) Token: 0x06001B9F RID: 7071 RVA: 0x0001D5BE File Offset: 0x0001B7BE
		public uint Width { get; set; }

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x0001D5C7 File Offset: 0x0001B7C7
		// (set) Token: 0x06001BA1 RID: 7073 RVA: 0x0001D5CF File Offset: 0x0001B7CF
		public uint Height { get; set; }

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0001D5D8 File Offset: 0x0001B7D8
		internal void Set(KeyImageInfoInternal? other)
		{
			if (other != null)
			{
				this.Type = other.Value.Type;
				this.Url = other.Value.Url;
				this.Width = other.Value.Width;
				this.Height = other.Value.Height;
			}
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0001D642 File Offset: 0x0001B842
		public void Set(object other)
		{
			this.Set(other as KeyImageInfoInternal?);
		}
	}
}
