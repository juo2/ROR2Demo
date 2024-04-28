using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000505 RID: 1285
	public class UserLoginInfo : ISettable
	{
		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x000208E0 File Offset: 0x0001EAE0
		// (set) Token: 0x06001EFF RID: 7935 RVA: 0x000208E8 File Offset: 0x0001EAE8
		public string DisplayName { get; set; }

		// Token: 0x06001F00 RID: 7936 RVA: 0x000208F4 File Offset: 0x0001EAF4
		internal void Set(UserLoginInfoInternal? other)
		{
			if (other != null)
			{
				this.DisplayName = other.Value.DisplayName;
			}
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x0002091F File Offset: 0x0001EB1F
		public void Set(object other)
		{
			this.Set(other as UserLoginInfoInternal?);
		}
	}
}
