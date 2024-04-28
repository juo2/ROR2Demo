using System;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x02000155 RID: 341
	public class PlayerSanction : ISettable
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x0000A57E File Offset: 0x0000877E
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x0000A586 File Offset: 0x00008786
		public long TimePlaced { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x0000A58F File Offset: 0x0000878F
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x0000A597 File Offset: 0x00008797
		public string Action { get; set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0000A5A0 File Offset: 0x000087A0
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x0000A5A8 File Offset: 0x000087A8
		public long TimeExpires { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0000A5B1 File Offset: 0x000087B1
		// (set) Token: 0x0600095D RID: 2397 RVA: 0x0000A5B9 File Offset: 0x000087B9
		public string ReferenceId { get; set; }

		// Token: 0x0600095E RID: 2398 RVA: 0x0000A5C4 File Offset: 0x000087C4
		internal void Set(PlayerSanctionInternal? other)
		{
			if (other != null)
			{
				this.TimePlaced = other.Value.TimePlaced;
				this.Action = other.Value.Action;
				this.TimeExpires = other.Value.TimeExpires;
				this.ReferenceId = other.Value.ReferenceId;
			}
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0000A62E File Offset: 0x0000882E
		public void Set(object other)
		{
			this.Set(other as PlayerSanctionInternal?);
		}
	}
}
