using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000435 RID: 1077
	public class CatalogRelease : ISettable
	{
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x0001BB7D File Offset: 0x00019D7D
		// (set) Token: 0x06001A45 RID: 6725 RVA: 0x0001BB85 File Offset: 0x00019D85
		public string[] CompatibleAppIds { get; set; }

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x0001BB8E File Offset: 0x00019D8E
		// (set) Token: 0x06001A47 RID: 6727 RVA: 0x0001BB96 File Offset: 0x00019D96
		public string[] CompatiblePlatforms { get; set; }

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x0001BB9F File Offset: 0x00019D9F
		// (set) Token: 0x06001A49 RID: 6729 RVA: 0x0001BBA7 File Offset: 0x00019DA7
		public string ReleaseNote { get; set; }

		// Token: 0x06001A4A RID: 6730 RVA: 0x0001BBB0 File Offset: 0x00019DB0
		internal void Set(CatalogReleaseInternal? other)
		{
			if (other != null)
			{
				this.CompatibleAppIds = other.Value.CompatibleAppIds;
				this.CompatiblePlatforms = other.Value.CompatiblePlatforms;
				this.ReleaseNote = other.Value.ReleaseNote;
			}
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x0001BC05 File Offset: 0x00019E05
		public void Set(object other)
		{
			this.Set(other as CatalogReleaseInternal?);
		}
	}
}
