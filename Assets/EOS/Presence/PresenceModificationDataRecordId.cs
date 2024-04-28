using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200021F RID: 543
	public class PresenceModificationDataRecordId : ISettable
	{
		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x0000F6C8 File Offset: 0x0000D8C8
		// (set) Token: 0x06000E39 RID: 3641 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
		public string Key { get; set; }

		// Token: 0x06000E3A RID: 3642 RVA: 0x0000F6DC File Offset: 0x0000D8DC
		internal void Set(PresenceModificationDataRecordIdInternal? other)
		{
			if (other != null)
			{
				this.Key = other.Value.Key;
			}
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0000F707 File Offset: 0x0000D907
		public void Set(object other)
		{
			this.Set(other as PresenceModificationDataRecordIdInternal?);
		}
	}
}
