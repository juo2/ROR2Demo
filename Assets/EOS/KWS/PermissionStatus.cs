using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003F7 RID: 1015
	public class PermissionStatus : ISettable
	{
		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x00019C30 File Offset: 0x00017E30
		// (set) Token: 0x06001878 RID: 6264 RVA: 0x00019C38 File Offset: 0x00017E38
		public string Name { get; set; }

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x00019C41 File Offset: 0x00017E41
		// (set) Token: 0x0600187A RID: 6266 RVA: 0x00019C49 File Offset: 0x00017E49
		public KWSPermissionStatus Status { get; set; }

		// Token: 0x0600187B RID: 6267 RVA: 0x00019C54 File Offset: 0x00017E54
		internal void Set(PermissionStatusInternal? other)
		{
			if (other != null)
			{
				this.Name = other.Value.Name;
				this.Status = other.Value.Status;
			}
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x00019C94 File Offset: 0x00017E94
		public void Set(object other)
		{
			this.Set(other as PermissionStatusInternal?);
		}
	}
}
