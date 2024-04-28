using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001E6 RID: 486
	public class ParticipantMetadata : ISettable
	{
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0000DC80 File Offset: 0x0000BE80
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x0000DC88 File Offset: 0x0000BE88
		public string Key { get; set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0000DC91 File Offset: 0x0000BE91
		// (set) Token: 0x06000CDA RID: 3290 RVA: 0x0000DC99 File Offset: 0x0000BE99
		public string Value { get; set; }

		// Token: 0x06000CDB RID: 3291 RVA: 0x0000DCA4 File Offset: 0x0000BEA4
		internal void Set(ParticipantMetadataInternal? other)
		{
			if (other != null)
			{
				this.Key = other.Value.Key;
				this.Value = other.Value.Value;
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0000DCE4 File Offset: 0x0000BEE4
		public void Set(object other)
		{
			this.Set(other as ParticipantMetadataInternal?);
		}
	}
}
